using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class EstateOwnershipTypeLookupQueryHandler : BaseQueryHandler<EstateOwnershipTypeLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IEstateOwnershipTypeRepository _estateOwnershipTypeRepository;

        public EstateOwnershipTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstateOwnershipTypeRepository estateOwnershipTypeRepository)
            : base(mediator, userService)
        {
            _estateOwnershipTypeRepository = estateOwnershipTypeRepository ?? throw new ArgumentNullException(nameof(estateOwnershipTypeRepository));
        }

        protected override bool HasAccess(EstateOwnershipTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(EstateOwnershipTypeLookupQuery request, CancellationToken cancellationToken)
        {


            bool isOrderBy = false;
            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower()
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
                gridSortInput.Sort = "Code";
                gridSortInput.SortType = "asc";
                isOrderBy = true;
            }

            var result = await _estateOwnershipTypeRepository.GetEstateOwnershipTypeItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                 isOrderBy, cancellationToken);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
