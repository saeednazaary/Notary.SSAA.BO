namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService
{
    public class GetGeolocationByIdViewModel
    {
        public GetGeolocationByIdViewModel()
        {
            GeolocationList = new List<GeolocationData>();
        }
        public List<GeolocationData> GeolocationList { get; set; }
    }
    public class GeolocationData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string LegacyId { get; set; }
        public int LocationType { get; set; }
    }
}
