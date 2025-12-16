using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public class ExecutiveBindingSubjectTypeLookupQueryHandler : BaseQueryHandler<ExecutiveBindingSubjectTypeLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IExecutiveBindingSubjectTypeRepository _executiveBindingSubjectTypeRepository;
        public ExecutiveBindingSubjectTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IExecutiveBindingSubjectTypeRepository executiveBindingSubjectTypeRepository)
            : base(mediator, userService)
        {
            _executiveBindingSubjectTypeRepository = executiveBindingSubjectTypeRepository ?? throw new ArgumentNullException(nameof(executiveBindingSubjectTypeRepository));


        }

        protected override bool HasAccess(ExecutiveBindingSubjectTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(ExecutiveBindingSubjectTypeLookupQuery request, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
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
            result = await _executiveBindingSubjectTypeRepository.GetExecutiveBindingSubjectTypeLookupItems(request.PageIndex, request.PageSize, request.ExtraParams.ExtraParam, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
