using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.SharedKernel.Result;



namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate
{
    public class RestrictionDealSummaryLookupQuery : BaseQueryRequest<ApiResult<RestrictionDealSummaryListViewModel>>
    {
        public RestrictionDealSummaryLookupQuery()
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
        public RestrictionDealSummaryLookupQueryExtraParam ExtraParams { get; set; }
    }

    public class RestrictionDealSummaryLookupQueryExtraParam
    {
        public string ScriptoriumId { get; set; }
        public List<string> EstateInquiryId { get; set; }
        public List<string> DealSummaryId { get; set; }
        public string DocumentClassifyNo { get; set; }
        public string DocumentSignDate { get; set; }
    }
}
