namespace MaktabBlog.Domain;

public interface IAudibleEntity
{
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; }
    public DateTime? DeletedAt { get; }
    public bool IsDeleted { get; }
    public void SetAsDeleted();
}