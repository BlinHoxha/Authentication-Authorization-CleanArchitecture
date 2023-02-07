namespace DDD.Application.InterfaceServices;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string roleName);
    Task<bool> DeleteRoleAsync(Guid roleId);
    Task<List<(Guid id, string roleName)>> GetRolesAsync();
    Task<string> GetRoleByIdAsync(Guid id);
    Task<bool> UpdateRole(Guid id, string roleName);
}
