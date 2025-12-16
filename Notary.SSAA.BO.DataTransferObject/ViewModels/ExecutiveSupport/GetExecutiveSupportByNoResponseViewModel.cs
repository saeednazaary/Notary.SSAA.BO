using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport
{
    public class GetExecutiveSupportByNoResponseViewModel : BaseCommandRequest<ApiResult>
    {
        public GetExecutiveSupportByNoResponseViewModel()
        {
            ExecutionSupportiveTypeId = new List<string>();
            RequesterPersonId = new List<string>();
            ExecutiveSupportiveCases = new List<ExecutiveSupportCaseViewModel>();
            PersonAddressChangedId = new List<string>();
            AddressType = new List<string>();
        }

        [System.ComponentModel.DisplayName("شناسه یکتای درخواست تبعی")]
        [System.ComponentModel.Description("")]
        public string ExecutionSupportiveId { get; set; }
        [System.ComponentModel.DisplayName("شناسه دفتر خانه صادر کننده")]
        [System.ComponentModel.Description("شناسه دفتر خانه صادر کننده")]
        public string ExecutionSupportiveScriptoriumId { get; set; }
        [System.ComponentModel.DisplayName("شناسه یکتا")]
        [System.ComponentModel.Description("شناسه یکتا")]
        public string ExecutionSupportiveNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ درخواست")]
        [System.ComponentModel.Description("تاریخ درخواست")]
        public string ExecutionSupportiveRequestDate { get; set; }
        [System.ComponentModel.DisplayName("متن درخواست")]
        [System.ComponentModel.Description("متن درخواست")]
        public string ExecutionSupportiveRequestText { get; set; }
        [System.ComponentModel.DisplayName("نوع اجرائیه")]
        [System.ComponentModel.Description("نوع اجرائیه")]
        public string ExecutionTypeId { get; set; }
        [System.ComponentModel.DisplayName("شناسه نوع خدمات تبعی اجرائیه")]
        [System.ComponentModel.Description("شناسه نوع خدمات تبعی اجرائیه")]
        public IList<string> ExecutionSupportiveTypeId { get; set; }
        [System.ComponentModel.DisplayName("شناسه واحد ثبتی دریافت کننده درخواست")]
        [System.ComponentModel.Description("شناسه واحد ثبتی دریافت کننده درخواست")]
        public string ExecutionSupportiveUnitId { get; set; }
        [System.ComponentModel.DisplayName("شناسه واحد ثبتی پاسخ دهنده درخواست")]
        [System.ComponentModel.Description("شناسه واحد ثبتی پاسخ دهنده درخواست")]
        public string ExecutionSupportiveReplyUnitId { get; set; }
        [System.ComponentModel.DisplayName("شماره اجرائیه")]
        [System.ComponentModel.Description("شماره اجرائیه")]
        public string ExecutionNo { get; set; }
        [System.ComponentModel.DisplayName("شماره پرونده")]
        [System.ComponentModel.Description("شماره پرونده")]
        public string ExecutionCaseNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ اجرائیه")]
        [System.ComponentModel.Description("تاریخ اجرائیه")]
        public string ExecutionDate { get; set; }
        [System.ComponentModel.DisplayName("خلاصه اطلاعات اجرائیه")]
        [System.ComponentModel.Description("خلاصه اطلاعات اجرائیه")]
        public string ExecutionInfoText { get; set; }
        [System.ComponentModel.DisplayName("شناسه شخص درخواست دهنده")]
        [System.ComponentModel.Description("شناسه شخص درخواست دهنده")]
        public IList<string> RequesterPersonId { get; set; }
        [System.ComponentModel.DisplayName("شناسه شخصی که آدرسش عوض می شود")]
        [System.ComponentModel.Description("شناسه شخصی که آدرسش عوض می شود")]
        public IList<string> PersonAddressChangedId { get; set; }
        [System.ComponentModel.DisplayName("نوع نشانی اعلامی")]
        [System.ComponentModel.Description("نوع نشانی اعلامی")]
        public IList<string> AddressType { get; set; }
        [System.ComponentModel.DisplayName("نشانی")]
        [System.ComponentModel.Description("نشانی")]
        public string Address { get; set; }
        [System.ComponentModel.DisplayName("کد پستی")]
        [System.ComponentModel.Description("کد پستی")]
        public string PostCode { get; set; }
        [System.ComponentModel.DisplayName("آیا هزینه های ارائه خدمات پرداخت شده است؟")]
        [System.ComponentModel.Description("آیا هزینه های ارائه خدمات پرداخت شده است؟")]
        public bool IsPayCostConfirmed { get; set; }
        [System.ComponentModel.DisplayName("جمع هزینه های ارائه پرداخت")]
        [System.ComponentModel.Description("جمع هزینه های ارائه پرداخت")]
        public long SumPeices { get; set; }
        [System.ComponentModel.DisplayName("تاریخ پرداخت هزینه های ارائه خدمت")]
        [System.ComponentModel.Description("تاریخ پرداخت هزینه های ارائه خدمت")]
        public string PayCostDate { get; set; }
        [System.ComponentModel.DisplayName("زمان پرداخت هزینه های ارائه خدمت")]
        [System.ComponentModel.Description("زمان پرداخت هزینه های ارائه خدمت")]
        public string PeyCostTime { get; set; }
        [System.ComponentModel.DisplayName("شیوه پرداخت")]
        [System.ComponentModel.Description("شیوه پرداخت")]
        public string HowToPay { get; set; }
        [System.ComponentModel.DisplayName("نوع پرداخت")]
        [System.ComponentModel.Description("نوع پرداخت")]
        public string PaymentType { get; set; }
        [System.ComponentModel.DisplayName("شناسه قبض")]
        [System.ComponentModel.Description("شناسه قبض")]
        public string BillNo { get; set; }
        [System.ComponentModel.DisplayName("شناسه مرجع تراکنش")]
        [System.ComponentModel.Description("شناسه مرجع تراکنش")]
        public string ReceiptNo { get; set; }
        [System.ComponentModel.DisplayName("شماره فاکتور")]
        [System.ComponentModel.Description("شماره فاکتور")]
        public string FactorNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ فاکتور")]
        [System.ComponentModel.Description("تاریخ فاکتور")]
        public string FactorDate { get; set; }
        [System.ComponentModel.DisplayName("آیا وضعیت احراز هویت اشحاص با ثبت احوال و کنترل بخش نامه ها بررسی و رویت شده است؟")]
        [System.ComponentModel.Description("آیا وضعیت احراز هویت اشحاص با ثبت احوال و کنترل بخش نامه ها بررسی و رویت شده است؟")]
        public bool IsFinalVerificationVisited { get; set; }
        [System.ComponentModel.DisplayName("نام سردفتر")]
        [System.ComponentModel.Description("نام سردفتر")]
        public string SardaftarFullName { get; set; }
        [System.ComponentModel.DisplayName("تاریخ تایید سردفتر")]
        [System.ComponentModel.Description("تاریخ تایید سردفتر")]
        public string SardaftarConfirmationDate { get; set; }
        [System.ComponentModel.DisplayName("زمان تایید سردفتر")]
        [System.ComponentModel.Description("زمان تایید سردفتر")]
        public string SardaftarConfirmationTime { get; set; }
        [System.ComponentModel.DisplayName("شناسه گواهی امضا الکترونیک")]
        [System.ComponentModel.Description("شناسه گواهی امضا الکترونیک")]
        public string SignCertificateNo { get; set; }
        [System.ComponentModel.DisplayName("امضا الکترونیک")]
        [System.ComponentModel.Description("امضا الکترونیک")]
        public string DigitalSign { get; set; }
        [System.ComponentModel.DisplayName("متن پاسخ اداره اجرا")]
        [System.ComponentModel.Description("متن پاسخ اداره اجرا")]
        public string ExecutionReplyText { get; set; }
        [System.ComponentModel.DisplayName("تاریخ پاسخ اداره اجرا")]
        [System.ComponentModel.Description("تاریخ پاسخ اداره اجرا")]
        public string ExecutionReplyDate { get; set; }
        [System.ComponentModel.DisplayName("شماره نامه وارده از طرف اداره اجرا")]
        [System.ComponentModel.Description("شماره نامه وارده از طرف اداره اجرا")]
        public string ExecutionUnitInputLetterNo { get; set; }
        [System.ComponentModel.DisplayName("شماره نامه صادره از طرف اداره اجرا")]
        [System.ComponentModel.Description("شماره نامه صادره از طرف اداره اجرا")]
        public string ExecutionUnitOutputLetterNo { get; set; }
        [System.ComponentModel.DisplayName("نام و نام خانوادگی اخرین اصلاح کننده")]
        [System.ComponentModel.Description("نام و نام خانوادگی اخرین اصلاح کننده")]
        public string ModifierPersonFullName { get; set; }
        [System.ComponentModel.DisplayName("تارخ آخرین اصلاح")]
        [System.ComponentModel.Description("تارخ آخرین اصلاح")]
        public string ModifyDate { get; set; }
        [System.ComponentModel.DisplayName("زمان آخرین اصلاح")]
        [System.ComponentModel.Description("زمان آخرین اصلاح")]
        public string ModifyTime { get; set; }
        [System.ComponentModel.DisplayName("وضعیت")]
        [System.ComponentModel.Description("وضعیت")]
        public string ExecutionSupportiveState { get; set; }


        public List<ExecutiveSupportCaseViewModel> ExecutiveSupportiveCases { get; set; }


    }
}
