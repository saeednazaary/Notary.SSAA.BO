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
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateArticle6Inquiry
{
    public class ReceiveArticle6InquiryRelatedOrgansCommandHandler : BaseExternalCommandHandler<EstateArticle6InquiryRelatedOrgansCommand, ExternalApiResult>
    {
        private protected SsrArticle6Inq masterEntity;
        private protected ExternalApiResult<object> apiResult;
        private readonly IRepository<SsrArticle6Inq> _inquiryRepository;
        private readonly IRepository<SsrArticle6SubOrgan> _estateArticle6OrganRepository;
        private readonly IRepository<SsrArticle6InqReceiverOrg> _Article6InqReceiverOrg;        
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly IRepository<SsrArticle6InqResponse> _inquiryResponseRepository;
        private readonly IDateTimeService _dateTimeService;
        public ReceiveArticle6InquiryRelatedOrgansCommandHandler(IMediator mediator, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IRepository<SsrArticle6Inq> repository, IRepository<SsrArticle6SubOrgan> estateArticle6OrganRepository, IRepository<SsrArticle6InqReceiverOrg> Article6InqReceiverOrg, IRepository<SsrApiExternalUser> ssrApiExternalUser, IRepository<SsrArticle6InqResponse> inquiryResponseRepository)
            : base(mediator, userService, logger)
        {
            apiResult = new() { ResCode = "1", ResMessage = SystemMessagesConstant.Operation_Successful };

            _inquiryRepository = repository;           
            _estateArticle6OrganRepository = estateArticle6OrganRepository;
            _dateTimeService = dateTimeService;
            _ssrApiExternalUser = ssrApiExternalUser;
            _inquiryResponseRepository = inquiryResponseRepository;
            _Article6InqReceiverOrg = Article6InqReceiverOrg;
        }
        protected override async Task<ExternalApiResult> ExecuteAsync(EstateArticle6InquiryRelatedOrgansCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                masterEntity =  await _inquiryRepository
                                     .Table                                                                          
                                     .Where(x => x.No == request.RequestNo)
                                     .FirstOrDefaultAsync(cancellationToken); 
                await BusinessValidation(request,cancellationToken);
                if (apiResult.ResCode == "1")
                {
                    foreach (var organData in request.RelatedOrganList)
                    {
                        var organ = await _estateArticle6OrganRepository.TableNoTracking.Where(x => x.Code == organData.OrganizationCode && x.SsrArticle6Organ.Code == organData.MinistryCode).FirstOrDefaultAsync(cancellationToken);
                        if (organ != null)
                        {
                            masterEntity.SsrArticle6InqReceiverOrgs.Add(new SsrArticle6InqReceiverOrg()
                            {
                                Id = Guid.NewGuid(),
                                ScriptoriumId = masterEntity.ScriptoriumId,
                                SendDate = organData.SendDate,
                                SsrArticle6InqId = masterEntity.Id,
                                SsrArticle6SubOrganId = organ.Id,
                                TrackingCode = organData.OrganizationTrackingCode
                            });
                            masterEntity.SsrArticle6InqResponses.Add(new SsrArticle6InqResponse()
                            {
                                Id = Guid.NewGuid(),
                                Ilm = "1",
                                ScriptoriumId = masterEntity.ScriptoriumId,
                                SenderOrgId = organ.Id,
                                SsrArticle6InqId = masterEntity.Id,
                                State = "2",
                                ResponseDate = "-",
                                ResponseTime = "-",
                                ResponseType = false
                            });
                        }
                        else
                        {
                            apiResult.ResCode = "109";
                            apiResult.ResMessage = "اطلاعات ورودی صحیح نمی باشد";
                            return apiResult;
                        }

                    }
                    await _inquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                var exp = ex;
                while (exp.InnerException != null)
                {
                    exp = exp.InnerException;
                }
                apiResult.ResCode = "901";
                apiResult.ResMessage =  "خطا در ثبت ثبت اطلاعات رخ داد";
            }
            return apiResult;
        }

        protected override bool HasAccess(EstateArticle6InquiryRelatedOrgansCommand request, IList<string> userRoles)
        {
            return true;
        }
        private async Task BusinessValidation(EstateArticle6InquiryRelatedOrgansCommand request,CancellationToken cancellationToken)
        {
            var user =await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResMessage = "نام کاربری یا کلمه عبور استفاده کننده اشتباه می باشد";
                apiResult.ResCode = "105";
            }
            else if (masterEntity == null)
            
            {
                apiResult.ResMessage = string.Format("استعلام  با شماره {0} یافت نشد", request.RequestNo);
                apiResult.ResCode = "106";
            }
            else
            {
                if (masterEntity.WorkflowStatesId != EstateConstant.EstateElzamSixArtInquiryStates.Sended)
                {
                    if (masterEntity.WorkflowStatesId == EstateConstant.EstateElzamSixArtInquiryStates.NotSended)
                    {
                        apiResult.ResMessage = "استعلام در وضعیت ثبت شده می باشد و  هنوز به سامانه پنجره واحد زمین ارسال نشده است";
                        apiResult.ResCode = "107";
                    }
                    else
                    {
                        apiResult.ResMessage = "استعلام در وضعیت پاسخ داده شده می باشد و مجاز به اعمال تغییرات نمی باشد";
                        apiResult.ResCode = "108";
                    }
                }
            }    

        }
    }
}
