namespace MaktabBlog.Domain;

public abstract class BaseEntity : IAudibleEntity
{
    public Guid Id { get; private set; } = new SequentialGuid.SequentialGuid();

    public abstract void Validate();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}