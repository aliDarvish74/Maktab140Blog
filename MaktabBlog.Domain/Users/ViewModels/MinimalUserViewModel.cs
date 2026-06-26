namespace MaktabBlog.Domain.Users.ViewModels;

public class MinimalUserViewModel : BaseEntityViewModel
{
    public string FullName { get; set; }
    public int? Age { get; set; }
}