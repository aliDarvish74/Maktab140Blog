using MaktabBlog.Domain.Posts;

namespace MaktabBlog.Domain.Users;

public class User : BaseEntity
{
    public User()
    {
        
    }
    public User(string firstName, string lastName, string nationalId, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        Age = age;
        Validate();
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalId { get; private set; }
    public int? Age { get; private set; }
    public List<Post> Posts { get; private set; } = new();

    public List<Post> LikedPosts { get; private set; } = new();

    public void UpdateUserInfo(string firstName, string lastName, string nationalId, int? age = null)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        Age = age ?? Age;
        Validate();
        ModifiedAt = DateTime.UtcNow;
    }
    
    public override void Validate()
    {
        if(string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentNullException($"{nameof(FirstName)} cannot be null or whitespace.");
    }
}