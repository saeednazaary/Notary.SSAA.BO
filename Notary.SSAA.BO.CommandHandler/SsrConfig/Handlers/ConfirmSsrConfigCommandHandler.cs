using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrConfig;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SsrConfig.Handlers
{
    public class ConfirmSsrConfigCommandHandler : BaseCommandHandler<ConfirmSsrConfigCommand, ApiResult>
    {
        private IRepository<Domain.Entities.SsrConfig> _ssrConfigRepository;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.SsrConfig masterEntity;
        private ApiResult<SsrConfigViewModel> apiResult;

        public ConfirmSsrConfigCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IRepository<Domain.Entities.SsrConfig> ssrConfigRepository,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _ssrConfigRepository = ssrConfigRepository;
        }

        protected override bool HasAccess(ConfirmSsrConfigCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.SSAAAdmin) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(ConfirmSsrConfigCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _ssrConfigRepository.GetByIdAsync(cancellationToken, request.ConfigId.ToGuid());

            BusinessValidation(cancellationToken);
            if (apiResult.IsSuccess)
            {
                masterEntity.SsrConfigMainSubjectId = request.SsrConfigMainSubjectId.First().ToGuid();
                masterEntity.SsrConfigSubjectId = request.SsrConfigSubjectId.First().ToGuid();
                masterEntity.ConfigStartDate = request.ConfigStartDate;
                masterEntity.ConfigEndDate = request.ConfigEndDate;
                masterEntity.ConfigStartTime = request.ConfigStartTime;
                masterEntity.ConfigEndTime = request.ConfigEndTime;
                masterEntity.ConditionType = request.ConditionType;
                masterEntity.IsConfirmed = "1";
                masterEntity.ConfirmDate = _dateTimeService.CurrentPersianDate;
                masterEntity.ConfirmTime = _dateTimeService.CurrentTime;
                masterEntity.Confirmer = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;
                await _ssrConfigRepository.UpdateAsync(masterEntity, cancellationToken);
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
        private void BusinessValidation(CancellationToken cancellationToken)
        {
            if (masterEntity is not null)
            {
                if (masterEntity.IsConfirmed == "1")
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.BadRequest;
                    apiResult.message.Add("پیکربندی قبلا تایید شده است .");
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.BadRequest;
                apiResult.message.Add("پیکربندی یافت نشد .");
            }
        }
    }
}