using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    internal class GetEstateSeparationElementsServiceHandler : BaseServiceHandler<GetEstateSeparationElementsInput, ApiResult<GetEstateSeparationElementsViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IRepository<EstateDivisionRequestElement> _estateDivisionRequestElement;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstatePieceTypeRepository _estatePieceTypeRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
     
        public GetEstateSeparationElementsServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            , IRepository<EstateDivisionRequestElement> estateDivisionRequestElement
            , IRepository<ConfigurationParameter> configurationParameterRepository
            , IEstateSectionRepository estateSectionRepository
            , IEstateSubSectionRepository estateSubSectionRepository
            , IEstatePieceTypeRepository estatePieceTypeRepository
            , IEstateInquiryRepository estateInquiryRepository) : base(mediator, userService, logger)
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

        protected async override Task<ApiResult<GetEstateSeparationElementsViewModel>> ExecuteAsync(GetEstateSeparationElementsInput request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<GetEstateSeparationElementsViewModel>();
            var mainDivisionRequestServiceIsEnabled = await MainDivisionRequestServiceIsEnabled(cancellationToken);
            if (!mainDivisionRequestServiceIsEnabled)
            {
                apiResult.Data = await GetFakeElements(request.EstateInquiryId[0], cancellationToken);
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.Success;
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

                string serviceUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/EstateService/EstateSeparationElements";
                apiResult = await _httpEndPointCaller.CallPostApiAsync<GetEstateSeparationElementsViewModel, GetEstateSeparationElementsInput>(new HttpEndpointRequest<GetEstateSeparationElementsInput>
                    (serviceUrl, _userService.UserApplicationContext.Token, request), cancellationToken);
                if (apiResult.IsSuccess)
                {
                    if (apiResult.Data != null && string.IsNullOrWhiteSpace(apiResult.Data.ErrorMessage) && apiResult.Data.Separation != null)
                    {
                        await CorrectResult(apiResult, cancellationToken);
                    }
                }
            }
            return apiResult;
        }

        private async Task CorrectResult(ApiResult<GetEstateSeparationElementsViewModel> apiResult, CancellationToken cancellationToken)
        {
            var unitLegacyIdList = new List<string>
                        {
                            apiResult.Data.Separation.UnitId
                        };
            unitLegacyIdList.AddRange(apiResult.Data.Separation.TheSeparationElements.Select(x => x.UnitId));
            var units = await GetUnitByLegacyId(unitLegacyIdList.Distinct().ToArray(), cancellationToken);

            apiResult.Data.Separation.UnitId = units.UnitList.Where(x => x.LegacyId == apiResult.Data.Separation.UnitId).First().Id;
            foreach (var element in apiResult.Data.Separation.TheSeparationElements)
            {
                element.UnitId = units.UnitList.Where(x => x.LegacyId == element.UnitId).First().Id;
                element.SectionId = (await _estateSectionRepository.GetAsync(x => x.LegacyId == element.SectionId, cancellationToken)).Id;
                element.SubSectionId = (await _estateSubSectionRepository.GetAsync(x => x.LegacyId == element.SubSectionId, cancellationToken)).Id;
                element.AreaUnitId = (await GetMeasurementUnitTypeByLegacyId(new string[] { element.AreaUnitId }, cancellationToken)).MesurementUnitTypeList[0].Id;
            }
        }

        protected override bool HasAccess(GetEstateSeparationElementsInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
        private async Task<GetEstateSeparationElementsViewModel> GetFakeElements(string estateInquiryId, CancellationToken cancellationToken)
        {
            var result = new GetEstateSeparationElementsViewModel();
            EstateDivisionRequestElement prevElements = null;
            DateTime dt = DateTime.Now;
            string basic = "";
            string secondary = "";
            string sectionId = "";
            string subsectionId = "";
            string UnitId = "";

            var inquiry = await _estateInquiryRepository.TableNoTracking.Where(x => x.Id == estateInquiryId.ToGuid()).FirstOrDefaultAsync(cancellationToken);
            basic = inquiry.Basic;
            secondary = inquiry.Secondary;
            sectionId = inquiry.EstateSectionId;
            subsectionId = inquiry.EstateSubsectionId;
            UnitId = inquiry.UnitId;

            var iddt = inquiry.InquiryDate.ToDateTime();
            if (iddt.HasValue)
                dt = iddt.Value;
            prevElements = await _estateDivisionRequestElement
                           .TableNoTracking
                           .Where(x =>
                                  x.Unitid == inquiry.UnitId &&
                                  x.EstateSectionId == inquiry.EstateSectionId &&
                                  x.EstateSubsectionId == inquiry.EstateSubsectionId &&
                                  x.EstateBasic == inquiry.Basic &&
                                  x.EstateSecondary == inquiry.Secondary
                                  )
                           .FirstOrDefaultAsync(cancellationToken);

            if (prevElements != null)
            {
                result.Separation = JsonConvert.DeserializeObject<Separation>(prevElements.ElementsJson);
                return result;
            }



            var dtNew = dt.AddDays(-10);
            var dtNew1 = dt.AddDays(-3);


            int n = SeriouslyRandom.Next(1, 10000);
            var m = n % 2;
            int functionType = 2;

            result.Separation = new Separation()
            {
                TheSeparationElements = new List<SeparationElement>(),
                NatureDate = dtNew.ToPersianDate(),
                MappingDate = dtNew1.ToPersianDate(),
                NatureNum = SeriouslyRandom.Next(1000, 9999).ToString(),
                Agent = "-",
                Description = "-",
                EEstateId = Guid.NewGuid().ToString().Replace("-", "").Replace("_", ""),
                EngineerLicenseNo = SeriouslyRandom.Next(10000000, 99999999).ToString(),
                EngineerName = "-",
                FunctionType = functionType,
                Id = Guid.NewGuid().ToString().Replace("-", "").Replace("_", ""),
                UnitId = UnitId,
                PrintedText = "-"
            };
            int elementsCount = result.Separation.FunctionType == 1 ? SeriouslyRandom.Next(10, 80) : SeriouslyRandom.Next(5, 20);
            var baseSecondary = SeriouslyRandom.Next(1000, 99999);
            for (int i = 0; i < elementsCount; i++)
            {
                var element = new SeparationElement()
                {
                    Area = SeriouslyRandom.Next(60, 200),
                    AreaUnitId = "1",
                    Block = "",
                    Class = (i + 1).ToString(),
                    EastLimit = "-",
                    EKindPieceCode = result.Separation.FunctionType == 2 ? "3" : "D1",
                    EPieceType = result.Separation.FunctionType == 2 ? "اپارتمان مسکونی" : "زمین",
                    EPieceTypeId = result.Separation.FunctionType == 2 ? "0149" : "0770",
                    EstateHightLaw = "",
                    HasOwnership = false,
                    IsAssigned = false,
                    NorthLimit = "-",
                    OtherDescription = "-",
                    PlaqueOriginal = basic,
                    ProfitLaw = "-",
                    SectionId = sectionId,
                    Sector = (i + 1).ToString(),
                    Separate = secondary,
                    Side = "-",
                    SidewayPlaque = baseSecondary.ToString(),
                    SouthLimit = "-",
                    SubSectionId = subsectionId,
                    UnitId = UnitId,
                    WestLimit = "-",
                };
                baseSecondary++;
                result.Separation.TheSeparationElements.Add(element);
            }
            result.Separation.TheSeparationElements[1].HasOwnership = true;
            result.Separation.TheSeparationElements[elementsCount - 1].HasOwnership = true;
            if (result.Separation.FunctionType == 2)
            {
                for (int i = 0; i < elementsCount; i++)
                {
                    var element = new SeparationElement()
                    {
                        Area = SeriouslyRandom.Next(2, 5),
                        AreaUnitId = "1",
                        Block = "",
                        Class = "0",
                        EastLimit = "-",
                        EKindPieceCode = "4",
                        EPieceType = "انباری",
                        EPieceTypeId = "0130",
                        EstateHightLaw = "",
                        HasOwnership = false,
                        IsAssigned = false,
                        NorthLimit = "-",
                        OtherDescription = "-",
                        PlaqueOriginal = "",
                        ProfitLaw = "-",
                        SectionId = sectionId,
                        Sector = i.ToString(),
                        Separate = secondary,
                        Side = "-",
                        SidewayPlaque = "",
                        SouthLimit = "-",
                        SubSectionId = subsectionId,
                        UnitId = UnitId,
                        WestLimit = "-",
                    };

                    result.Separation.TheSeparationElements.Add(element);
                }
                for (int i = 0; i < elementsCount; i++)
                {
                    var element = new SeparationElement()
                    {
                        Area = SeriouslyRandom.Next(8, 10),
                        AreaUnitId = "1",
                        Block = "",
                        Class = "0",
                        EastLimit = "-",
                        EKindPieceCode = "5",
                        EPieceType = "پارگینگ",
                        EPieceTypeId = "0842",
                        EstateHightLaw = "",
                        HasOwnership = false,
                        IsAssigned = false,
                        NorthLimit = "-",
                        OtherDescription = "-",
                        PlaqueOriginal = "",
                        ProfitLaw = "-",
                        SectionId = sectionId,
                        Sector = i.ToString(),
                        Separate = secondary,
                        Side = "-",
                        SidewayPlaque = "",
                        SouthLimit = "-",
                        SubSectionId = subsectionId,
                        UnitId = UnitId,
                        WestLimit = "-",
                    };

                    result.Separation.TheSeparationElements.Add(element);
                }
            }
            var json = JsonConvert.SerializeObject(result.Separation);
            await _estateDivisionRequestElement.AddAsync(new EstateDivisionRequestElement()
            {
                ElementsJson = json,
                EstateBasic = basic,
                EstateSecondary = secondary,
                EstateSectionId = sectionId,
                EstateSubsectionId = subsectionId,
                Unitid = UnitId,
                Id = Guid.NewGuid()
            }, cancellationToken);
            return result;
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
       
        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypeByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var result = new MeasurementUnitTypeByIdViewModel();
            if (legacyId == null) return result;
            if (legacyId.Length == 0) return result;
            var response = await _mediator.Send(new MeasurementUnitTypeByLegacyIdQuery(legacyId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<GetUnitByIdViewModel> GetUnitByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var result = new GetUnitByIdViewModel();
            if (legacyId == null) return result;
            if (legacyId.Length == 0) return result;
            var response = await _mediator.Send(new GetUnitByLegacyIdQuery(legacyId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
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

    public static class SeriouslyRandom
    {
        /// <summary>
        /// Holds the current seed value.
        /// </summary>
        private static int seed = Environment.TickCount;

        /// <summary>
        /// Holds a separate instance of Random per thread.
        /// </summary>
        private static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() =>
                new Random(Interlocked.Increment(ref seed)));

        /// <summary>
        /// Returns a Seriously Random value.
        /// </summary>
        public static int Next(int minValue, int maxValue)
        {
            return random.Value.Next(minValue, maxValue);
        }
    }
}
