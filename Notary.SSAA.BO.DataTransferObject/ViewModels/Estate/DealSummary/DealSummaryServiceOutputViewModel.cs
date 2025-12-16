using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;


namespace SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary
{
    public class DealSummaryServiceOutputViewModel
    {        
        public DSUDealSummaryObject DsuDealSummary { get; set; }        
        public object Tag { get; set; }        
        public bool Result { get; set; }        
        public string ErrorMessage { get; set; }        
        public List<string> SendedInquiryList { get; set; }        
        public List<string> SendedRemoveRestrictionDealSummaries { get; set; }
    }
}
