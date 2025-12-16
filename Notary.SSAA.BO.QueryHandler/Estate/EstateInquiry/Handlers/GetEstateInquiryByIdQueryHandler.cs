using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;


namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetEstateInquiryByIdQueryHandler : BaseQueryHandler<GetEstateInquiryByIdQuery, ApiResult<EstateInquiryViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;


        public GetEstateInquiryByIdQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(GetEstateInquiryByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        private async Task<Notary.SSAA.BO.Domain.Entities.EstateInquiry> GetEstateInquiry(GetEstateInquiryByIdQuery request, CancellationToken cancellationToken)
        {
            var scriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            if (!string.IsNullOrWhiteSpace(request.EstateInquiryId))
                return await _estateInquiryRepository.GetAsync(x => x.ScriptoriumId == scriptoriumId && x.Id == request.EstateInquiryId.ToGuid(), cancellationToken);
            else if (!string.IsNullOrWhiteSpace(request.LegacyId))
            {
                return await _estateInquiryRepository.GetAsync(x => x.ScriptoriumId == scriptoriumId && x.LegacyId == request.LegacyId, cancellationToken);
            }
            return null;
        }
        protected async override Task<ApiResult<EstateInquiryViewModel>> RunAsync(GetEstateInquiryByIdQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryViewModel result = new();
            ApiResult<EstateInquiryViewModel> apiResult = new();

            var command = new GetEstateSeparationInfoCommand(request.EstateInquiryId) { ReturnEstateInquiry = false };
            var serviceResponse = await this._mediator.Send(command, cancellationToken);

            var estateInquiry = await GetEstateInquiry(request, cancellationToken);
            if (estateInquiry != null)
            {
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);               
                result = EstateInquiryMapper.EntityToViewModel(estateInquiry);

                var inquiryPersonViewModel = EstateInquiryMapper.EntityToViewModel(estateInquiry.EstateInquiryPeople.First());
                result.InqInquiryPerson = inquiryPersonViewModel;
                result.IsValid = true;
                result.InqInquiryPerson.IsValid = true;
                if (inquiryPersonViewModel.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                {
                    if (!string.IsNullOrWhiteSpace(inquiryPersonViewModel.PersonNationalityCode))
                    {
                        var sabtAhvalPhotoListServiceQueryResult = await _mediator.Send(new GetPersonPhotoListServiceInput() { NationalNos = new List<string>() { result.InqInquiryPerson.PersonNationalityCode } }, cancellationToken);
                        if (sabtAhvalPhotoListServiceQueryResult.IsSuccess)
                        {
                            if (sabtAhvalPhotoListServiceQueryResult.Data != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Count > 0)
                                result.InqInquiryPerson.PersonalImage = Convert.ToBase64String(sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.First().PersonalImage);
                        }
                    }
                }
                result.ExtraParams = new EstateInquiryExtraParam
                {
                    CanDelete = false,
                    CanEdit = false,
                    CanSend = false,
                    CanArchive = false,
                    CanDeArchive = false,
                    IsFollowable = false,
                    RelatedServer = "BO"
                };

                if (estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NotSended)
                {
                    result.ExtraParams.CanDelete = true;
                    result.ExtraParams.CanEdit = true;
                    result.ExtraParams.CanSend = true;
                }
                else if (estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedCorrection)
                {
                    result.ExtraParams.CanEdit = true;
                    result.ExtraParams.CanSend = true;
                }
                else if (estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.ConfirmResponse || estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.RejectResponse)
                {
                    result.ExtraParams.CanArchive = true;
                }
                else if (estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.Archived)
                    result.ExtraParams.CanDeArchive = true;
                if ((estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.RejectResponse || estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.Archived) && estateInquiry.ResponseResult == "False")
                {

                    List<string> list = new();
                    await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.InverseEstateInquiryNavigation, cancellationToken);
                    if (estateInquiry.InverseEstateInquiryNavigation != null && estateInquiry.InverseEstateInquiryNavigation.Count > 0)
                    {
                        foreach (Domain.Entities.EstateInquiry inquiry in estateInquiry.InverseEstateInquiryNavigation)
                        {
                            list.Add(string.Format("استعلام پیرو به شماره {0} و به تاریخ {1} برای این استعلام ثبت شده است", inquiry.InquiryNo, inquiry.InquiryDate));
                        }
                        if (list.Count > 0)
                            result.ExtraParams.Message = string.Join(Environment.NewLine, list);
                    }
                    else if (!string.IsNullOrWhiteSpace(estateInquiry.SystemMessage))
                    {
                        list.Add(estateInquiry.SystemMessage);
                        result.ExtraParams.Message = string.Join(Environment.NewLine, list);
                    }
                }
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { estateInquiry.UnitId }, cancellationToken);
                var unitGeo = await _baseInfoServiceHelper.GetGeoLocationById(new string[] { unit.UnitList[0].GeoLocationId }, cancellationToken);
                if (unitGeo != null)
                {
                    if (unitGeo.GeolocationList[0].LegacyId == "12156A00CC1A490EB2CA2BB2841D5F1E")
                        result.IsTehranEstate = true;
                }
                if (!string.IsNullOrWhiteSpace(estateInquiry.ResponseResult) && estateInquiry.ResponseResult.Equals("true", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(estateInquiry.TrtsReadDate) && (estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.ConfirmResponse || estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.Archived))
                {
                    var scriptoprium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { estateInquiry.ScriptoriumId }, cancellationToken);
                    var responseDateTime = (estateInquiry.TrtsReadDate + (!string.IsNullOrWhiteSpace(estateInquiry.TrtsReadTime) ? "-" + estateInquiry.TrtsReadTime : "")).ToDateTime();
                    if (scriptoprium.ScriptoriumList[0].GeoLocationId == unit.UnitList[0].GeoLocationId)
                    {
                        if (responseDateTime.HasValue && DateTime.Now.Subtract(responseDateTime.Value).TotalDays < 37)
                        {
                            result.ExtraParams.IsFollowable = true;
                        }
                    }
                    else
                    {
                        if (responseDateTime.HasValue && DateTime.Now.Subtract(responseDateTime.Value).TotalDays < 50)
                        {
                            result.ExtraParams.IsFollowable = true;
                        }
                    }
                }
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام ملک مربوطه یافت نشد");
            }
            return apiResult;
        }      
    }
}
