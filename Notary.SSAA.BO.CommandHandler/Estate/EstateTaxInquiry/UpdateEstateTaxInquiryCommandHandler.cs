using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class UpdateEstateTaxInquiryCommandHandler : BaseCommandHandler<UpdateEstateTaxInquiryCommand, ApiResult>
    {
        private protected Domain.Entities.EstateTaxInquiry masterEntity;
        private protected ApiResult<EstateTaxInquiryViewModel> apiResult;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;        
        SSAA.BO.Domain.Entities.EstateTaxInquiry prevEstateTaxInquiry = null;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper = null;
       
        public UpdateEstateTaxInquiryCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IWorkfolwStateRepository workfolwStateRepository) : base(mediator, userService, logger)
        {
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            _workfolwStateRepository = workfolwStateRepository;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            masterEntity = new();
            apiResult = new();                                  
        }
        protected override bool HasAccess(UpdateEstateTaxInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult> ExecuteAsync(UpdateEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _estateTaxInquiryRepository.GetByIdAsync(cancellationToken, request.InquiryId.ToGuid());
            if (masterEntity != null)
            {
                await _estateTaxInquiryRepository.LoadReferenceAsync(masterEntity, x => x.WorkflowStates, cancellationToken);
                await _estateTaxInquiryRepository.LoadCollectionAsync(masterEntity, x => x.EstateTaxInquiryPeople, cancellationToken);
                await _estateTaxInquiryRepository.LoadCollectionAsync(masterEntity, x => x.EstateTaxInquiryAttaches, cancellationToken);
            }
            await BusinessValidation(request, cancellationToken);                        
            if (apiResult.IsSuccess)
            {
                try
                {
                    await UpdateDatabase(request, cancellationToken);
                    if (apiResult.IsSuccess)
                    {
                        if (prevEstateTaxInquiry != null)
                        {
                            await _estateTaxInquiryRepository.UpdateAsync(prevEstateTaxInquiry, cancellationToken, false);
                        }
                        await _estateTaxInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                        var response = await _mediator.Send(new GetEstateTaxInquiryByIdQuery() { EstateTaxInquiryId = masterEntity.Id.ToString() }, cancellationToken);
                        if (response.IsSuccess)
                        {
                            apiResult.Data = response.Data.Adapt<EstateTaxInquiryViewModel>();
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
                catch (Exception)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.message.Add("خطا در ثبت درخواست رخ داد ");

                }
            }
            return apiResult;
        }

        private async Task BusinessValidation(UpdateEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            if (masterEntity == null)
            {
                apiResult.message.Add("استعلام مالیاتی با شناسه داده شده یافت نشد");
            }
            else if (masterEntity.IsActive == EstateConstant.BooleanConstant.False)
            {
                apiResult.message.Add("استعلام  مالیاتی غیر فعال می باشد و امکان ویرایش آن وجود ندارد");
            }
            else if (masterEntity.WorkflowStates.State == "40")
            {
                if (request.RequestType != 1 && request.RequestType != 3)
                {
                    apiResult.message.Add("استعلام در وضعیت قابل ویرایش نمی باشد");
                }
            }
            else if (masterEntity.WorkflowStates.State == "30")
            {

                if (request.RequestType != 3)
                {
                    apiResult.message.Add("استعلام در وضعیت قابل ویرایش نمی باشد");
                }
            }
            else if (masterEntity.WorkflowStates.State != "0" && masterEntity.WorkflowStates.State != "8" && masterEntity.WorkflowStates.State != "33" && masterEntity.WorkflowStates.State != "41" && masterEntity.WorkflowStates.State != "43")
            {
                apiResult.message.Add("استعلام در وضعیت قابل ویرایش نمی باشد");
            }

            var geoOfIran = await _baseInfoServiceHelper.GetGeoLocationOfIran(cancellationToken);
            if (geoOfIran != null)
            {
                if (request.TheInquiryOwner.PersonIsIrani)
                    request.TheInquiryOwner.PersonNationalityId = new List<string>() { geoOfIran.GeolocationList[0].Id };
                foreach (var buyer in request.TheInquiryBuyersList)
                {
                    if (buyer.PersonIsIrani)
                    {
                        buyer.PersonNationalityId = new List<string>() { geoOfIran.GeolocationList[0].Id };
                    }
                }
            }
            else
            {
                apiResult.message.Add("خطا در دریافت اطلاعات از سرویس اصلاعات پایه ....");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
            }
        }
        private async Task UpdateDatabase(UpdateEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            bool timestampAdded = false;
            if (request.IsDirty)
            {

                if (request.RequestType.HasValue)
                {
                    if (masterEntity.WorkflowStates.State == "40")
                    {
                        if (request.RequestType == 1)
                        {
                            var newState = await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "41", cancellationToken);
                            masterEntity.WorkflowStatesId = newState.Id;
                        }
                        if (request.RequestType == 3)
                        {
                            var newState = await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "43", cancellationToken);
                            masterEntity.WorkflowStatesId = newState.Id;
                        }
                    }
                    else if (masterEntity.WorkflowStates.State == "30")
                    {

                        if (request.RequestType == 3)
                        {
                            var newState = await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "33", cancellationToken);
                            masterEntity.WorkflowStatesId = newState.Id;
                        }
                    }

                }
                else
                {
                    prevEstateTaxInquiry = await _estateTaxInquiryRepository.Entities
                        .Include(x => x.WorkflowStates)
                        .Where(x => x.Id != request.InquiryId.ToGuid() && x.EstateInquiryId == request.EstateInquiryId.First().ToGuid() && x.IsActive == EstateConstant.BooleanConstant.True)
                        .FirstOrDefaultAsync(cancellationToken);
                    if (prevEstateTaxInquiry != null)
                    {
                        if (prevEstateTaxInquiry.WorkflowStates.State == "0" || prevEstateTaxInquiry.WorkflowStates.State == "8")
                        {
                            apiResult.IsSuccess = false;
                            apiResult.message.Add(string.Format("امکان ثبت وجود ندارد.استعلام مشابه  به شماره {0} و تاریخ {1} قبلا ثبت شده است که قابل ویرایش و ارسال  می باشد .لطفا از همان استعلام جهت ویرایش و ارسال به سازمان امور مالیاتی استفاده کنید ", prevEstateTaxInquiry.No, prevEstateTaxInquiry.CreateDate));
                            return;
                        }
                        else
                            prevEstateTaxInquiry.IsActive = EstateConstant.BooleanConstant.False;
                    }
                }
                if (request.IsDirty && request.IsValid)
                {
                    EstateTaxInquiryMapper.SetEntityValues(request, ref masterEntity);
                    if (masterEntity.EstateTaxInquiryBuildingStatusId == "01")
                    {
                        masterEntity.EstateTaxInquiryBuildingConstructionStepId = null;
                    }
                    if (masterEntity.IsGroundLevel == EstateConstant.BooleanConstant.True)
                    {
                        masterEntity.FloorNo = null;
                    }
                    masterEntity.Timestamp += 1;
                    timestampAdded = true;
                    masterEntity.IsActive = EstateConstant.BooleanConstant.True;
                }
            }
            if (request.TheInquiryOwner != null)
            {
                if (request.TheInquiryOwner.IsDirty && !request.TheInquiryOwner.IsDelete && request.TheInquiryOwner.IsValid)
                {
                    var person = masterEntity.EstateTaxInquiryPeople.Where(x => x.Id == request.TheInquiryOwner.PersonId.ToGuid()).First();                                        
                    EstateTaxInquiryMapper.SetEntityValues(request.TheInquiryOwner, ref person);
                    person.ChangeState = "2";
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                    person.Timestamp = person.Timestamp + 1;
                }
            }
            foreach (var buyer in request.TheInquiryBuyersList)
            {

                if (buyer.IsNew && !buyer.IsDelete && buyer.IsValid)
                {

                    var person = EstateTaxInquiryMapper.ViewModelToEntity(buyer);
                    person.Timestamp = 1;
                    person.Ilm = "1";
                    person.ScriptoriumId = masterEntity.ScriptoriumId;
                    person.Id = Guid.NewGuid();
                    person.LegacyId = "";
                    person.ChangeState = "1";
                    masterEntity.EstateTaxInquiryPeople.Add(person);
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;

                }
                else if (buyer.IsDirty && !buyer.IsNew && !buyer.IsDelete && buyer.IsValid)
                {
                    var person = masterEntity.EstateTaxInquiryPeople.Where(x => x.Id == buyer.PersonId.ToGuid()).First();                    
                    EstateTaxInquiryMapper.SetEntityValues(buyer, ref person);
                    person.ChangeState = "2";
                    person.Timestamp += 1;
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                }
                else if (buyer.IsDelete && !buyer.IsNew && buyer.IsValid)
                {
                    var person = masterEntity.EstateTaxInquiryPeople.Where(x => x.Id == buyer.PersonId.ToGuid()).First();                    
                    person.ChangeState = "3";
                    person.Timestamp += 1;
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                }
            }
            foreach (var estateAttach in request.TheInquiryEstateAttachList)
            {
                if (estateAttach.IsValid && !estateAttach.IsDelete && estateAttach.IsNew)
                {
                    var estateTaxInquiryAttach = EstateTaxInquiryMapper.ViewModelToEntity(estateAttach);
                    estateTaxInquiryAttach.Timestamp = 1;
                    estateTaxInquiryAttach.Ilm = "1";
                    estateTaxInquiryAttach.ScriptoriumId = masterEntity.ScriptoriumId;
                    estateTaxInquiryAttach.Id = Guid.NewGuid();
                    estateTaxInquiryAttach.LegacyId = "";
                    estateTaxInquiryAttach.ChangeState = "1";
                    masterEntity.EstateTaxInquiryAttaches.Add(estateTaxInquiryAttach);
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                }
                else if (estateAttach.IsValid && !estateAttach.IsNew && !estateAttach.IsDelete && estateAttach.IsDirty)
                {
                    var estateTaxInquiryAttach = masterEntity.EstateTaxInquiryAttaches.Where(x => x.Id == estateAttach.AttachId.ToGuid()).First();
                    EstateTaxInquiryMapper.SetEntityValues(estateAttach, ref estateTaxInquiryAttach);
                    estateTaxInquiryAttach.ChangeState = "2";
                    estateTaxInquiryAttach.Timestamp += 1;
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                }
                else if (estateAttach.IsValid && !estateAttach.IsNew && estateAttach.IsDelete)
                {
                    var estateTaxInquiryAttach = masterEntity.EstateTaxInquiryAttaches.Where(x => x.Id == estateAttach.AttachId.ToGuid()).First();                    
                    estateTaxInquiryAttach.ChangeState = "3";
                    estateTaxInquiryAttach.Timestamp += 1;
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                }
            }
        }       
    }
}
