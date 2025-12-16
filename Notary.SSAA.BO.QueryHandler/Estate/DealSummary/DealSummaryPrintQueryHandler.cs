using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.QueryHandler.Estate.DealSummary.Handlers
{
    public class DealSummaryPrintQueryHandler : BaseQueryHandler<DealSummaryPrintQuery, ApiResult<DealSummaryPrintViewModel>>
    {
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;

        public DealSummaryPrintQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryRepository dealSummaryRepository, IWebHostEnvironment webHostEnvironment)
            : base(mediator, userService)
        {

            _dealSummaryRepository = dealSummaryRepository;  
            _webHostEnvironment = webHostEnvironment;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(DealSummaryPrintQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DealSummaryPrintViewModel>> RunAsync(DealSummaryPrintQuery request, CancellationToken cancellationToken)
        {

           
            ApiResult<DealSummaryPrintViewModel> apiResult = new();

            var dealSummary = await _dealSummaryRepository.GetDealSummaryById(request.DealSummaryId, cancellationToken);


            if (dealSummary != null)
            {
                var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { dealSummary.ScriptoriumId }, cancellationToken);
                var geolocation = await GetGeoLocations(dealSummary, cancellationToken);
                var measurementUnittypes = await GetMeasurementUnitTypes(dealSummary, cancellationToken);
                StiReport report = new();


                var contentRootPath = _webHostEnvironment.ContentRootPath + "\\Content";                
                var reportName = contentRootPath + "\\Reports\\Estate\\DealSummary\\DealSummaryPrintReport.mrt";
                report.Load(reportName);
                var reportData = new
                {
                    DealSummaryNo = dealSummary.DealNo,
                    DealSummaryDate = dealSummary.DealDate,
                    DealSummaryDoneDate = dealSummary.TransactionDate,
                    DealSummaryUniqueNo = dealSummary.No,
                    Amount = dealSummary.Amount.HasValue ? dealSummary.Amount.Value.ToString() + " " + (!string.IsNullOrWhiteSpace(dealSummary.AmountUnitId) ? measurementUnittypes.MesurementUnitTypeList.Where(m => m.Id == dealSummary.AmountUnitId).First().Name : "") : "",
                    Duration = dealSummary.Duration.HasValue ? dealSummary.Duration.Value.ToString() + " " + (!string.IsNullOrWhiteSpace(dealSummary.TimeUnitId) ? measurementUnittypes.MesurementUnitTypeList.Where(m => m.Id == dealSummary.TimeUnitId).First().Name : "") : "",
                    dealSummary.Description,
                    dealSummary.RemoveRestrictionNo,
                    dealSummary.RemoveRestrictionDate,
                    ExordiumName = GetExordiumTitle(dealSummary, scriptorium.ScriptoriumList[0]),
                    RemoveRestrictionType = dealSummary.UnrestrictionType != null ? dealSummary.UnrestrictionType.Title : "",
                    DealSummaryText = GetDealSummaryText(dealSummary, scriptorium.ScriptoriumList[0], geolocation.GeolocationList.Count > 0 ? geolocation.GeolocationList[0].Name : ""),
                    DealSummarySignText = GetDigitalSignatureText(dealSummary),
                    AttachText = dealSummary.AttachedText,
                    IsRestricted = dealSummary.DealSummaryTransferType.Isrestricted

                };
                List<dynamic> persons = new();
                foreach (Domain.Entities.DealSummaryPerson person in dealSummary.DealSummaryPeople)
                {
                    persons.Add(new
                    {
                        RelationType = dealSummary.DealSummaryTransferType.Isrestricted == "1" && person.RelationType.Code == "100" ? "مالک" : person.RelationType.Title,
                        PersonName = person.Name + " " + person.Family,
                        DealCase = GetOwnershipText(dealSummary, person),
                        PostCode = person.PostalCode,
                        person.FatherName,
                        person.IdentityNo,
                        NationalCode = person.NationalityCode,
                        IssuePlace = person.IssuePlaceId.HasValue ? geolocation.GeolocationList.Where(e => e.Id == person.IssuePlaceId.Value.ToString()).First().Name : ""
                    }
                    );

                }
                report.RegBusinessObject("ReportData", "MasterData", reportData);
                report.RegBusinessObject("ReportDetailData", "DetailData", persons);

                var PdfSettings = new StiPdfExportSettings()
                {
                    ImageResolution = 128,
                    ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg,
                    ImageQuality = 100,
                    EmbeddedFonts = true,
                    StandardPdfFonts = true,
                };

                var generatedReport = StiNetCoreReportResponse.ResponseAsPdf(report, PdfSettings);                
                DealSummaryPrintViewModel reportViewModel = new();
                reportViewModel.Data = generatedReport.Data;
                reportViewModel.ContentType = generatedReport.ContentType;
                reportViewModel.FileName = generatedReport.FileName;
                apiResult.Data = reportViewModel;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("خلاصه معامله مربوطه پیدا نشد");
            }
            return apiResult;
        }
       
        
        public async Task<GetGeolocationByIdViewModel> GetGeoLocations(Domain.Entities.DealSummary dealSummary, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.IssuePlaceId.HasValue).Select(x => x.IssuePlaceId.Value.ToString()).ToList());
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.CityId.HasValue).Select(x => x.CityId.Value.ToString()).ToList());
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.BirthPlaceId.HasValue).Select(x => x.BirthPlaceId.Value.ToString()).ToList());
            list.AddRange(dealSummary.DealSummaryPeople.Where(x => x.NationalityId.HasValue).Select(x => x.NationalityId.Value.ToString()).ToList());
            if (dealSummary.EstateInquiry.GeoLocationId.HasValue)
                list.Add(dealSummary.EstateInquiry.GeoLocationId.Value.ToString());
            if (list.Count == 0) return new GetGeolocationByIdViewModel();
            return await _baseInfoServiceHelper.GetGeoLocationById(list.ToArray(), cancellationToken);

        }
        private async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypes(Domain.Entities.DealSummary dealSummary, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(dealSummary.AmountUnitId)) list.Add(dealSummary.AmountUnitId);
            if (!string.IsNullOrWhiteSpace(dealSummary.TimeUnitId)) list.Add(dealSummary.TimeUnitId);
            if (list.Count == 0) return new MeasurementUnitTypeByIdViewModel();
            return await _baseInfoServiceHelper.GetMeasurementUnitTypeById(list.ToArray(), cancellationToken);

        }
        private static string GetDealSummaryText(Domain.Entities.DealSummary dealSummary, ScriptoriumData scriptorium, string geolocationName)
        {
            string basicAppendant = (dealSummary.EstateInquiry.BasicRemaining == EstateConstant.BooleanConstant.True) ? "دارد" : "ندارد";
            string secondaryAppendant = (dealSummary.EstateInquiry.SecondaryRemaining == EstateConstant.BooleanConstant.True) ? "دارد" : "ندارد";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append( scriptorium.Name + " ، " );
            stringBuilder.AppendLine();

            stringBuilder.Append(" " + "شماره پلاک اصلی:");
            stringBuilder.Append(dealSummary.EstateInquiry.Basic + " ، " );


            stringBuilder.Append(" " + "باقیمانده:");
            stringBuilder.Append(basicAppendant + " ، " );


            stringBuilder.Append(" " + "فرعی:");
            stringBuilder.Append(dealSummary.EstateInquiry.Secondary + " ، " );


            stringBuilder.Append(" " + "باقیمانده:");
            stringBuilder.Append(secondaryAppendant + " ، " );


            stringBuilder.Append(" " + "محل وقوع:");
            stringBuilder.Append(geolocationName + " ، " );


            stringBuilder.Append(" " + "بخش:");
            stringBuilder.Append(dealSummary.EstateInquiry.EstateSection.Title + " ، " );


            stringBuilder.Append(" " + "ناحیه:");
            stringBuilder.Append(dealSummary.EstateInquiry.EstateSubsection.Title + " ، " );


            stringBuilder.Append(" " + "کد پستی:");
            stringBuilder.Append(dealSummary.EstateInquiry.EstatePostalCode + " ، " );

            
            if (dealSummary.EstateInquiry.EstateInquiryType.Code != "2")
            {
                if (string.IsNullOrWhiteSpace(dealSummary.EstateInquiry.ElectronicEstateNoteNo))
                {
                    
                    stringBuilder.Append(" " + "شماره دفتر:");
                    stringBuilder.Append(dealSummary.EstateInquiry.EstateNoteNo + " ، ");

                    
                    stringBuilder.Append(" " + "شماره صفحه:");
                    stringBuilder.Append(dealSummary.EstateInquiry.PageNo + " ، ");

                    
                    stringBuilder.Append(" " + "کد سری دفتر:");
                    stringBuilder.Append(dealSummary.EstateInquiry.EstateSeridaftar.SsaaCode + " ، ");


                    stringBuilder.Append(" " + "ثبت شده در شماره ثبت:");
                    stringBuilder.Append(dealSummary.EstateInquiry.RegisterNo + " ، ");
                }
                else
                {
                    
                    stringBuilder.Append(" " + "شماره صفحه دفتر الکترونیک:");
                    stringBuilder.Append(dealSummary.EstateInquiry.ElectronicEstateNoteNo + " ، ");

                }
            }
            else
            {
                stringBuilder.Append(" " + "شماره اظهارنامه پرونده ملک جاری:");
                stringBuilder.Append(dealSummary.EstateInquiry.EdeclarationNo + " ، ");
            }



            stringBuilder.Append(" " + "زمان ارسال:");
            stringBuilder.Append(dealSummary.SendDate+" "+dealSummary.SendTime + " ، " );


            if (!string.IsNullOrWhiteSpace(dealSummary.EstateInquiry.DocPrintNo))
            {
                stringBuilder.Append(" " + "شماره مستند چاپی:");
                stringBuilder.Append(dealSummary.EstateInquiry.DocPrintNo + " ، ");
            }
            stringBuilder.Append(" " + "نوع معامله:");
            stringBuilder.Append(dealSummary.DealSummaryTransferType.Title);

            return stringBuilder.ToString();
        }

        private static string GetExordiumTitle(Domain.Entities.DealSummary dealSummary,ScriptoriumData scriptorium)
        {
            if (dealSummary.WorkflowStatesId != EstateConstant.DealSummaryStates.NotSended && !string.IsNullOrWhiteSpace(dealSummary.SubjectDn) && dealSummary.DataDigitalSignature != null)
            {
                string[] stringArray = dealSummary.SubjectDn.Split(',');
                bool isKafil = false;
                string name = "";
                string lname = "";

                if (stringArray.Length > 1)
                {
                    var TStr = stringArray.Where(str => str.StartsWith("T=") || str.StartsWith(" T=")).FirstOrDefault();
                    if (TStr != null)
                    {
                        string[] a1 = TStr.Split('=');

                        if (a1[1] == "كفيل" || a1[1] == "کفيل" || a1[1] == "کفیل")
                        {
                            isKafil = true;
                        }
                    }
                    var GStr = stringArray.Where(str => str.StartsWith("G=") || str.StartsWith(" G=")).FirstOrDefault();
                    if (GStr != null)
                    {
                        string[] a2 = GStr.Split('=');

                        name = a2[1];

                    }
                    var SNStr = stringArray.Where(str => str.StartsWith("SN=") || str.StartsWith(" SN=")).FirstOrDefault();
                    if (SNStr != null)
                    {
                        string[] a3 = SNStr.Split('=');

                        lname = a3[1];

                    }
                }
                if (isKafil)
                {
                    string signerTitle = "کفیل " + scriptorium.Name + ": " + name + " " + lname;
                    return signerTitle;

                }
                else
                {
                    string signerTitle = name + " " + lname;
                    return signerTitle;

                }
            }
            return "";
        }

        private static string GetDigitalSignatureText(Domain.Entities.DealSummary dealSummary)
        {
            if (dealSummary.DataDigitalSignature != null)
            {
                Int64 ds = 0;
                if (dealSummary.DataDigitalSignature.Length > 7)
                {
                    ds = BitConverter.ToInt64(dealSummary.DataDigitalSignature, 0);
                }
                else
                {


                    for (int t = 0; t < dealSummary.DataDigitalSignature.Length; t++)
                    {
                        ds += dealSummary.DataDigitalSignature[t];
                    }



                }

                return Math.Abs(ds).ToString();
            }
            return "0";
        }

        private static string GetOwnershipText(Domain.Entities.DealSummary dealSummary, Domain.Entities.DealSummaryPerson person)
        {
            string output ;
            string ownershipType;
            if (dealSummary.EstateOwnershipType.LegacyId == "100")
                ownershipType = "مفروز";
            else if (dealSummary.EstateOwnershipType.LegacyId == "101")
                ownershipType = "مشاع";
            else
                ownershipType = " ";
            if (person.RelationType.Code != "100")
            {
                if (person.SharePart != null && person.ShareTotal != null)
                {
                    output = person.SharePart + " سهم " + ownershipType + " از " + person.ShareTotal + "سهم";
                }
                else
                    if (person.ShareText != null && person.ShareText != string.Empty)
                    output = person.ShareText;
                else
                    output = string.Empty;
            }
            else
            {
                if (person.SharePart != null && person.ShareTotal != null)
                {
                    output = person.SharePart + " سهم " + ownershipType + " از " + person.ShareTotal + "سهم";
                }
                else
                {

                    if (person.ShareText != null && person.ShareText != string.Empty)
                        output = person.ShareText;
                    else
                        output = string.Empty;
                }
            }
            return output;
        }
    }
}
