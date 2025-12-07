using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Coordinator;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.Queries.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSix;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateArticle6Inquiry
{
    public class SendArticle6InquiryCommandHandler : BaseCommandHandler<SendElzamArtSixCommand, ApiResult>
    {

        private IElzamArtSixRepository _ElzamArtSixRepository;
        IDateTimeService _dateTimeService;
        private SsrArticle6Inq masterEntity;
        private ApiResult<ElzamArtSixViewModel> apiResult;
        private readonly ILogger _logger;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        private readonly IConfiguration _configuration;
        private readonly ConfigurationParameterHelper _configurationParameterHelper;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        public SendArticle6InquiryCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IDateTimeService dateTimeService
        , IElzamArtSixRepository ElzamArtSixRepository, IConfiguration configuration, IRepository<ConfigurationParameter> configurationParameterRepository) : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _logger = logger;
            _ElzamArtSixRepository = ElzamArtSixRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixRepository));
            _dateTimeService = dateTimeService;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            _configuration = configuration;
            _configurationParameterRepository = configurationParameterRepository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
        }

        protected override bool HasAccess(SendElzamArtSixCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar);
        }

        protected override async Task<ApiResult> ExecuteAsync(SendElzamArtSixCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            masterEntity = await _ElzamArtSixRepository
                                 .Table
                                 .Include(x => x.EstateSection)
                                 .Include(x => x.EstateSubsection)
                                 .Include(x => x.EstateUsing)
                                 .Include(x => x.EstateType)
                                 .Include(x => x.Province)
                                 .Include(x => x.County)
                                 .Include(x => x.SsrArticle6InqPeople)
                                 .Include(x => x.SsrArticle6InqReceivers)
                                 .ThenInclude(y => y.SsrArticle6Organ)
                                 .Where(x => x.Id == request.Id.ToGuid())
                                 .FirstOrDefaultAsync(cancellationToken);
            if (masterEntity != null)
            {
                if (masterEntity.WorkflowStatesId == EstateConstant.EstateElzamSixArtInquiryStates.NotSended)
                {
                    var RealEnviromentEnabled = await _configurationParameterHelper.GetConfigurationParameter("Estate_Article6_Real_Enviroment_Enabled", cancellationToken);
                    if (RealEnviromentEnabled == "false")
                    {
                        await SendForTestEnviroment(cancellationToken);
                    }
                    else
                    {
                        var unit = await _baseInfoServiceHelper.GetUnitById([masterEntity.EstateUnitId], cancellationToken);
                        var unitLegacyId = unit != null && unit.UnitList.Count > 0 ? unit.UnitList[0].LegacyId : string.Empty;
                        var unitNo = unit != null && unit.UnitList.Count > 0 ? unit.UnitList[0].No : string.Empty;
                        var cadastrServiceInput = new GetEstateGeoJsonServiceInput()
                        {
                            PlakOrginal = Convert.ToInt32(masterEntity.EstateMainPlaque),
                            PlaqkSub = Convert.ToInt32(masterEntity.EstateSecondaryPlaque),
                            SectionCode = masterEntity.EstateSection.SsaaCode,
                            SubSectionCode = masterEntity.EstateSubsection.SsaaCode,
                            UnitId = unitLegacyId
                        };
                        var cadastrServiceOutput = await _mediator.Send(cadastrServiceInput, cancellationToken);
                        if (cadastrServiceOutput.IsSuccess && cadastrServiceOutput.Data != null && cadastrServiceOutput.Data.IsSuccess)
                        {
                            EstateElzamSixArtInquiryGeometry geometry = null;
                            if (cadastrServiceOutput.Data.Data != null && !string.IsNullOrWhiteSpace(cadastrServiceOutput.Data.Data.GeoJsonMap))
                                geometry = JsonConvert.DeserializeObject<EstateElzamSixArtInquiryGeometry>(cadastrServiceOutput.Data.Data.GeoJsonMap);
                            if (geometry != null)
                            {
                                var Seller = masterEntity.SsrArticle6InqPeople.Where(x => x.RelationType == "100").First();
                                var Buyer = masterEntity.SsrArticle6InqPeople.Where(x => x.RelationType == "101").First();
                                EstateElzamSixArtInquiryServiceInput sendServiceInput = new();

                                sendServiceInput.SendData = new EstateElzamSixArtInquiryServiceInputData
                                {
                                    ProvinceCode = Convert.ToInt32(masterEntity.Province.Code),
                                    CountyCode = Convert.ToInt32(masterEntity.County.Code),
                                    UnitNumberSdd = unitNo,
                                    AreaT = masterEntity.EstateArea,
                                    PartIdSdd = masterEntity.EstateSection.SsaaCode,
                                    CountyIdSdd = masterEntity.EstateSubsection.SsaaCode,
                                    FracShare = Seller.SharePart.ToString(),
                                    TotalShare = Seller.ShareTotal.ToString(),
                                    Geometry = geometry,
                                    OriginalNumberSdd = masterEntity.EstateMainPlaque,
                                    OwnerType = Convert.ToInt32(masterEntity.EstateType.Id),
                                    PostalCode = masterEntity.EstatePostCode,
                                    RequestDate = _dateTimeService.CurrentPersianDate,
                                    RequestType = 1,
                                    SabtTrackingCode = masterEntity.No,
                                    SubNumberSdd = masterEntity.EstateSecondaryPlaque,
                                    UserCode = Convert.ToInt32(masterEntity.EstateUsing.Code),
                                    SellerNationalCode = Seller.NationalityCode,
                                    BuyerNationalCode = Buyer.NationalityCode

                                };
                                foreach (var receiver in masterEntity.SsrArticle6InqReceivers)
                                {
                                    sendServiceInput.SendData.InqOrgan.Add(receiver.SsrArticle6Organ.Code);
                                }

                                var attachments = await GetAttachments(cancellationToken);
                                if (!apiResult.IsSuccess)
                                    return apiResult;
                                byte[] zipFile = null;
                                if (attachments.Count > 0)
                                {
                                    zipFile = await ZipHelper.CreateZipFile(attachments, cancellationToken);
                                    if (zipFile != null)
                                    {
                                        sendServiceInput.SendData.File = new EstateElzamSixArtInquiryFile()
                                        {
                                            Name = "attachments",
                                            Size = zipFile.Length,
                                            Type = "zip",
                                            Value = Convert.ToBase64String(zipFile)
                                        };
                                    }
                                }
                                var sendServiceOutput = await _mediator.Send(sendServiceInput, cancellationToken);
                                if (sendServiceOutput.IsSuccess && sendServiceOutput.Data != null && sendServiceOutput.Data.ResCode == 1)
                                {
                                    if (zipFile != null)
                                    {
                                        masterEntity.Attachments = zipFile;
                                    }
                                    masterEntity.TrackingCode = sendServiceOutput.Data.Data.TrackingCode;
                                    masterEntity.SendDate = _dateTimeService.CurrentPersianDate;
                                    masterEntity.SendTime = _dateTimeService.CurrentTime;
                                    masterEntity.EstateMap = cadastrServiceOutput.Data.Data.GeoJsonMap;
                                    masterEntity.WorkflowStatesId = EstateConstant.EstateElzamSixArtInquiryStates.Sended;
                                    await _ElzamArtSixRepository.UpdateAsync(masterEntity, cancellationToken);
                                    apiResult.message.Add("استعلام با موفقیت ارسال شد");
                                }
                                else
                                {
                                    apiResult.IsSuccess = false;
                                    apiResult.message.Add(sendServiceOutput.message.Count > 0 ? sendServiceOutput.message[0] : "خطا در ارسال استعلام رخ داد");
                                }
                            }
                            else
                            {
                                apiResult.IsSuccess = false;
                                apiResult.message.Add("خطا در آماده سازی مختصات ملک دریافت شده از کاداستر ، جهت ارسال رخ داد ");
                            }

                           
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("استعلام به دلیل عدم دریافت مختصات ملک مربوطه از سامانه کاداستر ارسال نشد");
                        }
                    }
                    #region Get
                    if (apiResult.IsSuccess)
                    {
                        ApiResult<ElzamArtSixViewModel> getResponse = await _mediator.Send(new GetElzamArtSixByIdQuery(masterEntity.Id.ToString()), cancellationToken);
                        if (getResponse.IsSuccess)
                        {
                            if (getResponse.Data != null)
                            {
                                apiResult.Data = getResponse.Data.Adapt<ElzamArtSixViewModel>();
                                apiResult.message.Add(message);
                            }
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = getResponse.statusCode;
                            apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");
                            apiResult.message = getResponse.message;
                        }
                    }
                    #endregion
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("استعلام قبلا ارسال شده است");
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("استعلام یافت نشد");
            }            
            return apiResult;
        }

        private async Task<List<FileToZip>> GetAttachments(CancellationToken cancellationToken)
        {
            var result = new List<FileToZip>();
            var relatedRecordId = masterEntity.Id.ToString();

            var attachmentInput = new LoadAttachmentServiceInput
            {
                ClientId = "9122",
                RelatedRecordIds = new List<string> { relatedRecordId }
            };
            var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);
            if (attachmentResult == null || attachmentResult.Data == null || attachmentResult.Data.AttachmentViewModels == null)
                return result;

            int i = 1;
            foreach (var item in attachmentResult.Data.AttachmentViewModels)
            {
                if (item?.Medias == null || item.DocTypeId != "0935")
                    continue;
                foreach (var foundMedia in item.Medias)
                {
                    var downloadAttachmentInput = new DownloadMediaServiceInput
                    {
                        AttachmentFileId = foundMedia.MediaId,
                        AttachmentClientId = attachmentInput.ClientId,
                        AttachmentRelatedRecordId = masterEntity.Id.ToString(),
                        AttachmentTypeId = "0935"
                    };
                    var downloadAttachmentOutput = await _mediator.Send(downloadAttachmentInput, cancellationToken);
                    if (downloadAttachmentOutput?.IsSuccess == true && downloadAttachmentOutput.Data != null)
                    {
                        result.Add(new FileToZip() { Content = downloadAttachmentOutput.Data.MediaFile, FileName = "file" + i.ToString() + "." + downloadAttachmentOutput.Data.MediaExtension });
                    }
                    i++;
                }
            }
            return result;
        }

        private async Task<ApiResult> SendForTestEnviroment(CancellationToken cancellationToken)
        {
            EstateElzamSixArtInquiryGeometry gm = JsonConvert.DeserializeObject<EstateElzamSixArtInquiryGeometry>(masterEntity.EstateMap);
            var unit = await _baseInfoServiceHelper.GetUnitById([masterEntity.EstateUnitId], cancellationToken);
            var unitLegacyId = unit != null && unit.UnitList.Count > 0 ? unit.UnitList[0].LegacyId : string.Empty;
            var unitNo = unit != null && unit.UnitList.Count > 0 ? unit.UnitList[0].No : string.Empty;
            if (gm != null)
            {
                EstateElzamSixArtInquiryGeometry geometry = gm;
                var Seller = masterEntity.SsrArticle6InqPeople.Where(x => x.RelationType == "100").First();
                var Buyer = masterEntity.SsrArticle6InqPeople.Where(x => x.RelationType == "101").First();
                EstateElzamSixArtInquiryServiceInput sendServiceInput = new();
                sendServiceInput.SendData = new EstateElzamSixArtInquiryServiceInputData
                {
                    ProvinceCode = Convert.ToInt32(masterEntity.Province.Code),
                    CountyCode = Convert.ToInt32(masterEntity.County.Code),
                    UnitNumberSdd = unitNo,
                    AreaT = masterEntity.EstateArea,
                    PartIdSdd = masterEntity.EstateSection.SsaaCode,
                    CountyIdSdd = masterEntity.EstateSubsection.SsaaCode,
                    FracShare = "6",// Seller.SharePart.ToString(),
                    TotalShare = "6",//Seller.ShareTotal.ToString(),
                    Geometry = geometry,
                    OriginalNumberSdd = masterEntity.EstateMainPlaque,
                    OwnerType = 1,//Convert.ToInt32(masterEntity.EstateType.Code),
                    PostalCode = masterEntity.EstatePostCode,
                    RequestDate = _dateTimeService.CurrentPersianDate,
                    RequestType = 1,
                    SabtTrackingCode = masterEntity.No,
                    SubNumberSdd = masterEntity.EstateSecondaryPlaque,
                    UserCode = 1,//Convert.ToInt32(masterEntity.EstateUsing.Code),
                    SellerNationalCode = Seller.NationalityCode,
                    BuyerNationalCode = Buyer.NationalityCode

                };
                foreach (var receiver in masterEntity.SsrArticle6InqReceivers)
                {
                    sendServiceInput.SendData.InqOrgan.Add(receiver.SsrArticle6Organ.Code);
                }

                var attachments = await GetAttachments(cancellationToken);
                if (!apiResult.IsSuccess)
                    return apiResult;
                byte[] zipFile = null;
                if (attachments.Count > 0)
                {
                    zipFile = await ZipHelper.CreateZipFile(attachments, cancellationToken);
                    if (zipFile != null)
                    {
                        sendServiceInput.SendData.File = new EstateElzamSixArtInquiryFile()
                        {
                            Name = "attachments",
                            Size = zipFile.Length,
                            Type = "zip",
                            Value = Convert.ToBase64String(zipFile)
                        };
                    }
                }
                var sendServiceOutput = await _mediator.Send(sendServiceInput, cancellationToken);
                if (sendServiceOutput.IsSuccess && sendServiceOutput.Data != null && sendServiceOutput.Data.ResCode == 1)
                {
                    if (zipFile != null)
                    {
                        masterEntity.Attachments = zipFile;
                    }
                    masterEntity.TrackingCode = sendServiceOutput.Data.Data.TrackingCode;
                    masterEntity.SendDate = _dateTimeService.CurrentPersianDate;
                    masterEntity.SendTime = _dateTimeService.CurrentTime;
                    //masterEntity.EstateMap = cadastrServiceOutput.Data.Data.GeoJsonMap;
                    masterEntity.WorkflowStatesId = EstateConstant.EstateElzamSixArtInquiryStates.Sended;
                    //masterEntity.SsrArticle6InqResponses.Add(new SsrArticle6InqResponse()
                    //{
                    //    EstateMap = masterEntity.EstateMap,
                    //    Description = "",
                    //    Ilm = "1",
                    //    Id = Guid.NewGuid(),
                    //    ResponseDate = _dateTimeService.CurrentPersianDate,
                    //    ResponseNo = "1234567890",
                    //    ResponseTime = _dateTimeService.CurrentTime,
                    //    ResponseType = true,
                    //    SenderOrgId = masterEntity.SsrArticle6InqReceivers.First().SsrArticle6OrganId,
                    //    ScriptoriumId = masterEntity.ScriptoriumId,
                    //    SsrArticle6InqId = masterEntity.Id
                    //});
                    await _ElzamArtSixRepository.UpdateAsync(masterEntity, cancellationToken);
                    apiResult.message.Add("استعلام با موفقیت ارسال شد");
                }
                else
                {
                    apiResult.IsSuccess = false;                   
                    apiResult.message.Add(sendServiceOutput.message.Count > 0 ? sendServiceOutput.message[0] : "خطا در ارسال استعلام رخ داد");
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("استعلام به دلیل عدم دریافت مختصات ملک مربوطه از سامانه کاداستر ارسال نشد");
            }
            return apiResult;
        }
    }
}