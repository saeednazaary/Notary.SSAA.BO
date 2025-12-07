using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetSignDataTestQueryHandler : BaseQueryHandler<TestQuery, ApiResult<EstateInquiryResponsePrintViewModel>>
    {
        readonly IEstateInquiryRepository _estateInquiryRepository;
        public GetSignDataTestQueryHandler(IMediator mediator, IUserService userService, IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository;
        }
        protected override bool HasAccess(TestQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult<EstateInquiryResponsePrintViewModel>> RunAsync(TestQuery request, CancellationToken cancellationToken)
        {
            ApiResult<EstateInquiryResponsePrintViewModel> apiResult = new();
            var est = await _estateInquiryRepository.GetAsync(x => x.InquiryNo == request.InquiryNo && x.InquiryDate == request.InquiryDate && x.ScriptoriumId == _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (est != null)
            {
                if (est.ResponseDigitalsignature != null)
                {
                    apiResult.Data = new EstateInquiryResponsePrintViewModel();
                    apiResult.Data.FileName = "file.zip";
                    apiResult.Data.Data = est.ResponseDigitalsignature;
                    apiResult.Data.ContentType = "application/zip";
                }
            }
            return apiResult;
        }
    }
    public class TestQuery : BaseQueryRequest<ApiResult<EstateInquiryResponsePrintViewModel>>
    {
        public string InquiryNo { get; set; }
        public string InquiryDate { get; set; }
    }
}
