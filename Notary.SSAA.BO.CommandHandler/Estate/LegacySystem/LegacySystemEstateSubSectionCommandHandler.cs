using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.CommandHandler.Estate.LegacySystem
{
    public class LegacySystemEstateSubSectionCommandHandler : BaseExternalCommandHandler<EstateSubSectionLegacySystemCommand, ExternalApiResult>
    {        
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private string Id;
        public LegacySystemEstateSubSectionCommandHandler(IMediator mediator, IUserService userService,ILogger logger,            
            IEstateSectionRepository estateSectionRepository ,
            IEstateSubSectionRepository estateSubSectionRepository,
            IRepository<SsrApiExternalUser> ssrApiExternalUser
            )
            : base(mediator, userService,logger)
        {            
            _estateSectionRepository = estateSectionRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _ssrApiExternalUser = ssrApiExternalUser;
            Id = string.Empty;
        }
        protected override bool HasAccess(EstateSubSectionLegacySystemCommand request, IList<string> userRoles)
        {
            return true;
        }
        protected async override Task<ExternalApiResult> ExecuteAsync(EstateSubSectionLegacySystemCommand request, CancellationToken cancellationToken)
        {
            ExternalApiResult apiResult = new() { ResCode = "1", ResMessage = SystemMessagesConstant.Operation_Successful };
            var user = await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResCode = "105";
                apiResult.ResMessage = "نام کاربری یا کلمه عبور استفاده کننده  اشتباه می باشد";
                return apiResult;
            }
            else if (request.UserName != "OLD_ESTATE_APP")
            {
                apiResult.ResCode = "106";
                apiResult.ResMessage = "مجاز به استفاده از این سرویس نمی باشید";
                return apiResult;
            }
            try
            {
                if (request.Data.Count > 0)
                    request.Data = request.Data.OrderBy(x => x.OrderNo).ToList();
                var list = request.Data.Where(x => x.EntityName.Equals("bstsection", StringComparison.OrdinalIgnoreCase)).ToList();
                int index = 1;
                foreach (var data in list)
                {
                    bool commitChanges = false;
                    if (index == list.Count)
                        commitChanges = true;
                    if (data.CommandType == 1 || data.CommandType == 2)
                    {
                        await SaveEstateSubSection(data, commitChanges, cancellationToken);
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                apiResult.ResCode = "901";
                apiResult.ResMessage = "خطا در ثبت اطلاعات رخ داده است";
            }
            return apiResult;
        }
        private async Task SaveEstateSubSection(EntityData data,bool commitChanges, CancellationToken cancellationToken)
        {            
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var bstSectionIdValue = data.FieldValues.Where(fv => fv.FieldName.Equals("bstsectionid", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var estateSubSection = await _estateSubSectionRepository.GetAsync(x => x.Id == idValue || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase), cancellationToken);        
            bool isNew = false;
            if (estateSubSection == null)
            {
                estateSubSection = new Domain.Entities.EstateSubsection();
                estateSubSection.Id = await GetNewId(cancellationToken);
                estateSubSection.LegacyId = idValue;               
                isNew = true;
            }
            var section = await _estateSectionRepository.TableNoTracking.Where(x => x.LegacyId.Equals(bstSectionIdValue, StringComparison.OrdinalIgnoreCase)).FirstAsync(cancellationToken);
            SetProperties(estateSubSection, data, section.Id);          
            if(isNew)
            {
                if (commitChanges)
                    await _estateSubSectionRepository.AddAsync(estateSubSection, cancellationToken);
                else
                    await _estateSubSectionRepository.AddAsync(estateSubSection, cancellationToken,false);
            }
            else if(commitChanges)
                await _estateSubSectionRepository.UpdateAsync(estateSubSection, cancellationToken);
            else
                await _estateSubSectionRepository.UpdateAsync(estateSubSection, cancellationToken,false);
        }
        private async Task<string> GetNewId(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(this.Id))
            {
                var count = await _estateSectionRepository.TableNoTracking.CountAsync(cancellationToken);
                var newId = (count + 1).ToString();
                Id = newId;
            }
            else
            {
                var n = Convert.ToInt32(this.Id);
                n++;
                Id = n.ToString();
            }
            return Id;
        }
        private static void SetProperties(Domain.Entities.EstateSubsection estateSubSection,EntityData data,string sectionId)
        {                        
            estateSubSection.Code = GetValue<string>(data, "CODE");
            estateSubSection.Title = GetValue<string>(data, "UNITNAME");
            estateSubSection.SsaaCode = GetValue<string>(data, "SSAACODE");
            estateSubSection.EstateSectionId = sectionId;
            var state= GetValue<int>(data, "state");
            estateSubSection.State = state == 1 ? "1" : "2";
        }      
        private static T GetValue<T>(EntityData data,string fieldName)
        {
            return GeneralHelper.GetValue<T>(data, fieldName);
        }                       
    }
}
