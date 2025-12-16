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
    public sealed class SignRequestRelatedPersonLookupQueryHandler : BaseQueryHandler<SignRequestRelatedPersonLookupQuery, ApiResult<SignRequestAgentPersonLookupRepositoryObject>>
    {
        private readonly ISignRequestPersonRepository _signRequestAgentPersonRepository;
        public SignRequestRelatedPersonLookupQueryHandler(IMediator mediator, IUserService userService,
            ISignRequestPersonRepository signRequestAgentPersonRepository)
            : base(mediator, userService)
        {
            _signRequestAgentPersonRepository = signRequestAgentPersonRepository ?? throw new ArgumentNullException(nameof(signRequestAgentPersonRepository));


        }

        protected override bool HasAccess(SignRequestRelatedPersonLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected async override Task<ApiResult<SignRequestAgentPersonLookupRepositoryObject>> RunAsync(SignRequestRelatedPersonLookupQuery request, CancellationToken cancellationToken)
        {
            SignRequestAgentPersonLookupRepositoryObject result = new();
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
            result = await _signRequestAgentPersonRepository.GetSignRequestRelatedPersonLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, _userService.UserApplicationContext.BranchAccess.BranchCode,cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<SignRequestAgentPersonLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
