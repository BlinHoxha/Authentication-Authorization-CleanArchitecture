using DDD.Application.InterfaceRepositories.Authentication;
using DDD.Application.InterfaceRepositories.Users;
using DDD.Application.InterfaceServices;
using DDD.Domain.Identity;
using DDD.Infrastructure.Authentication;
using DDD.Infrastructure.Repository.Users;
using DDD.Infrastructure.Service;
using Microsoft.AspNetCore.Identity;

namespace DDD.api.Extensions.Modules
{
    public static partial class ServicesModule
    {                  
        public static void RepositoryManagerInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ServicesManagerInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICreateToken, CreateToken>();
        }

        public static void UserManagerInjection(this IServiceCollection services)
        {
            services.AddScoped<UserManager<ApplicationUser>>();
        }

        public static void RoleManagerInjection(this IServiceCollection services)
        {
            services.AddScoped<RoleManager<ApplicationRole>>();
        }

        public static void SignInManagerInjection(this IServiceCollection services)
        {
            services.AddScoped<SignInManager<ApplicationUser>>();
        }            
    }
}
