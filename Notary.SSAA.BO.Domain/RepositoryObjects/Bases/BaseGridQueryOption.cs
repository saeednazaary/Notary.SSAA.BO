namespace Notary.SSAA.BO.Domain.RepositoryObjects.Bases
{

    public class SearchData
    {
        public string Filter { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class SortData
    {
        public string Sort { get; set; }
        public string SortType { get; set; }

    }

    public class BaseGridQueryOption
    {
        public BaseGridQueryOption()
        {
            SortColumns = new List<SortData>();
            SearchColumns = new List<SearchData>();
        }
        public int RowIndex { get; set; }
        public int PageSize { get; set; }
        public IList<SortData> SortColumns { get; set; }
        public IList<SearchData> SearchColumns { get; set; }
        public string GlobalSearch { get; set; }
    }
}
