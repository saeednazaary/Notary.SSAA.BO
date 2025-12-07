using MediatR;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Bases
{
    public abstract class BaseCommandRequest<TResult> : IRequest<TResult> where TResult : ApiResult
    {
    }

    public abstract class BaseExternalCommandRequest<TResult> : IRequest<TResult> where TResult : ExternalApiResult
    {
    }
}
