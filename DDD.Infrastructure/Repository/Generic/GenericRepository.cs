using DDD.Application.InterfaceRepositories.Generic;
using DDD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Repository.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected AppDbContext _context;
    internal DbSet<T> dbSet;
    protected readonly ILogger _logger;
    public GenericRepository(AppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
        dbSet = context.Set<T>();
    }
    public async Task<bool> Create(T entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        return await dbSet.ToListAsync();
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task Update(T entity)
    {
        this.dbSet.Update(entity);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}
