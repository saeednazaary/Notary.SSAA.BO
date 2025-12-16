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
    public sealed class DocumentReliablePersonReasonLookupQueryHandler : BaseQueryHandler<DocumentReliablePersonReasonLookupQuery, ApiResult<DocumentReliablePersonReasonLookupRepositoryObject>>
    {
        private readonly IDocumentReliablePersonReasonRepository _documentReliablePersonReasonRepository;
        public DocumentReliablePersonReasonLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentReliablePersonReasonRepository documentTypeRepository)
            : base(mediator, userService)
        {
            _documentReliablePersonReasonRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));


        }

        protected override bool HasAccess(DocumentReliablePersonReasonLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentReliablePersonReasonLookupRepositoryObject>> RunAsync(DocumentReliablePersonReasonLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentReliablePersonReasonLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            isOrderBy = true;
            if (request.GridSortInput.Count > 0)
            {
                foreach (var item in request.GridSortInput)
                {
                    gridSortInput.Sort = item.Sort;
                    gridSortInput.SortType = item.SortType;
                }
            }
            else
            {
                gridSortInput.Sort = "code";
                gridSortInput.SortType = "asc";
            }
            result = await _documentReliablePersonReasonRepository.GetDocumentReliablePersonReasonLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentReliablePersonReasonLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
