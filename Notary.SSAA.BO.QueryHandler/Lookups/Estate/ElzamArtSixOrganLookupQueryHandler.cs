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
    internal class ElzamArtSixOrganLookupQueryHandler : BaseQueryHandler<ElzamArtSixOrganLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IElzamArtSixOrganRepository _elzamArtSixOrganRepository;
        public ElzamArtSixOrganLookupQueryHandler(IMediator mediator, IUserService userService,
            IElzamArtSixOrganRepository elzamArtSixOrganRepository) : base(mediator, userService)
        {
            _elzamArtSixOrganRepository = elzamArtSixOrganRepository ?? throw new ArgumentNullException(nameof(elzamArtSixOrganRepository));
        }

        protected override bool HasAccess(ElzamArtSixOrganLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(ElzamArtSixOrganLookupQuery request, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
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
            result = await _elzamArtSixOrganRepository.GetElzamArtSixOrganLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}