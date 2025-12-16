using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Mappers.EPayment;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Result;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using MediatR;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class KatebEpaymentCostCalculatorQueryHandler : BaseQueryHandler<KatebEpaymentCostCalculatorQuery, ApiResult<KatebEpaymentCostCalculatorViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private ApiResult<KatebEpaymentCostCalculatorViewModel> apiResult;

        public KatebEpaymentCostCalculatorQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            apiResult = new();
        }

        protected override bool HasAccess(KatebEpaymentCostCalculatorQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin);
        }

        protected override async Task<ApiResult<KatebEpaymentCostCalculatorViewModel>> RunAsync(KatebEpaymentCostCalculatorQuery request, CancellationToken cancellationToken)
        {
            ApiResult<EpaymentCostCalculatorViewModel> EpaymentCosts = await _mediator.Send(EPaymentMapper.ToCalculatorServiceInput(request), cancellationToken);
            if (EpaymentCosts.IsSuccess)
            {
                apiResult.IsSuccess = true;
                apiResult.Data = EPaymentMapper.ToCalculatorKatebViewModel(EpaymentCosts.Data);
                return apiResult;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("سرویس با خطا مواجه شد");
                return apiResult;
            }
        }

    }
}