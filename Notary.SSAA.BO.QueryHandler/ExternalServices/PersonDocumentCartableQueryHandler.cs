using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class PersonDocumentCartableQueryHandler : BaseQueryHandler<PersonDocumentCartableQuery, ApiResult<PersonDocumentCartableViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public PersonDocumentCartableQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(PersonDocumentCartableQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<PersonDocumentCartableViewModel>> RunAsync(PersonDocumentCartableQuery request, CancellationToken cancellationToken)
        {
            var input = new PersonDocumentCartableInput()
            {
                BearerToken = request.BearerToken,
                ExtraParam = request.ExtraParam,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            return await _mediator.Send(input, cancellationToken);
        }

    }
}
