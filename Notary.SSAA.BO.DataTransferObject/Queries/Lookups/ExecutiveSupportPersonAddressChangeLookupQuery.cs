using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups
{
    public class ExecutiveSupportPersonAddressChangeLookupQuery : BaseQueryRequest<ApiResult<ExecutiveSupportPersonLookupRepositoryObject>>
    {
        public ExecutiveSupportPersonAddressChangeLookupQuery()
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
        public ExecutiveSupportPersonAddressChangeLookupQueryExtraParams ExtraParams { get; set; }
    }
    public class ExecutiveSupportPersonAddressChangeLookupQueryExtraParams
    {
        //شناسه درخواست تبعی
        public string  ExtraParam1 { get; set; }
        //شناسه نوع درخواست تبعی
        public string  ExtraParam2 { get; set; }
    }
}
