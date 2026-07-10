using MaktabBlog.Domain.Users;

namespace MaktabBlog.Domain;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; }
    public Guid? CreatedById { get; }
    public User? Creator { get; }
    public DateTime? ModifiedAt { get; }
    public Guid? ModifiedById { get; }
    public User? Modifier { get; }
    public DateTime? DeletedAt { get; }
    public Guid? DeletedById { get; }
    public User? Deleter { get; }
    public bool IsDeleted { get; }
    void SetAsDeleted(Guid requesterId);
    void SetModificationInfo(Guid requesterId);
}