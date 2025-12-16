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
    public sealed class DocumentPersonTypeLookupQueryHandler : BaseQueryHandler<DocumentPersonTypeLookupQuery, ApiResult<DocumentPersonTypeLookupRepositoryObject>>
    {
        private readonly IDocumentPersonTypeRepository _documentTypeRepository;
        public DocumentPersonTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentPersonTypeRepository documentTypeRepository)
            : base(mediator, userService)
        {
            _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));
        }

        protected override bool HasAccess(DocumentPersonTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentPersonTypeLookupRepositoryObject>> RunAsync(DocumentPersonTypeLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentPersonTypeLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "IsSanaRequired".ToLower(),
                "IsShahkarRequired".ToLower(),
                "IsRequired".ToLower(),
                "IsProhibitionCheckRequired".ToLower(),
                "IsOwner".ToLower()
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
                gridSortInput.Sort = "code";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }
            result = await _documentTypeRepository.GetDocumentPersonTypeLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentPersonTypeLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
