namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport
{
    public class ExecutiveSupportCaseViewModel
    {
        public ExecutiveSupportCaseViewModel()
        {
            TheXCasePersonList = new List<ExecutiveSupportCasePersonViewModel>();
        }
        
        public List<ExecutiveSupportCasePersonViewModel> TheXCasePersonList { get; set; }

        public string XCaseId { get; set; }

        public string XCaseNo{ get; set; }

        public string CreatorUnitId { get; set; }

        public string CreatorUnitTitle { get; set; }

        public string XCaseSubNo { get; set; }

        public string XCaseCreateDate { get; set; }
    }


}
