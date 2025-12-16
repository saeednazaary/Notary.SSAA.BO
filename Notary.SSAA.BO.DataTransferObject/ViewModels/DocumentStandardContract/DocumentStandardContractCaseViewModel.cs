using EntityState = Notary.SSAA.BO.DataTransferObject.Bases.EntityState;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractCaseViewModel : EntityState
    {
        public string DocumentCaseId { get; set; }
        public string DocumentId { get; set; }
        public string RowNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ScriptoriumId { get; set; }
        public string LegacyId { get; set; }
    }
}
