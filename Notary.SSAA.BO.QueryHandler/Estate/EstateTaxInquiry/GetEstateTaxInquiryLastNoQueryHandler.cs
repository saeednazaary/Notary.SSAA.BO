using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryLastNoQueryHandler : BaseQueryHandler<GetEstateTaxInquiryLastNoQuery, ApiResult<EstateInquiryLastNoViewModel>>
    {
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;



        public GetEstateTaxInquiryLastNoQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryRepository estateTaxInquiryRepository)
            : base(mediator, userService)
        {

            _estateTaxInquiryRepository = estateTaxInquiryRepository;

        }
        protected override bool HasAccess(GetEstateTaxInquiryLastNoQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }


        protected async override Task<ApiResult<EstateInquiryLastNoViewModel>> RunAsync(GetEstateTaxInquiryLastNoQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryLastNoViewModel result = new();
            ApiResult<EstateInquiryLastNoViewModel> apiResult = new();
            if (string.IsNullOrWhiteSpace(request.ScriptoriumCode) || string.IsNullOrWhiteSpace(request.Year)) return apiResult;

            var maxNo = await _estateTaxInquiryRepository.
                TableNoTracking.
                Where(x => x.No.StartsWith(request.Year + "211" + request.ScriptoriumCode)).
                Select(x => x.No).
                MaxAsync(cancellationToken);

            result.LastNo = maxNo;
            apiResult.Data = result;
            return apiResult;
        }

    }
}
