using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using static Stimulsoft.Report.StiRecentConnections;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    internal sealed class DocumentTypeLookupQueryHandler : BaseQueryHandler<DocumentTypeLookupQuery, ApiResult<DocumentTypeRepositoryObject>>
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        public DocumentTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentTypeRepository documentTypeRepository)
            : base(mediator, userService)
        {
            _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));


        }

        protected override bool HasAccess(DocumentTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentTypeRepositoryObject>> RunAsync(DocumentTypeLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentTypeRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "HasSubject".ToLower(),
                "SubjectIsRequired".ToLower(),
                "WealthType".ToLower(),
                "HasEstateInquiry".ToLower(),
                "EstateInquiryIsRequired".ToLower(),
                "State".ToLower(),
                "IsSupportive".ToLower(),
                "IsSelected".ToLower(),
                "AssetTypeIsRequired".ToLower(),
                "HasAssetType".ToLower(),
                "DocumentTypeGroupTwoId".ToLower(),
                "DocumentTypeGroupOneId".ToLower(),
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
            result = await _documentTypeRepository.GetDocumentTypeLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput,
                request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);


            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentTypeRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
