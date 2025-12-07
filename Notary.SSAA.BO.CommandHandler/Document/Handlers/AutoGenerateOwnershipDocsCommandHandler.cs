namespace Notary.SSAA.BO.CommandHandler.Document.Handlers
{
    using MediatR;
    using Serilog;
    using Notary.SSAA.BO.CommandHandler.Base;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.Coordinator.NotaryDocument;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
    using System.Threading;

    public class AutoGenerateOwnershipDocsCommandHandler : BaseCommandHandler<AutoGenerateOwnershipDocsCommand,ApiResult>
    {
        private ApiResult<DocumentViewModel> _apiResult;
        private readonly PersonOwnershipDocsInheritorCore  _personOwnershipDocsInheritorCore;
        private readonly LoadDocumentCoordinator _loadDocumentCoordinator;
        public AutoGenerateOwnershipDocsCommandHandler ( IMediator mediator,
            IUserService userService,
            ILogger logger,
            PersonOwnershipDocsInheritorCore personOwnershipDocsInheritorCore,
            LoadDocumentCoordinator loadDocumentCoordinator )
            : base ( mediator, userService, logger )
        {
            _personOwnershipDocsInheritorCore = personOwnershipDocsInheritorCore;
            _loadDocumentCoordinator = loadDocumentCoordinator;
            _apiResult = new ();
        }

        protected override bool HasAccess (AutoGenerateOwnershipDocsCommand request, IList<string> userRoles )
        {
            return userRoles.Contains ( RoleConstants.Sardaftar ) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }

        protected override async Task<ApiResult> ExecuteAsync (AutoGenerateOwnershipDocsCommand request, CancellationToken cancellationToken )
        {
            bool isValid;
            List<string> messages=new List<string>(){};
            (bool isGenerated, string message) =   await _personOwnershipDocsInheritorCore.AutoGenerateRestrictedOwnershipDocs(request.ReqNo, request.RestRegisterServiceReqID , cancellationToken);
            messages.Add(message);
            if (isGenerated) {
                _apiResult = await _loadDocumentCoordinator.LoadDocumentDetail(request.RestRegisterServiceReqID, DocumentDatailName.DocumentCase.GetString(), CaseType.DocumentEstate, cancellationToken);
                _apiResult.message = new List<string> { "اطلاعات با موفقیت ذخیره شد" };
                _apiResult.IsSuccess = true;
                _apiResult.statusCode = ApiResultStatusCode.Success;
            }
            else
            {
                _apiResult.message = messages;
                _apiResult.IsSuccess = false;
                _apiResult.statusCode = ApiResultStatusCode.Success;
            }
            return _apiResult;
        }


    }
}
