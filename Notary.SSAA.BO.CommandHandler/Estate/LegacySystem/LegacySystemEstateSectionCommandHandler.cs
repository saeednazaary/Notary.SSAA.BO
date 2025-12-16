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
    public class LegacySystemEstateSectionCommandHandler : BaseExternalCommandHandler<EstateSectionLegacySystemCommand, ExternalApiResult>
    {        
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        private string Id;
        public LegacySystemEstateSectionCommandHandler(IMediator mediator, IUserService userService,ILogger logger,            
            IEstateSectionRepository estateSectionRepository,
            IRepository<SsrApiExternalUser> ssrApiExternalUser
            )
            : base(mediator, userService,logger)
        {            
            _estateSectionRepository = estateSectionRepository;
            _ssrApiExternalUser = ssrApiExternalUser;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            Id = string.Empty;
        }
        protected override bool HasAccess(EstateSectionLegacySystemCommand request, IList<string> userRoles)
        {
            return true;
        }
        protected async override Task<ExternalApiResult> ExecuteAsync(EstateSectionLegacySystemCommand request, CancellationToken cancellationToken)
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
                        await SaveEstateSection(data, commitChanges, cancellationToken);
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
        private async Task SaveEstateSection(EntityData data,bool commitChanges, CancellationToken cancellationToken)
        {            
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var unitIdValue = data.FieldValues.Where(fv => fv.FieldName.Equals("unitid", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var estateSection = await _estateSectionRepository.GetAsync(x => x.Id == idValue || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase), cancellationToken);        
            bool isNew = false;
            if (estateSection == null)
            {
                estateSection = new Domain.Entities.EstateSection();
                estateSection.Id = await GetNewId(cancellationToken);
                estateSection.LegacyId = idValue;               
                isNew = true;
            }
            var unit = await _baseInfoServiceHelper.GetUnitByLegacyId(new string[] { unitIdValue }, cancellationToken);            
            SetProperties(estateSection, data, isNew,  unit.UnitList.First().Id );          
            if(isNew)
            {
                if (commitChanges)
                    await _estateSectionRepository.AddAsync(estateSection, cancellationToken);
                else
                    await _estateSectionRepository.AddAsync(estateSection, cancellationToken,false);
            }
            else if(commitChanges)
                await _estateSectionRepository.UpdateAsync(estateSection, cancellationToken);
            else
                await _estateSectionRepository.UpdateAsync(estateSection, cancellationToken,false);
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
        private static void SetProperties(Domain.Entities.EstateSection estateSection,EntityData data,bool isNew,string unitId)
        {                        
            estateSection.Code = GetValue<string>(data, "CODE");
            estateSection.Title = GetValue<string>(data, "UNITNAME");
            estateSection.SsaaCode = GetValue<string>(data, "SSAACODE");
            estateSection.UnitId = unitId;
            var state= GetValue<int>(data, "state");
            estateSection.State = state == 1 ? "1" : "2";
        }      
        private static T GetValue<T>(EntityData data,string fieldName)
        {
           return GeneralHelper.GetValue<T>(data, fieldName);
        }                  
    }
}
