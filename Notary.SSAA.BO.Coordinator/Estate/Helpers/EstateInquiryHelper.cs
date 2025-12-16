using Azure.Core;
using Mapster;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Threading;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Domain.Abstractions;

namespace Notary.SSAA.BO.Coordinator.Estate.Helpers
{
    public class EstateInquiryHelper
    {
        private IMediator _mediator;
        private ConfigurationParameterHelper _configurationParameterHelper;
        private GeneralExternalServiceHelper _generalExternalServiceHelper;
        private IDateTimeService _dateTimeService;
        public EstateInquiryHelper(IRepository<BO.Domain.Entities.ConfigurationParameter> configurationParameterRepository, IMediator mediator,IDateTimeService dateTimeService)
        {
            this._mediator = mediator;
            _configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, this._mediator);
            _generalExternalServiceHelper = new GeneralExternalServiceHelper(configurationParameterRepository, _mediator);
            _dateTimeService = dateTimeService;
        }
        public async Task<bool> EstateInquiryCheckSana(EstateInquiryPersonViewModel person, CancellationToken cancellationToken)
        {
            var result = await _configurationParameterHelper.EstateInquirySanaIsRequired(cancellationToken);
            if (result == "true")
            {
                if (string.IsNullOrWhiteSpace(person.PersonNationalityCode)) return false;
                var sanaObject = await _generalExternalServiceHelper.CallSanaService(person.PersonNationalityCode, cancellationToken);
                if (sanaObject != null)
                {
                    person.PersonSanaState = true;
                    person.PersonSanaMoileNo = sanaObject.MobileNo;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }

        public async Task<(bool, List<string>)> EstateInquiryCheckShahkar(EstateInquiryPersonViewModel person, CancellationToken cancellationToken)
        {
            var smsIsActiveConfigResult = await _configurationParameterHelper.EstateInquirySendSMSIsEnabled(cancellationToken);
            var shahkarConfigResult = await _configurationParameterHelper.EstateInquiryShahkarIsRequired(cancellationToken);
            var result = new List<string>();
            if (shahkarConfigResult == "true")
            {
                if (string.IsNullOrWhiteSpace(person.PersonNationalityCode)) return (false, result);
                if (string.IsNullOrWhiteSpace(person.PersonMobileNo))
                {
                    result.Add("ShahkarServiceError");
                    result.Add("شماره تلفن همراه مالک اجباری می باشد");
                    return (false, result);
                }
                var shahkarData = await _generalExternalServiceHelper.CallShahkarService(person.PersonNationalityCode, person.PersonMobileNo, cancellationToken);
                if (shahkarData != null)
                {
                    person.PersonMobileNoState = true;
                    return (true, result);
                }
                else
                    return (false, result);
            }
            if (smsIsActiveConfigResult == "true")
            {
                if (string.IsNullOrWhiteSpace(person.PersonMobileNo))
                {
                    result.Add("ShahkarServiceError");
                    result.Add("شماره تلفن همراه مالک اجباری می باشد");
                    return (false, result);
                }
                var shahkarData = await _generalExternalServiceHelper.CallShahkarService(person.PersonNationalityCode, person.PersonMobileNo, cancellationToken);
                if (shahkarData != null)
                {
                    person.PersonMobileNoState = true;
                    return (true, result);
                }
                else
                    return (false, result);

            }
            return (true, result);
        }
        public bool CompareEstateInquiryPersonBySabtAhval(EstateInquiryPersonViewModel person,SabtAhvalServiceViewModel serviceData,  ref List<string> lstErr)
        {
            lstErr = new List<string>();
            if (serviceData.AlphabetSeri.PersianToArabic() != person.PersonSeriAlpha.PersianToArabic())
                lstErr.Add("بخش الفبایی سریال شناسنامه با ثبت احوال مطابقت ندارد");
            if (serviceData.Name.PersianToArabic() != person.PersonName.PersianToArabic())
                lstErr.Add("نام مالک با ثبت احوال مطابقت ندارد");
            if (serviceData.Family.PersianToArabic() != person.PersonFamily.PersianToArabic())
                lstErr.Add("نام خانوادگی مالک با ثبت احوال مطابقت ندارد");
            if (serviceData.FatherName.PersianToArabic() != person.PersonFatherName.PersianToArabic())
                lstErr.Add("نام پدر مالک با ثبت احوال مطابقت ندارد");
            if (serviceData.ShenasnameNo.PersianToArabic() != person.PersonIdentityNo.PersianToArabic())
                lstErr.Add("شماره شناسنامه با ثبت احوال مطابقت ندارد");
            if (serviceData.ShenasnameSeri.PersianToArabic() != person.PersonSeri.PersianToArabic())
                lstErr.Add("سری شناسنامه با ثبت احوال مطابقت ندارد");
            if (serviceData.ShenasnameSerial.PersianToArabic() != person.PersonSerialNo.PersianToArabic())
                lstErr.Add("شماره سریال شناسنامه با ثبت احوال مطابقت ندارد");
            if (person.PersonSexType != serviceData.SexType)
                lstErr.Add("جنسیت مالک با ثبت احوال مطابقت ندارد");
            if (lstErr.Count > 0)
                return false;
            return true;
        }
        public bool CompareEstateInquiryPersonByForeignerService(EstateInquiryPersonViewModel person, RealForeignerServiceViewModel serviceData, ref List<string> lstErr)
        {
            lstErr = new List<string>();
            string NationalityId1 = person.PersonNationalityId != null && person.PersonNationalityId.Count > 0 ? person.PersonNationalityId.First() : "";
            if (serviceData.Name.PersianToArabic() != person.PersonName.PersianToArabic())
                lstErr.Add("نام مالک با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.Family.PersianToArabic() != person.PersonFamily.PersianToArabic())
                lstErr.Add("نام خانوادگی مالک با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.FatherName.PersianToArabic() != person.PersonFatherName.PersianToArabic())
                lstErr.Add("نام پدر مالک با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.IdentityNo.PersianToArabic() != person.PersonIdentityNo.PersianToArabic())
                lstErr.Add("شماره شناسنامه مالک با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.BirthDate.PersianToArabic() != person.PersonBirthDate.PersianToArabic())
                lstErr.Add("تاریخ تولد مالک با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.SexType.PersianToArabic() != person.PersonSexType.PersianToArabic())
                lstErr.Add("جنسیت مالک  با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.NationalityId != NationalityId1)
                lstErr.Add("تابعیت مالک  با سامانه اتباع خارجی مطابقت ندارد");
            if (lstErr.Count > 0)
                return false;
            return true;
        }
        public bool CompareEstateInquiryPersonByForeignerService(EstateInquiryPersonViewModel person, LegalForeignerServiceViewModel serviceData, ref List<string> lstErr)
        {
            lstErr = new List<string>();
            string NationalityId1 = person.PersonNationalityId != null && person.PersonNationalityId.Count > 0 ? person.PersonNationalityId.First() : "";
            if (serviceData.Name.PersianToArabic() != person.PersonName.PersianToArabic())
                lstErr.Add("نام مالک با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.RegisterNo.PersianToArabic() != person.PersonIdentityNo.PersianToArabic())
                lstErr.Add("شماره ثبت مالک (شخص حقوقی) با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.RegisterDate.PersianToArabic() != person.PersonBirthDate.PersianToArabic())
                lstErr.Add("تاریخ ثبت مالک (شخص حقوقی) با سامانه اتباع خارجی مطابقت ندارد");
            if (serviceData.NationalityId != NationalityId1)
                lstErr.Add("تابعیت مالک (شخص حقوقی) با سامانه اتباع خارجی مطابقت ندارد");
            if (lstErr.Count > 0)
                return false;
            return true;
        }
        public bool CompareEstateInquiryPersonByIlenc(EstateInquiryPersonViewModel person, ILENCServiceViewModel serviceData, ref List<string> lstErr)
        {
            lstErr = new List<string>();
            if (serviceData.TheCCompany.Name.PersianToArabic() != person.PersonName.PersianToArabic())
                lstErr.Add(string.Format("نام شخص ({0}) با سامانه اشخاص حقوقی مطابقت ندارد", person.PersonName));
            if (serviceData.TheCCompany.RegisterDate != person.PersonBirthDate)
                lstErr.Add("تاریخ ثبت شخص حقوقی با سامانه اشخاص حقوقی مطابقت ندارد");
            if (serviceData.TheCCompany.RegisterNumber != person.PersonIdentityNo)
                lstErr.Add("شماره ثبت  شخص حقوقی با سامانه اشخاص حقوقی مطابقت ندارد");
            if (lstErr.Count > 0)
                return false;
            return true;
        }
        public async Task<(List<string>,bool)> CheckSimilarInquiries(object input, List<EstateInquirySpecialFields> similarInquiryList,Dictionary<string,string> scriptoriumGeoDictionary, Dictionary<string,string> unitGeoDictionary, IDealSummaryRepository _dealSummaryRepository,List<string> messages,CancellationToken cancellationToken)
        {
            var request = input as CreateEstateInquiryCommand;
            if (request == null)
            {
                var req = input as UpdateEstateInquiryCommand;
                if (req == null)
                    throw new Exception("خطا در اجرای سرویس");
                request = req.Adapt<CreateEstateInquiryCommand>();
            }
            bool UpdateFollowedInquiry = false;
            List<string> inquiryIdList = new();
            foreach (EstateInquirySpecialFields estateInquiry in similarInquiryList)
            {
                if (estateInquiry.State == EstateConstant.EstateInquiryStates.NeedCorrection)
                {
                    if (!string.IsNullOrWhiteSpace(estateInquiry.FirstSendDate))
                    {
                        if (_dateTimeService.CurrentPersianDate.GetDateTimeDistance("1391/7/24").TotalDays >= 1)
                        {
                            TimeSpan ts = _dateTimeService.CurrentPersianDate.GetDateTimeDistance(estateInquiry.FirstSendDate);

                            if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 30)
                            {
                                inquiryIdList.Add(estateInquiry.Id.ToString());
                            }
                        }
                    }
                }
                if (estateInquiry.State == EstateConstant.EstateInquiryStates.Sended)
                {
                    if (!string.IsNullOrWhiteSpace(estateInquiry.FirstSendDate))
                    {
                        TimeSpan ts = _dateTimeService.CurrentPersianDate.GetDateTimeDistance(estateInquiry.FirstSendDate);
                        if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 30)
                        {
                            inquiryIdList.Add(estateInquiry.Id.ToString());
                        }
                    }
                }
                if (estateInquiry.State == EstateConstant.EstateInquiryStates.EditAndReSend)
                {
                    if (!string.IsNullOrWhiteSpace(estateInquiry.LastSendDate))
                    {
                        TimeSpan ts = _dateTimeService.CurrentPersianDate.GetDateTimeDistance(estateInquiry.LastSendDate);
                        if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 30)
                        {
                            inquiryIdList.Add(estateInquiry.Id.ToString());
                        }
                    }
                }
                try
                {
                    if (estateInquiry.State == EstateConstant.EstateInquiryStates.ConfirmResponse && estateInquiry.ResponseResult == "True" || estateInquiry.State == EstateConstant.EstateInquiryStates.Archived && estateInquiry.ResponseResult == "True")
                    {
                        string date = string.Empty;
                        if (estateInquiry.ResponseDate != null && estateInquiry.ResponseDate != string.Empty)
                        {
                            date = estateInquiry.ResponseDate;
                        }
                        else if (estateInquiry.TrtsReadDate != null && estateInquiry.TrtsReadDate != string.Empty)
                        {
                            date = estateInquiry.TrtsReadDate;
                        }
                        else if (estateInquiry.LastSendDate != null && estateInquiry.LastSendDate != string.Empty)
                        {
                            date = estateInquiry.LastSendDate;
                        }
                        else
                        {
                            date = estateInquiry.FirstSendDate;
                        }
                        if (date == string.Empty) continue;
                        TimeSpan ts = _dateTimeService.CurrentPersianDate.GetDateTimeDistance(date);
                        var ScriptoriumGeolocationId = scriptoriumGeoDictionary[estateInquiry.ScriptoriumId];
                        var UnitGeolocationId = unitGeoDictionary[estateInquiry.UnitId];
                        if (ScriptoriumGeolocationId != UnitGeolocationId)
                        {
                            if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 50)
                            {
                                inquiryIdList.Add(estateInquiry.Id.ToString());
                            }
                            else if (await _dealSummaryRepository.IsAttachedToDealSummary(estateInquiry.Id.ToString(), cancellationToken))
                            {
                                inquiryIdList.Add(estateInquiry.Id.ToString());
                            }
                            else if (request.InqEstateInquiryId != null && request.InqEstateInquiryId.Count > 0)
                            {
                                if (estateInquiry.Id == request.InqEstateInquiryId.First().ToGuid())
                                {
                                    if (!await _dealSummaryRepository.IsAttachedToDealSummary(request.InqEstateInquiryId.First(), cancellationToken))
                                    {
                                        inquiryIdList.Add(estateInquiry.Id.ToString());
                                        UpdateFollowedInquiry = true;
                                    }
                                    else
                                    {
                                        messages.Add(" قبلا برای استعلامی که در قسمت پیرو وارد نموده اید خلاصه معامله ثبت شده است");
                                    }
                                }
                            }
                        }
                        else if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 37)
                        {
                            inquiryIdList.Add(estateInquiry.Id.ToString());
                        }
                        else if (await _dealSummaryRepository.IsAttachedToDealSummary(estateInquiry.Id.ToString(), cancellationToken))
                        {
                            inquiryIdList.Add(estateInquiry.Id.ToString());
                        }
                        else if (request.InqEstateInquiryId != null && request.InqEstateInquiryId.Count > 0)
                        {
                            if (estateInquiry.Id.ToString() == request.InqEstateInquiryId.First())
                            {
                                if (!await _dealSummaryRepository.IsAttachedToDealSummary(request.InqEstateInquiryId.First(), cancellationToken))
                                {
                                    inquiryIdList.Add(estateInquiry.Id.ToString());
                                    UpdateFollowedInquiry = true;
                                }
                                else
                                {
                                    messages.Add(" قبلا برای استعلامی که در قسمت پیرو وارد نموده اید خلاصه معامله ثبت شده است");
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            return (inquiryIdList,UpdateFollowedInquiry);
        }
    }
}
