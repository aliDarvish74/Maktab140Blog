namespace MaktabBlog.Domain.Users.ViewModels;

public class DetailedUserViewModel : BaseEntityViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Age { get; set; }
}