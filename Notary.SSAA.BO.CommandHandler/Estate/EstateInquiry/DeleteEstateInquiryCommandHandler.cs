using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class DeleteEstateInquiryCommandHandler : BaseCommandHandler<DeleteEstateInquiryCommand, ApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private protected Domain.Entities.EstateInquiry masterEntity;
        private protected ApiResult apiResult;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;

        public DeleteEstateInquiryCommandHandler(IMediator mediator, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger,
            IHttpEndPointCaller httpEndPointCaller,
            IConfiguration configuration,
            IRepository<ConfigurationParameter> configurationParameterRepository)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            _estateInquiryRepository = estateInquiryRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _configurationParameterRepository = configurationParameterRepository;            
        }
        protected override async Task<ApiResult> ExecuteAsync(DeleteEstateInquiryCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
            BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                if (masterEntity != null)
                {
                    bool blv = false;
                    await _estateInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateInquiryPeople, cancellationToken);
                    await _estateInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateInquirySendreceiveLogs, cancellationToken);
                    var copiedInquiry = MakeCopy(masterEntity);
                    masterEntity.EstateInquiryPeople.Remove(masterEntity.EstateInquiryPeople.First());
                    masterEntity.EstateInquirySendreceiveLogs.Clear();
                    Notary.SSAA.BO.Domain.Entities.EstateInquiry followedInquiry = null;
                    if (masterEntity.EstateInquiryId.HasValue && masterEntity.IsFollowedInquiryUpdated == EstateConstant.BooleanConstant.True)
                    {
                        blv = true;
                        followedInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, masterEntity.EstateInquiryId.Value);
                        if (!string.IsNullOrWhiteSpace(followedInquiry.ResponseResult) && followedInquiry.ResponseResult == "False")
                        {
                            followedInquiry.ResponseResult = "True";
                            followedInquiry.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;
                            followedInquiry.SystemMessage = "";
                            followedInquiry.Timestamp++;
                            _estateInquiryRepository.Update(followedInquiry, false);
                        }
                    }
                    await _estateInquiryRepository.DeleteAsync(masterEntity, cancellationToken);                    
                }
            }
            return apiResult;
        }
        private static Domain.Entities.EstateInquiry MakeCopy(Domain.Entities.EstateInquiry masterEntity)
        {
            Domain.Entities.EstateInquiry estateInquiry = new Domain.Entities.EstateInquiry();
            var type = typeof(Domain.Entities.EstateInquiry);
            var pl = type.GetProperties();
            var sa = new string[] { "DealSummaries"
                ,"DocumentEstateInquiries"
                ,"EstateInquiryNavigation"
                ,"EstateInquiryPeople"
                ,"EstateInquirySendreceiveLogs"
                ,"EstateInquiryType"
                ,"EstateSection"
                ,"EstateSeridaftar"
                ,"EstateSubsection"
                ,"EstateTaxInquiries"
                ,"ForestorgInquiries"
                ,"InverseEstateInquiryNavigation"
                ,"WorkflowStates"
                ,"EstateInquirySendedSms"};
            foreach (var prop in pl)
            {
                if (!sa.Contains(prop.Name))
                    prop.SetValue(estateInquiry, prop.GetValue(masterEntity));
            }
            var person = masterEntity.EstateInquiryPeople.First();
            pl = person.GetType().GetProperties();
            sa = new string[] { "EstateInquiry" };
            var copiedPerson = new Domain.Entities.EstateInquiryPerson();
            foreach (var prop in pl)
            {
                if (!sa.Contains(prop.Name))
                    prop.SetValue(copiedPerson, prop.GetValue(person));
            }
            estateInquiry.EstateInquiryPeople.Add(copiedPerson);

            type = typeof(Domain.Entities.EstateInquirySendreceiveLog);
            pl = type.GetProperties();
            sa = new string[] { "EstateInquiry", "EstateInquiryActionType", "WorkflowStates" };
            foreach (var sendreceiveLog in masterEntity.EstateInquirySendreceiveLogs)
            {
                var newSendreceiveLog = new Domain.Entities.EstateInquirySendreceiveLog();
                foreach (var prop in pl)
                {
                    if (!sa.Contains(prop.Name))
                        prop.SetValue(newSendreceiveLog, prop.GetValue(sendreceiveLog));
                }
                estateInquiry.EstateInquirySendreceiveLogs.Add(newSendreceiveLog);
            }

            type = typeof(Domain.Entities.EstateInquirySendedSm);
            pl = type.GetProperties();
            sa = new string[] { "WorkflowStates", "EstateInquiry" };
            foreach (var sendedSms in masterEntity.EstateInquirySendedSms)
            {
                var newSendedSms = new Domain.Entities.EstateInquirySendedSm();
                foreach (var prop in pl)
                {
                    if (!sa.Contains(prop.Name))
                        prop.SetValue(newSendedSms, prop.GetValue(sendedSms));
                }
                estateInquiry.EstateInquirySendedSms.Add(newSendedSms);
            }
            return estateInquiry;
        }
        protected override bool HasAccess(DeleteEstateInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        private void BusinessValidation(DeleteEstateInquiryCommand request)
        {
            if (masterEntity != null)
            {
                if (string.IsNullOrWhiteSpace(masterEntity.WorkflowStatesId))
                {
                    apiResult.message.Add("استعلام قابل حذف نمی باشد");
                }
                else if (masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.NotSended)
                {
                    apiResult.message.Add("استعلام قابل حذف نمی باشد");
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
