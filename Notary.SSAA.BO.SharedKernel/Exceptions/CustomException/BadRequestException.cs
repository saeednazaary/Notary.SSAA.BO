using Notary.SSAA.BO.SharedKernel.Exceptions;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.SharedKernel.Exceptions.CustomException
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(ApiResultStatusCode.BadRequest, message)
        {
        }
    }
}
