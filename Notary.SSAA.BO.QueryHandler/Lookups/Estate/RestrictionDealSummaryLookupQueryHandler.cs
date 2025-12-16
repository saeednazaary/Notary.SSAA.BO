using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class RestrictionDealSummaryLookupQueryHandler : BaseQueryHandler<RestrictionDealSummaryLookupQuery, ApiResult<RestrictionDealSummaryListViewModel>>
    {
        private readonly IDealSummaryRepository _dealSummaryRepository;

        public RestrictionDealSummaryLookupQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryRepository dealSummaryRepository)
            : base(mediator, userService)
        {
            _dealSummaryRepository = dealSummaryRepository ?? throw new ArgumentNullException(nameof(dealSummaryRepository));
        }

        protected override bool HasAccess(RestrictionDealSummaryLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<RestrictionDealSummaryListViewModel>> RunAsync(RestrictionDealSummaryLookupQuery request, CancellationToken cancellationToken)
        {
            RestrictionDealSummaryListViewModel result = new();
            if (request.GridSortInput.Count == 0)
            {
                request.GridSortInput.Add(new SortData() { Sort = "DealSummaryDate", SortType = "desc" });
            }
            result = await _dealSummaryRepository.GetRestrictedDealSummaryList(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, request.GridSortInput.FirstOrDefault(), request.SelectedItems, null, request.ExtraParams.ScriptoriumId, true, new string[] { EstateConstant.DealSummaryStates.Responsed, EstateConstant.DealSummaryStates.Archived }, request.ExtraParams.EstateInquiryId, request.ExtraParams.DealSummaryId, request.ExtraParams.DocumentClassifyNo, request.ExtraParams.DocumentSignDate, cancellationToken);
            
            Helper.NormalizeStringValuesDeeply(result, false);           
            return new ApiResult<RestrictionDealSummaryListViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
