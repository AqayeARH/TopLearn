using System.Linq.Expressions;
using _0.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0.Framework.Infrastructure;

public class EfCoreGenericRepository<TKey, T> : IGenericRepository<TKey, T> where T : class
{
    #region constructor injection

    private readonly DbContext _dbContext;
    public EfCoreGenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    public async Task<List<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> Get(TKey key)
    {
        return await _dbContext.FindAsync<T>(key);
    }

    public async Task Create(T entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<bool> IsExist(Expression<Func<T, bool>> expression)
    {
        return await _dbContext.Set<T>().AnyAsync(expression);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}