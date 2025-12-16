using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    internal class ElzamArtSixEstateTypeLookupQueryHandler : BaseQueryHandler<ElzamArtSixEstateTypeLookupQuery, ApiResult<ElzamArtSixEstateTypeLookupRepositoryObject>>
    {
        private readonly IElzamArtSixEstateTypeRepository _ElzamArtSixEstateTypeRepository;

        public ElzamArtSixEstateTypeLookupQueryHandler(IMediator mediator, IUserService userService, IElzamArtSixEstateTypeRepository ElzamArtSixEstateTypeRepository) : base(mediator, userService)
        {
            _ElzamArtSixEstateTypeRepository = ElzamArtSixEstateTypeRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixEstateTypeRepository));
        }

        protected override bool HasAccess(ElzamArtSixEstateTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ElzamArtSixEstateTypeLookupRepositoryObject>> RunAsync(ElzamArtSixEstateTypeLookupQuery request, CancellationToken cancellationToken)
        {
            ElzamArtSixEstateTypeLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "Id".ToLower(),
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
                gridSortInput.Sort = "Title";
                gridSortInput.SortType = "asc";
            }
            result = await _ElzamArtSixEstateTypeRepository.GetElzamArtSixEstateTypeLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            return new ApiResult<ElzamArtSixEstateTypeLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
