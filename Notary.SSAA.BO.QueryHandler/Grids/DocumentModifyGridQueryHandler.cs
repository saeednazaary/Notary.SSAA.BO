using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentModifySearch;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal sealed class DocumentModifyGridQueryHandler : BaseQueryHandler<DocumentModifyGridQuery, ApiResult<DocumentModifyGridViewModel>>
    {
        private readonly ISsrDocModifyClassifyNoRepository _ssrDocModifyClassifyNoRepository;



        private readonly List<string> sortType = new()
        {
                "asc".ToLower(),
                "desc".ToLower(),
        };

        private ApiResult<DocumentModifyGridViewModel> apiResult;

        public DocumentModifyGridQueryHandler(IMediator mediator, IUserService userService,
            ISsrDocModifyClassifyNoRepository ssrDocModifyClassifyNoRepository)
            : base(mediator, userService)
        {
            _ssrDocModifyClassifyNoRepository = ssrDocModifyClassifyNoRepository ?? throw new ArgumentNullException(nameof(ssrDocModifyClassifyNoRepository));
            apiResult = new ApiResult<DocumentModifyGridViewModel>();
        }

        protected override bool HasAccess(DocumentModifyGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentModifyGridViewModel>> RunAsync(DocumentModifyGridQuery request, CancellationToken cancellationToken)
        {
            DocumentModifyGridViewModel result = new();
            if (apiResult.IsSuccess)
            {


                bool isOrderBy = false;
                SortData gridSortInput = new SortData();
                // 
                //  فیلدهایی که لیست می باشند را در متغییر زیر نیز قرار دهید و به صورت دستی باید فیلتر بزنید
                //
                List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "DocumentId".ToLower(),
                "DocumentType".ToLower(),
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
                    gridSortInput.Sort = "ClassifyNoNew";
                    gridSortInput.SortType = "desc";
                    isOrderBy = true;
                }

                var databseresult = await _ssrDocModifyClassifyNoRepository.GetDocumentModifyGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                     request.SelectedItems.Select(Guid.Parse).ToList(), FieldsNotInFilterSearch,
                    _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken, isOrderBy);

                foreach (var item in databseresult.GridItems)
                {
                    if (request.SelectedItems.Contains(item.Id))
                        item.IsSelected = true;
                }

                apiResult.Data = DocumentModifySearchMapper.ToViewModel(databseresult);
            }

            return apiResult;
        }


    }
}


