# Next.js Blog Starter - AWS Deployment

A statically generated blog using Next.js, Markdown, and TypeScript, deployed to AWS.

**Live Demo:** [https://d2d4nyf8x6q72s.cloudfront.net](https://d2d4nyf8x6q72s.cloudfront.net)

## AWS Deployment Approach

Static site hosting using S3 + CloudFront with automated CI/CD pipeline:
- **Infrastructure**: AWS CDK (C#) provisions S3 bucket and CloudFront distribution
- **Pipeline**: Self-mutating CodePipeline triggered by GitHub commits
- **Build**: CodeBuild compiles Next.js to static files and deploys to S3
- **Delivery**: CloudFront CDN serves content globally with HTTPS

## AWS Architecture

**Architecture Diagram:**

![AWS Architecture Diagram](./docs/architecture-diagram.png)

[View on draw.io](https://app.diagrams.net/?tags=%7B%7D&lightbox=1&highlight=0000ff&edit=_blank&layers=1&nav=1&title=aws-architecture-diagram.drawio&dark=auto#R%3Cmxfile%3E%3Cdiagram%20name%3D%22AWS-Architecture%22%20id%3D%22aws-nextjs-blog%22%3E7Vxbd5s4EP41Pmf3ITkyN5NHX9q028vmbNJt%2B7RHgGzUYsSCiO3%2B%2BpWMMDfFIQQwzuYhNhqEkL5vNJqRxhmp8%2FX2OoSB%2B4k4yBspwNmO1MVIURRgGOyLS3aJZDwGaiJZhdgRskxwi38hIQRCGmMHRYWKlBCP4qAotInvI5sWZDAMyaZYbUm84lsDuEIVwa0Nvar0K3aomw7DuMpuvEN45YpXm8okubGGaWUxksiFDtnkROqbkToPCaHJ1Xo7Rx5HL8Ulee7tA3cPHQuRT%2Bs8QDFlg6o8JNqJ6C4dMkVbdm%2Fm0rXHBGN2GdGQ%2FERz4pGQSXzis5qzJfa8kgh6eOWzos1aR0w%2Bu0chxQzMqbixxo7DXzPbuJii2wDa%2FJ0bpjtMFpLYdxDvLeDNE58KdRibaVl0kvcp6fY99GLR7c%2Bs25c%2FIiaceWTFvi7Y3%2FTrLftcoMAjuzUfMxOFNn%2B5TeNQwMF7ibY5OASA14isEQ13rIqbo1gVhG4yfVAnQiZa0YAo74qPQKGNq0PLGWHsQnAm589B98gjAcO1BodM2QJ%2BGa%2B9qU1JnouP0ELeDYkwxYRzYhFKyVpCFiVBUQ1ITD3sMz1IpxooqcFIUR2IzKVd0Rl2x7BNZC1lzC2KI3siHYaEjiIbZpGMsa4%2Fn40Vpm5s1aEiU%2BvxA4qfg7iE5tK0kS1F0zJ1TQcyNK8xfcd7Bv5CAWeZiIE9FVetiutYKQKrlNR8bLSALNxEF7ZHYqdzcJdLRQ6uY1iGbrA7DozcQ%2Bu8cAMps2x8drCRAl2GfyOwdVBFewKKaKtlPVZasCoba%2FrnB6zf%2FfHFhgD88%2BPvu6l6IXBGTmVtrHJB4tAWtWy2%2FAc4QNxIVJjijaX2m4TUJSviQ%2B9NJi3Z%2F6zOR8IN0Z6CH4jSnVgVYExJkV%2FhHsBwhUQnI1VKT5WLEHmQ4vvicJ%2BDaxWL7hQZjR0dTWSKfGVMVGjItDRZGOeslzeHXoLf5osP7CuV%2FN5QlR%2B1G5pSthstaDJH3Iqx17ndeB7cs6SL4DO7TtwVDgZIxSPFgOtg34RwW9paFMskGGXj3caymE647tBn2JuOJkPfVCzVkKO%2Fhr%2BYr6OAW5V%2FUDbVbXbxFVlsfeSK%2F45EFPurrqAu67sK2tB3vkYuQ8Kd2gFDPufdfJt0E1x7xIJ8EPPFZw773d3NbV%2F63Q7ozs8Bm5e99X7vL0PIno1FhMPHm5ierpA2u7AkGK67RrqGA%2FgA0u%2Bnn7iDTTwUlWz2DfGwjbm4H6xb0eo4QmFUB%2B2zCisPxuZLNrzWY0tD0wt8aHoLfOw3rho64aVtglN74Pk4OccNE%2FOhxJErJ6F1f3wPqdIM0vwYTo1nNa7IobpBlkvIzz4hVZtBejRU7Ai2XHCQw4yGeLXiswUc7tfE7rDaPQylaOmG4L37k1Yhy2WEaAXrwwvrw681hz8Hx8mVOvWqcrw4IvgBmPszZ0aL0YyWNHo6OR%2Bl0CJHC2HTBft9WpjJGap4Yd%2FwSfhinxWwA%2FdhqQ1tF%2FWJtdkQ63T%2BDgrlQwSRg9cOEYM26hPTq2aY5nzyAZsDEcEz6P6NUUT7Ala6a62YDwP7erpZN%2BQ50emmnNGrOoyeVVB6jmedUm7S974efrZ5%2BCmH%2BsiK8XoUWuCmfewb7hnIG9NOsaCjLabf%2BOOXuih9z91ZbEXL%2B8JOFKpOgHxAupTzZ3gA%2B0enYQh3uQoBD9TyvkUauaWbcEZRHVKj%2BPaB%2Bpp5tD67SHrQNAaUYqUXM9WglaqAxJAeVcWS9uzXuCLj9T2VEEX4l%2BgK510gzRrXZyN9IZvUh8BcRFcAsraX0Kb1t1il2iAeuQCXAIzVAj8XjylMR7G8nIbOjzhfz%2FMftaBDBf9ln%2B4fXQMGev78ko785fgbg8b%2FZZ3%2FSxnQGm6byemcSFf5rn1EnwHxLV%2F4nnmMvJi5ifvSU%2F1E4xRuoVl2C%2FXjbuHkmfXVXtxI8HKUDVxO9IK%2BPaJth59QPDlEGbrq6eOj9btRpZT9QTpTLymXSY5%2Bre3q1%2Fymzvyml7e5fL4ZT3K%2Fqs2lLj1LOPVh2tHN9FNmSMkpaJiFdnSUw6Sg%2BlORftOp5Pi3uv1cTS%2FpDV2tgu7ZJl7JiWqYCDegc4K6VE4qVJ5rrpacyYY5dUc3x4bJpFFhstf0Ljn8%2Bv9mIlXhf1b2l%2FDPwKVypT0Wz%2FY6oRpmQw5ox6Iuo2aF0SAk9zhiAcJTUqOyozBDmwyKyobJmEcDsGFSWZ2c%2FeS6sWL2P0ES3rJ%2FraK%2B%2BQ8%3D%3C%2Fdiagram%3E%3C%2Fmxfile%3E)

### Core Components
- **Amazon S3**: Static file hosting with website configuration
- **Amazon CloudFront**: Global CDN with edge caching and HTTPS
- **Origin Access Identity**: Secure connection between CloudFront and S3 (prevents direct S3 access)
- **AWS CodePipeline**: CI/CD orchestration with GitHub integration
- **AWS CodeBuild**: Automated build environment with Node.js runtime
- **AWS CDK**: Infrastructure as Code using C# constructs
- **AWS Secrets Manager**: Secure GitHub token storage

## Build Pipeline

### Self-Mutating Pipeline Architecture

The deployment uses a **5-stage self-mutating CDK pipeline** that manages both infrastructure and application deployment:

**Stage 1: Source**
- **Tool**: GitHub repository with webhook integration
- **Trigger**: Automatic on push to main branch
- **Authentication**: GitHub token stored in AWS Secrets Manager

**Stage 2: CDK Synthesis**
- **Tool**: AWS CodeBuild
- **Runtime**: Amazon Linux 2, .NET 10.0
- **Process**: Compiles C# CDK code and generates CloudFormation templates

**Stage 3: Self-Update**
- **Tool**: AWS CloudFormation
- **Process**: Updates pipeline stack if CDK pipeline code changed
- **Capability**: Pipeline can modify its own configuration

**Stage 4: Deploy Infrastructure**
- **Tool**: AWS CloudFormation
- **Process**: Deploys BlogInfrastructureStack (S3 + CloudFront + IAM)
- **Output**: Provides bucket name and distribution ID to next stage

**Stage 5: Deploy Application**
- **Tool**: AWS CodeBuild
- **Runtime**: Amazon Linux 2, Node.js 18
- **Process**: 
  - `npm ci` - Install dependencies
  - `npm run build` - Generate static files
  - `aws s3 sync` - Upload to S3 bucket
  - `aws cloudfront create-invalidation` - Clear CDN cache

### Tools Used
- **AWS CodePipeline**: Pipeline orchestration and stage management
- **AWS CodeBuild**: Build environments for CDK synthesis and Next.js compilation
- **AWS CloudFormation**: Infrastructure deployment and stack management
- **AWS CDK**: Infrastructure as Code using C# constructs
- **GitHub**: Source code repository with webhook integration
- **AWS Secrets Manager**: Secure credential storage

## Design Rationale

### Why This Architecture?

**Static Site Generation Choice:**
- Next.js static export eliminates server runtime costs and complexity
- Pre-rendered HTML provides optimal SEO and performance
- Markdown content enables non-technical content management

**S3 + CloudFront Strategy:**
- **Cost Efficiency**: Pay only for storage and data transfer, no server costs
- **Global Performance**: CloudFront edge locations reduce latency worldwide
- **Scalability**: Automatically handles traffic spikes without configuration
- **Reliability**: 99.99% SLA with built-in redundancy

**Self-Mutating Pipeline Benefits:**
- **GitOps Workflow**: All changes (infrastructure + application) via git commits
- **Reduced Operational Overhead**: Pipeline maintains itself, no manual updates
- **Consistency**: Same deployment process for infrastructure and application changes
- **Audit Trail**: All changes tracked through version control

**CDK Over Alternatives:**
- **Type Safety**: C# provides compile-time validation of infrastructure code
- **Reusability**: Constructs enable modular, testable infrastructure components
- **AWS Integration**: Native CloudFormation generation with best practices built-in
- **Developer Experience**: Familiar programming language vs. YAML/JSON templates
