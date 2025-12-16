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
    internal class ElzamArtSixEstateUsingLookupQueryHandler : BaseQueryHandler<ElzamArtSixEstateUsingLookupQuery, ApiResult<ElzamArtSixEstateUsingLookupRepositoryObject>>
    {
        private readonly IElzamArtSixEstateUsingRepository _ElzamArtSixEstateUsingRepository;

        public ElzamArtSixEstateUsingLookupQueryHandler(IMediator mediator, IUserService userService, IElzamArtSixEstateUsingRepository ElzamArtSixEstateUsingRepository) : base(mediator, userService)
        {
            _ElzamArtSixEstateUsingRepository = ElzamArtSixEstateUsingRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixEstateUsingRepository));
        }

        protected override bool HasAccess(ElzamArtSixEstateUsingLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ElzamArtSixEstateUsingLookupRepositoryObject>> RunAsync(ElzamArtSixEstateUsingLookupQuery request, CancellationToken cancellationToken)
        {
            ElzamArtSixEstateUsingLookupRepositoryObject result = new();
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
            result = await _ElzamArtSixEstateUsingRepository.GetElzamArtSixEstateUsingLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            return new ApiResult<ElzamArtSixEstateUsingLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
