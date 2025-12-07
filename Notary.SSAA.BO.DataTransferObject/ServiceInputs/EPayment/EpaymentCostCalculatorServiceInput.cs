using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment
{
    public class EpaymentCostCalculatorServiceInput : BaseExternalRequest<ApiResult<EpaymentCostCalculatorViewModel>>
    {
        public string[] DocumentTypeId { get; set; }
        public string[] EpaymentUseCaseId { get; set; }
        public long? Price { get; set; } = null;
        public int? Quantity { get; set; } = null;
        public int? Quantity1 { get; set; } = null;
        public int? Quantity2 { get; set; } = null;
        public int? Quantity3 { get; set; } = null;
        public bool? Cadastre { get; set; } = false;
        public bool? FinancialDocument { get; set; } = false;
        public bool? Elzam { get; set; } = false;
        public bool? Eghale { get; set; } = false;
    }
}
