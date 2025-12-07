using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.Validators.DocumentModify;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentModifyController : BaseController
    {
        public DocumentModifyController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(CreateDocumentModifyCommand command)
        {

            var validator = new CreateDocumentModifyValidator();

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


        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] string DocumentModifyId)
        {

            var command = new GetDocumentModifyByIdQuery(DocumentModifyId);
            var validator = new GetDocumentModifyByIdValidator();

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



    }
}
