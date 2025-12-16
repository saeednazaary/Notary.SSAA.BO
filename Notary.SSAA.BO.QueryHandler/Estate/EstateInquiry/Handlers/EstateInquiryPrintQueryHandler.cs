using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text;
using Notary.SSAA.BO.Utilities.Extensions;
using Microsoft.AspNetCore.Hosting;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;


namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class EstateInquiryPrintQueryHandler : BaseQueryHandler<EstateInquiryPrintQuery, ApiResult<EstateInquiryPrintViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;   
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        public EstateInquiryPrintQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository, IWebHostEnvironment webHostEnvironment)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository;
            _webHostEnvironment = webHostEnvironment;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(EstateInquiryPrintQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult<EstateInquiryPrintViewModel>> RunAsync(EstateInquiryPrintQuery request, CancellationToken cancellationToken)
        {
            ApiResult<EstateInquiryPrintViewModel> apiResult = new();
            var scriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            var estateInquiry = await _estateInquiryRepository.GetAsync(x => x.ScriptoriumId == scriptoriumId && x.Id == request.EstateInquiryId.ToGuid(), cancellationToken);           
            if (estateInquiry != null)
            {
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, e => e.EstateInquiryPeople, cancellationToken);
                await _estateInquiryRepository.LoadReferenceAsync(estateInquiry, e => e.EstateSection, cancellationToken);
                await _estateInquiryRepository.LoadReferenceAsync(estateInquiry, e => e.EstateSubsection, cancellationToken);
                await _estateInquiryRepository.LoadReferenceAsync(estateInquiry, e => e.EstateSeridaftar, cancellationToken);
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { estateInquiry.UnitId }, cancellationToken);
                var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { estateInquiry.ScriptoriumId }, cancellationToken);
                GetGeolocationByIdViewModel geolocation = null;
                if (estateInquiry.GeoLocationId.HasValue)
                    geolocation = await _baseInfoServiceHelper.GetGeoLocationById(new string[] { estateInquiry.GeoLocationId.Value.ToString() }, cancellationToken);
                var person = estateInquiry.EstateInquiryPeople.First();
                StiReport report = new();
                var contentRootPath = _webHostEnvironment.ContentRootPath + "\\Content";                
                var reportName = contentRootPath + "\\Reports\\Estate\\EstateInquiry\\EstateInquiryPrintReport.mrt";
                report.Load(reportName);
                report.RegBusinessObject("ReportData", "ReportData", new
                {
                    InquiryNo = estateInquiry.InquiryNo,
                    InquiryDate = estateInquiry.InquiryDate,
                    InquiryUniqueNo = estateInquiry.No,
                    InquiryText = GetEstateInquiryText(estateInquiry, geolocation != null && geolocation.GeolocationList.Count > 0 ? geolocation.GeolocationList[0].Name : ""),
                    InquiryUnitName = "اداره ثبت اسناد و املاک " + unit.UnitList[0].Name,
                    InquiryScriptoriumName = "سردفتر اسناد رسمی " + scriptorium.ScriptoriumList[0].Name + Environment.NewLine + "محل امضای دفتر :",
                    InquiryLastSendTime = (!string.IsNullOrWhiteSpace(estateInquiry.LastSendDate)) ? estateInquiry.LastSendDate + "-" + estateInquiry.LastSendTime : "",
                    InquiryPaymentNo = estateInquiry.InquiryPaymantRefno,
                    InquiryOwnerName = person.Name + " " + person.Family,
                    InquiryOwnerNationalNo = person.NationalityCode,
                    InquiryOwnerIdentityNo = person.IdentityNo,
                });
                var PdfSettings = new StiPdfExportSettings()
                {
                    ImageResolution = 128,
                    ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg,
                    ImageQuality = 100,
                    EmbeddedFonts = true,
                    StandardPdfFonts = true,
                };

                var generatedReport = StiNetCoreReportResponse.ResponseAsPdf(report, PdfSettings);                
                EstateInquiryPrintViewModel reportViewModel = new();
                reportViewModel.Data = generatedReport.Data;
                reportViewModel.ContentType = generatedReport.ContentType;
                reportViewModel.FileName = generatedReport.FileName;
                apiResult.Data = reportViewModel;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام ملک مربوطه پیدا نشد");
            }
            return apiResult;
        }
      
        private static string GetEstateInquiryText(Domain.Entities.EstateInquiry estateInquiry, string geolocationName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (estateInquiry.EstateInquiryTypeId == "1")
            {
                if (string.IsNullOrEmpty(estateInquiry.ElectronicEstateNoteNo))
                {
                    stringBuilder.Append("  " + "چون یکی از مالکین ملک به شماره کد پستی:");
                    stringBuilder.Append(estateInquiry.EstatePostalCode + "  ");
                    stringBuilder.Append("  " + "واقع در شهر:");
                    stringBuilder.Append(geolocationName + "  ");
                    stringBuilder.Append(" ، " + "بخش:");
                    stringBuilder.Append(estateInquiry.EstateSection.Title + "  ");
                    stringBuilder.Append(" ، " + "ناحیه:");
                    stringBuilder.Append(estateInquiry.EstateSubsection.Title + "  ");
                    stringBuilder.Append("  " + "به مساحت:");
                    stringBuilder.Append(estateInquiry.Area.Value.ToString() + "  ");
                    stringBuilder.Append("  " + "دارای پلاک اصلی:");
                    stringBuilder.Append(estateInquiry.Basic + "  ");
                    stringBuilder.Append("  " + " و فرعی:");
                    stringBuilder.Append(estateInquiry.Secondary + "  ");
                    stringBuilder.Append("  " + "ثبت شده در شماره ثبت:");
                    stringBuilder.Append(estateInquiry.RegisterNo + "  ");
                    stringBuilder.Append(" ، " + "شماره دفتر:");
                    stringBuilder.Append(estateInquiry.EstateNoteNo + "  ");
                    stringBuilder.Append("  " + "و شماره صفحه:");
                    stringBuilder.Append(estateInquiry.PageNo + "  ");
                    stringBuilder.Append("  " + "به شماره مستند چاپی:");
                    stringBuilder.Append(estateInquiry.DocPrintNo + "  ");
                    stringBuilder.Append("  " + "در دفتر سری:");
                    stringBuilder.Append(estateInquiry.EstateSeridaftar.Title + "  ");
                    stringBuilder.Append("  " + "قصد دارد معامله یا تفکیک انجام دهد خواهشمند است  دستور فرمایید جریان ثبتی ملک و عدم بازداشت و بند(ز)  آن را اعلام فرمایند");
                }
                else
                {
                    stringBuilder.Append("  " + "چون یکی از مالکین ملک به شماره کد پستی:");
                    stringBuilder.Append(estateInquiry.EstatePostalCode + "  ");
                    stringBuilder.Append("  " + "واقع در شهر:");
                    stringBuilder.Append(geolocationName + "  ");
                    stringBuilder.Append(" ، " + "بخش:");
                    stringBuilder.Append(estateInquiry.EstateSection.Title + "  ");
                    stringBuilder.Append(" ، " + "ناحیه:");
                    stringBuilder.Append(estateInquiry.EstateSubsection.Title + "  ");
                    stringBuilder.Append("  " + "به مساحت:");
                    stringBuilder.Append(estateInquiry.Area.Value.ToString() + "  ");
                    stringBuilder.Append("  " + "دارای پلاک اصلی:");
                    stringBuilder.Append(estateInquiry.Basic + "  ");
                    stringBuilder.Append("  " + " و فرعی:");
                    stringBuilder.Append(estateInquiry.Secondary + "  ");
                    stringBuilder.Append("  " + "ثبت شده در شماره صفحه دفتر الکترونیک:");
                    stringBuilder.Append(estateInquiry.ElectronicEstateNoteNo + "  ");
                    stringBuilder.Append("  " + "به شماره مستند چاپی:");
                    stringBuilder.Append(estateInquiry.DocPrintNo + "  ");
                    stringBuilder.Append("  " + "قصد دارد معامله یا تفکیک انجام دهد خواهشمند است  دستور فرمایید جریان ثبتی ملک و عدم بازداشت و بند(ز)  آن را اعلام فرمایند");
                }
            }
            else
            {
                stringBuilder.Append("  " + "چون یکی از مالکین ملک به شماره کد پستی:");
                stringBuilder.Append(estateInquiry.EstatePostalCode + "  ");
                stringBuilder.Append("  " + "واقع در شهر:");
                stringBuilder.Append(geolocationName + "  ");
                stringBuilder.Append(" ، " + "بخش:");
                stringBuilder.Append(estateInquiry.EstateSection.Title + "  ");
                stringBuilder.Append(" ، " + "ناحیه:");
                stringBuilder.Append(estateInquiry.EstateSubsection.Title + "  ");
                stringBuilder.Append("  " + "به مساحت:");
                stringBuilder.Append(estateInquiry.Area.Value.ToString() + "  ");
                stringBuilder.Append("  " + "دارای پلاک اصلی:");
                stringBuilder.Append(estateInquiry.Basic + "  ");
                stringBuilder.Append("  " + " و فرعی:");
                stringBuilder.Append(estateInquiry.Secondary + "  ");
                stringBuilder.Append("  " + "ثبت شده در پرونده ملک جاری به شماره اظهارنامه:");
                stringBuilder.Append(estateInquiry.EdeclarationNo + "  ");               
                stringBuilder.Append("  " + "قصد دارد معامله یا تفکیک انجام دهد خواهشمند است  دستور فرمایید جریان ثبتی ملک و عدم بازداشت و بند(ز)  آن را اعلام فرمایند");
            }
            return stringBuilder.ToString();
        }        
    }
}
