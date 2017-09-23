using Moq;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace AzureADBearerAuthentication.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void ServiceCollectionExtensions_UseAuthentication_OnInvalidArgument_ThrowsException()
        {
            var serviceCollectionMock = new Mock<IServiceCollection>();
            var serviceCollection = serviceCollectionMock.Object;

            Assert.Throws<ArgumentException>(() =>
                serviceCollection.AddAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions
                {
                    Audience = null,
                    AzureADInstance = "http://localhost",
                    Tenant = "the-tenant"
                })
            );

            Assert.Throws<ArgumentException>(() =>
                serviceCollection.AddAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions
                {
                    Audience = "audience",
                    AzureADInstance = null,
                    Tenant = "the-tenant"
                })
            );

            Assert.Throws<ArgumentException>(() =>
                serviceCollection.AddAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions
                {
                    Audience = "audience",
                    AzureADInstance = "http://localhost",
                    Tenant = null
                })
            );
        }

        [Fact]
        public void ServiceCollectionExtensions_UseAuthentication_AddsAuthenticationWithJwtBearerOptions()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions
            {
                Audience = "audience",
                AzureADInstance = "http://localhost",
                Tenant = "the-tenant"
            }, x => x.Challenge = "challenge");

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var authenticationOptions = serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>().Value;
            Assert.NotNull(authenticationOptions);
            Assert.Equal(JwtBearerDefaults.AuthenticationScheme, authenticationOptions.DefaultScheme);

            var jwtBearerOptionsConfigurer = serviceProvider.GetRequiredService<IConfigureOptions<JwtBearerOptions>>() as IConfigureNamedOptions<JwtBearerOptions>;
            Assert.NotNull(jwtBearerOptionsConfigurer);
            var jwtBearerOptions = new JwtBearerOptions();
            jwtBearerOptionsConfigurer.Configure(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions);
            Assert.Equal("http://localhost/the-tenant", jwtBearerOptions.Authority);
            Assert.Equal("audience", jwtBearerOptions.Audience);
            Assert.Equal("challenge", jwtBearerOptions.Challenge);
        }
    }
}
