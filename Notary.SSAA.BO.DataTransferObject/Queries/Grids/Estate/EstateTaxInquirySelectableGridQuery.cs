using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate
{
    public class EstateTaxInquirySelectableGridQuery : BaseQueryRequest<ApiResult<EstateTaxInquiryGrid>>
    {
        public EstateTaxInquirySelectableGridQuery()
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

        public EstateTaxInquiryGridQueryExtraParams ExtraParams { get; set; }


    }
    public class EstateTaxInquiryGridQueryExtraParams
    {
        public string NoFrom { get; set; }
        public string NoTo { get; set; }
        public string CreateDateFrom { get; set; }
        public string CreateDateTo { get; set; }
        public string SendDateFrom { get; set; }
        public string SendDateTo { get; set; }
        public string ResponseDateFrom { get; set; }
        public string ResponseDateTo { get; set; }
        public string Status { get; set; }
        public string EstateUnitId { get; set; }
        public string EstateSectionId { get; set; }
        public string EstateSubSectionId { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonNationalityCode { get; set; }
        public string[] States { get; set; }
    }
}
