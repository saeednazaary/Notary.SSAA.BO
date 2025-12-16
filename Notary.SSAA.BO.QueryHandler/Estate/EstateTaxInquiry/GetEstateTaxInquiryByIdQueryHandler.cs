using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryByIdQueryHandler : BaseQueryHandler<GetEstateTaxInquiryByIdQuery, ApiResult<EstateTaxInquiryViewModel>>
    {
        private readonly IEstateTaxInquiryRepository _estatetaxInquiryRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;

        public GetEstateTaxInquiryByIdQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryRepository estateTaxInquiryRepository, IRepository<ConfigurationParameter> configurationParameterRepository)
            : base(mediator, userService)
        {

            _estatetaxInquiryRepository = estateTaxInquiryRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(GetEstateTaxInquiryByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateTaxInquiryViewModel>> RunAsync(GetEstateTaxInquiryByIdQuery request, CancellationToken cancellationToken)
        {
            EstateTaxInquiryViewModel result = new();
            ApiResult<EstateTaxInquiryViewModel> apiResult = new();

            Notary.SSAA.BO.Domain.Entities.EstateTaxInquiry estateTaxInquiry = null;

            if (!string.IsNullOrWhiteSpace(request.EstateTaxInquiryId))
                estateTaxInquiry = await _estatetaxInquiryRepository
                        .TableNoTracking
                        .Include(x => x.EstateInquiry)
                       .Include(x => x.WorkflowStates)
                       .Include(x => x.EstateTaxInquiryPeople)
                       .Include(x => x.EstateTaxInquiryAttaches)
                       .ThenInclude(y => y.EstateSideType)
                        .Include(x => x.EstateTaxInquiryAttaches)
                       .ThenInclude(y => y.EstatePieceType)
                       .Include(x => x.EstateTaxInquiryFiles)
                        .Where(x => x.Id == request.EstateTaxInquiryId.ToGuid())
                        .FirstAsync(cancellationToken);

            else if (!string.IsNullOrWhiteSpace(request.LegacyId))
                estateTaxInquiry = await _estatetaxInquiryRepository
                       .TableNoTracking
                       .Include(x => x.EstateInquiry)
                       .Include(x => x.WorkflowStates)
                       .Include(x => x.EstateTaxInquiryPeople)
                       .Include(x => x.EstateTaxInquiryAttaches)
                       .ThenInclude(y => y.EstateSideType)
                        .Include(x => x.EstateTaxInquiryAttaches)
                       .ThenInclude(y => y.EstatePieceType)
                       .Include(x => x.EstateTaxInquiryFiles)
                       .Where(x => x.LegacyId == request.LegacyId)
                       .FirstAsync(cancellationToken);
            if (estateTaxInquiry != null)
            {


                result = EstateTaxInquiryMapper.EntityToViewModel(estateTaxInquiry);

                result.IsValid = true;
                var inquiryPersonViewModel = EstateTaxInquiryMapper.EntityToOwnerViewModel(estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108" && x.ChangeState != "3").First());
                result.TheInquiryOwner = inquiryPersonViewModel;
                result.TheInquiryOwner.IsValid = true;

                var sabtAhvalPhotoListServiceQuery = new SabtAhvalPhotoListServiceQuery();
                sabtAhvalPhotoListServiceQuery.NationalNos = new List<string>();
                sabtAhvalPhotoListServiceQuery.NationalNos = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.PersonType == EstateConstant.PersonTypeConstant.RealPerson).Select(x => x.NationalityCode).ToList();
                ApiResult<SabtAhvalPhotoListServiceViewModel> sabtAhvalPhotoListServiceQueryResult = null;

                sabtAhvalPhotoListServiceQueryResult = await _mediator.Send(sabtAhvalPhotoListServiceQuery, cancellationToken);


                if (inquiryPersonViewModel.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                {
                    if (!string.IsNullOrWhiteSpace(inquiryPersonViewModel.PersonNationalityCode))
                    {

                        if (sabtAhvalPhotoListServiceQueryResult != null && sabtAhvalPhotoListServiceQueryResult.IsSuccess)
                        {

                            if (sabtAhvalPhotoListServiceQueryResult.Data != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Count > 0)
                                inquiryPersonViewModel.PersonalImage = Convert.ToBase64String(sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Where(x => x.NationalNo == inquiryPersonViewModel.PersonNationalityCode).First().PersonalImage);

                        }
                    }
                }
                result.TheInquiryBuyersList = new List<EstateTaxInquiryBuyerViewModel>();
                result.TheInquiryEstateAttachList = new List<EstateTaxInquiryAttachViewModel>();
                var buyers = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId != "108" && x.ChangeState != "3").ToList();
                foreach (var buyer in buyers)
                {
                    var buyerPersonViewModel = EstateTaxInquiryMapper.EntityToBuyerViewModel(buyer);
                    if (buyerPersonViewModel.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                    {
                        if (!string.IsNullOrWhiteSpace(buyerPersonViewModel.PersonNationalityCode))
                        {
                            if (sabtAhvalPhotoListServiceQueryResult != null && sabtAhvalPhotoListServiceQueryResult.IsSuccess)
                            {
                                if (sabtAhvalPhotoListServiceQueryResult.Data != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData != null && sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Count > 0)
                                    buyerPersonViewModel.PersonalImage = Convert.ToBase64String(sabtAhvalPhotoListServiceQueryResult.Data.PersonsData.Where(x => x.NationalNo == buyerPersonViewModel.PersonNationalityCode).First().PersonalImage);

                            }
                        }
                    }
                    result.TheInquiryBuyersList.Add(buyerPersonViewModel);
                    buyerPersonViewModel.IsValid = true;
                }


                foreach (var estateAttach in estateTaxInquiry.EstateTaxInquiryAttaches)
                {
                    if (estateAttach.ChangeState != "3")
                    {
                        var estateAttachViewModel = EstateTaxInquiryMapper.EntityToViewModel(estateAttach);
                        result.TheInquiryEstateAttachList.Add(estateAttachViewModel);
                        estateAttachViewModel.IsValid = true;
                    }
                }
                result.ExtraParams = new EstateTaxInquiryExtraParam();
                result.ExtraParams.RelatedServer = "FO";


                if (estateTaxInquiry.WorkflowStates.State == "0" || estateTaxInquiry.WorkflowStates.State == "8" || estateTaxInquiry.WorkflowStates.State == "33" || estateTaxInquiry.WorkflowStates.State == "41" || estateTaxInquiry.WorkflowStates.State == "43")
                {
                    result.ExtraParams.CanEdit = true;
                    result.ExtraParams.CanSend = true;
                }
                if (estateTaxInquiry.WorkflowStates.State == "0")
                    result.ExtraParams.CanDelete = true;
                if (estateTaxInquiry.WorkflowStates.State != "0" && estateTaxInquiry.WorkflowStates.State != "9")
                    result.ExtraParams.CanCancel = true;

                var bv = estateTaxInquiry.EstateTaxInquiryFiles.Where(x => string.IsNullOrWhiteSpace(x.ArchiveMediaFileId) && x.FileContent != null).Any();
                if (bv)
                {
                    await _mediator.Send(new SendEstateTaxInquiryFilesToArchiveCommand(request.EstateTaxInquiryId), cancellationToken);
                }

                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { estateTaxInquiry.EstateUnitId }, cancellationToken);
                var unitGeo = await _baseInfoServiceHelper.GetGeoLocationById(new string[] { unit.UnitList[0].GeoLocationId }, cancellationToken);
                if (unitGeo != null)
                {
                    if (unitGeo.GeolocationList[0].LegacyId == "12156A00CC1A490EB2CA2BB2841D5F1E")
                        result.IsTehranEstate = true;
                }
                if (estateTaxInquiry.WorkflowStates.State == "30")
                {
                    if (!string.IsNullOrWhiteSpace(estateTaxInquiry.ShebaNo))
                        result.InquiryStatusDescription = string.Format("محاسبه مبلغ مالیاتی انجام شده است که با شناسه قبض {0} و شناسه پرداخت {1} قابل پرداخت می باشد", estateTaxInquiry.TaxBillIdentity, estateTaxInquiry.TaxPaymentIdentity);
                    else
                        result.InquiryStatusDescription = string.Format("محاسبه مبلغ مالیاتی انجام شده است که با شناسه پرداخت {0} و شماره شبا {1} قابل پرداخت می باشد", estateTaxInquiry.TaxBillIdentity, estateTaxInquiry.ShebaNo);
                }
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام مالیاتی مربوطه یافت نشد");
            }
            return apiResult;
        }        
    }
}
