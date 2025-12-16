using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
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

namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    internal class SetEstateSeparationElementsOwnershipsServiceHandler : BaseServiceHandler<SetEstateSeparationElementsOwnershipInput, ApiResult<SetEstateSeparationElementsOwnershipViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstatePieceTypeRepository _estatePieceTypeRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;

        public SetEstateSeparationElementsOwnershipsServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            , IRepository<ConfigurationParameter> configurationParameterRepository
            , IEstateSectionRepository estateSectionRepository
            , IEstateSubSectionRepository estateSubSectionRepository
            , IEstatePieceTypeRepository estatePieceTypeRepository
            , IEstateInquiryRepository estateInquiryRepository) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _configurationParameterRepository = configurationParameterRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _estatePieceTypeRepository = estatePieceTypeRepository;
            _estateInquiryRepository = estateInquiryRepository;
        }

        protected async override Task<ApiResult<SetEstateSeparationElementsOwnershipViewModel>> ExecuteAsync(SetEstateSeparationElementsOwnershipInput request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<SetEstateSeparationElementsOwnershipViewModel>();                        
            var mainDivisionRequestServiceIsEnabled = await MainDivisionRequestServiceIsEnabled(cancellationToken);
            if (!mainDivisionRequestServiceIsEnabled)
            {
                apiResult.Data = new SetEstateSeparationElementsOwnershipViewModel() { Result = true, ErrorMessage = "" };
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.Success;
                return apiResult;
            }
            else
            {

                await CorrectInput(request, cancellationToken);
                string serviceUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/EstateService/EstateSeparationElementsOwnership";
                return await _httpEndPointCaller.CallPostApiAsync<SetEstateSeparationElementsOwnershipViewModel, SetEstateSeparationElementsOwnershipQuery>(new HttpEndpointRequest<SetEstateSeparationElementsOwnershipQuery>
                    (serviceUrl, _userService.UserApplicationContext.Token, request.SetEstateSeparationElementsOwnershipQuery), cancellationToken);
            }
            return apiResult;
        }

        

        protected override bool HasAccess(SetEstateSeparationElementsOwnershipInput request, IList<string> userRoles)
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

        public async Task<GetUnitByIdViewModel> GetUnitById(string[] Id, CancellationToken cancellationToken)
        {
            var result = new GetUnitByIdViewModel();
            if(Id==null) return result;
            if (Id.Length == 0) return result;
            var response = await _mediator.Send(new GetUnitByIdQuery(Id), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }
        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypes(string[] Id, CancellationToken cancellationToken)
        {
            var result = new MeasurementUnitTypeByIdViewModel();
            if (Id == null) return result;
            if (Id.Length == 0) return result;
            var response = await _mediator.Send(new MeasurementUnitTypeByIdQuery(Id), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }
        public async Task<OrganizationViewModel> GetOrganizationByScriptoriumId(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var result = new OrganizationViewModel();
            if (scriptoriumId == null) return result;
            if(scriptoriumId.Length == 0)  return result; 
            var response = await _mediator.Send(new GetOrganizationByScriptoriumIdQuery(scriptoriumId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }
        public async Task<GetGeolocationByIdViewModel> GetGeoLocationById(int[] id,CancellationToken cancellationToken)
        {
            var result = new GetGeolocationByIdViewModel();
            if (id == null) return result;
            if (id.Length == 0) return result;
            var response = await _mediator.Send(new GetGeolocationByIdQuery(id) { FetchGeolocationOfIran = false }, cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }

        private async Task CorrectInput(SetEstateSeparationElementsOwnershipInput request, CancellationToken cancellationToken)
        {
            var estateInquiryIdList = new List<Guid>();
            if (!string.IsNullOrWhiteSpace(request.SetEstateSeparationElementsOwnershipQuery.InquiryId))
                estateInquiryIdList.Add(request.SetEstateSeparationElementsOwnershipQuery.InquiryId.ToGuid());
            if (request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList != null && request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList.Count > 0)
                estateInquiryIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList.Select(x => x.ToGuid()));
            List<EstateInquiry> estateInquiryList = new List<EstateInquiry>();
            if (estateInquiryIdList.Count > 0)
                estateInquiryList = await _estateInquiryRepository.TableNoTracking.Where(x => estateInquiryIdList.Contains(x.Id)).ToListAsync(cancellationToken);

            request.SetEstateSeparationElementsOwnershipQuery.NotaryOfficeCmsId = (await GetOrganizationByScriptoriumId(new string[] { request.SetEstateSeparationElementsOwnershipQuery.NotaryOfficeCmsId }, cancellationToken)).OrganizationList[0].LegacyId;

            var inquiry = estateInquiryList.Where(x => x.Id == request.SetEstateSeparationElementsOwnershipQuery.InquiryId.ToGuid()).First();
            request.SetEstateSeparationElementsOwnershipQuery.InquiryId = !string.IsNullOrWhiteSpace(inquiry.LegacyId) ? inquiry.LegacyId : inquiry.Id.ToString().Replace("_", "").Replace("-", "").ToUpper();
            if (request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList != null && request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList.Count > 0)
            {
                var lst = new List<string>();
                foreach (var inquiryId in request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList)
                {
                    var Inquiry = estateInquiryList.Where(x => x.Id == inquiryId.ToGuid()).First();
                    lst.Add(!string.IsNullOrWhiteSpace(Inquiry.LegacyId) ? Inquiry.LegacyId : Inquiry.Id.ToString().Replace("_", "").Replace("-", "").ToUpper());
                }
                request.SetEstateSeparationElementsOwnershipQuery.OtherRelatedInquiryList = lst;
            }
            var geoIdList = new List<int>();
            geoIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => !string.IsNullOrWhiteSpace(x.IssueLocationId)).Select(x => Convert.ToInt32(x.IssueLocationId)));
            geoIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => !string.IsNullOrWhiteSpace(x.BirthLocationId)).Select(x => Convert.ToInt32(x.BirthLocationId)));
            geoIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => !string.IsNullOrWhiteSpace(x.NationalityId)).Select(x => Convert.ToInt32(x.NationalityId)));
            GetGeolocationByIdViewModel geoLocations = new();
            if (geoIdList.Count > 0)
                geoLocations = await GetGeoLocationById(geoIdList.Distinct().ToArray(), cancellationToken);

            var measurmentUnitTypeIdList = new List<string>();
            measurmentUnitTypeIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => x.TheElementInfo != null && !string.IsNullOrWhiteSpace(x.TheElementInfo.AreaUnitId)).Select(x => x.TheElementInfo.AreaUnitId));
            measurmentUnitTypeIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ParentElement != null && !string.IsNullOrWhiteSpace(x.ParentElement.AreaUnitId)).Select(x => x.ParentElement.AreaUnitId));
            measurmentUnitTypeIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ChildElement != null && !string.IsNullOrWhiteSpace(x.ChildElement.AreaUnitId)).Select(x => x.ChildElement.AreaUnitId));
            MeasurementUnitTypeByIdViewModel measurementUnitTypes = new();
            if (measurmentUnitTypeIdList.Count > 0)
                measurementUnitTypes = await GetMeasurementUnitTypes(measurmentUnitTypeIdList.Distinct().ToArray(), cancellationToken);

            var UnitIdList = new List<string>();
            UnitIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => x.TheElementInfo != null && !string.IsNullOrWhiteSpace(x.TheElementInfo.UnitId)).Select(x => x.TheElementInfo.UnitId));
            UnitIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ParentElement != null && !string.IsNullOrWhiteSpace(x.ParentElement.UnitId)).Select(x => x.ParentElement.UnitId));
            UnitIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ChildElement != null && !string.IsNullOrWhiteSpace(x.ChildElement.UnitId)).Select(x => x.ChildElement.UnitId));
            GetUnitByIdViewModel Units = new();
            if (UnitIdList.Count > 0)
                Units = await GetUnitById(UnitIdList.Distinct().ToArray(), cancellationToken);

            var sectionIdList = new List<string>();
            sectionIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => x.TheElementInfo != null && !string.IsNullOrWhiteSpace(x.TheElementInfo.SectionId)).Select(x => x.TheElementInfo.SectionId));
            sectionIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ParentElement != null && !string.IsNullOrWhiteSpace(x.ParentElement.SectionId)).Select(x => x.ParentElement.SectionId));
            sectionIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ChildElement != null && !string.IsNullOrWhiteSpace(x.ChildElement.SectionId)).Select(x => x.ChildElement.SectionId));
            List<EstateSection> sections = new();
            if (sectionIdList.Count > 0)
            {
                sectionIdList = sectionIdList.Distinct().ToList();
                sections = await _estateSectionRepository.TableNoTracking.Where(x => sectionIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            var subSectionIdList = new List<string>();
            subSectionIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => x.TheElementInfo != null && !string.IsNullOrWhiteSpace(x.TheElementInfo.SubSectionId)).Select(x => x.TheElementInfo.SubSectionId));
            subSectionIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ParentElement != null && !string.IsNullOrWhiteSpace(x.ParentElement.SubSectionId)).Select(x => x.ParentElement.SubSectionId));
            subSectionIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ChildElement != null && !string.IsNullOrWhiteSpace(x.ChildElement.SubSectionId)).Select(x => x.ChildElement.SubSectionId));
            List<EstateSubsection> subSections = new();
            if (subSectionIdList.Count > 0)
            {
                subSectionIdList = subSectionIdList.Distinct().ToList();
                subSections = await _estateSubSectionRepository.TableNoTracking.Where(x => subSectionIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }
            var pieceTypeIdList = new List<string>();
            pieceTypeIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList.Where(x => x.TheElementInfo != null && !string.IsNullOrWhiteSpace(x.TheElementInfo.EPieceTypeId)).Select(x => x.TheElementInfo.EPieceTypeId));
            pieceTypeIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ParentElement != null && !string.IsNullOrWhiteSpace(x.ParentElement.EPieceTypeId)).Select(x => x.ParentElement.EPieceTypeId));
            pieceTypeIdList.AddRange(request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList.Where(x => x.ChildElement != null && !string.IsNullOrWhiteSpace(x.ChildElement.EPieceTypeId)).Select(x => x.ChildElement.EPieceTypeId));
            List<EstatePieceType> pieceTypes = new();
            if (pieceTypeIdList.Count > 0)
            {
                pieceTypeIdList = pieceTypeIdList.Distinct().ToList();
                pieceTypes = await _estatePieceTypeRepository.TableNoTracking.Include(x => x.EstatePieceMainType).Where(x => pieceTypeIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            foreach (var element in request.SetEstateSeparationElementsOwnershipQuery.TheElementOwnershipInfoList)
            {
                if (!string.IsNullOrWhiteSpace(element.BirthLocationId))
                    element.BirthLocationId = geoLocations.GeolocationList.Where(x => x.Id == element.BirthLocationId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(element.IssueLocationId))
                    element.IssueLocationId = geoLocations.GeolocationList.Where(x => x.Id == element.IssueLocationId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(element.NationalityId))
                    element.NationalityId = geoLocations.GeolocationList.Where(x => x.Id == element.NationalityId).First().LegacyId;
                if (element.TheElementInfo != null)
                {
                    if (!string.IsNullOrWhiteSpace(element.TheElementInfo.AreaUnitId))
                        element.TheElementInfo.AreaUnitId = measurementUnitTypes.MesurementUnitTypeList.Where(x => x.Id == element.TheElementInfo.AreaUnitId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.TheElementInfo.EKindPieceCode))
                        element.TheElementInfo.EKindPieceCode = pieceTypes.Where(x => x.Id == element.TheElementInfo.EPieceTypeId).First().EstatePieceMainType.Code;
                    if (!string.IsNullOrWhiteSpace(element.TheElementInfo.EPieceTypeId))
                        element.TheElementInfo.EPieceTypeId = pieceTypes.Where(x => x.Id == element.TheElementInfo.EPieceTypeId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.TheElementInfo.SectionId))
                        element.TheElementInfo.SectionId = sections.Where(x => x.Id == element.TheElementInfo.SectionId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.TheElementInfo.SubSectionId))
                        element.TheElementInfo.SubSectionId = subSections.Where(x => x.Id == element.TheElementInfo.SubSectionId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.TheElementInfo.UnitId))
                        element.TheElementInfo.UnitId = Units.UnitList.Where(x => x.Id == element.TheElementInfo.UnitId).First().LegacyId;
                }
            }
            foreach (var element in request.SetEstateSeparationElementsOwnershipQuery.TheElementsRelationInfoList)
            {

                if (element.ParentElement != null)
                {
                    if (!string.IsNullOrWhiteSpace(element.ParentElement.AreaUnitId))
                        element.ParentElement.AreaUnitId = measurementUnitTypes.MesurementUnitTypeList.Where(x => x.Id == element.ParentElement.AreaUnitId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ParentElement.EKindPieceCode))
                        element.ParentElement.EKindPieceCode = pieceTypes.Where(x => x.Id == element.ParentElement.EPieceTypeId).First().EstatePieceMainType.Code;
                    if (!string.IsNullOrWhiteSpace(element.ParentElement.EPieceTypeId))
                        element.ParentElement.EPieceTypeId = pieceTypes.Where(x => x.Id == element.ParentElement.EPieceTypeId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ParentElement.SectionId))
                        element.ParentElement.SectionId = sections.Where(x => x.Id == element.ParentElement.SectionId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ParentElement.SubSectionId))
                        element.ParentElement.SubSectionId = subSections.Where(x => x.Id == element.ParentElement.SubSectionId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ParentElement.UnitId))
                        element.ParentElement.UnitId = Units.UnitList.Where(x => x.Id == element.ParentElement.UnitId).First().LegacyId;
                }

                if (element.ChildElement != null)
                {
                    if (!string.IsNullOrWhiteSpace(element.ChildElement.AreaUnitId))
                        element.ChildElement.AreaUnitId = measurementUnitTypes.MesurementUnitTypeList.Where(x => x.Id == element.ChildElement.AreaUnitId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ChildElement.EKindPieceCode))
                        element.ChildElement.EKindPieceCode = pieceTypes.Where(x => x.Id == element.ChildElement.EPieceTypeId).First().EstatePieceMainType.Code;
                    if (!string.IsNullOrWhiteSpace(element.ChildElement.EPieceTypeId))
                        element.ChildElement.EPieceTypeId = pieceTypes.Where(x => x.Id == element.ChildElement.EPieceTypeId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ChildElement.SectionId))
                        element.ChildElement.SectionId = sections.Where(x => x.Id == element.ChildElement.SectionId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ChildElement.SubSectionId))
                        element.ChildElement.SubSectionId = subSections.Where(x => x.Id == element.ChildElement.SubSectionId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(element.ChildElement.UnitId))
                        element.ChildElement.UnitId = Units.UnitList.Where(x => x.Id == element.ChildElement.UnitId).First().LegacyId;
                }
            }

        }

    }
}
