namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport
{
    public class GetExecutiveSupportByNoViewModel
    {
        public GetExecutiveSupportByNoViewModel()
        {
            TheXCaseDataList = new List<ExecutiveSupportCaseViewModel>();
        }

        public List<ExecutiveSupportCaseViewModel> TheXCaseDataList { get; set; }

        public bool IsExistXCase { get; set; }

        public string ExecuteTypeId { get; set; }

        public string ExecuteTitle { get; set; }

        public string ExecuteIssueDate { get; set; }

        public string ExecuteNo { get; set; }

    }
}
