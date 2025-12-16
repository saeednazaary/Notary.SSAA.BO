namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class SignRequestGrid
    {
        public SignRequestGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SignRequestGridItem> GridItems { get; set; }
        public List<SignRequestGridItem> SelectedItems { get; set; }
    }

    public record SignRequestGridItem
    {
        public string Id { get; set; }
        public string ReqNo { get; set; }
        public string NationalNo { get; set; }
        public string ReqDate { get; set; }
        public string SignDate { get; set; }
        public string SignRequestSubjectTitle { get; set; }
        public string SignRequestGetterTitle { get; set; }
        public string Persons { get; set; }
        public List<string> PersonList { get; set; }
        public string StateId { get; set; }
        public bool IsSelected { get; set; }
    }
}
