using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class SendEstateTaxInquiryCommandHandler : BaseCommandHandler<SendEstateTaxInquiryCommand, ApiResult>
    {
        private protected Domain.Entities.EstateTaxInquiry masterEntity;
        private protected ApiResult<EstateTaxInquiryViewModel> apiResult;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        SSAA.BO.Domain.Entities.EstateTaxInquiry prevEstateTaxInquiry = null;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IRepository<EstateTaxInquirySendedSm> _EstateTaxInquirySendedSmRepository;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private BaseInfoServiceHelper _baseInfoServiceHelper = null;
        private readonly ExternalServiceHelper externalServiceHelper = null;
        public SendEstateTaxInquiryCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IDateTimeService dateTimeService, IWorkfolwStateRepository workfolwStateRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IRepository<ConfigurationParameter> configurationParameterRepository, IRepository<EstateTaxInquirySendedSm> EstateTaxInquirySendedSmRepository) : base(mediator, userService, logger)
        {
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            _workfolwStateRepository = workfolwStateRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _dateTimeService = dateTimeService;
            masterEntity = new();
            apiResult = new();
            _configuration = configuration;
            _configurationParameterRepository = configurationParameterRepository;
            _EstateTaxInquirySendedSmRepository = EstateTaxInquirySendedSmRepository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            externalServiceHelper = new ExternalServiceHelper(_mediator, _dateTimeService, _userService, _configurationParameterHelper, null, null, null, _configuration, _httpEndPointCaller, _workfolwStateRepository, _estateTaxInquiryRepository);
        }
        protected override bool HasAccess(SendEstateTaxInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(SendEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            await BusinessValidation(request, cancellationToken);
         
            if (apiResult.IsSuccess)
            {
                try
                {
                    var sendResult = await externalServiceHelper.SendEstateTaxInquiry(request.EstateTaxInquiryId, "", cancellationToken);
                    if (string.IsNullOrWhiteSpace(sendResult))
                    {
                        await SendSMS(request.EstateTaxInquiryId, cancellationToken);
                        await externalServiceHelper.SendEstateTaxInquiryToSabtTerminalServerByService(cancellationToken);

                        var response = await _mediator.Send(new GetEstateTaxInquiryByIdQuery() { EstateTaxInquiryId = request.EstateTaxInquiryId, RelatedServer = "FO" }, cancellationToken);
                        if (response.IsSuccess)
                        {
                            apiResult.Data = response.Data.Adapt<EstateTaxInquiryViewModel>();
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = response.statusCode;
                            apiResult.message.Add("خطا  در بازیابی اطلاعات از پایگاه داده ..... ");
                            apiResult.message = response.message;
                        }
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.statusCode = ApiResultStatusCode.Success;
                        apiResult.message.Add(sendResult);
                    }

                }
                catch (Exception)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.message.Add("خطا در ارسال استعلام مالیاتی رخ داد ");

                }
            }
            return apiResult;
        }
        private async Task BusinessValidation(SendEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(request.EstateTaxInquiryId, cancellationToken);
            if (masterEntity != null)
            {
                if (masterEntity.IsActive == EstateConstant.BooleanConstant.False)
                {
                    apiResult.message.Add("استعلام  مالیاتی غیر فعال می باشد و امکان اجرای عملیات روی آن وجود ندارد");
                }
                else
                {
                    string[] states = new string[] { "0", "8", "33", "41", "43" };
                    if (!states.Contains(masterEntity.WorkflowStates.State))
                    {
                        apiResult.message.Add("استعلام قبلا به سازمان امور مالیاتی ارسال شده است");
                    }
                }
            }
            else
            {
                apiResult.message.Add("استعلام مالیاتی یافت نشد");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
            }
        }        
        private async Task SendSMS(string estateTaxInquiryId, CancellationToken cancellationToken)
        {
            if (!await _configurationParameterHelper.EstateTaxInquirySendSMSIsEnabled(cancellationToken))
                return;
            var masterEntity = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(estateTaxInquiryId, cancellationToken);
            if (masterEntity.WorkflowStates.State != "10") return;
            var person = masterEntity.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108").FirstOrDefault();
            if (person == null) return;
            if (!string.IsNullOrWhiteSpace(person.MobileNo) && person.MobileNoState == EstateConstant.BooleanConstant.True)
            {

                var scriptoriumViewModel = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { masterEntity.ScriptoriumId }, cancellationToken);
                ScriptoriumData scriptorium = scriptoriumViewModel.ScriptoriumList.FirstOrDefault();
                var scriptoriumName = scriptorium != null ? scriptorium.Name : "";
                var msg = "";
                string msg1 = "مالک محترم" + Environment.NewLine + "درخواست استعلام مالیاتی ملک شما با شناسه یکتا سند مالکیت {0} در تاریخ {1} توسط {2} به واحد سازمان امور مالیاتی ارسال شد.";
                string msg2 = "مالک محترم" + Environment.NewLine + "درخواست استعلام مالیاتی ملک شما با پلک اصلی  {0} و فرعی {1} در تاریخ {2} توسط {3} به واحد سازمان امور مالیاتی ارسال شد.";
                if (!string.IsNullOrWhiteSpace(masterEntity.EstateInquiry.ElectronicEstateNoteNo))
                    msg = string.Format(msg1, masterEntity.EstateInquiry.ElectronicEstateNoteNo, masterEntity.LastSendDate, scriptoriumName);
                else
                    msg = string.Format(msg2, masterEntity.EstateInquiry.Basic, masterEntity.EstateInquiry.Secondary, masterEntity.LastSendDate, scriptoriumName);
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
                        var estateInquirySendedSm = new EstateTaxInquirySendedSm()
                        {
                            EstateTaxInquiryId = estateTaxInquiryId.ToGuid(),
                            Id = Guid.NewGuid(),
                            Message = msg,
                            MobileNo = person.MobileNo,
                            SendDate = _dateTimeService.CurrentPersianDate,
                            SendTime = _dateTimeService.CurrentTime,
                            SmsTrackingNo = smsResult.Data.TrackCode,
                            WorkflowStatesId = masterEntity.WorkflowStatesId
                        };
                        await _EstateTaxInquirySendedSmRepository.AddAsync(estateInquirySendedSm, cancellationToken);
                    }
                }
            }
        }       
    }
}
