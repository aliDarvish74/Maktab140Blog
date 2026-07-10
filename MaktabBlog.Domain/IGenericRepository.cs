using System.Linq.Expressions;

namespace MaktabBlog.Domain;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);

    Task<List<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>> predicate,
        Paging paging,
        bool tracking = false);
    
    Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false);
    Task HardDeleteAsync(Guid id);
    Task SoftDeleteAsync(Guid id, Guid requesterId);
    Task UpdateAsync(TEntity entity);
}