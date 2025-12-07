using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetEstateInquiryLastNo2QueryHandler : BaseQueryHandler<GetEstateInquiryLastNo2Query, ApiResult<EstateInquiryLastNoViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        


        public GetEstateInquiryLastNo2QueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            
        }
        protected override bool HasAccess(GetEstateInquiryLastNo2Query request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

       
        protected async override Task<ApiResult<EstateInquiryLastNoViewModel>> RunAsync(GetEstateInquiryLastNo2Query request, CancellationToken cancellationToken)
        {
            EstateInquiryLastNoViewModel result = new();
            ApiResult<EstateInquiryLastNoViewModel> apiResult = new();
            var scriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            var maxNo = await _estateInquiryRepository.
                TableNoTracking.
                Where(x => x.ScriptoriumId == scriptoriumId && x.No2.StartsWith(request.Year + "880" + request.ScriptoriumCode)).
                Select(x => x.No2).MaxAsync(cancellationToken);

            result.LastNo = maxNo;
            apiResult.Data = result;
            return apiResult;
        }

    }
}
