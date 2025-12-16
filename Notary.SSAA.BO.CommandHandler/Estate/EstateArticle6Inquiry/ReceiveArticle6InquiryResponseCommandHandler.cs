using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateArticle6Inquiry;
using Notary.SSAA.BO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateArticle6Inquiry
{
    public class ReceiveArticle6InquiryResponseCommandHandler : BaseExternalCommandHandler<EstateArticle6InquiryResponseCommand, ExternalApiResult>
    {
        private protected SsrArticle6Inq masterEntity;
        private protected ExternalApiResult<object> apiResult;
        private readonly IRepository<SsrArticle6Inq> _inquiryRepository;
        private readonly IRepository<SsrArticle6SubOrgan> _estateArticle6OrganRepository;
        private readonly IRepository<SsrArticle6OppositReason> _estateArticle6OppositReasonRepository;
        private readonly IRepository<WorkflowState> _workflowStateRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly IRepository<SsrArticle6InqResponse> _inquiryResponseRepository;
        private readonly IRepository<SsrArticle6InqReceiverOrg> _article6InqReceiverOrgRepository;
        private readonly IDateTimeService _dateTimeService;
        public ReceiveArticle6InquiryResponseCommandHandler(IMediator mediator, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IRepository<SsrArticle6Inq> repository, IRepository<SsrArticle6SubOrgan> estateArticle6OrganRepository, IRepository<SsrArticle6OppositReason> estateArticle6OppositReasonRepository, IRepository<WorkflowState> workflowStateRepository, IRepository<SsrApiExternalUser> ssrApiExternalUser, IRepository<SsrArticle6InqResponse> inquiryResponseRepository, IRepository<SsrArticle6InqReceiverOrg> article6InqReceiverOrgRepository)
            : base(mediator, userService, logger)
        {
            apiResult = new() { ResCode = "00", ResMessage = SystemMessagesConstant.Operation_Successful };

            _inquiryRepository = repository;
            _estateArticle6OppositReasonRepository = estateArticle6OppositReasonRepository;
            _estateArticle6OrganRepository = estateArticle6OrganRepository;
            _workflowStateRepository = workflowStateRepository;
            _dateTimeService = dateTimeService;
            _ssrApiExternalUser = ssrApiExternalUser;
            _inquiryResponseRepository = inquiryResponseRepository;
            _article6InqReceiverOrgRepository = article6InqReceiverOrgRepository;
        }
        protected override async Task<ExternalApiResult> ExecuteAsync(EstateArticle6InquiryResponseCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ResponseDate))
            {
                request.ResponseDate = _dateTimeService.CurrentPersianDate;
            }
            try
            {
                masterEntity =  await _inquiryRepository
                                     .Table                                                                          
                                     .Where(x => x.No == request.RequestNo)
                                     .FirstOrDefaultAsync(cancellationToken); 
                await BusinessValidation(request,cancellationToken);
                if (apiResult.ResCode == "00")
                {
                    var organ = await _estateArticle6OrganRepository.GetAsync(x => x.Code == request.ResponseOrganization, cancellationToken);
                    if (organ == null)
                    {
                        apiResult.ResCode = "09";
                        apiResult.ResMessage = string.Format("دستگاه پاسخ دهنده استعلام با کد {0} در سامانه یافت نشد", request.ResponseOrganization);
                        return apiResult;
                    }
                    SsrArticle6OppositReason oppositionReason = null;
                    if (request.ResponseType == 2)
                    {
                        oppositionReason = await _estateArticle6OppositReasonRepository.GetAsync(x => x.Code == request.OppositionReasonCode && x.SsrArticle6SubOrganId == organ.Id, cancellationToken);
                        if (oppositionReason == null)
                        {
                            apiResult.ResCode = "10";
                            apiResult.ResMessage = string.Format("رکورد علت مخالفت با کد {0} ، مرتبط با دستگاه '{1}' در سامانه یافت نشد", request.OppositionReasonCode, organ.Title);
                            return apiResult;
                        }
                    }
                    var receiverOrg = await _article6InqReceiverOrgRepository.Table.Where(x => x.SsrArticle6InqId == masterEntity.Id && x.SsrArticle6SubOrganId == organ.Id).FirstOrDefaultAsync(cancellationToken);
                    if (receiverOrg == null)
                    {
                        apiResult.ResCode = "12";
                        apiResult.ResMessage = string.Format("از طرف سامانه پنجره واحد زمین ، دستگاه با کد {0} ، به عنوان گیرنده استعلام اعلام نشده است", organ.Code);
                        return apiResult;
                    }

                    var response = await _inquiryResponseRepository.Table.Where(x => x.SsrArticle6InqId == masterEntity.Id && x.SenderOrgId == organ.Id).FirstOrDefaultAsync(cancellationToken);
                    if (response != null)
                    {
                        response.Description = request.Description;
                        response.OppositionId = request.ResponseType == 2 ? oppositionReason.Id : null;
                        response.ResponseDate = !string.IsNullOrWhiteSpace(request.ResponseDate) ? request.ResponseDate.Substring(0, 10) : null;
                        response.ResponseType = request.ResponseType == 1 ? true : false;
                        response.ResponseNo = request.ResponseNo;
                        response.Ilm = "1";
                        response.ResponseTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
                        response.ScriptoriumId = masterEntity.ScriptoriumId;
                        response.EstateMap = request.Map;
                        response.State = "1";
                        await _inquiryResponseRepository.UpdateAsync(response, cancellationToken);
                        var lst = await _inquiryResponseRepository.Table.Where(x => x.SsrArticle6InqId == masterEntity.Id && x.State == "2").ToListAsync(cancellationToken);
                        if (lst.Count == 0)
                        {
                            masterEntity.WorkflowStatesId = (await _workflowStateRepository.GetAsync(x => x.TableName == "SSR_ARTICLE6_INQ" && x.State == "3", cancellationToken)).Id;                            
                            await _inquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                        }
                    }
                    else
                    {
                        apiResult.ResCode = "12";
                        apiResult.ResMessage = "کد دستگاه پاسخ دهنده ({0}) برای این استعلام معتبر نمی باشد .";
                        return apiResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var exp = ex;
                while (exp.InnerException != null)
                {
                    exp = exp.InnerException;
                }
                apiResult.ResCode = "13";
                apiResult.ResMessage = "خطا در ثبت پاسخ استعلام ماده 6 رخ داد";
            }
            return apiResult;
        }

        protected override bool HasAccess(EstateArticle6InquiryResponseCommand request, IList<string> userRoles)
        {
            return true;
        }
        private async Task BusinessValidation(EstateArticle6InquiryResponseCommand request,CancellationToken cancellationToken)
        {
            var user =await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResMessage = "نام کاربری یا کلمه عبور استفاده کننده اشتباه می باشد";
                apiResult.ResCode = "02";
            }
            else if (masterEntity != null)
            {
                if (!(request.ResponseType == 1 || request.ResponseType == 2))
                {
                    apiResult.ResMessage = "نوع پاسخ استعلام معتبر نمی باشد";
                    apiResult.ResCode = "11";
                }

            }
            else
            {
                apiResult.ResMessage = string.Format("استعلام  با شماره {0} یافت نشد", request.RequestNo);
                apiResult.ResCode = "08";
            }

        }
    }
}
