namespace MaktabBlog.Domain;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task HardDeleteAsync(Guid id);
    Task SoftDeleteAsync(Guid id);
    Task UpdateAsync(TEntity entity);
}