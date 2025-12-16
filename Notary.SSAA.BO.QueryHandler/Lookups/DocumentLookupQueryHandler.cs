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
    internal sealed class DocumentLookupQueryHandler : BaseQueryHandler<DocumentLookupQuery, ApiResult<DocumentLookupRepositoryObject>>
    {
        private readonly IDocumentRepository _documentTypeRepository;
        public DocumentLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository documentTypeRepository)
            : base(mediator, userService)
        {
            _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));


        }

        protected override bool HasAccess(DocumentLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentLookupRepositoryObject>> RunAsync(DocumentLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                 "DocumentPersons".ToLower(),
                "DocumentPersonList".ToLower(),
                "DocumentCases".ToLower(),
                "DocumentCaseList".ToLower(),
                "StateId".ToLower(),
                "DocumentTypeId".ToLower(),
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
                gridSortInput.Sort = "requestno";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }
            result = await _documentTypeRepository.GetDocumentLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, 
                request.SelectedItems, _userService.UserApplicationContext.BranchAccess.BranchCode, _userService.UserApplicationContext.BranchAccess.BranchCode, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
