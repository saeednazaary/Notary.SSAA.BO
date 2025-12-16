using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixPerson;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixResponse;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSix
{
    public class ElzamArtSixViewModel : EntityState
    {
        public ElzamArtSixViewModel()
        {
            //ProvinceId = new List<string>();
            //CountyId = new List<string>();
            EstateUnitId = new List<string>();
            EstateSectionId = new List<string>();
            EstateSubsectionId = new List<string>();
        }
        public string Id { get; set; }
        public string No { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string Type { get; set; }
        public List<string> ProvinceId { get; set; }
        public List<string> CountyId { get; set; }
        public string EstateMap { get; set; }
        public List<string> ElzamArtSixOrganId { get; set; }
        public List<string> EstateUnitId { get; set; }
        public List<string> EstateSectionId { get; set; }
        public List<string> EstateSubsectionId { get; set; }
        public bool? EstRelatedInfoLoadBySvc { get; set; }
        public string EstateMainPlaque { get; set; }
        public string EstateSecondaryPlaque { get; set; }
        public long EstateArea { get; set; }
        public string EstatePostCode { get; set; }
        public string Attachments { get; set; }
        public string SendDate { get; set; }
        public string SendTime { get; set; }
        public string ScriptoriumId { get; set; }
        public string WorkflowStatesId { get; set; }
        public string WorkflowStatesTitle { get; set; }
        public string Ilm { get; set; }
        public string TrackingCode { get; set; }
        public string EstateDocElectronicNoteNo { get; set; }
        public string Address { get; set; }
        public List<string> EstateUsingId { get; set; }
        public List<string> EstateTypeId { get; set; }
        public ElzamArtSixPersonViewModel ElzamArtSixSellerPerson { get; set; }
        public ElzamArtSixPersonViewModel ElzamArtSixBuyerPerson { get; set; }
        public List<ElzamArtSixResponseViewModel> ElzamArtSixResponses { get; set; }
    }
}