// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public class AzureADBearerAuthenticationOptions
    {
        public string Audience { get; set; }
        // ReSharper disable once InconsistentNaming
        public string AzureADInstance { get; set; }
        public string Tenant { get; set; }
    }
}
