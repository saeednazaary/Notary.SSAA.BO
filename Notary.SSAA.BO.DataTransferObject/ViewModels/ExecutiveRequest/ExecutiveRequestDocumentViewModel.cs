using Notary.SSAA.BO.DataTransferObject.Bases;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveRequest
{
    public class ExecutiveRequestDocumentViewModel : EntityState
    {

        public ExecutiveRequestDocumentViewModel()
        {
            AttachmentTypeId=new List<string>();
            DocumentBankId=new List<string>();
            DocumentMeasurementUnitTypeId=new List<string>();
        }

        [System.ComponentModel.DisplayName("شناسه تقاضانامه اجرائيه برون سپاري شده به دفترخانه")]
        [System.ComponentModel.Description("شناسه تقاضانامه اجرائيه برون سپاري شده به دفترخانه")]
        public string ExecutionRequestId { get; set; }

        [System.ComponentModel.DisplayName("رديف نوع سند يا پيوست")]
        [System.ComponentModel.Description("نوع سند")]
        public IList<string> AttachmentTypeId { get; set; }

        [System.ComponentModel.DisplayName("عنوان نوع سند يا پيوست")]
        [System.ComponentModel.Description("عنوان نوع سند")]
        public string AttachmentTitle { get; set; }

        [System.ComponentModel.DisplayName("شماره سند مربوط")]
        [System.ComponentModel.Description("شماره سند")]
        public string DocumentNo { get; set; }

        [System.ComponentModel.DisplayName("تاريخ سند مربوط")]
        [System.ComponentModel.Description("تاريخ سند")]
        public string DocumentDate { get; set; }

        [System.ComponentModel.DisplayName("رديف بانك")]
        [System.ComponentModel.Description("بانک")]
        public IList<string> DocumentBankId { get; set; }
        [System.ComponentModel.DisplayName("عنوان بانك")]
        [System.ComponentModel.Description("بانک")]
        public string DocumentBankTitle { get; set; }

        [System.ComponentModel.DisplayName("رديف")]
        [System.ComponentModel.Description("شناسه ردیف")]
        public string ExecutiveRequestDocumentId { get; set; }

        [System.ComponentModel.DisplayName("رديف انواع واحد اندازه گيري")]
        [System.ComponentModel.Description("واحد پولی")]
        public IList<string> DocumentMeasurementUnitTypeId { get; set; }

        [System.ComponentModel.DisplayName("عنوان انواع واحد اندازه گيري")]
        [System.ComponentModel.Description("واحد پولی")]
        public string DocumentMeasurementUnitTypeTitle{ get; set; }

        //FieldValue1
        [System.ComponentModel.DisplayName("اولين مقدار")]
        [System.ComponentModel.Description("وجه چک")]
        public decimal ChequeAmount { get; set; }

        //FieldDate1
        [System.ComponentModel.DisplayName("اولين تاريخ")]
        [System.ComponentModel.Description("تاریخ برگشتی بانک در مورد چک")]
        public string ChequeReturnedDate { get; set; }

        //FieldDesc1
        [System.ComponentModel.DisplayName("اولين شرح")]
        [System.ComponentModel.Description("شعبه بانک برگشت زننده چک")]
        public string ChequeReturnedBranch { get; set; }

        //FieldValue2
        [System.ComponentModel.DisplayName("دومين مقدار")]
        [System.ComponentModel.Description("شماره گواهی نامه")]
        public long CertificateNo { get; set; }

        //FieldDesc2
        [System.ComponentModel.DisplayName("دومين شرح")]
        [System.ComponentModel.Description("شرح متن و ظهر چک")]
        public string ChequeDescription { get; set; }

        //FieldValue1
        [System.ComponentModel.DisplayName("اولین مقدار")]
        [System.ComponentModel.Description("مبلغ قبض اقساطی")]
        public decimal AmountInstallmentBill { get; set; }

        //FieldValue1
        [System.ComponentModel.DisplayName("اولین مقدار")]
        [System.ComponentModel.Description("مبلغ وام")]
        public decimal LoanAmount { get; set; }
        
        //FieldDesc1
        [System.ComponentModel.DisplayName("اولين شرح")]
        [System.ComponentModel.Description("نرخ خسارت تاخیر")]
        public string DelayDamageRate { get; set; }

        //FieldValue1
        [System.ComponentModel.DisplayName("اولین مقدار")]
        [System.ComponentModel.Description("مبلغ عوارض رای کمیسیون آبیاری")]
        public decimal IrrigationCommission { get; set; }

        //FieldDesc1
        [System.ComponentModel.DisplayName("اولين شرح")]
        [System.ComponentModel.Description("حکم دادگاه مبنی بر اثبات عمد مسبب در ایجاد حادثه")]
        public string CourtJudgment { get; set; }

        //FieldDesc2
        [System.ComponentModel.DisplayName("دومین شرح")]
        [System.ComponentModel.Description("تاییدیه نیروی انتظامی یا پزشکی قانونی یا دادگاه مبنی بر مست بودن")]
        public string ApprovalLegalAuthorities { get; set; }

        //FieldDesc3
        [System.ComponentModel.DisplayName("سومین شرح")]
        [System.ComponentModel.Description("تاییدیه راهنمایی رانندگی مبنی بر فقدان گواهینامه")]
        public string ApprovalDrivingPolice { get; set; }

        //FieldDesc4
        [System.ComponentModel.DisplayName("چهارمین شرح")]
        [System.ComponentModel.Description("تاییدیه نیروی انتظامی مبنی بر مسروقه بودن")]
        public string ApprovalPolice { get; set; }

    }
}
