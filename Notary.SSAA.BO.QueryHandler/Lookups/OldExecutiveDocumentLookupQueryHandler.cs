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
    internal sealed class OldExecutiveOldExecutiveDocumentLookupQueryHandler : BaseQueryHandler<OldExecutiveDocumentLookupQuery, ApiResult<OldExecutiveDocumentLookupRepositoryObject>>
    {
        private readonly IDocumentRepository _OldExecutiveDocumentTypeRepository;
        public OldExecutiveOldExecutiveDocumentLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository OldExecutiveDocumentTypeRepository)
            : base(mediator, userService)
        {
            _OldExecutiveDocumentTypeRepository = OldExecutiveDocumentTypeRepository ?? throw new ArgumentNullException(nameof(OldExecutiveDocumentTypeRepository));


        }

        protected override bool HasAccess(OldExecutiveDocumentLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<OldExecutiveDocumentLookupRepositoryObject>> RunAsync(OldExecutiveDocumentLookupQuery request, CancellationToken cancellationToken)
        {
            OldExecutiveDocumentLookupRepositoryObject result = new();
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
            result = await _OldExecutiveDocumentTypeRepository.GetOldExecutiveDocumentLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, 
                request.SelectedItems, _userService.UserApplicationContext.BranchAccess.BranchCode, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<OldExecutiveDocumentLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
