using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.Intranet.SharedKernel.Services;

public class GenericRepository<T>(DbContext context) : IGenericRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken: cancellationToken);
    }
    
    public async ValueTask<T?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }
    
    public async ValueTask InsertAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
    
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
    
    public async ValueTask DeleteAsync(object id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
    
    public async ValueTask<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}