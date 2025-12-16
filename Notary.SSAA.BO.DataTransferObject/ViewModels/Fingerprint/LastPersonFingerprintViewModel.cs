namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint
{
    public class LastPersonFingerprintViewModel
    {
        public LastPersonFingerprintViewModel()
        {
            PersonLastFingerprintImage = "";
            ScriptoriumName = "";
            PersonLastFingerprintFeature = "";
            PersonLastFingerprintRawImage = "";
            GetDateTime = "";
            this.FingerPrintDevice = "";
            this.EntityId = "";
            this.EntityClassifyNo = "";
            this.EntityConfirmDateTime = "";
            this.EntityNationalNo = "";
        }
        public string PersonLastFingerprintImage { get; set; }
        public string PersonLastFingerprintRawImage { get; set; }
        public string PersonLastFingerprintFeature { get; set; }
        public string ScriptoriumName { get; set; }
        public string GetDateTime { get; set; }
        public int PersonLastFingerprintImageHeight { get; set; }
        public int PersonLastFingerprintImageWidth { get; set; }
        public string FingerPrintDevice { get; set; }
        public bool? CompareResult { get; set; }
        public RelatedEntity EntityType { get; set; }
        public string EntityId { get; set; }
        public string EntityLegacyId { get; set; }
        public string EntityNationalNo { get; set; }
        public string EntityClassifyNo { get; set; }
        public string EntityConfirmDateTime { get; set; }
        public string ScriptoriumCode { get; set; }
    }

    public enum RelatedEntity
    {
        Document = 1,
        SignRequest = 2
    }
}
