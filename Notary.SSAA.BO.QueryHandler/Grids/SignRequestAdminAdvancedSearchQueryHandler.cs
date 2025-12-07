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

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal class SignRequestForFOAdvancedSearchQueryHandler : BaseQueryHandler<SignRequestAdminAdvancedSearchQuery, ApiResult<SignRequestGridViewModel>>
    {
        private readonly ISignRequestRepository _advancedSearchGridRepository;

        public SignRequestForFOAdvancedSearchQueryHandler(IMediator mediator, IUserService userService,
            ISignRequestRepository advancedSearchGridRepository)
            : base(mediator, userService)
        {
            _advancedSearchGridRepository = advancedSearchGridRepository ?? throw new ArgumentNullException(nameof(advancedSearchGridRepository));


        }

        protected override bool HasAccess(SignRequestAdminAdvancedSearchQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.SSAAAdmin) ;
        }

        protected override async Task<ApiResult<SignRequestGridViewModel>> RunAsync(SignRequestAdminAdvancedSearchQuery request, CancellationToken cancellationToken)
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
                "persons".ToLower(),
                "PersonList".ToLower(),
                "ByteId".ToLower()
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
            SignRequestGrid databseResult = await _advancedSearchGridRepository.GetSignRequestAdminGridItems(request.PageIndex, request.PageSize,
                request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, request.ExtraParams, cancellationToken, isOrderBy);
            var result = databseResult.Adapt<SignRequestGridViewModel>();

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

            return new ApiResult<SignRequestGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { "..عملیات با موفقیت انجام شد" });
        }
    }
}


