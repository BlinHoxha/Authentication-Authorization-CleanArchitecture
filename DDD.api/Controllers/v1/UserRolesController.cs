using DDD.Application.InterfaceServices;
using DDD.Contracts.DTO.Role;
using EnsureThat;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.api.Controllers.v1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserRolesController : BaseController
{
    private readonly IRoleService _roleService;
    private readonly IUserRoleService _userRoleService;
    private readonly ILogger<UserRolesController> _logger;
    public UserRolesController(
        IRoleService roleService,
        IUserRoleService userRoleService,
        ILoggerFactory loggerFactory
    ) : base()
    {
        _roleService = EnsureArg.IsNotNull(roleService, nameof(roleService));
        _userRoleService = EnsureArg.IsNotNull(userRoleService, nameof(userRoleService));

        _logger = loggerFactory.CreateLogger<UserRolesController>();
        EnsureArg.IsNotNull(loggerFactory, nameof(loggerFactory));
    }

    [HttpGet("GetUserRolesAsync")]
    public async Task<ActionResult> GetUserRolesAsync(string userName)
    {
        var userRoles = await _userRoleService.GetUserRolesAsync(userName);
        return Ok(userRoles);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("AssignUserToRoles")]
    public async Task<ActionResult> AssignUserToRolesAsync(string userName, string roleName)
    {
        var roleAssigned = await _userRoleService.AssignUserToRole(userName, roleName);
        return Ok(roleAssigned);
    }

    [HttpGet("IsInRole")]
    public async Task<ActionResult> IsInRoleAsync(Guid id, string role)
    {
        var IsInRole = await _userRoleService.IsInRoleAsync(id, role);
        return Ok(IsInRole);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("DeleteUserRole")]
    public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
    {
        var deleteUserFromRole = await _userRoleService.RemoveUserFromRole(email, roleName);
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("EditUserRoles")]
    public async Task<ActionResult> UpdateRoleAsync(string userName, string usersRole)
    {
        await _userRoleService.UpdateUsersRole(userName, usersRole);
        return NoContent();  
    }
}
