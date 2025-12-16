

using Mapster;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using SSAA.Notary.DataTransferObject.ServiceInputs.SSOTokenValidation;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal class KatebSignRequestSearchQueryHandler : BaseQueryHandler<KatebSignRequestSearchQuery, ApiResult<KatebSignRequestGridViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        public KatebSignRequestSearchQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository;
        }

        protected override bool HasAccess(KatebSignRequestSearchQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin);
        }

        protected override async Task<ApiResult<KatebSignRequestGridViewModel>> RunAsync(KatebSignRequestSearchQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new();
            // 
            //  فیلدهایی که لیست می باشند را در متغییر زیر نیز قرار دهید و به صورت دستی باید فیلتر بزنید
            //
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "IsReadyToPay".ToLower(),
                "IsCostPaid".ToLower(),
                "IsRemote".ToLower(),
                "RemoteRequestId".ToLower(),
            };
            if (request.GridSortInput.Count > 0)
            {
                isOrderBy = true;
            }
            foreach (SortData item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            if (request.GridSortInput.Count == 0)
            {
                gridSortInput.Sort = "reqno";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }

            SSOTokenValidationServiceInput tokenValidationServiceInput = new();
            tokenValidationServiceInput.Token = request.ExtraParams.NationalNo;
            var tokenValidation = await _mediator.Send(tokenValidationServiceInput, cancellationToken);
            if (tokenValidation.IsSuccess)
            {
                request.ExtraParams.NationalNo = tokenValidation.Data;

                KatebSignRequestGrid databseResult = await _signRequestRepository.GetKatebSignRequestGridItems(request.PageIndex, request.PageSize,
                    request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                    _userService.UserApplicationContext.BranchAccess.BranchCode, request.ExtraParams, cancellationToken, isOrderBy);
                var result = databseResult.Adapt<KatebSignRequestGridViewModel>();

                foreach (var item in result.GridItems)
                {

                    if (request.SelectedItems.Contains(item.Id))
                    {
                        item.IsSelected = true;
                    }
                }

                foreach (var item in result.SelectedItems)
                {


                    if (request.SelectedItems.Contains(item.Id))
                    {
                        item.IsSelected = true;
                    }
                }

                return new ApiResult<KatebSignRequestGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
            }

            return new ApiResult<KatebSignRequestGridViewModel>(true, tokenValidation.statusCode, null, new List<string> { "عملیات اعتبارسنجی توکن کاربر با خطا مواجه شد." });
        }
    }
}
