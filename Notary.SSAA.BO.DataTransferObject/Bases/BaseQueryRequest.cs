using MediatR;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Bases
{
    public abstract class BaseQueryRequest<TResult> : IRequest<TResult> where TResult : ApiResult
    {
    }

    public abstract class BaseExternalQueryRequest<TResult> : IRequest<TResult> where TResult : ExternalApiResult
    {
    }
}
