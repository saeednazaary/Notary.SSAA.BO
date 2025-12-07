using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;




namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class GetEstateSeparationInfoCommandHandler : BaseCommandHandler<GetEstateSeparationInfoCommand, ApiResult<EstateInquiryViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private protected Domain.Entities.EstateInquiry masterEntity;
        private protected ApiResult<EstateInquiryViewModel> apiResult;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.ConfigurationParameter> _configurationParameterRepository;
        private ExternalServiceHelper externalServiceHelper = null;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private BaseInfoServiceHelper _baseInfoServiceHelper = null;
        public GetEstateSeparationInfoCommandHandler(IMediator mediator, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger,IDateTimeService dateTimeService,IRepository<Notary.SSAA.BO.Domain.Entities.ConfigurationParameter> repository,IEstateSectionRepository estateSectionRepository,IEstateSubSectionRepository estateSubSectionRepository,IEstateSeriDaftarRepository estateSeriDaftarRepository,IWorkfolwStateRepository workfolwStateRepository,IConfiguration configuration,IHttpEndPointCaller httpEndPointCaller)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            _estateInquiryRepository = estateInquiryRepository;
            _configurationParameterRepository = repository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
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
        protected override async Task<ApiResult<EstateInquiryViewModel>> ExecuteAsync(GetEstateSeparationInfoCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
            if (masterEntity != null)
            {
                await _estateInquiryRepository.LoadReferenceAsync(masterEntity, x => x.EstateSection, cancellationToken);
                await _estateInquiryRepository.LoadReferenceAsync(masterEntity, x => x.EstateSubsection, cancellationToken);
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { masterEntity.UnitId }, cancellationToken);
                var query = new GetEstateSepartionInfoInput()
                {
                    UnitId = (unit != null && unit.UnitList.Count > 0) ? unit.UnitList[0].LegacyId : "",
                    Basic = masterEntity.Basic,
                    Secondary = masterEntity.Secondary,
                    SectionSSAACode = masterEntity.EstateSection.SsaaCode,
                    SubSectionSSAACode = masterEntity.EstateSubsection.SsaaCode
                };
                var result = await externalServiceHelper.GetEstateSeparationInfo(query, cancellationToken);

                bool bv = false;
                if (result != null && result.IsSuccess)
                {
                    if (result.Data.Data.Successful)
                    {
                        masterEntity.SeparationDate = null;
                        masterEntity.SeparationNo = null;
                        masterEntity.ApartmentsTotalarea = null;
                        if (!string.IsNullOrWhiteSpace(result.Data.Data.SeparationNo))
                        {
                            masterEntity.SeparationNo = result.Data.Data.SeparationNo;
                            bv = true;
                        }

                        if (!string.IsNullOrWhiteSpace(result.Data.Data.SeparationDate))
                        {
                            masterEntity.SeparationDate = result.Data.Data.SeparationDate;
                            bv = true;
                        }
                        if (result.Data.Data.AppartmentsTotalArea > 0)
                        {
                            masterEntity.ApartmentsTotalarea = result.Data.Data.AppartmentsTotalArea;
                            bv = true;
                        }
                        if (bv)
                        {
                            await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                        }
                    }
                }
                if (bv)
                {
                    if (request.ReturnEstateInquiry)
                    {
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
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode =  ApiResultStatusCode.Success;                                        
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام ملک یافت نشد");

            }
            return apiResult;
        }
        protected override bool HasAccess(GetEstateSeparationInfoCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }       
    }
}
