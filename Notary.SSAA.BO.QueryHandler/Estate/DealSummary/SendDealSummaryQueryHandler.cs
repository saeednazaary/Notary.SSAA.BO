using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;


namespace Notary.SSAA.BO.QueryHandler.Estate.DealSummary
{
    internal class SendDealSummaryQueryHandler : BaseQueryHandler<SendDealSummaryQuery<DealSummaryServiceOutputViewModel>, ApiResult<DealSummaryServiceOutputViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SendDealSummaryQueryHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<DealSummaryServiceOutputViewModel>> RunAsync(SendDealSummaryQuery<DealSummaryServiceOutputViewModel> request, CancellationToken cancellationToken)
        {
            if (request.IsRemoveRestrictionDealSummary == null)
            {
                var result = new ApiResult<DealSummaryServiceOutputViewModel>() { IsSuccess = false, statusCode = ApiResultStatusCode.Success };
                result.message.Add("نوع ارسال خلاصه معامله (خلاصه معامله جدید/فک محدودیت) ورودی را مشخص کنید");
                return result;
            }
            var input = new SendDealSummaryServiceCommand() { DsuDealSummary = request.DsuDealSummary, IsRemoveRestrictionDealSummary = request.IsRemoveRestrictionDealSummary };
            return await _mediator.Send(input, cancellationToken);          
        }

        protected override bool HasAccess(SendDealSummaryQuery<DealSummaryServiceOutputViewModel> request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
    }
}
