using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Validators.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DigitalSignController : BaseController
    {
        public DigitalSignController(IMediator mediator)
    : base(mediator)
        {
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetData(GetDataToSignQuery command)
        {

            var validator = new GetDataToSignValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> ValidateCertificate(ValidateCertificateQuery command)
        {

            var validator = new ValidateCertificateValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> VerifySign(VerifySignQuery command)
        {

            var validator = new VerifySignValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
    }
}
