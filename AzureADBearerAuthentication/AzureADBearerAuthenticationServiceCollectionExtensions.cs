using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static class AzureADBearerAuthenticationServiceCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddAzureADBearerAuthentication(
            this IServiceCollection services,
            AzureADBearerAuthenticationOptions authenticationOptions,
            Action<JwtBearerOptions> jwtBearerOptionsAction = null)
        {
            if (string.IsNullOrEmpty(authenticationOptions.Audience))
            {
                throw new ArgumentException("Audience needs to be specified");
            }

            if (string.IsNullOrEmpty(authenticationOptions.AzureADInstance))
            {
                throw new ArgumentException("Azure AD Instance needs to be specified");
            }

            if (string.IsNullOrEmpty(authenticationOptions.Tenant))
            {
                throw new ArgumentException("Tenant needs to be specified");
            }

            var authorityBuilder = new StringBuilder();
            authorityBuilder.Append(authenticationOptions.AzureADInstance);
            if (!authenticationOptions.AzureADInstance.EndsWith("/"))
            {
                authorityBuilder.Append("/");
            }
            authorityBuilder.Append(authenticationOptions.Tenant);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Authority = authorityBuilder.ToString();
                    o.Audience = authenticationOptions.Audience;
                    jwtBearerOptionsAction?.Invoke(o);
                });
            return services;
        }
    }
}
