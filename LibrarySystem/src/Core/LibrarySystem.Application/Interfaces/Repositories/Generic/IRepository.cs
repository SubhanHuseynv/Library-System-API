using System.Linq.Expressions;

namespace LibrarySystem.Application.Interfaces.Repositories.Generic;

public interface IRepository<T>
{
    Task<IReadOnlyList<T>> GetAllAsync(
        List<Expression<Func<T, bool>>>? filters = null,
        Expression<Func<T, object>>? sort = null,
        int page = 0,
        int take = 0,
        bool isDesc = false,
        params string[] includes);
    Task<T?> GetByIdAsync(long id, params string[] includes);
    Task<int> GetValuesCountAsync();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> func);
}
