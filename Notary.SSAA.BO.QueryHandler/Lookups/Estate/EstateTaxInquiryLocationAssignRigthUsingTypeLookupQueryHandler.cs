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
    public class EstateTaxInquiryLocationAssignRigthUsingTypeLookupQueryHandler : BaseQueryHandler<EstateTaxLocationAssignRigthUsingTypeLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IEstateTaxInquiryLocationAssignRigthUsingTypeRepository  _estateTaxInquiryLocationAssignRigthUsingTypeRepository;

        public EstateTaxInquiryLocationAssignRigthUsingTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryLocationAssignRigthUsingTypeRepository estateTaxInquiryLocationAssignRigthUsingTypeRepository)
            : base(mediator, userService)
        {
            _estateTaxInquiryLocationAssignRigthUsingTypeRepository = estateTaxInquiryLocationAssignRigthUsingTypeRepository;

        }

        protected override bool HasAccess(EstateTaxLocationAssignRigthUsingTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(EstateTaxLocationAssignRigthUsingTypeLookupQuery request, CancellationToken cancellationToken)
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


            var result = await _estateTaxInquiryLocationAssignRigthUsingTypeRepository.GetEstateTaxInquiryLocationAssignRigthUsingTypeItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                isOrderBy, cancellationToken);



            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
