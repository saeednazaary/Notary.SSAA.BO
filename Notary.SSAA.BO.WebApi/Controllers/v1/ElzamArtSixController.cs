using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.Queries.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.Validators.ElzamArtSix;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ElzamArtSixController : BaseController
    {
        public ElzamArtSixController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get(string Id)
        {
            var command = new GetElzamArtSixByIdQuery(Id);
            var validator = new GetElzamArtSixByIdValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> Create(CreateElzamArtSixCommand command)
        {
            var validator = new CreateElzamArtSixValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> Send(SendElzamArtSixCommand command)
        {
            var validator = new SendElzamArtSixByIdValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }
    }
}
