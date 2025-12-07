using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class GetDetailsOfDocumentQueryHandler : BaseQueryHandler<GetDetailsOfDocumentQuery, ApiResult<DocumentViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private LoadDocumentCoordinator loadDocumentCoordinator;

        public GetDetailsOfDocumentQueryHandler ( IMediator mediator, IUserService userService,
            LoadDocumentCoordinator _loadDocumentCoordinator
            )
            : base(mediator, userService)
        {
            loadDocumentCoordinator = _loadDocumentCoordinator;
        }
        protected override bool HasAccess( GetDetailsOfDocumentQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentViewModel>> RunAsync( GetDetailsOfDocumentQuery request, CancellationToken cancellationToken)
        {

            DocumentViewModel result = new();
            ApiResult<DocumentViewModel> apiResult = new();
            apiResult=   await loadDocumentCoordinator.LoadDocumentDetail ( request.DocumentId, request.DetailName, request.CaseType,cancellationToken );
            return apiResult;
        }

    }
}
