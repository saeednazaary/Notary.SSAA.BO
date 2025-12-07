using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveRequest
{
    public class ExecutiveRequestBindingSubjectViewModel : EntityState
    {
        public ExecutiveRequestBindingSubjectViewModel()
        {
            ExecutiveBindingSubjectTypeId=new List<string>();
            BindingSubjectCurrencyTypeId=new List<string>();
        }

        [System.ComponentModel.DisplayName("شناسه تقاضانامه اجرائيه")]
        [System.ComponentModel.Description("شناسه تقاضانامه اجرائيه")]
        public string ExecutionRequestId { get; set; }

        [System.ComponentModel.DisplayName("شناسه نوع موضوع لازم الاجرا")]
        [System.ComponentModel.Description("نوع موضوع لازم الاجرا")]
        public IList<string> ExecutiveBindingSubjectTypeId { get; set; }

        [System.ComponentModel.DisplayName("عنوان نوع موضوع لازم الاجرا")]
        [System.ComponentModel.Description("نوع موضوع لازم الاجرا")]
        public string ExecutiveBindingSubjectTypeTitle { get; set; }

        [System.ComponentModel.DisplayName("مبلغ")]
        [System.ComponentModel.Description("مبلغ")]
        public decimal BindingSubjectPrice { get; set; }

        [System.ComponentModel.DisplayName("شناسه واحد پولي")]
        [System.ComponentModel.Description("واحد اندازه گیری")]
        public IList<string> BindingSubjectCurrencyTypeId { get; set; }

        [System.ComponentModel.DisplayName("شناسه واحد پولي")]
        [System.ComponentModel.Description("واحد اندازه گیری")]
        public string BindingSubjectCurrencyTypeTitle { get; set; }

        [System.ComponentModel.DisplayName("شناسه ردیف موضوع لازم الاجرا")]
        [System.ComponentModel.Description("شناسه ردیف موضوع لازم الاجرا")]
        public string BindingSubjectId { get; set; }

        [System.ComponentModel.DisplayName("تاريخ مبناي محاسبه خسارت")]
        [System.ComponentModel.Description("تاريخ مبناي محاسبه خسارت")]
        public string BindingSubjectDurDate { get; set; }

        [System.ComponentModel.DisplayName("شرح")]
        [System.ComponentModel.Description("شرح")]
        public string BindingSubjectDescription { get; set; }
    }
}
