using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetEstateInquiryLastNoQueryHandler : BaseQueryHandler<GetEstateInquiryLastNoQuery, ApiResult<EstateInquiryLastNoViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        


        public GetEstateInquiryLastNoQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            
        }
        protected override bool HasAccess(GetEstateInquiryLastNoQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

       
        protected async override Task<ApiResult<EstateInquiryLastNoViewModel>> RunAsync(GetEstateInquiryLastNoQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryLastNoViewModel result = new();
            ApiResult<EstateInquiryLastNoViewModel> apiResult = new();
            var scriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            var maxNo = await _estateInquiryRepository.
                TableNoTracking.
                Where(x => x.ScriptoriumId == scriptoriumId && x.No.StartsWith(request.Year + "700" + request.ScriptoriumCode)).
                Select(x => x.No).
                MaxAsync(cancellationToken);

            result.LastNo = maxNo;
            apiResult.Data = result;
            return apiResult;
        }

    }
}
