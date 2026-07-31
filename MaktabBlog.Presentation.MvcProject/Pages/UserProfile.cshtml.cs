using System.Security.Claims;
using MaktabBlog.Business.Users;
using MaktabBlog.Business.Users.Contracts.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabBlog.Presentation.MvcProject.Pages;

[Authorize]
[Route("users/profile")]
public class UserProfile : PageModel
{
    [BindProperty]
    public UserProfileModel UserProfileModel { get; set; } = new();
    
    private readonly IUserService _userService;

    public UserProfile(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetByIdAsync(userId ?? string.Empty);

        UserProfileModel = new UserProfileModel
        {
            Id = user.Id,
            FullName = user.FirstName + " " + user.LastName,
            NationalCode = user.NationalId,
            Age = user.Age
        };
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var command = new UpdateUserInfoCommand
        {
            Id = UserProfileModel.Id,
            RequesterId = new Guid(userId ?? string.Empty),
            FirstName = UserProfileModel.FullName.Split(" ",2).First(),
            LastName = UserProfileModel.FullName.Split(" ",2).Last(),
            Age = UserProfileModel.Age
        };

        await _userService.UpdateUserInfoAsync(command);

        return Page();
    }
}

public class UserProfileModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string NationalCode { get; set; }
    public int? Age { get; set; }
}