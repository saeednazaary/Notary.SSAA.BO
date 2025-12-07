
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class GetEstateGeoJsonViewModel
    {
        public GetEstateGeoJsonViewModel()
        {
            Message = string.Empty;
            Description = string.Empty;
        }
        public GetEstateGeoJsonInternalData Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
    public class GetEstateGeoJsonInternalData
    {
        public GetEstateGeoJsonInternalData()
        {
            GeoJsonMap = string.Empty;
            Messeag = string.Empty;
        }
        public string GeoJsonMap { get; set; }
        public string Messeag { get; set; }
        public bool IsSuccess { get; set; }
    }
}
