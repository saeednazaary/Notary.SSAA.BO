

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public class KatebSignRequestGridViewModel
    {
        public KatebSignRequestGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<KatebSignRequestGridItemViewModel> GridItems { get; set; }
        public List<KatebSignRequestGridItemViewModel> SelectedItems { get; set; }

    }

    public class KatebSignRequestGridItemViewModel
    {
        public bool IsSelected { get; set; }
        public string Id { get; set; }
        public string ReqNo { get; set; }
        public string SsarNo { get; set; }
        public string ReqDate { get; set; }
        public string ReqTime { get; set; }
        public string ScriptoriumId { get; set; }
        public string StateId { get; set; }
        public string IsCostPaid { get; set; }
        public string IsRemote { get; set; }
        public string IsReadyToPay { get; set; }
        public string SignRequestSubjectTitle { get; set; }
    }
}
