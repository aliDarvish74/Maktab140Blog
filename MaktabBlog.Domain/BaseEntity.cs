namespace MaktabBlog.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = new SequentialGuid.SequentialGuid();
    public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; protected set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public void SetAsDeleted()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }

    public abstract void Validate();
}