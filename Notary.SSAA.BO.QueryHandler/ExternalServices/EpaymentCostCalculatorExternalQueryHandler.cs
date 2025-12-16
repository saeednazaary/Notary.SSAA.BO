using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Mappers.EPayment;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Result;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using MediatR;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Stimulsoft.Data.Expressions.Antlr.Runtime;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ClientLogin;
using Notary.SSAA.BO.SharedKernel.SharedModels;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ClientLogin;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.Security;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class EpaymentCostCalculatorExternalQueryHandler : BaseExternalQueryHandler<EpaymentCostCalculatorExternalQuery, ExternalApiResult<KatebEpaymentCostCalculatorViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private static ClientLoginViewModel _cachedToken;
        private ExternalApiResult<KatebEpaymentCostCalculatorViewModel> apiResult;

        public EpaymentCostCalculatorExternalQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IDateTimeService dateTimeService, IRepository<SsrApiExternalUser> ssrApiExternalUser) : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _ssrApiExternalUser = ssrApiExternalUser ?? throw new ArgumentNullException(nameof(ssrApiExternalUser));
            apiResult = new();
        }

        protected override bool HasAccess(EpaymentCostCalculatorExternalQuery request, IList<string> userRoles)
        {
            return true;
        }

        protected override async Task<ExternalApiResult<KatebEpaymentCostCalculatorViewModel>> RunAsync(EpaymentCostCalculatorExternalQuery request, CancellationToken cancellationToken)
        {
            #region GetToken
            ClientLoginServiceInput clientLoginServiceInput = new();
            if (_cachedToken is null || string.IsNullOrWhiteSpace(_cachedToken.Credential.AccessToken) || _dateTimeService.CurrentDateTime.AddSeconds(30) >= _cachedToken.Credential.ExpireDate)
            {
                _cachedToken = new();
                 var tokenRes = await _mediator.Send(clientLoginServiceInput, cancellationToken);
                if (tokenRes is not null && tokenRes.IsSuccess)
                {
                    _cachedToken.Credential.AccessToken = "Bearer " + tokenRes.Data.Credential.AccessToken;
                    _cachedToken.Credential.ExpireDate = tokenRes.Data.Credential.ExpireDate;
                    _cachedToken.UserBranchAccesses.BranchAccesses = tokenRes.Data.UserBranchAccesses.BranchAccesses;
                }
                else
                {
                    apiResult.ResCode = "114";
                    apiResult.ResMessage= "ارتباط با سرویس توکن برقرار نشد.";
                    return apiResult;
                }
            }
            _userService.UserApplicationContext.Token = _cachedToken.Credential.AccessToken;
            _userService.UserApplicationContext.UserRole = new(RoleConstants.Admin, "");
            #endregion

            ApiResult<EpaymentCostCalculatorViewModel> EpaymentCosts = await _mediator.Send(EPaymentMapper.ToCalculatorServiceInput(request), cancellationToken);
            if (EpaymentCosts is not null && EpaymentCosts.IsSuccess)
            {
                apiResult.ResCode = "1";
                apiResult.ResCode = "سرویس با موفقیت اجرا شد";
                apiResult.Data = EPaymentMapper.ToCalculatorKatebViewModel(EpaymentCosts.Data);
                return apiResult;
            }
            else
            {
                apiResult.ResCode = "113";
                apiResult.ResMessage = string.Join("_", EpaymentCosts!=null? EpaymentCosts.message: "سرویس با خطا مواجه شد");
                return apiResult;
            }
        }

    }
}