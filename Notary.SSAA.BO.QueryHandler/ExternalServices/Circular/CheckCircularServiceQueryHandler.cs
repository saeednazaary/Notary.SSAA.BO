using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Microsoft.Extensions.Configuration;
using Mapster;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Circular;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Circular;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices.Circular
{
    public class CheckCircularServiceQueryHandler : BaseQueryHandler<CheckCircularGridQuery, ApiResult<CheckCircularViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public CheckCircularServiceQueryHandler(IMediator mediator, IUserService userService,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
              : base(mediator, userService)

        {

            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(CheckCircularGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<CheckCircularViewModel>> RunAsync(CheckCircularGridQuery request, CancellationToken cancellationToken)
        {
            var BOInput = request.Adapt<CheckCircularServiceInput>();
            var result = await _mediator.Send(BOInput, cancellationToken);

            return result;
        }

    }
}
