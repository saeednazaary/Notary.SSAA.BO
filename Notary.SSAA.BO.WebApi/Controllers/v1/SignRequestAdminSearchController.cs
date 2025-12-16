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
    public class SignRequestAdminSearchController : BaseController
    {
        public SignRequestAdminSearchController(IMediator mediator) : base(mediator)
        {

        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> SignRequestGetter(SignRequestGetterLookupQuery command)
        {

            var validator = new SignRequestGetterLookupValidator();

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
        [HttpPost]

        public async Task<IActionResult> SignRequestSubject(SignRequestSubjectLookupQuery command)
        {

            var validator = new SignRequestSubjectLookupValidator();

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
        [HttpPost]
        public async Task<IActionResult> SignRequestRelatedPerson(SignRequestRelatedPersonLookupQuery command)
        {

            var validator = new SignRequestRelatedPersonLookupValidator();

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
        [HttpPost]
        public async Task<IActionResult> SignRequestOriginalPerson(SignRequestOriginalPersonLookupQuery command)
        {
            var validator = new SignRequestOriginalPersonLookupValidator();

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
        [HttpPost]
        public async Task<IActionResult> SignRequestAgentType(SignRequestAgentTypLookupQuery command)
        {

            var validator = new SignRequestAgentTypeLookupValidator();

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
        [HttpPost]
        public async Task<IActionResult> SignRequestReliablePersonReason(SignRequestReliablePersonReasonLookupQuery command)
        {

            var validator = new SignRequestReliablePersonReasonLookupValidator();

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
        [HttpPost]
        public async Task<IActionResult> SignRequest([FromBody] SignRequestAdminAdvancedSearchQuery command)
        {

            var validator = new SignRequestAdminAdvancedSearchValidator();

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
        [HttpGet]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> ElectronicBookPage([FromQuery] SignRequestElectronicBookPageQuery query)
        {

            var validator = new SignRequestElectronicValidator();

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


        [HttpGet]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> ElectronicBookPageCount([FromQuery] SignRequestElectronicBookPageCountQuery query)
        {

            var validator = new SignRequestElectronicCountValidator();

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

        [HttpGet]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> SignRequestFingerPrintReport([FromQuery] SignRequestFingerPrintReportQuery command)
        {

            var validator = new SignRequestFingerPrintReportValidator();

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
