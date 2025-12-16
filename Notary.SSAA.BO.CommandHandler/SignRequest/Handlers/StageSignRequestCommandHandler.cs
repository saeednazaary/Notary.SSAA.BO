using Mapster;
using MediatR;
using Newtonsoft.Json;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Other;
using Serilog;
using System.Linq;

namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class StageSignRequestCommandHandler : BaseCommandHandler<StageSignRequestCommand, ApiResult<StageSignRequestViewModel>>
    {
        private Domain.Entities.SignRequestSemaphore SemaphoreEntity;
        private Domain.Entities.SignRequest SignRequestEntity;
        private Domain.Entities.SignRequestFile SignRequestFileEntity;
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IRepository<Domain.Entities.SignRequestFile> _signRequestFileRepository;
        private readonly ISignElectronicBookRepository _signElectronicBookRepository;
        private readonly ISsrSignEbookBaseinfoRepository _ssrSignEbookBaseinfoRepository;
        private readonly ISignRequestSemaphoreRepository _signRequestSemaphoreRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly ISsrConfigRepository _ssrConfigRepository;
        private ApiResult<StageSignRequestViewModel> _apiResult;

        public StageSignRequestCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IRepository<Domain.Entities.SignRequestFile> signRequestFileRepository,
            ISignRequestRepository signRequestRepository, ISignElectronicBookRepository signElectronicBookRepository,
            ISignRequestSemaphoreRepository signRequestSemaphoreRepository, ISsrSignEbookBaseinfoRepository ssrSignEbookBaseinfoRepository, IDateTimeService dateTimeService,
            IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository) : base(mediator, userService, logger)
        {
            _apiResult = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _signElectronicBookRepository = signElectronicBookRepository ?? throw new ArgumentNullException(nameof(signElectronicBookRepository));
            _ssrSignEbookBaseinfoRepository = ssrSignEbookBaseinfoRepository ?? throw new ArgumentNullException(nameof(ssrSignEbookBaseinfoRepository));
            _signRequestSemaphoreRepository = signRequestSemaphoreRepository ?? throw new ArgumentNullException(nameof(signRequestSemaphoreRepository));
            _signRequestFileRepository = signRequestFileRepository ?? throw new ArgumentNullException(nameof(signRequestFileRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
        }

        protected override async Task<ApiResult<StageSignRequestViewModel>> ExecuteAsync(StageSignRequestCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                ApiResult<RollBackSignRequestViewModel> apiResult = new();
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return _apiResult;
            }

            SignRequestEntity = await _signRequestRepository.SignRequestTracking(signRequestId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);

            var validateResult = await ValidateBusiness(cancellationToken);
            if (validateResult.message.Count > 0)
            {
                _apiResult.message = validateResult.message;
                _apiResult.IsSuccess = false;
                return _apiResult;
            }
            var updateResult = await UpdateDatabase(cancellationToken);
            if (updateResult.message.Count > 0)
            {
                _apiResult.message = updateResult.message;
                _apiResult.IsSuccess = false;
                return _apiResult;

            }
            var generateFileResult = await GenerateSignRequestFile(cancellationToken);
            if (generateFileResult.message.Count > 0)
            {
                _apiResult.message = generateFileResult.message;
                _apiResult.IsSuccess = false;
                return _apiResult;
            }
            SemaphoreEntity = await _signRequestSemaphoreRepository.GetAsync(x => x.ScriptoriumId ==
            _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (SemaphoreEntity == null)
            {
                var semaphoreRes = await GenerateSemaphore(cancellationToken);
                if (semaphoreRes.message.Count > 0)
                {
                    _apiResult.IsSuccess = false;
                    _apiResult.message.AddRange(semaphoreRes.message);
                }
            }
            else
            {
                if (SemaphoreEntity.RecordDate.AddSeconds(70) < _dateTimeService.CurrentDateTime || SemaphoreEntity.SignRequestId == SignRequestEntity.Id)
                {
                    await _signRequestSemaphoreRepository.DeleteAsync(SemaphoreEntity, cancellationToken);
                    var semaphoreRes = await GenerateSemaphore(cancellationToken);
                    if (semaphoreRes.message.Count > 0)
                    {
                        _apiResult.IsSuccess = false;
                        _apiResult.message.AddRange(semaphoreRes.message);
                    }
                }
                else
                {
                    _apiResult.IsSuccess = false;
                    _apiResult.message.Add("شخصی در دفترخانه در حال ثبت گواهی امضا است ، لطفا تا پایان کار وی صبر کنید . ");
                }
            }
            return _apiResult;
        }

        protected override bool HasAccess(StageSignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        private async Task<string> CreateSignRequestNationalNo(string beginNationalNo, CancellationToken cancellationToken)
        {
            string beginReqNo = _dateTimeService.CurrentPersianDate[..4];
            beginReqNo += "021";
            beginReqNo += beginNationalNo;
            string reqNo = await _signRequestRepository.GetMaxNationalNo(beginReqNo, cancellationToken);
            if (string.IsNullOrWhiteSpace(reqNo))
            {
                reqNo = _dateTimeService.CurrentPersianDate[..4];
                reqNo += "021";
                reqNo += beginNationalNo;
                reqNo += "000001";
            }
            else
            {
                decimal numberReqNo = decimal.Parse(reqNo);
                numberReqNo++;
                reqNo = numberReqNo.ToString();
            }
            return reqNo;
        }
        private static string RandomNumberGenerator()
        {

            Random generator = new();
            return generator.Next(0, 1000000).ToString("D6");
        }
        private async Task<ApiResult> GenerateSemaphore(CancellationToken cancellationToken)
        {
            _signRequestRepository.Detach(SignRequestEntity);

            ApiResult apiResult = new();
            SemaphoreEntity = new();
            SemaphoreEntity.Id = Guid.NewGuid();
            SemaphoreEntity.State = "1";
            SemaphoreEntity.SignRequestId = SignRequestEntity.Id;
            SemaphoreEntity.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            SemaphoreEntity.RecordDate = _dateTimeService.CurrentDateTime;

            SemaphoreEntity.OriginalSignRequestData = JsonConvert.SerializeObject(SignRequestEntity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                });




            int? lastClassifyNo = await _signElectronicBookRepository.GetLastClassifyNo(SignRequestEntity.ScriptoriumId, cancellationToken);
            if (lastClassifyNo is null || lastClassifyNo == 0)
            {
                lastClassifyNo = await _ssrSignEbookBaseinfoRepository.GetLastClassifyNo(SignRequestEntity.ScriptoriumId, cancellationToken);
                if (lastClassifyNo is null || lastClassifyNo == 0)
                {
                    apiResult.message.Add("تأیید گواهی امضاء قابل انجام نیست. ابتدا باید در فرم بهره برداری از دفتر الکترونیک گواهی امضاء، آخرین شماره ترتیب گواهی امضاء، قبل از راه اندازی دفتر الکترونیک گواهی امضاء را تعیین نمایید.");
                    return apiResult;
                }
            }
            SemaphoreEntity.LastClassifyNo = lastClassifyNo.Value;


            SignRequestEntity.State = "2";
            SignRequestEntity.NationalNo = await CreateSignRequestNationalNo(SignRequestEntity.ScriptoriumId, cancellationToken);
            SignRequestEntity.SecretCode = RandomNumberGenerator();
            SignRequestEntity.ConfirmDate = _dateTimeService.CurrentPersianDate;
            SignRequestEntity.SignDate = _dateTimeService.CurrentPersianDate;
            SignRequestEntity.SignTime = _dateTimeService.CurrentTime;
            SignRequestEntity.ConfirmTime = _dateTimeService.CurrentTime;
            SignRequestEntity.Confirmer = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;

            SemaphoreEntity.LastChangeDate = _dateTimeService.CurrentPersianDate;
            SemaphoreEntity.LastChangeTime = _dateTimeService.CurrentTime;

            var signRequestSignViewModel = new SignRequestSignViewModel();

            signRequestSignViewModel = SignRequestEntity.Adapt<SignRequestSignViewModel>();

            List<Domain.Entities.SignElectronicBook> classes = new();
            List<SignElectronicBookSignViewModel> signElectronicBooksSign = new();
            var getPersonFingerprintImage = new GetPersonFingerprintImageQuery(SignRequestEntity.Id.ToString());


            var fingerprintRes = await _mediator.Send(getPersonFingerprintImage, cancellationToken);

            if (!fingerprintRes.IsSuccess || fingerprintRes.Data == null || fingerprintRes.Data.Count < 1)
            {
                apiResult.message.Add("اثرانگشت اشخاص یافت نشد");
                return apiResult;
            }

            foreach (var item in SignRequestEntity.SignRequestPeople)
            {
                if (item.IsOriginal == "1")
                {
                    lastClassifyNo++;
                    var tempSignElectronicBook = new Domain.Entities.SignElectronicBook();
                    item.SignClassifyNo = lastClassifyNo;
                    tempSignElectronicBook.ClassifyNo = (int)item.SignClassifyNo;
                    tempSignElectronicBook.RecordDate = _dateTimeService.CurrentDateTime;
                    tempSignElectronicBook.SignRequestId = SignRequestEntity.Id;
                    tempSignElectronicBook.ConfirmDate = _dateTimeService.CurrentPersianDate;
                    tempSignElectronicBook.ConfirmTime = _dateTimeService.CurrentTime;
                    tempSignElectronicBook.SignDate = _dateTimeService.CurrentPersianDate;
                    tempSignElectronicBook.SignRequestNationalNo = SignRequestEntity.NationalNo;
                    tempSignElectronicBook.Ilm = SignRequestConstants.CreateIlm;
                    tempSignElectronicBook.SignRequestPersonId = item.Id;
                    tempSignElectronicBook.ScriptoriumId = SignRequestEntity.ScriptoriumId;
                    tempSignElectronicBook.Id = Guid.NewGuid();
                    if (SignRequestEntity.IsRemoteRequest != "1")
                    {
                        // find fingerprint for the person
                        var foundFingerPrint = fingerprintRes.Data.FirstOrDefault(x => x.PersonObjectId == item.Id.ToString());

                        var validAgentTypes = new List<string>()
                        {
                            "2","3","5","7","14","18","10"
                        };

                        if (foundFingerPrint == null)
                        {
                            var foundRelated = GetRelatedRecursive(item.Id, SignRequestEntity.SignRequestPersonRelateds, validAgentTypes);

                            if (foundRelated == null)
                            {
                                apiResult.message.Add("اثرانگشت شخص یا نماینده‌ی وی یافت نشد");
                                return apiResult;
                            }

                            var foundAgentFingerprint = fingerprintRes.Data
                                .FirstOrDefault(x => x.PersonObjectId == foundRelated.AgentPersonId.ToString());

                            if (foundAgentFingerprint?.FingerPrintImage == null)
                            {
                                apiResult.message.Add("اثرانگشت نماینده برای ثبت یافت نشد");
                                return apiResult;
                            }


                            try
                            {
                                tempSignElectronicBook.HashOfFingerprint = Convert.ToBase64String(foundAgentFingerprint.FingerPrintImage).GetSha256Hash();
                            }
                            catch (Exception ex)
                            {
                                apiResult.message.Add("خطا در پردازش اثرانگشت نماینده: " + ex.Message);
                                return apiResult;
                            }
                        }
                        else
                        {
                            if (foundFingerPrint.FingerPrintImage == null)
                            {
                                apiResult.message.Add("اثر انگشت شخص موجود نیست");
                                return apiResult;
                            }

                            try
                            {
                                tempSignElectronicBook.HashOfFingerprint = Convert.ToBase64String(foundFingerPrint.FingerPrintImage).GetSha256Hash();
                            }
                            catch (Exception ex)
                            {
                                apiResult.message.Add("خطا در پردازش اثرانگشت شخص: " + ex.Message);
                                return apiResult;
                            }
                        }
                    }

                    if (SignRequestFileEntity == null || SignRequestFileEntity.ScanFile == null)
                    {
                        apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد");
                        return apiResult;
                    }

                    try
                    {
                        tempSignElectronicBook.HashOfFile = Convert.ToBase64String(SignRequestFileEntity.ScanFile).GetSha256Hash();
                    }
                    catch (Exception ex)
                    {
                        apiResult.message.Add("خطا در پردازش فایل اسکن: " + ex.Message);
                        return apiResult;
                    }

                    classes.Add(tempSignElectronicBook);
                    var tempSignElectronicBooksSign = tempSignElectronicBook.Adapt<SignElectronicBookSignViewModel>();
                    signElectronicBooksSign.Add(tempSignElectronicBooksSign);
                }
            }

            _apiResult.Data = new StageSignRequestViewModel();
            _apiResult.Data.SignElectronicBookSignObject = signElectronicBooksSign;
            _apiResult.Data.SignRequestSignObject = signRequestSignViewModel;
            SemaphoreEntity.SignElectronicBookData = JsonConvert.SerializeObject(classes, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                });

            SignRequestEntity.SignRequestFile = null;
            SemaphoreEntity.NewSignRequestData = JsonConvert.SerializeObject(SignRequestEntity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                });

            await _signRequestSemaphoreRepository.AddAsync(SemaphoreEntity, cancellationToken);
            return apiResult;
        }
        private async Task<ApiResult> GenerateSignRequestFile(CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();

            if (SignRequestEntity == null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد.");
                return apiResult;
            }

            SignRequestFileEntity = await _signRequestFileRepository.GetAsync(x => x.SignRequestId == SignRequestEntity.Id, cancellationToken);

            if (SignRequestFileEntity is null)
            {
                SignRequestFileEntity = new Domain.Entities.SignRequestFile
                {
                    Id = Guid.NewGuid(),
                    SignRequestId = SignRequestEntity.Id,
                    ScriptoriumId = SignRequestEntity.ScriptoriumId,
                    Ilm = SignRequestConstants.CreateIlm,
                    RecordDate = _dateTimeService.CurrentDateTime
                };

                await ProcessScanFileAttachment(apiResult, cancellationToken);

                if (!apiResult.IsSuccess)
                {
                    return apiResult;
                }

                await _signRequestFileRepository.AddAsync(SignRequestFileEntity, cancellationToken);
            }
            else
            {
                if (SignRequestFileEntity.ScanFile is null)
                {
                    await ProcessScanFileAttachment(apiResult, cancellationToken);

                    if (!apiResult.IsSuccess)
                    {
                        return apiResult;
                    }

                    await _signRequestFileRepository.UpdateAsync(SignRequestFileEntity, cancellationToken);
                }
            }

            return apiResult;
        }
        private SignRequestPersonRelated GetRelatedRecursive(
            Guid mainPersonId,
            IEnumerable<SignRequestPersonRelated> allRelateds,
            List<string> validAgentTypes
        )
        {
            return GetRelatedRecursiveInternal(mainPersonId, allRelateds, validAgentTypes, new HashSet<Guid>());
        }

        private SignRequestPersonRelated GetRelatedRecursiveInternal(
            Guid currentId,
            IEnumerable<SignRequestPersonRelated> allRelateds,
            List<string> validAgentTypes,
            HashSet<Guid> visited
        )
        {
            // Cycle detection
            if (!visited.Add(currentId))
            {
                // Already visited this ID — loop detected
                return null;
            }

            var direct = allRelateds
                .FirstOrDefault(x =>
                    x.MainPersonId == currentId &&
                    validAgentTypes.Contains(x.AgentTypeId)
                );

            if (direct != null)
            {
                // Recurse deeper
                var nested = GetRelatedRecursiveInternal(direct.AgentPersonId, allRelateds, validAgentTypes, visited);
                return nested ?? direct; // return deepest found or current
            }

            return null; // End of chain
        }

        private async Task ProcessScanFileAttachment(ApiResult apiResult, CancellationToken cancellationToken)
        {
            var relatedRecordId = SignRequestEntity?.IsRemoteRequest == "1"
                ? SignRequestEntity.RemoteRequestId?.ToString()
                : SignRequestEntity?.Id.ToString();

            if (string.IsNullOrEmpty(relatedRecordId))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه گواهی امضا مربوطه یافت نشد.");
                return;
            }

            var attachmentInput = new LoadAttachmentServiceInput
            {
                ClientId = SignRequestConstants.AttachmentClientId,
                RelatedRecordIds = new List<string> { relatedRecordId }
            };

            var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

            if (attachmentResult?.IsSuccess != true || attachmentResult.Data?.AttachmentViewModels == null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("ارتباط با سرویس آرشیو سازمان ثبت برقرار نشد");
                return;
            }

            bool scanFileFound = false;
            List<byte[]> scanFiles = new();
            foreach (var item in attachmentResult.Data.AttachmentViewModels)
            {
                if (item?.Medias == null || item.DocTypeId != SignRequestConstants.ScanFileDocumentType)
                    continue;



                foreach (var downloadItem in item.Medias)
                {
                    var downloadAttachmentInput = new DownloadMediaServiceInput
                    {
                        AttachmentFileId = downloadItem.MediaId,
                        AttachmentClientId = attachmentInput.ClientId,
                        AttachmentRelatedRecordId = SignRequestEntity?.Id.ToString(),
                        AttachmentTypeId = SignRequestConstants.ScanFileDocumentType
                    };

                    var downloadAttachmentOutput = await _mediator.Send(downloadAttachmentInput, cancellationToken);

                    if (downloadAttachmentOutput.Data.MediaFile != null)
                    {
                        scanFileFound = true;
                        scanFiles.Add(downloadAttachmentOutput.Data.MediaFile);
                        SignRequestFileEntity.ScanFileCreateDate = _dateTimeService.CurrentPersianDate;
                        SignRequestFileEntity.ScanFileCreateTime = _dateTimeService.CurrentTime;
                        break;
                    }
                    else
                    {
                        scanFileFound = false;
                        break;
                    }

                }



            }

            if (!scanFileFound || scanFiles.Count < 1)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد");
            }
            else
            {
                //var scanFileAsTiff = await MagickImageArrayExtensions.ToTiffBytesAsync(scanFiles);
                SignRequestFileEntity.ScanFile = scanFiles[0];
            }
        }
        private async Task<ApiResult> ValidateBusiness(CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();
            apiResult.IsSuccess = false;

            if (SignRequestEntity is null)
            {
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد .");
                return apiResult;
            }
            var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
            scriptoriumInfo.ScriptoriumId = SignRequestEntity.ScriptoriumId;
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
                return apiResult;
            }
            if (!SignRequestBusinessRule.CheckWorkPermit(configs, baseInfoApiResult.Data.ScriptoriumId))
            {
                apiResult.message.Add("دفترخانه مجوز ثبت گواهی امضا ندارد. لفطا با راهبر سیستم تماس بگیرید");
                return apiResult;
            }
            var getNationalNoPermitted = SignRequestBusinessRule.IsGetNationalNoOnWorkTime(_dateTimeService.CurrentDateTime, baseInfoApiResult.Data.DayOfWeek, configs);
            if (!getNationalNoPermitted)
            {
                apiResult.IsSuccess = false;

                apiResult.message.Add("ساعت کاری اخذ شناسه یکتا به پایان رسیده است.");
                return apiResult;
            }
            if (SignRequestEntity.State != "1")
            {
                apiResult.message.Add("گواهی امضا در وضعیت ایجاد شده نمیباشد .");
                return apiResult;
            }
            var attachmentInput = new LoadAttachmentServiceInput();
            if (SignRequestEntity.IsRemoteRequest == "1")
            {
                attachmentInput.ClientId = SignRequestConstants.AttachmentClientId;
                attachmentInput.RelatedRecordIds = new List<string> { SignRequestEntity.RemoteRequestId.ToString() };

                var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

                if (attachmentResult.IsSuccess)
                {
                    bool flag = false;
                    foreach (var item in attachmentResult.Data.AttachmentViewModels)
                    {
                        if (item.Medias.Count > 0 && item.DocTypeId == SignRequestConstants.ScanFileDocumentType)
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
                attachmentInput.ClientId = SignRequestConstants.AttachmentClientId;
                attachmentInput.RelatedRecordIds = new List<string> { SignRequestEntity.Id.ToString() };

                var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

                if (attachmentResult.IsSuccess)
                {
                    bool flag = false;
                    foreach (var item in attachmentResult.Data.AttachmentViewModels)
                    {
                        if (item.Medias.Count > 0 && item.DocTypeId == SignRequestConstants.ScanFileDocumentType)
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


            if (_userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Sardaftar && _userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Admin)
            {
                apiResult.message.Add("سمت کاربر ، سر دفتر نمیباشد . ");
                return apiResult;
            }
            if (SignRequestEntity.ScriptoriumId == "57999" || SignRequestEntity.ScriptoriumId == "57998")
            {
                Console.WriteLine(JsonConvert.SerializeObject(_userService.UserApplicationContext));
                if (_userService.UserApplicationContext.BranchAccess.IsBranchOwner == "false" &&
                    _userService.UserApplicationContext.BranchAccess.IssueDocAccess != "true")
                {
                    apiResult.message.Add("با توجه به حکم ابلاغ صادر شده، شما مجاز به تنظیم اسناد نمی باشید.");
                    return apiResult;
                }
            }




            //Check if any Original person exists
            if (!SignRequestEntity.SignRequestPeople.Any(x => x.IsOriginal == "1"))
            {
                apiResult.message.Add("شخص اصیل در گواهی امضا وجود ندارد . ");
                return apiResult;
            }

            string message = SignRequestPersonBusinessRule.CheckSabteAhval(SignRequestEntity.SignRequestPeople, configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            var notCheckPersonList = new List<Guid>();
            foreach (var item in SignRequestEntity.SignRequestPersonRelateds)
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

            message = SignRequestPersonBusinessRule.CheckSana(SignRequestEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckShahkar(SignRequestEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckMobileNo(SignRequestEntity.SignRequestPeople);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckRelatedPersonExists(SignRequestEntity.SignRequestPeople, SignRequestEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckInheritorExists(SignRequestEntity.SignRequestPeople, SignRequestEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckAmlakEskan(SignRequestEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckSaghir(SignRequestEntity.SignRequestPeople, SignRequestEntity.SignRequestPersonRelateds, _dateTimeService.CurrentPersianDate);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            if (!SignRequestRelatedPersonBusinessRule.CheckLoopInPersonRelated(SignRequestEntity.SignRequestPersonRelateds))
            {
                apiResult.message.Add("در گراف وابستگی اشخصاص حلقه به وجود آمده است .");
                return apiResult;
            }

            var fingerprintPersons = SignRequestPersonBusinessRule.FingerprintPersons(SignRequestEntity.SignRequestPeople);

            message = SignRequestPersonBusinessRule.CheckFingerprintPerson(SignRequestEntity.SignRequestPeople, SignRequestEntity.SignRequestPersonRelateds);

            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }

            if (SignRequestEntity.IsCostPaid == null || SignRequestEntity.IsCostPaid == "2")
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
        private async Task<ApiResult> UpdateDatabase(CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();

            GetInquiryPersonFingerprintListQuery getInquiryPersonFingerprint = new(SignRequestEntity.Id.ToString(), SignRequestEntity.SignRequestPeople.Select(x => x.NationalNo).ToList());

            var inquiryPersonFingerprintResult = await _mediator.Send(getInquiryPersonFingerprint, cancellationToken);
            if (inquiryPersonFingerprintResult.IsSuccess)
            {
                foreach (SignRequestPerson item in SignRequestEntity.SignRequestPeople)
                {
                    /*--------*/
                    var foundPerson = inquiryPersonFingerprintResult.Data.FirstOrDefault(x => x.PersonObjectId == item.Id.ToString());
                    if (foundPerson is not null)
                    {
                        item.TfaState = foundPerson.TFAState;
                        item.MocState = foundPerson.MOCState;
                        item.IsFingerprintGotten = foundPerson.IsFingerprintGotten.ToYesNo();
                    }

                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("استعلام از سرویس اثرانگشت با خطا مواجه شد .");
            }

            await _signRequestRepository.UpdateAsync(SignRequestEntity, cancellationToken);
            return apiResult;
        }

    }
}
