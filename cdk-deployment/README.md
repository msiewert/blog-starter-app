# Self-Mutating Pipeline with GitHub

## What This Does
- **Single Pipeline**: Deploys both infrastructure AND application
- **GitHub Integration**: Triggers on pushes to your GitHub repo
- **Self-Updating**: Pipeline updates itself when CDK code changes

## Pipeline Flow
```
GitHub → Synth CDK → Update Pipeline → Deploy Infrastructure → Build & Deploy Next.js
```

## Setup Steps

### 1. GitHub Token Setup
- Create a GitHub Personal Access Token with `repo` scope only
- Store your GitHub personal access token in AWS Secrets Manager:
```bash
aws secretsmanager create-secret \
  --name github-token \
  --description "GitHub token for CDK pipeline" \
  --secret-string "your_github_personal_access_token"
```

### 2. Update Repository Details
Edit `BlogPipelineStack.cs` line 17:
```csharp
Input = CodePipelineSource.GitHub("YOUR_GITHUB_USERNAME/YOUR_REPO_NAME", "main", ...)
```
Replace with your actual GitHub username and repository name.

### 3. Bootstrap CDK
```bash
cdk bootstrap
```

### 4. Deploy Pipeline
```bash
cdk deploy BlogPipelineStack
```

### 5. Push Code to GitHub
The pipeline will automatically trigger when you push to the main branch.

## What Happens on Each Push
1. **Source**: Pipeline pulls latest code from GitHub
2. **Synth**: Generates CloudFormation templates from CDK code
3. **Self-Update**: Updates pipeline if CDK code changed
4. **Deploy Infrastructure**: Creates/updates S3 bucket and CloudFront
5. **Build Next.js**: Runs `npm run build`
6. **Deploy**: Uploads to S3 and invalidates CloudFront cache

## Benefits
- **GitOps**: All deployments via git commits
- **Infrastructure as Code**: Infrastructure changes through code
- **Automatic**: No manual deployments needed
- **Self-Healing**: Pipeline updates itself

## Monitoring
- AWS CodePipeline console shows pipeline status
- CloudFormation shows infrastructure deployment progress
- Your blog updates automatically on successful deployments