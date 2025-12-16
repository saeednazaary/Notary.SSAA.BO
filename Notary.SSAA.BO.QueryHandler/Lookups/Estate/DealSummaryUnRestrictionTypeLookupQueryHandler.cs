using Mapster;
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
    public class DealSummaryUnRestrictionTypeLookupQueryHandler : BaseQueryHandler<DealSummaryUnRestrictionTypeLookupQuery, ApiResult<DealSummaryUnRestrictionTypeLookupViewModel>>
    {
        private readonly IDealSummaryUnRestrictionTypeRepository _dealSummaryUnRestrictionTypeRepository;


        public DealSummaryUnRestrictionTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryUnRestrictionTypeRepository dealSummaryUnRestrictionTypeRepository)
            : base(mediator, userService)
        {
            _dealSummaryUnRestrictionTypeRepository = dealSummaryUnRestrictionTypeRepository ?? throw new ArgumentNullException(nameof(dealSummaryUnRestrictionTypeRepository));
        }

        protected override bool HasAccess(DealSummaryUnRestrictionTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DealSummaryUnRestrictionTypeLookupViewModel>> RunAsync(DealSummaryUnRestrictionTypeLookupQuery request, CancellationToken cancellationToken)
        {
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

            var result = await _dealSummaryUnRestrictionTypeRepository.GetDealSummaryUnRestrictionTypeItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, isOrderBy, cancellationToken);
            var model = result.Adapt<DealSummaryUnRestrictionTypeLookupViewModel>();
            return new ApiResult<DealSummaryUnRestrictionTypeLookupViewModel>(true, ApiResultStatusCode.Success, model, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }
}
