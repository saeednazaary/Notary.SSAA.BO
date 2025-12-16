using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign
{
    public class GetDataToSignQuery : BaseQueryRequest<ApiResult<GetDataToSignViewModel>>
    {
        public string FormName { get; set; }
        public string ConfigName { get; set; }
        public string EntityId { get; set; }
        
    }

    public class GetDataToNewSignQuery : BaseQueryRequest<ApiResult<GetDataToSignViewModel2>>
    {
        public SignDataQueryHandlers SignDataQueryHandler { get; set; }
        public string HandlerName { get; set; }
        public string HandlerInput { get; set; }        
    }
    public class GetSignDataListQuery : BaseQueryRequest<ApiResult<List<GetDataToSignViewModel2>>>
    {
       public List<SignDataHandlerInfo> HandlerInfo { get; set; }
        public void SetHandlerNames()
        {
            foreach (var handler in HandlerInfo)
            {
                if(handler.SignDataQueryHandler== SignDataQueryHandlers.Document)
                {
                    handler.HandlerName = "Notary.SSAA.BO.QueryHandler.Document.GetDocumentSignDataQueryHandler";                    
                }
                if (handler.SignDataQueryHandler == SignDataQueryHandlers.DocumentDealSummary)
                {
                    handler.HandlerName = "Notary.SSAA.BO.QueryHandler.Document.GetDSUDealSummariesDataToSignHandler";
                }
                if (handler.SignDataQueryHandler == SignDataQueryHandlers.DocumentElectronicBook)
                {
                    handler.HandlerName = "Notary.SSAA.BO.QueryHandler.Document.GetElectronicBookDataToSignQueryHandler";
                }
                if (handler.SignDataQueryHandler == SignDataQueryHandlers.SignRequest)
                {
                    handler.HandlerName = "Notary.SSAA.BO.QueryHandler.SignRequest.GetSignRequestSignDataQueryHandler";
                }
            }
        }
    }

    public class SignDataHandlerInfo
    {
        public SignDataQueryHandlers SignDataQueryHandler { get; set; }
        public string HandlerName { get; set; }
        public string HandlerInput { get; set; }
        public SignDataHandlerInfo()
        {
            this.SignDataQueryHandler = SignDataQueryHandlers.None;
        }
    }

    public enum SignDataQueryHandlers
    {
        None = 0,
        Document = 1,
        DocumentDealSummary = 2,
        DocumentElectronicBook = 3,
        SignRequest = 4
    }
}
