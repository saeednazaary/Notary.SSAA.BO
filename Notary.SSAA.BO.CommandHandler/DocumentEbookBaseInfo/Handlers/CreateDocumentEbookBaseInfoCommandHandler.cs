using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentEbookBaseInfo;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.DocumentEbookBaseInfo.Handlers
{
    public class CreateDocumentEbookBaseInfoCommandHandler : BaseCommandHandler<CreateDocumentEbookBaseInfoCommand, ApiResult>
    {
        private IRepository<DocumentElectronicBookBaseinfo> _DocumentEbookBaseInfoRepository;
        private IDocumentRepository _Document;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.DocumentElectronicBookBaseinfo masterEntity;
        private ApiResult<DocumentEbookBaseInfoViewModel> apiResult;

        public CreateDocumentEbookBaseInfoCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IRepository<DocumentElectronicBookBaseinfo> DocumentEbookBaseInfoRepository, IDocumentRepository Document,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _DocumentEbookBaseInfoRepository = DocumentEbookBaseInfoRepository;
            _Document = Document;
        }

        protected override bool HasAccess(CreateDocumentEbookBaseInfoCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateDocumentEbookBaseInfoCommand request, CancellationToken cancellationToken)
        {
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                UpdateDatabase(request, cancellationToken);
                await _DocumentEbookBaseInfoRepository.AddAsync(masterEntity, cancellationToken);
                ApiResult<DocumentEbookBaseInfoViewModel> getResponse = await _mediator.Send(new GetDocumentEbookBaseInfoByIdQuery(), cancellationToken);
                if (getResponse.IsSuccess)
                {
                    apiResult = getResponse;
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getResponse.statusCode;
                    apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");
                    apiResult.message = getResponse.message;
                }
            }
            return apiResult;
        }
        private void UpdateDatabase(CreateDocumentEbookBaseInfoCommand request, CancellationToken cancellationToken)
        {
            masterEntity = new()
            {
                Id = Guid.NewGuid(),
                ExordiumConfirmDate = _dateTimeService.CurrentPersianDate,
                ExordiumConfirmTime = _dateTimeService.CurrentTime.Substring(0,5),
                ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                ExordiumDigitalSign = "-",
            };
            DocumentEbookBaseInfoMapper.ToEntity(ref masterEntity, request);
        }
        private async Task BusinessValidation(CreateDocumentEbookBaseInfoCommand request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                var DocumentEbookBaseInfo = await _mediator.Send(new GetDocumentEbookBaseInfoByIdQuery(), cancellationToken);
                if (DocumentEbookBaseInfo != null && DocumentEbookBaseInfo.Data != null && DocumentEbookBaseInfo.IsSuccess == true)
                {
                    apiResult.message.Add("برای این دفترخانه، اطلاعات پایه دفتر الكترونیك سند مربوطه  قبلا ساخته شده است ");
                }
                else
                {
                    var branchCode = _userService.UserApplicationContext.BranchAccess.BranchCode;
                    int MaxClassifyNo = await _Document.GetLastClassifyNoFromDocs(branchCode, cancellationToken);
                    if (request.LastClassifyNo.ToInt() != MaxClassifyNo)
                    {
                        apiResult.message.Add("مقدار شماره ترتیب آخرین سند  ثبت شده ارسالی با آخرین سند ثبت شده مطابقت دارد ");
                    }
                }
                
            }
            else
            {
                apiResult.message.Add("درخواست ارسالی نامعتبر می باشد.");
            }

            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }
    }
}