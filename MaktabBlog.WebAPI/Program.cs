using System.Reflection;
using System.Text;
using MaktabBlog.Business.Authentications;
using MaktabBlog.Business.Authentications.Constants;
using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Users;
using MaktabBlog.Domain.Users;
using MaktabBlog.ExternalServices.Inquiries;
using MaktabBlog.ExternalServices.Notifiers;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Users;
using MaktabBlog.WebAPI.Extensions;
using MaktabBlog.WebAPI.Filters;
using MaktabBlog.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(option =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
    
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });

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

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddEntityFrameworkStores<MaktabBlogDbContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtConfigurations").Get<JwtSettings>()!;
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtConfigurations"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ValidAudience = jwtSettings.Audience,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateIssuer = true
    };
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(
        "VipOnly",
        policy => policy.RequireClaim(ClaimConstants.VipUser.Type, ClaimConstants.VipUser.Value));
});

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
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
await app.SeedDataBaseAsync();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();