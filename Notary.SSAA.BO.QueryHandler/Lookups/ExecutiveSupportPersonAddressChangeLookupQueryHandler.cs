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
    public class ExecutiveSupportPersonAddressChangeLookupQueryHandler : BaseQueryHandler<ExecutiveSupportPersonAddressChangeLookupQuery, ApiResult<ExecutiveSupportPersonLookupRepositoryObject>>
    {
        private readonly IExecutiveSupportPersonRepository _executiveSupportPersonRepository;
        public ExecutiveSupportPersonAddressChangeLookupQueryHandler(IMediator mediator, IUserService userService,
            IExecutiveSupportPersonRepository executiveSupportPersonRepository)
            : base(mediator, userService)
        {
            _executiveSupportPersonRepository = executiveSupportPersonRepository ?? throw new ArgumentNullException(nameof(executiveSupportPersonRepository));
        }

        protected override bool HasAccess(ExecutiveSupportPersonAddressChangeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<ExecutiveSupportPersonLookupRepositoryObject>> RunAsync(ExecutiveSupportPersonAddressChangeLookupQuery request, CancellationToken cancellationToken)
        {
            ExecutiveSupportPersonLookupRepositoryObject result = new();
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
            result = await _executiveSupportPersonRepository.GetExecutiveSupportPersonAddressChangeLookupItems(request.PageIndex, request.PageSize, request.ExtraParams.ExtraParam1, request.ExtraParams.ExtraParam2, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<ExecutiveSupportPersonLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
