namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class ElzamArtSixGridViewModel
    {
        public ElzamArtSixGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ElzamArtSixItemViewModel> GridItems { get; set; }
        public List<ElzamArtSixItemViewModel> SelectedItems { get; set; }
    }

    public record ElzamArtSixItemViewModel
    {
        public bool IsSelected { get; set; }
        public Guid Id { get; set; }
        public string No { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public long? Type { get; set; }
        public string ProvinceId { get; set; }
        public string CountyId { get; set; }
        public string EstateMap { get; set; }
        public string EstateUnitId { get; set; }
        public string EstateSectionId { get; set; }
        public string EstateSubsectionId { get; set; }
        public string EstateMainPlaque { get; set; }
        public string EstateSecondaryPlaque { get; set; }
        public long? EstateArea { get; set; }
        public string EstatePostCode { get; set; }
        public byte[] Attachments { get; set; }
        public string SendDate { get; set; }
        public string SendTime { get; set; }
        public string ScriptoriumId { get; set; }
        public string WorkflowStatesId { get; set; }
        public string WorkflowStatesTitle { get; set; }
        public string ResponseDate { get; set; }
        public string ResponseTime { get; set; }
        public string Ilm { get; set; }
        public string TrackingCode { get; set; }
        public string EstateUsingId { get; set; }
        public string EstateTypeId { get; set; }
        public string SellerNationalNO { get; set; }
        public string SellerName { get; set; }
        public string SellerFamily { get; set; }
    }
}