using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Lookups
{
    public class SignRequestRelatedPersonLookupQuery : BaseQueryRequest<ApiResult<SignRequestAgentPersonLookupRepositoryObject>>
    {
        public SignRequestRelatedPersonLookupQuery()
        {
            GridSortInput = new List<SortData>();
            GridFilterInput = new List<SearchData>();
            SelectedItems = new List<string>();
            ExtraParams = new SignRequestAgentPersonExtraParams();

        }
        public IList<string> SelectedItems { get; set; }
        public SignRequestAgentPersonExtraParams ExtraParams { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ICollection<SortData> GridSortInput { get; set; }
        public ICollection<SearchData> GridFilterInput { get; set; }
        public string GlobalSearch { get; set; }
    }


}

