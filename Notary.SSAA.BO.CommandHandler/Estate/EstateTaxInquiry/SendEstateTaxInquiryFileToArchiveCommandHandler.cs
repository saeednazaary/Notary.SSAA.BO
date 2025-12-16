using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class SendEstateTaxInquiryFileToArchiveCommandHandler : BaseCommandHandler<SendEstateTaxInquiryFilesToArchiveCommand, ApiResult>
    {
        private protected Domain.Entities.EstateTaxInquiry masterEntity;
        private protected ApiResult<EstateTaxInquiryViewModel> apiResult;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        SSAA.BO.Domain.Entities.EstateTaxInquiry prevEstateTaxInquiry = null;
        private readonly IConfiguration _configuration;
        public SendEstateTaxInquiryFileToArchiveCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IDateTimeService dateTimeService, IWorkfolwStateRepository workfolwStateRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            _workfolwStateRepository = workfolwStateRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _dateTimeService = dateTimeService;
            masterEntity = new();
            apiResult = new();
        }
        protected override bool HasAccess(SendEstateTaxInquiryFilesToArchiveCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(SendEstateTaxInquiryFilesToArchiveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool bv = false;
                masterEntity = await _estateTaxInquiryRepository.GetByIdAsync(cancellationToken, request.EstateTaxInquiryId.ToGuid());
                if (masterEntity.IsActive == EstateConstant.BooleanConstant.False)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.message.Add("استعلام  مالیاتی غیر فعال می باشد و امکان اجرای عملیات روی آن وجود ندارد");
                    return apiResult;
                }
                await _estateTaxInquiryRepository.LoadCollectionAsync(masterEntity, x => x.EstateTaxInquiryFiles, cancellationToken);
                if (masterEntity.WorkflowStates.State == "40")
                {
                    if (masterEntity.CertificateFile != null)
                    {
                        var pdf = masterEntity.EstateTaxInquiryFiles.Where(x => x.AttachmentNo == masterEntity.No + "_1" && x.FileExtention == "pdf").FirstOrDefault();
                        if (pdf == null)
                        {
                            pdf = new Domain.Entities.EstateTaxInquiryFile();
                            pdf.EstateTaxInquiryId = masterEntity.Id;
                            pdf.ScriptoriumId = masterEntity.ScriptoriumId;
                            pdf.Ilm = "1";
                            pdf.AttachmentNo = masterEntity.No + "_1";
                            pdf.FileContent = masterEntity.CertificateFile;
                            pdf.FileExtention = "pdf";
                            pdf.Timestamp = 1;
                            pdf.CreateDate = _dateTimeService.CurrentPersianDate;
                            pdf.CreateTime = _dateTimeService.CurrentTime;
                            masterEntity.EstateTaxInquiryFiles.Add(pdf);
                        }
                        else
                        {
                            pdf.ChangeState = "";
                            pdf.FileContent = masterEntity.CertificateFile;

                        }
                    }
                    else
                    {
                        var pdf = masterEntity.EstateTaxInquiryFiles.Where(x => x.AttachmentNo == masterEntity.No + "_1" && x.FileExtention == "pdf").FirstOrDefault();
                        if (pdf != null)
                        {
                            if (string.IsNullOrWhiteSpace(pdf.ArchiveMediaFileId))
                            {
                                masterEntity.EstateTaxInquiryFiles.Remove(pdf);
                                bv = true;
                            }
                            else
                                pdf.ChangeState = "3";
                        }
                    }
                }
                foreach (var file in masterEntity.EstateTaxInquiryFiles)
                {
                    if (file.FileContent != null && file.ChangeState != "3")
                    {
                        var result = await UploadFileToArchive(file, cancellationToken);
                        if (result != null && result.IsSuccess)
                        {
                            file.FileContent = null;
                            file.ArchiveMediaFileId = result.MediaId;
                            bv = true;
                        }
                    }
                    else if (file.ChangeState == "3")
                    {
                        var removeResult = await RemoveFileFromArchive(file, cancellationToken);
                        if (removeResult)
                        {
                            file.ChangeState = "4";
                        }
                    }
                }
                var removedFiles = masterEntity.EstateTaxInquiryFiles.Where(f => f.ChangeState == "4").ToList();
                foreach (var file in removedFiles)
                {
                    masterEntity.EstateTaxInquiryFiles.Remove(file);
                    bv = true;
                }
            
                if (bv)
                    await _estateTaxInquiryRepository.UpdateAsync(masterEntity, cancellationToken);

            }
            catch (Exception)
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("خطا در ارسال فایل های استعلام مالیاتی به ساماه آرشیو رخ داد ");

            }

            return apiResult;
        }

        private async Task<MediaUpload> UploadFileToArchive(Domain.Entities.EstateTaxInquiryFile taxInquiryFile, CancellationToken cancellationToken)
        {
            
            var baseInfoUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl");
            if (string.IsNullOrWhiteSpace(taxInquiryFile.ArchiveMediaFileId))
            {
                string docId = null;
                using (MultipartFormDataContent formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(""), "Media.MediaId");
                    formData.Add(new StringContent(""), "Media.RowNo");
                    formData.Add(new StringContent(""), "DocId");
                    formData.Add(new StringContent(""), "DocDescription");
                    formData.Add(new StringContent(""), "DocCreateDate");
                    formData.Add(new StringContent(""), "DocOtherData");
                    formData.Add(new StringContent(""), "Media.MediaFile");
                    formData.Add(new StringContent(""), "Media.MediaThumbNail");
                    formData.Add(new StringContent(""), "Media.MediaExtension");
                    formData.Add(new StringContent(""), "Media.FileName");
                    formData.Add(new StringContent(""), "Media.FileType");
                    formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentTitle) ? taxInquiryFile.AttachmentTitle : "-"), "DocTitle");
                    formData.Add(new StringContent(taxInquiryFile.EstateTaxInquiryId.ToString()), "RelatedRecordIds");
                    formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentNo) ? taxInquiryFile.AttachmentNo : "-"), "DocNo");
                    formData.Add(new StringContent("0910"), "DocType");
                    formData.Add(new StringContent("9007"), "ClientId");
                    var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(baseInfoUrl + "Media/Upload", _userService.UserApplicationContext.Token, formData), cancellationToken);
                    if (apiResult.IsSuccess && !string.IsNullOrWhiteSpace(apiResult.AttachmentId))
                    {
                        docId = apiResult.AttachmentId;
                    }
                }
                if (!string.IsNullOrWhiteSpace(docId))
                {
                    using (MultipartFormDataContent formData = new MultipartFormDataContent())
                    {
                        var mediaFileContent = new ByteArrayContent(taxInquiryFile.FileContent);
                        formData.Add(mediaFileContent, "Media.MediaFile", "TaxInquiryFile");
                        formData.Add(new StringContent("null"), "Media.MediaThumbNail");
                        formData.Add(new StringContent("null"), "Media.MediaId");
                        formData.Add(new StringContent(taxInquiryFile.FileExtention), "Media.MediaExtension");
                        formData.Add(new StringContent("null"), "Media.RowNo");
                        formData.Add(new StringContent("TaxInquiryFile"), "Media.FileName");
                        formData.Add(new StringContent("image/png"), "Media.FileType");
                        formData.Add(new StringContent(docId), "DocId");
                        formData.Add(new StringContent("0910"), "DocType");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentNo) ? taxInquiryFile.AttachmentNo : "-"), "DocNo");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentTitle) ? taxInquiryFile.AttachmentTitle : "-"), "DocTitle");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDate) ? taxInquiryFile.AttachmentDate : "-"), "DocCreateDate");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDesc) ? taxInquiryFile.AttachmentDesc : "-"), "DocDescription");
                        formData.Add(new StringContent("-"), "DocOtherData");
                        formData.Add(new StringContent("9007"), "ClientId");
                        formData.Add(new StringContent(taxInquiryFile.EstateTaxInquiryId.ToString()), "RelatedRecordIds");
                        var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(baseInfoUrl + "Media/Upload", _userService.UserApplicationContext.Token, formData), cancellationToken);
                        if (apiResult.IsSuccess && !string.IsNullOrWhiteSpace(apiResult.AttachmentId))
                        {
                            return apiResult;
                        }
                    }
                }
            }
            else
            {
                var archiveFile = await DownloadFileFromArchive(taxInquiryFile, cancellationToken);
                if (archiveFile != null)
                {
                    using (MultipartFormDataContent formData = new MultipartFormDataContent())
                    {
                        var mediaFileContent = new ByteArrayContent(taxInquiryFile.FileContent);
                        formData.Add(mediaFileContent, "Media.MediaFile", "TaxInquiryFile");
                        formData.Add(new StringContent("null"), "Media.MediaThumbNail");
                        formData.Add(new StringContent(taxInquiryFile.ArchiveMediaFileId), "Media.MediaId");
                        formData.Add(new StringContent(taxInquiryFile.FileExtention), "Media.MediaExtension");
                        formData.Add(new StringContent("null"), "Media.RowNo");
                        formData.Add(new StringContent("TaxInquiryFile"), "Media.FileName");
                        formData.Add(new StringContent("image/png"), "Media.FileType");
                        formData.Add(new StringContent(archiveFile.AttachmentID), "DocId");
                        formData.Add(new StringContent("0910"), "DocType");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentNo) ? taxInquiryFile.AttachmentNo : "-"), "DocNo");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentTitle) ? taxInquiryFile.AttachmentTitle : "-"), "DocTitle");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDate) ? taxInquiryFile.AttachmentDate : "-"), "DocCreateDate");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDesc) ? taxInquiryFile.AttachmentDesc : "-"), "DocDescription");
                        formData.Add(new StringContent("-"), "DocOtherData");
                        formData.Add(new StringContent("9007"), "ClientId");
                        formData.Add(new StringContent(taxInquiryFile.EstateTaxInquiryId.ToString()), "RelatedRecordIds");
                        var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(baseInfoUrl + "Media/Upload", _userService.UserApplicationContext.Token, formData), cancellationToken);
                        if (apiResult.IsSuccess && !string.IsNullOrWhiteSpace(apiResult.AttachmentId))
                        {
                            return apiResult;
                        }
                    }
                }
            }
            return null;
        }
        private async Task<DownloadAttachmentViewModel> DownloadFileFromArchive(Domain.Entities.EstateTaxInquiryFile taxInquiryFile, CancellationToken cancellationToken)
        {
            var downloadAttachmentInput = new DownloadMediaServiceInput
            {
                AttachmentFileId = taxInquiryFile.ArchiveMediaFileId,
                AttachmentClientId = "9007",
                AttachmentRelatedRecordId = taxInquiryFile.EstateTaxInquiryId.ToString(),
                AttachmentTypeId = "0910"
            };
            var r = await _mediator.Send(downloadAttachmentInput, cancellationToken);
            if (r.IsSuccess && r.Data != null)
                return r.Data;
            return null;
        }
        private async Task<bool> RemoveFileFromArchive(Domain.Entities.EstateTaxInquiryFile taxInquiryFile, CancellationToken cancellationToken)
        {
            var archiveFile = await DownloadFileFromArchive(taxInquiryFile, cancellationToken);
            if (archiveFile != null)
            {
                var removeAttachmentInput = new MediaRemoveServiceInput
                {
                    MediaId = taxInquiryFile.ArchiveMediaFileId,
                    ClientId = "9007",
                    DocId = archiveFile.AttachmentID
                };
                var r = await _mediator.Send(removeAttachmentInput, cancellationToken);
                return r.IsSuccess;
            }
            return false;
        }

    }
}
