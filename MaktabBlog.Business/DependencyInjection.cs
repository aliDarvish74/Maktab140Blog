using MaktabBlog.Business.Authentications;
using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Posts;
using MaktabBlog.Business.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaktabBlog.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtConfigurations"));
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        
        var notifierConfigurations = new List<NotifierConfiguration>();
        var config = configuration.GetSection("NotificationConfiguration");
        config.Bind(notifierConfigurations);
        services
            .Configure<List<NotifierConfiguration>>(configuration.GetSection("NotificationConfiguration"));
        services.AddScoped<INotifierFactory, NotifierFactory>();
        return services;
    }
}