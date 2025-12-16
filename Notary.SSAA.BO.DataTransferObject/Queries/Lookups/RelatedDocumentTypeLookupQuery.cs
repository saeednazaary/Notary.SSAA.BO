using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups
{
    public sealed class RelatedDocumentTypeLookupQuery : BaseQueryRequest<ApiResult<BaseLookupRepositoryObject>>
    {
        public RelatedDocumentTypeLookupQuery()
        {
            GridSortInput = new List<SortData>();
            GridFilterInput = new List<SearchData>();
            SelectedItems = new List<string>();
            ExtraParams = new ();
        }
        public IList<string> SelectedItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IList<SortData> GridSortInput { get; set; }
        public IList<SearchData> GridFilterInput { get; set; }
        public string GlobalSearch { get; set; }
        public RelatedDocumentTypeExtraParams ExtraParams { get; set; }

    }
}
