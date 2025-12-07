using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign
{
    public class VerifySignViewModel
    {
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
        public string Id { set; get; }
        public string MainObjectId { get; set; }
        public string HandlerName { get; set; }
        public SignDataQueryHandlers SignDataQueryHandler { get; set; }
        public string RawDataBase64 { get; set; }
    }
}
