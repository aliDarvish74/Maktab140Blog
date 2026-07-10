using MaktabBlog.Domain.Posts;
using Microsoft.AspNetCore.Identity;

namespace MaktabBlog.Domain.Users;

public sealed class User : IdentityUser<Guid>, IAuditableEntity
{
    private User()
    {
    }
    public User(string firstName, string lastName, string nationalId, int? age = null, Guid? requesterId = null)
    {
        Id = new SequentialGuid.SequentialGuid();
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        UserName = nationalId;
        Age = age;
        CreatedById = requesterId ?? Id;
        Validate();
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalId { get; private set; }
    public int? Age { get; private set; }
    public List<Post> Posts { get; private set; } = new();
    public List<Post> LikedPosts { get; private set; } = new();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public Guid? CreatedById { get; private set; }
    public User? Creator { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedById { get; private set; }
    public User Modifier { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public Guid? DeletedById { get; private set; }
    public User Deleter { get; private set; }
    public bool IsDeleted { get; private set; }
    public void UpdateUserInfo(string firstName, string lastName, string nationalId, Guid requesterId, int? age = null)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        Age = age ?? Age;
        Validate();
        SetModificationInfo(requesterId);
    }

    private void Validate()
    {
        if(string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentNullException($"{nameof(FirstName)} cannot be null or whitespace.");
    }
    public void SetAsDeleted(Guid requesterId)
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
        DeletedById = requesterId;
    }

    public void SetModificationInfo(Guid requesterId)
    {
        ModifiedAt = DateTime.UtcNow;
        ModifiedById = requesterId;
    }
}