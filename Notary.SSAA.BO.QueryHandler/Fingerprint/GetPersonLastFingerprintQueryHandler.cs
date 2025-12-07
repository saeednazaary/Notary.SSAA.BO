using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ClientLogin;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.SharedModels;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Fingerprint;
using SourceAFIS;
using System.Data;


namespace Notary.SSAA.BO.QueryHandler.Fingerprint
{
    public class GetPersonLastFingerprintQueryHandler : BaseExternalQueryHandler<GetPersonLastFingerprintQuery, ExternalApiResult<LastPersonFingerprintViewModel>>
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.Document> _documentRepository;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.SignRequest> _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private ExternalApiResult<LastPersonFingerprintViewModel> apiResult;
        private static CachedToken _cachedToken;
        private int isInNewDatabase=0;
        private string[] validUsers = ["NEW_SSAR_APP", "OLD_SSAR_APP"];
        public GetPersonLastFingerprintQueryHandler(IMediator mediator, IPersonFingerprintRepository personFingerprintRepository, IUserService userService,
            IDateTimeService dateTimeService, IRepository<Notary.SSAA.BO.Domain.Entities.Document> documentRepository,
            IRepository<Notary.SSAA.BO.Domain.Entities.SignRequest> signRequestRepository, IRepository<SsrApiExternalUser> ssrApiExternalUser) : base(mediator, userService)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            apiResult = new();
            apiResult.Data = new();
            _ssrApiExternalUser = ssrApiExternalUser ?? throw new ArgumentNullException(nameof(ssrApiExternalUser));
        }

        protected override async Task<ExternalApiResult<LastPersonFingerprintViewModel>> RunAsync(GetPersonLastFingerprintQuery request, CancellationToken cancellationToken)
        {
            LastPersonFingerprintViewModel inquiryFingerprintResult = new();
            apiResult.ResCode = "1";
            var user = await _ssrApiExternalUser.TableNoTracking.Include(x => x.SsrDocVerifExternalUsers).Where(x => x.UserName == request.UserName && x.UserPassword == request.Password &&
            x.State == "1").FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResMessage = "نام کاربری یا رمز عبور وارد شده معتبر نمیباشد";
                apiResult.ResCode = "103";
                return apiResult;

            }
            if (!validUsers.Contains(user.UserName))
            {
                apiResult.ResMessage = "کاربر محترم شما مجاز به فراخوانی سرویس نمیباشید.";
                apiResult.ResCode = "104";
                return apiResult;
            }
            ClientLoginServiceInput clientLoginServiceInput = new();
            if (_cachedToken is null || string.IsNullOrWhiteSpace(_cachedToken.AccessToken) || _dateTimeService.CurrentDateTime.AddSeconds(30) >= _cachedToken.ExpiresAt)
            {
                _cachedToken = new();
                var tokenRes = await _mediator.Send(clientLoginServiceInput, cancellationToken);
                if (tokenRes is not null && tokenRes.IsSuccess)
                {
                    _cachedToken.AccessToken = "Bearer " + tokenRes.Data.Credential.AccessToken;
                    _cachedToken.ExpiresAt = tokenRes.Data.Credential.ExpireDate;
                }
                else
                {
                    apiResult.ResCode = "106";
                    apiResult.ResMessage = "ارتباط با سرویس توکن برقرار نشد.";
                    return apiResult;
                }
            }
            _userService.UserApplicationContext.Token = _cachedToken.AccessToken;
            var lastFingerprintFromServiceInput = new FindLastFingerprintServiceInput();
            lastFingerprintFromServiceInput.PersonNationalNo = request.PersonNationalNo;
            lastFingerprintFromServiceInput.DocumentId = request.PersonNationalNo;
            if (!string.IsNullOrWhiteSpace(request.DocumentId))
            {
                lastFingerprintFromServiceInput.DocumentId += "|LegacyId=" + request.DocumentId;
            }
            lastFingerprintFromServiceInput.SelectedFinger = request.SelectedFinger;
            var lastFingerprintFromService = await _mediator.Send(lastFingerprintFromServiceInput, cancellationToken);

            if (lastFingerprintFromService.IsSuccess)
            {
                var lastDocdocuments = await _documentRepository.TableNoTracking.Include(x=>x.DocumentInfoConfirm).Where(x => x.DocumentPeople.Any(y => y.NationalNo == request.PersonNationalNo && y.ScriptoriumId == x.ScriptoriumId)
                && x.State == "6" && x.LegacyId==null&&!TestScriptoriumIds.ScriptoriumIds.Contains(x.ScriptoriumId)).ToListAsync(cancellationToken);


                var lastSignRequests = await _signRequestRepository.TableNoTracking.Include(x=>x.SignElectronicBooks).Where(x => x.SignRequestPeople.Any(y => y.NationalNo == request.PersonNationalNo && y.ScriptoriumId == x.ScriptoriumId)
                && x.State == "2" && x.LegacyId == null && !TestScriptoriumIds.ScriptoriumIds.Contains(x.ScriptoriumId)).ToListAsync(cancellationToken);

                var lastObjectIds = lastDocdocuments.Select(x => x.Id.ToString()).ToList().Concat(lastSignRequests.Select(x => x.Id.ToString()).ToList()).Distinct().ToList();

                PersonFingerprint lastFingerprintInNew = new();
                if (lastObjectIds.Count == 0)
                {
                    lastFingerprintInNew = null;
                }
                else
                {
                    lastFingerprintInNew = await _personFingerprintRepository.GetLastFingerprint(lastObjectIds, request.PersonNationalNo,
                        request.SelectedFinger.ToString(), TestScriptoriumIds.ScriptoriumIds, cancellationToken);
                }

                var lastFingerprint = LastFingerprint(lastFingerprintFromService?.Data, lastFingerprintInNew);
                if (lastFingerprint is null )
                {
                    apiResult.ResCode = "102";
                    apiResult.ResMessage = "هیچ اثر انگشتی برای متقاضی در پایگاه داده‌های جدید و قدیم یافت نشد.";
                    return apiResult;
                }



                if (isInNewDatabase==1)
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
                    else
                    {
                        apiResult.ResCode = "105";
                        apiResult.ResMessage = "ارتباط با سرویس توکن برقرار نشد.";
                        return apiResult;
                    }

                    if (lastFingerprintInNew.PersonFingerprintUseCaseId == "1")
                    {

                        var selectedSignRequest = lastSignRequests.FirstOrDefault(x => x.Id == lastFingerprintInNew.UseCaseMainObjectId.ToGuid());
                        apiResult.Data.ScriptoriumName=scriptoriumTitle +
                            "-شماره ترتیب سند : " + selectedSignRequest.SignElectronicBooks?.FirstOrDefault()?.ClassifyNo.ToString() +
                            "-تاریخ سند : " + selectedSignRequest.ConfirmDate+"-"+ selectedSignRequest.ConfirmTime;
                        apiResult.Data.EntityType = RelatedEntity.SignRequest;
                        apiResult.Data.EntityId = selectedSignRequest.Id.ToString();
                        apiResult.Data.EntityLegacyId = selectedSignRequest.LegacyId;
                        apiResult.Data.EntityNationalNo = selectedSignRequest.NationalNo;
                        apiResult.Data.EntityConfirmDateTime = selectedSignRequest.ConfirmDate + "-" + selectedSignRequest.ConfirmTime;
                        apiResult.Data.ScriptoriumCode = selectedSignRequest.ScriptoriumId;
                    }
                    else if (lastFingerprintInNew.PersonFingerprintUseCaseId == "2")
                    {

                        var selectedDocument = lastDocdocuments.FirstOrDefault(x => x.Id == lastFingerprintInNew.UseCaseMainObjectId.ToGuid());
                        apiResult.Data.ScriptoriumName= scriptoriumTitle +
                            "-شماره ترتیب سند : " + selectedDocument.ClassifyNo.ToString() +
                            "-تاریخ سند : " + selectedDocument.DocumentInfoConfirm.ConfirmDate+"-"+ selectedDocument.DocumentInfoConfirm.ConfirmTime.ToString();
                        apiResult.Data.EntityType = RelatedEntity.Document;
                        apiResult.Data.EntityId = selectedDocument.Id.ToString();
                        apiResult.Data.EntityLegacyId = selectedDocument.LegacyId;
                        apiResult.Data.EntityNationalNo = selectedDocument.NationalNo;
                        apiResult.Data.EntityConfirmDateTime = selectedDocument.DocumentInfoConfirm.ConfirmDate + "-" + selectedDocument.DocumentInfoConfirm.ConfirmTime;
                        apiResult.Data.ScriptoriumCode = selectedDocument.ScriptoriumId;
                    }

                }
                else
                {
                    apiResult.Data.EntityType = lastFingerprint.EntityType;
                    apiResult.Data.EntityId = lastFingerprint.EntityId;
                    apiResult.Data.EntityLegacyId = lastFingerprint.EntityLegacyId;
                    apiResult.Data.EntityNationalNo = lastFingerprint.EntityNationalNo;
                    apiResult.Data.EntityConfirmDateTime = lastFingerprint.EntityConfirmDateTime;
                    apiResult.Data.ScriptoriumCode = lastFingerprint.ScriptoriumCode;
                    apiResult.Data.ScriptoriumName = lastFingerprint.ScriptoriumName;
                }
                apiResult.Data.PersonLastFingerprintFeature = lastFingerprint.PersonLastFingerprintFeature;
                apiResult.Data.FingerPrintDevice = lastFingerprint.FingerPrintDevice;
                apiResult.Data.GetDateTime = lastFingerprint.GetDateTime;
                apiResult.Data.PersonLastFingerprintImageWidth = lastFingerprint.PersonLastFingerprintImageWidth;
                apiResult.Data.PersonLastFingerprintImageHeight = lastFingerprint.PersonLastFingerprintImageHeight;
                apiResult.Data.PersonLastFingerprintImage = lastFingerprint.PersonLastFingerprintImage;
                apiResult.Data.PersonLastFingerprintRawImage = lastFingerprint.PersonLastFingerprintRawImage;



                if (lastFingerprint != null && (!string.IsNullOrWhiteSpace(lastFingerprint.PersonLastFingerprintImage)
                    || !string.IsNullOrWhiteSpace(lastFingerprint.PersonLastFingerprintRawImage)))
                {
                    try
                    {
                        var fingerPrint1 = GetImage(
                            !string.IsNullOrWhiteSpace(request.FingerprintDevice) ? request.FingerprintDevice.ToLower() : "",
                            request.FingerprintWidth,
                            request.FingerPrintHeight,
                            Convert.FromBase64String(request.FingerprintRawImage));

                        var fingerPrint2 = GetImage(
                            !string.IsNullOrWhiteSpace(lastFingerprint.FingerPrintDevice) ? lastFingerprint.FingerPrintDevice.ToLower() : "",
                            lastFingerprint.PersonLastFingerprintImageWidth,
                            lastFingerprint.PersonLastFingerprintImageHeight,
                            Convert.FromBase64String(lastFingerprint.PersonLastFingerprintRawImage));

                        apiResult.Data.CompareResult = MatchFingerprints(fingerPrint1, fingerPrint2);
                    }
                    catch
                    {
                        var fingerPrint1 = GetImage(
                            !string.IsNullOrWhiteSpace(request.FingerprintDevice) ? request.FingerprintDevice.ToLower() : "",
                            request.FingerprintWidth,
                            request.FingerPrintHeight,
                            Convert.FromBase64String(request.FingerprintRawImage));

                        apiResult.Data.CompareResult = MatchFingerprints(
                            fingerPrint1,
                            Convert.FromBase64String(lastFingerprint.PersonLastFingerprintImage));
                    }
                }

            }
            else
            {
                if (lastFingerprintFromService.message.Count > 0)
                    apiResult.ResMessage = lastFingerprintFromService.message[0];
                else
                    apiResult.ResMessage="خطا در دریافت اخرین اثر انگشت ثبت شده ی متقاضی رخ داد ";
                apiResult.ResCode = "101";
            }
            return apiResult;
        }

        protected override bool HasAccess(GetPersonLastFingerprintQuery request, IList<string> userRoles)
        {
            return true;
        }

        public static bool MatchFingerprints(byte[] firstFingerprint, byte[] secondFingerprint)
        {
            var options = new FingerprintImageOptions { Dpi = 500 };
            var probe = new FingerprintTemplate(
                new FingerprintImage(firstFingerprint, options));

            FingerprintTemplate candidate = null;
            double score = 0;
            if (secondFingerprint != null)
            {
                candidate = new FingerprintTemplate(
                new FingerprintImage(secondFingerprint, options));
            }

            if (candidate != null)
            {
                score = new FingerprintMatcher(probe)
                   .Match(candidate);
            }
            int threshhold = 30;
            if (score >= threshhold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static byte[] GetImage(string fingerDevice, int width, int height, byte[] rawImage)
        {
            try
            {

                FingerDevice device = FingerDevice.Fotronic;
                if (fingerDevice.StartsWith("futronic"))
                {
                    device = FingerDevice.Fotronic;
                }
                else if (fingerDevice == "suprema")
                {
                    device = FingerDevice.Suprima;
                }
                else if (fingerDevice == "hongdas580")
                {
                    device = FingerDevice.Hongda;

                }

                return FingerprintUtilities.ConvertToJPG(device, width, height, rawImage);

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
                isInNewDatabase = 1;
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
                isInNewDatabase = 2;

                result = oldDatabase;
                return result;
            }
            var newDatabaseGetDateTime = (newDatabase.FingerprintGetDate + "-" + newDatabase.FingerprintGetTime).ToGregorianDateTime();
            if (newDatabaseGetDateTime > oldDatabase.GetDateTime.ToGregorianDateTime())
            {
                isInNewDatabase = 1;
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
                isInNewDatabase = 2;

                result = oldDatabase;
            }
            return result;
        }
    }
}
