namespace Notary.SSAA.BO.CommandHandler.Base
{
    using MediatR;
    using Serilog;
    using Notary.SSAA.BO.DataTransferObject.Bases;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using System.Security.Authentication;

    /// <summary>
    /// Defines the <see cref="SaveCommandHandler{TRequest, TResult, TViewModel, TEntity}" />
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class SaveCommandHandler<TRequest, TResult, TViewModel, TEntity> : IRequestHandler<TRequest, TResult>
           where TRequest : BaseCommandRequest<TResult> where TResult : ApiResult, new()
    {
        /// <summary>
        /// Defines the _mediator
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// Defines the _userService
        /// </summary>
        protected IUserService _userService;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCommandHandler{TRequest, TResult, TViewModel, TEntity}"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="logger">The logger<see cref="ILogger"/></param>
        protected SaveCommandHandler ( IMediator mediator, IUserService userService, /*IAccountService accountService, IAuditLogRepository auditLogepository,*/
                                         ILogger logger )
        {
            _mediator = mediator ?? throw new ArgumentNullException ( nameof ( userService ), "MediatR is Null" ); ;
            this._userService = userService ?? throw new ArgumentNullException ( nameof ( userService ), "User Serivce is Null" );

            _logger = logger ?? throw new ArgumentNullException ( nameof ( logger ) );
        }

        /// <summary>
        /// The ValidateViewModel
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <returns>The <see cref="(bool,List{string})"/></returns>
        protected abstract (bool, List<string>) ValidateViewModel ( TRequest request );

        /// <summary>
        /// The MapToEntity
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected abstract Task MapToEntity ( TRequest request, CancellationToken cancellationToken );

        /// <summary>
        /// The ValidateBeforeSave
        /// </summary>
        /// <returns>The <see cref="Task{(bool, List{string})}"/></returns>
        protected abstract Task<(bool, List<string>)> ValidateBeforeSave ( );

        /// <summary>
        /// The LoadEntity
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected abstract Task LoadEntity ( TRequest request, CancellationToken cancellationToken );

        /// <summary>
        /// The SaveEntity
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected abstract Task SaveEntity ( TRequest request, CancellationToken cancellationToken );

        /// <summary>
        /// The AfterSave
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        protected abstract Task<TResult> AfterSave ( TRequest request, CancellationToken cancellationToken );

        /// <summary>
        /// The Handle
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        public async Task<TResult> Handle ( TRequest request, CancellationToken cancellationToken )
        {
            string[] roles = new string[] { _userService.UserApplicationContext.UserRole.RoleId };
            if ( !HasAccess ( request, roles ) )
                throw new AuthenticationException ( "Unauthorized Access to contents." );

            TResult result;
            result = await ExecuteAsync ( request, cancellationToken );

            return result;
        }

        /// <summary>
        /// The HasAccess
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="userRoles">The userRoles<see cref="IList{string}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected abstract bool HasAccess ( TRequest request, IList<string> userRoles );

        /// <summary>
        /// The ExecuteAsync
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        protected abstract Task<TResult> ExecuteAsync ( TRequest request, CancellationToken cancellationToken );
    }
}
