using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentPayments
{
    public class DocumentPaymentsViewModel : EntityState
    {
        public DocumentPaymentsViewModel()
        {
            DocumentPayments = new List<DocumentPaymentViewModel>();
        }
        public IList<DocumentPaymentViewModel> DocumentPayments { get; set; }
    }
}