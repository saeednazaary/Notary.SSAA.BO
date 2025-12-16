using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DealSummarySearchController : BaseController
    {
        public DealSummarySearchController(IMediator mediator) : base(mediator)
        {

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DealSummary([FromBody] DealSummarySelectableGridQuery command)
        {
            
            var validator = new DealSummarySelectableGridValidator();

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

        

        

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DealSummaryPerson(DealSummaryPersonLookupQuery command)
        {
            var validator = new DealsummaryPersonLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, result.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> RestrictionDealSummary(RestrictionDealSummaryLookupQuery command)
        {
            var validator = new RestrictionDealSummaryLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, result.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> UnRestrictionType(DealSummaryUnRestrictionTypeLookupQuery command)
        {
            var validator = new DealSummaryUnRestrictionTypeLookupValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DealSummaryStatus(DealSummaryStatusLookupQuery command)
        {
            var validator = new DealSummaryStatusLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, result.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            return await RunQueryAsync(command);
        }
    }
}
