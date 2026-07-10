using MaktabBlog.Domain.Users;

namespace MaktabBlog.Domain;

public abstract class BaseEntity : IAuditableEntity
{
    public Guid Id { get; private set; } = new SequentialGuid.SequentialGuid();
    public abstract void Validate();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public Guid? CreatedById { get; protected set; }
    public User? Creator { get; protected set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedById { get; private set; }
    public User? Modifier { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public Guid? DeletedById { get; set; }
    public User? Deleter { get; set; }
    public bool IsDeleted { get; private set; }
    
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