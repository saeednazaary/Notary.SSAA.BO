

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices.EDM
{
    public class ImportEdmServiceResponse 
    {
        public int ResponseCode { get; set; }
        public int ReasonCode { get; set; }
        public string? ResponseDescription { get; set; }
        public string? DocumentId { get; set; }
        public int Version { get; set; }
        public string? RequestDateTime { get; set; }
        public string? ResponseDateTime { get; set; }
        public string? RrackCode { get; set; }
        public string? ExternalTrackCode { get; set; }
    }
}
