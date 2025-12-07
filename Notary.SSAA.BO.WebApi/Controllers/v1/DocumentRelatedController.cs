using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Validators.RelatedDocument;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Commands.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.Queries.RelatedDocument;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentRelatedController : BaseController
    {
        public DocumentRelatedController( IMediator mediator)
            : base(mediator)
        {
        }

     

        [Route("[action]"), HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] string documentId)
        {
            var command = new GetDocumentDetailByIdQuery(documentId);
            var validator = new GetDocumentDetailValidator();

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

        [Route("[action]"), HttpPost, Authorize]
        public async Task<IActionResult> Create(CreateRelatedDocumentCommand command)
        {
            var validator = new CreateRelatedDocumentValidator();

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

        [HttpPut, Route("[action]"), Authorize]
        public async Task<IActionResult> Update(UpdateRelatedDocumentCommand command)
        {
            var validator = new UpdateDocumentRelatedValidator();

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
    }
}
