using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpEndPointCaller.Policies;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Exceptions.CustomException;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Net.Http.Headers;
using System.Text;

namespace Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpEndPointCaller
{
    public sealed class HttpEndPointCaller : IHttpEndPointCaller
    {
        private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _clientFactory;
        public JsonConverter JsonConverter { get; set; }
        public HttpEndPointCaller(ClientPolicy clientPolicy, IHttpClientFactory clientFactory)
        {
            _clientPolicy = clientPolicy;
            _clientFactory = clientFactory;
        }

        public async Task<TResult> CallExternalGetApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken) where TResult : class
        {
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            HttpClient client = new HttpClient(handler1);
            client.Timeout = TimeSpan.FromSeconds(20);
            if (request.UseToken)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(request.Token);

            string url = QueryHelpers.AddQueryString(request.EndPointAddress, request.QueryParameters);
            HttpResponseMessage response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                => client.GetAsync(url, cancellationToken));
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            TResult result = JsonConvert.DeserializeObject<TResult>(responseBody);

            return result;
        }
        public async Task<TResult> CallExternalPostApiAsync<TResult, TRequest>(HttpEndpointRequest<TRequest> request, CancellationToken cancellationToken)
           where TResult : class
           where TRequest : class
        {
            using var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true // Disable SSL validation (only for development)
            };

            using var client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromSeconds(20);
            StringContent content = new(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");

            if (request.UseToken)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(request.Token);

            using var response = request.CallType switch
            {
                MethodCallType.None => throw new LogicalException("CallType cannot be None "),
                MethodCallType.FromBody => await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(() =>
                                        client.PostAsync(request.EndPointAddress, content, cancellationToken)),
                MethodCallType.FromQuery => throw new LogicalException("CallType cannot be FromQuery "),
                MethodCallType.FromForm => throw new LogicalException("CallType cannot be FromForm "),
                _ => throw new LogicalException("CallType cannot be Null "),
            };

            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<TResult>(responseBody);
        }
        public async Task<TResult> CallExternalPostApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken) where TResult : class
        {
            HttpResponseMessage response = new();
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            HttpClient client = new HttpClient(handler1);
            client.Timeout = TimeSpan.FromSeconds(20);
            if (request.UseToken)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(request.Token);

            switch (request.CallType)
            {
                case MethodCallType.None:
                    throw new LogicalException("CallType cannot be None ");
                    break;
                case MethodCallType.FromBody:
                    throw new LogicalException("CallType cannot be FromBody ");
                    break;
                case MethodCallType.FromQuery:
                    string url = QueryHelpers.AddQueryString(request.EndPointAddress, request.QueryParameters);
                    response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                        => client.PostAsync(url, null, cancellationToken));
                    break;
                case MethodCallType.FromForm:
                    response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                        => client.PostAsync(request.EndPointAddress, request.FromFormParameters, cancellationToken));

                    break;
                default:
                    throw new LogicalException("CallType cannot be Null ");
                    break;
            }

            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            TResult result = JsonConvert.DeserializeObject<TResult>(responseBody);

            return result;
        }
        public async Task<ApiResult<TResult>> CallGetApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken) where TResult : class
        {
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            HttpClient client = new HttpClient(handler1);
            client.Timeout = TimeSpan.FromSeconds(20);
            if (request.UseToken)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(request.Token);

            string url = QueryHelpers.AddQueryString(request.EndPointAddress, request.QueryParameters);
            HttpResponseMessage response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                => client.GetAsync(url, cancellationToken));
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            ApiResult<TResult> result = JsonConvert.DeserializeObject<ApiResult<TResult>>(responseBody);

            if (result == null)
            {
                result = new ApiResult<TResult>
                {
                    Data = null,
                    IsSuccess = false
                };
                result.message.Add("Response of api is not in form of Apiresult");
            }
            if(result.statusCode!= ApiResultStatusCode.Success)
                result.statusCode = ApiResultStatusCode.Success;

            return result;
        }
        public async Task<ApiResult<TResult>> CallPostApiAsync<TResult, TRequest>(HttpEndpointRequest<TRequest> request, CancellationToken cancellationToken) where TRequest : class where TResult : class
        {
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            HttpClient client = new HttpClient(handler1);
            client.Timeout = TimeSpan.FromSeconds(20);
            StringContent content = new(JsonConvert.SerializeObject(request.Data), System.Text.Encoding.UTF8, "application/json");
            if (request.UseToken)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(request.Token);
            HttpResponseMessage response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                => client.PostAsync(request.EndPointAddress, content, cancellationToken));
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            ApiResult<TResult> result = JsonConvert.DeserializeObject<ApiResult<TResult>>(responseBody);
            if (result is null)
            {
                result = new();

            }
            result.message.Add(JsonConvert.SerializeObject(response));
            result.message.Add(JsonConvert.SerializeObject(request));

            if (result == null)
            {
                result = new ApiResult<TResult>
                {
                    Data = null,
                    IsSuccess = false
                };
                result.message.Add("Response of api is not in form of Apiresult");
            }
            if (result.statusCode != ApiResultStatusCode.Success)
                result.statusCode = ApiResultStatusCode.Success;
            return result;
        }
        public async Task<ApiResult<TResult>> CallExternalPutApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken) where TResult : class
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            HttpClient client = new HttpClient(handler1);
            client.Timeout = TimeSpan.FromSeconds(20);
            if (request.UseToken)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(request.Token);
            if (request.Headers != null && request.Headers.Count > 0)
            {
                foreach (var item in request.Headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            switch (request.CallType)
            {
                case MethodCallType.None:
                    throw new LogicalException("CallType cannot be None ");
                case MethodCallType.FromQuery:
                    string url = QueryHelpers.AddQueryString(request.EndPointAddress, request.QueryParameters);
                    response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                        => client.PutAsync(url, null, cancellationToken));
                    break;
                case MethodCallType.FromForm:
                    response = await _clientPolicy.ExponentialHttpRetryWithBackoff.ExecuteAsync(()
                        => client.PutAsync(request.EndPointAddress, request.FromFormParameters, cancellationToken));
                    break;

                default:
                    throw new LogicalException("CallType cannot be Null ");
            }

            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            ApiResult<TResult> result = JsonConvert.DeserializeObject<ApiResult<TResult>>(responseBody);
            if (result != null && result.statusCode != ApiResultStatusCode.Success)
                result.statusCode = ApiResultStatusCode.Success;
            return result;
        }
    }
}
