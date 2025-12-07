using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;
using Stimulsoft.Base.Wizards;



namespace Notary.SSAA.BO.CommandHandler.Estate.DealSummary
{
    public class SaveDealSummaryCommandHandler : BaseCommandHandler<SendDealSummaryServiceCommand, ApiResult<DealSummaryServiceOutputViewModel>>
    {
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private protected Domain.Entities.DealSummary masterEntity;
        private protected ApiResult<DealSummaryViewModel> apiResult;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private ExternalServiceHelper externalServiceHelper = null;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IDateTimeService _dateTimeService;
        private readonly IConfiguration _configuration;
        public SaveDealSummaryCommandHandler(IMediator mediator, IDealSummaryRepository dealSummaryRepository, IUserService userService,
            ILogger logger, IRepository<ConfigurationParameter> configurationParameterRepository, IHttpEndPointCaller httpEndPointCaller, IDateTimeService dateTimeService, IConfiguration configuration)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            _dealSummaryRepository = dealSummaryRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _dateTimeService = dateTimeService;
            _configuration = configuration;
           

        }
        protected override async Task<ApiResult<DealSummaryServiceOutputViewModel>> ExecuteAsync(SendDealSummaryServiceCommand request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<DealSummaryServiceOutputViewModel>()
            {
                Data = new DealSummaryServiceOutputViewModel()
                {
                    Result = true,
                    ErrorMessage = ""
                },
                IsSuccess = true,
                statusCode = ApiResultStatusCode.Success
            };
            await Task.Run(() => { }, cancellationToken);
            return apiResult;
        }               
        protected override bool HasAccess(SendDealSummaryServiceCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        
    }
}
