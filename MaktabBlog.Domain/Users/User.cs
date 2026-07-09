using MaktabBlog.Domain.Posts;
using Microsoft.AspNetCore.Identity;

namespace MaktabBlog.Domain.Users;

public sealed class User : IdentityUser, IAudibleEntity
{
    public User()
    {
    }
    public User(string firstName, string lastName, string nationalId, int? age = null)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        UserName = nationalId;
        Age = age;
        Validate();
    }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string NationalId { get;  set; }
    public int? Age { get;  set; }
    public List<Post> Posts { get; private set; } = new();
    public List<Post> LikedPosts { get; private set; } = new();
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public void UpdateUserInfo(string firstName, string lastName, string nationalId, int? age = null)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        Age = age ?? Age;
        Validate();
        ModifiedAt =  DateTime.UtcNow;
    }

    private void Validate()
    {
        if(string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentNullException($"{nameof(FirstName)} cannot be null or whitespace.");
    }
    public void SetAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
    }
}