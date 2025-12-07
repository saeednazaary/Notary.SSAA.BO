using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using  Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using  Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using  Notary.SSAA.BO.DataTransferObject.Validators.Grids;
using  Notary.SSAA.BO.DataTransferObject.Validators.Lookups;
using  Notary.SSAA.BO.SharedKernel.Result;

namespace  Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentTemplateSearchController : BaseController
    {
        public DocumentTemplateSearchController(IMediator mediator) : base(mediator)
        {

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentTemplate([FromBody] DocumentTemplateGridQuery command)
        {
            var validator = new DocumentTemplateGridValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentType([FromBody] DocumentTypeLookupQuery command)
        {
            var validator = new DocumentTypeLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentTemplateSearch([FromBody] DocumentTemplateLookupQuery query)
        {
            var validator = new DocumentTemplateLookupValidator();

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
    }
}
