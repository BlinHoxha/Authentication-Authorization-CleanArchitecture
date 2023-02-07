using DDD.Contracts.DTO.Role;

namespace DDD.Application.InterfaceServices;

public interface IUserRoleService
{
    Task<bool> IsInRoleAsync(Guid userId, string role);
    Task<List<string>> GetUserRolesAsync(string email);
    Task<bool> AssignUserToRole(string userName, string roleName);
    Task UpdateUsersRole(string userName, string usersRole);
    Task<bool> RemoveUserFromRole(string email, string roleName);
}
