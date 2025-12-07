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
    public class OtherPaymentsTypeLookupQueryHandler : BaseQueryHandler<OtherPaymentsTypeLookupQuery, ApiResult<OtherPaymentsTypeLookupQueryRepositoryObject>>
    {
        private readonly IOtherPaymentsTypeRepository _otherPaymentsTypeRepository;
        private ApiResult<OtherPaymentsTypeLookupQueryRepositoryObject> _apiResult;
        public OtherPaymentsTypeLookupQueryHandler(IMediator mediator, IUserService userService, IOtherPaymentsTypeRepository otherPaymentsTypeRepository) : base(mediator, userService)
        {
            _otherPaymentsTypeRepository = otherPaymentsTypeRepository ?? throw new ArgumentNullException(nameof(otherPaymentsTypeRepository));
            _apiResult = new ApiResult<OtherPaymentsTypeLookupQueryRepositoryObject>();
        }

        protected override bool HasAccess(OtherPaymentsTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<OtherPaymentsTypeLookupQueryRepositoryObject>> RunAsync(OtherPaymentsTypeLookupQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new();
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            if (request.GridSortInput.Any())
            {
                isOrderBy = true;
                gridSortInput.Sort = request.GridSortInput.Last().Sort;
                gridSortInput.SortType = request.GridSortInput.Last().SortType;
            }
            var databseresult = await _otherPaymentsTypeRepository.GetOtherPaymentsTypeLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, gridSortInput, request.GlobalSearch, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);
            foreach (var item in databseresult.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }
            _apiResult.Data = databseresult;
            return _apiResult;
        }
    }
}
