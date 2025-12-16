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
    public class EstatePieceTypeForEstateTaxInquiryLookupQueryHandler : BaseQueryHandler<EstatePeiceTypeForEstateTaxInquiryLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IEstatePieceTypeRepository _estatePieceTypeRepository;

        public EstatePieceTypeForEstateTaxInquiryLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstatePieceTypeRepository estatePieceTypeRepository)
            : base(mediator, userService)
        {
            _estatePieceTypeRepository = estatePieceTypeRepository;
        }

        protected override bool HasAccess(EstatePeiceTypeForEstateTaxInquiryLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(EstatePeiceTypeForEstateTaxInquiryLookupQuery request, CancellationToken cancellationToken)
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

            var result = await _estatePieceTypeRepository.GetEstatePieceTypeForEstateTaxInquiryItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
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
