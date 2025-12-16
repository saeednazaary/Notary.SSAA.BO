using Notary.SSAA.BO.SharedKernel.Exceptions;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.SharedKernel.Exceptions.CustomException
{
    public class LogicalException : AppException
    {
        public LogicalException(string message) : base(ApiResultStatusCode.BadRequest, message)
        {
        }
    }
}
