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

namespace Notary.SSAA.BO.CommandHandler.DocumentTemplate.Handlers
{
    internal sealed class CreateDocumentTemplateCommadHandler : BaseCommandHandler<CreateDocumentTemplateCommand, ApiResult>
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDateTimeService _dateTimeService;

        private Domain.Entities.DocumentTemplate masterEntity;
        private ApiResult<DocumentTemplateViewModel> apiResult;
        public CreateDocumentTemplateCommadHandler(IMediator mediator, IUserService userService, ILogger logger, IDateTimeService dateTimeService,
            IDocumentTemplateRepository documentTemplateRepository) : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _documentTemplateRepository = documentTemplateRepository;
            _dateTimeService = dateTimeService;

        }
        protected override bool HasAccess(CreateDocumentTemplateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateDocumentTemplateCommand request, CancellationToken cancellationToken)
        {

            var sameCodeExist = await _documentTemplateRepository.GetAsync(x => x.State != "0" && x.Code == request.DocumentTemplateCode && x.DocumentTypeId == request.DocumentTypeId.FirstOrDefault(), cancellationToken);
            if (sameCodeExist != null)
                apiResult.message.Add("کد و سند وارد شده نباید تکراری باشند!");

            else
            {
                UpdateDatabase(request);
                await _documentTemplateRepository.AddAsync(masterEntity, cancellationToken);

                ApiResult<DocumentTemplateViewModel> getResponse = await _mediator.Send(new GetDocumenttemplateByIdQuery() { DocumentTemplateId = masterEntity.Id.ToString() }, cancellationToken);

                if (getResponse.IsSuccess)
                {
                    apiResult.Data = getResponse.Data;
                    apiResult.IsSuccess = true;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                }
            }


            if (apiResult.message.Count > 0)
            {
                apiResult.IsSuccess = false;
                apiResult.Data = null;
                apiResult.statusCode = ApiResultStatusCode.Success;
            }


            return apiResult;
        }

        private void UpdateDatabase(CreateDocumentTemplateCommand request)
        {
            masterEntity = new()
            {
                Id = Guid.NewGuid(),
                ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family,
                CreateDate = _dateTimeService.CurrentPersianDate,
                RecordDate = _dateTimeService.CurrentDateTime,
                ModifyDateTime = _dateTimeService.CurrentPersianDateTime,
                CreateTime = _dateTimeService.CurrentTime,
            };
            DocumentTemplateMapper.ToEntity(request, ref masterEntity);
        }
    }
}
