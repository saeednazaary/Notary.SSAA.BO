using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class DataTransferSendServiceInput : BaseExternalRequest<ApiResult<DataTransferSendServiceViewModel>>
    {
        public DataTransferSendServiceInput()
        {
            TheEntities = new List<EntityInfo>();
            Commands = new List<Command>();
            ClientId = "SSAR";
        }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        [JsonProperty("receiverCmsorganizationId")]
        public string ReceiverCmsorganizationId { get; set; }

        [JsonProperty("receiverSubsystemName")]
        public int ReceiverSubsystemName { get; set; }

        [JsonProperty("requestDateTime")]
        public string RequestDateTime { get; set; }

        [JsonProperty("senderCmsorganizationId")]
        public string SenderCmsorganizationId { get; set; }

        [JsonProperty("commands")]
        public List<Command> Commands { get; set; }

        [JsonProperty("theEntities")]
        public List<EntityInfo> TheEntities { get; set; }

    }

    public partial class EntityInfo
    {
        [JsonProperty("commandType")]
        public int CommandType { get; set; }

        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("entitySystemObjectId")]
        public string EntitySystemObjectId { get; set; }

        [JsonProperty("orderNo")]
        public int OrderNo { get; set; }
    }

}
