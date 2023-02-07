using DDD.Application.InterfaceServices;
using DDD.Contracts.DTO.User;
using EnsureThat;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DDD.api.Controllers.v1;

[EnableCors("CorsPolicy")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : BaseController
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        IUserService userService,
        ILoggerFactory loggerFactory
        ) : base()
    {
        _userService = EnsureArg.IsNotNull(userService, nameof(userService));

        _logger = loggerFactory.CreateLogger<UsersController>();
        EnsureArg.IsNotNull(loggerFactory, nameof(loggerFactory));
    }

    [HttpGet("GetUsers")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesDefaultResponseType(typeof(UserDTO))]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersAsync(cancellationToken);
        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var userDto = await _userService.GetUserByIdAsync(userId, cancellationToken);
        return Ok(userDto);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("CreateUser")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesDefaultResponseType(typeof(CreateUserDTO))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDto)
    {
        var userDto = await _userService.AddUserAsync(createUserDto);
        //return CreatedAtRoute(nameof(GetUserById), new { userId = userDto.Id }, userDto);
        return Ok(userDto);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserDTO updateUserDto, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(userId, updateUserDto, cancellationToken);
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(userId, cancellationToken);
        return NoContent();
    }
}
