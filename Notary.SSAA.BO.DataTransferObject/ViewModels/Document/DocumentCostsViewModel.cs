using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentCostsViewModel : EntityState
    {
        public DocumentCostsViewModel() 
        {
            DocumentCosts = new List<DocumentCostViewModel>();
        }
        public IList<DocumentCostViewModel> DocumentCosts { get; set; }
    }
}
