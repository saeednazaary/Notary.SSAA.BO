using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
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
    internal sealed class DocumentCaseGridQueryHandler : BaseQueryHandler<DocumentCaseGridQuery, ApiResult<DocumentCaseGridViewModel>>
    {
        private readonly IDocumentCaseRepository _documentCaseRepository;

        private readonly List<string> fieldsInFilterSearch = new()
        {
                "DocumentId".ToLower(),
                "Title".ToLower(),
                "RequestDate".ToLower(),
                "Description".ToLower(),
                "scriptorumId".ToLower(),
        };

        private readonly List<string> sortType = new()
        {
                "asc".ToLower(),
                "desc".ToLower(),
        };

        private ApiResult<DocumentCaseGridViewModel> apiResult;

        public DocumentCaseGridQueryHandler(IMediator mediator, IUserService userService,
            IDocumentCaseRepository documentCaseRepository)
            : base(mediator, userService)
        {
            _documentCaseRepository = documentCaseRepository ?? throw new ArgumentNullException(nameof(documentCaseRepository));
            apiResult= new ApiResult<DocumentCaseGridViewModel>();
        }

        protected override bool HasAccess(DocumentCaseGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentCaseGridViewModel>> RunAsync(DocumentCaseGridQuery request, CancellationToken cancellationToken)
        {
            DocumentCaseGridViewModel result = new();
            BusinessValidation(request);
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
                    gridSortInput.Sort = "title";
                    gridSortInput.SortType = "desc";
                    isOrderBy = true;
                }

                var databseresult = await _documentCaseRepository.GetDocumentCaseGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                     request.SelectedItems.Select(Guid.Parse).ToList(), FieldsNotInFilterSearch,
                    _userService.UserApplicationContext.BranchAccess.BranchCode, request.ExtraParams, cancellationToken, isOrderBy);

                foreach (var item in databseresult.GridItems)
                {
                    if (request.SelectedItems.Contains(item.Id))
                        item.IsSelected = true;
                }

                apiResult.Data = DocumentSearchMapper.DocumentCaseGridToViewModel(databseresult);
            }

            return apiResult;
        }

        private void BusinessValidation(DocumentCaseGridQuery request)
        {
            if (request.GridFilterInput is not null && request.GridFilterInput.Any())
            {
                foreach (var item in request.GridFilterInput)
                {
                    if (!fieldsInFilterSearch.Contains(item.Filter.ToLower()))
                        apiResult.message.Add("فیلد مورد جستجو معتبر نیست ");
                }

                foreach (var item in request.GridSortInput)
                {
                    if (!fieldsInFilterSearch.Contains(item.Sort.ToLower()))
                        apiResult.message.Add("فیلد مورد مرتب سازی معتبر نیست ");

                    if (!sortType.Contains(item.SortType.ToLower()))
                        apiResult.message.Add("روش مرتب سازی معتبر نیست ");
                }

            }

            if (apiResult.message.Any())
                apiResult.IsSuccess = false;

        }

    }
}


