using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


namespace Notary.SSAA.BO.WebApi.Configuration;

public static class ConfigureHealthCheck
{
    public static WebApplication AddHealthCheck(this WebApplication app)
    {
        

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return app;
    }
}
