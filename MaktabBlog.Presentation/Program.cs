using System.Text.Json;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Posts;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;

var connectionString = "Data Source=SF-11202; Initial Catalog=MaktabBlog;TrustServerCertificate=True;Integrated Security=True;";

var userRepo = new UserRepository(connectionString);
var postRepo = new PostRepository(connectionString);

var dbContext = new MaktabBlogDbContext();

/*var user = await dbContext.Users
    .Select(u => new UserDto
    {
        UserId = u.Id,
        FullName = u.FirstName + " " + u.LastName,
        Age = u.Age
    })
    .FirstOrDefaultAsync(u => u.UserId == new Guid("7FB4C02F-67C6-45EF-9548-4818E11C10DF"));*/

var users = await dbContext.Users
    .Where(u => u.CreatedAt > DateTime.Now.AddDays(-50))
    .OrderByDescending(u => u.CreatedAt)
    .ToListAsync();

// var hasNationalId = await dbContext.Set<User>().AnyAsync(u => u.NationalId == "123456789");

Console.WriteLine(JsonSerializer.Serialize(users, new JsonSerializerOptions()
{
    WriteIndented = true
}));


public class UserDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public int? Age { get; set; }
}