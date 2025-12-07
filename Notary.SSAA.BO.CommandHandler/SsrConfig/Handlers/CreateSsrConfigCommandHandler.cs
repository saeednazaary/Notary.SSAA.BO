using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.Mappers.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.ViewModels;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SsrConfig.Handlers
{
    public class CreateSsrConfigCommandHandler : BaseCommandHandler<CreateSsrConfigCommand, ApiResult>
    {
        private IRepository<Domain.Entities.SsrConfig> _ssrConfigRepository;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.SsrConfig masterEntity;
        private ApiResult<SsrConfigViewModel> apiResult;

        public CreateSsrConfigCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IRepository<Domain.Entities.SsrConfig> ssrConfigRepository,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _ssrConfigRepository = ssrConfigRepository;
        }

        protected override bool HasAccess(CreateSsrConfigCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.SSAAAdmin) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateSsrConfigCommand request, CancellationToken cancellationToken)
        {
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                UpdateDatabase(request, cancellationToken);
                await _ssrConfigRepository.AddAsync(masterEntity, cancellationToken);
                ApiResult<SsrConfigViewModel> getResponse = await _mediator.Send(new GetSsrConfigByIdQuery(masterEntity.Id.ToString()), cancellationToken);
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
        private void UpdateDatabase(CreateSsrConfigCommand request, CancellationToken cancellationToken)
        {
            masterEntity = new()
            {
                Id = Guid.NewGuid(),
                ConfigStartDate=_dateTimeService.CurrentPersianDate,
                ConfigStartTime=_dateTimeService.CurrentTime,
                ConfigEndDate="9999/12/29",
                ConfigEndTime="12:00",
                IsConfirmed="2",
                ActionType="2"
                
            };
            SsrConfigMapper.ToEntity(ref masterEntity, request);
        }
        private async Task BusinessValidation(CreateSsrConfigCommand request, CancellationToken cancellationToken)
        {
            //if (request != null)
            //{
            //    var ssrSignEbook = await _mediator.Send(new GetSsrConfigByIdQuery(), cancellationToken);
            //    if (ssrSignEbook != null && ssrSignEbook.Data != null)
            //    {
            //        apiResult.message.Add("برای این دفترخانه، اطلاعات پایه دفتر الكترونیك گواهی امضاء مربوطه  قبلا ساخته شده است ");
            //    }
            //}
            //else
            //{
            //    apiResult.message.Add("درخواست ارسالی نامعتبر می باشد.");
            //}

            //if (apiResult.message.Count > 0)
            //{
            //    apiResult.statusCode = ApiResultStatusCode.Success;
            //    apiResult.IsSuccess = false;
            //}
        }
    }
}