

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignElectronicBookSignViewModel
    {
        public string Id { get; set; }
        public string ScriptoriumId { get; set; }
        public string SignRequestId { get; set; }
        public string SignRequestNationalNo { get; set; }
        public string SignRequestPersonId { get; set; }
        public string SignDate { get; set; }
        public string HashOfFingerprint { get; set; }
        public string HashOfFile { get; set; }
        public string ConfirmDate { get; set; }
        public string ConfirmTime { get; set; }
    }
}
