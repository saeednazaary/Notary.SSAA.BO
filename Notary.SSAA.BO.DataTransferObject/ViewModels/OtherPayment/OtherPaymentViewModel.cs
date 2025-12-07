namespace Notary.SSAA.BO.DataTransferObject.ViewModels.OtherPayment
{
    public class OtherPaymentViewModel
    {
        public OtherPaymentViewModel()
        {
            OtherPaymentsTypeId = new List<string>();
            GeoLocationId = new List<string>();
            UnitId = new List<string>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string Id { get; set; }
        public string NationalNo { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string Fee { get; set; }
        public string ItemCount { get; set; }
        public string SumPrices { get; set; }
        public string CompanyName { get; set; }
        public string EstateOwnerNationalNo { get; set; }
        public string EstateOwnerNameFamily { get; set; }
        public string PlaqueNo { get; set; }
        public string EstateAddress { get; set; }
        public string Description { get; set; }
        public IList<string> OtherPaymentsTypeId { get; set; }
        public IList<string> GeoLocationId { get; set; }
        public IList<string> UnitId { get; set; }
    }
}
