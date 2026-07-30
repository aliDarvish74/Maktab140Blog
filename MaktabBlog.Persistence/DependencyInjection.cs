using MaktabBlog.Domain.Comments;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence.Comments;
using MaktabBlog.Persistence.Posts;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MaktabBlog.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerConnectionString = configuration.GetConnectionString("SqlServerDB");

        services.AddDbContext<MaktabBlogDbContext>(options =>
        {
            options
                .LogTo(Console.WriteLine, LogLevel.Information)
                .UseSqlServer(sqlServerConnectionString);
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        return services;
    }
}