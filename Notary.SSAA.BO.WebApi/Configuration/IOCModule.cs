using Autofac;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Module = Autofac.Module;
using Stimulsoft.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Stimulsoft.Drawing;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.DateTime;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpEndPointCaller.Policies;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.Security;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpEndPointCaller;
using Notary.SSAA.BO.Infrastructure.Persistence.Interceptors;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpExternalServiceCaller;
using Oracle.ManagedDataAccess.Client;
using Notary.SSAA.BO.Infrastructure.Persistence.Services;
using System.Diagnostics;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.ApplicationIdGenerator;


namespace Notary.SSAA.BO.WebApi.Configuration
{
    public class IOCModule : Module
    {
        private IConfiguration _configuration;
        public IOCModule(IConfiguration configuration)
        {
            _configuration = configuration;


        }
        protected override void Load(ContainerBuilder builder)
        {
            StiLicense.Key = StimulsoftConstant.LisenceKey;

            var fontsPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts");
            string[] ttfFiles = Directory.GetFiles(fontsPath, "*.TTF");
            foreach (var fontFile in ttfFiles)
            {
                StiFontCollection.AddFontFile(fontFile);


            }

            // builder.RegisterType<SignRequestRepository>().As<ISignRequestRepository>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            var repositoryAssembly = typeof(Repository<>).Assembly;
            builder.RegisterAssemblyTypes(repositoryAssembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces().InstancePerLifetimeScope();

            var dappeRepositoryAssembly = typeof(DapperRepository).Assembly;
            builder.RegisterAssemblyTypes(dappeRepositoryAssembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .WithParameter("connectionString", _configuration.GetConnectionString("OracleConnection"))
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            var connectionString = _configuration.GetConnectionString("OracleConnection");
            var connection = new OracleConnection(connectionString); // Use SqlConnection for SQL Server
            connection.Open(); // Open the connection once and reuse it
            var dRepositoryAssembly = typeof(DRepository).Assembly;
            builder.RegisterAssemblyTypes(dRepositoryAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .WithParameter("connection", connection)
                .AsImplementedInterfaces().InstancePerLifetimeScope();



            var options = new DbContextOptionsBuilder<SsarContext>()
                      .UseOracle(_configuration.GetConnectionString("OracleConnection"))
                           .EnableServiceProviderCaching()
                           .EnableSensitiveDataLogging()
                           .EnableDetailedErrors()
                           .Options;
            bool useEventLog = _configuration.GetValue<bool>("UseEventLog");
            if (useEventLog)
            {
                options = new DbContextOptionsBuilder<SsarContext>()
                           .UseOracle(_configuration.GetConnectionString("OracleConnection"))
                                .EnableServiceProviderCaching()
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors()
                                .AddInterceptors(new ChangesInterceptor(_configuration))
                                      .Options;

            }

            builder.RegisterType<ApplicationContext>()
           .AsSelf().InstancePerLifetimeScope()
            .WithParameter("options", options);

            builder.RegisterType<DateTimeService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CryptoService> ().SingleInstance ();

            builder.RegisterType<ApplicationIdGeneratorService>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<HttpContextAccessor>()
           .As<IHttpContextAccessor>()
           .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<UserService>()
           .As<IUserService>()
           .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationContextService>()
                .As<IApplicationContextService>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ClientPolicy>();
            builder.Register(_ =>
            {
                var services = new ServiceCollection();
                services.AddHttpClient();
                var provider = services.BuildServiceProvider();
                return provider.GetRequiredService<IHttpClientFactory>();
            });
            builder.RegisterType<HttpEndPointCaller>()
                .WithParameter(
                    (p, c) => p.ParameterType == typeof(ClientPolicy),
                    (p, c) => c.Resolve<ClientPolicy>())
                .WithParameter(
                    (p, c) => p.ParameterType == typeof(IHttpClientFactory),
                    (p, c) => c.Resolve<IHttpClientFactory>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<HttpExternalServiceCaller>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();




        }
    }
}
