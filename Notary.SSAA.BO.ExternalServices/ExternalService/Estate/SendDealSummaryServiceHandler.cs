using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    public class SendDealSummaryServiceHandler : BaseServiceHandler<SendDealSummaryInput<DealSummaryServiceOutputViewModel>, ApiResult<DealSummaryServiceOutputViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeriDaftarRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IRepository<EstateOwnershipType> _estateOwnershipRepository;
        private readonly IRepository<EstateTransitionType> _estateTransitionRepository;
        private readonly IRepository<DealSummaryTransferType> _dealSummaryTransferTypeRepository;
        private readonly IRepository<DealsummaryUnrestrictionType> _dealsummaryUnrestrictionTypeRepository;
        private readonly IRepository<DealsummaryPersonRelateType> _dealsummaryPersonRelateTypeRepository;
        private readonly IRepository<EstateInquiryPerson> _estateInquiryPersonRepository;
        private readonly IRepository<DealSummaryPerson> _dealSummaryPersonRepository;
        private DealSummaryObjectConvertHelper helper;
        public SendDealSummaryServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IRepository<ConfigurationParameter> configurationParameterRepository,
             IEstateSectionRepository estateSectionRepository, IEstateInquiryRepository estateInquiryRepository, IEstateSeriDaftarRepository estateSeriDaftarRepository, IEstateSubSectionRepository estateSubSectionRepository
            , IRepository<EstateOwnershipType> estateOwnershipRepository, IRepository<EstateTransitionType> estateTransitionRepository
            , IRepository<DealSummaryTransferType> dealSummaryTransferTypeRepository
            , IRepository<DealsummaryUnrestrictionType> dealsummaryUnrestrictionTypeRepository
            , IRepository<DealsummaryPersonRelateType> dealsummaryPersonRelateTypeRepository,
             IRepository<EstateInquiryPerson> estateInquiryPersonRepository,
             IRepository<DealSummaryPerson> dealSummaryPersonRepository
            ) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _configurationParameterRepository = configurationParameterRepository;
            _estateInquiryRepository = estateInquiryRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSeriDaftarRepository = estateSeriDaftarRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _estateOwnershipRepository = estateOwnershipRepository;
            _estateTransitionRepository = estateTransitionRepository;
            _dealsummaryPersonRelateTypeRepository = dealsummaryPersonRelateTypeRepository;
            _dealSummaryTransferTypeRepository = dealSummaryTransferTypeRepository;
            _dealsummaryUnrestrictionTypeRepository = dealsummaryUnrestrictionTypeRepository;
            _estateInquiryPersonRepository = estateInquiryPersonRepository;
            _dealSummaryPersonRepository = dealSummaryPersonRepository;

            helper = new DealSummaryObjectConvertHelper(
                _mediator,
                _estateSectionRepository,
                _estateInquiryRepository,
                _estateSeriDaftarRepository,
                _estateSubSectionRepository,
                _estateOwnershipRepository,
                _estateTransitionRepository,
                _dealSummaryTransferTypeRepository,
                _dealsummaryUnrestrictionTypeRepository,
                _dealsummaryPersonRelateTypeRepository,
                _estateInquiryPersonRepository,
                _dealSummaryPersonRepository);
        }

        protected async override Task<ApiResult<DealSummaryServiceOutputViewModel>> ExecuteAsync(SendDealSummaryInput<DealSummaryServiceOutputViewModel> request, CancellationToken cancellationToken)
        {
            if (request.IsRemoveRestrictionDealSummary == null)
            {
                var result = new ApiResult<DealSummaryServiceOutputViewModel>() { IsSuccess = false, statusCode = ApiResultStatusCode.Success };
                result.message.Add("نوع ارسال خلاصه معامله (خلاصه معامله جدید/فک محدودیت) ورودی را مشخص کنید");
                return result;
            }
            if (!await IsRealSendDealSummaryIsEnabled(cancellationToken))
            {

                var result = new ApiResult<DealSummaryServiceOutputViewModel>()
                {
                    Data = new DealSummaryServiceOutputViewModel()
                    {
                        Result = true,
                        ErrorMessage = ""
                    },
                    IsSuccess = true,
                    statusCode = ApiResultStatusCode.Success
                };
                return result;

            }
            else
            {
                try
                {
                    request.DsuDealSummary = await helper.CorrectInput(request.DsuDealSummary, cancellationToken);
                }
                catch
                {

                }
                string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/";
                var receiveUrl = mainUrl + "EstateService/SendDealSummary";
                return await _httpEndPointCaller.CallPostApiAsync<DealSummaryServiceOutputViewModel, SendDealSummaryInput<DealSummaryServiceOutputViewModel>>(new HttpEndpointRequest<SendDealSummaryInput<DealSummaryServiceOutputViewModel>>
                    (receiveUrl, _userService.UserApplicationContext.Token, request), cancellationToken);
            }
        }

        protected override bool HasAccess(SendDealSummaryInput<DealSummaryServiceOutputViewModel> request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
        public async Task<bool> IsActiveBOServerForDealSummary(CancellationToken cancellationToken)
        {
            var configurationParameter = await _configurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Deal_Summary_BO_Server_Is_Active")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                var value = await ParseConfigValue(configurationParameter.Value, cancellationToken);
                if (Convert.ToBoolean(value))
                    return true;
                return false;
            }
            return false;
        }
        public async Task<bool> IsRealSendDealSummaryIsEnabled(CancellationToken cancellationToken)
        {
            var configurationParameter = await _configurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_DealSummary_Real_Send_Enabled")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                var value = await ParseConfigValue(configurationParameter.Value, cancellationToken);
                if (Convert.ToBoolean(value))
                    return true;
                return false;
            }
            return false;
        }
        public async Task<string> ParseConfigValue(string configValue, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ParseConfigValueQuery() { Value = configValue }, cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data.Result;
            }
            else
            {
                return null;
            }

        }

    }
}
