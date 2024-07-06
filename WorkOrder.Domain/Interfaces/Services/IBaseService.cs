using System.Linq.Expressions;

namespace WorkOrder.Domain.Interfaces.Services;

public interface IBaseService<T>
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T model);
    Task DeleteAsync(int id);
    Task UpdateAsync(T model);
}
