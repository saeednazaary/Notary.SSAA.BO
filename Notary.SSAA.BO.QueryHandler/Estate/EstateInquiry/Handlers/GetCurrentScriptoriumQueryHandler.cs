using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetCurrentScriptoriumQueryHandler : BaseQueryHandler<GetCurrentScriptoriumQuery, ApiResult<GetCurrentScriptoriumViewModel>>
    {
        public GetCurrentScriptoriumQueryHandler(IMediator mediator, IUserService userService)
            : base(mediator, userService)
        {
        }
        protected override bool HasAccess(GetCurrentScriptoriumQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult<GetCurrentScriptoriumViewModel>> RunAsync(GetCurrentScriptoriumQuery request, CancellationToken cancellationToken)
        {
            ApiResult<GetCurrentScriptoriumViewModel> apiResult = new();
            await Task.Run(() =>
            {
                GetCurrentScriptoriumViewModel result = new()
                {
                    Title = _userService.UserApplicationContext.BranchAccess.BranchName,
                    Code = _userService.UserApplicationContext.BranchAccess.BranchCode,
                    Id = _userService.UserApplicationContext.BranchAccess.BranchId
                };
                apiResult.Data = result;
            },cancellationToken);
            return apiResult;
        }
    }
}
