using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Fingerprint;
using Serilog;
using SourceAFIS;


namespace Notary.SSAA.BO.CommandHandler.Fingerprint_V2.Handlers
{
    internal class MatchPersonFingerprintCommandHandler : BaseCommandHandler<MatchPersonFingerprintV2Command, ApiResult>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.Document> _documentRepository;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.SignRequest> _signRequestRepository;
        private ApiResult apiResult;
        private readonly IDateTimeService _dateTimeService;

        public MatchPersonFingerprintCommandHandler(IMediator mediator, IPersonFingerprintRepository personFingerprintRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IRepository<Notary.SSAA.BO.Domain.Entities.Document> documentRepository,
            IRepository<Notary.SSAA.BO.Domain.Entities.SignRequest> signRequestRepository) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            apiResult = new ApiResult();
        }

        protected override async Task<ApiResult> ExecuteAsync(MatchPersonFingerprintV2Command request, CancellationToken cancellationToken)
        {

            masterEntity = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());

            if (masterEntity is not null)
            {
                await _personFingerprintRepository.LoadReferenceAsync(masterEntity, x => x.PersonFingerType, cancellationToken);
                string LegacyId = "";
                if (masterEntity.PersonFingerprintUseCaseId == "2")
                {
                    var document = await _documentRepository.TableNoTracking.Where(x => x.Id == masterEntity.UseCaseMainObjectId.ToGuid()).FirstOrDefaultAsync(cancellationToken);
                    if (document is not null)
                    {
                        if (!string.IsNullOrWhiteSpace(document.LegacyId))
                        {
                            LegacyId = document.LegacyId;
                        }
                    }
                }
                var lastFingerprintFromServiceInput = new FindLastFingerprintServiceInput();
                lastFingerprintFromServiceInput.PersonNationalNo = masterEntity.PersonNationalNo;
                lastFingerprintFromServiceInput.DocumentId = masterEntity.UseCaseMainObjectId.ToString().Replace("-", "").Replace("_", "").ToUpper();
                if (!string.IsNullOrWhiteSpace(LegacyId))
                {
                    lastFingerprintFromServiceInput.DocumentId += "|LegacyId=" + LegacyId;
                }
                lastFingerprintFromServiceInput.SelectedFinger = Convert.ToInt32(masterEntity.PersonFingerType.SabtahvalCode);
                var lastFingerprintFromService = await _mediator.Send(lastFingerprintFromServiceInput, cancellationToken);

                if (lastFingerprintFromService.IsSuccess)
                {
                    var lastDocdocuments = await _documentRepository.TableNoTracking.Include(x => x.DocumentInfoConfirm).Where(x => x.DocumentPeople.Any(y => y.NationalNo == masterEntity.PersonNationalNo && y.ScriptoriumId == x.ScriptoriumId)
                    && x.State == "6" && x.LegacyId == null && !TestScriptoriumIds.ScriptoriumIds.Contains(x.ScriptoriumId)).ToListAsync(cancellationToken);


                    var lastSignRequests = await _signRequestRepository.TableNoTracking.Include(x => x.SignElectronicBooks).Where(x => x.SignRequestPeople.Any(y => y.NationalNo == masterEntity.PersonNationalNo && y.ScriptoriumId == x.ScriptoriumId)
                    && x.State == "2" && x.LegacyId == null && !TestScriptoriumIds.ScriptoriumIds.Contains(x.ScriptoriumId)).ToListAsync(cancellationToken);

                    var lastObjectIds = lastDocdocuments.Select(x => x.Id.ToString()).ToList().Concat(lastSignRequests.Select(x => x.Id.ToString()).ToList()).Distinct().ToList();

                    PersonFingerprint lastFingerprintInNew = new();
                    if (lastObjectIds.Count == 0)
                    {
                        lastFingerprintInNew = null;
                    }
                    else
                    {
                        lastFingerprintInNew = await _personFingerprintRepository.GetLastFingerprint(lastObjectIds, masterEntity.PersonNationalNo,masterEntity.PersonFingerType.SabtahvalCode, 
                            TestScriptoriumIds.ScriptoriumIds, cancellationToken);
                    }

                    var lastFingerprint = LastFingerprint(lastFingerprintFromService?.Data, lastFingerprintInNew);
                    if (lastFingerprint != null && (!string.IsNullOrWhiteSpace(lastFingerprint.PersonLastFingerprintImage)
                        || !string.IsNullOrWhiteSpace(lastFingerprint.PersonLastFingerprintRawImage)))
                    {
                        try
                        {
                            byte[] alternativeSecondImage = null;
                            if (!string.IsNullOrWhiteSpace(lastFingerprint.PersonLastFingerprintRawImage))
                                alternativeSecondImage = GetImage(lastFingerprint, cancellationToken);
                            MatchFingerprints(masterEntity.FingerprintImageFile, !string.IsNullOrWhiteSpace(lastFingerprint.PersonLastFingerprintImage) ? Convert.FromBase64String(lastFingerprint.PersonLastFingerprintImage) : null, alternativeSecondImage);
                            if (apiResult.HasAllarmMessage)
                            {
                                if (string.IsNullOrWhiteSpace(lastFingerprint?.ScriptoriumName))
                                {
                                    string scriptoriumTitle = string.Empty;
                                    var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                                    scriptoriumInfo.ScriptoriumId = lastFingerprintInNew.OrganizationId;
                                    scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
                                    var baseInfoApiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
                                    if (baseInfoApiResult.IsSuccess && baseInfoApiResult.Data is not null)
                                    {
                                        scriptoriumTitle = baseInfoApiResult.Data.ScriptoriumName;

                                    }
                                    if (lastFingerprintInNew.PersonFingerprintUseCaseId == "1")
                                    {

                                        var selectedSignRequest = lastSignRequests.FirstOrDefault(x => x.Id == lastFingerprintInNew.UseCaseMainObjectId.ToGuid());
                                        lastFingerprint.ScriptoriumName = scriptoriumTitle +
                                            "-شماره ترتیب سند : " + selectedSignRequest.SignElectronicBooks?.FirstOrDefault()?.ClassifyNo.ToString() +
                                            "-تاریخ سند : " + selectedSignRequest.ConfirmDate + "-" + selectedSignRequest.ConfirmTime;

                                    }
                                    else if (lastFingerprintInNew.PersonFingerprintUseCaseId == "2")
                                    {

                                        var selectedDocument = lastDocdocuments.FirstOrDefault(x => x.Id == lastFingerprintInNew.UseCaseMainObjectId.ToGuid());
                                        lastFingerprint.ScriptoriumName = scriptoriumTitle +
                                            "-شماره ترتیب سند : " + selectedDocument.ClassifyNo.ToString() +
                                            "-تاریخ سند : " + selectedDocument.DocumentInfoConfirm.ConfirmDate + "-" + selectedDocument.DocumentInfoConfirm.ConfirmTime.ToString();

                                    }

                                }
                                apiResult.message =
                                    [
                                        $"هشدار! اثرانگشت دریافت شده با آخرین اثرانگشت ثبت شده برای این شخص در {lastFingerprint.ScriptoriumName} مطابقت ندارد.سردفتر محترم، آیا با شرایط اعلام شده، ثبت این اثرانگشت را ادامه می دهید ؟",
                                        ];
                            }
                        }
                        catch
                        {
                            apiResult.message.Add(" خطا در مقایسه  اخرین اثر انگشت ثبت شده ی متقاضی با اثر انگشت جاری وی رخ داد");
                            apiResult.IsSuccess = false;
                        }
                    }

                }
                else
                {
                    if (lastFingerprintFromService.message.Count > 0)
                        apiResult.message = lastFingerprintFromService.message;
                    else
                        apiResult.message.Add("خطا در دریافت اخرین اثر انگشت ثبت شده ی متقاضی رخ داد ");
                    apiResult.IsSuccess = false;
                }

            }
            else
            {
                apiResult.message.Add("اثرانگشت یافت نشد .");
                apiResult.IsSuccess = false;
            }

            return apiResult;
        }

        protected override bool HasAccess(MatchPersonFingerprintV2Command request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        private void MatchFingerprints(byte[] firstFingerprint, byte[] secondFingerprint, byte[] alternativeSecondFingerprint)
        {
            var options = new FingerprintImageOptions { Dpi = 500 };
            var probe = new FingerprintTemplate(new FingerprintImage(firstFingerprint, options));

            FingerprintTemplate candidate = null;
            FingerprintTemplate candidateAlt = null;
            double score = 0;
            double scoreAlt = 0;

            // Prepare candidates if available
            if (secondFingerprint is not null)
                candidate = new FingerprintTemplate(new FingerprintImage(secondFingerprint, options));

            if (alternativeSecondFingerprint is not null)
                candidateAlt = new FingerprintTemplate(new FingerprintImage(alternativeSecondFingerprint, options));

            // Reuse matcher for efficiency
            var matcher = new FingerprintMatcher(probe);

            if (candidate is not null)
                score = matcher.Match(candidate);

            if (candidateAlt is not null)
                scoreAlt = matcher.Match(candidateAlt);

            // Load threshold safely
            int threshold = 40;
            if (int.TryParse(Configuration.Settings.MinimumFingerprintQualityThreshold, out int cfgThreshold))
                threshold = cfgThreshold;

            // Evaluate result
            apiResult.IsSuccess = score >= threshold || scoreAlt >= threshold;

            if (!apiResult.IsSuccess)
            {
                apiResult.message.Add("اثر انگشت اخذ شده با اثر انگشت موجود در پایگاه داده مطابقت ندارد ، آیا مایل به ادامه هستید ؟");
                apiResult.HasAllarmMessage = true;
            }

        }

        private static byte[] GetImage(FindLastFingerprintServiceViewModel request, CancellationToken cancellationToken)
        {
            try
            {
                var convertToJpegInput = new FingerprintRAWToJPEGInput();
                convertToJpegInput.Width = request.PersonLastFingerprintImageWidth;
                convertToJpegInput.Height = request.PersonLastFingerprintImageHeight;
                convertToJpegInput.Data = request.PersonLastFingerprintRawImage;
                var str = request.FingerPrintDevice.ToLower();
                if (str.StartsWith("futronic"))
                {
                    convertToJpegInput.DeviceType = FingerDevice.Fotronic;
                }
                else if (str == "suprema")
                {
                    convertToJpegInput.DeviceType = FingerDevice.Suprima;
                }
                else if (str == "hongdas580")
                {
                    convertToJpegInput.DeviceType = FingerDevice.Hongda;

                }

                return Convert.FromBase64String(FingerprintUtilities.ConvertToJPEGBase64(convertToJpegInput));

            }
            catch
            {
                return null;
            }
        }

        private FindLastFingerprintServiceViewModel LastFingerprint(FindLastFingerprintServiceViewModel oldDatabase, PersonFingerprint newDatabase)
        {
            var result = new FindLastFingerprintServiceViewModel();
            if (oldDatabase == null && newDatabase == null)
            {
                return null;
            }
            if (oldDatabase is null || string.IsNullOrWhiteSpace(oldDatabase.PersonLastFingerprintImage))
            {
                result.FingerPrintDevice = newDatabase.FingerprintScannerDeviceType;
                result.ScriptoriumCode = newDatabase.OrganizationId;
                result.PersonLastFingerprintRawImage = Convert.ToBase64String(newDatabase.FingerprintRawImage);
                result.PersonLastFingerprintFeature = Convert.ToBase64String(newDatabase.FingerprintFeatures);
                result.PersonLastFingerprintImage = Convert.ToBase64String(newDatabase.FingerprintImageFile);
                result.PersonLastFingerprintImageHeight = newDatabase.FingerprintImageHeight ?? 0;
                result.PersonLastFingerprintImageWidth = newDatabase.FingerprintImageWidth ?? 0;
                result.GetDateTime = newDatabase.FingerprintGetDate + "-" + newDatabase.FingerprintGetTime;
                return result;
            }
            if (newDatabase is null || newDatabase.FingerprintRawImage is null)
            {
                result = oldDatabase;
                return result;
            }
            var newDatabaseGetDateTime = (newDatabase.FingerprintGetDate + "-" + newDatabase.FingerprintGetTime).ToGregorianDateTime();
            if (newDatabaseGetDateTime > oldDatabase.GetDateTime.ToGregorianDateTime())
            {
                result.FingerPrintDevice = newDatabase.FingerprintScannerDeviceType;
                result.ScriptoriumCode = newDatabase.OrganizationId;
                result.PersonLastFingerprintRawImage = Convert.ToBase64String(newDatabase.FingerprintRawImage);
                result.PersonLastFingerprintFeature = Convert.ToBase64String(newDatabase.FingerprintFeatures);
                result.PersonLastFingerprintImage = Convert.ToBase64String(newDatabase.FingerprintImageFile);
                result.PersonLastFingerprintImageHeight = newDatabase.FingerprintImageHeight ?? 0;
                result.PersonLastFingerprintImageWidth = newDatabase.FingerprintImageWidth ?? 0;
                result.GetDateTime = newDatabase.FingerprintGetDate + "-" + newDatabase.FingerprintGetTime;

            }
            else
            {
                result = oldDatabase;
            }
            return result;
        }


    }
}
