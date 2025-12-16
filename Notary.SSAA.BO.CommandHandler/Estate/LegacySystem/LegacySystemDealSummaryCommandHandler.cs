using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Notary.SSAA.BO.CommandHandler.Estate.LegacySystem
{
    public class LegacySystemDealSummaryCommandHandler : BaseExternalCommandHandler<DealSummaryLegacySystemCommand, ExternalApiResult>
    {
        private readonly IDealSummaryRepository _dealSummaryRepository;        
        private readonly IEstateInquiryRepository _estateInquiryRepository;       
        private readonly IEstateTransitionTypeRepository _estateTransitionTypeRepository;
        private readonly IEstateOwnershipTypeRepository _estateOwnershipTypeRepository;
        private readonly IDealSummaryTransferTypeRepository _dealSummaryTransferTypeRepository;
        private readonly IDealSummaryUnRestrictionTypeRepository _dealSummaryUnRestrictionTypeRepository;
        private readonly IDealSummaryPersonRelationTypeRepository _dealSummaryPersonRelationTypeRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper = null;
        LegacySystemCommand _request = null;
        public LegacySystemDealSummaryCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
            IDealSummaryRepository dealSummaryRepository,
            IEstateInquiryRepository estateInquiryRepository,
            IEstateTransitionTypeRepository estateTransitionTypeRepository,
            IEstateOwnershipTypeRepository estateOwnershipTypeRepository,
            IDealSummaryTransferTypeRepository dealSummaryTransferTypeRepository,
            IDealSummaryUnRestrictionTypeRepository dealSummaryUnRestrictionTypeRepository,
            IDealSummaryPersonRelationTypeRepository dealSummaryPersonRelationTypeRepository,
            IRepository<SsrApiExternalUser> ssrApiExternalUser
            )
            : base(mediator, userService, logger)
        {
            _dealSummaryRepository = dealSummaryRepository;
            _estateInquiryRepository = estateInquiryRepository;
            _dealSummaryPersonRelationTypeRepository = dealSummaryPersonRelationTypeRepository;
            _dealSummaryTransferTypeRepository = dealSummaryTransferTypeRepository;
            _estateOwnershipTypeRepository = estateOwnershipTypeRepository;
            _estateTransitionTypeRepository = estateTransitionTypeRepository;
            _dealSummaryUnRestrictionTypeRepository = dealSummaryUnRestrictionTypeRepository;
            _ssrApiExternalUser = ssrApiExternalUser;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(DealSummaryLegacySystemCommand request, IList<string> userRoles)
        {            
            return true;
        }
        int nextOrder = int.MaxValue;
        protected async override Task<ExternalApiResult> ExecuteAsync(DealSummaryLegacySystemCommand request, CancellationToken cancellationToken)
        {
            this._request = request;
            ExternalApiResult apiResult = new() { ResCode = "1", ResMessage = SystemMessagesConstant.Operation_Successful };
            var user = await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResCode = "105";
                apiResult.ResMessage = "نام کاربری یا کلمه عبور استفاده کننده  اشتباه می باشد";
                return apiResult;
            }
            else if (request.UserName!="OLD_ESTATE_APP")
            {
                apiResult.ResCode = "106";
                apiResult.ResMessage = "مجاز به استفاده از این سرویس نمی باشید";
                return apiResult;
            }
            try
            {
                if (request.Data.Count > 0)
                    request.Data = request.Data.OrderBy(x => x.OrderNo).ToList();

                var estateInquirylist = request.Data.Where(x => x.EntityName.Equals("estestateinquiry", StringComparison.OrdinalIgnoreCase)).ToList();
                if (estateInquirylist.Count > 0)
                {
                    EstateInquiryLegacySystemCommand estateInquiryLegacySystemCommand = new EstateInquiryLegacySystemCommand();
                    estateInquiryLegacySystemCommand.Data = request.Data;
                    await _mediator.Send(estateInquiryLegacySystemCommand, cancellationToken);
                }
                var list = request.Data.Where(x => x.EntityName.Equals("dsudealsummary", StringComparison.OrdinalIgnoreCase)).ToList();
                var orders = list.Select(x => x.OrderNo).ToArray();
                int index = 0;
                foreach (var data in list)
                {
                    try
                    {
                        nextOrder = orders[index + 1];
                    }
                    catch(IndexOutOfRangeException)
                    {

                    }
                    bool commitChanges = false;
                    if (index == list.Count - 1)
                        commitChanges = true;
                    if (data.CommandType == 1 || data.CommandType == 2)
                    {
                        await SaveDealSummary(data, commitChanges, cancellationToken);
                    }
                    else if (data.CommandType == 3)
                    {
                        await DeleteDealSummary(data, commitChanges, cancellationToken);
                    }

                    index++;
                }
            }
            catch(Exception ex)
            {
                apiResult.ResCode = "901";
                apiResult.ResMessage = "خطا در ثبت اطلاعات رخ داده است";
            }
            return apiResult;
        }

        private async Task DeleteDealSummary(EntityData data, bool commitChanges, CancellationToken cancellationToken)
        {
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var upperIdValue = idValue.ToUpper();
            var lowerIdValue = idValue.ToLower();
            var masterEntity = await _dealSummaryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId == upperIdValue || x.LegacyId == lowerIdValue, cancellationToken);
            await _dealSummaryRepository.LoadCollectionAsync(masterEntity, e => e.DealSummaryPeople, cancellationToken);
            await _dealSummaryRepository.LoadCollectionAsync(masterEntity, e => e.DealSummarySendreceiveLogs, cancellationToken);
            masterEntity.DealSummaryPeople.Clear();
            masterEntity.DealSummarySendreceiveLogs.Clear();           
            _dealSummaryRepository.Delete(masterEntity,commitChanges);
        }

        GetGeolocationByIdViewModel geLocations = new GetGeolocationByIdViewModel();
        GetGeolocationByIdViewModel iranGeoLocation=new GetGeolocationByIdViewModel();
        private async Task SaveDealSummary(EntityData data, bool commitChanges, CancellationToken cancellationToken)
        {
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var upperIdValue = idValue.ToUpper();
            var lowerIdValue = idValue.ToLower();
            var estEstateInquiryidValue = data.FieldValues.Where(fv => fv.FieldName.Equals("estestateinquiryid", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var upperEstEstateInquiryidValue=estEstateInquiryidValue.ToUpper();
            var lowerEstEstateInquiryidValue = estEstateInquiryidValue.ToUpper();
            var dealSummary = await _dealSummaryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId == upperIdValue || x.LegacyId == lowerIdValue, cancellationToken);
            var amountUnitId = data.FieldValues.Where(fv => fv.FieldName.Equals("AMOUNTUNITID", StringComparison.OrdinalIgnoreCase)).First().Value;
            var timeUnitId = data.FieldValues.Where(fv => fv.FieldName.Equals("TIMEUNITID", StringComparison.OrdinalIgnoreCase)).First().Value;
            bool isNew = false;
            if (dealSummary == null)
            {
                dealSummary = new Domain.Entities.DealSummary();
                dealSummary.Id = Guid.NewGuid();
                dealSummary.LegacyId = idValue;
                isNew = true;
            }
            else
            {
                await _dealSummaryRepository.LoadCollectionAsync(dealSummary, x => x.DealSummaryPeople, cancellationToken);
                await _dealSummaryRepository.LoadCollectionAsync(dealSummary, x => x.DealSummarySendreceiveLogs, cancellationToken);
            }
            MeasurementUnitTypeByIdViewModel measurementUnitTypes = null;
            if (amountUnitId != null || timeUnitId != null)
            {
                List<string> lst = new List<string>();
                if (amountUnitId != null)
                    lst.Add(amountUnitId.ToString());
                if (timeUnitId != null)
                    lst.Add(timeUnitId.ToString());
                measurementUnitTypes = await _baseInfoServiceHelper. GetMeasurementUnitTypeByLegacyId(lst.ToArray(), cancellationToken);
            }
            var estateInquiry = await _estateInquiryRepository.GetAsync(x => x.Id == estEstateInquiryidValue.ToGuid() || x.LegacyId == upperEstEstateInquiryidValue || x.LegacyId == lowerEstEstateInquiryidValue, cancellationToken);
            dealSummary.EstateInquiryId = estateInquiry.Id;
            if (amountUnitId != null)
                dealSummary.AmountUnitId = measurementUnitTypes.MesurementUnitTypeList.Where(x => !string.IsNullOrWhiteSpace(x.LegacyId) && x.LegacyId.Equals(amountUnitId.ToString(), StringComparison.OrdinalIgnoreCase)).First().Id;
            if (timeUnitId != null)
                dealSummary.TimeUnitId = measurementUnitTypes.MesurementUnitTypeList.Where(x => !string.IsNullOrWhiteSpace(x.LegacyId) && x.LegacyId.Equals(timeUnitId.ToString(), StringComparison.OrdinalIgnoreCase)).First().Id;
            await SetProperties(dealSummary, data, estateInquiry.ScriptoriumId, cancellationToken);            
            bool isLegacyInquiry = false;
            if (!string.IsNullOrWhiteSpace(dealSummary.LegacyId))
                isLegacyInquiry = true;
            object cityId = "---";
            object nationalityId = "---";
            object issuePlaceId = "---";
            object birthPlaceId = "---";
            var PersonList = _request.Data.Where(x => x.EntityName.Equals("dsureallegalperson", StringComparison.OrdinalIgnoreCase) && x.OrderNo > data.OrderNo && x.OrderNo < nextOrder).ToList();
            bool hasPerson = false;
            List<string> legacyGeoIdList = new List<string>();
            foreach (EntityData person in PersonList)
            {
                hasPerson = true;
                cityId = person.FieldValues.Where(fv => fv.FieldName.Equals("CITYID", StringComparison.OrdinalIgnoreCase)).First().Value;
                nationalityId = person.FieldValues.Where(fv => fv.FieldName.Equals("NATIONALITYID", StringComparison.OrdinalIgnoreCase)).First().Value;
                birthPlaceId = person.FieldValues.Where(fv => fv.FieldName.Equals("BIRTHDATEID", StringComparison.OrdinalIgnoreCase)).First().Value;
                issuePlaceId = person.FieldValues.Where(fv => fv.FieldName.Equals("ISSUEPLACEID", StringComparison.OrdinalIgnoreCase)).First().Value;
                if (cityId != null)
                    legacyGeoIdList.Add(cityId.ToString());
                if (nationalityId != null)
                    legacyGeoIdList.Add(nationalityId.ToString());
                if (birthPlaceId != null)
                    legacyGeoIdList.Add(birthPlaceId.ToString());
                if (issuePlaceId != null)
                    legacyGeoIdList.Add(issuePlaceId.ToString());

            }
            if (legacyGeoIdList.Count > 0)
            {
                geLocations = await _baseInfoServiceHelper.GetGeolocationByLegacyId(legacyGeoIdList.ToArray(), cancellationToken);
            }
            iranGeoLocation = await _baseInfoServiceHelper.GetGeoLocationOfIran(cancellationToken);

            if(hasPerson && !isNew)
            {
                dealSummary.DealSummaryPeople.Clear();
            }
            foreach (var person in PersonList)
            {
                var dealPerson = new DealSummaryPerson();
                dealPerson.Id = Guid.NewGuid();
                dealPerson.DealSummaryId = dealSummary.Id;
                await SetProperties(dealPerson, person, isNew, isLegacyInquiry, estateInquiry.ScriptoriumId, cancellationToken);
                dealSummary.DealSummaryPeople.Add(dealPerson);
            }              
            var newMessageLog = new DealSummarySendreceiveLog();
            newMessageLog.ActionNumber = "-";
            if (dealSummary.WorkflowStatesId == EstateConstant.DealSummaryStates.NotSended)
            {
                newMessageLog.ActionText = "ثبت خلاصه معامله";
                newMessageLog.DealSummaryActionTypeId = "1";
                newMessageLog.WorkflowStatesId = EstateConstant.DealSummaryStates.NotSended;
                newMessageLog.ActionTime = GeneralHelper.GetTimePart(GetValue<string>(data, "CREATEDATETIME"));
                newMessageLog.ActionDate = dealSummary.DealDate;
            }
            else if (dealSummary.WorkflowStatesId == EstateConstant.DealSummaryStates.Sended)
            {
                newMessageLog.ActionText = "ارسال خلاصه معامله";
                newMessageLog.DealSummaryActionTypeId = "2";
                newMessageLog.WorkflowStatesId = EstateConstant.DealSummaryStates.Sended;
                newMessageLog.ActionTime = GeneralHelper.GetTimePart(GetValue<string>(data, "SENDDATE"));
                newMessageLog.ActionDate = GeneralHelper.GetDatePart(GetValue<string>(data, "SENDDATE"));
            }
            else if (dealSummary.WorkflowStatesId == EstateConstant.DealSummaryStates.SendRemoveRestriction)
            {
                newMessageLog.ActionText = "ارسال رفع محدودیت";
                newMessageLog.DealSummaryActionTypeId = "2";
                newMessageLog.WorkflowStatesId = EstateConstant.DealSummaryStates.SendRemoveRestriction;
                newMessageLog.ActionTime = GeneralHelper.GetTimePart(GetValue<string>(data, "SENDDATE"));
                newMessageLog.ActionDate = GeneralHelper.GetDatePart(GetValue<string>(data, "SENDDATE"));
            }
            else if (dealSummary.WorkflowStatesId == EstateConstant.DealSummaryStates.Responsed)
            {
                newMessageLog.ActionText = "دریافت پاسخ";
                newMessageLog.DealSummaryActionTypeId = "3";
                newMessageLog.WorkflowStatesId = EstateConstant.DealSummaryStates.Responsed;
                newMessageLog.ActionTime = GeneralHelper.GetTimePart(GetValue<string>(data, "RESPONSEDATE"));
                newMessageLog.ActionDate = GeneralHelper.GetDatePart(GetValue<string>(data, "RESPONSEDATE"));
            }
            newMessageLog.Id = new Guid();
            newMessageLog.Ilm = "1";
            newMessageLog.Timestamp = 1;
            newMessageLog.ScriptoriumId = estateInquiry.ScriptoriumId;
            newMessageLog.DealSummaryId = dealSummary.Id;
            dealSummary.DealSummarySendreceiveLogs.Add(newMessageLog);
            if (isNew)
            {
                if (commitChanges)
                    await _dealSummaryRepository.AddAsync(dealSummary, cancellationToken);
                else
                    await _dealSummaryRepository.AddAsync(dealSummary, cancellationToken, false);
            }
            else if (commitChanges)
                await _dealSummaryRepository.UpdateAsync(dealSummary, cancellationToken);
            else
                await _dealSummaryRepository.UpdateAsync(dealSummary, cancellationToken, false);
        }

        private async Task SetProperties(DealSummaryPerson dealSummaryPerson, EntityData personData, bool isNew, bool isLegacyInquiry, string scriptoriumId, CancellationToken cancellationToken)
        {
            if (dealSummaryPerson == null) return;
            if (personData == null) return;
            var personType = GetValue<string>(personData, "PERSONTYPE");
            dealSummaryPerson.PersonType = personType;
            if (personType == "0")
                dealSummaryPerson.PersonType = "2";
            var nationality = GetValue<string>(personData, "NATIONALITYID");
            dealSummaryPerson.Address = GetValue<string>(personData, "ADDRESS");
            dealSummaryPerson.BirthDate = GetValue<string>(personData, "BIRTHDATE");
            dealSummaryPerson.ScriptoriumId = scriptoriumId;
            var birthPlaceId = GetValue<string>(personData, "BIRTHDATEID");
            if (!string.IsNullOrWhiteSpace(birthPlaceId))
            {
                var birthPlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(birthPlaceId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (birthPlace != null)
                {
                    dealSummaryPerson.BirthPlaceId = Convert.ToInt32(birthPlace.Id);
                }
                else
                    dealSummaryPerson.BirthPlaceId = null;
            }
            else
                dealSummaryPerson.BirthPlaceId = null;
            var cityId = GetValue<string>(personData, "CITYID");
            if (!string.IsNullOrWhiteSpace(cityId))
            {
                var cityPlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(cityId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (cityPlace != null)
                {
                    dealSummaryPerson.CityId = Convert.ToInt32(cityPlace.Id);
                }
                else
                    dealSummaryPerson.CityId = null;
            }
            else
                dealSummaryPerson.CityId = null;
            dealSummaryPerson.Family = GetValue<string>(personData, "FAMILY");
            dealSummaryPerson.FatherName = GetValue<string>(personData, "FATHERNAME");
            dealSummaryPerson.IdentityNo = GetValue<string>(personData, "IDENTIFICATIONNO");
            dealSummaryPerson.Ilm = "1";
            var issuePlaceId = GetValue<string>(personData, "ISSUEPLACEID");
            if (!string.IsNullOrWhiteSpace(issuePlaceId))
            {
                var issuePlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(issuePlaceId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (issuePlace != null)
                {
                    dealSummaryPerson.IssuePlaceId = Convert.ToInt32(issuePlace.Id);
                }
                else
                    dealSummaryPerson.IssuePlaceId = null;
            }
            else
                dealSummaryPerson.IssuePlaceId = null;
            dealSummaryPerson.LegacyId = (isNew || isLegacyInquiry) ? GetValue<string>(personData, "id") : null;
            dealSummaryPerson.Name = GetValue<string>(personData, "NAME");
            dealSummaryPerson.NationalityCode = GetValue<string>(personData, "NATIONALCODE");
            if (!string.IsNullOrWhiteSpace(nationality))
            {
                var na = geLocations.GeolocationList.Where(x => x.LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (na != null)
                {
                    dealSummaryPerson.NationalityId = Convert.ToInt32(na.Id);
                }
                else
                    dealSummaryPerson.NationalityId = Convert.ToInt32(iranGeoLocation.GeolocationList[0].Id);
            }
            else
                dealSummaryPerson.NationalityId = null;
            dealSummaryPerson.PostalCode = GetValue<string>(personData, "POSTALCODE");
            dealSummaryPerson.Seri = GetValue<string>(personData, "SERI");
            dealSummaryPerson.SerialNo = GetValue<string>(personData, "SERIAL");
            var sex = GetValue<int?>(personData, "SEX");
            if (sex != null)
            {
                dealSummaryPerson.SexType = (sex == 1) ? EstateConstant.PersonSexType.Male : EstateConstant.PersonSexType.Female;
            }
            else
                dealSummaryPerson.SexType = null;
            dealSummaryPerson.SharePart = GetValue<decimal?>(personData, "SHAREPART");
            dealSummaryPerson.ShareTotal = GetValue<decimal?>(personData, "SHARETOTAL");
            dealSummaryPerson.ShareText = GetValue<string>(personData, "SHARECONTEXT");
            dealSummaryPerson.Timestamp = GetValue<decimal>(personData, "Timestamp");
            dealSummaryPerson.ConditionText = GetValue<string>(personData, "DESCRIPTION");
            dealSummaryPerson.Ilm = "1";
            dealSummaryPerson.OctantQuarter = GetValue<string>(personData, "OctantQuarter");
            dealSummaryPerson.OctantQuarterPart = GetValue<string>(personData, "OctantQuarterPart");
            dealSummaryPerson.OctantQuarterTotal = GetValue<string>(personData, "OctantQuarterTotal");
            var relationType = GetValue<string>(personData, "DSURELATIONKINDID");
            dealSummaryPerson.RelationTypeId = (await _dealSummaryPersonRelationTypeRepository.GetAsync(x => x.LegacyId.Equals(relationType, StringComparison.OrdinalIgnoreCase), cancellationToken)).Id;
            return;
        }
        private async Task SetProperties(Domain.Entities.DealSummary dealSummary,EntityData data,string scriptoriumId,CancellationToken cancellationToken)
        {            
            var sendingStatus = GetValue<decimal?>(data, "SENDINGSTATUS");
            var workflowStatesId= GetDealSummaryWorkflowState(sendingStatus);                 
            dealSummary.FirstReceiveDate = GeneralHelper.GetDatePart(GetValue<string>(data, "INSERTTIME"));
            dealSummary.FirstReceiveTime = GeneralHelper.GetTimePart(GetValue<string>(data, "INSERTTIME"));                       
            dealSummary.Ilm = "1";          
            dealSummary.LasteditReceiveDate = GeneralHelper.GetDatePart(GetValue<string>(data, "LASTEDITRECEIVETIME"));
            dealSummary.LasteditReceiveTime = GeneralHelper.GetTimePart(GetValue<string>(data, "LASTEDITRECEIVETIME"));           
            dealSummary.No= GetValue<string>(data, "SYSTEMDEALNO");           
            dealSummary.Response = GetValue<string>(data, "RESPONSE");            
            dealSummary.ResponseDate = GeneralHelper.GetDatePart(GetValue<string>(data, "RESPONSEDATE"));
            dealSummary.ResponseTime= GeneralHelper.GetTimePart(GetValue<string>(data, "RESPONSEDATE"));                     
            dealSummary.ResponseReceiveDate = GeneralHelper.GetDatePart(GetValue<string>(data, "RESPONSERECEIVETIME"));
            dealSummary.ResponseReceiveTime = GeneralHelper.GetTimePart(GetValue<string>(data, "RESPONSERECEIVETIME"));            
            dealSummary.ScriptoriumId = scriptoriumId;          
            dealSummary.Timestamp = GetValue<decimal>(data, "Timestamp");            
            dealSummary.WorkflowStatesId = workflowStatesId;
            dealSummary.Amount = GetValue<long?>(data, "AMOUNT");
            dealSummary.AttachedText = GetValue<string>(data, "ATTACHED");
            dealSummary.CertificateBase64 = GetValue<string>(data, "CERTIFICATEBASE64");
            dealSummary.DataDigitalSignature = GetValue<byte[]>(data, "DATASIGNATURE");
            dealSummary.DealDate = GetValue<string>(data, "DEALDATE"); ;
            dealSummary.DealNo = GetValue<string>(data, "DEALNO"); 
            var transferTypeId= GetValue<string>(data, "DSUTRANSFERTYPEID");
            if (!string.IsNullOrWhiteSpace(transferTypeId))
                dealSummary.DealSummaryTransferTypeId = (await _dealSummaryTransferTypeRepository.GetAsync(x => x.LegacyId.Equals(transferTypeId, StringComparison.OrdinalIgnoreCase), cancellationToken)).Id;
            else
                dealSummary.DealSummaryTransferTypeId = null;
            dealSummary.Description = GetValue<string>(data, "DESCRIPTION"); 
            dealSummary.Duration = GetValue<decimal?>(data, "DURATION"); 
            var ownershipTypeId = GetValue<string>(data, "DSUOWNERSHIPTYPEID");
            if (!string.IsNullOrWhiteSpace(ownershipTypeId))
                dealSummary.EstateOwnershipTypeId = (await _estateOwnershipTypeRepository.GetAsync(x => x.LegacyId.Equals(ownershipTypeId, StringComparison.OrdinalIgnoreCase), cancellationToken)).Id;
            else
                dealSummary.EstateOwnershipTypeId = null;
            var transitionType = GetValue<string>(data, "DSUTRANSITIONCASEID");
            if (!string.IsNullOrWhiteSpace(transitionType))
                dealSummary.EstateTransitionTypeId = (await _estateTransitionTypeRepository.GetAsync(x => x.LegacyId.Equals(transitionType, StringComparison.OrdinalIgnoreCase), cancellationToken)).Id;
            else
                dealSummary.EstateTransitionTypeId = null;
            dealSummary.NotaryDocumentId = GetValue<string>(data, "NOTARYDOCUMENTNO");
            dealSummary.RemoveRestrictionDate = GetValue<string>(data, "REMOVERESTRICTIONDATE");
            dealSummary.RemoveRestrictionNo = GetValue<string>(data, "REMOVERESTRICTIONNO");
            dealSummary.SendDate = GeneralHelper.GetDatePart(GetValue<string>(data, "SENDDATE"));
            dealSummary.SendTime = GeneralHelper.GetTimePart(GetValue<string>(data, "SENDDATE"));
            dealSummary.SubjectDn = GetValue<string>(data, "SUBJECTDN");
            dealSummary.TransactionDate = GetValue<string>(data, "DEALMAINDATE");
            var unrestrictionTypeId = GetValue<string>(data, "DSUREMOVERESTIRCTIONTYPEID");
            if (!string.IsNullOrWhiteSpace(unrestrictionTypeId))
                dealSummary.UnrestrictionTypeId = (await _dealSummaryUnRestrictionTypeRepository.GetAsync(x => x.LegacyId.Equals(unrestrictionTypeId, StringComparison.OrdinalIgnoreCase), cancellationToken)).Id;
            else
                dealSummary.UnrestrictionTypeId = null;
        }
        private static string GetDealSummaryWorkflowState(decimal? sendingStatus)
        {
            if (sendingStatus==null)
                return EstateConstant.DealSummaryStates.NotSended;
            else if (sendingStatus == 1)
                return EstateConstant.DealSummaryStates.Sended;          
            else if (sendingStatus == 4)
                return EstateConstant.DealSummaryStates.Archived;
            else if (sendingStatus == 7)
                return EstateConstant.DealSummaryStates.SendRemoveRestriction;
            else if (sendingStatus == 3 )
                return EstateConstant.DealSummaryStates.Responsed;
            else
                return EstateConstant.DealSummaryStates.None;
            return "";
        }
        private static T GetValue<T>(EntityData data, string fieldName)
        {
            return GeneralHelper.GetValue<T>(data, fieldName);
        }                
    }
}