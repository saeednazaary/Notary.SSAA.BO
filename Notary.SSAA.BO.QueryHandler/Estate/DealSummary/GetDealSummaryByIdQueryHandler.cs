using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.QueryHandler.Estate.DealSummary
{
    public class GetDealSummaryByIdQueryHandler : BaseQueryHandler<GetDealSummaryByIdQuery, ApiResult<DealSummaryViewModel>>
    {

        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;


        public GetDealSummaryByIdQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryRepository dealSummaryRepository)
            : base(mediator, userService)
        {

            _dealSummaryRepository = dealSummaryRepository ?? throw new ArgumentNullException(nameof(dealSummaryRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(GetDealSummaryByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DealSummaryViewModel>> RunAsync(GetDealSummaryByIdQuery request, CancellationToken cancellationToken)
        {
            DealSummaryViewModel result = new();
            ApiResult<DealSummaryViewModel> apiResult = new();

            Notary.SSAA.BO.Domain.Entities.DealSummary dealSummary = null;
            if (!string.IsNullOrWhiteSpace(request.DealSummaryId))
                dealSummary = await _dealSummaryRepository.GetDealSummaryById(request.DealSummaryId, cancellationToken);
            else if (!string.IsNullOrWhiteSpace(request.LegacyId))
                dealSummary = await _dealSummaryRepository.GetDealSummaryByLegacyId(request.LegacyId, cancellationToken);
            if (dealSummary != null)
            {

                
                result = DealSummaryMapper.EntityToViewModel(dealSummary);
                result.IsValid = true;
                result.DS_Persons = new List<DealSummaryPersonViewModel>();
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { dealSummary.EstateInquiry.UnitId }, cancellationToken);               
                var geolocationList = await GetGeoLocations(dealSummary, cancellationToken);
                var measurementUnitTypeList = await GetMeasurementUnitTypes(dealSummary, cancellationToken);

                var sabtAhvalPhotoListServiceQuery = new GetPersonPhotoListServiceInput();
                sabtAhvalPhotoListServiceQuery.NationalNos = new List<string>();
                sabtAhvalPhotoListServiceQuery.NationalNos = dealSummary.DealSummaryPeople.Where(x => x.PersonType == EstateConstant.PersonTypeConstant.RealPerson).Select(x => x.NationalityCode).ToList();
                ApiResult<GetPersonPhotoListViewModel> sabtAhvalPhotoListServiceQueryResult = null;
                sabtAhvalPhotoListServiceQueryResult = await _mediator.Send(sabtAhvalPhotoListServiceQuery, cancellationToken);
                

                result.DS_Unit = unit.UnitList[0].Name;
                result.DS_Scriptorium = _userService.UserApplicationContext.BranchAccess.BranchName;
                result.DS_GeoLocation = geolocationList.GeolocationList.Where(x => x.Id == dealSummary.EstateInquiry.GeoLocationId.ToString()).First().Name;
                if (!string.IsNullOrWhiteSpace(dealSummary.AmountUnitId))
                    result.DS_AmountType = measurementUnitTypeList.MesurementUnitTypeList.Where(m => m.Id == dealSummary.AmountUnitId).First().Name;
                if (!string.IsNullOrWhiteSpace(dealSummary.TimeUnitId))
                    result.DS_DurationType = measurementUnitTypeList.MesurementUnitTypeList.Where(m => m.Id == dealSummary.TimeUnitId).First().Name;
                foreach (DealSummaryPerson person in dealSummary.DealSummaryPeople)
                {
                    var dealSumaryPersonViewModel = DealSummaryMapper.EntityToViewModel(person);
                   
                    if (dealSumaryPersonViewModel.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                    {
                        if (!string.IsNullOrWhiteSpace(dealSumaryPersonViewModel.PersonNationalityCode))
                        {
                            if (sabtAhvalPhotoListServiceQueryResult != null && sabtAhvalPhotoListServiceQueryResult.IsSuccess)
                            {
                                if (sabtAhvalPhotoListServiceQueryResult.Data != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Count > 0)
                                    dealSumaryPersonViewModel.PersonImage = Convert.ToBase64String(sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Where(p => p.NationalNo == dealSumaryPersonViewModel.PersonNationalityCode).First().PersonalImage);
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(dealSumaryPersonViewModel.PersonCity))
                        dealSumaryPersonViewModel.PersonCity = geolocationList.GeolocationList.Where(x => x.Id == dealSumaryPersonViewModel.PersonCity).First().Name;
                    if (!string.IsNullOrWhiteSpace(dealSumaryPersonViewModel.PersonIssuePlace))
                        dealSumaryPersonViewModel.PersonIssuePlace = geolocationList.GeolocationList.Where(x => x.Id == dealSumaryPersonViewModel.PersonIssuePlace).First().Name;
                    if (!string.IsNullOrWhiteSpace(dealSumaryPersonViewModel.PersonBirthPlace))
                        dealSumaryPersonViewModel.PersonBirthPlace = geolocationList.GeolocationList.Where(x => x.Id == dealSumaryPersonViewModel.PersonBirthPlace).First().Name;
                    if (!string.IsNullOrWhiteSpace(dealSumaryPersonViewModel.PersonNationality))
                        dealSumaryPersonViewModel.PersonNationality = geolocationList.GeolocationList.Where(x => x.Id == dealSumaryPersonViewModel.PersonNationality).First().Name;
                    result.DS_Persons.Add(dealSumaryPersonViewModel);
                }
                result.ExtraParams = new DealSummaryExtraParam
                {
                    CanDelete = false,
                    CanEdit = false,
                    CanSend = false,
                    CanUnRestrict = false,
                    RelatedServer = "BO"
                };
                if (dealSummary.DealSummaryTransferType.Isrestricted == "1")
                {
                    if (dealSummary.WorkflowStatesId == EstateConstant.DealSummaryStates.Responsed)
                        result.ExtraParams.CanUnRestrict = true;
                }
               
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("خلاصه معامله مربوطه یافت نشد");
            }
            return apiResult;
        }             
        public async Task<GetGeolocationByIdViewModel> GetGeoLocations(Domain.Entities.DealSummary dealSummary, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.IssuePlaceId.HasValue).Select(x => x.IssuePlaceId.Value.ToString()).ToList());
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.CityId.HasValue).Select(x => x.CityId.Value.ToString()).ToList());
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.BirthPlaceId.HasValue).Select(x => x.BirthPlaceId.Value.ToString()).ToList());
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.NationalityId.HasValue).Select(x => x.NationalityId.Value.ToString()).ToList());
            if (dealSummary.EstateInquiry.GeoLocationId.HasValue)
                list.Add(dealSummary.EstateInquiry.GeoLocationId.Value.ToString());
            if (list.Count == 0) return new GetGeolocationByIdViewModel();
            return await _baseInfoServiceHelper.GetGeoLocationById(list.ToArray(), cancellationToken);

        }
        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypes(Domain.Entities.DealSummary dealSummary, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(dealSummary.AmountUnitId)) list.Add(dealSummary.AmountUnitId);
            if(!string.IsNullOrWhiteSpace(dealSummary.TimeUnitId)) list.Add(dealSummary.TimeUnitId);
            if (list.Count == 0) return new MeasurementUnitTypeByIdViewModel();
            return await _baseInfoServiceHelper.GetMeasurementUnitTypeById(list.ToArray(), cancellationToken);
        }
    }
}
