using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    public class FingerprintExternalController : ExternalBaseController
    {
        public FingerprintExternalController(IMediator mediator): base(mediator)
        {
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PersonLastFingerPrint([FromBody] GetPersonLastFingerprintQuery query, CancellationToken cancellationToken)
        {
            ExternalUserAuthenticateQuery externalUserAuthenticateQuery=new (query?.UserName,query?.Password);
            var authobj = await RunQueryAsync(externalUserAuthenticateQuery);
            if (authobj.ResCode!="1")
            {
                var resobj = new ExternalApiResult<object>
                {
                    ResCode = authobj.ResCode,
                    ResMessage = authobj.ResMessage,
                    Data = null
                };

                return Unauthorized(resobj);
            }
            var validator = new GetPersonLastFingerprintValidator();

            var result = await validator.ValidateAsync(query, cancellationToken);
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
                var resobj = await RunQueryAsync(query);

                if (resobj.ResCode == "103" || resobj.ResCode == "104")
                    return Unauthorized(resobj);

                if (resobj.ResCode == "102")
                    return NotFound(resobj);

                if (resobj.ResCode == "106" || resobj.ResCode == "105" || resobj.ResCode == "101")
                    return StatusCode(299, resobj);

                return Ok(resobj);
            }
        }

    }
}

