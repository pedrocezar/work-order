using System.Linq.Expressions;

namespace WorkOrder.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> FindAsync(int id);
    Task<T> FindAsync(decimal id);
    Task<T> FindAsync(Expression<Func<T, bool>> expression);
    Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> expression);
    Task<int> CountAsync<K>(Expression<Func<T, IEnumerable<K>>> selectExpression);
    Task<int> CountAsync<K>(Expression<Func<T, bool>> expression, Expression<Func<T, IEnumerable<K>>> selectExpression);
    Task<List<T>> ListAsync();
    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    Task<List<T>> ListPaginationAsync<K>(Expression<Func<T, K>> sortExpression, int page, int count);
    Task<List<T>> ListPaginationAsync<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, int page, int count);
    Task AddAsync(T item);
    Task RemoveAsync(T item);
    Task EditAsync(T item);
}
