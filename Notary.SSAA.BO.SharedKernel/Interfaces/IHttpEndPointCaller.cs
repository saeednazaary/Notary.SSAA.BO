using Newtonsoft.Json;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IHttpEndPointCaller
    {
        public Task<ApiResult<TResult>> CallPostApiAsync<TResult, TRequest>(HttpEndpointRequest<TRequest> request,CancellationToken cancellationToken) where TRequest : class where TResult : class;
        public Task<ApiResult<TResult>> CallGetApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken)  where TResult : class;
        public Task<TResult> CallExternalPostApiAsync<TResult, TRequest>(HttpEndpointRequest<TRequest> request, CancellationToken cancellationToken) where TRequest : class where TResult : class;
        public Task<TResult> CallExternalPostApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken) where TResult : class;
        public Task<TResult> CallExternalGetApiAsync<TResult>(HttpEndpointRequest request, CancellationToken cancellationToken) where TResult : class;
        public JsonConverter JsonConverter { get; set; }
        public Task<ApiResult<TResult>> CallExternalPutApiAsync<TResult>(HttpEndpointRequest httpRequest, CancellationToken cancellationToken) where TResult : class;
    }
}
