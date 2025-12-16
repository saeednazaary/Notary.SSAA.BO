using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal sealed class GetSignRequestDocumentTemplateByIdQueryHandler : BaseQueryHandler<GetSignRequestDocumentTemplateByIdQuery, ApiResult<SignRequestDocumentTemplateViewModel>>
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        public GetSignRequestDocumentTemplateByIdQueryHandler(IMediator mediator, IUserService userService, IDocumentTemplateRepository documentTemplateRepository) : base(mediator, userService)
        {
            _documentTemplateRepository = documentTemplateRepository;
        }

        protected override bool HasAccess(GetSignRequestDocumentTemplateByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignRequestDocumentTemplateViewModel>> RunAsync(GetSignRequestDocumentTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            SignRequestDocumentTemplateViewModel result = new();
            ApiResult<SignRequestDocumentTemplateViewModel> apiResult = new();

            Domain.Entities.DocumentTemplate documentTemplateEntity = await _documentTemplateRepository.GetAsync(x => x.Id == request.DocumentTemplateId.ToGuid(), cancellationToken);
            if (documentTemplateEntity is not null)
            {
                result = SignRequestMapper.ToViewModel(documentTemplateEntity);
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
