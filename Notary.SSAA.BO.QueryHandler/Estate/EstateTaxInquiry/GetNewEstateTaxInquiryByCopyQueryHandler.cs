using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
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
    public class GetNewEstateTaxInquiryByCopyQueryHandler : BaseQueryHandler<GetNewEstateTaxInquiryByCopyQuery, ApiResult<EstateTaxInquiryViewModel>>
    {
        private readonly IEstateTaxInquiryRepository _estatetaxInquiryRepository;       
        public GetNewEstateTaxInquiryByCopyQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryRepository estateTaxInquiryRepository)
            : base(mediator, userService)
        {
            _estatetaxInquiryRepository = estateTaxInquiryRepository;           
        }
        protected override bool HasAccess(GetNewEstateTaxInquiryByCopyQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult<EstateTaxInquiryViewModel>> RunAsync(GetNewEstateTaxInquiryByCopyQuery request, CancellationToken cancellationToken)
        {
            EstateTaxInquiryViewModel result = new();
            ApiResult<EstateTaxInquiryViewModel> apiResult = new();

            var estateTaxInquiry = await _estatetaxInquiryRepository.GetByIdAsync(cancellationToken, request.EstateTaxInquiryId.ToGuid());
           
            if (estateTaxInquiry != null)
            {
                await _estatetaxInquiryRepository.LoadReferenceAsync(estateTaxInquiry, x => x.WorkflowStates, cancellationToken);
                if (estateTaxInquiry.WorkflowStates.State == "0" || estateTaxInquiry.WorkflowStates.State == "8")
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.message.Add("امکان ثبت استعلام جدید وجود ندارد به دلیل اینکه استعلام مالیاتی شماره {0} ، قابل ویرایش و ارسال می باشد .لطفا از همین استعلام جهت ویرایش و ارسال آن به سازمان امور مالیاتی استفاده کنید  ");
                    apiResult.Data = null;
                    return apiResult;
                }
                await _estatetaxInquiryRepository.LoadReferenceAsync(estateTaxInquiry, x => x.EstateInquiry, cancellationToken);
                await _estatetaxInquiryRepository.LoadCollectionAsync(estateTaxInquiry, x => x.EstateTaxInquiryPeople, cancellationToken);
                await _estatetaxInquiryRepository.LoadCollectionAsync(estateTaxInquiry, x => x.EstateTaxInquiryAttaches, cancellationToken);

                result = EstateTaxInquiryMapper.EntityToViewModel(estateTaxInquiry);
                result.IsValid = true;
                result.InquiryId = string.Empty;
                result.IsNew = true;
                result.InquiryStatus = "0";
                result.InquiryTimestamp = 0;
                result.InquiryLastSendTime = string.Empty;
                result.InquiryCreateDate = string.Empty;
                result.InquiryTaxBillIdentity = string.Empty;
                result.InquiryTaxPaymentIdentity = string.Empty;
                result.InquiryLastReceiveStatusTime = string.Empty;
                result.InquiryStatusDescription = string.Empty;
                result.InquiryTrackingCode = string.Empty;
                result.InquiryTaxAmount = string.Empty;
                result.InquiryTaxBillIdentity = string.Empty;
                result.InquiryTaxPaymentIdentity = string.Empty;
                result.InquiryShebaNo = string.Empty;
                result.InquiryTaxBillHtml = string.Empty;
                result.InquiryNo = string.Empty;
                result.InquiryNo2 = string.Empty;
                var inquiryPersonViewModel = EstateTaxInquiryMapper.EntityToOwnerViewModel(estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108" && x.ChangeState != "3").First());
                result.TheInquiryOwner = inquiryPersonViewModel;
                result.TheInquiryOwner.PersonId = string.Empty;
                result.TheInquiryOwner.IsValid = true;
                result.TheInquiryOwner.IsNew = true;


                var buyers = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId != "108" && x.ChangeState != "3").ToList();
                foreach (var buyer in buyers)
                {
                    var buyerPersonViewModel = EstateTaxInquiryMapper.EntityToBuyerViewModel(buyer);

                    buyerPersonViewModel.IsValid = true;
                    buyerPersonViewModel.IsNew = true;
                    buyerPersonViewModel.PersonId = string.Empty;
                    result.TheInquiryBuyersList.Add(buyerPersonViewModel);

                }


                foreach (var estateAttach in estateTaxInquiry.EstateTaxInquiryAttaches)
                {
                    var estateAttachViewModel = EstateTaxInquiryMapper.EntityToViewModel(estateAttach);
                    estateAttachViewModel.IsValid = true;
                    estateAttachViewModel.IsNew = true;
                    estateAttachViewModel.AttachId = string.Empty;
                    result.TheInquiryEstateAttachList.Add(estateAttachViewModel);

                }
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام مالیاتی با شناسه داده شده یافت نشد");
            }
            return apiResult;
        }      
    }
}
