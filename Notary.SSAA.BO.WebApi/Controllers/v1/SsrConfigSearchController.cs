using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SsrConfigSearchController : BaseController
    {
        public SsrConfigSearchController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> SsrConfigSubject(SsrConfigSubjectLookupQuery command)
        {
            var validator = new SsrConfigSubjectLookupValidator();

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
        public async Task<IActionResult> SsrConfigMainSubject(SsrConfigMainSubjectLookupQuery command)
        {


            var validator = new SsrConfigMainSubjectLookupValidator();

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
        public async Task<IActionResult> SsrConfig(SsrConfigAdvancedSearchQuery command)
        {


            var validator = new SsrConfigAdvancedSearchValidator();

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
        public async Task<IActionResult> LastSsrConfig(LastSsrConfigAdvancedSearchQuery command)
        {


            var validator = new LastSsrConfigAdvancedSearchValidator();

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
