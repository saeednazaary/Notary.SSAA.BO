using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Validators.EpaymentCostCalculator;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class KatebEPayment : BaseController
    {
        public KatebEPayment(IMediator mediator) : base(mediator)
        {

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> KatebEpaymentCostCalculator([FromBody] KatebEpaymentCostCalculatorQuery Request)
        {
            var validator = new KatebEpaymentCostCalculatorValidator();

            var result = await validator.ValidateAsync(Request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(Request);
            }
            
        }
    }
}
