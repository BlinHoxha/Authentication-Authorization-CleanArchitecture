using DDD.Application.InterfaceServices;
using EnsureThat;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.api.Controllers.v1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RolesController : BaseController
{
    private readonly IRoleService _roleService;
    private readonly IUserRoleService _userRoleService;
    private readonly ILogger<RolesController> _logger;
    public RolesController(
        IRoleService roleService,
        IUserRoleService userRoleService,
        ILoggerFactory loggerFactory
    ) : base()
    {
        _roleService = EnsureArg.IsNotNull(roleService, nameof(roleService));
        _userRoleService = EnsureArg.IsNotNull(userRoleService, nameof(userRoleService));

        _logger = loggerFactory.CreateLogger<RolesController>();
        EnsureArg.IsNotNull(loggerFactory, nameof(loggerFactory));
    }

    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoleAsync()
    {
        var getRoles = await _roleService.GetRolesAsync();
        return Ok(getRoles);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleByIdAsync(Guid id)
    {
        var getRolesById = await _roleService.GetRoleByIdAsync(id);
        return Ok(getRolesById);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateRoleAsync(string roleName)
    {
        var roleCreated = await _roleService.CreateRoleAsync(roleName);
        return Ok(roleCreated);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        var deleteRole = await _roleService.DeleteRoleAsync(id);
        return Ok(deleteRole);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("Edit/{id}")]
    public async Task<ActionResult> UpdateRoleAsync(Guid id, string roleName)
    {
        var updateRole = await _roleService.UpdateRole(id, roleName);
        return Ok(updateRole);  
    }
}
