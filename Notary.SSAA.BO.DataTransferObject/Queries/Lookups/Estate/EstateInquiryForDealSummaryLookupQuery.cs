using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.SharedKernel.Result;



namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate
{
    public class EstateInquiryForDealSummaryLookupQuery : BaseQueryRequest<ApiResult<EstateInquiryGrid2>>
    {
        public EstateInquiryForDealSummaryLookupQuery()
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
        public EstateInquiryForDealSummaryLookupQueryExtraParam ExtraParams { get; set; }


    }

    public class EstateInquiryForDealSummaryLookupQueryExtraParam
    {
        public string ScriptoriumId { get; set; }
        public bool IsRestrictedDealSummary { get; set; }
        public bool OnlyCurrentEstateInquiry { get; set; }
    }
}
