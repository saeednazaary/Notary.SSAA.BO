using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal class ExecutiveRequestGridQueryHandler : BaseQueryHandler<ExecutiveRequestGridQuery, ApiResult<ExecutiveRequestGridViewModel>>
    {
        private readonly IExecutiveRequestRepository _executiveRequestRepository;

        public ExecutiveRequestGridQueryHandler(IMediator mediator, IUserService userService,
            IExecutiveRequestRepository executiveRequestRepository)
            : base(mediator, userService)
        {
            _executiveRequestRepository = executiveRequestRepository ?? throw new ArgumentNullException(nameof(executiveRequestRepository));


        }

        protected override bool HasAccess(ExecutiveRequestGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ExecutiveRequestGridViewModel>> RunAsync(ExecutiveRequestGridQuery request, CancellationToken cancellationToken)
        {
            ExecutiveRequestGridViewModel result = new();
            IDictionary<string, string> searchMap = new Dictionary<string, string>();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            // 
            //  فیلدهایی که لیست می باشند را در متغییر زیر نیز قرار دهید و به صورت دستی باید فیلتر بزنید
            //
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "ExecutiveRequestPerson".ToLower(),
                "ExecutiveRequestPersonList".ToLower()
            };
            if (request.GridSortInput.Count > 0)
            {
                isOrderBy = true;
            }
            foreach (var item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            if (request.GridSortInput.Count == 0)
            {
                gridSortInput.Sort = "executiverequestno";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }
            var resultTemp = await _executiveRequestRepository.GetExecutiveRequestGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken, isOrderBy);
            result.SelectedItems = resultTemp.SelectedItems;
            result.GridItems = resultTemp.GridItems;
            result.TotalCount = resultTemp.TotalCount;
            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }
            return new ApiResult<ExecutiveRequestGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}


