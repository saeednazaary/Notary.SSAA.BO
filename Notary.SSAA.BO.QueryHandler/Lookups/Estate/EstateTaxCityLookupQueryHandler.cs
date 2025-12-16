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
    public class EstateTaxCityLookupQueryHandler : BaseQueryHandler<EstateTaxCityLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IEstateTaxCityRepository  _estateTaxCityRepository;

        public EstateTaxCityLookupQueryHandler(IMediator mediator, IUserService userService,
           IEstateTaxCityRepository estateTaxCityRepository)
            : base(mediator, userService)
        {
            _estateTaxCityRepository = estateTaxCityRepository;

        }

        protected override bool HasAccess(EstateTaxCityLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(EstateTaxCityLookupQuery request, CancellationToken cancellationToken)
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


            var result = await _estateTaxCityRepository.GetEstateTaxCityItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                isOrderBy,request.ExtraParams.CountyId, cancellationToken);



            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
