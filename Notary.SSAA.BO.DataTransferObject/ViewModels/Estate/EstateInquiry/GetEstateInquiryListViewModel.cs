using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry
{
    public class GetEstateInquiryListViewModel
    {
        public GetEstateInquiryListViewModel()
        {
            this.EstateInquiryList = new List<EntityData>();
        }
        public List<EntityData> EstateInquiryList { get; set; }
    }


}
