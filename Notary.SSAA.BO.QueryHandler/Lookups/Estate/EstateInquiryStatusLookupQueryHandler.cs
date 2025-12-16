using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class EstateInquiryStatusLookupQueryHandler : BaseQueryHandler<EstateInquiryStatusLookupQuery, ApiResult<EstateInquiryStatusLookupViewModel>>
    {
        private readonly IWorkfolwStateRepository _workfolwStateRepository;

        public EstateInquiryStatusLookupQueryHandler(IMediator mediator, IUserService userService,
            IWorkfolwStateRepository Repository)
            : base(mediator, userService)
        {
            _workfolwStateRepository = Repository ?? throw new ArgumentNullException(nameof(Repository));
        }

        protected override bool HasAccess(EstateInquiryStatusLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<EstateInquiryStatusLookupViewModel>> RunAsync(EstateInquiryStatusLookupQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryStatusLookupViewModel result = new();

            bool isOrderBy = false;
            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()

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
                gridSortInput.Sort = "State";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }
            var resultTemp = await _workfolwStateRepository.GetEstateInquiryStatusLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, isOrderBy, cancellationToken);
            if (request.PageIndex == 1)
                resultTemp.GridItems.Insert(0, new BaseLookupItem() { Code = "-1", Id = "-1", Title = "همه" });
            if (request.SelectedItems.Contains("-1"))
                resultTemp.SelectedItems.Add(resultTemp.GridItems.Where(i => i.Id == "-1").Any() ? resultTemp.GridItems.Where(i => i.Id == "-1").First() : new BaseLookupItem() { Code = "-1", Id = "-1", Title = "همه" });
            result.SelectedItems = resultTemp.SelectedItems;
            result.GridItems = resultTemp.GridItems;
            result.TotalCount = request.PageIndex == 1 ? resultTemp.TotalCount + 1 : resultTemp.TotalCount;
            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }
            return new ApiResult<EstateInquiryStatusLookupViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
