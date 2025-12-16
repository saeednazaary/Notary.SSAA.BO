

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class DealSummaryGridItem
    {
        public string DealSummaryId { get; set; }
        public string DealSummaryNumber { get; set; }
        public string DealSummaryDate { get; set; }
        public string DealSummaryUnitName { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }
        public string EstateSection { get; set; }
        public string EstateSubSection { get; set; }
        public string EstateDocPrintNo { get; set; }
        public string Status { get; set; }
        public bool IsSelected { get; set; }
        public string StatusTitle { get; set; }
        public string TransferType { get; set; }
        public bool IsRestricted { get; set; }
        public string RelatedServer { get; set; }
    }

    public class RestrictionDealSummaryListItem
    {
        public bool IsSelected { get; set; }
        public string DealSummaryId { get; set; }
        public string DealSummaryNumber { get; set; }
        public string DealSummaryDate { get; set; }
        public string DealSummaryMainDate { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }
        public string EstateSection { get; set; }
        public string EstateSubSection { get; set; }
        public string EstateSeridaftar { get; set; }
        public string EstateNoteNo { get; set; }
        public string EstatePageNo { get; set; }
        public string EstateElectronicNoteNo { get; set; }
        public string TransitionCaseTitle { get; set; }        
        public string LastEditReceiveDate { get; set; }
        public string FirstReceiveDate { get; set; }    
        public string TransferType { get; set; }        
        public string RelatedServer { get; set; }
    }
}
