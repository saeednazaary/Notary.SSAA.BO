using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class ValidateEstateInquiryItemsQueryHandler : BaseQueryHandler<ValidateEstateInquiryItemsQuery, ApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private bool UpdateFollowedInquiry;
        public ValidateEstateInquiryItemsQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository, IDateTimeService dateTimeService, IDealSummaryRepository dealSummaryRepository)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _dateTimeService = dateTimeService;
            _dealSummaryRepository = dealSummaryRepository;
            UpdateFollowedInquiry = false;
        }
        protected override bool HasAccess(ValidateEstateInquiryItemsQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }


        protected async override Task<ApiResult> RunAsync(ValidateEstateInquiryItemsQuery request, CancellationToken cancellationToken)
        {
            
            ApiResult apiResult = new() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            if (await _estateInquiryRepository.IsExistsInquiry(!string.IsNullOrWhiteSpace(request.InquiryDate) ? request.InquiryDate : _dateTimeService.CurrentPersianDate, request.InquiryNo, request.InquiryId, request.ScriptoriumId, cancellationToken))
            {
                apiResult.message.Add("یک دفتر خانه در  یک سال ، دو استعلام با یک شماره نمی تواند ثبت کند");
            }
            var similarInquiryList = await _estateInquiryRepository.GetSimilarInquiryList(request.ScriptoriumId, request.UnitId, request.PageNumber, request.NoteBookNo, request.SeriDaftarId, request.PersonMelliCode, request.Basic, request.Secondary, request.InquiryId, request.IsExecuteTransfer, request.PersonName, request.PersonFamily, request.DocPrintNo, cancellationToken);            
            if (similarInquiryList != null && similarInquiryList.Count > 0)
            {
                
                var list = similarInquiryList.Select(est => est.Id.ToString() + "|" + est.InquiryNo + "|" + est.InquiryDate).ToList();
                var unitGeoDictionary = await GetGeolocationOfRegistrationUnit(similarInquiryList.Select(x => x.UnitId).ToArray(), cancellationToken);
                var scriptoriumGeoDictionary = await GetGeolocationOfScriptorium(new string[] { request.ScriptoriumId }, cancellationToken);

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
                                else if (!string.IsNullOrWhiteSpace(request.FollowedInquiryId))
                                {
                                    if (estateInquiry.Id == request.FollowedInquiryId.ToGuid())
                                    {
                                        if (!await _dealSummaryRepository.IsAttachedToDealSummary(request.FollowedInquiryId, cancellationToken))
                                        {
                                            inquiryIdList.Add(estateInquiry.Id.ToString());
                                            UpdateFollowedInquiry = true;
                                        }
                                        else
                                        {
                                            apiResult.message.Add(" قبلا برای استعلامی که در قسمت پیرو وارد نموده اید خلاصه معامله ثبت شده است");
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
                            else if (!string.IsNullOrWhiteSpace(request.FollowedInquiryId))
                            {
                                if (estateInquiry.Id == request.FollowedInquiryId.ToGuid())
                                {
                                    if (!await _dealSummaryRepository.IsAttachedToDealSummary(request.FollowedInquiryId, cancellationToken))
                                    {
                                        inquiryIdList.Add(estateInquiry.Id.ToString());
                                        UpdateFollowedInquiry = true;
                                    }
                                    else
                                    {
                                        apiResult.message.Add(" قبلا برای استعلامی که در قسمت پیرو وارد نموده اید خلاصه معامله ثبت شده است");
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                foreach (string st in inquiryIdList)
                {
                    list.RemoveAll(str => str.StartsWith(st));
                }
                if (list.Count > 0)
                {
                    string[] stringArray = list[0].Split('|');
                    string enquiryno = stringArray[1];
                    string enquirydate = stringArray[2];
                    apiResult.message.Add(string.Format("شما قبلا استعلامی را با شماره {0}  در تاریخ {1} با این مشخصات ثبت کرده اید ", enquiryno, enquirydate));
                }
                if (apiResult.message.Count > 0)
                {
                    apiResult.IsSuccess = false;
                }
                else
                {
                    if (UpdateFollowedInquiry)
                        apiResult.message.Add("UpdateFollowedInquiry");
                }
            }
            
            return apiResult;
        }

        public async Task<GetUnitByIdViewModel> GetUnitById(string[] unitId, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(new GetUnitByIdQuery(unitId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public async Task<GetScriptoriumByIdViewModel> GetScriptoriumById(string[] scriptoriumId, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(new GetScriptoriumByIdQuery(scriptoriumId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public async Task<Dictionary<string, string>> GetGeolocationOfRegistrationUnit(string[] UnitId, CancellationToken cancellationToken)
        {
            var unitList = await GetUnitById(UnitId, cancellationToken);
            Dictionary<string, string> result = new();
            if (unitList != null)
            {
                foreach (var unit in unitList.UnitList)
                {
                    result.Add(unit.Id, unit.GeoLocationId);
                }
            }
            return result;
        }
        
        public async Task<Dictionary<string, string>> GetGeolocationOfScriptorium(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var scriptoriumList = await GetScriptoriumById(scriptoriumId, cancellationToken);
            Dictionary<string, string> result = new();
            if (scriptoriumList != null)
            {
                foreach (var scr in scriptoriumList.ScriptoriumList)
                {
                    result.Add(scr.Id, scr.GeoLocationId);                    
                }
            }
            return result;
        }

        
    }
}
