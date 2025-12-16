using MediatR;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Security.Authentication;

namespace Notary.SSAA.BO.QueryHandler.Base
{
    public abstract class BaseQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : BaseQueryRequest<TResult> where TResult : ApiResult, new()
    {
        protected readonly IMediator _mediator;
        protected IUserService _userService;

        protected BaseQueryHandler(IMediator mediator, IUserService userService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(userService), "MediatR is Null");
            _userService = userService ?? throw new ArgumentNullException(nameof(userService), "User Serivce is Null");
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            string[] roles = new string[] { _userService.UserApplicationContext.UserRole.RoleId };
            if (!HasAccess(request, roles))
                throw new AuthenticationException("Unauthorized Access to contents.");

            TResult result;
            result = await RunAsync(request, cancellationToken);

            return result;
        }
        protected abstract Task<TResult> RunAsync(TRequest request, CancellationToken cancellationToken);

        protected abstract bool HasAccess(TRequest request, IList<string> userRoles);


        #region Private Methods

        #endregion
    }
    public abstract class BaseExternalQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : BaseExternalQueryRequest<TResult>
    where TResult : ExternalApiResult, new()
    {
        protected readonly IMediator _mediator;
        protected readonly IUserService _userService;

        protected BaseExternalQueryHandler(
            IMediator mediator,
            IUserService userService
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "MediatR is Null");
            _userService = userService ?? throw new ArgumentNullException(nameof(userService), "User Service is Null");

        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var roles = new string[] { _userService.UserApplicationContext.UserRole.RoleId };

            if (!HasAccess(request, roles))
                throw new AuthenticationException("Unauthorized Access to contents.");

            TResult result = await RunAsync(request, cancellationToken);
            return result;
        }

        /// <summary>
        /// Determines whether the current user has access to execute this query.
        /// </summary>
        protected abstract bool HasAccess(TRequest request, IList<string> userRoles);

        /// <summary>
        /// Executes the query logic.
        /// </summary>
        protected abstract Task<TResult> RunAsync(TRequest request, CancellationToken cancellationToken);

        #region Private Methods
        // Add common helpers if needed later
        #endregion
    }

}
