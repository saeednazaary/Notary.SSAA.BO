using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;


namespace Notary.SSAA.BO.QueryHandler.Estate.DealSummary
{
    internal class DealSummaryVerificationWithoutOwnerCheckingQueryHandler : BaseQueryHandler<DealSummaryVerificationWithoutOwnerCheckingQuery<DealSummaryVerificationResultViewModel>, ApiResult<DealSummaryVerificationResultViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IDealSummaryTransferTypeRepository _dealSummaryTransferTypeRepository;
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly IRepository<Domain.Entities.DocumentEstateInquiry> _documentEstateInquiryRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        List<string> stringLst = new List<string>();
        string dealno = "";
        string dealdate = "";
        string notaryOfficeName = "";        
        public DealSummaryVerificationWithoutOwnerCheckingQueryHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IEstateInquiryRepository estateInquiryRepository, IDealSummaryTransferTypeRepository dealSummaryTransferTypeRepository, IDealSummaryRepository dealSummaryRepository, IRepository<Domain.Entities.DocumentEstateInquiry> documentEstateInquiryRepository, IDateTimeService dateTimeService) : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _estateInquiryRepository = estateInquiryRepository;
            _dealSummaryTransferTypeRepository = dealSummaryTransferTypeRepository;
            _dealSummaryRepository = dealSummaryRepository;
            _documentEstateInquiryRepository = documentEstateInquiryRepository;
            _dateTimeService = dateTimeService;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected async override Task<ApiResult<DealSummaryVerificationResultViewModel>>  RunAsync(DealSummaryVerificationWithoutOwnerCheckingQuery<DealSummaryVerificationResultViewModel> input, CancellationToken cancellationToken)
        {
            ApiResult<DealSummaryVerificationResultViewModel> msg = new ApiResult<DealSummaryVerificationResultViewModel>() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            msg.Data = new DealSummaryVerificationResultViewModel();
            bool isResend = (input.Tag != null && input.Tag.ToString().Equals("resend", StringComparison.OrdinalIgnoreCase)) ? true : false;
            if (!isResend)
            {
                foreach (DSUDealSummaryObject dsu in input.DsuDealSummary)
                {
                    
                    var data = await _dealSummaryRepository.TableNoTracking.Where(x => x.NotaryDocumentId == dsu.NotaryDocumentNo || x.NewNotaryDocumentId == dsu.NotaryDocumentNo.ToGuid()).ToListAsync(cancellationToken);
                    if (data != null && data.Count > 0)
                    {
                        isResend = true;
                        break;
                    }
                }
            }
            if (isResend)
            {
                if (input.Tag == null || (!input.Tag.ToString().Equals("resend", StringComparison.OrdinalIgnoreCase)))
                    input.Tag = "resend";
            }
            msg.Data.Result = true;
            if (await IsExistsMoreThanOneOwnershipDealsummary(input.DsuDealSummary, cancellationToken))
            {
                msg.Data.Result = false;
                msg.Data.ErrorMessage = "در لیست ورودی بیش از یک خلاصه معامله مالکیت بروی یک استعلام ثبت شده است";
            }
            foreach (DSUDealSummaryObject deal in input.DsuDealSummary)
            {
                if (deal.Id != null || deal.SENDINGSTATUS.HasValue)
                {
                    msg.Data.Result = false;
                    msg.Data.ErrorMessage = "خلاصه معامله در وضعیت جدید قرار ندارد";
                    break;
                }                
                if (string.IsNullOrEmpty(deal.ESTEstateInquiryId))
                {
                    msg.Data.Result = false;
                    msg.Data.ErrorMessage = "استعلام مربوط به خلاصه معامله مشخص نشده است";
                    break;
                }
                if (await MandatoryFieldsValidation(deal,  cancellationToken))
                {
                    if (FieldsFormatValidation(deal))
                    {
                        if (await DealSummaryInquiryValidation(deal, deal.ESTEstateInquiryId.Trim(), cancellationToken, true, input.Tag))
                        {
                            if (await DealSummaryPersonValidation(deal, cancellationToken))
                            {
                                if (deal.TheDSUTransferType.IsRestricted == "0")
                                {                                    
                                    if (!await HasNoneRestrictDsudealsummary(deal.ESTEstateInquiryId, cancellationToken))
                                    {
                                        var (result, inquiryNo, inquiryDate) = await VerifyResponse(deal.ESTEstateInquiryId, cancellationToken);
                                        if (result != 1)
                                        {
                                            msg.Data.Result = false;
                                            msg.Data.ErrorMessage = string.Format("امضای دیجیتال پاسخ استعلام به شماره {0} و تاریخ {1}  تایید نشد", inquiryNo, inquiryDate);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        msg.Data.Result = false;
                                        msg.Data.ErrorMessage = notaryOfficeName + string.Format(" خلاصه معامله قطعی به شماره {0} و به تاریخ {1} برای این ملک و مالکیت ارسال نموده است ", dealno, dealdate);
                                        break;
                                    }
                                }
                                else
                                {
                                    var (result, inquiryNo, inquiryDate) = await VerifyResponse(deal.ESTEstateInquiryId, cancellationToken);
                                    if (result != 1)
                                    {
                                        msg.Data.Result = false;
                                        msg.Data.ErrorMessage = string.Format("امضای دیجیتال پاسخ استعلام به شماره {0} و تاریخ {1}  تایید نشد", inquiryNo, inquiryDate);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                string errorMsg = GetErrorMessage(stringLst);                                
                                msg.Data.Result = false;
                                msg.Data.ErrorMessage = errorMsg;
                                break;
                            }

                        }
                        else
                        {
                            string errorMsg = GetErrorMessage(stringLst);                            
                            msg.Data.Result = false;
                            msg.Data.ErrorMessage = errorMsg;
                            break;
                        }
                    }
                    else
                    {
                        string errorMsg = GetErrorMessage(stringLst);                        
                        msg.Data.Result = false;
                        msg.Data.ErrorMessage = errorMsg;
                        break;
                    }
                }
                else
                {
                    string errorMsg = GetErrorMessage(stringLst);                    
                    msg.Data.Result = false;
                    msg.Data.ErrorMessage = errorMsg;
                    break;
                }
            }
            if (!msg.Data.Result)
            {
                msg.IsSuccess = false;
                msg.message.Add(msg.Data.ErrorMessage);
            }
            return msg;
        }
        protected override bool HasAccess(DealSummaryVerificationWithoutOwnerCheckingQuery<DealSummaryVerificationResultViewModel> request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
        public async Task<bool> MandatoryFieldsValidation(DSUDealSummaryObject dealsummary, CancellationToken cancellationToken)
        {
            bool result = true;
            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, dealsummary.ESTEstateInquiryId.ToGuid());
            if (string.IsNullOrEmpty(dealsummary.NotaryDocumentNo))
            {
                result = false;
                string s = "شماره سند خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.DealNo))
            {
                result = false;
                string s = "شماره  سند خلاصه معامله خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.DealDate))
            {
                result = false;
                string s = "تاریخ خلاصه معامله خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.DealMainDate))
            {
                result = false;
                string s = "تاریخ انجام معامله خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.DSUTransferTypeId))
            {
                result = false;
                string s = "نوع انتقال خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.ESTEstateInquiryId))
            {
                result = false;
                string s = "استعلام مربوط به خلاصه معامله خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.DealSummeryIssueeId))
            {
                result = false;
                string s = "واحد ثبتی خلاصه معامله مشخص نشده است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.Basic))
            {
                result = false;
                string s = "پلاک اصلی ملک خالی است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.SectionId))
            {
                result = false;
                string s = "آیتم بخش  مربوط به ملک مشخص نشده است";
                stringLst.Add(s);
            }
            if (string.IsNullOrEmpty(dealsummary.SubsectionId))
            {
                result = false;
                string s = "آیتم ناحیه مربوط به ملک مشخص نشده است";
                stringLst.Add(s);
            }
            if (!dealsummary.Area.HasValue)
            {
                result = false;
                string s = "مساحت ملک خالی است";
                stringLst.Add(s);
            }
            string dno, ddate;
            if (estateInquiry.EstateInquiryTypeId == "1")
            {
                if (string.IsNullOrEmpty(dealsummary.PrintNumberDoc))
                {
                    result = false;
                    string s = "شماره چاپی سند خالی است";
                    stringLst.Add(s);
                }
                if (string.IsNullOrEmpty(dealsummary.PageNoteSystemNo))
                {
                    if (string.IsNullOrEmpty(dealsummary.RegisterNo))
                    {
                        result = false;
                        string s = "شماره ثبت خالی است";
                        stringLst.Add(s);
                    }
                    if (string.IsNullOrEmpty(dealsummary.PageNo))
                    {
                        result = false;
                        string s = "شماره صفحه خالی است";
                        stringLst.Add(s);
                    }
                    if (string.IsNullOrEmpty(dealsummary.OfficeNo))
                    {
                        result = false;
                        string s = "شماره دفتر خالی است";
                        stringLst.Add(s);
                    }
                    if (string.IsNullOrEmpty(dealsummary.BSTSeriDaftarId))
                    {
                        result = false;
                        string s = "سری دفتر مشخص نشده است";
                        stringLst.Add(s);
                    }
                }
            }
            var TransferType = await _dealSummaryTransferTypeRepository.GetAsync(x => x.Id == dealsummary.DSUTransferTypeId || x.LegacyId == dealsummary.DSUTransferTypeId, cancellationToken);
            if (TransferType.Isrestricted == "1" && TransferType.Code != "239")
            {
                bool bvTmp = false;
                if (dealsummary.Amount.HasValue && !string.IsNullOrEmpty(dealsummary.AmountUnitId))
                {
                    bvTmp = true;
                }
                if (dealsummary.Duration.HasValue && !string.IsNullOrEmpty(dealsummary.TimeUnitId))
                {
                    bvTmp = true;
                }
                if (!bvTmp)
                {
                    result = false;
                    if (!dealsummary.Duration.HasValue)
                    {
                        stringLst.Add("آیتم مدت خلاصه معامله خالی است");
                    }
                    if (string.IsNullOrEmpty(dealsummary.TimeUnitId))
                    {
                        stringLst.Add("آیتم واحد زمان خلاصه معامله خالی است");
                    }
                    if (!dealsummary.Amount.HasValue)
                    {
                        stringLst.Add("آیتم مبلغ خلاصه معامله خالی است");
                    }
                    if (string.IsNullOrEmpty(dealsummary.AmountUnitId))
                    {
                        stringLst.Add("آیتم واحد مبلغ خلاصه معامله خالی است");
                    }
                }
            }
            if (dealsummary.TheDSURealLegalPersonList.Count == 0)
            {
                result = false;
                string s = "خلاصه معامله شخص ( معامل،متعامل،ذینفع ) ندارد";
                stringLst.Add(s);
            }
            return result;
        }
        public  bool FieldsFormatValidation(DSUDealSummaryObject dealsummary)
        {
            bool result = true;
            try
            {
                long d = Convert.ToInt64(dealsummary.DealNo);

                if (d <= 0)
                {
                    stringLst.Add("شماره خلاصه معامله نمی تواند صفر یا کوچکتر از صفر باشد");
                    result = false;
                }
            }
            catch
            {
                stringLst.Add("شماره خلاصه معامله باید عدد بزرگتر از صفر باشد");
                result = false;
            }
            if (!string.IsNullOrEmpty(dealsummary.PostalCode) && !CheckPostalCode(dealsummary.PostalCode))
            {
                result = false;
                stringLst.Add("کدپستی ملک باید یک عدد ده رقمی باشد");
            }
            return result;
        }
        private static bool CheckPostalCode(string value)
        {
            bool result = true;
            if (value != null)
            {
                System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"^([0-9]{10})$");
                if (!regEx.IsMatch(value))
                {
                    result = false;
                }
            }
            return result;
        }
        public async Task<bool> DealSummaryInquiryValidation(DSUDealSummaryObject dealsummary, string inquiryId, CancellationToken cancellationToken, bool tag = false, object flag = null)
        {
            bool result = true;
            if (flag != null && flag.ToString().Equals("resend", StringComparison.OrdinalIgnoreCase))
            {
                return result;
            }
            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, dealsummary.ESTEstateInquiryId.ToGuid());
            await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);
            await _estateInquiryRepository.LoadReferenceAsync(estateInquiry, x => x.WorkflowStates, cancellationToken);
            var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { estateInquiry.UnitId }, cancellationToken);
            var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { estateInquiry.ScriptoriumId }, cancellationToken);
            var TransferType = await _dealSummaryTransferTypeRepository.GetAsync(x => x.Id == dealsummary.DSUTransferTypeId || x.LegacyId == dealsummary.DSUTransferTypeId, cancellationToken);
            if (await IsInquiryUsedInSeparation(inquiryId, estateInquiry.ScriptoriumId, cancellationToken))
            {
                stringLst.Add("استعلام در سند تقسیم نامه استفاده شده است");
                result = false;
                return result;
            }
            if (TransferType.State == null || TransferType.State != "1")
            {
                stringLst.Add("نوع انتقال فعال نیست");
                result = false;
            }
            if (estateInquiry.SpecificStatus == "1")
            {
                stringLst.Add("ملک مربوط به استعلام مورد افراز، تفکیک و یا تجمیع قرار گرفته است");
            }
            if (estateInquiry.SpecificStatus == "2")
            {
                stringLst.Add("مالکیت ملک مورد استعلام بازداشت است");
            }
            if (estateInquiry.SpecificStatus == "3")
            {
                stringLst.Add("ملک مربوط به استعلام مورد افراز، تفکیک و یا تجمیع قرار گرفته است");
                stringLst.Add("مالکیت ملک مورد استعلام بازداشت است");
            }
            if (result)
            {
                if (TransferType.Isrestricted == "0")
                {
                    if (unit.UnitList[0].GeoLocationId == scriptorium.ScriptoriumList[0].GeoLocationId)
                    {
                        if (await HasConstraintDealsummary(dealsummary.ESTEstateInquiryId, cancellationToken))
                        {
                            result = false;
                            stringLst.Add(string.Format("برای استعلام شماره {0} و تاریخ {1} قبلا خلاصه معامله محدودیت ثبت شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                        }
                        else
                            if (estateInquiry.AttachedToDealsummary == "1")
                        {
                            result = false;
                            stringLst.Add(string.Format("برای استعلام به شماره {0} و تاریخ {1} ، قبلا خلاصه معامله دیگری ثبت  و ارسال شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));

                        }
                        else if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "31" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.EstateInquiryPeople.First().ExecutiveTransfer == EstateConstant.BooleanConstant.True && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == "2"))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 37)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.ResponseResult == "True" && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || (estateInquiry.SpecificStatus != "2" && estateInquiry.SpecificStatus != "1" && estateInquiry.SpecificStatus != "3")))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 37)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else
                        {
                            result = false;
                            stringLst.Add(string.Format("روی استعلام شماره {0} و تاریخ {1} نمی توانید خلاصه معامله ای تنظیم کنید چون این استعلام یا پاسخ ندارد و یا پاسخ تایید نگرفته و یا مالکیت ملک مورد بازداشت است و یا ملک مورد افراز ، تفکیک و یا تجمیع قرار گرفته  است ", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                        }
                    }
                    else
                    {
                        if (await HasConstraintDealsummary(dealsummary.ESTEstateInquiryId, cancellationToken))
                        {
                            result = false;
                            stringLst.Add(string.Format("برای استعلام شماره {0} و تاریخ {1} قبلا خلاصه معامله محدودیت ثبت شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                        }
                        else

                            if (estateInquiry.AttachedToDealsummary == "1")
                        {
                            result = false;
                            stringLst.Add(string.Format("برای استعلام به شماره {0} و تاریخ {1} ، قبلا خلاصه معامله دیگری ثبت  و ارسال شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));

                        }
                        else if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "31" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.EstateInquiryPeople.First().ExecutiveTransfer == EstateConstant.BooleanConstant.True && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == "2"))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 50)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.ResponseResult == "True" && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || (estateInquiry.SpecificStatus != "2" && estateInquiry.SpecificStatus != "1" && estateInquiry.SpecificStatus != "3")))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 50)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else
                        {
                            result = false;
                            stringLst.Add(string.Format(" روی استعلام شماره {0} و تاریخ {1} نمی توانید خلاصه معامله ای تنظیم کنید چون این استعلام یا پاسخ ندارد و یا پاسخ تایید نگرفته است و یا مالکیت ملک مورد بازداشت است و یا ملک مورد افراز ، تفکیک و یا تجمیع قرار گرفته است ", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                        }
                    }
                }
                else if (TransferType.Isrestricted == "1")
                {
                    if (unit.UnitList[0].GeoLocationId == scriptorium.ScriptoriumList[0].GeoLocationId)
                    {
                        if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "31" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.EstateInquiryPeople.First().ExecutiveTransfer == EstateConstant.BooleanConstant.True && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == "2"))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 37)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.ResponseResult == "True" && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || (estateInquiry.SpecificStatus != "2" && estateInquiry.SpecificStatus != "1" && estateInquiry.SpecificStatus != "3")))
                        {

                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 37)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else
                        {
                            result = false;
                            stringLst.Add(string.Format("روی استعلام شماره {0} و تاریخ {1} نمی توانید خلاصه معامله ای تنظیم کنید چون این استعلام یا پاسخ ندارد و یا پاسخ تایید نگرفته و یا مالکیت ملک مورد بازداشت است و یا ملک مورد افراز ، تفکیک و یا تجمیع قرار گرفته  است ", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                        }
                    }
                    else
                    {
                        if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "31" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.EstateInquiryPeople.First().ExecutiveTransfer == EstateConstant.BooleanConstant.True && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == "2"))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');
                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 50)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else if ((estateInquiry.WorkflowStates.State == "3" || estateInquiry.WorkflowStates.State == "4") && estateInquiry.ResponseResult == "True" && (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || (estateInquiry.SpecificStatus != "2" && estateInquiry.SpecificStatus != "1" && estateInquiry.SpecificStatus != "3")))
                        {
                            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                            string[] sa1 = estateInquiry.ResponseDate.Substring(0, 10).Split('/');

                            DateTime dtNow = DateTime.Now;
                            if (!string.IsNullOrEmpty(dealsummary.SignDate))
                            {
                                string[] saSignDate = dealsummary.SignDate.Split('/');
                                dtNow = pc.ToDateTime(Convert.ToInt32(saSignDate[0]), Convert.ToInt32(saSignDate[1]), Convert.ToInt32(saSignDate[2]), 0, 0, 0, 0);
                            }
                            DateTime responsedate = pc.ToDateTime(Convert.ToInt32(sa1[0]), Convert.ToInt32(sa1[1]), Convert.ToInt32(sa1[2]), 0, 0, 0, 0);
                            if (dtNow.Subtract(responsedate).TotalDays > 50)
                            {
                                result = false;
                                stringLst.Add(string.Format("اعتبار استعلام  شماره {0} و تاریخ {1} برای خلاصه معامله زدن تمام شده است", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                            }
                        }
                        else
                        {
                            result = false;
                            stringLst.Add(string.Format(" روی استعلام شماره {0} و تاریخ {1} نمی توانید خلاصه معامله ای تنظیم کنید چون این استعلام یا پاسخ ندارد و یا پاسخ تایید نگرفته است و یا مالکیت ملک مورد بازداشت است و یا ملک مورد افراز ، تفکیک و یا تجمیع قرار گرفته است ", estateInquiry.InquiryNo, estateInquiry.InquiryDate));
                        }
                    }
                }
            }
            if (result)
            {
                int oc = 0;
                int mc = 0;
                if (TransferType.Isrestricted == "0")
                {
                    foreach (DSURealLegalPersonObject rlp in dealsummary.TheDSURealLegalPersonList)
                    {
                        if (rlp.DSURelationKindId == "100")
                        {
                            oc++;
                        }
                        if (rlp.DSURelationKindId == "101")
                        {
                            mc++;
                        }
                    }
                    if (oc == 0)
                    {
                        result = false;
                        stringLst.Add("خلاصه معامله معامل ( فروشنده) ندارد");
                    }
                    if (oc > 1)
                    {
                        result = false;
                        stringLst.Add("خلاصه معامله بیش از یک معامل نمی تواند داشته باشد");
                    }
                    if (mc == 0)
                    {
                        result = false;
                        stringLst.Add("خلاصه معامله متعامل ( خریدار) ندارد");
                    }
                }
                if (TransferType.Isrestricted == "1")
                {
                    DSURealLegalPersonObject realLegalPerson = null;
                    foreach (DSURealLegalPersonObject rlp in dealsummary.TheDSURealLegalPersonList)
                    {
                        if (rlp.DSURelationKindId == "102")
                        {
                            realLegalPerson = rlp;
                            oc++;
                        }
                        if (!tag)
                        {
                            if (rlp.DSURelationKindId == "100")
                            {
                                mc++;
                            }
                        }
                        if (rlp.DSURelationKindId == "101")
                        {
                            result = false;
                            stringLst.Add("خلاصه معامله محدودیت نمی تواند شامل  متعامل باشد");
                        }
                    }
                    if (result)
                    {
                        if (oc == 0)
                        {
                            result = false;
                            stringLst.Add("خلاصه معامله ذینفع ندارد");
                        }
                        if (!tag && mc == 0)
                        {
                            result = false;
                            stringLst.Add("خلاصه معامله مالک ( معامل) ندارد");
                        }
                        if (oc > 1)
                        {
                            result = false;
                            stringLst.Add("خلاصه معامله بیش از یک ذینفع نمی تواند داشته باشد");
                        }
                        if (mc > 1)
                        {
                            result = false;
                            stringLst.Add("خلاصه معامله نمی تواند بیش از یک مالک داشته باشد");
                        }
                        string count = null;
                        if (await HasNotSendedOwnerShipDealSummary(inquiryId, cancellationToken))
                        {
                            result = false;
                            stringLst.Add("برای این استعلام، قبلا خلاصه معامله ای از نوع انتقال مالکیت ثبت کرده اید که هنوز آن را ارسال نکرده اید،اول آن را ارسال کنید و بعد این خلاصه معامله را ارسال کنید");
                        }                        
                    }
                }
                if (result)
                {
                    var userInfo = _configuration.GetSection("Validate_Inquiry_For_DealSummary_Service_User").Get<ValidateInquiryForDealSummaryServiceUser>();
                    var organization = await _baseInfoServiceHelper.GetOrganizationByUnitId(new string[] { estateInquiry.UnitId }, cancellationToken);
                    var input = new ValidateInquiryForDealSummaryInput()
                    {
                        ConsumerPassword = userInfo.Password,
                        ConsumerUsername = userInfo.UserName,
                        InquiryId = !string.IsNullOrWhiteSpace(estateInquiry.LegacyId) ? estateInquiry.LegacyId : estateInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper(),
                        ReceiverCmsorganizationId = organization.OrganizationList[0].LegacyId
                    };
                    var output = await _mediator.Send(input, cancellationToken);
                    if (output.IsSuccess)
                    {
                        if (output.Data.IsValid != 1)
                        {
                            result = false;
                            stringLst.Add(output.Data.ErrorMessage);
                        }
                    }
                    else
                    {
                        result = false;
                    }
                    
                    if (!await CheckInquiryInfo(dealsummary, cancellationToken))
                    {
                        result = false;
                        stringLst.Add("مشخصات خلاصه معامله با مشخصه معادل آن در استعلام همخوانی ندارد");
                    }
                }

            }

            return result;
        }

        

        private async Task<bool> HasNotSendedOwnerShipDealSummary(string inquiryid, CancellationToken cancellationToken)
        {
            bool result = false;
            var dealsummary = await _dealSummaryRepository.GetAsync(x => x.DealSummaryTransferType.Isrestricted == "0" && x.WorkflowStates.State == "0" && x.EstateInquiryId == inquiryid.ToGuid(), cancellationToken);

            if (dealsummary != null)
            {
                result = true;
            }

            return result;
        }
        private async Task<bool> CheckInquiryInfo(DSUDealSummaryObject dealsummary, CancellationToken cancellationToken)
        {
            bool result = true;
            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, dealsummary.ESTEstateInquiryId.ToGuid());

            if (dealsummary.DealSummeryIssuerId != null && dealsummary.DealSummeryIssuerId != estateInquiry.ScriptoriumId)
                result = false;
            else
                if (dealsummary.DealSummeryIssuerId == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.ScriptoriumId))
                    result = false;
            }

            if (dealsummary.DealSummeryIssueeId != null && dealsummary.DealSummeryIssueeId != estateInquiry.UnitId)
                result = false;
            else if (dealsummary.DealSummeryIssueeId == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.UnitId))
                    result = false;
            }

            if (dealsummary.Basic != null && dealsummary.Basic != estateInquiry.Basic)
                result = false;
            else if (dealsummary.Basic == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.Basic))
                    result = false;
            }

            if (dealsummary.BasicAppendant != null && dealsummary.BasicAppendant != estateInquiry.BasicRemaining)
                result = false;
            else if (dealsummary.BasicAppendant == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.BasicRemaining))
                    result = false;
            }

            if (dealsummary.Secondary != null && dealsummary.Secondary != estateInquiry.Secondary)
                result = false;
            else if (dealsummary.Secondary == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.Secondary))
                    result = false;
            }

            if (dealsummary.SecondaryAppendant != null && dealsummary.SecondaryAppendant != estateInquiry.SecondaryRemaining)
                result = false;
            else if (dealsummary.SecondaryAppendant == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.SecondaryRemaining))
                    result = false;
            }

            if (dealsummary.GeoLocationId != null && dealsummary.GeoLocationId != estateInquiry.GeoLocationId.ToString())
                result = false;
            else if (dealsummary.GeoLocationId == null)
            {
                if (estateInquiry.GeoLocationId.HasValue)
                    result = false;
            }

            if (dealsummary.SectionId != null && dealsummary.SectionId != estateInquiry.EstateSectionId)
                result = false;
            else if (dealsummary.SectionId == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.EstateSectionId))
                    result = false;
            }

            if (dealsummary.SubsectionId != null && dealsummary.SubsectionId != estateInquiry.EstateSubsectionId)
                result = false;
            else if (dealsummary.SubsectionId == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.EstateSubsectionId))
                    result = false;
            }

            if (dealsummary.PostalCode != null && dealsummary.PostalCode != estateInquiry.EstatePostalCode)
                result = false;
            else if (dealsummary.PostalCode == null)
            {
                if (!string.IsNullOrEmpty(estateInquiry.EstatePostalCode))
                    result = false;
            }

            if (dealsummary.Area.HasValue && dealsummary.Area.Value != ((estateInquiry.Area.HasValue) ? estateInquiry.Area : 0))
                result = false;
            else if (!dealsummary.Area.HasValue)
            {
                if (estateInquiry.Area.HasValue)
                    result = false;
            }
            if (estateInquiry.EstateInquiryTypeId == "1")
            {
                if (dealsummary.RegisterNo != null && dealsummary.RegisterNo != estateInquiry.RegisterNo)
                    result = false;
                else if (dealsummary.RegisterNo == null)
                {
                    if (!string.IsNullOrEmpty(estateInquiry.RegisterNo))
                        result = false;
                }

                if (dealsummary.PrintNumberDoc != null && dealsummary.PrintNumberDoc != estateInquiry.DocPrintNo)
                    result = false;
                else if (dealsummary.PrintNumberDoc == null)
                {
                    if (!string.IsNullOrEmpty(estateInquiry.DocPrintNo))
                        result = false;
                }

                if (dealsummary.PageNo != null && dealsummary.PageNo != estateInquiry.PageNo)
                    result = false;
                else if (dealsummary.PageNo == null)
                {
                    if (!string.IsNullOrEmpty(estateInquiry.PageNo))
                        result = false;
                }

                if (dealsummary.OfficeNo != null && dealsummary.OfficeNo != estateInquiry.EstateNoteNo)
                    result = false;
                else if (dealsummary.OfficeNo == null)
                {
                    if (!string.IsNullOrEmpty(estateInquiry.EstateNoteNo))
                        result = false;
                }

                if (dealsummary.BSTSeriDaftarId != null && dealsummary.BSTSeriDaftarId != estateInquiry.EstateSeridaftarId)
                    result = false;
                else if (dealsummary.BSTSeriDaftarId == null)
                {
                    if (!string.IsNullOrEmpty(estateInquiry.EstateSeridaftarId))
                        result = false;
                }

                if (dealsummary.PageNoteSystemNo != null && dealsummary.PageNoteSystemNo != estateInquiry.ElectronicEstateNoteNo)
                    result = false;
                else if (dealsummary.PageNoteSystemNo == null)
                {
                    if (!string.IsNullOrEmpty(estateInquiry.ElectronicEstateNoteNo))
                        result = false;
                }
            }

            return result;
        }
        private async Task<bool> HasConstraintDealsummary(string inquiryId, CancellationToken cancellationToken)
        {
            bool b = false;
            string[] states = new string[] { "3", "4" };
            var dealsummary = await _dealSummaryRepository.GetAsync(x => x.EstateInquiryId == inquiryId.ToGuid() && x.DealSummaryTransferType.Isrestricted == "1" && (x.WorkflowStates.State == "0" || x.WorkflowStates.State == "1" || x.WorkflowStates.State == "7" || (states.Contains(x.WorkflowStates.State) && (x.Response.Contains("مورد تایید می باشد") || x.Response.Contains("مورد تاييد مي باشد")))), cancellationToken);

            if (dealsummary != null)
            {
                b = true;
            }
            return b;
        }

        public async Task<bool> IsInquiryUsedInSeparation(string inquiryId, string scriptoriumId, CancellationToken cancellationToken)
        {
            var stateList = new string[] { "8", "9" };
            var DocumentTypes = new string[] { "611", "612" };
            var currentRequest = await _documentEstateInquiryRepository.
                  TableNoTracking
                  .Where(x => x.EstateInquiryId == inquiryId.ToGuid() && !stateList.Contains(x.DocumentEstate.Document.State) && x.DocumentEstate.Document.ScriptoriumId == scriptoriumId && DocumentTypes.Contains(x.DocumentEstate.Document.DocumentType.Code))
                  .Select(x => x.DocumentEstate.Document.RequestNo)
                  .ToListAsync(cancellationToken);
            if (currentRequest != null && currentRequest.Count > 0)
                return true;
            return false;
        }

        public async Task<bool> IsExistsMoreThanOneOwnershipDealsummary(List<DSUDealSummaryObject> dealsummaryList, CancellationToken cancellationToken)
        {
            bool Result = false;
            Dictionary<string, DealSummaryTransferType> transferTypes = new Dictionary<string, DealSummaryTransferType>();

            int k = 1;
            foreach (DSUDealSummaryObject obj in dealsummaryList)
            {
                obj.Id = k.ToString();
                k++;
            }
            foreach (DSUDealSummaryObject dsu in dealsummaryList)
            {
                var dstt = await _dealSummaryTransferTypeRepository.GetAsync(x => x.Id == dsu.DSUTransferTypeId || x.LegacyId == dsu.DSUTransferTypeId, cancellationToken);
                transferTypes.Add(dsu.Id, dstt);
                if (dsu.TheDSUTransferType == null)
                    dsu.TheDSUTransferType = new DsuTransferTypeObject() { Code = dstt.Code, IsRestricted = dstt.Isrestricted, State = dstt.State, Title = dstt.Title };
            }
            foreach (DSUDealSummaryObject dsu in dealsummaryList)
            {
                if (transferTypes[dsu.Id].Isrestricted == "0")
                {
                    foreach (DSUDealSummaryObject dsu1 in dealsummaryList)
                    {
                        if (transferTypes[dsu1.Id].Isrestricted == "0" && dsu1.Id != dsu.Id && dsu.ESTEstateInquiryId == dsu1.ESTEstateInquiryId)
                        {
                            Result = true;
                        }
                    }
                }
            }

            foreach (DSUDealSummaryObject obj in dealsummaryList)
            {
                obj.Id = null;

            }
            return Result;
        }

        private static bool CheckNationalCode(string Value, bool IsLegalPersonNationalCode)
        {
            bool result = true;
            if (IsLegalPersonNationalCode)
            {
                System.Text.RegularExpressions.Regex regular1 = new System.Text.RegularExpressions.Regex(@"^(([0-9]{9})|([0-9]{11}))$");
                if (!regular1.IsMatch(Value))
                {
                    result = false;
                }
            }
            else
            {
                System.Text.RegularExpressions.Regex regular = new System.Text.RegularExpressions.Regex(@"^([0-9]{10})$");
                if (!regular.IsMatch(Value))
                {
                    result = false;
                }
            }
            return result;
        }
        public async Task<bool> DealSummaryPersonValidation(DSUDealSummaryObject dealsummary, CancellationToken cancellationToken)
        {
            bool result = true;
            var transferType = await _dealSummaryTransferTypeRepository.GetAsync(x => x.Id == dealsummary.DSUTransferTypeId || x.LegacyId == dealsummary.DSUTransferTypeId, cancellationToken);


            foreach (DSURealLegalPersonObject rlp in dealsummary.TheDSURealLegalPersonList)
            {
                if (!rlp.personType.HasValue)
                {
                    result = false;
                    stringLst.Add("نوع شخص ( حقیقی یا حقوقی ) مشخص نشده است");
                    break;
                }
                if (rlp.personType.Value == 1)
                {
                    if (rlp.DSURelationKindId == "101" || rlp.DSURelationKindId == "102")

                    {
                        if (string.IsNullOrEmpty(rlp.NationalCode) || !CheckNationalCode(rlp.NationalCode, false))
                        {
                            result = false;
                            stringLst.Add("کدملی خالی می باشد و یا عدد ده رقمی نمی باشد");
                        }

                        if (string.IsNullOrEmpty(rlp.Name))
                        {
                            result = false;
                            stringLst.Add("نام متعامل/ذینفع نمی تواند خالی باشد");
                        }

                        if (string.IsNullOrEmpty(rlp.Family))
                        {
                            result = false;
                            stringLst.Add("نام خانوادگی متعامل/ذینفع نمی تواند خالی باشد");
                        }

                        if (string.IsNullOrEmpty(rlp.FatherName))
                        {
                            result = false;
                            stringLst.Add("نام پدر متعامل/ذینفع نمی تواند خالی باشد");
                        }

                        if (string.IsNullOrEmpty(rlp.BirthDate))
                        {
                            result = false;
                            stringLst.Add("تاریخ تولد متعامل/ذینفع نمی تواند خالی باشد");
                        }

                        //if (string.IsNullOrEmpty(rlp.BirthdateId) )
                        //{
                        //    result = false;
                        //    errorMessages.Add("محل تولد متعامل/ذینفع نمی تواند خالی باشد");
                        //}

                        if (string.IsNullOrEmpty(rlp.IssuePlaceId))
                        {
                            result = false;
                            stringLst.Add("محل صدور متعامل/ذینفع نمی تواند خالی باشد");
                        }

                        if (string.IsNullOrWhiteSpace(rlp.IdentificationNo))
                        {
                            result = false;
                            stringLst.Add("شماره شناسنامه متعامل/ذینفع نمی تواند خالی باشد");
                        }
                        if (string.IsNullOrEmpty(rlp.PostalCode))
                        {
                            result = false;
                            stringLst.Add("کد پستی متعامل/ذینفع نمی تواند خالی باشد");

                        }

                        if (!CheckPostalCode(rlp.PostalCode))
                        {
                            result = false;
                            stringLst.Add(" کد پستی متعامل/ذینفع باید یک عدد 10 رقمی باشد");

                        }
                        if (string.IsNullOrEmpty(rlp.CityId))
                        {
                            result = false;
                            stringLst.Add("شهر متعامل/ذینفع نمی تواند خالی باشد");
                        }


                        if (rlp.DSURelationKindId == "101" && string.IsNullOrEmpty(rlp.ShareContext) && (!rlp.SharePart.HasValue || !rlp.ShareTotal.HasValue))
                        {
                            result = false;
                            stringLst.Add("یکی از مقادیر جز سهم  کل سهم ( باهم) و یا متن سهم را برای متعامل/ذینفع وارد کنید");
                        }
                    }
                    else if (rlp.DSURelationKindId == "100")
                    {
                        if (string.IsNullOrEmpty(rlp.ShareContext) && (!rlp.SharePart.HasValue || !rlp.ShareTotal.HasValue))
                        {
                            result = false;
                            stringLst.Add("یکی از مقادیر جز سهم  کل سهم ( باهم) و یا متن سهم را برای معامل وارد کنید");
                        }
                    }
                }
                else
                if (rlp.personType.Value == 0)
                {
                    if (rlp.DSURelationKindId == "101" || rlp.DSURelationKindId == "102")
                    {
                        if (string.IsNullOrEmpty(rlp.Name))
                        {
                            result = false;
                            stringLst.Add("نام متعامل/ذینفع نمی تواند خالی باشد");
                        }

                        if (rlp.DSURelationKindId == "101" && string.IsNullOrEmpty(rlp.ShareContext) && (!rlp.SharePart.HasValue || !rlp.ShareTotal.HasValue))
                        {
                            result = false;
                            stringLst.Add("یکی از مقادیر جز سهم  کل سهم ( باهم) و یا متن سهم را برای متعامل/ذینفع وارد کنید");
                        }
                    }
                    else if (rlp.DSURelationKindId == "100")
                    {
                        if (string.IsNullOrEmpty(rlp.ShareContext) && (!rlp.SharePart.HasValue || !rlp.ShareTotal.HasValue))
                        {
                            result = false;
                            stringLst.Add("یکی از مقادیر جز سهم  کل سهم ( باهم) و یا متن سهم را برای معامل وارد کنید");
                        }
                    }
                }

                if (!result)
                    break;
            }
            bool tag = true;
            if (result)
            {

                foreach (DSURealLegalPersonObject rlp in dealsummary.TheDSURealLegalPersonList)
                {
                    if (rlp.DSURelationKindId == "100")
                    {
                        if (rlp.ShareContext == null || rlp.ShareContext == string.Empty)
                        {
                            if (!rlp.SharePart.HasValue || !rlp.ShareTotal.HasValue)
                            {
                                result = false;
                                stringLst.Add("جز سهم و کل سهم معامل نمی تواند خالی باشد");
                                break;
                            }

                            else if (rlp.SharePart.Value == 0 || rlp.ShareTotal.Value == 0)
                            {
                                result = false;
                                stringLst.Add("جز سهم و کل سهم معامل نمی تواند صفر  باشد");
                                break;
                            }
                            else
                                if (rlp.SharePart.Value > rlp.ShareTotal.Value)
                            {
                                result = false;
                                stringLst.Add("جز سهم معامل بیشتر از کل سهم آن است");
                                break;
                            }
                        }
                        else
                            tag = false;
                    }
                    if (rlp.DSURelationKindId == "101")
                    {
                        if (rlp.ShareContext == null || rlp.ShareContext == string.Empty)
                        {
                            if (!rlp.SharePart.HasValue || !rlp.ShareTotal.HasValue)
                            {
                                result = false;
                                stringLst.Add("جز سهم و کل سهم متعامل نمی تواند خالی باشد");
                                break;
                            }
                            else if (rlp.SharePart.Value == 0 || rlp.ShareTotal.Value == 0)
                            {
                                result = false;
                                stringLst.Add("جز سهم و کل سهم متعامل نمی تواند صفر  باشد");
                                break;
                            }
                            else
                                if (rlp.SharePart.Value > rlp.ShareTotal.Value)
                            {
                                result = false;
                                stringLst.Add("جز سهم متعامل بیشتر از کل سهم آن است");
                                break;
                            }
                        }
                        else
                            tag = false;
                    }                  
                }
            }
            if (tag && result)
            {
                if (transferType.Isrestricted == "0")
                {
                    RationalNumber trn1 = new RationalNumber();
                    List<RationalNumber> RNLst = new List<RationalNumber>();
                    foreach (DSURealLegalPersonObject rlp in dealsummary.TheDSURealLegalPersonList)
                    {
                        if (rlp.DSURelationKindId == "100")
                        {
                            trn1.S = Convert.ToDecimal(rlp.SharePart.Value);
                            trn1.M = Convert.ToDecimal(rlp.ShareTotal.Value);
                        }
                        if (rlp.DSURelationKindId == "101")
                        {
                            RNLst.Add(new RationalNumber(Convert.ToDecimal(rlp.SharePart.Value), Convert.ToDecimal(rlp.ShareTotal.Value)));
                        }

                    }
                    RationalNumber trn = RNLst[0];
                    bool r = false;
                    for (int k = 1; k < RNLst.Count; k++)
                    {
                        decimal[] da = Helper.Sum(trn.S, RNLst[k].S, trn.M, RNLst[k].M);
                        trn = new RationalNumber(da[0], da[1]);
                    }
                    double d1 = Convert.ToDouble(trn.S * trn1.M);
                    double d2 = Convert.ToDouble(trn1.S * trn.M);
                    if (d1 <= d2)
                    {
                        r = true;

                    }
                    else
                        r = false;
                    if (!r)
                    {
                        result = false;
                        stringLst.Add("مجموع سهم های متعاملین بیشتر از سهم مالک ( معامل ) می باشد. لطفا جزسهم و کل سهم متعاملین را اصلاح کنید.");
                    }
                }
            }
            return result;
        }
        public static string GetErrorMessage(List<string> errors)
        {
            string result = "";
            int c = 1;
            foreach (string str in errors)
            {
                result += c.ToString() + "-" + str + Environment.NewLine;
                c++;
            }

            return result;
        }
        public async Task<bool> HasNoneRestrictDsudealsummary(string inquiryid, CancellationToken cancellationToken)
        {
            bool result = false;            
            DateTime dtnow = DateTime.Now;           
            var cdt = _dateTimeService.CurrentPersianDateTime.Replace("-", " ");
            var cd = cdt.Substring(0, 10);
            var ct = cdt.Substring(11);
            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, inquiryid.ToGuid());
            System.Data.DataTable dt = new System.Data.DataTable();
            if (estateInquiry != null)
            {

                string rod = estateInquiry.RelatedOwnershipId;
                if (string.IsNullOrEmpty(rod)) return false;

                var query = _estateInquiryRepository.TableNoTracking.Where(x =>
                x.Id != inquiryid.ToGuid() &&
                x.RelatedOwnershipId == estateInquiry.RelatedOwnershipId &&
                x.UnitId == estateInquiry.UnitId &&
                x.EstateSectionId == estateInquiry.EstateSectionId &&
                x.EstateSubsectionId == estateInquiry.EstateSubsectionId &&
                x.Basic == estateInquiry.Basic &&
                x.Secondary == estateInquiry.Secondary &&
                x.DocPrintNo == estateInquiry.DocPrintNo);
                if (string.IsNullOrEmpty(estateInquiry.ElectronicEstateNoteNo))
                    query = query.Where(x => x.PageNo == estateInquiry.PageNo && x.EstateNoteNo == estateInquiry.EstateNoteNo && x.EstateSeridaftarId == estateInquiry.EstateSeridaftarId);
                else
                    query = query.Where(x => x.ElectronicEstateNoteNo == estateInquiry.ElectronicEstateNoteNo);
                var estateInquiryList = await query.ToListAsync(cancellationToken);
                if (estateInquiryList.Count > 0)
                {
                    var estateInquiryIdList = estateInquiryList.Select(x => x.Id).ToList();

                    string[] states = new string[] { "3", "4" };
                    var dealSummaryList = await _dealSummaryRepository
                        .TableNoTracking
                        .Where(d => estateInquiryIdList.Contains(d.EstateInquiryId) &&
                        (d.WorkflowStates.State == "1" || (states.Contains(d.WorkflowStates.State) && (d.Response.Contains("خلاصه معامله مورد تایید می باشد") || d.Response.Contains("خلاصه معامله مورد تاييد مي باشد")))) &&
                        d.SendDate.CompareTo(cd) <= 0 &&
                        d.SendTime.CompareTo(ct) <= 0 &&
                        d.DealSummaryTransferType.Isrestricted == "0").ToListAsync(cancellationToken);


                    if (dealSummaryList.Count > 0)
                    {
                        result = true;
                        dealdate = dealSummaryList[0].SendDate.ToString() + " " + dealSummaryList[0].SendTime.ToString();
                        dealno = dealSummaryList[0].DealNo.ToString();                        
                        notaryOfficeName = (await _baseInfoServiceHelper.GetScriptoriumById(new string[] { dealSummaryList[0].ScriptoriumId }, cancellationToken)).ScriptoriumList[0].Name;
                    }
                }
            }
            return result;
        }
        public async Task<(int success, string inquiryNo, string inquirydate)> VerifyResponse(string inquiryid, CancellationToken cancellationToken)
        {
            int result = 1;
            try
            {
                var estObject = await _estateInquiryRepository.GetByIdAsync(cancellationToken, inquiryid.ToGuid());

                if (estObject.ResponseDigitalsignature == null)
                {
                    result = -1;
                    return (result, estObject.InquiryNo, estObject.InquiryDate);
                }
                else return (result, estObject.InquiryNo, estObject.InquiryDate);

            }
            catch
            {
                result = -3;
            }
            return (result, "", "");
        }       
    }
}
