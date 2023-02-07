using DDD.Application.Exceptions;
using DDD.Application.InterfaceServices;
using DDD.Contracts.DTO.Role;
using DDD.Domain.Identity;
using DDD.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DDD.Infrastructure.Data;

namespace DDD.Infrastructure.Service;

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UserRoleService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<List<string>> GetUserRolesAsync(string userName)
    {
        // Check if the email is valid

        var user = await _userManager.FindByNameAsync(userName);

        if(user == null) // User does not exist
        {
            throw new UserNotFoundException($"User with {userName} does not exist");
        }

        // return the roles
        var roles = await _userManager.GetRolesAsync(user);

        return roles.ToList();       
    }

    public async Task<bool> AssignUserToRole(string userName, string roleName)
    {
        // Check if the user exist
        var user = await _userManager.FindByNameAsync(userName);

        if(user == null) // User does not exist
        {
            throw new UserNotFoundException(userName);
        }

        // Check if the role exist
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if(!roleExist) // checks on the role exist status
        {
            throw new RoleNotExistException("Role does not exist");
        }
        
        var result = await _userManager.AddToRoleAsync(user, roleName);

        // Check if the user is assigned to the role successfully
        if (result.Succeeded) 
        {
          return result.Succeeded;
        }
        else
        {
            throw new UserNotAddedToRoleException("User was not able to be added to role");
        }
    }

    public async Task UpdateUsersRole(string userName, string userRole)
    {              
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, existingRoles);
            await _userManager.AddToRoleAsync(user, userRole);

            //await _context.SaveChangesAsync();       
    }

    public async Task<bool> RemoveUserFromRole(string email, string roleName)
    {
        // Check if the user exist
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) // User does not exist
        {
            throw new UserNotFoundException($"User with {email} not found");
        }

        // Check if the role exist
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist) // checks on the role exist status
        {
            throw new RoleNotExistException($"Role {roleName} does not exist");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (result.Succeeded)
        {
            return result.Succeeded;
        }
        else
        {
            throw new RoleUnableToRemoveException($"Unable to remove User {email} from role {roleName}");
        }
    }
}

