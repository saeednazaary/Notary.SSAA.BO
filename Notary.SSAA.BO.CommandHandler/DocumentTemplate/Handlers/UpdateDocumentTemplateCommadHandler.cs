using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentTemplate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.DocumentTemplate.Handlers
{
    internal sealed class UpdateDocumentTemplateCommadHandler : BaseCommandHandler<UpdateDocumentTemplateCommand, ApiResult>
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDateTimeService _dateTimeService;

        private Domain.Entities.DocumentTemplate masterEntity;
        private readonly ApiResult<DocumentTemplateViewModel> apiResult;
        public UpdateDocumentTemplateCommadHandler(IMediator mediator, IUserService userService, ILogger logger, IDateTimeService dateTimeService,
            IDocumentTemplateRepository documentTemplateRepository) : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _documentTemplateRepository = documentTemplateRepository;
            _dateTimeService = dateTimeService;

        }
        protected override bool HasAccess(UpdateDocumentTemplateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(UpdateDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _documentTemplateRepository.GetByIdAsync(cancellationToken, request.DocumentTemplateId.ToGuid());

            if (masterEntity is null)
            {
                apiResult.IsSuccess = false;
                apiResult.Data = null;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("رکورد مورد نظر یافت نشد ");
            }
            else
            {
                var sameCodeExist = await _documentTemplateRepository.GetAsync(x => x.State != "0" && x.Id != request.DocumentTemplateId.ToGuid() &&  x.Code == request.DocumentTemplateCode && x.DocumentTypeId == request.DocumentTypeId.FirstOrDefault(), cancellationToken);

                if (sameCodeExist is not null)
                {
                    apiResult.message.Add("کد و سند وارد شده نباید تکراری باشند!");
                    apiResult.IsSuccess = false;
                    apiResult.Data = null;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                }
                else
                {
                    UpdateDatabase(request);
                    await _documentTemplateRepository.UpdateAsync(masterEntity, cancellationToken);

                    ApiResult<DocumentTemplateViewModel> getResponse = await _mediator.Send(new GetDocumenttemplateByIdQuery() { DocumentTemplateId = masterEntity.Id.ToString() }, cancellationToken);

                    if (getResponse.IsSuccess)
                        apiResult.Data = getResponse.Data;

                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.Data = null;
                        apiResult.statusCode = ApiResultStatusCode.Success;
                        apiResult.message.Add("خطا در بازیابی اطلاعات ");
                    }
                }

            }

            return apiResult;
        }

        private void UpdateDatabase(UpdateDocumentTemplateCommand request)
        {
            if (request.ScriptoriumId == null) 
                masterEntity.State = request.DocumentTemplateStateId;
            else
                DocumentTemplateMapper.ToEntity(request, ref masterEntity);
            
            masterEntity.ModifyDateTime = _dateTimeService.CurrentPersianDateTime;
            masterEntity.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;

        }
    }
}
