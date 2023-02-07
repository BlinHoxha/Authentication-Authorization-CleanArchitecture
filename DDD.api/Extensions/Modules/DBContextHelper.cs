using DDD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DDD.api.Extensions.Modules;

public static partial class ServicesModule
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
    services.AddDbContext<AppDbContext>(
        opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("DDD.Infrastructure")));
}
