using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign
{
    public class ValidateCertificateViewModel
    {
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
