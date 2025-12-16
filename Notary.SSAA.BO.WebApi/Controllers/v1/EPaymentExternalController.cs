using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Validators.EpaymentCostCalculator;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EPaymentExternalController : ExternalBaseController
    {
        public EPaymentExternalController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EpaymentCostCalculatorExternal(EpaymentCostCalculatorExternalQuery request)
        {
            #region Authenticate
            ExternalUserAuthenticateQuery externalUserAuthenticateQuery = new(request?.UserName, request?.Password);
            var authobj = await RunQueryAsync(externalUserAuthenticateQuery);
            if (authobj.ResCode != "1")
            {
                var resobj = new ExternalApiResult<object>
                {
                    ResCode = authobj.ResCode,
                    ResMessage = authobj.ResMessage,
                    Data = null
                };

                return Unauthorized(resobj);
            }
            #endregion
            var validator = new EpaymentCostCalculatorExternalValidator();

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var firstErrorCode = result.Errors[0].ErrorCode;
                var resobj = new ExternalApiResult<object>
                {
                    ResCode = firstErrorCode,
                    ResMessage = result.Errors[0].ErrorMessage,
                    Data = null
                };

                return BadRequest(resobj);
            }
            else
            {
                var resobj = await _mediator.Send(request);
                if (resobj.ResCode == "1")
                    return Ok(await _mediator.Send(request));
                else
                    return BadRequest(resobj);

            }

        }
    }
}
