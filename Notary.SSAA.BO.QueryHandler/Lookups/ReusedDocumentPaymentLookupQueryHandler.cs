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
    internal class ReusedDocumentPaymentLookupQueryHandler : BaseQueryHandler<ReusedDocumentPaymentLookupQuery, ApiResult<ReusedDocumentPaymentLookupRepositoryObject>>
    {
        private readonly IReusedDocumentPaymentRepository _ReusedDocumentPaymentRepository;

        public ReusedDocumentPaymentLookupQueryHandler(IMediator mediator, IUserService userService, IReusedDocumentPaymentRepository ReusedDocumentPaymentRepository) : base(mediator, userService)
        {
            _ReusedDocumentPaymentRepository = ReusedDocumentPaymentRepository ?? throw new ArgumentNullException(nameof(ReusedDocumentPaymentRepository));
        }

        protected override bool HasAccess(ReusedDocumentPaymentLookupQuery request, IList<string> userRoles)
        {
            return  userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ReusedDocumentPaymentLookupRepositoryObject>> RunAsync(ReusedDocumentPaymentLookupQuery request, CancellationToken cancellationToken)
        {
            ReusedDocumentPaymentLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "Id".ToLower(),
                "DocumentId".ToLower(),
                "CostTypeId".ToLower(),
                "Price".ToLower(),
                "RecordDate".ToLower(),
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
                gridSortInput.Sort = "No";
                gridSortInput.SortType = "asc";
            }
            result = await _ReusedDocumentPaymentRepository.GetReusedDocumentPaymentLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

                     //foreach (var item in result.GridItems)
            //{
            //    if (request.SelectedItems.Contains(item.Id))
            //        item.IsSelected = true;
            //}

            return new ApiResult<ReusedDocumentPaymentLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
