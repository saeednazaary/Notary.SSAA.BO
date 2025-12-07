using MediatR;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Bases
{
    public abstract class BaseExternalRequest<TResult> : IRequest<TResult> where TResult : ApiResult
    {
    }
}
