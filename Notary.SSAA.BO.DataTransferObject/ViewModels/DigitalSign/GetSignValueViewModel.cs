using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign
{
    public class GetSignValueViewModel
    {
        public string Certificate { get; set; }
        public string SignValue { get; set; }
        public string MainObjectId { get; set; }
        public string Id { get; set; }
        public string RawDataBase64 { get; set; }

    }
}
