

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class KatebSignRequestGrid
    {
        public KatebSignRequestGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<KatebSignRequestGridItem> GridItems { get; set; }
        public List<KatebSignRequestGridItem> SelectedItems { get; set; }
    }
    public class KatebSignRequestGridItem
    {
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
    public class KatebSignRequestSearchExtraParams
    {
        public string NationalNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StateId { get; set; }
        public List<string> IsCostPaid { get; set; }
        public string IsReadyToPay { get; set; }
    }
}
