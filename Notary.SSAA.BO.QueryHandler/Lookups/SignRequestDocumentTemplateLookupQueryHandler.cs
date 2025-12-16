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
    internal sealed class SignRequestDocumentTemplateLookupQueryHandler : BaseQueryHandler<SignRequestDocumentTemplateLookupQuery, ApiResult<SignRequestDocumentTemplateRepositoryObject>>
    {
        private readonly IDocumentTemplateRepository _documentTypeRepository;
        public SignRequestDocumentTemplateLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentTemplateRepository documentTypeRepository)
            : base(mediator, userService)
        {
            _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));


        }

        protected override bool HasAccess(SignRequestDocumentTemplateLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<SignRequestDocumentTemplateRepositoryObject>> RunAsync(SignRequestDocumentTemplateLookupQuery request, CancellationToken cancellationToken)
        {
            SignRequestDocumentTemplateRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
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
            result = await _documentTypeRepository.GetSignRequestDocumentTemplateLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, 
                request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<SignRequestDocumentTemplateRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
