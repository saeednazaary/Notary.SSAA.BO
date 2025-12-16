using Mapster;
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
    internal class ElzamArtSixCountyLookupQueryHandler : BaseQueryHandler<ElzamArtSixCountyLookupQuery, ApiResult<ElzamArtSixCountyLookupRepositoryObject>>
    {
        private readonly IElzamArtSixCountyRepository _ElzamArtSixCountyRepository;

        public ElzamArtSixCountyLookupQueryHandler(IMediator mediator, IUserService userService, IElzamArtSixCountyRepository ElzamArtSixCountyRepository) : base(mediator, userService)
        {
            _ElzamArtSixCountyRepository = ElzamArtSixCountyRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixCountyRepository));
        }

        protected override bool HasAccess(ElzamArtSixCountyLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ElzamArtSixCountyLookupRepositoryObject>> RunAsync(ElzamArtSixCountyLookupQuery request, CancellationToken cancellationToken)
        {
            ElzamArtSixCountyLookupRepositoryObject result = new();
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
            result = await _ElzamArtSixCountyRepository.GetElzamArtSixCountyLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

                     //foreach (var item in result.GridItems)
            //{
            //    if (request.SelectedItems.Contains(item.Id))
            //        item.IsSelected = true;
            //}

            return new ApiResult<ElzamArtSixCountyLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
