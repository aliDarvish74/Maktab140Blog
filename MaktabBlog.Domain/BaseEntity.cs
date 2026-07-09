namespace MaktabBlog.Domain;

public abstract class BaseEntity : IAudibleEntity
{
    public Guid Id { get; private set; } = new SequentialGuid.SequentialGuid();
    public abstract void Validate();
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; protected set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public void SetAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
    }
}