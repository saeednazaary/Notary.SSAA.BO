using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups
{
    public sealed class ReusedDocumentPaymentLookupQuery : BaseQueryRequest<ApiResult<ReusedDocumentPaymentLookupRepositoryObject>>
    {
        public ReusedDocumentPaymentLookupQuery()
        {
            GridSortInput = new List<SortData>();
            GridFilterInput = new List<SearchData>();
            SelectedItems = new List<Guid>();
        }
        public IList<Guid> SelectedItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IList<SortData> GridSortInput { get; set; }
        public IList<SearchData> GridFilterInput { get; set; }
        public string GlobalSearch { get; set; }
        public ReusedDocumentPaymentLookupExtraParams ExtraParams { get; set; }
    }
}