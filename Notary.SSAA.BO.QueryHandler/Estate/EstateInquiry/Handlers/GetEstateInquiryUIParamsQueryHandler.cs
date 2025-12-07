using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetEstateInquiryUIParamsQueryHandler : BaseQueryHandler<GetEstateInquiryUIConfigParamsQuery, ApiResult<GetEstateInquiryUIConfigParamsViewModel>>
    {
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        public GetEstateInquiryUIParamsQueryHandler(IMediator mediator, IUserService userService, IRepository<ConfigurationParameter> configurationParameterRepository)
            : base(mediator, userService)
        {
            _configurationParameterRepository = configurationParameterRepository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, _mediator);
        }
        protected override bool HasAccess(GetEstateInquiryUIConfigParamsQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetEstateInquiryUIConfigParamsViewModel>> RunAsync(GetEstateInquiryUIConfigParamsQuery request, CancellationToken cancellationToken)
        {
            ApiResult<GetEstateInquiryUIConfigParamsViewModel> apiResult = new();
            GetEstateInquiryUIConfigParamsViewModel result = new GetEstateInquiryUIConfigParamsViewModel();
            result.ConfigValues = new List<ConfigValue>();
            if (string.IsNullOrWhiteSpace(request.ConfigName))
            {
                var configList = await _configurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Estate_BO_Server_Is_Active" || x.Subject == "Current_Estate_Inquiry_Enabled" || x.Subject == "Estate_Inquiry_Legal_Person_Info_ReadOnly").ToListAsync(cancellationToken);
                foreach(var config in configList)
                {
                    result.ConfigValues.Add(new ConfigValue() { ConfigName = config.Subject, Value = await _configurationParameterHelper.ParseConfigValue(config.Value, cancellationToken) });
                }
            }
            else
            {
                var configList = await _configurationParameterRepository.TableNoTracking.Where(x => x.Subject == request.ConfigName).ToListAsync(cancellationToken);
                foreach (var config in configList)
                {
                    result.ConfigValues.Add(new ConfigValue() { ConfigName = config.Subject, Value = await _configurationParameterHelper.ParseConfigValue(config.Value, cancellationToken) });
                }
            }
            apiResult.Data = result;
            return apiResult;
        }      
    }
}
