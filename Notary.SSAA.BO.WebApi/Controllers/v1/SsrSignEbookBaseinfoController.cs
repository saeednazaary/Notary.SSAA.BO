using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Validators.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SsrSignEbookBaseinfoController : BaseController
    {
        public SsrSignEbookBaseinfoController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(CreateSsrSignEbookBaseInfoCommand command)
        {

            var validator = new CreateSsrSignEbookBaseInfoValidator();

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

            var command = new GetSsrSignEbookBaseInfoByIdQuery();
            var validator = new GetSsrSignEbookBaseInfoByIdValidator();

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
