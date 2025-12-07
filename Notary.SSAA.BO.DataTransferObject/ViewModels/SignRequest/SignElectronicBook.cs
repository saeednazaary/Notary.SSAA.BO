
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignElectronicBook
    {
        public string SignRequestId { get; set; }
        public string PersonId { get; set; }
        public string SignElectronicBookId { get; set; }
        public string ClassifyNo { get; set; }
        public string SignRequestNational { get; set; }
        public string SignDate { get; set; }
        public string HashOfFingerprints { get; set; }
        public string HashOfImage { get; set; }
        public string DigitalSign { get; set; }
    }
}
