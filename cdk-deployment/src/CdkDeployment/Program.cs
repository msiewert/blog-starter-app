using Amazon.CDK;

namespace CdkDeployment
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            
            // Only deploy the pipeline - it will deploy infrastructure automatically
            new BlogPipelineStack(app, "BlogPipelineStack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }
            });

            app.Synth();
        }
    }
}