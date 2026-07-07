using System.Linq.Expressions;
using MaktabBlog.Domain;
using Microsoft.EntityFrameworkCore;

namespace MaktabBlog.Persistence;

public abstract class GenericRepository<TEntity> 
    : IGenericRepository<TEntity> 
    where TEntity : BaseEntity, IAudibleEntity
{
    protected readonly MaktabBlogDbContext DbContext;

    protected GenericRepository(MaktabBlogDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<List<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>> predicate,
        Paging paging,
        bool tracking = false)
    {
        var query = DbContext.Set<TEntity>().AsQueryable();

        if (!tracking) query = query.AsNoTracking();
        return await query
            .Where(predicate)
            .Skip(paging.Skip)
            .Take(paging.PageSize)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false)
    {
        var query = DbContext.Set<TEntity>().AsQueryable().Where(u => u.IsDeleted == false);

        if (!tracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task HardDeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, true);
        if (entity == null) return;
        
        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, true);
        
        if(entity is null) return;
        
        entity.SetAsDeleted();
        await DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        await DbContext.SaveChangesAsync();
    }
}