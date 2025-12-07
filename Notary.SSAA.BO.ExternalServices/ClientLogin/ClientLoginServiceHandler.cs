using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ClientLogin;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ClientLogin;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Net;
using System.Net.Http.Json;
using Notary.SSAA.BO.ServiceHandler.Base;

namespace Notary.SSAA.BO.ServiceHandler.ClientLogin
{
    internal class ClientLoginServiceHandler : BaseServiceHandler<ClientLoginServiceInput, ApiResult<ClientLoginViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public ClientLoginServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<ClientLoginViewModel>> ExecuteAsync(ClientLoginServiceInput request, CancellationToken cancellationToken = default)
        {

            var httpclient = new HttpClient();
            string address = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Account/" + "ClientLogin";
            var loginConfig = _configuration.GetSection("ClientLogin");
            request.Username = loginConfig.GetValue<string>("Username");
            request.Password = loginConfig.GetValue<string>("Password");
            request.ClientId = loginConfig.GetValue<string>("ClientId");
            request.GrantType = loginConfig.GetValue<string>("GrantType");
            request.RememberLogin = loginConfig.GetValue<bool>("RememberLogin");
            var eventResponse = await httpclient.PostAsJsonAsync(address, request, cancellationToken).ConfigureAwait(false);

            if (eventResponse.StatusCode == HttpStatusCode.OK)
            {
                string res = await eventResponse.Content.ReadAsStringAsync(cancellationToken);
                var content = JsonConvert.DeserializeObject<ClientLoginViewModel>(res);
                if (content != null && content.Succeeded == true)
                {

                    return content;

                }
                else

                    return null;


            }
            else
            {
                return null;
            }
        }

        protected override bool HasAccess(ClientLoginServiceInput request, IList<string> userRoles)
        {
            return true;
        }
        public sealed record CreateEventRequest(string content);
        public sealed record UpdateEventCommand(string eventId, int status, DateTime startDateTime, DateTime endDateTime, string Response);
    }
}
