namespace Notary.SSAA.BO.DataTransferObject.ViewModels.InquiryFromUnit
{
    public sealed class InquiryFromUnitViewModel
    {
        public InquiryFromUnitViewModel()
        {
            InquiryFromUnitPersons = new List<InquiryFromUnitPersonViewModel>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string InquiryFromUnitId { get; set; }
        public string InquiryNo { get; set; }
        public string InquiryDate { get; set; }
        public IList<string> InquiryFromUnitTypeId { get; set; }
        public IList<string> InquiryUnitId { get; set; }
        public string InquiryStatementNo { get; set; }
        public string InquiryReplyText { get; set; }
        public string StateTitle { get; set; }
        public string InquiryText { get; set; }
        public string InquiryItemDescription { get; set; }

        public IList<InquiryFromUnitPersonViewModel> InquiryFromUnitPersons { get; set; }

    }
}
