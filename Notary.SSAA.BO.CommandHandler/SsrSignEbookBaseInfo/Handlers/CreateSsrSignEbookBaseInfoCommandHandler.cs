using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Mappers.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SsrSignEbookBaseInfo.Handlers
{
    public class CreateSsrSignEbookBaseInfoCommandHandler : BaseCommandHandler<CreateSsrSignEbookBaseInfoCommand, ApiResult>
    {
        private IRepository<SsrSignEbookBaseinfo> _ssrSignEbookBaseinfoRepository;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.SsrSignEbookBaseinfo masterEntity;
        private ApiResult<SsrSignEbookBaseInfoViewModel> apiResult;

        public CreateSsrSignEbookBaseInfoCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IRepository<SsrSignEbookBaseinfo> ssrSignEbookBaseinfoRepository,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _ssrSignEbookBaseinfoRepository = ssrSignEbookBaseinfoRepository;
        }

        protected override bool HasAccess(CreateSsrSignEbookBaseInfoCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateSsrSignEbookBaseInfoCommand request, CancellationToken cancellationToken)
        {
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                UpdateDatabase(request, cancellationToken);
                await _ssrSignEbookBaseinfoRepository.AddAsync(masterEntity, cancellationToken);
                ApiResult<SsrSignEbookBaseInfoViewModel> getResponse = await _mediator.Send(new GetSsrSignEbookBaseInfoByIdQuery(), cancellationToken);
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
        private void UpdateDatabase(CreateSsrSignEbookBaseInfoCommand request, CancellationToken cancellationToken)
        {
            masterEntity = new()
            {
                Id = Guid.NewGuid(),
                ExordiumConfirmDate = _dateTimeService.CurrentPersianDate,
                ExordiumConfirmTime = _dateTimeService.CurrentTime.Substring(0,5),
                ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                ExordiumDigitalSign = "-",
            };
            SsrSignEbookBaseinfoMapper.ToEntity(ref masterEntity, request);
        }
        private async Task BusinessValidation(CreateSsrSignEbookBaseInfoCommand request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                var ssrSignEbook = await _mediator.Send(new GetSsrSignEbookBaseInfoByIdQuery(), cancellationToken);
                if (ssrSignEbook != null && ssrSignEbook.Data != null)
                {
                    apiResult.message.Add("برای این دفترخانه، اطلاعات پایه دفتر الكترونیك گواهی امضاء مربوطه  قبلا ساخته شده است ");
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