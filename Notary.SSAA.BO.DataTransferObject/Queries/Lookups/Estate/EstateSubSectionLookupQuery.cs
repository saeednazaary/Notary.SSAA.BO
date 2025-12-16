using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate
{
    public class EstateSubSectionLookupQuery : BaseQueryRequest<ApiResult<EstateSubSectionLookupViewModel>>
    {
        public EstateSubSectionLookupQuery()
        {
            GridSortInput = new List<SortData>();
            GridFilterInput = new List<SearchData>();
            SelectedItems = new List<string>();
        }
        public IList<string> SelectedItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ICollection<SortData> GridSortInput { get; set; }
        public ICollection<SearchData> GridFilterInput { get; set; }
        public string GlobalSearch { get; set; }
        public EstateSubSectionLookupQueryExtraParam ExtraParams { get; set; }
    }

    public class EstateSubSectionLookupQueryExtraParam
    {
        public string SectionId { get; set; }
    }
}
