using System.Reflection;
using MaktabBlog.Business;
using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Users;
using MaktabBlog.Domain.Users;
using MaktabBlog.ExternalServices.Inquiries;
using MaktabBlog.ExternalServices.Notifiers;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Users;
using MaktabBlog.WebAPI.Filters;
using MaktabBlog.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Http.Logging;

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
    options.LogTo(Console.WriteLine, LogLevel.Information)
        .UseSqlServer(sqlServerConnectionString);
});

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