using Newtonsoft.Json;

namespace Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller
{
    public class HttpEndpointRequest
    {
        public HttpEndpointRequest(string endPointAddress)
        {
            UseToken = false;
            EndPointAddress = endPointAddress;
        }
        public HttpEndpointRequest(string endPointAddress, string token)
        {
            EndPointAddress = endPointAddress;
            Token = token;
            UseToken = true;

        }

        public HttpEndpointRequest(string endPointAddress, string token, MultipartFormDataContent parameters)
        {
            FromFormParameters = parameters;
            EndPointAddress = endPointAddress;
            Token = token;
            UseToken = true;
            CallType = MethodCallType.FromForm;


        }

        public HttpEndpointRequest(string endPointAddress, string token, MultipartFormDataContent parameters, Dictionary<string, string> headers)
        {
            FromFormParameters = parameters;
            Headers = headers;
            EndPointAddress = endPointAddress;
            Token = token;
            UseToken = true;
            CallType = MethodCallType.FromForm;
        }

        public HttpEndpointRequest(string endPointAddress, string token, Dictionary<string, string> queryParameters)
        {
            QueryParameters = queryParameters;
            EndPointAddress = endPointAddress;
            Token = token;
            UseToken = true;
            CallType = MethodCallType.FromQuery;


        }

        public HttpEndpointRequest(string endPointAddress, string token, Dictionary<string, string> queryParameters, Dictionary<string, string> headers)
        {
            QueryParameters = queryParameters;
            Headers = headers;
            EndPointAddress = endPointAddress;
            Token = token;
            UseToken = true;
            CallType = MethodCallType.FromQuery;
        }

        public HttpEndpointRequest()
        {

        }
        public string EndPointAddress { get; internal set; }
        public bool UseToken { get; internal set; }
        public string Token { get; internal set; }
        public Dictionary<string, string> Headers { get; internal set; }
        public Dictionary<string, string> QueryParameters { get; internal set; }
        public MultipartFormDataContent FromFormParameters { get; internal set; }
        public MethodCallType CallType { get; internal set; }

    }

    public class HttpEndpointRequest<TData> : HttpEndpointRequest
                where TData : class
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; private set; }
        public HttpEndpointRequest(string endPointAddress, TData data)
        {
            Data = data;
            EndPointAddress = endPointAddress;
            UseToken = false;
            CallType = MethodCallType.FromBody;
        }
        public HttpEndpointRequest(string endPointAddress, string token, TData data)
        {
            Data = data;
            EndPointAddress = endPointAddress;
            Token = token;
            UseToken = true;
            CallType = MethodCallType.FromBody;

        }

        public HttpEndpointRequest(string endPointAddress, string token, Dictionary<string, string> headers, TData data)
        {
            Data = data;
            EndPointAddress = endPointAddress;
            Token = token;
            Headers = headers;
            UseToken = true;
            CallType = MethodCallType.FromBody;

        }
    }

    public enum MethodCallType
    {
        None = 0,
        FromBody = 1,
        FromQuery = 2,
        FromForm = 3
    }
}
