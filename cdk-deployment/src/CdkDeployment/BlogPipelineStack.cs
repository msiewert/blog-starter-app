using Amazon.CDK;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.Pipelines;
using Constructs;
using Amazon.CDK.AWS.S3;

namespace CdkDeployment
{
    public class BlogPipelineStack : Stack
    {
        public BlogPipelineStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Create the pipeline
            var pipeline = new CodePipeline(this, "BlogPipeline", new CodePipelineProps
            {
                PipelineName = "NextJsBlogPipeline",
                CodeBuildDefaults = new CodeBuildOptions
                {
                    BuildEnvironment = new BuildEnvironment
                    {
                        BuildImage = LinuxBuildImage.STANDARD_7_0,
                        ComputeType = ComputeType.SMALL
                    },
                    PartialBuildSpec = BuildSpec.FromObject(new Dictionary<string, object>
                    {
                        ["version"] = "0.2",
                        ["phases"] = new Dictionary<string, object>
                        {
                            ["install"] = new Dictionary<string, object>
                            {
                                ["runtime-versions"] = new Dictionary<string, string>
                                {
                                    ["dotnet"] = "8.0",
                                    ["nodejs"] = "20"
                                }
                            }
                        }
                    })
                },
                Synth = new ShellStep("Synth", new ShellStepProps
                {
                    Input = CodePipelineSource.GitHub("msiewert/blog-starter-app", "main", new GitHubSourceOptions
                    {
                        Authentication = SecretValue.SecretsManager("github-token")
                    }),
                    Commands = new[]
                    {
                        "dotnet --version",
                        "dotnet --list-sdks",
                        "dotnet --version",
                        "cd cdk-deployment/src/CdkDeployment",
                        "dotnet restore",
                        "dotnet build",
                        "cd ../../..",
                        "npx cdk synth --app \"dotnet run --project cdk-deployment/src/CdkDeployment/CdkDeployment.csproj\""
                    },
                    PrimaryOutputDirectory = "cdk.out"
                })
            });

            // Add deployment stage
            var deployStage = new BlogDeploymentStage(this, "Deploy", new StageProps
            {
                Env = props?.Env
            });

            var deployStageAdded = pipeline.AddStage(deployStage);

            // Add Next.js build and deploy step
            var deployNextJSStep = new CodeBuildStep("DeployNextJS", new CodeBuildStepProps
            {
                Commands = new[]
                {
                    "node --version",
                    "npm --version",
                    "pwd",
                    "ls -la",
                    "echo BUCKET_NAME=$BUCKET_NAME",
                    "echo DISTRIBUTION_ID=$DISTRIBUTION_ID",
                    "npm ci",
                    "npm run build",
                    "ls -la out/",
                    "aws s3 sync out/ s3://$BUCKET_NAME --delete",
                    "aws cloudfront create-invalidation --distribution-id $DISTRIBUTION_ID --paths \"/*\""
                },
                EnvFromCfnOutputs = new Dictionary<string, CfnOutput>
                {
                    ["BUCKET_NAME"] = deployStage.BucketNameOutput,
                    ["DISTRIBUTION_ID"] = deployStage.DistributionIdOutput
                },
                RolePolicyStatements = new[]
                {
                    new PolicyStatement(new PolicyStatementProps
                    {
                        Effect = Effect.ALLOW,
                        Actions = new[] { "s3:GetObject", "s3:PutObject", "s3:DeleteObject", "s3:ListBucket" },
                        Resources = new[] { "arn:aws:s3:::nextjs-blog-*", "arn:aws:s3:::nextjs-blog-*/*" }
                    }),
                    new PolicyStatement(new PolicyStatementProps
                    {
                        Effect = Effect.ALLOW,
                        Actions = new[] { "cloudfront:CreateInvalidation" },
                        Resources = new[] { "*" }
                    })
                }
            });

            deployStageAdded.AddPost(deployNextJSStep);

            // Output pipeline name
            // new CfnOutput(this, "PipelineName", new CfnOutputProps
            // {
            //     Value = pipeline.Pipeline.PipelineName,
            //     Description = "CodePipeline name"
            // });
        }
    }

    public class BlogDeploymentStage : Stage
    {
        public CfnOutput BucketNameOutput { get; }
        public CfnOutput DistributionIdOutput { get; }
        public Bucket WebsiteBucket { get; }

        public BlogDeploymentStage(Construct scope, string id, IStageProps props = null) : base(scope, id, props)
        {
            var infraStack = new BlogInfrastructureStack(this, "BlogInfrastructure");

            BucketNameOutput = infraStack.BucketNameOutput;
            DistributionIdOutput = infraStack.DistributionIdOutput;
            WebsiteBucket = infraStack.WebsiteBucket;
        }
    }
}