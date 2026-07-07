using System.Reflection;
using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Users;
using MaktabBlog.Domain.Users;
using MaktabBlog.ExternalServices.Inquiries;
using MaktabBlog.ExternalServices.Notifiers;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Users;
using MaktabBlog.WebAPI.Filters;
using MaktabBlog.WebAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(option =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);

});

builder.Services
    .AddControllers(option => option.Filters.Add<RequestModelValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServerDB");

builder.Services.AddDbContext<MaktabBlogDbContext>(options =>
{
    options
        .LogTo(Console.WriteLine, LogLevel.Information)
        .UseSqlServer(sqlServerConnectionString);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddEntityFrameworkStores<MaktabBlogDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MaktabBlogRedis";
});

var t = new List<NotifierConfiguration>();
var config = builder.Configuration.GetSection("NotificationConfiguration");
config.Bind(t);
builder.Services
    .Configure<List<NotifierConfiguration>>(builder.Configuration.GetSection("NotificationConfiguration"));
builder.Services.AddScoped<INotifierFactory, NotifierFactory>();
builder.Services.AddScoped<INotifier, EmailNotifier>();
builder.Services.AddScoped<INotifier, SmsNotifier>();
builder.Services.AddScoped<IInquiryService, InquiryService>();
builder.Services.Configure<InquiryConfiguration>(builder.Configuration.GetSection("InquiryConfiguration"));

builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();
builder.Services.AddScoped<LoggingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();