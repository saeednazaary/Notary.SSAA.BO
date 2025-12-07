using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate
{
    public class DealSummarySelectableGridQuery : BaseQueryRequest<ApiResult<DealSummarySelectableGridViewModel>>
    {
        public DealSummarySelectableGridQuery()
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

        public DealSummaryGridQueryExtraParams ExtraParams { get; set; }


    }
    public class DealSummaryGridQueryExtraParams
    {
        public string DS_NoFrom { get; set; }
        public string DS_NoTo { get; set; }

        public string DS_DateFrom { get; set; }

        public string DS_DateTo { get; set; }

        public string DS_SystemNo { get; set; }

        public string DS_Status { get; set; }

        public string DS_UnitId { get; set; }
        public string DS_SectionId { get; set; }
        public string DS_SubSectionId { get; set; }
        public string DS_Basic { get; set; }
        public string DS_Secondary { get; set; }
        public string DS_PrintDocNo { get; set; }

        public string DS_EstateElectronicNoteNo { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonNationalityCode { get; set; }
        public string NotaryDocumentNo { get; set; }
    }
}
