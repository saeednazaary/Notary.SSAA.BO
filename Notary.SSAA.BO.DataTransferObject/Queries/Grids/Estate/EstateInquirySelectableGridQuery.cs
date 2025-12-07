using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate
{
    public class EstateInquirySelectableGridQuery : BaseQueryRequest<ApiResult<EstateInquirySelectableGridViewModel>>
    {
        public EstateInquirySelectableGridQuery()
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

        public EstateInquiryGridQueryExtraParams ExtraParams { get; set; }


    }
    public class EstateInquiryGridQueryExtraParams
    {
        public string InqNoFrom { get; set; }
        public string InqNoTo { get; set; }

        public string InqDateFrom { get; set; }

        public string InqDateTo { get; set; }

        public string InqSystemNo { get; set; }

        public string InqStatus { get; set; }

        public string InqUnitId { get; set; }
        public string InqSectionId { get; set; }
        public string InqSubSectionId { get; set; }
        public string InqBasic { get; set; }
        public string InqSecondary { get; set; }
        public string InqPrintDocNo { get; set; }

        public string InqEstateElectronicNoteNo { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonNationalityCode { get; set; }
    }
}
