using System;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class AzureADBearerAuthenticationAppBuilderExtensions
    {
        public static IApplicationBuilder UseAzureADBearerAuthentication(
               this IApplicationBuilder app,
               AzureADBearerAuthenticationOptions authenticationOptions)
        {
            if(string.IsNullOrEmpty(authenticationOptions.Audience))
            {
                throw new ArgumentException("Audience needs to be specified");
            }

            if(string.IsNullOrEmpty(authenticationOptions.AzureADInstance))
            {
                throw new ArgumentException("Azure AD Instance needs to be specified");
            }

            if(string.IsNullOrEmpty(authenticationOptions.Tenant))
            {
                throw new ArgumentException("Tenant needs to be specified");
            }

            var authorityBuilder = new StringBuilder();
            authorityBuilder.Append(authenticationOptions.AzureADInstance);
            if(!authenticationOptions.AzureADInstance.EndsWith("/"))
            {
                authorityBuilder.Append("/");
            }
            authorityBuilder.Append(authenticationOptions.Tenant);

            var jwtBearerAuthOptions = new JwtBearerOptions
            {
                Audience = authenticationOptions.Audience,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Authority = authorityBuilder.ToString()
            };

            app.UseJwtBearerAuthentication(jwtBearerAuthOptions);

            return app;
        }
    }
}
