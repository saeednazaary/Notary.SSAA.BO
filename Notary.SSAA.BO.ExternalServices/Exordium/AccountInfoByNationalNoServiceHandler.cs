using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Exordium;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Exordium;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;

namespace Notary.SSAA.BO.ServiceHandler.Exordium
{
    internal class AccountInfoByNationalNoServiceHandler : BaseServiceHandler<AccountInfoByNationalNoServiceInput, ApiResult<AccountInfoByNationalNoViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public AccountInfoByNationalNoServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }


        protected override async Task<ApiResult<AccountInfoByNationalNoViewModel>> ExecuteAsync(AccountInfoByNationalNoServiceInput request, CancellationToken cancellationToken)
        {
            return await _httpEndPointCaller.CallPostApiAsync<AccountInfoByNationalNoViewModel, AccountInfoByNationalNoServiceInput>(new HttpEndpointRequest<AccountInfoByNationalNoServiceInput>
                ("http://192.168.20.36:84/" + "ExternalServices/v1/AccountInfoByNationalNo", _userService.UserApplicationContext.Token, request), cancellationToken);
        }

        protected override bool HasAccess(AccountInfoByNationalNoServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
