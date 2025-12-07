using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpExternalServiceCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint.Handlers
{
    public class SendTFACodeCommandHandler : BaseCommandHandler<SendTFACodeCommand, ApiResult>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IRepository<PersonFingerprint> _personFingerprintRepository;
        private readonly IDateTimeService _dateTimeService;
        private ApiResult<SendTFACodeViewModel> apiResult;
        private readonly IHttpExternalServiceCaller _httpExternalServiceCaller;
        private readonly IConfiguration _configuration;
        public SendTFACodeCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
            IRepository<PersonFingerprint> personFingerprintRepository, IDateTimeService dateTimeService, IConfiguration configuration,
            IHttpExternalServiceCaller httpExternalServiceCaller) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _httpExternalServiceCaller = httpExternalServiceCaller ?? throw new ArgumentNullException(nameof(httpExternalServiceCaller));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            apiResult = new ApiResult<SendTFACodeViewModel>();
        }

        protected override async Task<ApiResult> ExecuteAsync(SendTFACodeCommand request, CancellationToken cancellationToken)
        {
            SendTFACodeViewModel viewModel = new();
            masterEntity = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            if (masterEntity is not null)
            {
                if (masterEntity.TfaSendDate.IsNullOrWhiteSpace() && masterEntity.TfaSendTime.IsNullOrWhiteSpace())
                {
                    if (masterEntity.TfaIsRequired == "1" && masterEntity.TfaState == "1")
                    {
                        Random random = new();
                        viewModel.ExpireTime = "120";
                        masterEntity.TfaValue = random.Next(100000, 999999).ToString("D6");
                        masterEntity.TfaSendDate = _dateTimeService.CurrentPersianDate;
                        masterEntity.TfaSendTime = _dateTimeService.CurrentTime;
                        await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                        SendSmsFromKanoonServiceInput sendSMSServiceInput = new();
                        sendSMSServiceInput.Message = string.Format(@"گواهی امضا بنام {0} در {1} در حال تنظیم است. درصورت موافقت کد اعتبارسنجی را جهت ادامه روند اعلام فرمایید.
کد اعتبارسنجی: {2}", masterEntity.NameFamily, _userService.UserApplicationContext.BranchAccess.BranchName, masterEntity.TfaValue);
                        sendSMSServiceInput.MobileNo = masterEntity.TfaMobileNo;
                        sendSMSServiceInput.ClientId = ExternalServiceConstant.ClientId.SendOTPFingerprint;

                        var sendMessageResult = await _httpExternalServiceCaller.CallExternalServicePostApiAsync<ApiResult<SMSFromKanoonServiceViewModel>, SendSmsFromKanoonServiceInput>(new HttpExternalServiceRequest<SendSmsFromKanoonServiceInput>
                       (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/" + "SMSService", sendSMSServiceInput), cancellationToken);

                        apiResult.Data = viewModel;
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("اثر انگشت در وضعیت معتبر برای احراز دو مرحله ای نیست .");
                    }

                }
                else
                {
                    var lastSendDateTime = masterEntity.TfaSendDate + "-" + masterEntity.TfaSendTime;

                    if (lastSendDateTime.ToGregorianDateTime().AddSeconds(120) < _dateTimeService.CurrentDateTime)
                    {
                        Random random = new();
                        viewModel.ExpireTime = "120";
                        masterEntity.TfaValue = random.Next(100000, 999999).ToString("D6");
                        masterEntity.TfaSendDate = _dateTimeService.CurrentPersianDate;
                        masterEntity.TfaSendTime = _dateTimeService.CurrentTime;
                        await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                        SendSmsFromKanoonServiceInput sendSMSServiceInput = new();
                        sendSMSServiceInput.Message = string.Format(@"گواهی امضا بنام {0} در {1} در حال تنظیم است. درصورت موافقت کد اعتبارسنجی را جهت ادامه روند اعلام فرمایید.
کد اعتبارسنجی: {2}", masterEntity.NameFamily, _userService.UserApplicationContext.BranchAccess.BranchName, masterEntity.TfaValue);
                        sendSMSServiceInput.MobileNo = masterEntity.TfaMobileNo;
                        sendSMSServiceInput.ClientId = ExternalServiceConstant.ClientId.SendOTPFingerprint;

                        var sendMessageResult = await _httpExternalServiceCaller.CallExternalServicePostApiAsync<ApiResult<SMSFromKanoonServiceViewModel>, SendSmsFromKanoonServiceInput>(new HttpExternalServiceRequest<SendSmsFromKanoonServiceInput>
                       (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/" + "SMSService", sendSMSServiceInput), cancellationToken);

                        apiResult.Data = viewModel;
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("اعتبار کد یکبار مصرف تمام نشده است . لطفا بعد از مدتی تلاش کنید .");

                    }
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound; apiResult.message.Add("اثرانگشت مربوطه یافت نشد . ");

            }
            return apiResult;
        }

        protected override bool HasAccess(SendTFACodeCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
