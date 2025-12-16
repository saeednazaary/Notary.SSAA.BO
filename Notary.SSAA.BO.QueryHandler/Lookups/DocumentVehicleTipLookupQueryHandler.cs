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
    internal sealed class DocumentVehicleTipLookupQueryHandler : BaseQueryHandler<DocumentVehicleTipLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IDocumentVehicleTipRepository _documentVehicleTipRepository;
        public DocumentVehicleTipLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentVehicleTipRepository documentVehicleRepository)
            : base(mediator, userService)
        {
            _documentVehicleTipRepository = documentVehicleRepository ?? throw new ArgumentNullException(nameof(documentVehicleRepository));


        }

        protected override bool HasAccess(DocumentVehicleTipLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(DocumentVehicleTipLookupQuery request, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new ();
            List<string> FieldsNotInFilterSearch = new ()
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
            result = await _documentVehicleTipRepository.GetDocumentVehicleTipLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, 
                request.SelectedItems, FieldsNotInFilterSearch,request.ExtraParams, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
