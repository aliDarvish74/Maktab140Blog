using MaktabBlog.Business.Users.Contracts.Results.Args;

namespace MaktabBlog.WebAPI.Models.Users.ResponseDtos.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string NationalId { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }

    public static UserDto FromArg(UserArg arg)
    {
        return new UserDto
        {
            Id = arg.Id,
            FullName = arg.FirstName  + " " + arg.LastName,
            Username = arg.Username,
            NationalId = arg.NationalId,
            Email = arg.Email,
            Age = arg.Age
        };
    }
}