using Microsoft.Extensions.Caching.Memory;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Serilog;

namespace Notary.SSAA.BO.WebApi.Configuration;

public static class ConfigureServices
{

    public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton(Log.Logger);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            });
        });
        services.AddHealthChecks();
        return services;
    }
  


}
