using DDD.Application.Exceptions;
using DDD.Application.InterfaceServices;
using DDD.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Service;

public class RoleService : IRoleService
    {       
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
     
        public async Task<bool> CreateRoleAsync(string roleName)
        {
            //Check if role exist
            var role_exist = await _roleManager.RoleExistsAsync(roleName);

            if (!role_exist) // check role exist status
            {
              var result = await _roleManager.CreateAsync(new ApplicationRole(roleName));
              
              // check if the role has been added succesfully
              if (result.Succeeded)
              {
                return result.Succeeded;                
              }
              else
              {
                throw new ValidateException(result.Errors);
              }
            }
          throw new RoleAlreadyExistException("Role Already Exists");
        }

    public async Task<bool> DeleteRoleAsync(Guid roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId.ToString());
            if (roleDetails == null)
            {
                throw new NotFoundException("Role not found");
            }

            if (roleDetails.Name == "Admin")
            {
                throw new BadRequestException("You can not delete Administrator Role");
            }
            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                throw new ValidateException(result.Errors);
            }
            return result.Succeeded;
        }

    public async Task<List<(Guid id, string roleName)>> GetRolesAsync()
    {

        var roles = await _roleManager.Roles.Select(x => new
        {
            x.Id,
            x.Name
        }).ToListAsync();

        return roles.Select(role => (role.Id, role.Name)).ToList();
    }

    public async Task<string> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null) // Role does not exist
        {
            throw new RoleNotExistException($"Role with {role} id does not exist");
        }

        var roles = await _roleManager.GetRoleNameAsync(role);

        return roles;
        }

        public async Task<bool> UpdateRole(Guid id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }
            return false;
        }
    }

