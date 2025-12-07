using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.Security;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpExternalServiceCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpExternalServiceCaller
{
    public sealed class HttpExternalServiceCaller : IHttpExternalServiceCaller
    {
       private readonly IUserService _userService;
        public HttpExternalServiceCaller(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<TResult> CallExternalServicePostApiAsync<TResult, TRequest>(HttpExternalServiceRequest<TRequest> request,string Token, CancellationToken cancellationToken)
            where TResult : class
            where TRequest : class
        {
            TResult result;
            try
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
                };
                using var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Token);
                var jsonBody = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(5000);
                var httpResponse = await client.PostAsync(request.EndPointAddress, jsonBody, cancellationTokenSource.Token);
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                 result = JsonConvert.DeserializeObject<TResult>(jsonResponse);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result = null;
            }
            return result;
        }


        public async Task<TResult> CallExternalServiceGetApiAsync<TResult>(HttpExternalServiceRequest request,string Token, CancellationToken cancellationToken) where TResult : class
        {
            TResult result;
            try
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
                };
                using var client = new HttpClient(handler);
                var requestUrl = QueryHelpers.AddQueryString(request.EndPointAddress, request.QueryParameters);
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                httpRequest.Headers.Authorization = AuthenticationHeaderValue.Parse(Token);
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(5000);
                var httpResponse = await client.SendAsync(httpRequest, cancellationTokenSource.Token);
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                 result = JsonConvert.DeserializeObject<TResult>(jsonResponse);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result =null;
            }
            return result;
        }
        public async Task<TResult> CallExternalServicePostApiAsync<TResult, TRequest>(HttpExternalServiceRequest<TRequest> request, CancellationToken cancellationToken)
      where TResult : class
      where TRequest : class
        {
            TResult result;
            try
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
                };
                using var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_userService.UserApplicationContext.Token);
                var jsonBody = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(5000);
                var httpResponse = await client.PostAsync(request.EndPointAddress, jsonBody, cancellationTokenSource.Token);
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                 result = JsonConvert.DeserializeObject<TResult>(jsonResponse);
              
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result = null;
            }
            return result;
        }


        public async Task<TResult> CallExternalServiceGetApiAsync<TResult>(HttpExternalServiceRequest request, CancellationToken cancellationToken) where TResult : class
        {
            TResult result;
            try
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
                };
                using var client = new HttpClient(handler);
                var requestUrl = QueryHelpers.AddQueryString(request.EndPointAddress, request.QueryParameters);
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                httpRequest.Headers.Authorization = AuthenticationHeaderValue.Parse(_userService.UserApplicationContext.Token);
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(5000);
                var httpResponse = await client.SendAsync(httpRequest, cancellationTokenSource.Token);
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                result = JsonConvert.DeserializeObject<TResult>(jsonResponse);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                result = null;
            }

            return result;
        }
    }


}
