using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using MediatR;
using Module = Autofac.Module;
namespace Notary.SSAA.BO.WebApi.Configuration
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
          
        }
    }
}
