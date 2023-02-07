using DDD.Application.InterfaceRepositories.Generic;
using DDD.Application.InterfaceRepositories.Users;
using DDD.Infrastructure.Data;
using DDD.Infrastructure.Repository.Users;
using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Repository.Generic;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public IUserRepository Users { get; private set; }

    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("db_logs");
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
