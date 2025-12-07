using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SsrSignEbookBaseInfo
{
    public class SsrSignEbookBaseInfoViewModel
    {
        public SsrSignEbookBaseInfoViewModel()
        {
        }
        public string SignRequestElectronicBookBaseInfoId { get; set; }
        public string NumberOfBooks { get; set; }
        public string LastRegisterVolumeNo { get; set; }
        public string LastRegisterPaperNo { get; set; }
        public string LastClassifyNo { get; set; }
        public string FirstElectronicBookNo =>( LastClassifyNo.ToNullableInt() + 1).To_String();

    }
}
