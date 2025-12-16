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
    public class EstateSeriDaftarLookupQueryHandler : BaseQueryHandler<EstateSeridaftarLookupQuery, ApiResult<EstateSeriDaftarLookupViewModel>>
    {
        private readonly IEstateSeriDaftarRepository _estateSeriDaftarRepository;


        public EstateSeriDaftarLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstateSeriDaftarRepository estateSeriDaftarRepository)
            : base(mediator, userService)
        {
            _estateSeriDaftarRepository = estateSeriDaftarRepository ?? throw new ArgumentNullException(nameof(estateSeriDaftarRepository));
        }

        protected override bool HasAccess(EstateSeridaftarLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateSeriDaftarLookupViewModel>> RunAsync(EstateSeridaftarLookupQuery request, CancellationToken cancellationToken)
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

            var result = await _estateSeriDaftarRepository.GetEstateSeriDaftarItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams.UnitId, isOrderBy, cancellationToken);
            return new ApiResult<EstateSeriDaftarLookupViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }
}
