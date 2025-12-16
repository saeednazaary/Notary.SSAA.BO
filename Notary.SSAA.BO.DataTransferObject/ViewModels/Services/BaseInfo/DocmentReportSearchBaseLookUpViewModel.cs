namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo
{
    public class DocmentReportSearchBaseLookUpViewModel
    {
        public DocmentReportSearchBaseLookUpViewModel ( )
        {
            Items = new List<DetailsLookUpItem> ();
        }
        public List<DetailsLookUpItem> Items { get; set; }
    }

    public class DetailsLookUpItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
    }
}
