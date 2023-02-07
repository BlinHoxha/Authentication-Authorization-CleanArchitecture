using AutoMapper;
using DDD.Application.InterfaceRepositories.Authentication;
using DDD.Application.InterfaceServices;
using DDD.Contracts.DTO.Authenticate;
using DDD.Domain.Identity;
using EnsureThat;
using Microsoft.AspNetCore.Identity;

namespace DDD.Infrastructure.Service;

public class AuthenticationService : IAuthenticationService
    {
        private readonly ICreateToken _createToken;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthenticationService(
            ICreateToken createToken,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper)
        {
        _userManager = EnsureArg.IsNotNull(userManager, nameof(userManager));
        _signInManager = EnsureArg.IsNotNull(signInManager, nameof(signInManager));
        _roleManager = EnsureArg.IsNotNull(roleManager, nameof(roleManager));
        _createToken = EnsureArg.IsNotNull(createToken, nameof(createToken));
        _mapper = EnsureArg.IsNotNull(mapper, nameof(mapper));
    }

        public async Task<AuthResult> RegisterAsync(UserRegistrationRequestDTO userRegistration)
        {
            var authResult = new AuthResult();

        //Check if user already exist
        var userExist = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (userExist != null) 
            {
            authResult.StatusCode = 0;
            authResult.Message = "User already exists";
            return authResult;
            }
           
            var newUser = _mapper.Map<ApplicationUser>(userRegistration);
            var result = await _userManager.CreateAsync(newUser, userRegistration.Password);

            if (!result.Succeeded)
            {

            authResult.StatusCode = 0;
            authResult.Message = "User creation failed";
               return authResult;
            }

        var jwtToken = await _createToken.CreateTokenAsync(newUser);

        // checking if user has role
        if (!await _roleManager.RoleExistsAsync(userRegistration.Role.ToString()))
            await _roleManager.CreateAsync(new ApplicationRole(userRegistration.Role.ToString()));

        if (await _roleManager.RoleExistsAsync(userRegistration.Role.ToString()))
        {
            // We need to add user to a role
            await _userManager.AddToRoleAsync(newUser, userRegistration.Role.ToString());
        }

        authResult.Token = jwtToken;
        authResult.StatusCode = 1;
        authResult.Message = "User has registered successfully";

        return authResult;        
        }

        public async Task<AuthResult> LoginAsync(UserLoginRequestDTO loginDto)
        {
          var authResult = new AuthResult();
          //Check if user already exist
          var user = await _userManager.FindByEmailAsync(loginDto.Email);
          if (user == null)
          {
            authResult.StatusCode = 0;
            authResult.Message = "Invalid email";
            return authResult;
          }

          var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

          if(!isCorrectPassword)
          {
            authResult.StatusCode= 0;
            authResult.Message = "Invalid password";
            return authResult;
          }

          var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, true);
          if (signInResult.Succeeded) 
          {           
            var jwtToken = await _createToken.CreateTokenAsync(user);

            authResult.Token = jwtToken;
            authResult.StatusCode = 1;
            authResult.Message = "Logged is successfully";
            return authResult;            
          }
        else if (signInResult.IsLockedOut)
          {
            authResult.StatusCode = 0;
            authResult.Message = "User locked out";
            return authResult;
          }
        else
          {
            authResult.StatusCode = 0;
            authResult.Message = "Error on loggin";
            return authResult;
          }             
        }

    }

