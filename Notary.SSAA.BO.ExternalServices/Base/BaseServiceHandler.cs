using MediatR;
using Newtonsoft.Json;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Security.Authentication;

namespace Notary.SSAA.BO.ServiceHandler.Base
{
    public abstract class BaseServiceHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
           where TRequest : BaseExternalRequest<TResult> where TResult : ApiResult, new()
    {
        protected readonly IMediator _mediator;
        protected IUserService _userService;

        private readonly ILogger _logger;

        protected BaseServiceHandler(IMediator mediator, IUserService userService, /*IAccountService accountService, IAuditLogRepository auditLogepository,*/
                                         ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(userService), "MediatR is Null"); ;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService), "User Serivce is Null");

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {            
            string[] roles = new string[] { _userService.UserApplicationContext.UserRole.RoleId };
            if (!HasAccess(request, roles))
                throw new AuthenticationException("Unauthorized Access to contents.");


            TResult result;
            result = await ExecuteAsync(request, cancellationToken);


            return result;
        }

        protected abstract bool HasAccess(TRequest request, IList<string> userRoles);
        protected abstract Task<TResult> ExecuteAsync(TRequest request, CancellationToken cancellationToken);


        #region Private Methods


        #endregion
    }
}
