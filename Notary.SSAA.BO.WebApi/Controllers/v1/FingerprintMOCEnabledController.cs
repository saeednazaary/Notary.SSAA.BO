using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    public class FingerprintMOCEnabledController : BaseController
    {
        public FingerprintMOCEnabledController(IMediator mediator)
    : base(mediator)
        {
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> FingerprintPerson(CreatePersonFingerprintV2Command command)
        {

            var validator = new CreatePersonFingerprintValidator_V2();

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
        public async Task<IActionResult> DeletePersonFingerprint(DeletePersonFingerprintV2Command command)
        {

            var validator = new DeletePersonFingerprintValidator_V2();

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
        public async Task<IActionResult> SendTFACode(SendTFACodeV2Command command)
        {

            var validator = new SendTFACodeValidator_V2();

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
        public async Task<IActionResult> VerifyTFACode(VerifyTFACodeV2Command command)
        {

            var validator = new VerifyTFACodeValidator_V2();

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
        public async Task<IActionResult> TakePersonFingerprint(TakePersonFingerprintV2Command command)
        {

            var validator = new TakePersonFingerprintValidator_V2();

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
        public async Task<IActionResult> MatchPersonFingerprint(MatchPersonFingerprintV2Command command)
        {

            var validator = new MatchPersonFingerprintValidator_V2();

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
        public async Task<IActionResult> InquiryFingerprint([FromQuery] GetInquiryPersonFingerprintQuery query)
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
        public async Task<IActionResult> Undo(UndoPersonFingerprintV2Command command)
        {

            var validator = new UndoPersonFingerprintValidator_V2();

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
        [Route("[action]/{fingerPrintId}"), HttpGet, Authorize]
        public async Task<IActionResult> FingerprintMocRelatedData(string fingerPrintId)
        {

            var query = new GetPersonFingerprintMocRelatedDataQuery(fingerPrintId);
            var validator = new GetInquiryPersonFingerprintMocRelatedDataValidator();

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
        public async Task<IActionResult> SetMocState(SetPersonMocStateCommand command)
        {

            var validator = new SetPersonMocStateValidator();

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
        public async Task<IActionResult> MocLog(CreateMocLogCommand command)
        {

            var validator = new CreateMocLogValidator();

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
        public async Task<IActionResult> RemoteDocumentFingerprint(CreateFakePersonFingerprintCommand command)
        {

            return await ExecuteCommandAsync(command);

        }
    }
}

