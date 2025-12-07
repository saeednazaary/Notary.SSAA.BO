
using Newtonsoft.Json;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.EDM
{
    public class ReportEdmViewModel
    {
        public long ResponseCode { get; set; }
        public long ReasonCode { get; set; }
        public string ResponseDescription { get; set; }
        public string DocumentId { get; set; }
        public long Version { get; set; }
        public DateTimeOffset RequestDateTime { get; set; }
        public DateTimeOffset ResponseDateTime { get; set; }
        public Guid TrackCode { get; set; }
        public string ExternalTrackCode { get; set; }
    }
}
