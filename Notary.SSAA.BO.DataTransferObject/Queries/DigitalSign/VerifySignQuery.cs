using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign
{
    public class VerifySignQuery : BaseQueryRequest<ApiResult<VerifySignViewModel>>
    {       
        public string FormName { get; set; }
        public string ConfigName { get; set; }
        public string EntityId { get; set; }
        public string Sign { get; set; }
        public string Certificate { get; set; }

        public string JsonData { get; set; }
    }

    public class VerifyNewSignQuery : BaseQueryRequest<ApiResult<VerifySignViewModel>>
    {
        public string Data { get; set; }
        public string Sign { get; set; }
        public string Certificate { get; set; }
        public string HandlerName { get; set; }
        public string HandlerInput { get; set; }
        public bool SaveSignValue { get; set; }
        public string MainObjectId { get; set; }

        public VerifyNewSignQuery()
        {
            this.SaveSignValue = false;
        }
        public SignDataQueryHandlers SignDataQueryHandler { get; set; }
    }

    public class VerifySignListQuery : BaseQueryRequest<ApiResult<List<VerifySignViewModel>>>
    {
        public List<VerifySignInfo> SignList { get; set; }
        public void SetHandlerNames()
        {
            foreach (var handler in SignList)
            {
                if (handler.SignDataQueryHandler == SignDataQueryHandlers.Document)
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
            }
        }
    }

    public class VerifySignInfo
    {
        public string Data { get; set; }
        public string Sign { get; set; }
        public string Certificate { get; set; }
        public string HandlerName { get; set; }
        public string HandlerInput { get; set; }
        public bool SaveSignValue { get; set; }
        public string MainObjectId { get; set; }

        public VerifySignInfo()
        {
            this.SaveSignValue = false;
            this.SignDataQueryHandler = SignDataQueryHandlers.None;
        }
        public SignDataQueryHandlers SignDataQueryHandler { get; set; }
       
    }

  
}
