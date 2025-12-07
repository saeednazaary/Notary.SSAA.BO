using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EstateInquirySearchController : BaseController
    {
        public EstateInquirySearchController(IMediator mediator) : base(mediator)
        {

        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateInquiry([FromBody] EstateInquirySelectableGridQuery command)
        {
            
            var validator = new EstateInquirySelectableGridValidator();

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
        public async Task<IActionResult> EstateFollowedInquiry(EstateFollowedInquiryLookupQuery command)
        {
            
            var validator = new EstateFollowedInquiryLookupValidator();

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
        public async Task<IActionResult> EstateInquiryListForDealSummary(EstateInquiryForDealSummaryLookupQuery command)
        {
            var validator = new EstateInquiryForDealSummaryLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateInquiryListForDivisionRequest(EstateInquiryForDivisionRequestLookupQuery command)
        {
            var validator = new EstateInquiryForDivisionRequestLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> UsableEstateInquiryList(UsableEstateInquiryLookupQuery command)
        {
            var validator = new UsableEstateInquiryLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateInquiryList(GetEstateInquiryListQuery command)
        {            
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateInquiryStatus(EstateInquiryStatusLookupQuery command)
        {
            var validator = new EstateInquiryStatusLookupValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            return await RunQueryAsync(command);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateInquiryType(EstateInquiryTypeLookupQuery command)
        {
            var validator = new EstateInquiryTypeLookupValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateSeriDaftar(EstateSeridaftarLookupQuery command)
        {
            var validator = new EstateSeriDaftarLookupValidator();

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
