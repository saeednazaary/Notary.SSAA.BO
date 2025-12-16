using MediatR;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.EDM;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Security;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;


namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class ReportSignRequestQueryHandler : BaseQueryHandler<ReportSignRequestQuery, ApiResult<ReportSignRequestViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;

        public ReportSignRequestQueryHandler(IMediator mediator, IUserService userService,
            ISignRequestRepository signRequestRepository, IDateTimeService dateTimeService, IWebHostEnvironment webHostEnvironment, IApplicationIdGeneratorService applicationIdGeneratorService)
            : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
        }
        protected override bool HasAccess(ReportSignRequestQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }

        protected override async Task<ApiResult<ReportSignRequestViewModel>> RunAsync(ReportSignRequestQuery request, CancellationToken cancellationToken)
        {
            SignRequestViewModel result = new();
            Domain.Entities.SignRequest signRequest = null;
            ApiResult<ReportSignRequestViewModel> apiResult = new();
            Guid signRequestId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(request.SignRequestId))
                signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty && string.IsNullOrWhiteSpace(request.SignRequestNo))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            signRequest = !request.SignRequestNo.IsNullOrWhiteSpace()
                ? await _signRequestRepository.SignRequestTracking(request.SignRequestNo, cancellationToken)
                : await _signRequestRepository.SignRequestTracking(signRequestId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (signRequest != null)
            {
                if (signRequest.State == "2")
                {

                    ReportSignRequestViewModel reportViewModel = new();
                    SignRequestPrintViewModel reportObject = SignRequestMapper.ToPrintViewModel(signRequest);

                    string[] idList = new string[] { signRequest.ScriptoriumId };

                    var attachmentInput = new LoadAttachmentServiceInput
                    {
                        ClientId = SignRequestConstants.AttachmentClientId,
                        RelatedRecordIds = new List<string> { signRequest.Id.ToString() }
                    };

                    var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

                    if (attachmentResult?.IsSuccess != true || attachmentResult?.Data?.AttachmentViewModels == null)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("ارتباط با سرویس آرشیو سازمان ثبت برقرار نشد");
                        return apiResult;
                    }

                    List<byte[]> scanFiles = new();

                    foreach (var item in attachmentResult.Data.AttachmentViewModels)
                    {
                        if (item?.Medias == null ||
                            item.DocTypeId != SignRequestConstants.ScanFileDocumentType)
                            continue;

                        foreach (var media in item.Medias)
                        {
                            var downloadResult = await _mediator.Send(new DownloadMediaServiceInput
                            {
                                AttachmentFileId = media.MediaId,
                                AttachmentClientId = attachmentInput.ClientId,
                                AttachmentRelatedRecordId = signRequest.Id.ToString(),
                                AttachmentTypeId = SignRequestConstants.ScanFileDocumentType
                            }, cancellationToken);

                            var fileBytes = downloadResult?.Data?.MediaFile;

                            if (fileBytes == null || fileBytes.Length == 0)
                            {
                                apiResult.IsSuccess = false;
                                apiResult.message.Add("ارتباط با آرشیو سازمان برقرار نشد.");
                                return apiResult;
                            }

                            scanFiles.Add(fileBytes);
                        }
                    }

                    if (scanFiles.Count == 0)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("ارتباط با آرشیو سازمان برقرار نشد.");
                        return apiResult;
                    }
                    else
                    {
                        //var scanFileAsTiff = await MagickImageArrayExtensions.ToTiffBytesAsync(scanFiles);
                        reportObject.SignRequestPics = scanFiles.Select(pic => new SignRequestPictures { SignPics = pic }).ToList();
                    }

                    SignRequestBasicInfoServiceInput scriptoriumInput = new();
                    scriptoriumInput.ScriptoriumId = signRequest.ScriptoriumId;
                    scriptoriumInput.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;

                    var scriptoriumResponse = await _mediator.Send(scriptoriumInput, cancellationToken);

                    if (scriptoriumResponse.IsSuccess)
                    {
                        reportObject.ScriptoriumCode = scriptoriumResponse.Data.ScriptoriumCode;
                        reportObject.ScriptoriumNo = scriptoriumResponse.Data.ScriptoriumNo;
                        reportObject.ScriptoriumLocation = scriptoriumResponse.Data.GeoLocationName.Replace("ء", "ی");
                        reportObject.ScriptoriumAddress = " نشانی دفترخانه: " + scriptoriumResponse.Data.Address.Replace("ء", "ی");
                        reportObject.ScriptoriumTell = scriptoriumResponse.Data.Tel;
                        reportObject.ScriptoriumName = $"دفترخانه اسناد رسمی {reportObject.ScriptoriumNo} {reportObject.ScriptoriumLocation}";
                        reportObject.LegacyCode = $"سردفتر {reportObject.ScriptoriumNo} {reportObject.ScriptoriumLocation} - {scriptoriumResponse.Data.ExordiumFullName}";
                        if(signRequest.ScriptoriumId=="57999" || signRequest.ScriptoriumId=="57998")
                        {
                            Console.WriteLine(JsonConvert.SerializeObject(_userService.UserApplicationContext));
                            if (_userService.UserApplicationContext.BranchAccess.IsBranchOwner == "false")
                            {
                                reportObject.LegacyCode = $"سردفتر کفیل {reportObject.ScriptoriumNo} {reportObject.ScriptoriumLocation} - {scriptoriumResponse.Data.ExordiumFullName}";

                            }
                        }

                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("سرویس اطلاعات پایه با خطا مواجه شد . ");
                        return apiResult;
                    }

                    int row = 0;
                    GetPersonFingerprintImageQuery getPersonFingerprintImage = new(signRequest.Id.ToString());

                    ApiResult<List<Domain.RepositoryObjects.Fingerprint.GetPersonFingerprintImageRepositoryObject>> fingerprintRes = await _mediator.Send(getPersonFingerprintImage, cancellationToken);
                    if (!fingerprintRes.IsSuccess || fingerprintRes.Data == null || fingerprintRes.Data.Count == 0)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("اثرانگشت اشخاص یا مشکل مواجه شد .");
                        return apiResult;
                    }
                    List<Guid> notCheckPersonList = [];
                    foreach (Domain.Entities.SignRequestPersonRelated item in signRequest.SignRequestPersonRelateds)
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
                                {
                                    notCheckPersonList.Add(item.MainPersonId);
                                }

                                break;
                            default:
                                break;
                        }
                    }
                    notCheckPersonList = notCheckPersonList.Distinct().ToList();

                    foreach (Domain.Entities.SignRequestPerson item in signRequest.SignRequestPeople)
                    {
                        SignRequestPersonPrintViewModel Persons = new();
                        if (!notCheckPersonList.Any(x => x == item.Id))
                        {
                            var fp = fingerprintRes.Data.FirstOrDefault(x => x.PersonObjectId == item.Id.ToString());

                            if (fp?.FingerPrintImage == null)
                            {
                                apiResult.IsSuccess = false;
                                apiResult.message.Add($"اثرانگشت "+item.Name+" "+item.Family+"یافت نشد");
                                return apiResult;
                            }

                            SignRequestPersonsPrintViewModel SignRequestPersons = new()
                            {
                                FullName = item.Name + " " + item.Family,
                                FingerPrintImage = fingerprintRes.Data.FirstOrDefault(x => x.PersonObjectId == item.Id.ToString())?.FingerPrintImage
                            };
                            reportObject.SignRequestPersons.Add(SignRequestPersons);
                        }


                        if (item.IsOriginal == "1")
                        {
                            row++;
                            Persons.PersonDescription = " شماره ترتیب: " + item.SignClassifyNo + Environment.NewLine;
                            Persons.IsOriginalPerson = item.IsOriginal;
                            if (item.PersonType == "1")
                            {
                                if (item.IsIranian == "1")
                                {
                                    if (item.SexType == "1")
                                    {
                                        Persons.PersonDescription += "خانم " + item.Name + " " + item.Family + " فرزند " + item.FatherName + " به شماره ملی "
                                            + item.NationalNo + " تاریخ تولد" + item.BirthDate + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(signRequest, item);

                                    }
                                    else
                                    {
                                        Persons.PersonDescription += "آقای " + item.Name + " " + item.Family + " فرزند " + item.FatherName + " شماره ملی "
                                            + item.NationalNo + " تاریخ تولد" + item.BirthDate + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(signRequest, item);
                                    }
                                }
                                else
                                {
                                    if (item.SexType == "1")
                                    {
                                        Persons.PersonDescription += "خانم " + item.Name + " " + item.Family + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(signRequest, item);
                                    }
                                    else
                                    {
                                        Persons.PersonDescription += "آقای " + item.Name + " " + item.Family + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(signRequest, item);
                                    }
                                }
                            }
                            else
                            {
                                if (item.IsIranian == "1")
                                {
                                    Persons.PersonDescription += item.Name + " " + "به شناسه ملی " + item.NationalNo + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(signRequest, item);
                                }
                                else
                                {
                                    Persons.PersonDescription += item.Name + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(signRequest, item);
                                }
                            }
                            reportObject.Persons.Add(Persons);
                        }
                    }

                    int x = reportObject.Persons.Where(p => p.IsOriginalPerson == "1").ToList().Count;
                    if (x > 1)
                    {
                        reportObject.Desc = "فقط صحت امضا متقاضیان ذکر شده در این برگ مورد تایید .است";
                    }
                    else if (x == 1)
                    {
                        reportObject.Desc = "فقط صحت امضا متقاضی ذکر شده در این برگ مورد تایید .است";
                    }
                    string EncryptedText = EncryptionHelper.Encrypt(reportObject.SignNationalNo + "-" + reportObject.SecretCode, reportObject.ScriptoriumCode);
                    string EncryptedPass = EncryptionHelper.Encrypt(reportObject.ScriptoriumCode, EncryptedText[..5]);

                    reportObject.MatrixBarcode = @"برای تصدیق اصالت گواهی امضاء از برنامه موبایل سازمان ثبت یا درگاه ssaa.ir استفاده کنید." + "#" + EncryptedText + "#1#" + EncryptedPass;

                    StiReport stimulTools = new();
                    string reportName = _webHostEnvironment.ContentRootPath + "/Content/Reports/SignRequest/PrintSignRequest.mrt";

                    stimulTools.RegBusinessObject("ONotarySignReqEntity", reportObject);
                    Console.WriteLine(JsonConvert.SerializeObject(reportObject));
                    StiReport report = stimulTools.Load(reportName);
                    StiPdfExportSettings PdfSettings = new()
                    {
                        ImageResolution = 200,
                        ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg,
                        ImageQuality = 100,
                        EmbeddedFonts = true,
                        StandardPdfFonts = false,
                        ImageFormat = StiImageFormat.Grayscale
                    };
                    report.Render(false);
                    StiNetCoreActionResult generatedReport = StiNetCoreReportResponse.ResponseAsPdf(report, PdfSettings);

                    if (generatedReport is not null)
                    {
                        //SignRequestReportEdmQuery signRequestEDMService = new(signRequest , scriptoriumResponse.Data , fingerprintRes.Data);
                        //var edmResult = await _mediator.Send(signRequestEDMService, cancellationToken);
                        //if (!edmResult.IsSuccess)
                        //{
                        //    apiResult.IsSuccess = false;
                        //    apiResult.message.Add("سرویس EDM با خطا مواجه شد ");
                        //    apiResult.message.AddRange(edmResult.message);
                        //    return apiResult;
                        //}
                        reportViewModel.Data = generatedReport.Data;
                        reportViewModel.ContentType = generatedReport.ContentType;
                        reportViewModel.FileName = generatedReport.FileName;
                        apiResult.Data = reportViewModel;

                    }
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.NotFound;
                    apiResult.message.Add("وضعیت گواهی امضا ، تایید شده نمیباشد.");
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("گواهی امضا مربوطه پیدا نشد");
            }
            return apiResult;
        }

    }
}
