using Microsoft.AspNetCore.Mvc;

namespace DDD.api.Extensions.Modules
{
    public static partial class ServicesModule
    {                                
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
                {
                    Duration = 30
                });
            }).AddNewtonsoftJson();
        }

        public static void ApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {//Provides to the client the different Api versions that we have
                opt.ReportApiVersions = true;

                //this will allow the api to automatically provide a default version
                opt.AssumeDefaultVersionWhenUnspecified = true;

                opt.DefaultApiVersion = ApiVersion.Default;
            });
        }
    }
}
