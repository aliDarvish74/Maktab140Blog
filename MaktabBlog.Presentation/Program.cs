using System.Linq.Expressions;
using System.Text.Json;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Posts;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;

var dbContext = new MaktabBlogDbContext();
var userRepo = new UserRepository(dbContext);
var postRepo = new PostRepository(dbContext);

/*var user = await dbContext.Users
    .Select(u => new UserDto
    {
        UserId = u.Id,
        FullName = u.FirstName + " " + u.LastName,
        Age = u.Age
    })
    .FirstOrDefaultAsync(u => u.UserId == new Guid("7FB4C02F-67C6-45EF-9548-4818E11C10DF"));*/

/*var users = await dbContext.Users
    .Where(u => u.CreatedAt > DateTime.Now.AddDays(-50))
    .OrderByDescending(u => u.CreatedAt)
    .ToListAsync();*/

// var hasNationalId = await dbContext.Set<User>().AnyAsync(u => u.NationalId == "123456789");

/*var user = new User("Neda", "Akbari", "987654321", 22);

await userRepo.AddAsync(user);*/

/*var user = await userRepo.GetByIdAsync(new Guid("019E2A67-F841-7000-81E6-21EDD67E8908"));

if (user == null)
    throw new Exception("User not found");

user.UpdateUserInfo("Nima", "MatinKia", "123456789");

await userRepo.SoftDeleteAsync(user.Id);

Console.WriteLine(JsonSerializer.Serialize(user, new JsonSerializerOptions()
{
    WriteIndented = true
}));*/

// var users = userRepo.QueryAsync(u => u.CreatedAt >= DateTime.Now || u.FirstName.StartsWith("A"));
var user = dbContext.Users.FirstOrDefault();
dbContext.Entry(user).State = EntityState.Modified;

public class UserDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public int? Age { get; set; }
}