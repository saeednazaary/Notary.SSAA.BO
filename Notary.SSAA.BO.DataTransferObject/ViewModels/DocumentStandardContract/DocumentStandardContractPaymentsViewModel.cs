using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractPaymentsViewModel : EntityState
    {
        public DocumentStandardContractPaymentsViewModel()
        {
            DocumentPayments = new List<DocumentStandardContractPaymentViewModel>();
        }
        public IList<DocumentStandardContractPaymentViewModel> DocumentPayments { get; set; }
    }
}