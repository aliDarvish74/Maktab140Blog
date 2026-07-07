namespace MaktabBlog.Business.Users.Contracts.Results.Args;

public class UserArg
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string NationalId { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }
}