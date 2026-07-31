using System.ComponentModel.DataAnnotations;
using MaktabBlog.Business.Authentications.Contracts.Commands;
using MaktabBlog.Framework.Presentation.Utilities;

namespace MaktabBlog.Presentation.MvcProject.Models.Authentications;

public class RegisterUserVm
{
    /// <summary>
    /// Registering user's first name
    /// </summary>
    /// <example>Ali</example>
    [Required(ErrorMessage = "First name is required.", AllowEmptyStrings =  false)]
    [MinLength(3, ErrorMessage = "Firstname should be at least 3 characters long.")]
    public string FirstName { get; set; }
    
    /// <summary>
    /// Registering user's last name
    /// </summary>
    /// <example>Darvish</example>
    [Required(ErrorMessage = "First name is required.", AllowEmptyStrings =  false)]
    [MinLength(3, ErrorMessage = "Firstname should be at least 3 characters long.")]
    public string LastName { get; set; }
    
    /// <summary>
    /// Registering user's national id
    /// </summary>
    /// <example>1234567890</example>
    [IsValidNationalId]
    public string NationalId { get; set; }
    
    /// <summary>
    /// Registering user's age
    /// </summary>
    /// <example>18</example>
    [Range(18, 80, ErrorMessage = "Age should be in current limitation")]
    public int Age { get; set; }
    
    /// <summary>
    /// Registering user's password
    /// </summary>
    /// <example>Ali123</example>
    public string Password { get; set; }

    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(FirstName, LastName, NationalId, Password, Age);
    }
}