using AutoMapper;
using DDD.Contracts.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace DDD.api.Extensions.Modules
{
    public static partial class ServicesModule
    {             
        public static void ConfigureMapping(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<UserProfile>();               
                map.AddProfile<RoleProfile>();               
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }              
      
    }
}
