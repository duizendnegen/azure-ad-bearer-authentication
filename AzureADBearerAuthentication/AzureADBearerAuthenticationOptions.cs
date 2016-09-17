namespace Microsoft.AspNetCore.Builder
{
    public class AzureADBearerAuthenticationOptions
    {
        public string Audience { get; set; }
        public string AzureADInstance { get; set; }
        public string Tenant { get; set; }
    }
}
