using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ElzamArtSixSearchController : BaseController
    {
        public ElzamArtSixSearchController(IMediator mediator) : base(mediator)
        {

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> ElzamArtSix([FromBody] ElzamArtSixGridQuery command)
        {

            var validator = new ElzamArtSixGridValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));

            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> ElzamArtSixOrgan([FromBody] ElzamArtSixOrganLookupQuery command)
        {
            var validator = new ElzamArtSixOrganLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateUsing([FromBody] ElzamArtSixEstateUsingLookupQuery command)
        {
            var validator = new ElzamArtSixEstateUsingLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateType([FromBody] ElzamArtSixEstateTypeLookupQuery command)
        {
            var validator = new ElzamArtSixEstateTypeLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> County([FromBody] ElzamArtSixCountyLookupQuery command)
        {
            var validator = new ElzamArtSixCountyLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> Province([FromBody] ElzamArtSixProvinceLookupQuery command)
        {
            var validator = new ElzamArtSixProvinceLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateSection(EstateSectionLookupQuery command)
        {
            var validator = new EstateSectionLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));

            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateSubSection(EstateSubSectionLookupQuery command)
        {
            var validator = new EstateSubSectionLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));

            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

    }
}
