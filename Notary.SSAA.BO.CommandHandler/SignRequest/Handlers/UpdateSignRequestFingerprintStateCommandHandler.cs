using Mapster;
using MediatR;
using Newtonsoft.Json;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class UpdateSignRequestFingerprintStateCommandHandler : BaseCommandHandler<UpdateSignRequestFingerprintStateCommand, ApiResult<SignRequestViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly ISsrConfigRepository _ssrConfigRepository;
        private readonly IDateTimeService _dateTimeService;
        private ApiResult<SignRequestViewModel> apiResult;

        public UpdateSignRequestFingerprintStateCommandHandler(IMediator mediator, IUserService userService, ILogger logger, ISignRequestRepository signRequestRepository,
            IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository, IDateTimeService dateTimeService)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        protected override async Task<ApiResult<SignRequestViewModel>> ExecuteAsync(UpdateSignRequestFingerprintStateCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }

            masterEntity = await _signRequestRepository.GetAsync(x => x.ScriptoriumId == _userService.UserApplicationContext.BranchAccess.BranchCode
            && x.Id == request.SignRequestId.ToGuid(), cancellationToken);
            await _signRequestRepository.LoadCollectionAsync(masterEntity, x => x.SignRequestPeople, cancellationToken);
            await _signRequestRepository.LoadCollectionAsync(masterEntity, x => x.SignRequestPersonRelateds, cancellationToken);

            if (masterEntity is not null)
            {
                var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                scriptoriumInfo.ScriptoriumId = masterEntity.ScriptoriumId;
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
                var getFingerprintPermitted = SignRequestBusinessRule.IsGetFingerprintOnWorkTime(_dateTimeService.CurrentDateTime, baseInfoApiResult.Data.DayOfWeek, configs);
                if (!getFingerprintPermitted)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("ساعت کاری اخذ اثرانگشت به پایان رسیده است.");
                    return apiResult;
                }
                if (!SignRequestRelatedPersonBusinessRule.CheckLoopInPersonRelated(masterEntity.SignRequestPersonRelateds))
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("در گراف وابستگی اشخصاص حلقه به وجود آمده است .");
                    return apiResult;
                }
                if (masterEntity.ScriptoriumId == "57999" || masterEntity.ScriptoriumId == "57998")
                {
                    Console.WriteLine(JsonConvert.SerializeObject(_userService.UserApplicationContext));

                    if (_userService.UserApplicationContext.UserRole.RoleId == RoleConstants.Sardaftar)
                    {
                        if (_userService.UserApplicationContext.BranchAccess.IsBranchOwner == "false" &&
                            _userService.UserApplicationContext.BranchAccess.IssueDocAccess != "true")
                        {
                            apiResult.message.Add("با توجه به حکم ابلاغ صادر شده، شما مجاز به تنظیم اسناد نمی باشید.");
                            return apiResult;
                        }
                    }
                }
                string message = SignRequestPersonBusinessRule.CheckSabteAhval(masterEntity.SignRequestPeople, configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(message);
                    return apiResult;
                }
                var notCheckPersonList = new List<Guid>();
                foreach (var item in masterEntity.SignRequestPersonRelateds)
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
                message = SignRequestPersonBusinessRule.CheckSana(masterEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(message);
                    return apiResult;
                }
                message = SignRequestPersonBusinessRule.CheckShahkar(masterEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(message);
                    return apiResult;
                }
                message = SignRequestPersonBusinessRule.CheckAmlakEskan(masterEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(message);
                    return apiResult;
                }


                GetInquiryPersonFingerprintListQuery getInquiryPersonFingerprint = new(masterEntity.Id.ToString(), masterEntity.SignRequestPeople.Select(x => x.NationalNo).ToList());

                var inquiryPersonFingerprintResult = await _mediator.Send(getInquiryPersonFingerprint, cancellationToken);
                if (inquiryPersonFingerprintResult?.IsSuccess == true)
                {
                    foreach (SignRequestPerson item in masterEntity.SignRequestPeople)
                    {

                        var foundPerson = inquiryPersonFingerprintResult.Data.FirstOrDefault(x => x.PersonObjectId == item.Id.ToString());
                        if (foundPerson is not null)
                        {
                            item.TfaState = foundPerson.TFAState;
                            item.MocState = foundPerson.MOCState;
                            item.IsFingerprintGotten = foundPerson.IsFingerprintGotten.ToYesNo();
                        }

                    }
                }
                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
                ApiResult<SignRequestViewModel> getResponse = await _mediator.Send(new GetSignRequestByIdQuery(request.SignRequestId), cancellationToken);
                if (getResponse.IsSuccess)
                {

                    apiResult.Data = getResponse.Data.Adapt<SignRequestViewModel>();

                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getResponse.statusCode;
                    apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");


                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد.");
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }

            return apiResult;

        }

        protected override bool HasAccess(UpdateSignRequestFingerprintStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
