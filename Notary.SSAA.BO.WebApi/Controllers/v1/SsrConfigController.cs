using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Validators.SsrConfig;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SsrConfigController : BaseController
    {
        public SsrConfigController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string configId)
        {
            var query = new GetSsrConfigByIdQuery(configId);
            var validator = new GetSsrConfigByIdValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSsrConfigCommand command)
        {


            var validator = new CreateSsrConfigValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await ExecuteCommandAsync(command);
            }


        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSsrConfigCommand command)
        {


            var validator = new UpdateSsrConfigValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmSsrConfigCommand command)
        {


            var validator = new ConfirmSsrConfigValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await ExecuteCommandAsync(command);
            }


        }
    }
}
