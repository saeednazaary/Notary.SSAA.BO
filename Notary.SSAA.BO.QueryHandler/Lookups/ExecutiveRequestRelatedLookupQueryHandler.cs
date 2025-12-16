using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public class ExecutiveRequestRelatedLookupQueryHandler : BaseQueryHandler<ExecutiveRequestRelatedPersonLookupQuery, ApiResult<ExecutiveRequestRelatedPersonLookupRepositoryObject>>
    {
        private readonly IExecutiveRequestPersonRepository _executiveRequestPersonRepository;

        public ExecutiveRequestRelatedLookupQueryHandler(IMediator mediator, IUserService userService,
            IExecutiveRequestPersonRepository executiveRequestPersonRepository)
            : base(mediator, userService)
        {
            _executiveRequestPersonRepository = executiveRequestPersonRepository ?? throw new ArgumentNullException(nameof(executiveRequestPersonRepository));
        }

        protected override bool HasAccess(ExecutiveRequestRelatedPersonLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ExecutiveRequestRelatedPersonLookupRepositoryObject>> RunAsync(ExecutiveRequestRelatedPersonLookupQuery request, CancellationToken cancellationToken)
        {
            ExecutiveRequestRelatedPersonLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();

            List<string> FieldsNotInFilterSearch = new List<string>()
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
                gridSortInput.Sort = "Name";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }
            if (request.ExtraParams.IsRelated.Equals("false"))
            {
                var resultTemp = await _executiveRequestPersonRepository.GetExecutiveRequestAgentedLookupItems(request.PageIndex, request.PageSize, request.ExtraParams.ExtraParam, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken
                   , isOrderBy);

                result.GridItems = resultTemp.GridItems;
                result.SelectedItems = resultTemp.SelectedItems;
                result.TotalCount = resultTemp.GridItems.Count;
            }
            else
            {
                var resultTemp = await _executiveRequestPersonRepository.GetExecutiveRequestAgentLookupItems(request.PageIndex, request.PageSize, request.ExtraParams.ExtraParam, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken
                  , isOrderBy);

                result.GridItems = resultTemp.GridItems;
                result.SelectedItems = resultTemp.SelectedItems;
                result.TotalCount = resultTemp.GridItems.Count;
            }
            return new ApiResult<ExecutiveRequestRelatedPersonLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
