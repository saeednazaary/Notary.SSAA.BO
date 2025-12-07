using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class KatebSignRequestSearchController : BaseController
    {
        public KatebSignRequestSearchController(IMediator mediator) : base(mediator)
        {

        }

        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> KatebSignRequest([FromBody] KatebSignRequestSearchQuery command)
        {
            var validator = new KatebSignRequestSearchValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        [HttpGet]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> GetKatebSignRequest([FromQuery] string signRequestId)
        {
            var command = new GetKatebSignRequestByIdQuery(signRequestId);
            var validator = new GetKatebSignRequestByIdValidator();

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
