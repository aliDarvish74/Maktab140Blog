using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MaktabBlog.WebAPI.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthorizationFilterAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKeyAuthSection = configuration.GetSection("ApiKeyAuthentication");
        var authDtos = new List<ClientApiAuthenticationDto>();
        apiKeyAuthSection.Bind(authDtos);
        
        if (authDtos.Count == 0)
            return;

        var hasApiKey = context.HttpContext.Request.Headers.TryGetValue("api-key", out var requestApiKey);
        if (!hasApiKey)
            throw new AuthenticationException("API Key is missing");
        
        var clientAuth = authDtos.FirstOrDefault(c => c.ApiKey == requestApiKey);
        
        if(clientAuth is null)
            throw new AuthenticationException("Invalid API Key");

        Console.WriteLine($"Client found: {clientAuth.ClientId}");
    }
}

public class ClientApiAuthenticationDto
{
    public int ClientId { get; set; }
    public string ApiKey { get; set; }
}