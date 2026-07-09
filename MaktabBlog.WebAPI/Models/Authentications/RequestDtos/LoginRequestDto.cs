using System.ComponentModel.DataAnnotations;
using MaktabBlog.Business.Authentications.Contracts.Commands;

namespace MaktabBlog.WebAPI.Models.Authentications.RequestDtos;

public class LoginRequestDto
{
    [Required(AllowEmptyStrings = false,  ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    [Required(AllowEmptyStrings = false,  ErrorMessage = "Password is required")]
    public string Password { get; set; }

    /// <summary>
    /// Map Login request dto to required login command in authentication service
    /// </summary>
    /// <returns>A proper login command for authentication service.</returns>
    public LoginCommand ToCommand() => new LoginCommand(Username, Password);

}