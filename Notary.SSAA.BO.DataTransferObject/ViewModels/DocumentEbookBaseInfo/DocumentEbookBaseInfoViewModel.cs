using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentEbookBaseInfo
{
    public class DocumentEbookBaseInfoViewModel
    {
        public DocumentEbookBaseInfoViewModel()
        {
        }
        public string DocumentEBookBaseInfoId { get; set; }
        public string LastClassifyNo { get; set; }
        public string LastRegisterVolumeNo { get; set; }
        public string LastRegisterPaperNo { get; set; }
        public string NumberOfBooksJari { get; set; }
        public string NumberOfBooksVehicle { get; set; }
        public string NumberOfBooksRahni { get; set; }
        public string NumberOfBooksOghaf { get; set; }
        public string NumberOfBooksArzi { get; set; }
        public string NumberOfBooksAgent { get; set; }
        public string NumberOfBooksOthers { get; set; }
        public string TotalBooks { get; set; }
        public string FirstRegisterElectronic => ( LastClassifyNo.ToNullableInt() + 1).To_String();

    }
}
