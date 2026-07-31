using MaktabBlog.Domain.Users;

namespace MaktabBlog.Business.Users.Contracts.Results.Args;

public class UserArg
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Username { get; set; }
    public string NationalId { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }

    public static UserArg FromUser(User user)
    {
        return new UserArg
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            NationalId = user.NationalId,
            Email = user.Email,
            Age = user.Age
        };
    }
}