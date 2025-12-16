

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment
{
    public class EpaymentCostCalculatorViewModel
    {
        public EpaymentCostCalculatorViewModel()
        {
            EpaymentCostCalculatorDetailList = new List<EpaymentCostCalculatorDetail>();
        }
        public long? TotalPrice { get; set; }
        public IList<EpaymentCostCalculatorDetail> EpaymentCostCalculatorDetailList { get; set; }
    }

    public class EpaymentCostCalculatorDetail
    {
        public string CostTypeId { get; set; }
        public long? Price { get; set; }
    }
    
}
