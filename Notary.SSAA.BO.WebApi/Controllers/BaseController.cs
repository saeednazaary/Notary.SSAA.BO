using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.SharedKernel.Exceptions.CustomException;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.WebApi.Filters;
using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("SSAA/BO/api/v{version:apiVersion}/[controller]")]
    [CheckModelStateFilter]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public class BaseController : ControllerBase
    {
        //protected readonly IAccountService accountService;
        protected readonly IMediator _mediator;

        protected BaseController(/*IAccountService accountService,*/ IMediator mediator)
        {
            //this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //protected async Task<Role> GetRoleByName(string roleName) => await accountService.GetRoleByNameAsync(roleName);

        //protected async Task<User> CurrentUser() => await accountService.GetUserByIdAsync(CurrentUserId());

        protected string CurrentUserId() => User.Identity.GetUserId() ?? throw new NotFoundException("User not found.");

        protected async Task<IActionResult> ExecuteCommandAsync<TResult>(BaseCommandRequest<TResult> request)
            where TResult : ApiResult
        {
            //Thread.CurrentPrincipal = new ClaimsPrincipal(User);


            var result = await _mediator.Send(request);

            if (result.statusCode == ApiResultStatusCode.Success)
                return Ok(result);

            return BadRequest(result);
        }

        protected async Task<IActionResult> RunQueryAsync<TResult>(BaseQueryRequest<TResult> request)
            where TResult : ApiResult
        {
            // Thread.CurrentPrincipal = new ClaimsPrincipal(User);


            var result = await _mediator.Send(request);

            if (result.statusCode==ApiResultStatusCode.Success)
                return Ok(result);

            return BadRequest(result);
        }

        
    }

    [ApiVersion("1.0")]
    [ApiController]
    [Route("NotaryExternal/api/v{version:apiVersion}/[controller]")]
    [CheckModelStateFilter]
    [ProducesResponseType(typeof(ExternalApiResult), 200)]
    public class ExternalBaseController : ControllerBase
    {
        //protected readonly IAccountService accountService;
        protected readonly IMediator _mediator;

        protected ExternalBaseController(/*IAccountService accountService,*/ IMediator mediator)
        {
            //this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //protected async Task<Role> GetRoleByName(string roleName) => await accountService.GetRoleByNameAsync(roleName);

        //protected async Task<User> CurrentUser() => await accountService.GetUserByIdAsync(CurrentUserId());

        protected string CurrentUserId() => User.Identity.GetUserId() ?? throw new NotFoundException("User not found.");

        protected async Task<ExternalApiResult> ExecuteCommandAsync<TResult>(BaseExternalCommandRequest<TResult> request)
            where TResult : ExternalApiResult
        {
            //Thread.CurrentPrincipal = new ClaimsPrincipal(User);


            var result = await _mediator.Send(request);
            return result;
            //if (result.ResCode == "200")
            //    return Ok(result);

            //return BadRequest(result);
        }

        protected async Task<ExternalApiResult> RunQueryAsync<TResult>(BaseExternalQueryRequest<TResult> request)
            where TResult : ExternalApiResult
        {
            // Thread.CurrentPrincipal = new ClaimsPrincipal(User);


            var result = await _mediator.Send(request);
            return result;
            //if (result.ResCode == "200")
            //    return Ok(result);

            //return BadRequest(result);
        }


    }
}
