using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using System.Linq.Expressions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class ReceiveEstateTaxInquiryResponseCommandHandler : BaseExternalCommandHandler<EstateTaxInquiryResponseReceiveCommand, ExternalApiResult>
    {
        private protected Domain.Entities.EstateTaxInquiry masterEntity;
        private protected ExternalApiResult apiResult;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        private readonly ConfigurationParameterHelper _configurationParameterHelper;
        private readonly IRepository<EstateTaxInquirySendedSm> _EstateTaxInquirySendedSmRepository;
        public ReceiveEstateTaxInquiryResponseCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IDateTimeService dateTimeService, IWorkfolwStateRepository workfolwStateRepository, IRepository<SsrApiExternalUser> ssrApiExternalUser, IRepository<ConfigurationParameter> configurationParameterRepository, IRepository<EstateTaxInquirySendedSm> estateTaxInquirySendedSmRepository) : base(mediator, userService, logger)
        {
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            _workfolwStateRepository = workfolwStateRepository;
            _dateTimeService = dateTimeService;
            _ssrApiExternalUser = ssrApiExternalUser;
            masterEntity = new();
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            _configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, mediator);
            apiResult = new() { ResCode = "1", ResMessage = SystemMessagesConstant.Operation_Successful };
            _EstateTaxInquirySendedSmRepository = estateTaxInquirySendedSmRepository;
        }
        protected override bool HasAccess(EstateTaxInquiryResponseReceiveCommand request, IList<string> userRoles)
        {
            return true;
        }
        protected override async Task<ExternalApiResult> ExecuteAsync(EstateTaxInquiryResponseReceiveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await BusinessValidation(request, cancellationToken);
                if (apiResult.ResCode == "1")
                {
                    masterEntity.WorkflowStatesId = await GetWorkflowEstateId(request.Status.ToString(), cancellationToken);
                    masterEntity.LastReceiveStatusDate = _dateTimeService.CurrentPersianDate;
                    masterEntity.LastReceiveStatusTime = _dateTimeService.CurrentTime;
                    if (request.Status == 30)
                    {
                        masterEntity.TaxBillIdentity = request.PaymentId;
                        masterEntity.TaxBillIdentity2 = request.PaymentId2;
                        masterEntity.TaxBillIdentity3 = request.PaymentId3;
                        masterEntity.TaxAmount = request.TaxAmount;
                        masterEntity.TaxAmount2 = request.TaxAmount2;
                        masterEntity.TaxAmount3 = request.TaxAmount3;
                        masterEntity.TaxBillHtml = request.TaxBillHtml;
                        masterEntity.ShebaNo = request.ShebaNo;
                        masterEntity.ShebaNo2 = request.ShebaNo2;
                        masterEntity.ShebaNo3 = request.ShebaNo3;
                    }
                    else if (request.Status == 40)
                    {
                        masterEntity.IsLicenceReady = request.IsLicenseReady.ToYesNo();
                        masterEntity.CertificateNo = request.LicenseNumber;
                        masterEntity.CertificateHtml = request.LicenseHtml;
                    }
                    await _estateTaxInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                    await SendSMS(masterEntity.Id.ToString(), cancellationToken);
                }
            }
            catch (Exception ex)
            {
                apiResult.ResCode = "901";
                apiResult.ResMessage = "خطا در ثبت اطلاعات رخ داده است";
            }
            return apiResult;
        }
        private async Task BusinessValidation(EstateTaxInquiryResponseReceiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResCode = "110";
                apiResult.ResMessage = "نام کاربری یا کلمه عبور اشتباه می باشد";
            }
            else if (request.UserName != "TaxUser")
            {
                apiResult.ResCode = "111";
                apiResult.ResMessage = "مجاز به استفاده از این سرویس نمی باشید";
            }
            else
            {
                masterEntity = await _estateTaxInquiryRepository.GetAsync(x => x.TrackingCode == request.TrackingCode, cancellationToken);
                if (masterEntity == null)
                {
                    apiResult.ResCode = "109";
                    apiResult.ResMessage = "استعلام  یافت نشد";
                }
            }
           
        }
        private async Task<string> GetWorkflowEstateId(string state,CancellationToken cancellationToken)
        {
            var ws = await _workfolwStateRepository.TableNoTracking.Where(x => x.State == state && x.TableName == "ESTATE_TAX_INQUIRY").FirstOrDefaultAsync(cancellationToken);
            if(ws!=null)
                return ws.Id;
            return string.Empty;
        }
        private async Task SendSMS(string estateTaxInquiryId, CancellationToken cancellationToken)
        {
            
            
            var masterEntity = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(estateTaxInquiryId, cancellationToken);
            if (masterEntity == null) return;
            if (masterEntity.WorkflowStates.State != "40") return;
            if (!Convert.ToBoolean(await _configurationParameterHelper.EstateTaxInquirySendSMSIsEnabled(cancellationToken)))
                return;
            var person = masterEntity.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108").FirstOrDefault();
            if (person == null) return;
            if (!string.IsNullOrWhiteSpace(person.MobileNo) && person.MobileNoState == EstateConstant.BooleanConstant.True)
            {
                var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { masterEntity.ScriptoriumId }, cancellationToken);
                var msg = "";
                string msg1 = "مالک محترم" + Environment.NewLine + "استعلام مالیاتی ملک شما با شناسه یکتا سند مالکیت {0} در تاریخ {1} توسط  سازمان امور مالیاتی به {2} پاسخ داده شد.";
                string msg2 = "مالک محترم" + Environment.NewLine + "استعلام مالیاتی ملک شما با پلک اصلی  {0} و فرعی {1} در تاریخ {2} توسط  سازمان امور مالیاتی به {3} پاسخ داده شد.";
                if (masterEntity.EstateInquiry != null)
                {
                    if (!string.IsNullOrWhiteSpace(masterEntity.EstateInquiry.ElectronicEstateNoteNo))
                        msg = string.Format(msg1, masterEntity.EstateInquiry.ElectronicEstateNoteNo, masterEntity.LastReceiveStatusDate, (scriptorium != null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList.First().Name : "-");
                    else
                        msg = string.Format(msg2, masterEntity.EstateInquiry.Basic, masterEntity.EstateInquiry.Secondary, masterEntity.LastReceiveStatusDate, (scriptorium != null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList.First().Name : "-");
                }
                else
                    msg = string.Format(msg2, masterEntity.Estatebasic, masterEntity.Estatesecondary, masterEntity.LastReceiveStatusDate, (scriptorium != null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList.First().Name : "-");
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
                            SmsTrackingNo = smsResult.Data.TrackCode
                        };
                        await _EstateTaxInquirySendedSmRepository.AddAsync(estateInquirySendedSm, cancellationToken);
                    }
                }
            }
        }
    }
}
