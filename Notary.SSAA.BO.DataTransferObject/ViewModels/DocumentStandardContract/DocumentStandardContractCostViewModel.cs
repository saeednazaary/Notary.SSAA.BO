using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractCostViewModel : EntityState
    {
        public DocumentStandardContractCostViewModel() { }
        public string DocumentId { get; set; }
        public string RequestId { get; set; }
        public string RequestUnchangedId { get; set; }
        public string RequestPrice { get; set; }
        public string RequestChangeReason { get; set; }
        public string RequestDescription { get; set; }
        public string RequestScriptoriumId { get; set; }
        public IList<string> RequestCostTypeId { get; set; }
        public string RequestCostTypeTitle { get; set; }
        public string RequestPriceUnchanged { get; set; }
        public string RequestTotalPrice { get; set; }
        public DateTime recordDateTime { get; set; }
    }
}
