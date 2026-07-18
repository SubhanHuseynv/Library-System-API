using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces.Repositories.Generic
{
    public interface IRepository<T>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id, params string[] includes);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        Task AnyAsync(Expression<Func<T, bool>> func);
    }
}
