using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    public class FingerprintController : BaseController
    {
        public FingerprintController(IMediator mediator)
    : base(mediator)
        {
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> FingerprintPerson(CreatePersonFingerprintCommand command)
        {
            var validator = new CreatePersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> DeletePersonFingerprint(DeletePersonFingerprintCommand command)
        {
            var validator = new DeletePersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> SendTFACode(SendTFACodeCommand command)
        {
            var validator = new SendTFACodeValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> VerifyTFACode(VerifyTFACodeCommand command)
        {
            var validator = new VerifyTFACodeValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> TakePersonFingerprint(TakePersonFingerprintCommand command)
        {
            var validator = new TakePersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> MatchPersonFingerprint(MatchPersonFingerprintCommand command)
        {
            var validator = new MatchPersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpGet, Authorize]
        public async Task<IActionResult> PersonFingerprintType()
        {
            GetPersonFingerTypeQuery query = new();
            return await RunQueryAsync(query);

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpGet, Authorize]
        public async Task<IActionResult> InquiryFingerprint([FromQuery]GetInquiryPersonFingerprintQuery query)
        {
            var validator = new GetInquiryFingerprintValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(query);
            }

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> Undo(UndoPersonFingerprintCommand command)
        {
            var validator = new UndoPersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }
    }
}

