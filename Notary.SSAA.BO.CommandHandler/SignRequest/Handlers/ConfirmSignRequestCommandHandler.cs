using MediatR;
using Newtonsoft.Json;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Queries.EDM;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class ConfirmSignRequestCommandHandler : BaseCommandHandler<ConfirmSignRequestCommand, ApiResult<SignRequestViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISignRequestSemaphoreRepository _signRequestSemaphoreRepository;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly IDateTimeService _dateTimeService;
        private ApiResult<SignRequestViewModel> apiResult;

        public ConfirmSignRequestCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
            ISignRequestRepository signRequestRepository, ISignRequestSemaphoreRepository signRequestSemaphoreRepository, IApplicationIdGeneratorService applicationIdGeneratorService, IDateTimeService dateTimeService) : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _signRequestSemaphoreRepository = signRequestSemaphoreRepository ?? throw new ArgumentNullException(nameof(signRequestSemaphoreRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        protected override async Task<ApiResult<SignRequestViewModel>> ExecuteAsync(ConfirmSignRequestCommand request, CancellationToken cancellationToken)
        {
            apiResult.Data = new SignRequestViewModel();
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
            }
            Domain.Entities.SignRequestSemaphore semaphore = await _signRequestSemaphoreRepository.GetAsync(x => x.SignRequestId == signRequestId, cancellationToken);
            if (semaphore != null)
            {
                Domain.Entities.SignRequest restoredSignRequest = JsonConvert.DeserializeObject<Domain.Entities.SignRequest>(semaphore.NewSignRequestData);
                List<Domain.Entities.SignElectronicBook> restoredElectronicBook = JsonConvert.DeserializeObject<List<Domain.Entities.SignElectronicBook>>(semaphore.SignElectronicBookData);
                var signValueId = request.SignValueIdList.Where(x => x.MainObjectId == restoredSignRequest.Id.ToString()).First();
                var getSignValueInput = new GetSignValueQuery() { Id = signValueId.Id };
                var getSignValueOutput = await _mediator.Send(getSignValueInput, cancellationToken);

                restoredSignRequest.DigitalSign = getSignValueOutput.Data.SignValue;
                restoredSignRequest.SignCertificateDn = getSignValueOutput.Data.Certificate;

                foreach (Domain.Entities.SignElectronicBook entity in restoredElectronicBook)
                {
                    signValueId = request.SignValueIdList.Where(x => x.MainObjectId == entity.Id.ToString()).First();
                    getSignValueInput = new GetSignValueQuery() { Id = signValueId.Id };
                    getSignValueOutput = await _mediator.Send(getSignValueInput, cancellationToken);

                    entity.SignCertificateDn = getSignValueOutput.Data.Certificate;
                    entity.DigitalSign = getSignValueOutput.Data.SignValue;
                }

                masterEntity = await _signRequestRepository.ConfirmSignRequest(restoredSignRequest, restoredElectronicBook, cancellationToken);
               await _signRequestRepository.LoadReferenceAsync(masterEntity, x => x.SignRequestSubject,cancellationToken);

                var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                scriptoriumInfo.ScriptoriumId = masterEntity.ScriptoriumId;
                scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
                var baseInfoApiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
                if (baseInfoApiResult.IsSuccess && baseInfoApiResult.Data!=null)
                {

                    foreach (var item in masterEntity.SignRequestPeople)
                    {
                        SendSmsFromKanoonServiceInput sendSMSServiceInput = new();
                        string SMSText = "گواهی امضا \" " + masterEntity.SignRequestSubject.Title + " \" با شناسه یکتا " + masterEntity.NationalNo
                            + " و شماره ترتیب " + item.SignClassifyNo + " و رمز تصدیق " + masterEntity.SecretCode + " در تاریخ " + masterEntity.SignDate + " در دفترخانه "
                            + baseInfoApiResult.Data.ScriptoriumCode + " " + baseInfoApiResult.Data.GeoLocationName + " برای شما صادر شد. برای تصدیق اصالت این گواهی امضا می توانید به  www.ssaa.ir مراجعه کنید" + Environment.NewLine
                            + " تلفن: " + baseInfoApiResult.Data.Tel;

                        sendSMSServiceInput.Message = SMSText;
                        sendSMSServiceInput.MobileNo = item.MobileNo;
                        sendSMSServiceInput.ClientId = "SSAR";
                        var smsRes = await _mediator.Send(sendSMSServiceInput, cancellationToken);

                    }
                }

                //SignRequestReportEdmQuery signRequestEDMService = new(masterEntity.Id.ToString());
                //var edmResult = await _mediator.Send(signRequestEDMService, cancellationToken);
                //apiResult.message.AddRange(edmResult.message);

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("رکورد یافت نشد .");
            }

            return apiResult;

        }
        protected override bool HasAccess(ConfirmSignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
