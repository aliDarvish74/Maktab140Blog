namespace MaktabBlog.Business.Users.Contracts.Commands;

public class UpdateUserInfoCommand
{
    public Guid Id { get; set; }
    public Guid RequesterId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}