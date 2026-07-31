using System.ComponentModel.DataAnnotations;
using MaktabBlog.Business.Authentications.Contracts.Commands;

namespace MaktabBlog.Presentation.MvcProject.Models.Authentications;

public class LoginVm
{
    [Required]
    public string NationalId { get; set; }
    
    [Required]
    public string Password { get; set; }

    public LoginCommand ToCommand()
    {
        return new LoginCommand(NationalId, Password);
    }
}