using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class EstateTaxInquiryStatusLookupQueryHandler : BaseQueryHandler<EstateTaxInquiryStatusLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IWorkfolwStateRepository _workfolwStateRepository;

        public EstateTaxInquiryStatusLookupQueryHandler(IMediator mediator, IUserService userService,
            IWorkfolwStateRepository Repository)
            : base(mediator, userService)
        {
            _workfolwStateRepository = Repository ?? throw new ArgumentNullException(nameof(Repository));
        }

        protected override bool HasAccess(EstateTaxInquiryStatusLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(EstateTaxInquiryStatusLookupQuery request, CancellationToken cancellationToken)
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
                gridSortInput.Sort = "State";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }
            result = await _workfolwStateRepository.GetEstateTaxInquiryStatusLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, isOrderBy, cancellationToken);
            
            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
