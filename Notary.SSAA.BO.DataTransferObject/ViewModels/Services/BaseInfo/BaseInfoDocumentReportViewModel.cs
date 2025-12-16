

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo
{
    public class BaseInfoDocumentReportViewModel
    {
        public string ScriptoriumCode { get; set; }
        public string ScriptoriumName { get; set; }
        public string ScriptoriumAddress { get; set; }
        public IList<GeoLocationViewModel> GeoLocationData { get; set; }
    }

    public class GeoLocationViewModel
    {
        public string GeoId { get; set; }
        public string Location { get; set; }
    }
}
