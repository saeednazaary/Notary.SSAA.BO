using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.WebApi.Configuration;
using Serilog;
using System.Net;
using System.Reflection;


namespace Notary.SSAA.BO.WebAPI;

public class Program
{

    public static void Main(string[] args)
    {


            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("runtimeconfig.template.json", optional: true, reloadOnChange: true);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())     
                .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.RegisterModule(new IOCModule(builder.Configuration));
                    containerBuilder.RegisterModule(new ConfigurationIOCModule());
                    containerBuilder.RegisterModule(new CoordinatorIOCModule());

                });

            builder.Services.AddExtraServices(builder.Configuration);
                builder.Host.UseSerilog(Log.Logger,dispose:true);
        builder.Services.AddWebAPIServices();

            //Register CommandeHandlers
            builder.Services.AddMediatR(cng => cng.RegisterServicesFromAssembly(typeof(BaseCommandHandler<,>).GetTypeInfo().Assembly));
            builder.Services.AddMediatR(cng => cng.RegisterServicesFromAssembly(typeof(BaseServiceHandler<,>).GetTypeInfo().Assembly));
            //Register QueryHandlers
            builder.Services.AddMediatR(cng => cng.RegisterServicesFromAssembly(typeof(BaseQueryHandler<,>).GetTypeInfo().Assembly));
            Oracle.ManagedDataAccess.Client.OracleConfiguration.LoadBalancing = false;
            Oracle.ManagedDataAccess.Client.OracleConfiguration.HAEvents = false;
           
            builder.Services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"));

            });
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SSAR.APIs", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWt Auth Using Bearer Scheme",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });


            builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddHsts ( options =>
       {
           options.Preload = true;
           options.IncludeSubDomains = true;
           options.MaxAge = TimeSpan.FromDays ( 365 ); // 1 year
           options.ExcludedHosts.Add ( "localhost" );
           options.ExcludedHosts.Add ( "192.168.20.38" );

           // Exclude localhost for development
       } );
            builder.Services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", opt =>
             {
                 opt.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidIssuers = new [ ] {  "http://identityserver-webapis.farad-backend.svc.cluster.local:5001",
                                               "https://webapis-identityserver.apps.lab.notary.ir",
                                                },
                 };

                 opt.RequireHttpsMetadata = false;
                 opt.Authority = builder.Configuration.GetValue<string>("IdentityServerAddress");
                 opt.Audience = "ssarApi";
             });
            builder.Services.AddHealthChecks().AddOracle(builder.Configuration.GetConnectionString("OracleConnection"), healthQuery: "SELECT 1 FROM DUAL", name: "Oracle", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" });
            bool useMetrics=  builder.Configuration.GetValue<bool>("UseMetrics");
            bool useTelemetry= builder.Configuration.GetValue<bool>("UseTelemetry");

            if (useTelemetry)
            {
              builder.Services.ConfigureTelemetryServices(builder.Environment);
 
            }



            var app = builder.Build();
            app.UseHsts();
            app.AddHealthCheck();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            // app.UseCustomExceptionHandler();
            app.UseMiddleware<Notary.SSAA.BO.WebApi.Middlewares.RequestExceptionHandler>();
            if (useTelemetry)
            {
                app.UseOpenTelemetryPrometheusScrapingEndpoint();

            }
            else 
            if(useMetrics)
            {

                app.UseHealthChecksPrometheusExporter("/metrics", options => options.ResultStatusCodes[HealthStatus.Unhealthy] = (int)HttpStatusCode.OK);

            }
            app.Run();
    }
   
}