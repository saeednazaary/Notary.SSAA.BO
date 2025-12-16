namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry
{
    public class EstateInquiryValidationResultViewModel
    {
        public Guid Estate_Inquiry_Id { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
