using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractInfoTextViewModel: EntityState
    {

        public string DocumentId { get; set; }
        public string DocumentInfoTextId { get; set; }

        public string DocumentText { get; set; }

        public string LegalText { get; set; }

        public string DocumentDescription { get; set; }

        public string Description { get; set; }

    }
}
