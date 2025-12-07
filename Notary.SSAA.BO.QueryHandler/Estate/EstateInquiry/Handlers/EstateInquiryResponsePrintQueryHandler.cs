using MediatR;
using Microsoft.AspNetCore.Hosting;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class EstateInquiryResponsePrintQueryHandler : BaseQueryHandler<EstateInquiryResponsePrintQuery, ApiResult<EstateInquiryResponsePrintViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;

        public EstateInquiryResponsePrintQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository, IWebHostEnvironment webHostEnvironment)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository;
            _webHostEnvironment = webHostEnvironment;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(EstateInquiryResponsePrintQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateInquiryResponsePrintViewModel>> RunAsync(EstateInquiryResponsePrintQuery request, CancellationToken cancellationToken)
        {

            ApiResult<EstateInquiryResponsePrintViewModel> apiResult = new();

            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
            
            if (estateInquiry != null)
            {
                if (estateInquiry.WorkflowStatesId != EstateConstant.EstateInquiryStates.ConfirmResponse &&
                    estateInquiry.WorkflowStatesId != EstateConstant.EstateInquiryStates.RejectResponse)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.message.Add("استعلام پاسخ ندارد");
                    return apiResult;
                }
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { estateInquiry.UnitId }, cancellationToken);
                var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { estateInquiry.ScriptoriumId }, cancellationToken);

                StiReport report = new();

                var contentRootPath = _webHostEnvironment.ContentRootPath + "\\Content";
                //StiFontCollection.AddFontFile(contentRootPath + "/Fonts/BNAZANIN.TTF");
                var reportName = contentRootPath + "\\Reports\\Estate\\EstateInquiry\\EstateInquiryResponsePrintReport.mrt";
                report.Load(reportName);
                report.RegBusinessObject("ReportData", "ReportData", new
                {

                    InquiryResponseNo = estateInquiry.ResponseNumber,
                    InquiryResponseDate = estateInquiry.ResponseDate,
                    InquiryDigitalSignNo = GetResponseDigitalSignNo(estateInquiry),
                    InquiryResponseText = estateInquiry.Response,
                    InquiryProvinceName = "اداره کل استان " + unit.UnitList[0].Province,
                    InquiryScriptoriumName = scriptorium.ScriptoriumList[0].Name

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
                //System.IO.File.WriteAllBytes("d:\\report1313.pdf", generatedReport.Data);
                EstateInquiryResponsePrintViewModel reportViewModel = new();
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

        
       
        private static string GetResponseDigitalSignNo(Domain.Entities.EstateInquiry estateInquiry)
        {
            var responseDigitalSignature = estateInquiry.ResponseDigitalsignature;
            if (responseDigitalSignature == null) return string.Empty;
            if (responseDigitalSignature.Length == 0) return string.Empty;
            return Math.Abs(BitConverter.ToInt64(responseDigitalSignature, 0)).ToString();
        }
    }
}
