using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveRequest
{
    public class ExecutiveRequestViewModel : BaseCommandRequest<ApiResult>
    {
        public ExecutiveRequestViewModel()
        {
            ExecutiveRequestTypeId = new List<string>();
            ExecutiveRequestUnitId = new List<string>();
            ExecutiveRequestCurrencyTypeId= new List<string>();
            ExecutiveRequestScriptoriumId= new List<string>();
            ExecutiveRequestPersons = new List<ExecutiveRequestPersonViewModel>();
            ExecutiveRequestBindingSubjects = new List<ExecutiveRequestBindingSubjectViewModel>();
            ExecutiveRequestDocuments = new List<ExecutiveRequestDocumentViewModel>();
            ExecutiveRequestRelatedPersons=new List<ExecutiveRequestRelatedPersonViewModel>();
        }

        public bool IsValid { get; set; }

        public bool IsNew { get; set; }

        public bool IsDelete { get; set; }

        public bool IsDirty { get; set; }

        [System.ComponentModel.DisplayName("شناسه یکتای تقاضانامه")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestId { get; set; }

        [System.ComponentModel.DisplayName("شماره یکتای تقاضانامه")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestNo { get; set; }

        [System.ComponentModel.DisplayName("تعداد اوراق")]
        [System.ComponentModel.Description("")]
        public int ExecutiveRequestPaperCount { get; set; }

        [System.ComponentModel.DisplayName("شماره فاکتور")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestFactorNo { get; set; }

        [System.ComponentModel.DisplayName("تاریخ فاکتور")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestFactorDate { get; set; }

        [System.ComponentModel.DisplayName("تاریخ درخواست")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestDate { get; set; }

        [System.ComponentModel.DisplayName("وضعیت")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestState { get; set; }

        [System.ComponentModel.DisplayName("آیا تقاضانامه جاری اصلاح کننده تقاضانامه دیگری است؟")]
        [System.ComponentModel.Description("")]
        public bool IsCorrectiveOfAnotherReq { get; set; }

        [System.ComponentModel.DisplayName("عنوان تقاضاي صدور  اجرائيه")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestTitle { get; set; }

        [System.ComponentModel.DisplayName("جمع هزينه هاي ارائه خدمت")]
        [System.ComponentModel.Description("")]
        public decimal ExecutiveRequestSumPrice { get; set; }

        [System.ComponentModel.DisplayName("آيا وضعيت احراز هويت اشخاص با ثبت احوال و کنترل بخشنامه ها بررسي و رويت شده است؟")]
        [System.ComponentModel.Description("")]
        public bool IsExecutiveRequestFinalVerification { get; set; }

        [System.ComponentModel.DisplayName("آيا هزينه هاي ارائه خدمت پرداخت شده است؟")]
        [System.ComponentModel.Description("")]
        public bool IsExecutiveRequestPayCostConfirmed { get; set; }

        [System.ComponentModel.DisplayName("نوع پرداخت")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestPaymentType { get; set; }

        [System.ComponentModel.DisplayName("نوع اجرائیه")]
        [System.ComponentModel.Description("")]
        public IList<string> ExecutiveRequestTypeId { get; set; }

        [System.ComponentModel.DisplayName("عنوان نوع اجرائیه")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestTypeTitle { get; set; }

        [System.ComponentModel.DisplayName("واحد ثبتی دریافت کننده اجرائیه")]
        [System.ComponentModel.Description("")]
        public IList<string> ExecutiveRequestUnitId { get; set; }

        [System.ComponentModel.DisplayName("دلیل درخواست")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestReason { get; set; }

        [System.ComponentModel.DisplayName("شناسه واحدهاي اندازه گيري - واحد پولي")]
        [System.ComponentModel.Description("")]
        public IList<string> ExecutiveRequestCurrencyTypeId { get; set; }

        [System.ComponentModel.DisplayName("توضيحات دفترخانه")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestNotaryDescription { get; set; }

        [System.ComponentModel.DisplayName("هامش اداره اجرا")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestUnitDescription { get; set; }

        [System.ComponentModel.DisplayName("امضاي الکترونيک")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestDigitalSign { get; set; }

        [System.ComponentModel.DisplayName("شناسه گواهي امضاي الکترونيک")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestDigitalSignDN { get; set; }

        [System.ComponentModel.DisplayName("متن حقوقی")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestText { get; set; }

        [System.ComponentModel.DisplayName("مبلغ اجرائيه")]
        [System.ComponentModel.Description("")]
        public decimal ExecutiveRequestPrice { get; set; }

        [System.ComponentModel.DisplayName("شناسه قبض")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestBillNo { get; set; }

        [System.ComponentModel.DisplayName("شناسه مرجع تراکنش")]
        [System.ComponentModel.Description("")]
        public string ReceiptNo { get; set; }

        [System.ComponentModel.DisplayName("تاریخ پرداخت هزینه")]
        [System.ComponentModel.Description("")]
        public string PayCostDate { get; set; }

        [System.ComponentModel.DisplayName("زمان پرداخت هزینه ها")]
        [System.ComponentModel.Description("")]
        public string PayCostTime { get; set; }

        [System.ComponentModel.DisplayName("شیوه پرداخت")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestHowToPay { get; set; }

        [System.ComponentModel.DisplayName("نام و نام خانوادگي آخرين اصلاح کننده")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestModifier { get; set; }

        [System.ComponentModel.DisplayName("تاريخ آخرين اصلاح")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestModifyDate { get; set; }

        [System.ComponentModel.DisplayName("زمان آخرين اصلاح")]
        [System.ComponentModel.Description("")]
        public string ExecutiveRequestModifyTime { get; set; }

        [System.ComponentModel.DisplayName("نام و نام خانوادگي سردفتر")]
        [System.ComponentModel.Description("")]
        public string SardaftarNameFamily { get; set; }

        [System.ComponentModel.DisplayName("تاريخ تأييد سردفتر")]
        [System.ComponentModel.Description("")]
        public string SardaftarConfirmDate { get; set; }

        [System.ComponentModel.DisplayName("زمان تأييد سردفتر")]
        [System.ComponentModel.Description("")]
        public string SardaftarConfirmTime { get; set; }

        [System.ComponentModel.DisplayName("شناسه دفترخانه صادرکننده")]
        [System.ComponentModel.Description("")]
        public IList<string> ExecutiveRequestScriptoriumId { get; set; }

        [System.ComponentModel.DisplayName("اشخاص درخواست تقاضانامه")]
        [System.ComponentModel.Description("")]
        public IList<ExecutiveRequestPersonViewModel> ExecutiveRequestPersons { get; set; }

        [System.ComponentModel.DisplayName("موضوعات لازم الاجرای درخواست تقاضانامه")]
        [System.ComponentModel.Description("")]
        public IList<ExecutiveRequestBindingSubjectViewModel> ExecutiveRequestBindingSubjects { get; set; }

        [System.ComponentModel.DisplayName("اسناد درخواست تقاضانامه")]
        [System.ComponentModel.Description("")]
        public IList<ExecutiveRequestDocumentViewModel> ExecutiveRequestDocuments { get; set; }

        [System.ComponentModel.DisplayName("اشخاص وابسته درخواست تقاضانامه")]
        [System.ComponentModel.Description("")]
        public IList<ExecutiveRequestRelatedPersonViewModel> ExecutiveRequestRelatedPersons { get; set; }
    }
}
