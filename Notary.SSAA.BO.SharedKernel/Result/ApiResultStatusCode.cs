using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Notary.SSAA.BO.SharedKernel.Result
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Successful Result")]
        Success = HttpStatusCode.OK,

        [Display(Name = "An error has occured in the Server")]
        ServerError = HttpStatusCode.InternalServerError,

        [Display(Name = "You may send some wrong data.")]
        BadRequest = HttpStatusCode.BadRequest,

        [Display(Name = "Not Found.")]
        NotFound = HttpStatusCode.NotFound,

        [Display(Name = "No Content")]
        ListEmpty = HttpStatusCode.NoContent,

        [Display(Name = "Unauthorized")]
        UnAuthorized = HttpStatusCode.Unauthorized,

        [Display(Name = "Forbidden Access")]
        Forbidden = HttpStatusCode.Forbidden
    }

}
