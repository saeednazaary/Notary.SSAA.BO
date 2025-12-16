using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractCostsViewModel : EntityState
    {
        public DocumentStandardContractCostsViewModel() 
        {
            DocumentCosts = new List<DocumentStandardContractCostViewModel>();
        }
        public IList<DocumentStandardContractCostViewModel> DocumentCosts { get; set; }
    }
}
