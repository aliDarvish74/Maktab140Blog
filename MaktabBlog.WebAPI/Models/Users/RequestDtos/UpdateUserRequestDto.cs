using MaktabBlog.Business.Users.Contracts.Commands;

namespace MaktabBlog.WebAPI.Models.Users.RequestDtos;

public class UpdateUserRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public UpdateUserInfoCommand ToCommand(Guid userId, Guid requesterId)
    {
        return new UpdateUserInfoCommand
        {
            Id = userId,
            RequesterId = requesterId,
            FirstName = FirstName,
            LastName = LastName,
            Age = Age
        };
    }
}