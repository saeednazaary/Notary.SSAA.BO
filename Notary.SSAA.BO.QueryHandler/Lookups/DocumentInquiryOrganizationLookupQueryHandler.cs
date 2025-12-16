using MediatR;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public sealed class DocumentInquiryOrganizationLookupQueryHandler : BaseQueryHandler<DocumentInquiryOrganizationLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IDocumentInquiryOrganizationRepository _DocumentInquiryOrganizationRepository;
        public DocumentInquiryOrganizationLookupQueryHandler ( IMediator mediator, IUserService userService,
            IDocumentInquiryOrganizationRepository DocumentInquiryOrganizationRepository )
            : base ( mediator, userService )
        {
            _DocumentInquiryOrganizationRepository = DocumentInquiryOrganizationRepository ?? throw new ArgumentNullException ( nameof ( DocumentInquiryOrganizationRepository ) );
        }

        protected override bool HasAccess ( DocumentInquiryOrganizationLookupQuery request, IList<string> userRoles )
        {
            return userRoles.Contains ( RoleConstants.Sardaftar ) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync ( DocumentInquiryOrganizationLookupQuery request, CancellationToken cancellationToken )
        {
            BaseLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            if ( request.GridSortInput.Count > 0 )
            {
                isOrderBy = true;
            }
            foreach ( var item in request.GridSortInput )
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            result = await _DocumentInquiryOrganizationRepository.GetDocumentInquiryOrganizationLookupItems ( request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy );

            foreach ( var item in result.GridItems )
            {
                if ( request.SelectedItems.Contains ( item.Id ) )
                    item.IsSelected = true;
            }

            return new ApiResult<BaseLookupRepositoryObject> ( true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful } );
        }


    }
}
