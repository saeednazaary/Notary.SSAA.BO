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
    internal sealed class DocumentGridQueryHandler : BaseQueryHandler<DocumentGridQuery, ApiResult<DocumentGridViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;

        private readonly List<string> fieldsInFilterSearch = new()
        {
                "RequestNo".ToLower(),
                "ClassifyNo".ToLower(),
                "RequestDate".ToLower(),
                "DocumentTypeTitle".ToLower(),
                "DocumentPersons".ToLower(),
                "DocumentCaseList".ToLower(),
                "StateId".ToLower(),
                "DocumentCases".ToLower(),
        };

        private readonly List<string> sortType = new()
        {
                "asc".ToLower(),
                "desc".ToLower(),
        };

        private ApiResult<DocumentGridViewModel> apiResult;

        public DocumentGridQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository documentRepository)
            : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            apiResult= new ApiResult<DocumentGridViewModel>();
        }

        protected override bool HasAccess(DocumentGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentGridViewModel>> RunAsync(DocumentGridQuery request, CancellationToken cancellationToken)
        {
            DocumentGridViewModel result = new();
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
                "DocumentPersons".ToLower(),
                "DocumentPersonList".ToLower(),
                "DocumentCases".ToLower(),
                "DocumentCaseList".ToLower(),
                "StateId".ToLower(),
                "DocumentTypeId".ToLower(),
                "DocumentTypeGroupOneId".ToLower(),
                "DocumentTypeGroupTwoId".ToLower(),
                "RelatedDocumentTypeId".ToLower()
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
                    gridSortInput.Sort = "requestno";
                    gridSortInput.SortType = "desc";
                    isOrderBy = true;
                }

                var databseresult = await _documentRepository.GetDocumentGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                     request.SelectedItems.Select(Guid.Parse).ToList(), FieldsNotInFilterSearch,
                    _userService.UserApplicationContext.BranchAccess.BranchCode, request.Extraparams, cancellationToken, isOrderBy);

                foreach (var item in databseresult.GridItems)
                {
                    if (request.SelectedItems.Contains(item.Id))
                        item.IsSelected = true;
                }

                apiResult.Data = DocumentSearchMapper.ToViewModel(databseresult);
            }

            return apiResult;
        }

        private void BusinessValidation(DocumentGridQuery request)
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


