using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices.Circular
{
    internal sealed class FollowingCircularLookupService : BaseQueryHandler<FollowingCircularLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;

        public FollowingCircularLookupService(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller) : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;

        }

        protected override bool HasAccess(FollowingCircularLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(FollowingCircularLookupQuery request, CancellationToken cancellationToken)
        {
            var apiResult = await _httpEndPointCaller.CallPostApiAsync<BaseLookupRepositoryObject, FollowingCircularLookupQuery>(new HttpEndpointRequest<FollowingCircularLookupQuery>
                ("http://192.168.1.36:83/api/v1/CircularSearch/FollowingCircular", _userService.UserApplicationContext.Token, request), cancellationToken);
            return apiResult;
        }
    }
}
