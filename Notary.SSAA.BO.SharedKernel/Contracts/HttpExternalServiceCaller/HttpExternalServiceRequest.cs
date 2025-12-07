using Newtonsoft.Json;

namespace Notary.SSAA.BO.SharedKernel.Contracts.HttpExternalServiceCaller
{
    public class HttpExternalServiceRequest
    {
        public HttpExternalServiceRequest(string endPointAddress)
        {
            EndPointAddress = endPointAddress;
        }

        public HttpExternalServiceRequest(string endPointAddress,  Dictionary<string, string> queryParameters)
        {
            QueryParameters = queryParameters;
            EndPointAddress = endPointAddress;
        }
        public HttpExternalServiceRequest()
        {
                
        }
        public string EndPointAddress { get; internal set; }
        public Dictionary<string,string> QueryParameters { get; internal set; }

    }

    public class HttpExternalServiceRequest<TData>: HttpExternalServiceRequest
                where TData : class
    {
        public HttpExternalServiceRequest(string endPointAddress,TData data)
        {
            Data = data;
            EndPointAddress = endPointAddress;

        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; private set; }

    }

   
}
