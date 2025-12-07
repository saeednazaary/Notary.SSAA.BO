
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

namespace Notary.SSAA.BO.CommandHandler.DocumentTemplate.Handlers;

internal sealed class DeleteDocumentTemplateCommadHandler : BaseCommandHandler<DeleteDocumentTemplateCommand, ApiResult>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IDocumentTemplateRepository _documentTemplateRepository;


    private Domain.Entities.DocumentTemplate masterEntity;
    private readonly ApiResult<DocumentTemplateViewModel> apiResult;

    public DeleteDocumentTemplateCommadHandler(IMediator mediator, IUserService userService, ILogger logger,
        IDateTimeService dateTimeService, IDocumentTemplateRepository documentTemplateRepository) : base(mediator, userService, logger)
    {
        masterEntity = new();
        apiResult = new();
        _dateTimeService = dateTimeService;
        _documentTemplateRepository = documentTemplateRepository;
    }

    protected override async Task<ApiResult> ExecuteAsync(DeleteDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        masterEntity = await _documentTemplateRepository.GetAsync(x => x.Id == request.DocumentTemplateId.ToGuid(), cancellationToken);

        if (masterEntity is null)
        {
            apiResult.IsSuccess = false;
            apiResult.Data = null;
            apiResult.statusCode = ApiResultStatusCode.NotFound;
            apiResult.message.Add("رکورد مورد نظر یافت نشد ");
        }
        else
        {
            if (masterEntity.ScriptoriumId is null)
                ReturnError("شما مجوز حذف این مورد را ندارید!");

            else
            {
                request.DocumentTemplateStateId = "0";// 0: isDeleted
                UpdateDatabase(request);
                await _documentTemplateRepository.UpdateAsync(masterEntity, cancellationToken);

                ApiResult<DocumentTemplateViewModel> getResponse = await _mediator.Send(new GetDocumenttemplateByIdQuery() { DocumentTemplateId = masterEntity.Id.ToString() }, cancellationToken);

                if (getResponse.IsSuccess)
                    apiResult.Data = getResponse.Data;

                else
                    ReturnError("خطا در بازیابی اطلاعات ");
            }

        }

        return apiResult;
    }


    protected override bool HasAccess(DeleteDocumentTemplateCommand request, IList<string> userRoles)
    {
        return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
    }


    private void ReturnError(string errorMessage)
    {
        apiResult.IsSuccess = false;
        apiResult.Data = null;
        apiResult.statusCode = ApiResultStatusCode.Success;
        apiResult.message.Add(errorMessage);
    }

    private void UpdateDatabase(DeleteDocumentTemplateCommand request)
    {
        DocumentTemplateMapper.ToEntity(request, ref masterEntity);
        masterEntity.ModifyDateTime = _dateTimeService.CurrentPersianDateTime;
        masterEntity.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;

    }

}
