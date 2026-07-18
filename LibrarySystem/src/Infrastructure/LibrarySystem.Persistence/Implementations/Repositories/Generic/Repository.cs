using LibrarySystem.Application.Interfaces.Repositories.Generic;
using LibrarySystem.Domain.Entities.common;
using LibrarySystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Persistence.Implementations.Repositories.Generic
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;
        public Repository(AppDbContext context)
        {

            _context = context;
            _dbset = _context.Set<T>();
        }
        
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id, params string[] includes)
        {
            IQueryable<T> query = _dbset.AsNoTracking();
            if (includes.Any())
            {
                query = _getIncludes(query ,includes);
            }
            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AnyAsync(Expression<Func<T, bool>> func)
        {
            await _dbset.AnyAsync(func);
        }

        private IQueryable<T> _getIncludes(IQueryable<T> query, params string[] includes)
        {
            foreach(var include in includes)
            {
              query = _dbset.Include(include);
            }
            return query;
        }
    }
}
