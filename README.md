# Next.js Blog Starter - AWS Deployment

A statically generated blog using Next.js, Markdown, and TypeScript, deployed to AWS.

**Live Demo:** [https://d2d4nyf8x6q72s.cloudfront.net](https://d2d4nyf8x6q72s.cloudfront.net)

## AWS Deployment Strategy

**Architecture Diagram:** [View on draw.io](./docs/aws-architecture-diagram.drawio)

### How the Pipeline Deploys This Application

This deployment is **fully automated** - the CDK pipeline handles all infrastructure provisioning and application deployment without manual intervention.

**Infrastructure Deployment (Automated):**
- CDK pipeline creates S3 bucket with static website hosting
- Configures bucket policy for public read access
- Provisions CloudFront distribution with S3 origin
- Sets up custom error pages and caching policies
- Enables HTTPS with default CloudFront certificate
- Creates IAM roles with least-privilege permissions

**Application Deployment (Automated):**
- Pipeline triggers on Git push to main branch
- CDK synthesizes and deploys infrastructure changes
- CodeBuild builds Next.js application
- Syncs static files to S3 bucket
- Invalidates CloudFront cache for immediate updates

### Components
- **Amazon S3**: Static file hosting with website configuration
- **Amazon CloudFront**: Global CDN with edge caching and HTTPS
- **AWS CodePipeline**: CI/CD orchestration with GitHub integration
- **AWS CodeBuild**: Automated build environment with Node.js runtime
- **AWS CDK**: Infrastructure as Code for automated provisioning

**Next.js Configuration:**
- Static export enabled via `output: 'export'` in next.config.js
- Trailing slashes and unoptimized images for static hosting
- Build generates static files to /out directory

## Build Pipeline Details

### Pipeline Architecture

**Source Stage:**
- **Tool**: AWS CodePipeline with GitHub integration
- **Trigger**: Webhook on push to main branch
- **Action**: Downloads source code and passes to build stage

**Build Stage:**
- **Tool**: AWS CodeBuild
- **Runtime**: Amazon Linux 2, Node.js 18
- **Build Commands**:
  ```bash
  # Install dependencies with clean install
  npm ci
  
  # Build Next.js application (includes static export)
  npm run build
  ```
- **Artifacts**: Static files in /out directory

**Deploy Stage:**
- **Tool**: AWS CodeBuild Step in CDK Pipeline
- **Actions**:
  1. Sync /out directory to S3 bucket (`aws s3 sync out/ s3://$BUCKET_NAME --delete`)
  2. Invalidate CloudFront cache (`aws cloudfront create-invalidation --distribution-id $DISTRIBUTION_ID --paths "/*"`)
  3. Environment variables passed from CDK stack outputs

### Build Tools & Configuration

**AWS CDK Pipeline:**
- **Environment**: `LinuxBuildImage.STANDARD_7_0`
- **Compute**: `ComputeType.SMALL`
- **Runtime**: Node.js 20, .NET 8.0
- **Infrastructure as Code**: C# CDK constructs

**GitHub Integration:**
- **Source**: GitHub repository with AWS Secrets Manager token
- **Trigger**: Push to main branch
- **Pipeline**: CDK Pipelines with self-mutating capability

**Deployment Automation:**
- **S3 Sync**: `aws s3 sync out/ s3://$BUCKET_NAME --delete`
- **Cache Invalidation**: `aws cloudfront create-invalidation --distribution-id $DISTRIBUTION_ID --paths "/*"`
- **IAM Permissions**: Scoped S3 and CloudFront permissions via CDK

### Why This Architecture?
- **Cost-effective**: Pay only for storage and data transfer
- **Scalable**: CloudFront handles global traffic spikes
- **Fast**: Edge locations serve content closest to users
- **Reliable**: 99.99% SLA with managed AWS services
