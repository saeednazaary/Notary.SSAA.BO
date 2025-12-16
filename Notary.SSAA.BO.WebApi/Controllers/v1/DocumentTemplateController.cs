using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentTemplateController : BaseController
    {
        public DocumentTemplateController(IMediator mediator) : base(mediator)
        {

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost]
        public async Task<IActionResult> DocumentTemplate([FromBody] CreateDocumentTemplateCommand command)
        {
            var validator = new CreateDocumentTemplateValidator();

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
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPut]
        public async Task<IActionResult> DocumentTemplate([FromBody] UpdateDocumentTemplateCommand command)
        {
            var validator = new UpdateDocumentTemplateValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpDelete]
        public async Task<IActionResult> DocumentTemplate([FromQuery] DeleteDocumentTemplateCommand command)
        {
            var validator = new DeleteDocumentTemplateValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));
            }

            else return await ExecuteCommandAsync(command);

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet]
        public async Task<IActionResult> DocumentTemplate([FromQuery] GetDocumenttemplateByIdQuery query)
        {
            var validator = new GetDocumenttemplateByIdValidator();

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
