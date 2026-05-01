using System.Text.Json;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence.Users;

var connectionString = "Data Source=SF-11202; Initial Catalog=MaktabBlog;TrustServerCertificate=True;Integrated Security=True;";

var userRepo = new UserRepository(connectionString);

var addUser = new User("Sian", "Hemmati", "9876543210", 21);

//await userRepo.AddAsync(addUser);

//var users = await userRepo.GetAllAsync();

//var jsonResult = JsonSerializer.Serialize(users);

var user = await userRepo.GetByIdAsync(new Guid("CA68F9F4-9DAF-4599-9C99-46CECC3ED96B"));

Console.WriteLine(JsonSerializer.Serialize(user));