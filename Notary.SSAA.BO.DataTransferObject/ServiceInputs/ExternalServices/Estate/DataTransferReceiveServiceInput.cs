using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class DataTransferReceiveServiceInput : BaseExternalRequest<ApiResult<DataTransferReceiveServiceViewModel>>
    {
        public DataTransferReceiveServiceInput()
        {
            EntityList = new List<EntityData>();
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

        [JsonProperty("entityList")]
        public List<EntityData> EntityList { get; set; }

    }

    public class Command
    {
        public Command()
        {
            this.Parameters = new List<Parameter>();
        }
        [JsonProperty("commandText")]
        public string CommandText { get; set; }

        [JsonProperty("orderNo")]
        public int OrderNo { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }

    public class EntityData
    {
        public EntityData()
        {
            this.FieldValues = new List<FieldValue>();
            this.Fields = new List<Field>();
        }

        [JsonProperty("commandType")]
        public int CommandType { get; set; }

        [JsonProperty("entityName")]
        public string EntityName { get; set; }

        [JsonProperty("fieldValues")]
        public List<FieldValue> FieldValues { get; set; }

        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty("orderNo")]
        public int OrderNo { get; set; }
    }

    public class FieldValue
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }

    public class Field
    {
        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }


}
