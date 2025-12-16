using MediatR;
using Notary.SSAA.BO.Coordinator.NotaryDocument;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.DocumentStandardContract
{
    public class GetDetailsOfDocumentStandardContractQueryHandler : BaseQueryHandler<GetDetailsOfDocumentStandardContractQuery, ApiResult<DocumentStandardContractViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private LoadDocumentStandardContractCoordinator loadDocumentCoordinator;

        public GetDetailsOfDocumentStandardContractQueryHandler(IMediator mediator, IUserService userService,
            LoadDocumentStandardContractCoordinator _loadDocumentCoordinator
            )
            : base(mediator, userService)
        {
            loadDocumentCoordinator = _loadDocumentCoordinator;
        }
        protected override bool HasAccess(GetDetailsOfDocumentStandardContractQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentStandardContractViewModel>> RunAsync(GetDetailsOfDocumentStandardContractQuery request, CancellationToken cancellationToken)
        {

            DocumentStandardContractViewModel result = new();
            ApiResult<DocumentStandardContractViewModel> apiResult = new();
            apiResult = await loadDocumentCoordinator.LoadDocumentDetail(request.DocumentId, request.DetailName, request.CaseType, cancellationToken);
            return apiResult;
        }

    }
}
