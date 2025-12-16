using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;



namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class SendEstateInquiryCommandHandler : BaseCommandHandler<SendEstateInquiryCommand, ApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeridaftarRepository;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IDateTimeService _dateTimeService;
        private protected Domain.Entities.EstateInquiry masterEntity;
        private protected ApiResult<EstateInquiryViewModel> apiResult;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IRepository<EstateInquirySendedSm> _EstateInquirySendedSmRepository;
        private readonly IConfiguration _configuration;
        private ExternalServiceHelper externalServiceHelper = null;
        private bool paymentIsRequired;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private BaseInfoServiceHelper _baseInfoServiceHelper = null;
        public SendEstateInquiryCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger, IRepository<ConfigurationParameter> configurationParameterRepository, IEstateSeriDaftarRepository estateSeridaftarRepository, IEstateSectionRepository estateSectionRepository, IEstateSubSectionRepository estateSubSectionRepository,
            IConfiguration configuration,
            IHttpEndPointCaller httpEndPointCaller, IRepository<EstateInquirySendedSm> EstateInquirySendedSmRepository)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            _estateInquiryRepository = estateInquiryRepository;
            _dateTimeService = dateTimeService;
            _configurationParameterRepository = configurationParameterRepository;
            _estateSeridaftarRepository = estateSeridaftarRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _EstateInquirySendedSmRepository = EstateInquirySendedSmRepository;
            _configurationParameterHelper=new ConfigurationParameterHelper(_configurationParameterRepository,mediator);
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            this.externalServiceHelper = new ExternalServiceHelper(_mediator,
                                                                  dateTimeService,
                                                                 userService,
                                                                 _configurationParameterHelper,
                                                                 _estateSeridaftarRepository,
                                                                 _estateSectionRepository,
                                                                 _estateSubSectionRepository,
                                                                 _configuration,
                                                                 _httpEndPointCaller,
                                                                 null,
                                                                 null
                                                                 );
            paymentIsRequired = false;
        }
        protected override async Task<ApiResult> ExecuteAsync(SendEstateInquiryCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());           
            this.paymentIsRequired = await _configurationParameterHelper.EstateInquiryPaymentIsRequired(cancellationToken);
            BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                if (masterEntity != null)
                {
                    await _estateInquiryRepository.LoadCollectionAsync(masterEntity, x => x.EstateInquiryPeople, cancellationToken);
                    await _estateInquiryRepository.LoadReferenceAsync(masterEntity, x => x.EstateInquiryNavigation, cancellationToken);
                    bool bv = false;
                    var PrevWorkflowStatesId = masterEntity.WorkflowStatesId;
                    var PrevFirstSendDate = masterEntity.FirstSendDate;
                    var PrevFirstSendTime = masterEntity.FirstSendTime;
                    var PrevLastSendDate = masterEntity.LastSendDate;
                    var PrevLastSendTime = masterEntity.LastSendTime;
                    var PrevResponse = masterEntity.Response;
                    var PrevResponseResult = masterEntity.ResponseResult;
                    var PrevResponseDate = masterEntity.ResponseDate;
                    var PrevResponseTime = masterEntity.ResponseTime;
                    var PrevResponseNumber = masterEntity.ResponseNumber;
                    var PrevResponseCode = masterEntity.ResponseCode;
                    var PrevTimestamp = masterEntity.Timestamp;
                    var PrevResponseDigitalsignature = masterEntity.ResponseDigitalsignature;
                    var prevResponseSubjectdn = masterEntity.ResponseSubjectdn;
                    var PrevSpecificStatus = masterEntity.SpecificStatus;
                    var PrevTrtsReadDate = masterEntity.TrtsReadDate;
                    var PrevTrtsReadTime = masterEntity.TrtsReadTime;
                    EstateInquirySendreceiveLog estateInquirySendreceiveLog = null;
                    if (masterEntity.WorkflowStatesId == EstateConstant.EstateInquiryStates.NotSended)
                    {
                        masterEntity.WorkflowStatesId = EstateConstant.EstateInquiryStates.Sended;
                        masterEntity.FirstSendDate = _dateTimeService.CurrentPersianDate;
                        masterEntity.FirstSendTime = _dateTimeService.CurrentTime;
                        masterEntity.LastSendDate = masterEntity.FirstSendDate;
                        masterEntity.LastSendTime = masterEntity.FirstSendTime;
                        masterEntity.Timestamp += 1;
                        bv = true;
                    }
                    else if (masterEntity.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedCorrection || masterEntity.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedDocument)
                    {
                        masterEntity.WorkflowStatesId = EstateConstant.EstateInquiryStates.EditAndReSend;
                        masterEntity.LastSendDate = _dateTimeService.CurrentPersianDate;
                        masterEntity.LastSendTime = _dateTimeService.CurrentTime;
                        masterEntity.Response = null;
                        masterEntity.ResponseResult = null;
                        masterEntity.ResponseDate = null;
                        masterEntity.ResponseTime = null;
                        masterEntity.ResponseNumber = null;
                        masterEntity.ResponseCode = null;
                        masterEntity.ResponseDigitalsignature = null;
                        masterEntity.ResponseSubjectdn = null;
                        masterEntity.SpecificStatus = null;
                        masterEntity.TrtsReadDate = null;
                        masterEntity.TrtsReadTime = null;
                        masterEntity.Timestamp += 1;
                        bv = true;
                    }
                    if (bv)
                    {
                        estateInquirySendreceiveLog = new EstateInquirySendreceiveLog
                        {
                            EstateInquiryId = masterEntity.Id,
                            ActionDate = _dateTimeService.CurrentPersianDate,
                            ActionTime = _dateTimeService.CurrentTime,
                            EstateInquiryActionTypeId = "2",
                            WorkflowStatesId = masterEntity.WorkflowStatesId,
                            ScriptoriumId = masterEntity.ScriptoriumId,
                            Id = Guid.NewGuid()
                        };
                        masterEntity.EstateInquirySendreceiveLogs.Add(estateInquirySendreceiveLog);
                    }
                    await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                    if (await _configurationParameterHelper.IsEstateInquiryRealSendEnabled(cancellationToken))
                    {                        
                        string sendResult = await SendToSabtEstateSystemByService(cancellationToken);
                        if (!string.IsNullOrWhiteSpace(sendResult))
                        {
                            masterEntity.EstateInquirySendreceiveLogs.Remove(estateInquirySendreceiveLog);
                            masterEntity.WorkflowStatesId = PrevWorkflowStatesId;
                            masterEntity.FirstSendDate = PrevFirstSendDate;
                            masterEntity.FirstSendTime = PrevFirstSendTime;
                            masterEntity.LastSendDate = PrevLastSendDate;
                            masterEntity.LastSendTime = PrevLastSendTime;
                            masterEntity.Response = PrevResponse;
                            masterEntity.ResponseResult = PrevResponseResult;
                            masterEntity.ResponseDate = PrevResponseDate;
                            masterEntity.ResponseTime = PrevResponseTime;
                            masterEntity.ResponseNumber = PrevResponseNumber;
                            masterEntity.ResponseCode = PrevResponseCode;
                            masterEntity.Timestamp = PrevTimestamp;
                            masterEntity.ResponseDigitalsignature = PrevResponseDigitalsignature;
                            masterEntity.ResponseSubjectdn = prevResponseSubjectdn;
                            masterEntity.SpecificStatus = PrevSpecificStatus;
                            masterEntity.TrtsReadDate = PrevTrtsReadDate;
                            masterEntity.TrtsReadTime = PrevTrtsReadTime;
                            await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                           
                            apiResult.IsSuccess = false;
                            apiResult.message.Add(sendResult);
                            apiResult.statusCode = ApiResultStatusCode.Success;
                        }
                        else
                        {
                            masterEntity.PrevFollowedInquiryId = null;
                            await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                            apiResult.IsSuccess = true;
                            apiResult.message = new()
                                              {
                                                "ارسال استعلام به سامانه املاک با موفقیت انجام شد"
                                              };
                            apiResult.statusCode = ApiResultStatusCode.Success;
                        }
                    }
                    else
                    {
                        apiResult.IsSuccess = true;
                        apiResult.message = new()
                                              {
                                                "ارسال استعلام به سامانه املاک با موفقیت انجام شد"
                                              };
                        apiResult.statusCode = ApiResultStatusCode.Success;
                    }
                    if (apiResult.IsSuccess)
                    {
                        await SendSMS(request.EstateInquiryId, cancellationToken);
                        
                        var response = await _mediator.Send(new GetEstateInquiryByIdQuery() { EstateInquiryId = masterEntity.Id.ToString(), RelatedServer = "FO" }, cancellationToken);
                        if (response.IsSuccess)
                        {
                            apiResult.Data = response.Data.Adapt<EstateInquiryViewModel>();
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = response.statusCode;
                            apiResult.message.Add("خطا  در بازیابی اطلاعات از پایگاه داده ..... ");
                            apiResult.message = response.message;
                        }
                    }
                }
            }
            return apiResult;
        }
        
        private async Task<string> SendToSabtEstateSystemByService(CancellationToken cancellationToken)
        {
            externalServiceHelper.EstateInquiry = masterEntity;
            await externalServiceHelper.GetEstateInquiryBaseInfoData(cancellationToken);
            return await externalServiceHelper.SendEstateInquiryToSabtEstateSystemByService(cancellationToken);            
        }
        protected override bool HasAccess(SendEstateInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar);
        }
        private void BusinessValidation(SendEstateInquiryCommand request)
        {
            if (masterEntity != null)
            {
                if (string.IsNullOrWhiteSpace(masterEntity.WorkflowStatesId))
                {
                    apiResult.message.Add("استعلام در وضعیت قابل ارسال نمی باشد");
                }
                else if (masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.NeedCorrection &&
                    masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.NotSended &&
                    masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.NeedDocument)
                {
                    apiResult.message.Add("استعلام قبلا ارسال شده است");
                }
                else if (string.IsNullOrWhiteSpace(masterEntity.IsCostPaid) || masterEntity.IsCostPaid != "1")
                {
                    if (paymentIsRequired)
                        apiResult.message.Add("هزینه استعلام هنوز پرداخت نشده است");
                }
            }
            else
            {
                apiResult.message.Add("استعلام یافت نشد");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }
        private async Task SendSMS(string estateInquiryId, CancellationToken cancellationToken)
        {
            if (!Convert.ToBoolean(await _configurationParameterHelper.EstateInquirySendSMSIsEnabled(cancellationToken)))
                return;
            var masterEntity = await _estateInquiryRepository.TableNoTracking.Include(x => x.EstateInquiryPeople).Where(x => x.Id == estateInquiryId.ToGuid()).FirstOrDefaultAsync(cancellationToken);
            if (masterEntity == null) return;
            if (masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.Sended) return;
            var person = masterEntity.EstateInquiryPeople.FirstOrDefault();
            if (person == null) return;
            if (!string.IsNullOrWhiteSpace(person.MobileNo) && person.MobileNoState == EstateConstant.BooleanConstant.True)
            {
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { masterEntity.UnitId }, cancellationToken);
                var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { masterEntity.ScriptoriumId }, cancellationToken);
                string msg1 = "مالک محترم" + Environment.NewLine + "درخواست استعلام ملک شما با شناسه یکتا سند مالکیت  {0} در تاریخ {1} توسط  {2} به واحد ثبت املاک {3} ارسال شد.";
                string msg2 = "مالک محترم" + Environment.NewLine + "درخواست استعلام ملک شما با پلاک اصلی  {0} و فرعی {1} در تاریخ {2} توسط  {3} به واحد ثبت املاک {4} ارسال شد.";
                var msg = "";
                if (!string.IsNullOrWhiteSpace(masterEntity.ElectronicEstateNoteNo))
                    msg = string.Format(msg1, masterEntity.ElectronicEstateNoteNo, masterEntity.LastSendDate, (scriptorium != null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList[0].Name : "-", (unit != null && unit.UnitList.Count > 0) ? unit.UnitList[0].Name.Replace("حوزه ثبت ملک", "").Replace("حوزه ثبت ملك", "") : "-");
                else
                    msg = string.Format(msg2, masterEntity.Basic, masterEntity.Secondary, masterEntity.LastSendDate, (scriptorium != null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList[0].Name : "-", (unit != null && unit.UnitList.Count > 0) ? unit.UnitList[0].Name.Replace("حوزه ثبت ملک", "").Replace("حوزه ثبت ملك", "") : "-");
                var sendSMSServiceInput = new SendSmsFromKanoonServiceInput()
                {
                    ClientId = "SSAR",
                    MobileNo = person.MobileNo,
                    Message = msg
                };
                var smsResult = await _mediator.Send(sendSMSServiceInput, cancellationToken);
                if (smsResult.IsSuccess)
                {
                    if (!string.IsNullOrWhiteSpace(smsResult.Data.TrackCode))
                    {
                        var estateInquirySendedSm = new EstateInquirySendedSm()
                        {
                            EstateInquiryId = estateInquiryId.ToGuid(),
                            Id = Guid.NewGuid(),
                            Message = msg,
                            MobileNo = person.MobileNo,
                            SendDate = _dateTimeService.CurrentPersianDate,
                            SendTime = _dateTimeService.CurrentTime,
                            SmsTrackingNo = smsResult.Data.TrackCode,
                            WorkflowStatesId = masterEntity.WorkflowStatesId
                        };
                        await _EstateInquirySendedSmRepository.AddAsync(estateInquirySendedSm, cancellationToken);
                    }
                }
            }
        }            
    }
}
