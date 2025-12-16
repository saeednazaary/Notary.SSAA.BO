using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public sealed class CheckCircularGridQuery : BaseQueryRequest<ApiResult<CheckCircularViewModel>>
    {
        public CheckCircularGridQuery()
        {
            GridSortInput = new List<SortData>();
            GridFilterInput = new List<SearchData>();
            SelectedItems = new List<string>();
            ExtraParams = new();
        }
        public IList<string> SelectedItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ICollection<SortData> GridSortInput { get; set; }
        public ICollection<SearchData> GridFilterInput { get; set; }
        public string GlobalSearch { get; set; }
        public CheckCircularExtraParams ExtraParams { get; set; }
    }
}
