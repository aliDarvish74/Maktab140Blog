using MaktabBlog.Business;
using MaktabBlog.Business.Users;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<MaktabBlogDbContext>(options =>
{
    options.LogTo(Console.WriteLine, LogLevel.Information)
        .UseSqlServer(
            "Data Source=SF-11202; Initial Catalog=MaktabBlogCodeFirst;TrustServerCertificate=True;Integrated Security=True;");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();