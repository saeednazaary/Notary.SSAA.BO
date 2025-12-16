using Notary.SSAA.BO.SharedKernel.Exceptions;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.SharedKernel.Exceptions.CustomException
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(ApiResultStatusCode.NotFound, message)
        {
        }
    }
}
