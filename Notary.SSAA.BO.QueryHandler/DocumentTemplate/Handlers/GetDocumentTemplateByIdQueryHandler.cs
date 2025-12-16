using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentTemplate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.DocumentTemplate.Handlers
{
    internal sealed class GetDocumentTemplateByIdQueryHandler : BaseQueryHandler<GetDocumenttemplateByIdQuery, ApiResult<DocumentTemplateViewModel>>
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        public GetDocumentTemplateByIdQueryHandler(IMediator mediator, IUserService userService, IDocumentTemplateRepository documentTemplateRepository) : base(mediator, userService)
        {
            _documentTemplateRepository = documentTemplateRepository;
        }

        protected override bool HasAccess(GetDocumenttemplateByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentTemplateViewModel>> RunAsync(GetDocumenttemplateByIdQuery request, CancellationToken cancellationToken)
        {
            DocumentTemplateViewModel result = new();
            ApiResult<DocumentTemplateViewModel> apiResult = new();

            Domain.Entities.DocumentTemplate documentTemplateEntity = await _documentTemplateRepository.GetAsync(x => x.Id == request.DocumentTemplateId.ToGuid(), cancellationToken);
            if (documentTemplateEntity is not null)
            {
                result = DocumentTemplateMapper.ToViewModel(documentTemplateEntity);
                if (documentTemplateEntity.ScriptoriumId is not null) 
                    result.ScriptoriumTitle = _userService.UserApplicationContext.BranchAccess.BranchName;

                result.IsInEditMode = !string.IsNullOrEmpty(documentTemplateEntity.ScriptoriumId);

                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("کلیشه مربوطه پیدا نشد . ");
            }

            return apiResult;
        }
    }
}
