using MaktabBlog.Business.Authentications.Constants;
using MaktabBlog.Domain.Users;
using MaktabBlog.WebAPI.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaktabBlog.Framework.Presentation.Utilities;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDataBaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await SeedRolesAsync(scope.ServiceProvider);
        await SeedAdminsAsync(scope.ServiceProvider);
    }

    private static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        if (roleManager.Roles.Any()) return;

        var godRole = new Role(RoleConstants.GodRoleName);
        var adminRole = new Role(RoleConstants.AdminRoleName);
        var userRole = new Role(RoleConstants.UserRoleName);

        await roleManager.CreateAsync(godRole);
        await roleManager.CreateAsync(adminRole);
        await roleManager.CreateAsync(userRole);

        await roleManager.AddClaimAsync(godRole, ClaimConstants.VipUser);
        await roleManager.AddClaimAsync(godRole, ClaimConstants.MightyHand);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.VipUser);
    }

    private static async Task SeedAdminsAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var adminsData = configuration.GetSection("AdminData").Get<List<AdminData>>();

        if (!adminsData?.Any() ?? true) return;

        var godData = adminsData.FirstOrDefault(d => d.Role == RoleConstants.GodRoleName);

        if (godData != null)
        {
            var godUser = new User(godData.FirstName, godData.LastName, godData.Username, 20);
            await userManager.CreateAsync(godUser, godData.Password);
            await userManager.AddToRoleAsync(godUser, RoleConstants.GodRoleName);
        }

        var adminData = adminsData.FirstOrDefault(d => d.Role == RoleConstants.AdminRoleName);

        if (adminData != null)
        {
            var adminUser = new User(adminData.FirstName, adminData.LastName, adminData.Username, 20);
            await userManager.CreateAsync(adminUser, adminData.Password);
            await userManager.AddToRoleAsync(adminUser, RoleConstants.AdminRoleName);
        }
    }
}