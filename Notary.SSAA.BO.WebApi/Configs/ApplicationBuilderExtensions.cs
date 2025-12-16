
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities;
using Notary.SSAA.BO.WebApi.Configs;

namespace Notary.SSAA.BO.WebApi.Configs
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHsts(this IApplicationBuilder app, bool isEnvInDev)
        {
            Assert.NotNull(app, nameof(app));
            if (!isEnvInDev)
                app.UseHsts();
        }


    }
}
