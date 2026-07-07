namespace MaktabBlog.Business.Users.Contracts.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string NationalId,
    string Password,
    int? Age = null);
