

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Circular
{
    public class CircularItemTypeServiceInput : BaseExternalRequest<ApiResult<BaseLookupRepositoryObject>>
    {
        public CircularItemTypeServiceInput()
        {
            GridSortInput = new List<SortData>();
            GridFilterInput = new List<SearchData>();
            SelectedItems = new List<string>();
            extraParams = new CircularItemTypeParams();
        }
        public IList<string> SelectedItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ICollection<SortData> GridSortInput { get; set; }
        public ICollection<SearchData> GridFilterInput { get; set; }
        public string GlobalSearch { get; set; }
        public CircularItemTypeParams extraParams { get; set; }
    }
    public class CircularItemTypeParams
    {
        public CircularItemTypeParams()
        {
            fields = new List<string>();
        }
        public string lookupName { get; set; }
        public List<string> fields { get; set; }
    }
}
