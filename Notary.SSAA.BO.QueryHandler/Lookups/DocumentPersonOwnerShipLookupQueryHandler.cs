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
    public class DocumentPersonOwnerShipLookupQueryHandler : BaseQueryHandler<DocumentPersonOwnerShipLookupQuery, ApiResult<DocumentPersonOwnerShipLookupRepositoryObject>>
    {
        private readonly IDocumentPersonRepository _documentPersonRepository;

        public DocumentPersonOwnerShipLookupQueryHandler(IMediator mediator, IUserService userService, IDocumentPersonRepository documentPersonRepositor) : base(mediator, userService)
        {
            _documentPersonRepository = documentPersonRepositor;
        }

        protected override bool HasAccess(DocumentPersonOwnerShipLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentPersonOwnerShipLookupRepositoryObject>> RunAsync(DocumentPersonOwnerShipLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentPersonOwnerShipLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "HasSmartCard".ToLower(),
                "IsOriginalPerson".ToLower(),
                "TfaState".ToLower(),
                "State".ToLower(),
                "IsRequired".ToLower(),
                "ProhibitionCheckingRequired".ToLower()
            };
            foreach (var item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            result = await _documentPersonRepository.GetDocumentPersonOwnerShipLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems,FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentPersonOwnerShipLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
