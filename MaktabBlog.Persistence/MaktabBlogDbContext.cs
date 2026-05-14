using System.Reflection;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaktabBlog.Persistence;

public class MaktabBlogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .UseSqlServer(
                "Data Source=SF-11202; Initial Catalog=MaktabBlogCodeFirst;TrustServerCertificate=True;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}