using DDD.api.Extensions.Modules;

namespace DDD.api.Extensions
{
    public static partial class DependencyInjection
    {
        public static void DependencyInjections(this IServiceCollection services)
        {
            services.RepositoryManagerInjection();
            services.ServicesManagerInjection();
            services.UserManagerInjection();
            services.RoleManagerInjection();
            services.SignInManagerInjection();
        }


    }
}
