using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveRequest
{
    public class ExecutiveRequestRelatedPersonViewModel : EntityState
    {
        public ExecutiveRequestRelatedPersonViewModel()
        {

        }
        [System.ComponentModel.DisplayName("شناسه شخص وابسته ")]
        [System.ComponentModel.Description("شناسه شخص وابسته ")]
        public string RelatedPersonId { get; set; }

        [System.ComponentModel.DisplayName("شناسه تقاضانامه اجرائيه")]
        [System.ComponentModel.Description("شناسه تقاضانامه اجرائيه")]
        public string ExecutionRequestId { get; set; }

        [System.ComponentModel.DisplayName("شناسه شخص اصلی")]
        [System.ComponentModel.Description("شناسه شخص اصلی")]

        public IList<string> MainPersonId { get; set; }
        [System.ComponentModel.DisplayName("نام شخص اصلی")]
        [System.ComponentModel.Description("نام شخص اصلی")]
        public string MainPersonName { get; set; }

        [System.ComponentModel.DisplayName("شناسه شخص وابسته")]
        [System.ComponentModel.Description("شناسه شخص وابسته")]
        public IList<string> RelatedAgentPersonId { get; set; }

        [System.ComponentModel.DisplayName("نام شخص وابسته")]
        [System.ComponentModel.Description("نام شخص وابسته")]
        public string RelatedAgentPersonName { get; set; }

        [System.ComponentModel.DisplayName("شناسه نوع وابستگی")]
        [System.ComponentModel.Description("شناسه نوع وابستگی")]
        public IList<string> RelatedAgentTypeId { get; set; }

        [System.ComponentModel.DisplayName("عنوان نوع وابستگی")]
        [System.ComponentModel.Description("عنوان نوع وابستگی")]
        public string RelatedAgentTypeTitle { get; set; }

        [System.ComponentModel.DisplayName("آيا وکالتنامه در مراجع قانوني خارج از کشور ثبت شده است؟")]
        [System.ComponentModel.Description("آيا وکالتنامه در مراجع قانوني خارج از کشور ثبت شده است؟")]
        public bool IsRelatedAgentDocumentAbroad { get; set; }

        [System.ComponentModel.DisplayName("شناسه کشوري که وکالتنامه در آن تنظيم شده است")]
        [System.ComponentModel.Description("شناسه کشوري که وکالتنامه در آن تنظيم شده است")]
        public IList<string> RelatedAgentDocumentCountryId { get; set; }

        [System.ComponentModel.DisplayName("نام کشوري که وکالتنامه در آن تنظيم شده است")]
        [System.ComponentModel.Description("نام کشوري که وکالتنامه در آن تنظيم شده است")]
        public string RelatedAgentDocumentCountryName { get; set; }

        [System.ComponentModel.DisplayName("آيا وکالتنامه در سامانه ثبت الکترونيک اسناد ثبت شده است؟")]
        [System.ComponentModel.Description("آيا وکالتنامه در سامانه ثبت الکترونيک اسناد ثبت شده است؟")]
        public bool IsRelatedDocumentInSSAR { get; set; }

        [System.ComponentModel.DisplayName("شماره وکالتنامه")]
        [System.ComponentModel.Description("شماره وکالتنامه")]
        public string RelatedAgentDocumentNo { get; set; }

        [System.ComponentModel.DisplayName("تاریخ وکالتنامه")]
        [System.ComponentModel.Description("تاریخ وکالتنامه")]
        public string RelatedAgentDocumentDate { get; set; }

        [System.ComponentModel.DisplayName("مرجع صدور وکالتنامه")]
        [System.ComponentModel.Description("مرجع صدور وکالتنامه")]
        public string RelatedAgentDocumentIssuer { get; set; }

        [System.ComponentModel.DisplayName("رمز تصديق وکالتنامه")]
        [System.ComponentModel.Description("رمز تصديق وکالتنامه")]
        public string RelatedAgentDocumentSecretCode { get; set; }

        [System.ComponentModel.DisplayName("شناسه دفترخانه تنظیم کننده وکالتنامه")]
        [System.ComponentModel.Description("شناسه دفترخانه تنظیم کننده وکالتنامه")]
        public IList<string> RelatedAgentDocumentScriptoriumId { get; set; }

        [System.ComponentModel.DisplayName("عنوان دفترخانه تنظیم کننده وکالتنامه")]
        [System.ComponentModel.Description("عنوان دفترخانه تنظیم کننده وکالتنامه")]
        public string RelatedAgentDocumentScriptoriumName { get; set; }

        [System.ComponentModel.DisplayName("آيا وکيل دادگستري است؟")]
        [System.ComponentModel.Description("آيا وکيل دادگستري است؟")]
        public bool IsRelatedPersonLawyer { get; set; }

        [System.ComponentModel.DisplayName("شناسه دليل نياز به معتمد")]
        [System.ComponentModel.Description("شناسه دليل نياز به معتمد")]
        public IList<string> RelatedReliablePersonReasonId { get; set; }

        [System.ComponentModel.DisplayName("عنوان دليل نياز به معتمد")]
        [System.ComponentModel.Description("عنوان دليل نياز به معتمد")]
        public string RelatedReliablePersonReasonTitle { get; set; }

        [System.ComponentModel.DisplayName("توضيحات")]
        [System.ComponentModel.Description("توضيحات")]
        public string RelatedPersonDescription { get; set; }



    }
}
