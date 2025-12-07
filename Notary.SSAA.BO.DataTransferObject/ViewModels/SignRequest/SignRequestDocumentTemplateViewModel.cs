namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public sealed class SignRequestDocumentTemplateViewModel
    {
        public SignRequestDocumentTemplateViewModel()
        {
            DocumentTypeId = new List<string>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsInEditMode { get; set; }
        public bool IsDirty { get; set; }
        public string LastModifer { get; set; }
        public string LastModifyDateTime { get; set; }
        public string CreateDate { get; set; }
        public string ScriptoriumTitle { get; set; }
        public string DocumentTemplateId { get; set; }
        public string DocumentTemplateCode { get; set; }
        public IList<string> DocumentTypeId { get; set; }
        public string DocumentTemplateTitle { get; set; }
        public string DocumentTemplateStateId { get; set; }
        public string DocumentTemplateText { get; set; }
        public string ScriptoriumId { get; set; }

    }
}
