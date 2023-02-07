namespace DDD.api.Extensions.Modules
{
    public static partial class ServicesModule
    {                               
        public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();       
      
    }
}
