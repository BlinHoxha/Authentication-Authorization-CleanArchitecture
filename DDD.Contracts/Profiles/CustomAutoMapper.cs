using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Contracts.Profiles;

public static class CustomAutoMapper
{
    public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfile());
            cfg.AddProfile(new RoleProfile());
        });

        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);
    }
}
