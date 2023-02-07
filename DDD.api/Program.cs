using DDD.api.Extensions;
using DDD.api.Extensions.Modules;

// Setting up the variables which we include below on services
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configManager = builder.Configuration;

// Add services to the container.

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureMapping();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(configManager);
builder.Services.ConfigureCors();
builder.Services.ConfigureControllers();

builder.Services.DependencyInjections();
builder.Services.ConfigureSqlContext(configManager);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ApiVersioning();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 2147483647;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseResponseCaching();
app.MapControllers();

app.Run();
