using System.ComponentModel.DataAnnotations;
using MaktabBlog.WebAPI.Filters;

namespace MaktabBlog.WebAPI.Models.Users.RequestDtos;

public class AddUserRequestDto
{
    [Required(ErrorMessage = "First name is required.", AllowEmptyStrings =  false)]
    [MinLength(3, ErrorMessage = "Firstname should be at least 3 characters long.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "First name is required.", AllowEmptyStrings =  false)]
    [MinLength(3, ErrorMessage = "Firstname should be at least 3 characters long.")]
    public string LastName { get; set; }
    
    [IsValidNationalId]
    public string NationalId { get; set; }
    [Range(18, 80, ErrorMessage = "Age should be in current limitation")]
    public int Age { get; set; }
}