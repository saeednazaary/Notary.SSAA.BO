using Newtonsoft.Json;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpExternalServiceCaller;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IHttpExternalServiceCaller
    {
        public Task<TResult> CallExternalServicePostApiAsync<TResult, TRequest>(HttpExternalServiceRequest<TRequest> request, CancellationToken cancellationToken) where TRequest : class where TResult : class;
        public Task<TResult> CallExternalServiceGetApiAsync<TResult>(HttpExternalServiceRequest request, CancellationToken cancellationToken) where TResult : class;
        public Task<TResult> CallExternalServicePostApiAsync<TResult, TRequest>(HttpExternalServiceRequest<TRequest> request,string Token, CancellationToken cancellationToken) where TRequest : class where TResult : class;
        public Task<TResult> CallExternalServiceGetApiAsync<TResult>(HttpExternalServiceRequest request,string Token , CancellationToken cancellationToken) where TResult : class;
    }
}
