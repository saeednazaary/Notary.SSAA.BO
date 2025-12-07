using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem
{
    public class LegacySystemCommand: BaseExternalCommandRequest<ExternalApiResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EntityType { get; set; }
        public LegacySystemCommand()
        {
            this.Data = new List<EntityData>();
            SendSms = true;
            TransferFilesToArchive = true;
        }
        public List<EntityData> Data { get; set; }

        public bool SendSms { get; set; }
        public bool TransferFilesToArchive { get; set; }       
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
