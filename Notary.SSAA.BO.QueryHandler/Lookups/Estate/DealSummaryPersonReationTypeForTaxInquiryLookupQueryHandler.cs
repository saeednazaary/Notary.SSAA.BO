using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class DealSummaryPersonReationTypeForTaxInquiryLookupQueryHandler : BaseQueryHandler<DealSummaryPersonRelationTypeForTaxInquiryLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IDealSummaryPersonRelationTypeRepository _dealSummaryPersonRelationTypeRepository;

        public DealSummaryPersonReationTypeForTaxInquiryLookupQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryPersonRelationTypeRepository dealSummaryPersonRelationTypeRepository)
            : base(mediator, userService)
        {
            _dealSummaryPersonRelationTypeRepository = dealSummaryPersonRelationTypeRepository;
        }

        protected override bool HasAccess(DealSummaryPersonRelationTypeForTaxInquiryLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(DealSummaryPersonRelationTypeForTaxInquiryLookupQuery request, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

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
                gridSortInput.Sort = "title";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }
            result = await _dealSummaryPersonRelationTypeRepository.GetDealSummaryPersonRelationTypeForTaxInquiryGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                 isOrderBy, cancellationToken);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
