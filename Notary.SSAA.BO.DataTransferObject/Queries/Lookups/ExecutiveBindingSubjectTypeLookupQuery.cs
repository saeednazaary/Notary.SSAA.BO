using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups
{
    public class ExecutiveBindingSubjectTypeLookupQuery : BaseQueryRequest<ApiResult<BaseLookupRepositoryObject>>
    {
        public ExecutiveBindingSubjectTypeLookupQuery()
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
        public ExecutiveBindingSubjectTypeLookupQueryExtraParams ExtraParams { get; set; }
    }

    public class ExecutiveBindingSubjectTypeLookupQueryExtraParams
    {
        public string ExtraParam { get; set; }
    }

}
