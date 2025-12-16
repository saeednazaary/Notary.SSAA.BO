namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class KatebEpaymentCostCalculatorViewModel
    {
        public KatebEpaymentCostCalculatorViewModel()
        {
            KatebEpaymentCostCalculatorDetailList = new List<KatebEpaymentCostCalculatorDetail>();
        }
        public long? TotalPrice { get; set; }
        public IList<KatebEpaymentCostCalculatorDetail> KatebEpaymentCostCalculatorDetailList { get; set; }
    }

    public class KatebEpaymentCostCalculatorDetail
    {
        public string CostTypeId { get; set; }
        public long? Price { get; set; }
    }
}
