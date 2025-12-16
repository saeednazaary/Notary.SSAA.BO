using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal sealed class DocumentTemplateGridQueryHandler : BaseQueryHandler<DocumentTemplateGridQuery, ApiResult<DocumentTemplateGrid>>
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;

        public DocumentTemplateGridQueryHandler(IMediator mediator, IUserService userService,
            IDocumentTemplateRepository documentTemplateRepository)
            : base(mediator, userService)
        {
            _documentTemplateRepository = documentTemplateRepository ?? throw new ArgumentNullException(nameof(documentTemplateRepository));
        }

        protected override bool HasAccess(DocumentTemplateGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentTemplateGrid>> RunAsync(DocumentTemplateGridQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new ();
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "DocumentTemplateId".ToLower(),
                "DocumentStateId".ToLower(),
                "DocumentTypeId".ToLower(),
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
            //if (request.GridSortInput.Count == 0)
            //{
            //    gridSortInput.Sort = "DocumentTemplateCreateDate";
            //    gridSortInput.SortType = "desc";
            //    isOrderBy = true;
            //}
            var result = await _documentTemplateRepository.GetDocumentTemplateGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, 
                request.SelectedItems.Select(Guid.Parse).ToList(), request.ExtraParams,FieldsNotInFilterSearch,_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken, 
                isOrderBy);
            foreach (var item in result.GridItems)
            {
                item.DocumentTemplateStateTitle = item.DocumentTemplateStateId == "1" ? "فعال" : "غیر فعال";
                if (request.SelectedItems.Contains(item.DocumentTemplateId))
                    item.IsSelected = true;
            }
            return new ApiResult<DocumentTemplateGrid>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}


