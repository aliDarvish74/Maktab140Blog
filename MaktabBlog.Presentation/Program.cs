using System.Text.Json;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence.Posts;
using MaktabBlog.Persistence.Users;

var connectionString = "Data Source=SF-11202; Initial Catalog=MaktabBlog;TrustServerCertificate=True;Integrated Security=True;";

var userRepo = new UserRepository(connectionString);
var postRepo = new PostRepository(connectionString);

/*var post = new Post(
    "My First Post",
    "Content of my first post",
    new Guid("7FB4C02F-67C6-45EF-9548-4818E11C10DF"));
    
await postRepo.AddAsync(post);*/

var result =  await postRepo.GetAllPostsWithUsersAsync();
Console.WriteLine(JsonSerializer.Serialize(result));