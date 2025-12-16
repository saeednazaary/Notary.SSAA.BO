
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequestReports;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using System.Collections.Generic;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal class SignRequestGetterReportQueryHandler : BaseQueryHandler<SignRequestStatisticReportQuery, ApiResult<ReportSignRequestViewModel>>
    {
        private ISignRequestRepository _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ApiResult<ReportSignRequestViewModel> _apiResult;
        public SignRequestGetterReportQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, IDateTimeService dateTimeService, IWebHostEnvironment webHostEnvironment) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository;
            _dateTimeService = dateTimeService;
            _webHostEnvironment = webHostEnvironment;
            _apiResult = new();
        }

        protected override bool HasAccess(SignRequestStatisticReportQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar);
        }

        protected override async Task<ApiResult<ReportSignRequestViewModel>> RunAsync(SignRequestStatisticReportQuery request, CancellationToken cancellationToken)
        {
            ReportSignRequestViewModel reportViewModel = new();
            List<SignRequestStatisticRepositoryObject> signRequestStatistic = await _signRequestRepository.generateSignRequsetStatisticReport(request.FromDate, request.ToDate, request.Getter, request.Subjects, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (signRequestStatistic.Count > 0)
            {
                SignRequestStatisticViewModel requestStatisticViewModel = new SignRequestStatisticViewModel()
                {
                    CurrentDate = _dateTimeService.CurrentPersianDate,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    SumSign = signRequestStatistic.Count.ToString(),
                };
                foreach (SignRequestStatisticRepositoryObject item in signRequestStatistic)
                {
                    requestStatisticViewModel.signRequestStatisticItems.Add(SignRequestMapper.ToSignReqStatisticReportViewModel(item));
                }
                StiReport stimulTools = new();
                var reportName = _webHostEnvironment.ContentRootPath + "/Content/Reports/SignRequest/SignRequestStatisticReport.mrt";

                var report = stimulTools.Load(reportName);
                stimulTools.RegBusinessObject("SignRequestStatisticEntity", requestStatisticViewModel);

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
