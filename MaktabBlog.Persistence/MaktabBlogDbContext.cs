using System.Reflection;
using MaktabBlog.Domain.Comments;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaktabBlog.Persistence;

public class MaktabBlogDbContext : DbContext
{
    public MaktabBlogDbContext()
    {
    }

    public MaktabBlogDbContext(DbContextOptions<MaktabBlogDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}