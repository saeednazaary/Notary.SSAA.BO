using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.Mappers.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.Queries.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSix;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.ElzamArtSix
{
    public class CreateElzamArtSixCommandHandler : BaseCommandHandler<CreateElzamArtSixCommand, ApiResult>
    {

        private IElzamArtSixRepository _ElzamArtSixRepository;
        private IRepository<SsrArticle6InqReceiver> _ElzamArtSixReceiverRepository;
        IDateTimeService _dateTimeService;
        private SsrArticle6Inq masterEntity;
        private ApiResult<ElzamArtSixViewModel> apiResult;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ConfigurationParameterHelper _configurationParameterHelper;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        public CreateElzamArtSixCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IDateTimeService dateTimeService
        , IElzamArtSixRepository ElzamArtSixRepository, IRepository<SsrArticle6InqReceiver> elzamArtSixReceiverRepository,IConfiguration configuration, IRepository<ConfigurationParameter> configurationParameterRepository) : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _logger = logger;
            _ElzamArtSixRepository = ElzamArtSixRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixRepository));
            _dateTimeService = dateTimeService;
            _ElzamArtSixReceiverRepository = elzamArtSixReceiverRepository;
            _configuration = configuration;
            _configurationParameterRepository = configurationParameterRepository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
        }

        protected override bool HasAccess(CreateElzamArtSixCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar);
        }

        protected override async Task<ApiResult> ExecuteAsync(CreateElzamArtSixCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            if (request.IsNew)
            {
                ElzamArtSixMapper.ToEntityElzamArtSixCreate(ref masterEntity, request, _userService.UserApplicationContext.BranchAccess.BranchCode);
                masterEntity.CreateDate = _dateTimeService.CurrentPersianDate;
                masterEntity.CreateTime = _dateTimeService.CurrentTime;
                masterEntity.WorkflowStatesId = EstateConstant.EstateElzamSixArtInquiryStates.NotSended;
                masterEntity.EstateMap = "{\"Type\": \"Polygon\",\"Coordinates\": [[[50.0541897,33.6552382],[50.0543419,33.6552243],[50.0542635,33.6551333],[50.0537183,33.6551015],[50.0541381,33.6551662],[50.0541897,33.6552382]]]}";
                var MaxNo = _ElzamArtSixRepository.Table.Max(x => x.No);

                if (!MaxNo.IsNullOrEmpty())
                {
                    
                    masterEntity.No = (Int64.Parse(MaxNo) + 1).ToString();
                }
                else
                {
                    string Year = _dateTimeService.CurrentPersianDate.Substring(0, 4);
                    string FormCode = "999";
                    string BranchCode = _userService.UserApplicationContext.BranchAccess.BranchCode;
                    masterEntity.No = Year + FormCode + BranchCode + "000001";
                }

                await _ElzamArtSixRepository.AddAsync(masterEntity, cancellationToken);
                foreach (var OrganId in request.ElzamArtSixOrganId)
                {
                    SsrArticle6InqReceiver Receiver = new SsrArticle6InqReceiver();
                    Receiver.Id = Guid.NewGuid();
                    Receiver.SsrArticle6InqId = masterEntity.Id;
                    Receiver.Ilm = "1";
                    Receiver.SsrArticle6OrganId = OrganId;
                    Receiver.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                    await _ElzamArtSixReceiverRepository.AddAsync(Receiver, cancellationToken);
                }


                message = "استعلام با موفقیت ثبت شد";
            }
            else
            {
                masterEntity = await _ElzamArtSixRepository.GetElzamArtSixGetById(request.Id.ToGuid(), cancellationToken);
                ElzamArtSixMapper.ToEntityElzamArtSixCreate(ref masterEntity, request, _userService.UserApplicationContext.BranchAccess.BranchCode);
                masterEntity.SsrArticle6InqPeople.First().ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                List<string> masterOrganIdList = masterEntity.SsrArticle6InqReceivers.Select(x => x.SsrArticle6OrganId).ToList();
                if (!request.ElzamArtSixOrganId.IsNullOrEmpty() && !masterEntity.SsrArticle6InqReceivers.IsNullOrEmpty())
                    await _ElzamArtSixReceiverRepository.DeleteRangeAsync(masterEntity.SsrArticle6InqReceivers.Where(x => !request.ElzamArtSixOrganId.Contains(x.SsrArticle6OrganId)), cancellationToken);
                else if (request.ElzamArtSixOrganId.IsNullOrEmpty() && !masterEntity.SsrArticle6InqReceivers.IsNullOrEmpty())
                    await _ElzamArtSixReceiverRepository.DeleteRangeAsync(masterEntity.SsrArticle6InqReceivers, cancellationToken);

                foreach (var OrganId in request.ElzamArtSixOrganId)
                {
                    if (!masterOrganIdList.Contains(OrganId))
                    {
                        SsrArticle6InqReceiver Receiver = new SsrArticle6InqReceiver();
                        Receiver.Id = Guid.NewGuid();
                        Receiver.SsrArticle6InqId = masterEntity.Id;
                        Receiver.SsrArticle6OrganId = OrganId;
                        Receiver.Ilm = "1";
                        Receiver.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                        await _ElzamArtSixReceiverRepository.AddAsync(Receiver, cancellationToken);
                    }
                }

                await _ElzamArtSixRepository.UpdateAsync(masterEntity, cancellationToken);
                message = "استعلام با موفقیت ویرایش شد";
            }

            #region Get
            ApiResult<ElzamArtSixViewModel> getResponse = await _mediator.Send(new GetElzamArtSixByIdQuery(masterEntity.Id.ToString()), cancellationToken);
            if (getResponse.IsSuccess)
            {
                if (getResponse.Data != null)
                {
                    apiResult.Data = getResponse.Data.Adapt<ElzamArtSixViewModel>();
                    apiResult.message.Add(message);
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = getResponse.statusCode;
                apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");
                apiResult.message = getResponse.message;
            }

            #endregion

            return apiResult;
        }
    }
}