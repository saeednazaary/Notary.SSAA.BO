using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateTaxInquiry
{
    public class GetTehranTaxBlockRadifQueryHandler : BaseQueryHandler<GetTehranTaxBlockRadifQuery, ApiResult<EstateTaxInquiryServiceBlockRadifObject>>
    {
        private readonly IEstateTaxInquiryRepository _estatetaxInquiryRepository;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetTehranTaxBlockRadifQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryRepository estateTaxInquiryRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {

            _estatetaxInquiryRepository = estateTaxInquiryRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetTehranTaxBlockRadifQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateTaxInquiryServiceBlockRadifObject>> RunAsync(GetTehranTaxBlockRadifQuery request, CancellationToken cancellationToken)
        {

            var input = new GetTehranTaxBlockRadifInput()
            {
                NosaziBlockNumber = request.NosaziBlockNumber,
                NosaziMelkNumber = request.NosaziMelkNumber,
                NosaziRadifNumber = request.NosaziRadifNumber,
                ServiceID = request.ServiceID
            };

            ApiResult<EstateTaxInquiryServiceBlockRadifObject> apiResult = new();
            var result = await _mediator.Send(input, cancellationToken);
            if (result.IsSuccess)
            {

                apiResult.Data = result.Data;
            }
            else
            {

                apiResult.Data = new EstateTaxInquiryServiceBlockRadifObject() { ErrorID = -1, ErrorMessage = "خطا در اجرای سرویس رخ داده است" };
            }

            return apiResult;
        }

    }
}
