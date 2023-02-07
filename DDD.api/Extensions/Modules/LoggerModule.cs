using DDD.Application.InterfaceServices;
using DDD.Infrastructure.Service;

namespace DDD.api.Extensions.Modules
{
    public static partial class ServicesModule
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Program>>();
            services.AddSingleton(typeof(ILogger), logger);
        }
     
    }
}
