namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate
{
    public class OtherPaymentGridViewModel
    {
        public OtherPaymentGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<OtherPaymentGridItemViewModel> GridItems { get; set; }
        public List<OtherPaymentGridItemViewModel> SelectedItems { get; set; }
    }

    public class OtherPaymentGridItemViewModel
    {
        public string Id { get; set; }
        public string NationalNo { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string OtherPaymentsTypeId { get; set; }
        public string OtherPaymentsTypeTitle { get; set; }
        public string ItemCount { get; set; }
        public string Fee { get; set; }
        public string SumPrices { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}
