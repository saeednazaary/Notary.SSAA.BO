using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class CheckSignRequestFinalStateQueryHandler : BaseQueryHandler<CheckSignRequestFinalStateQuery, ApiResult>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly ISsrConfigRepository _ssrConfigRepository;


        public CheckSignRequestFinalStateQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository,
            IDateTimeService dateTimeService, IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
        }

        protected override bool HasAccess(CheckSignRequestFinalStateQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult> RunAsync(CheckSignRequestFinalStateQuery request, CancellationToken cancellationToken)
        {
            ApiResult<string> apiResult = new();
            apiResult.IsSuccess = false;

            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            Domain.Entities.SignRequest signRequest = await _signRequestRepository.GetSignRequest(signRequestId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
            scriptoriumInfo.ScriptoriumId = signRequest.ScriptoriumId;
            scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
            var baseInfoApiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
            if (!baseInfoApiResult.IsSuccess || baseInfoApiResult.Data is null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("ارتباط با اطلاعات پایه برقرار نشد.");
                return apiResult;
            }

            SsrConfigRepositoryInput configRepositoryInput = new SsrConfigRepositoryInput();
            configRepositoryInput.CurrentScriptoriumId = baseInfoApiResult.Data.ScriptoriumId;
            configRepositoryInput.CurrentGeoLocationId = baseInfoApiResult.Data.GeoLocationId;
            configRepositoryInput.UnitLevelCode = baseInfoApiResult.Data.UnitLevelCode;
            configRepositoryInput.CurrentDayOfWeek = baseInfoApiResult.Data.DayOfWeek;
            configRepositoryInput.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;

            var configs = await _ssrConfigRepository.GetBusinessConfig(configRepositoryInput, cancellationToken);
            if (!SignRequestBusinessRule.CheckSignRequestConfig(configs))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("خطا در پیکربندی سرویس های گواهی امضا. لفطا با راهبر سیستم تماس بگیرید");

            }
            if (!SignRequestBusinessRule.CheckWorkPermit(configs, baseInfoApiResult.Data.ScriptoriumId))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("دفترخانه مجوز ثبت گواهی امضا ندارد. لفطا با راهبر سیستم تماس بگیرید");
                return apiResult;
            }
            var getFingerprintPermitted = SignRequestBusinessRule.IsGetNationalNoOnWorkTime(_dateTimeService.CurrentDateTime, baseInfoApiResult.Data.DayOfWeek, configs);
            if (!getFingerprintPermitted)
            {
                apiResult.IsSuccess = false;

                apiResult.message.Add("ساعت کاری اخذ شناسه یکتا به پایان رسیده است.");
                return apiResult;
            }

            if (signRequest.State != "1")
            {
                apiResult.message.Add("گواهی امضا در وضعیت ایجاد شده نمیباشد .");
                return apiResult;
            }
            var attachmentInput = new LoadAttachmentServiceInput();
            if (signRequest.IsRemoteRequest == "1")
            {
                attachmentInput.ClientId = "9001";
                attachmentInput.RelatedRecordIds = new List<string> { signRequest.RemoteRequestId.ToString() };

                var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

                if (attachmentResult.IsSuccess)
                {
                    bool flag = false;
                    foreach (var item in attachmentResult.Data.AttachmentViewModels)
                    {
                        if (item.Medias.Count > 0 && item.DocTypeId == "0909")
                        {
                            flag = true;

                        }
                    }
                    if (!flag)
                    {
                        apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد");
                        return apiResult;
                    }
                }
            }
            else
            {
                attachmentInput.ClientId = "9001";
                attachmentInput.RelatedRecordIds = new List<string> { signRequest.Id.ToString() };

                var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

                if (attachmentResult.IsSuccess)
                {
                    bool flag = false;
                    foreach (var item in attachmentResult.Data.AttachmentViewModels)
                    {
                        if (item.Medias.Count > 0 && item.DocTypeId == "0909")
                        {
                            flag = true;

                        }
                    }
                    if (!flag)
                    {
                        apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد");
                        return apiResult;
                    }
                }
            }


            if (_userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Sardaftar)
            {
                apiResult.message.Add("سمت کاربر ، سر دفتر نمیباشد . ");
                return apiResult;
            }

            //Check if any Original person exists
            if (!signRequest.SignRequestPeople.Any(x => x.IsOriginal == "1"))
            {
                apiResult.message.Add("شخص اصیل در گواهی امضا وجود ندارد . ");
                return apiResult;
            }

            string message = SignRequestPersonBusinessRule.CheckSabteAhval(signRequest.SignRequestPeople, configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            var notCheckPersonList = new List<Guid>();
            foreach (var item in signRequest.SignRequestPersonRelateds)
            {
                switch (item.AgentTypeId)
                {
                    case "2":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "3":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "5":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "7":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "14":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "18":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "10":
                        if (item.ReliablePersonReasonId == "7")
                            notCheckPersonList.Add(item.MainPersonId);
                        break;
                    default:
                        break;
                }
            }
            notCheckPersonList = notCheckPersonList.Distinct().ToList();
            message = SignRequestPersonBusinessRule.CheckSana(signRequest.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckShahkar(signRequest.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckAmlakEskan(signRequest.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckMobileNo(signRequest.SignRequestPeople);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckRelatedPersonExists(signRequest.SignRequestPeople, signRequest.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckInheritorExists(signRequest.SignRequestPeople, signRequest.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckSaghir(signRequest.SignRequestPeople, signRequest.SignRequestPersonRelateds, _dateTimeService.CurrentPersianDate);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            if (!SignRequestRelatedPersonBusinessRule.CheckLoopInPersonRelated(signRequest.SignRequestPersonRelateds))
            {
                apiResult.message.Add("در گراف وابستگی اشخصاص حلقه به وجود آمده است .");
                return apiResult;
            }

            var fingerprintPersons = SignRequestPersonBusinessRule.FingerprintPersons(signRequest.SignRequestPeople);

            message = SignRequestPersonBusinessRule.CheckFingerprintPerson(signRequest.SignRequestPeople, signRequest.SignRequestPersonRelateds);

            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }

            if (signRequest.IsCostPaid == null || signRequest.IsCostPaid == "2")
            {
                apiResult.message.Add("وضعیت پرداخت گواهی امضا معتبر نیست . ");
                return apiResult;
            }

            if (apiResult.message.Count < 1)
            {
                apiResult.IsSuccess = true;
            }
            return apiResult;

        }
    }
}
