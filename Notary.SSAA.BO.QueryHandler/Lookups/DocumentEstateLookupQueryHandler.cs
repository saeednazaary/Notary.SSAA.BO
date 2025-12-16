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
    public class DocumentEstateLookupQueryHandler : BaseQueryHandler<DocumentEstateLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IDocumentEstateRepository _documentEstateRepository;

        public DocumentEstateLookupQueryHandler(IMediator mediator, IUserService userService, IDocumentEstateRepository documentEstateRepository) : base(mediator, userService)
        {
            _documentEstateRepository = documentEstateRepository;
        }

        protected override bool HasAccess(DocumentEstateLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(DocumentEstateLookupQuery request, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            foreach (var item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            result = await _documentEstateRepository.GetDocumentEstateLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, request.ExtraParams, FieldsNotInFilterSearch, cancellationToken, isOrderBy = false);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
