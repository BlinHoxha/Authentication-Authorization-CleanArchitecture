using DDD.Application.InterfaceRepositories.Users;
using DDD.Domain.Identity;
using DDD.Infrastructure.Data;
using DDD.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Repository.Users;

public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(AppDbContext context, ILogger logger) : base(context, logger)
    {

    }
   
    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbSet.OrderBy(n => n.FirstName)
                              .AsNoTracking()
                              .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} All method has generated an error", typeof(UserRepository));
            return new List<ApplicationUser>();
        }
    }



    public async Task<bool> AddUser(ApplicationUser user) => await Create(user);

    public async Task UpdateUser(ApplicationUser user)
    {
        Update(user);
    }

    public async Task<ApplicationUser> GetByIdentityId(Guid identityId, CancellationToken cancellationToken)
    {
        try
        {
            return await dbSet.Where(x => x.Id == identityId)
                              .AsNoTracking()
                              .FirstOrDefaultAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} GetByIdentityId method has generated an error", typeof(UserRepository));
            return null;
        }
    }

    public void DeleteUser(ApplicationUser user) => Delete(user);
   
}
