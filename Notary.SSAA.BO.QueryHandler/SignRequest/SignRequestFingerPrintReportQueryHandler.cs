
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequestReports;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal class SignRequestFingerPrintReportQueryHandler : BaseQueryHandler<SignRequestFingerPrintReportQuery, ApiResult<ReportSignRequestViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult<ReportSignRequestViewModel> _apiResult;
        private readonly IDateTimeService _dateTimeService;
        public SignRequestFingerPrintReportQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, IDateTimeService dateTimeService, IWebHostEnvironment webHostEnvironment) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _apiResult = new();
            _dateTimeService = dateTimeService;
        }

        protected override bool HasAccess(SignRequestFingerPrintReportQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected override async Task<ApiResult<ReportSignRequestViewModel>> RunAsync(SignRequestFingerPrintReportQuery request, CancellationToken cancellationToken)
        {
            ReportSignRequestViewModel reportViewModel = new();
            List<SignRequestFingerPrintRepositoryObject> signRequestFingerList = await _signRequestRepository.getFingerPrintReport(request.signRequestId.ToGuid(), cancellationToken);

            if(signRequestFingerList.Count > 0)
            {
                string[] idList = new string[] { signRequestFingerList.First().scriptoriumId };
                ScriptoriumInput scriptoriumInput = new ScriptoriumInput(idList);
                ApiResult<ScriptoriumViewModel> scriptoriumResponse = await _mediator.Send(scriptoriumInput, cancellationToken);
                string scriptoriumName = string.Empty;
                if (scriptoriumResponse.IsSuccess)
                {
                    scriptoriumName = scriptoriumResponse.Data?.ScriptoriumList?.FirstOrDefault()?.Name;
                }
                var fingerPrintViewModel = new SignRequestFingerPrintViewModel()
                {
                    CurrentDate = _dateTimeService.CurrentPersianDate,
                    ScriptoriumName = scriptoriumName,
                };
                foreach (SignRequestFingerPrintRepositoryObject item in signRequestFingerList)
                {
                    fingerPrintViewModel.signRequestFingerItems.Add(SignRequestMapper.ToSignReqFingerReportViewModel(item));
                }
                StiReport stimulTools = new();
                var reportName = _webHostEnvironment.ContentRootPath + "/Content/Reports/SignRequest/SignRequestFingerReport.mrt";

                var report = stimulTools.Load(reportName);
                stimulTools.RegBusinessObject("SignRequestFingerEntity", fingerPrintViewModel);

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
                    _apiResult.IsSuccess = true;
                    _apiResult.Data = reportViewModel;


                }
                else
                {
                    _apiResult.IsSuccess = false;
                    _apiResult.Data = null;
                    _apiResult.message.Add("دریافت گزارش با خطا مواجه شد.");
                }

            }
            else
            {
                _apiResult.IsSuccess = false;
                _apiResult.Data = null;
                _apiResult.message.Add("در این بازه زمانی گواهی امضا یافت نشد.");
            }
            return _apiResult;
        }
    }
}
