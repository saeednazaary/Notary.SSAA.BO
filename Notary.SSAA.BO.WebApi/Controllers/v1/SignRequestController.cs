using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SignRequestController : BaseController
    {
        public SignRequestController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(CreateSignRequestCommand command)
        {

            var validator = new CreateSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [HttpPost, Authorize, Route("[action]")]
        public async Task<IActionResult> GenerateIdentifier(GenerateIdCommand command)
        {

            return await ExecuteCommandAsync(command);

        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> SignRequestDocumentTemplate([FromQuery] GetSignRequestDocumentTemplateByIdQuery query)
        {
            var validator = new GetSignRequestDocumentTemplateByIdValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> Update(UpdateSignRequestCommand command)
        {

            var validator = new UpdateSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }

        }
        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> ReadyToPay(ReadyToPaySignRequestCommand command)
        {

            var validator = new ReadyToPaySignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }


        }
        [HttpPost]
        [Route("[action]"), Authorize]

        public async Task<IActionResult> Dismiss(DismissSignRequestCommand command)
        {

            var validator = new DismissSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }


        }
        [HttpPost]
        [Route("[action]"), Authorize]

        public async Task<IActionResult> Confirm(ConfirmSignRequestCommand command)
        {
            return await ExecuteCommandAsync(command);
        }
        [HttpPost]
        [Route("[action]"), Authorize]

        public async Task<IActionResult> Stage(StageSignRequestCommand command)
        {

            var validator = new StageSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }


        }
        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> RollBack([FromBody] RollBackSignRequestCommand command)
        {

            var validator = new RollBackSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }


        }
        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> FingerprintState([FromBody] UpdateSignRequestFingerprintStateCommand query)
        {

            var validator = new UpdateSignRequestFingerprintStateValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(query);
            }


        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> RemoteFingerprintState([FromBody] UpdateRemoteRequestFingerprintStateCommand query)
        {

            var validator = new UpdateRemoteRequestFingerprintStateValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(query);
            }


        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> PaymentState([FromBody] UpdateSignRequestPaymentStateCommand query)
        {

            var validator = new UpdateSignRequestPaymentStateValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(query);
            }


        }
        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] string signRequestId)
        {

            var command = new GetSignRequestByIdQuery(signRequestId);
            var validator = new GetSignRequestByIdValidator();

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
        [HttpGet]
        [Route("[action]"), Authorize]

        public async Task<IActionResult> PersonFingerprint([FromQuery] string signRequestId)
        {

            var command = new GetSignRequestPersonFingerprintQuery(signRequestId);
            var validator = new GetSignRequestPersonFingerprintValidator();

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



        [HttpGet]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> FinalState([FromQuery] CheckSignRequestFinalStateQuery query)
        {

            var validator = new CheckSignRequestFinalStateValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(query);
            }


        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpGet]
        public async Task<IActionResult> ReportForFO([FromQuery] ReportForFOSignRequestQuery command)
        {

            var validator = new ReportForFOSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }



        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpGet]
        public async Task<IActionResult> Report([FromQuery] ReportSignRequestQuery command)
        {

            var validator = new ReportSignRequestValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpGet]
        public async Task<IActionResult> ReportText([FromQuery] ReportSignRequestTextQuery command)
        {
            var validator = new ReportSignRequestTextValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }


        }
    }
}
