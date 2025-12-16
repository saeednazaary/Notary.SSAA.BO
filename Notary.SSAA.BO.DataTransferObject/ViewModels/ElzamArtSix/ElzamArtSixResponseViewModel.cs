using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixResponse
{
    public class ElzamArtSixResponseViewModel : EntityState
    {
        public string Id { get; set; }
        public string ResponseDate { get; set; }
        public string ResponseTime { get; set; }
        public string Description { get; set; }
        public string ResponseNo { get; set; }
        public string ResponseType { get; set; }
        public string ElzamArtSixId { get; set; }
        public string ElzamArtSixOrganId { get; set; }
        public string ElzamArtSixOrganTitle { get; set; }
        public string ElzamArtSixOrganCode { get; set; }
        public string OppositionId { get; set; }
        public string OppositionTitle { get; set; }
        public string State { get; set; }
    }
}