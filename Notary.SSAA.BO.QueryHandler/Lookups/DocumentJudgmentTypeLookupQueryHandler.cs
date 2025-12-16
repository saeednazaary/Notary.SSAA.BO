using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public class DocumentJudgmentTypeLookupQueryHandler : BaseQueryHandler<DocumentJudgmentTypeLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IDocumentJudgmentTypeRepository _DocumentJudgmentTypeRepository;
        public DocumentJudgmentTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentJudgmentTypeRepository DocumentJudgmentTypeRepository)
            : base(mediator, userService)
        {
            _DocumentJudgmentTypeRepository = DocumentJudgmentTypeRepository ?? throw new ArgumentNullException(nameof(DocumentJudgmentTypeRepository));


        }

        protected override bool HasAccess(DocumentJudgmentTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(DocumentJudgmentTypeLookupQuery request, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
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
                gridSortInput.Sort = "code";
                gridSortInput.SortType = "asc";
            }
            result = await _DocumentJudgmentTypeRepository.GetDocumentJudgmentTypeLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
