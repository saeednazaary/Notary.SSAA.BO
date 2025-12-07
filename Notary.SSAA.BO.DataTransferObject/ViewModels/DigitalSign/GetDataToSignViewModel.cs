using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign
{
    public class GetDataToSignViewModel
    {
        public string Data { get; set; }        
    }

    public class SignDataViewModel
    {     
        public List<DataToSign> SignDataList { get; set; }
    }
    public class DataToSign
    {
        public string Data { get; set; }
        public string MainObjectId { get; set; }
    }

    public class GetDataToSignViewModel2 : SignDataViewModel
    {
        public string HandlerName { get; set; }
        public string HandlerInput { get; set; }
        public SignDataQueryHandlers SignDataQueryHandler { get; set; }

    }

}
