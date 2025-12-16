using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public class DocumentOwnerShipLookupQueryHandler : BaseQueryHandler<DocumentOwnerShipLookupQuery, ApiResult<DocumentOwnerShipLookupRepositoryObject>>
    {
        private readonly IDocumentOwnerShipRepository _documentOwnerShipRepository;

        public DocumentOwnerShipLookupQueryHandler(IMediator mediator, IUserService userService, IDocumentOwnerShipRepository documentOwnerShipRepository) : base(mediator, userService)
        {
            _documentOwnerShipRepository = documentOwnerShipRepository;
        }

        protected override bool HasAccess(DocumentOwnerShipLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentOwnerShipLookupRepositoryObject>> RunAsync(DocumentOwnerShipLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentOwnerShipLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "OwnershipDocumentType".ToLower(),
                "OwnershipDocTitle".ToLower(),
                "HasSmartCard".ToLower(),
                "IsOriginalPerson".ToLower(),
                "TfaState".ToLower()
            };
            foreach (var item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            result = await _documentOwnerShipRepository.GetDocumentOwnerShipLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems,FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentOwnerShipLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
