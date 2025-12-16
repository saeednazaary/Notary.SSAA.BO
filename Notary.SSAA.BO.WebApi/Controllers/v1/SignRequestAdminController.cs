using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SignRequestAdminController : BaseController
    {
        public SignRequestAdminController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] string signRequestId)
        {

            var command = new GetSignRequestAdminByIdQuery(signRequestId);
            var validator = new GetSignRequestAdminByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
    }
}
