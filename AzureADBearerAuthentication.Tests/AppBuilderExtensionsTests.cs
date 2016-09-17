using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using Xunit;

namespace AzureADBearerAuthentication.Tests
{
    public class AppBuilderExtensionsTests
    {
        [Fact]
        public void AppBuilderExtensions_UseAuthentication_OnInvalidArgument_ThrowsException()
        {
            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            var applicationBuilder = applicationBuilderMock.Object;

            Assert.Throws<ArgumentException>(() =>
                applicationBuilder.UseAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions()
                {
                    Audience = null,
                    AzureADInstance = "http://localhost",
                    Tenant = "the-tenant"
                })
            );

            Assert.Throws<ArgumentException>(() =>
                applicationBuilder.UseAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions()
                {
                    Audience = "audience",
                    AzureADInstance = null,
                    Tenant = "the-tenant"
                })
            );

            Assert.Throws<ArgumentException>(() =>
                applicationBuilder.UseAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions()
                {
                    Audience = "audience",
                    AzureADInstance = "http://localhost",
                    Tenant = null
                })
            );
        }

        [Fact]
        public void AppBuilderExtensions_UseAuthentication_SetsUpMiddleware()
        {
            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            applicationBuilderMock.Setup(m => m.Use(It.IsAny<Func<RequestDelegate, RequestDelegate>>()))
                .Verifiable();
            var applicationBuilder = applicationBuilderMock.Object;

            applicationBuilder.UseAzureADBearerAuthentication(new AzureADBearerAuthenticationOptions()
            {
                Audience = "audience",
                AzureADInstance = "http://localhost",
                Tenant = "the-tenant"
            });

            applicationBuilderMock.Verify(m => m.Use(It.IsAny<Func<RequestDelegate, RequestDelegate>>()), Times.Once);
        }
    }
}
