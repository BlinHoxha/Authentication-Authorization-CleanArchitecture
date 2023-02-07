using DDD.Application.InterfaceServices;
using DDD.Contracts.DTO.Authenticate;
using DDD.Domain.Identity;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DDD.api.Controllers.v1;

public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _accountService;
    
    public AuthenticationController(IAuthenticationService accountService) : base()
    {
        _accountService = EnsureArg.IsNotNull(accountService, nameof(accountService));
    }

    [HttpPost("RegisterUser")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequestDTO userRegistration)
    {       
        var userResult = await _accountService.RegisterAsync(userRegistration);

        if (userResult.StatusCode == 1)
        {
            return Ok(userResult);
        }
        else
        {
            return BadRequest(userResult);
        }
    }

    [HttpPost("Login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Login(UserLoginRequestDTO userLogin)
    {
        var userResult = await _accountService.LoginAsync(userLogin);

        if(userResult.StatusCode == 1) 
        {
            return Ok(userResult);
        }
        else
        {
            return BadRequest(userResult);
        }
    }
}
