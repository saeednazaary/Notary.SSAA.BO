using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class DeArchiveEstateInquiryCommandHandler : BaseCommandHandler<DeArchiveEstateInquiryCommand, ApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private protected Domain.Entities.EstateInquiry masterEntity;
        private protected ApiResult<EstateInquiryViewModel> apiResult;
        private ExternalServiceHelper externalServiceHelper = null;
        private readonly IRepository<SSAA.BO.Domain.Entities.ConfigurationParameter> _configurationParameterRepository;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        public DeArchiveEstateInquiryCommandHandler(IMediator mediator, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IRepository<SSAA.BO.Domain.Entities.ConfigurationParameter> repository, IEstateSectionRepository estateSectionRepository, IEstateSubSectionRepository estateSubSectionRepository, IEstateSeriDaftarRepository estateSeriDaftarRepository, IWorkfolwStateRepository workfolwStateRepository, IConfiguration configuration, IHttpEndPointCaller httpEndPointCaller)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            _estateInquiryRepository = estateInquiryRepository;
            _configurationParameterRepository = repository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
            externalServiceHelper = new ExternalServiceHelper(mediator,
               dateTimeService,
               userService,
               _configurationParameterHelper,
               estateSeriDaftarRepository,
               estateSectionRepository,
               estateSubSectionRepository,
               configuration,
               httpEndPointCaller,
               workfolwStateRepository,
               null);
        }
        protected override async Task<ApiResult> ExecuteAsync(DeArchiveEstateInquiryCommand request, CancellationToken cancellationToken)
        {            
            masterEntity = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
            BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                if (masterEntity != null)
                {
                    await _estateInquiryRepository.LoadCollectionAsync(masterEntity, x => x.EstateInquiryPeople, cancellationToken);
                    await _estateInquiryRepository.LoadReferenceAsync(masterEntity, x => x.EstateInquiryNavigation, cancellationToken);
                    var prevStatus = masterEntity.WorkflowStatesId;
                    var prevTimestamp= masterEntity.Timestamp;
                    if (masterEntity.ResponseResult == "True")
                        masterEntity.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;
                    else
                        masterEntity.WorkflowStatesId = EstateConstant.EstateInquiryStates.RejectResponse;
                    masterEntity.Timestamp += 1;
                    _estateInquiryRepository.Update(masterEntity);
                   
                }
                var response = await _mediator.Send(new GetEstateInquiryByIdQuery() { EstateInquiryId = masterEntity.Id.ToString() }, cancellationToken);
                if (response.IsSuccess)
                {
                    apiResult.Data = response.Data.Adapt<EstateInquiryViewModel>();
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = response.statusCode;
                    apiResult.message.Add("خطا  در بازیابی اطلاعات از پایگاه داده ..... ");
                    apiResult.message = response.message;
                }
            }
            return apiResult;
        }
        protected override bool HasAccess(DeArchiveEstateInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        private void BusinessValidation(DeArchiveEstateInquiryCommand request)
        {
            if (masterEntity != null)
            {
                if (string.IsNullOrWhiteSpace(masterEntity.WorkflowStatesId))
                {
                    apiResult.message.Add("استعلام در وضعیت بایگانی نمی باشد");
                }
                else if (masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.Archived)
                {
                    apiResult.message.Add("استعلام در وضعیت بایگانی نمی باشد");
                }
            }
            else
            {
                apiResult.message.Add("استعلام یافت نشد");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }
    }
}
