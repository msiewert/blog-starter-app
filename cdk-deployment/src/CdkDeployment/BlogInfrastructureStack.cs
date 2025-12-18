using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.CloudFront.Origins;
using Constructs;

namespace CdkDeployment
{
    public class BlogInfrastructureStack : Stack
    {
        public CfnOutput BucketNameOutput { get; }
        public CfnOutput DistributionIdOutput { get; }

        public BlogInfrastructureStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // S3 bucket for hosting static website
            var websiteBucket = new Bucket(this, "BlogWebsiteBucket", new BucketProps
            {
                BucketName = $"nextjs-blog-{Account}-{Region}",
                WebsiteIndexDocument = "index.html",
                WebsiteErrorDocument = "404.html",
                PublicReadAccess = true,
                BlockPublicAccess = BlockPublicAccess.BLOCK_ACLS,
                RemovalPolicy = RemovalPolicy.DESTROY,
                AutoDeleteObjects = true
            });

            // CloudFront Origin Access Identity
            var originAccessIdentity = new OriginAccessIdentity(this, "BlogOAI", new OriginAccessIdentityProps
            {
                Comment = "OAI for Next.js Blog"
            });

            // Grant CloudFront access to S3 bucket
            websiteBucket.GrantRead(originAccessIdentity);

            // CloudFront distribution
            var distribution = new Distribution(this, "BlogDistribution", new DistributionProps
            {
                DefaultBehavior = new BehaviorOptions
                {
                    Origin = new S3Origin(websiteBucket, new S3OriginProps
                    {
                        OriginAccessIdentity = originAccessIdentity
                    }),
                    ViewerProtocolPolicy = ViewerProtocolPolicy.REDIRECT_TO_HTTPS,
                    CachePolicy = CachePolicy.CACHING_OPTIMIZED
                },
                DefaultRootObject = "index.html",
                ErrorResponses = new[]
                {
                    new ErrorResponse
                    {
                        HttpStatus = 404,
                        ResponseHttpStatus = 404,
                        ResponsePagePath = "/404.html"
                    }
                }
            });

            // Output the CloudFront URL
            new CfnOutput(this, "BlogUrl", new CfnOutputProps
            {
                Value = $"https://{distribution.DistributionDomainName}",
                Description = "URL of the Next.js blog"
            });

            // Output the S3 bucket name for pipeline use
            BucketNameOutput = new CfnOutput(this, "BucketName", new CfnOutputProps
            {
                Value = websiteBucket.BucketName,
                Description = "S3 bucket name for deployment",
                ExportName = "BlogBucketName"
            });

            // Output the CloudFront distribution ID for cache invalidation
            DistributionIdOutput = new CfnOutput(this, "DistributionId", new CfnOutputProps
            {
                Value = distribution.DistributionId,
                Description = "CloudFront distribution ID",
                ExportName = "BlogDistributionId"
            });
        }
    }
}