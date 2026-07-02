using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MaktabBlog.WebAPI.Filters;

public class BasicAuthorizationFilterAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
        Console.WriteLine($"Auth Header value : {authHeader}");
        
        if (string.IsNullOrWhiteSpace(authHeader))
            throw new AuthenticationException("Authorization header missing.");

        var basicToken = authHeader.Replace("basic ", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        Console.WriteLine($"Basic token value : {basicToken}");

        var bytes = Convert.FromBase64String(basicToken);
        var value = Encoding.UTF8.GetString(bytes);
        
        var splitValue =  value.Split(':', 2);

        if (splitValue.Length != 2)
            throw new AuthenticationException("Invalid basic token provided.");
        
        var username = splitValue[0];
        var password = splitValue[1];
        
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var authUsers = new List<BasicAuthDto>();
        configuration.GetSection("BasicAuthentication").Bind(authUsers);

        
        var authenticatedUser = authUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
        
        if (authenticatedUser == null)
            throw new AuthenticationException("Invalid username or password.");

        Console.WriteLine("Login was successful.");
    }
}

public class BasicAuthDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}