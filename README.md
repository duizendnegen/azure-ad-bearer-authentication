# azure-ad-bearer-authentication

Azure Active Directory Bearer Authentication for .NET Core

## Usage

in your `Startup.cs`, in `Configure` (replacing these values with those provided through Azure AD):

```cs
app.UseAzureADBearerAuthentication(new AzureADAuthenticationOptions
{
    Audience = "1234ab-5678",
    AzureADInstance = "https://login.microsoftonline.com/", // or other instances
    Tenant = "your-tenant.com",
});
```
