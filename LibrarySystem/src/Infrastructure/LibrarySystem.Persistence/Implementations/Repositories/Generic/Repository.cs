using LibrarySystem.Application.Interfaces.Repositories.Generic;
using LibrarySystem.Domain.Entities.common;
using LibrarySystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibrarySystem.Persistence.Implementations.Repositories.Generic;

internal class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbset;
    public Repository(AppDbContext context)
    {

        _context = context;
        _dbset = _context.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>? sort = null,
        int page = 0,
        int take = 0,
        bool isDesc = false,
        params string[] includes)
    {
        IQueryable<T> query = _dbset.AsNoTracking();

        if (filter is not null )
        {
            query = query.Where(filter);
        }

        if (sort is not null)
        {
            if (!isDesc) query = query.OrderBy(sort);
            else query = query.OrderByDescending(sort);
        }

        if (page > 0 && take > 0)
        {
            query = query.Skip((page - 1) * take);
            query = query.Take(take);
        }

        if( includes.Any())
        {
            query = _getIncludes(query, includes);
        }

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(long id, params string[] includes)
    {
        IQueryable<T> query = _dbset.AsNoTracking();
        if (includes.Any())
        {
            query = _getIncludes(query, includes);
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

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> func)
    {
        return await _dbset.AnyAsync(func);
    }

    private IQueryable<T> _getIncludes(IQueryable<T> query, params string[] includes)
    {
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }
}
