using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
//using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
//using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
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
using System.Configuration.Internal;
using System.Threading;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    internal class ValidateEstateSeparationInquiriesServiceHandler : BaseServiceHandler<ValidateEstateSeparationInquiriesInput, ApiResult<ValidateEstateSeparationInquiriesViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IRepository<EstateDivisionRequestElement> _estateDivisionRequestElement;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstatePieceTypeRepository _estatePieceTypeRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        public ValidateEstateSeparationInquiriesServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            , IRepository<EstateDivisionRequestElement> estateDivisionRequestElement
            , IRepository<ConfigurationParameter> configurationParameterRepository
            , IEstateSectionRepository estateSectionRepository
            , IEstateSubSectionRepository estateSubSectionRepository
            , IEstatePieceTypeRepository estatePieceTypeRepository,
            IEstateInquiryRepository estateInquiryRepository) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _estateDivisionRequestElement = estateDivisionRequestElement;
            _configurationParameterRepository = configurationParameterRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _estatePieceTypeRepository = estatePieceTypeRepository;
            _estateInquiryRepository = estateInquiryRepository;
        }

        protected async override Task<ApiResult<ValidateEstateSeparationInquiriesViewModel>> ExecuteAsync(ValidateEstateSeparationInquiriesInput request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<ValidateEstateSeparationInquiriesViewModel>();
            var mainDivisionRequestServiceIsEnabled = await MainDivisionRequestServiceIsEnabled(cancellationToken);
            if (!mainDivisionRequestServiceIsEnabled)
            {
                apiResult.Data = new ValidateEstateSeparationInquiriesViewModel() { Result = true, ErrorMessage = "" };
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.Success;
                return apiResult;
            }
            else
            {
                var lst = request.EstateInquiryId.Select(x => x.ToGuid());
                var inquiryList = await _estateInquiryRepository.TableNoTracking.Where(x => lst.Contains(x.Id)).ToListAsync(cancellationToken);
                List<string> strList = new List<string>();
                foreach (var inquiry in inquiryList)
                {
                    if (string.IsNullOrWhiteSpace(inquiry.LegacyId))
                        strList.Add(inquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper());
                    else
                        strList.Add(inquiry.LegacyId);
                }
                request.EstateInquiryId = strList.ToArray();

                string serviceUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/EstateService/ValidateEstateSeparationInquiries";
                return await _httpEndPointCaller.CallPostApiAsync<ValidateEstateSeparationInquiriesViewModel, ValidateEstateSeparationInquiriesInput>(new HttpEndpointRequest<ValidateEstateSeparationInquiriesInput>
                    (serviceUrl, _userService.UserApplicationContext.Token, request), cancellationToken);
            }
            return apiResult;
        }


        protected override bool HasAccess(ValidateEstateSeparationInquiriesInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }

        public async Task<bool> MainDivisionRequestServiceIsEnabled(CancellationToken cancellationToken)
        {
            var configurationParameter = await _configurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_Main_Division_Request_Service_Is_Enabled")
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
        public async Task<GetUnitByIdViewModel> GetUnitByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUnitByLegacyIdQuery(legacyId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }
        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypes(string[] legacyId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new MeasurementUnitTypeByLegacyIdQuery(legacyId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }
    }


}
