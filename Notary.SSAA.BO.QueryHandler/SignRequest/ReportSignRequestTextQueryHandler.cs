

using MediatR;
using Microsoft.AspNetCore.Hosting;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal class ReportSignRequestTextQueryHandler : BaseQueryHandler<ReportSignRequestTextQuery, ApiResult<ReportSignRequestViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult<ReportSignRequestViewModel> _apiResult;
        public ReportSignRequestTextQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, IDateTimeService dateTimeService, IWebHostEnvironment webHostEnvironment) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            masterEntity = new();
            _apiResult = new();
        }

        protected override bool HasAccess(ReportSignRequestTextQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }

        protected override async Task<ApiResult<ReportSignRequestViewModel>> RunAsync(ReportSignRequestTextQuery request, CancellationToken cancellationToken)
        {
            if (!request.SignRequestNo.IsNullOrWhiteSpace())
            {
                masterEntity = await _signRequestRepository.SignRequestTracking(request.SignRequestNo, cancellationToken);
            }
            else
            {
                masterEntity = await _signRequestRepository.SignRequestTracking(request.SignRequestId.ToGuid(), _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            }
            if(masterEntity is null)
            {
                _apiResult.IsSuccess = false;
                _apiResult.message.Add("گواهی امضا مربوطه پیدا نشد");
                return _apiResult;
            }
            ReportSignRequestViewModel reportViewModel = new();
            if (masterEntity?.SignRequestFile?.LastFile is null)
            {
                SignRequestTextPrintViewModel reportObject = SignRequestMapper.ToTextPrintViewModel(masterEntity);
                string[] idList = new string[] { masterEntity.ScriptoriumId };
                ScriptoriumInput scriptoriumInput = new ScriptoriumInput(idList);
                ApiResult<ScriptoriumViewModel> scriptoriumResponse = await _mediator.Send(scriptoriumInput, cancellationToken);

                if (scriptoriumResponse.IsSuccess)
                {
                    reportObject.ScriptoriumCode = scriptoriumResponse.Data.ScriptoriumList.First().Code;
                    reportObject.ScriptoriumNo = scriptoriumResponse.Data.ScriptoriumList.First().ScriptoriumNo;
                    reportObject.ScriptoriumLocation = scriptoriumResponse.Data.ScriptoriumList.First().GeoLocationName.Replace("ء", "ی");
                    reportObject.ScriptoriumAddress = " نشانی دفترخانه: " + scriptoriumResponse.Data.ScriptoriumList.First().Address.Replace("ء", "ی");
                    reportObject.ScriptoriumTell = scriptoriumResponse.Data.ScriptoriumList.First().Tel;
                    reportObject.ScriptoriumName = $"دفترخانه اسناد رسمی {reportObject.ScriptoriumNo} {reportObject.ScriptoriumLocation}";
                    reportObject.LegacyCode = $"سردفتر {reportObject.ScriptoriumNo} {reportObject.ScriptoriumLocation} - {scriptoriumResponse.Data.ScriptoriumList.First().ExordiumFullName}";
                }
                else
                {
                    _apiResult.IsSuccess = false;
                    _apiResult.message.Add("سرویس اطلاعات پایه با خطا مواجه شد . ");
                    return _apiResult;
                }
                int row = 0;
               
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

                foreach (var item in masterEntity.SignRequestPeople)
                {
                    PersonEntity Persons = new();
                    if (!notCheckPersonList.Any(x => x == item.Id))
                    {
                        Signables SignRequestPersons = new();
                        SignRequestPersons.FullName = item.Name + " " + item.Family;
                    
                        reportObject.SignablePerson.Add(SignRequestPersons);
                    }


                    if (item.IsOriginal == "1")
                    {
                        row++;
                        Persons.IsOriginalPerson = item.IsOriginal;
                        if (item.PersonType == "1")
                        {
                            if (item.IsIranian == "1")
                            {
                                if (item.SexType == "1")
                                {
                                    Persons.PersonDescription += "خانم " + item.Name + " " + item.Family + " فرزند " + item.FatherName + " به شماره ملی "
                                        + item.NationalNo + " تاریخ تولد" + item.BirthDate + " نشانی " + item.Address + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(masterEntity, item);

                                }
                                else
                                {
                                    Persons.PersonDescription += "آقای " + item.Name + " " + item.Family + " فرزند " + item.FatherName + " شماره ملی "
                                        + item.NationalNo + " تاریخ تولد" + item.BirthDate + " نشانی " + item.Address + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(masterEntity, item);
                                }
                            }
                            else
                            {
                                if (item.SexType == "1")
                                {
                                    Persons.PersonDescription += "خانم " + item.Name + " " + item.Family + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(masterEntity, item);
                                }
                                else
                                {
                                    Persons.PersonDescription += "آقای " + item.Name + " " + item.Family + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(masterEntity, item);
                                }
                            }
                        }
                        else
                        {
                            if (item.IsIranian == "1")
                            {
                                Persons.PersonDescription += item.Name + " " + "به شناسه ملی " + item.NationalNo + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(masterEntity, item);
                            }
                            else
                            {
                                Persons.PersonDescription += item.Name + " " + SignRequestRelatedPersonBusinessRule.FindAgentsOfOriginalPerson(masterEntity, item);
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
                
                StiReport stimulTools = new();
                var reportName = _webHostEnvironment.ContentRootPath + "/Content/Reports/SignRequest/PrintSignRequestText.mrt";

                var report = stimulTools.Load(reportName);
                stimulTools.RegBusinessObject("ONotarySignReqPishEntity", reportObject);

                var PdfSettings = new StiPdfExportSettings()
                {
                    ImageResolution = 256,
                    ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg,
                    ImageQuality = 100,
                    EmbeddedFonts = true,
                    StandardPdfFonts = true,
                    ImageFormat = StiImageFormat.Grayscale
                };
                var generatedReport = StiNetCoreReportResponse.ResponseAsPdf(report, PdfSettings);

                if (generatedReport is not null)
                {

                    reportViewModel.Data = generatedReport.Data;
                    reportViewModel.ContentType = generatedReport.ContentType;
                    reportViewModel.FileName = generatedReport.FileName;
                    _apiResult.Data = reportViewModel;

      
                }
            }
            else
            {
                reportViewModel.Data = masterEntity.SignRequestFile.LastFile;
                reportViewModel.ContentType = "application/pdf";
                reportViewModel.FileName = "Report.pdf";
                _apiResult.Data = reportViewModel;

            }
            return _apiResult;
        }
    }
}
