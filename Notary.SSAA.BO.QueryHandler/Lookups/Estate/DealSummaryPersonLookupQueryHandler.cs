using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class DealSummaryPersonLookupQueryHandler : BaseQueryHandler<DealSummaryPersonLookupQuery, ApiResult<DealSummaryPersonGrid>>
    {
        private readonly IDealSummaryPersonRepository  _dealSummaryPersonRepository;

        public DealSummaryPersonLookupQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryPersonRepository dealSummaryPersonRepository)
            : base(mediator, userService)
        {
            _dealSummaryPersonRepository = dealSummaryPersonRepository;
        }

        protected override bool HasAccess(DealSummaryPersonLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DealSummaryPersonGrid>> RunAsync(DealSummaryPersonLookupQuery request, CancellationToken cancellationToken)
        {
            DealSummaryPersonGrid result = new();

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
                gridSortInput.Sort = "personname";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }
            result = await _dealSummaryPersonRepository.GetDealSummaryPersonGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                request.ExtraParams.DealSummaryId.ToGuid(), isOrderBy, cancellationToken);
           
            return new ApiResult<DealSummaryPersonGrid>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
