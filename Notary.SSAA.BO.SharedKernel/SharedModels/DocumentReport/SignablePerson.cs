

namespace Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport
{
    public class SignablePerson
    {
        public long SignablePersonCount { get; set; }
        public string PersonTypeTitle { get; set; }
        public string FullName { get; set; }
        public byte[] FinterPrintImage { get; set; }
        public bool IsSignedDocument { get; set; }
    }

    public class SignPics
    {
        public byte[] SignPic { get; set; }
    }
}
