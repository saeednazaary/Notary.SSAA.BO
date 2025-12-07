using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentElectronicBook;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentElectronic;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.DocumentElectronic
{
    public class DocumentElectronicBookCountQueryHandler : BaseQueryHandler<DocumentElectronicBookCountQuery, ApiResult<DocumentElectronicBookCountViewModel>>
    {
        private readonly IDocumentRepository _DocumentRepository;
        public DocumentElectronicBookCountQueryHandler(IMediator mediator, IUserService userService, IDocumentRepository DocumentRepository)
            : base(mediator, userService)
        {
            _DocumentRepository = DocumentRepository ?? throw new ArgumentNullException(nameof(DocumentRepository));
        }
        protected override bool HasAccess(DocumentElectronicBookCountQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentElectronicBookCountViewModel>> RunAsync(DocumentElectronicBookCountQuery request, CancellationToken cancellationToken)
        {
            DocumentElectronicBookCountViewModel result = new();
            ApiResult<DocumentElectronicBookCountViewModel> apiResult = new();
            int SignElectronicBookCount = 0;
            SignElectronicBookCount = await _DocumentRepository.GetDocumentElectronicBookTotalCount(_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (SignElectronicBookCount > 0)
            {
                result.DocumentTotalCount = SignElectronicBookCount;
                apiResult.IsSuccess = true;
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("سندی پیدا نشد");
            }
            return apiResult;
        }


    }
}
