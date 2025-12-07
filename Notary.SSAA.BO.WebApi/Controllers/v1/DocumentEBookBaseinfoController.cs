using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Validators.DocumentEbookBaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentEBookBaseinfoController : BaseController
    {
        public DocumentEBookBaseinfoController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }

            [HttpPost, Authorize]
            public async Task<IActionResult> Create(CreateDocumentEbookBaseInfoCommand command)
            {

                var validator = new CreateDocumentEbookBaseInfoValidator();

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
        public async Task<IActionResult> Get()
        {

            var command = new GetDocumentEbookBaseInfoByIdQuery();
            var validator = new GetDocumentEbookBaseInfoByIdValidator();

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
