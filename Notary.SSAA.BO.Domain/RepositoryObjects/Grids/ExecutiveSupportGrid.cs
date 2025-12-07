namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class ExecutiveSupportGrid
    {
        public ExecutiveSupportGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveSupportiGridItem> SelectedItems { get; set; }
        public List<ExecutiveSupportiGridItem> GridItems { get; set; }
    }
    public class ExecutiveSupportiGridItem
    {
        public string Id { get; set; }
        //شماره درخواست
        public string ExecutionSupportiveNo { get; set; }
        //تاریخ درخواست
        public string ExecutionSupportiveDate { get; set; }
        //نوع درخواست
        public string ExecutionSupportiveType { get; set; }
        //شماره پرونده اجرا
        public string ExecutionRequestCaseNo { get; set; }
        //نوع اجرائیه
        public string ExecutionType { get; set; }
        //تاریخ اجرائیه
        public string ExecutionDate { get; set; }
        //وضعیت درخواست
        public string ExecutionSupportiveState { get; set; }
        //تاریخ پاسخ
        public string ExecutionSupportiveReplyDate { get; set; }
        //شناسه واحد اجرا
        public string ExecutionUnitId { get; set; }
        //واحد اجرا
        public string ExecutionUnit { get; set; }
        public bool IsSelected { get; set; }
    }
}
