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
    public class EstateSubSectionLookupQueryHandler : BaseQueryHandler<EstateSubSectionLookupQuery, ApiResult<EstateSubSectionLookupViewModel>>
    {
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;


        public EstateSubSectionLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstateSubSectionRepository estateSubSectionRepository)
            : base(mediator, userService)
        {
            _estateSubSectionRepository = estateSubSectionRepository ?? throw new ArgumentNullException(nameof(estateSubSectionRepository));
        }

        protected override bool HasAccess(EstateSubSectionLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateSubSectionLookupViewModel>> RunAsync(EstateSubSectionLookupQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new();
            List<string> FieldsNotInFilterSearch = new()
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

            var result = await _estateSubSectionRepository.GetEstateSubSectionItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams.SectionId, isOrderBy, cancellationToken);
            return new ApiResult<EstateSubSectionLookupViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }



    }
}
