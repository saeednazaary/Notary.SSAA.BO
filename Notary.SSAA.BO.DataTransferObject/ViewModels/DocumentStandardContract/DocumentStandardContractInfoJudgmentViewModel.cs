using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractInfoJudgmentViewModel : EntityState
    {
        public string RequestId { get; set; }

        public string DocumentId { get; set; }
        public string IssuerName { get; set; }
        public string IssueNo { get; set; }
        public string IssueDate { get; set; }
        public string CaseClassifyNo { get; set; }
        public IList<string> DocumentJudgmentTypeId { get; set; }
    }
}
