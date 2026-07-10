using Microsoft.AspNetCore.Identity;

namespace MaktabBlog.Domain.Users;

public sealed class Role : IdentityRole<Guid>, IAuditableEntity
{
    private Role()
    {
        
    }
    public Role(string roleName, Guid requesterId) : base(roleName)
    {
        CreatedById = requesterId;
    }

    public DateTime CreatedAt { get; private set; }
    public Guid? CreatedById { get; private set;}
    public User? Creator { get; private set;}
    public DateTime? ModifiedAt { get; private set;}
    public Guid? ModifiedById { get; private set;}
    public User? Modifier { get; private set;}
    public DateTime? DeletedAt { get; private set;}
    public Guid? DeletedById { get; private set;}
    public User? Deleter { get; private set;}
    public bool IsDeleted { get; private set;}

    public void SetRoleName(string roleName, Guid requesterId)
    {
        Name = roleName;
        SetModificationInfo(requesterId);
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