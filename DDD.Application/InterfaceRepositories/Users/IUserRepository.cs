using DDD.Application.InterfaceRepositories.Generic;
using DDD.Domain.Identity;

namespace DDD.Application.InterfaceRepositories.Users;

public interface IUserRepository : IGenericRepository<ApplicationUser>
{
    Task<bool> AddUser(ApplicationUser user);
    Task UpdateUser(ApplicationUser user);
    Task<ApplicationUser> GetByIdentityId(Guid IdentityId, CancellationToken cancellationToken);
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(CancellationToken cancellationToken);
    void DeleteUser(ApplicationUser user);

}
