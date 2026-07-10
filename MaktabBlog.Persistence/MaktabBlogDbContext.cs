using System.Reflection;
using MaktabBlog.Domain.Comments;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaktabBlog.Persistence;

public class MaktabBlogDbContext : IdentityDbContext<User, Role, Guid>
{
    public MaktabBlogDbContext(DbContextOptions<MaktabBlogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}