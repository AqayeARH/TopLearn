using System.Linq.Expressions;

namespace _0.Framework.Domain;

public interface IGenericRepository<in TKey, T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> Get(TKey key);
    Task Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> IsExist(Expression<Func<T, bool>> expression);
    Task Save();
}