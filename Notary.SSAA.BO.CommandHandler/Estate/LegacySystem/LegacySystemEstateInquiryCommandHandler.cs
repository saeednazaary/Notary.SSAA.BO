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
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.LegacySystem
{
    public class LegacySystemEstateInquiryCommandHandler : BaseExternalCommandHandler<EstateInquiryLegacySystemCommand, ExternalApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeriDaftarRepository;
        private readonly IRepository<EstateInquirySendedSm> _EstateInquirySendedSmRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly IDateTimeService _dateTimeService;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        private readonly ConfigurationParameterHelper _configurationParameterHelper;
        EstateInquiryLegacySystemCommand _request = null;
        public LegacySystemEstateInquiryCommandHandler(IMediator mediator, IUserService userService,ILogger logger,
            IEstateInquiryRepository estateInquiryRepository,
            IEstateSectionRepository estateSectionRepository,
            IEstateSubSectionRepository estateSubSectionRepository,
            IEstateSeriDaftarRepository estateSeriDaftarRepository,
            IDateTimeService dateTimeService,
            IRepository<EstateInquirySendedSm> EstateInquirySendedSmRepository,
            IRepository<ConfigurationParameter> configurationParameterRepository,
            IRepository<SsrApiExternalUser> ssrApiExternalUser
            )
            : base(mediator, userService,logger)
        {
            _estateInquiryRepository = estateInquiryRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSeriDaftarRepository = estateSeriDaftarRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _dateTimeService = dateTimeService;
            _EstateInquirySendedSmRepository = EstateInquirySendedSmRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _ssrApiExternalUser = ssrApiExternalUser;
            _configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, mediator);
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(EstateInquiryLegacySystemCommand request, IList<string> userRoles)
        {
            return true;
        }
        int nextOrder = int.MaxValue;
        protected async override Task<ExternalApiResult> ExecuteAsync(EstateInquiryLegacySystemCommand request, CancellationToken cancellationToken)
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
                await SaveEstateSection(request.Data, cancellationToken);
                await SaveEstateSubSection(request.Data, cancellationToken);
                await SaveEstateSeriDaftar(request.Data, cancellationToken);
                var list = request.Data.Where(x => x.EntityName.Equals("estestateinquiry", StringComparison.OrdinalIgnoreCase)).ToList();
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
                        var estateInquiryId = await SaveEstateInquiry(data, commitChanges, cancellationToken);
                        await SendSMS(estateInquiryId.ToString(), cancellationToken);
                    }
                    else if (data.CommandType == 3)
                    {
                        await DeleteEstateInquiry(data, commitChanges, cancellationToken);
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
        private async Task SaveEstateSeriDaftar(List<EntityData> data, CancellationToken cancellationToken)
        {
            var list = data.Where(x => x.EntityName.Equals("bstseridaftar", StringComparison.OrdinalIgnoreCase)).ToList();            
            if (list.Count > 0)
                await _mediator.Send(new EstateSeriDaftarLegacySystemCommand() { Data = list }, cancellationToken);
        }
        private async Task SaveEstateSubSection(List<EntityData> data, CancellationToken cancellationToken)
        {
            var list = data.Where(x => x.EntityName.Equals("bstsection", StringComparison.OrdinalIgnoreCase)).ToList();
            List<EntityData> finalList = new List<EntityData>();
            foreach (var d in list)
            {
                if (d.FieldValues.Where(x => x.FieldName.Equals("bstsectionid", StringComparison.OrdinalIgnoreCase) && x.Value != null).Any())
                {
                    finalList.Add(d);
                }
            }
            if (finalList.Count > 0)
                await _mediator.Send(new EstateSubSectionLegacySystemCommand() { Data = finalList }, cancellationToken);
        }
        private async Task SaveEstateSection(List<EntityData> data, CancellationToken cancellationToken)
        {
            var list = data.Where(x => x.EntityName.Equals("bstsection", StringComparison.OrdinalIgnoreCase)).ToList();
            List<EntityData> finalList = new List<EntityData>();
            foreach (var d in list)
            {
                if (d.FieldValues.Where(x => x.FieldName.Equals("unitid", StringComparison.OrdinalIgnoreCase) && x.Value != null).Any())
                {
                    finalList.Add(d);
                }
            }
            if (finalList.Count > 0)
                await _mediator.Send(new EstateSectionLegacySystemCommand() { Data = finalList }, cancellationToken);
        }
        private async Task DeleteEstateInquiry(EntityData data, bool commitChanges, CancellationToken cancellationToken)
        {
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var masterEntity = await _estateInquiryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase), cancellationToken);
            await _estateInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateInquiryPeople, cancellationToken);
            await _estateInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateInquirySendreceiveLogs, cancellationToken);

            masterEntity.EstateInquiryPeople.Remove(masterEntity.EstateInquiryPeople.First());
            masterEntity.EstateInquirySendreceiveLogs.Clear();
            if (masterEntity.EstateInquiryId.HasValue && masterEntity.IsFollowedInquiryUpdated == EstateConstant.BooleanConstant.True)
            {
                var followedInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, masterEntity.EstateInquiryId.Value);

                followedInquiry.ResponseResult = "True";
                followedInquiry.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;
                followedInquiry.Timestamp++;
                _estateInquiryRepository.Update(followedInquiry, false);
            }
            _estateInquiryRepository.Delete(masterEntity,commitChanges);
        }
        GetGeolocationByIdViewModel geLocations = new();
        GetGeolocationByIdViewModel iranGeoLocation=new();
        private async Task<Guid> SaveEstateInquiry(EntityData data, bool commitChanges, CancellationToken cancellationToken)
        {
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var estateInquiry = await _estateInquiryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase) , cancellationToken);
            var geoloctionId = data.FieldValues.Where(fv => fv.FieldName.Equals("GeoLocationId", StringComparison.OrdinalIgnoreCase)).First().Value;
            bool isNew = false;
            if (estateInquiry == null)
            {
                estateInquiry = new Domain.Entities.EstateInquiry();
                estateInquiry.Id = Guid.NewGuid();
                estateInquiry.LegacyId = idValue;
                estateInquiry.CreateDate = GeneralHelper.GetDatePart(GetValue<string>(data, "REGISTRATIONTIME"));
                estateInquiry.CreateTime = GeneralHelper.GetTimePart(GetValue<string>(data, "REGISTRATIONTIME"));

                var person = new Domain.Entities.EstateInquiryPerson();
                person.Id = Guid.NewGuid();
                person.EstateInquiryId = estateInquiry.Id;
                estateInquiry.EstateInquiryPeople.Add(person);
                isNew = true;
            }
            else
            {
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquirySendreceiveLogs, cancellationToken);
                await _estateInquiryRepository.LoadReferenceAsync(estateInquiry, x => x.EstateInquiryNavigation, cancellationToken);
                
            }
            object cityId = "---";
            object nationalityId = "---";
            object issuePlaceId = "---";
            object birthPlaceId = "---";
            var bstPerson = _request.Data.Where(x => x.EntityName.Equals("bstperson", StringComparison.OrdinalIgnoreCase) && x.OrderNo == data.OrderNo + 1).FirstOrDefault();
            var bstPersonId = (bstPerson != null) ? bstPerson.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString() : "-";
            var legacyGeoIdList = new List<string>();
            if (geoloctionId != null)
            {
                legacyGeoIdList.Add(geoloctionId.ToString());
            }
            if (bstPerson != null)
            {
                cityId = bstPerson.FieldValues.Where(fv => fv.FieldName.Equals("CITYNAMEID", StringComparison.OrdinalIgnoreCase)).First().Value;
                nationalityId = bstPerson.FieldValues.Where(fv => fv.FieldName.Equals("NATIONALITYID", StringComparison.OrdinalIgnoreCase)).First().Value;
                if (cityId != null)
                    legacyGeoIdList.Add(cityId.ToString());
                if (nationalityId != null)
                    legacyGeoIdList.Add(nationalityId.ToString());
            }
            var bstRealPerson = _request.Data.Where(x => x.EntityName.Equals("bstrealperson", StringComparison.OrdinalIgnoreCase) && x.OrderNo == data.OrderNo + 2).FirstOrDefault();
            if (bstRealPerson != null)
            {
                issuePlaceId = bstRealPerson.FieldValues.Where(fv => fv.FieldName.Equals("BIRTHISSUEID", StringComparison.OrdinalIgnoreCase)).First().Value;
                birthPlaceId = bstRealPerson.FieldValues.Where(fv => fv.FieldName.Equals("BIRTHPLACEID", StringComparison.OrdinalIgnoreCase)).First().Value;
                if (issuePlaceId != null)
                    legacyGeoIdList.Add(issuePlaceId.ToString());
                if (birthPlaceId != null)
                    legacyGeoIdList.Add(birthPlaceId.ToString());
            }
            var bstLegalPerson = _request.Data.Where(x => x.EntityName.Equals("bstlegalperson", StringComparison.OrdinalIgnoreCase) && x.OrderNo == data.OrderNo + 2).FirstOrDefault();
            if (legacyGeoIdList.Count > 0)
            {
                geLocations = await _baseInfoServiceHelper.GetGeolocationByLegacyId(legacyGeoIdList.ToArray(), cancellationToken);
            }
            iranGeoLocation = await _baseInfoServiceHelper.GetGeoLocationOfIran(cancellationToken);
            var senderId = GetValue<string>(data, "PRODUCERINQUIRYID");
            var receiverId = GetValue<string>(data, "SELFINQUIRYID");
            var organizationList = await _baseInfoServiceHelper.GetOrganizationByLegacyId(new string[] { senderId, receiverId }, cancellationToken);
            var scriptoriumId = organizationList.OrganizationList.Where(x => x.LegacyId.Equals(senderId, StringComparison.OrdinalIgnoreCase)).First().ScriptoriumId;
            var unitId = organizationList.OrganizationList.Where(x => x.LegacyId.Equals(receiverId, StringComparison.OrdinalIgnoreCase)).First().UnitId;
            SetProperties(estateInquiry, data, scriptoriumId, unitId);
            bool isLegacyInquiry = false;
            if (!string.IsNullOrWhiteSpace(estateInquiry.LegacyId))
                isLegacyInquiry = true;
            if (bstRealPerson != null)
                SetProperties(estateInquiry.EstateInquiryPeople.FirstOrDefault(), bstPerson, bstRealPerson, isNew, isLegacyInquiry, scriptoriumId, false);
            else if (bstLegalPerson != null)
                SetProperties(estateInquiry.EstateInquiryPeople.FirstOrDefault(), bstPerson, bstLegalPerson, isNew, isLegacyInquiry, scriptoriumId, true);
            var messageLogList = _request.Data.Where(x => x.EntityName.Equals("ESTATEINQUIRYMESSAGELOG", StringComparison.OrdinalIgnoreCase) && x.OrderNo > data.OrderNo && x.OrderNo < nextOrder).ToList();            
            List<string> messageLogIdList = new List<string>();
            foreach (var messageLog in messageLogList)
            {
                messageLogIdList.Add(messageLog.FieldValues.First(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).Value.ToString().ToUpper());
            }
            if (messageLogIdList.Count > 0 && estateInquiry.EstateInquirySendreceiveLogs.Count > 0)
            {
                var lst = estateInquiry.EstateInquirySendreceiveLogs.Where(x => !string.IsNullOrWhiteSpace(x.LegacyId)).Select(x => x.LegacyId).ToList();
                messageLogIdList = messageLogIdList.Except(lst).ToList();
            }
            foreach (var messageLog in messageLogList)
            {
                if (messageLogIdList.Contains(GetValue<string>(messageLog, "ID").ToUpper()))
                {
                    var newMessageLog = new EstateInquirySendreceiveLog();
                    newMessageLog.ActionDate = GeneralHelper.GetDatePart(GetValue<string>(messageLog, "TRTSREADTIME"));
                    newMessageLog.ActionNumber = "-";
                    newMessageLog.ActionText = GetValue<string>(messageLog, "UNITMESSAGE");
                    newMessageLog.ActionTime = GeneralHelper.GetTimePart(GetValue<string>(messageLog, "TRTSREADTIME"));
                    newMessageLog.EstateInquiryActionTypeId = "3";
                    newMessageLog.Id = new Guid();
                    newMessageLog.Ilm = "1";
                    newMessageLog.LegacyId = GetValue<string>(messageLog, "ID");
                    newMessageLog.Timestamp = 1;
                    newMessageLog.ScriptoriumId = scriptoriumId;
                    newMessageLog.WorkflowStatesId = (estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedCorrection || estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedDocument) ? estateInquiry.WorkflowStatesId : EstateConstant.EstateInquiryStates.NeedCorrection;
                    newMessageLog.EstateInquiryId = estateInquiry.Id;
                    estateInquiry.EstateInquirySendreceiveLogs.Add(newMessageLog);
                }
            }
            if (
             estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.ConfirmResponse || estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.RejectResponse)
            {
                var newMessageLog = new EstateInquirySendreceiveLog();
                newMessageLog.ActionDate = _dateTimeService.CurrentPersianDate;
                newMessageLog.ActionNumber = "-";
                newMessageLog.ActionText = estateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.ConfirmResponse ? "دریافت پاسخ تایید" : "دریافت پاسخ رد/بازداشت";
                newMessageLog.ActionTime = _dateTimeService.CurrentTime;
                newMessageLog.EstateInquiryActionTypeId = "3";
                newMessageLog.Id = new Guid();
                newMessageLog.Ilm = "1";
                newMessageLog.LegacyId = null;
                newMessageLog.Timestamp = 1;
                newMessageLog.ScriptoriumId = scriptoriumId;
                newMessageLog.WorkflowStatesId = estateInquiry.WorkflowStatesId;
                newMessageLog.EstateInquiryId = estateInquiry.Id;
                estateInquiry.EstateInquirySendreceiveLogs.Add(newMessageLog);
            }
            if (isNew)
            {               
                if (commitChanges)
                    await _estateInquiryRepository.AddAsync(estateInquiry, cancellationToken);
                else
                    await _estateInquiryRepository.AddAsync(estateInquiry, cancellationToken, false);
            }
            else if (commitChanges)
                await _estateInquiryRepository.UpdateAsync(estateInquiry, cancellationToken);
            else
                await _estateInquiryRepository.UpdateAsync(estateInquiry, cancellationToken, false);
            return estateInquiry.Id;
        }        
        private void SetProperties(EstateInquiryPerson estateInquiryPerson, EntityData bstPerson, EntityData bstRealLegalPerson, bool isNew,bool isLegacyInquiry,string scriptoriumId,bool isLegalPerson)
        {
            if (estateInquiryPerson == null) return;
            if (bstPerson == null) return;
            if (bstRealLegalPerson == null) return;
            if(!isLegalPerson)
            {
                var nationality = GetValue<string>(bstPerson, "NATIONALITYID");
                bool isIrani = true;
                if (!string.IsNullOrWhiteSpace(nationality))
                {
                    if (!iranGeoLocation.GeolocationList[0].LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase))
                        isIrani = false;
                }
                estateInquiryPerson.Address = GetValue<string>(bstPerson, "ADDRESS");
                estateInquiryPerson.BirthDate= GetValue<string>(bstRealLegalPerson, "BIRTHDATE");
                estateInquiryPerson.ScriptoriumId = scriptoriumId;
                var birthPlaceId = GetValue<string>(bstRealLegalPerson, "BIRTHPLACEID");
                if (!string.IsNullOrWhiteSpace(birthPlaceId))
                {
                    var birthPlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(birthPlaceId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (birthPlace != null)
                    {
                        estateInquiryPerson.BirthPlaceId = Convert.ToInt32(birthPlace.Id);
                    }
                    else
                        estateInquiryPerson.BirthPlaceId = null;
                }
                else
                    estateInquiryPerson.BirthPlaceId = null;

                var cityId = GetValue<string>(bstPerson, "CITYNAMEID");
                if (!string.IsNullOrWhiteSpace(cityId))
                {
                    var cityPlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(cityId, StringComparison.OrdinalIgnoreCase) ).FirstOrDefault();
                    if (cityPlace != null)
                    {
                        estateInquiryPerson.CityId = Convert.ToInt32(cityPlace.Id);
                    }
                    else
                        estateInquiryPerson.CityId = null;
                }
                else
                    estateInquiryPerson.CityId = null;
                var executiveTransfer = GetValue<int?>(bstRealLegalPerson, "EXECUTIVETRANSFER");
                estateInquiryPerson.ExecutiveTransfer = executiveTransfer != null && executiveTransfer == 1 ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                estateInquiryPerson.Family = GetValue<string>(bstRealLegalPerson, "FAMILY");
                estateInquiryPerson.FatherName = GetValue<string>(bstRealLegalPerson, "FATHERNAME");
                estateInquiryPerson.ForiegnBirthPlace = GetValue<string>(bstRealLegalPerson, "FORIEGNBIRTHPLACE");
                estateInquiryPerson.ForiegnIssuePlace= GetValue<string>(bstRealLegalPerson, "FORIEGNISSUEPLACE");
                estateInquiryPerson.IdentityNo= GetValue<string>(bstRealLegalPerson, "IDENTIFICATIONNO");
                estateInquiryPerson.Ilm = "1";
                estateInquiryPerson.IsOriginal = EstateConstant.BooleanConstant.True;
                estateInquiryPerson.IsRelated = EstateConstant.BooleanConstant.False;
                estateInquiryPerson.IsIlencChecked = EstateConstant.BooleanConstant.False;
                estateInquiryPerson.IsIlencCorrect= EstateConstant.BooleanConstant.False;
                var isVerified = GetValue<string>(bstPerson, "ISVERIFIED");
                if(isIrani)
                {
                    estateInquiryPerson.IsForeignerssysChecked = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsForeignerssysCorrect = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsIranian = EstateConstant.BooleanConstant.True;
                    estateInquiryPerson.IsSabtahvalChecked = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsSabtahvalCorrect = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                }
                else
                {
                    estateInquiryPerson.IsSabtahvalChecked = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsSabtahvalCorrect = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsIranian = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsForeignerssysChecked = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsForeignerssysCorrect = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                }
                var issuePlaceId = GetValue<string>(bstRealLegalPerson, "BIRTHISSUEID");
                if (!string.IsNullOrWhiteSpace(issuePlaceId))
                {
                    var issuePlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(issuePlaceId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (issuePlace != null)
                    {
                        estateInquiryPerson.IssuePlaceId = Convert.ToInt32(issuePlace.Id);
                    }
                    else
                        estateInquiryPerson.IssuePlaceId = null;
                }
                else
                    estateInquiryPerson.IssuePlaceId = null;
                estateInquiryPerson.IssuePlaceText = GetValue<string>(bstRealLegalPerson, "SABTEAHVALISUUEPLACE");                
                estateInquiryPerson.LegacyId = (isNew || isLegacyInquiry) ? GetValue<string>(bstRealLegalPerson, "id") : null;
                estateInquiryPerson.OtherLegacyId = (isNew || isLegacyInquiry) ? GetValue<string>(bstPerson, "id") : null;
                estateInquiryPerson.Name= GetValue<string>(bstRealLegalPerson, "NAME");
                estateInquiryPerson.NationalityCode= GetValue<string>(bstRealLegalPerson, "MELLICODE");
                if (!string.IsNullOrWhiteSpace(nationality))
                {
                    var na = geLocations.GeolocationList.Where(x => x.LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (na != null)
                    {
                        estateInquiryPerson.NationalityId = Convert.ToInt32(na.Id);
                    }
                    else
                        estateInquiryPerson.NationalityId = Convert.ToInt32(iranGeoLocation.GeolocationList[0].Id);
                }
                else
                    estateInquiryPerson.NationalityId = null;
                estateInquiryPerson.PersonType = EstateConstant.PersonTypeConstant.RealPerson;
                estateInquiryPerson.PostalCode= GetValue<string>(bstPerson, "POSTALCODE");               
                estateInquiryPerson.Seri = GetValue<string>(bstRealLegalPerson, "SERI");
                estateInquiryPerson.SerialNo = GetValue<string>(bstRealLegalPerson, "SERIAL");
                var sex = GetValue<int?>(bstRealLegalPerson, "SEX");
                if (sex != null)
                {
                    estateInquiryPerson.SexType = (sex == 1) ? EstateConstant.PersonSexType.Male : EstateConstant.PersonSexType.Female;
                }
                else
                    estateInquiryPerson.SexType = null;
                estateInquiryPerson.SharePart= GetValue<decimal>(bstPerson, "SHAREPART");
                estateInquiryPerson.ShareTotal= GetValue<decimal>(bstPerson, "SHARETOTAL");
                estateInquiryPerson.ShareText = GetValue<string>(bstPerson, "SHARECONTEXT");
                estateInquiryPerson.Timestamp = GetValue<decimal>(bstPerson, "Timestamp");//isNew ? 1 : estateInquiryPerson.Timestamp + 1;
                estateInquiryPerson.VerifiedBySourceservice = null;
            }
            else
            {
                var nationality = GetValue<string>(bstPerson, "NATIONALITYID");
                bool isIrani = true;
                if (!string.IsNullOrWhiteSpace(nationality))
                {
                    if (!iranGeoLocation.GeolocationList[0].LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase))
                        isIrani = false;
                }
                estateInquiryPerson.ScriptoriumId = scriptoriumId;
                
                estateInquiryPerson.Address = GetValue<string>(bstPerson, "ADDRESS");
                estateInquiryPerson.BirthDate = GetValue<string>(bstRealLegalPerson, "REGISTERDATE");
                estateInquiryPerson.BirthPlaceId = null;

                var cityId = GetValue<string>(bstPerson, "CITYNAMEID");
                if (!string.IsNullOrWhiteSpace(cityId))
                {
                    var cityPlace = geLocations.GeolocationList.Where(g => g.LegacyId.Equals(cityId, StringComparison.OrdinalIgnoreCase) ).FirstOrDefault();
                    if (cityPlace != null)
                    {
                        estateInquiryPerson.CityId = Convert.ToInt32(cityPlace.Id);
                    }
                    else
                        estateInquiryPerson.CityId = null;
                }
                else estateInquiryPerson.CityId = null;

                var executiveTransfer = GetValue<int?>(bstRealLegalPerson, "EXECUTIVETRANSFER");
                estateInquiryPerson.ExecutiveTransfer = executiveTransfer != null && executiveTransfer == 1 ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                estateInquiryPerson.Family = null;
                estateInquiryPerson.FatherName = null ;
                estateInquiryPerson.ForiegnBirthPlace = null;
                estateInquiryPerson.ForiegnIssuePlace = null;
                estateInquiryPerson.IdentityNo = GetValue<string>(bstRealLegalPerson, "REGISTERNO");
                estateInquiryPerson.Ilm = "1";
                estateInquiryPerson.IsOriginal = EstateConstant.BooleanConstant.True;
                estateInquiryPerson.IsRelated = EstateConstant.BooleanConstant.False;
                estateInquiryPerson.IsSabtahvalChecked = EstateConstant.BooleanConstant.False;
                estateInquiryPerson.IsSabtahvalCorrect = EstateConstant.BooleanConstant.False;
                var isVerified = GetValue<string>(bstPerson, "ISVERIFIED");
                if (isIrani)
                {
                    estateInquiryPerson.IsForeignerssysChecked = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsForeignerssysCorrect = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsIranian = EstateConstant.BooleanConstant.True;
                    estateInquiryPerson.IsIlencChecked = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsIlencCorrect = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                }
                else
                {
                    estateInquiryPerson.IsIlencChecked = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsIlencCorrect = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsIranian = EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsForeignerssysChecked = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                    estateInquiryPerson.IsForeignerssysCorrect = isVerified != null && isVerified == "true" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                }
                estateInquiryPerson.IssuePlaceId = null;
                estateInquiryPerson.IssuePlaceText = null;               
                estateInquiryPerson.LegacyId = (isNew || isLegacyInquiry) ? GetValue<string>(bstRealLegalPerson, "id") : null;
                estateInquiryPerson.OtherLegacyId = (isNew || isLegacyInquiry) ? GetValue<string>(bstPerson, "id") : null;
                estateInquiryPerson.Name = GetValue<string>(bstRealLegalPerson, "NAME");
                estateInquiryPerson.NationalityCode = GetValue<string>(bstRealLegalPerson, "MELLICODE");
                if (!string.IsNullOrWhiteSpace(nationality))
                {
                    var na = geLocations.GeolocationList.Where(x => x.LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (na != null)
                    {
                        estateInquiryPerson.NationalityId = Convert.ToInt32(na.Id);
                    }
                    else
                        estateInquiryPerson.NationalityId = Convert.ToInt32(iranGeoLocation.GeolocationList[0].Id);
                }
                else
                    estateInquiryPerson.NationalityId = null;
                estateInquiryPerson.PersonType = EstateConstant.PersonTypeConstant.LegalPerson;
                estateInquiryPerson.PostalCode = GetValue<string>(bstPerson, "POSTALCODE");
                estateInquiryPerson.Seri = null;
                estateInquiryPerson.SerialNo = null;
                estateInquiryPerson.SeriAlpha = null;
                estateInquiryPerson.SexType = null;
                estateInquiryPerson.SharePart = GetValue<decimal>(bstPerson, "SHAREPART");
                estateInquiryPerson.ShareTotal = GetValue<decimal>(bstPerson, "SHARETOTAL");
                estateInquiryPerson.ShareText = GetValue<string>(bstPerson, "SHARECONTEXT");
                estateInquiryPerson.Timestamp = GetValue<decimal>(bstPerson, "Timestamp");//isNew ? 1 : estateInquiryPerson.Timestamp + 1;
                estateInquiryPerson.VerifiedBySourceservice = null;
            }
            return ;
        }
        private void SetProperties(Domain.Entities.EstateInquiry estateInquiry,EntityData data,string scriptoriumId,string unitId)
        {            
            var sendingStatus= GetValue<string>(data, "SENDINGSTATUS");            
            estateInquiry.RelatedOwnershipId = GetValue<string>(data, "relatedownershipid");
            estateInquiry.ApartmentsTotalarea = GetValue<decimal?>(data, "APARTMENTSTOTALAREA");
            estateInquiry.Area = GetValue<decimal?>(data, "AREA");
            var atds = GetValue<string>(data, "ATTACHEDTODEALSUMMARY");
            estateInquiry.AttachedToDealsummary = atds != null && atds == "1" ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            estateInquiry.Basic = GetValue<string>(data, "BASIC");
            var basicRemaining = GetValue<string>(data, "BASICAPPENDANT");
            estateInquiry.BasicRemaining = basicRemaining != null && basicRemaining.Equals("true", StringComparison.OrdinalIgnoreCase) ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            estateInquiry.BillNo= GetValue<string>(data, "BILLNO");
            estateInquiry.CaseagentResponse = GetValue<string>(data, "CaseAgentResponse");
            estateInquiry.DealSummaryDate= GetValue<string>(data, "DEALSUMMARYDATE");
            estateInquiry.DealSummaryNo= GetValue<string>(data, "DEALSUMMARYNO");
            estateInquiry.DealSummaryScriptorium= GetValue<string>(data, "REGULATORYNOTARYOFFICE");
            estateInquiry.DocPrintNo= GetValue<string>(data, "PRINTEDDOCSNO");
            var documentIsNote = GetValue<string>(data, "ISNOTEBOOKDOCUMENT");
            estateInquiry.DocumentIsNote= documentIsNote != null && documentIsNote.Equals("true", StringComparison.OrdinalIgnoreCase) ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            var documentIsReplica = GetValue<string>(data, "HASREPLACEDOCUMENT");
            estateInquiry.DocumentIsReplica= documentIsReplica != null && documentIsReplica.Equals("true", StringComparison.OrdinalIgnoreCase) ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            estateInquiry.EdeclarationNo= GetValue<string>(data, "EDECLERATIONNO");
            estateInquiry.ElectronicEstateNoteNo = GetValue<string>(data, "PAGENOTESYSTEMNO");
            estateInquiry.EstateAddress= GetValue<string>(data, "ADDRESS");
            var inquiryType= GetValue<decimal?>(data, "ISCURRENTESTATEINQUIRY");
            estateInquiry.EstateInquiryTypeId = inquiryType != null && inquiryType == 1 ? "2" : "1";
            estateInquiry.EstateNoteNo= GetValue<string>(data, "NOTEBOOKNO");
            estateInquiry.EstatePostalCode= GetValue<string>(data, "POSTALCODE");
            var sectionId = GetValue<string>(data, "SECTIONID");
            if (!string.IsNullOrWhiteSpace(sectionId))
            {
                estateInquiry.EstateSectionId = _estateSectionRepository.Get(x => x.LegacyId.Equals(sectionId, StringComparison.OrdinalIgnoreCase)).Id;
            }
            else
                estateInquiry.EstateSectionId = null;
            var seriDaftarId = GetValue<string>(data, "BSTSERIDAFTARID");
            if (!string.IsNullOrWhiteSpace(seriDaftarId))
                estateInquiry.EstateSeridaftarId = _estateSeriDaftarRepository.Get(x => x.LegacyId.Equals(seriDaftarId, StringComparison.OrdinalIgnoreCase)).Id;
            else
                estateInquiry.EstateSeridaftarId = null;
            var subSectionId = GetValue<string>(data, "SUBSECTIONID");
            if (!string.IsNullOrWhiteSpace(subSectionId))
            {
                estateInquiry.EstateSubsectionId = _estateSubSectionRepository.Get(x => x.LegacyId.Equals(subSectionId, StringComparison.OrdinalIgnoreCase)).Id;
            }
            else
                estateInquiry.EstateSubsectionId = null;
            estateInquiry.FirstReceiveDate = GeneralHelper.GetDatePart(GetValue<string>(data, "INSERTTIME"));
            estateInquiry.FirstReceiveTime = GeneralHelper.GetTimePart(GetValue<string>(data, "INSERTTIME"));
            estateInquiry.FirstSendDate= GeneralHelper.GetDatePart(GetValue<string>(data, "SENDTIME"));
            estateInquiry.FirstSendTime = GeneralHelper.GetTimePart(GetValue<string>(data, "SENDTIME"));

            var geolocationId = GetValue<string>(data, "GeoLocationId");
            if (!string.IsNullOrWhiteSpace(geolocationId))
            {
                estateInquiry.GeoLocationId = Convert.ToInt32( geLocations.GeolocationList.Where(x => x.LegacyId.Equals(geolocationId, StringComparison.OrdinalIgnoreCase)).First().Id);
            }
            else
                estateInquiry.GeoLocationId = null;
            estateInquiry.HowToPay = GetValue<string>(data, "HOWTOPAY");
            estateInquiry.Ilm = "1";
            estateInquiry.InquiryDate = GetValue<string>(data, "ENQUIRYDATE");
            estateInquiry.InquiryNo = GetValue<string>(data, "ENQUIRYNO");
            estateInquiry.InquiryPaymantRefno = GetValue<string>(data, "PISHNO");
            var isCostPaid = GetValue<int?>(data, "CONFIRMPAYCOST");
            estateInquiry.IsCostPaid = isCostPaid != null && isCostPaid == 1 ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            estateInquiry.LasteditReceiveDate = GeneralHelper.GetDatePart(GetValue<string>(data, "LASTEDITRECEIVETIME"));
            estateInquiry.LasteditReceiveTime = GeneralHelper.GetTimePart(GetValue<string>(data, "LASTEDITRECEIVETIME"));
            estateInquiry.LastSendDate= GeneralHelper.GetDatePart(GetValue<string>(data, "LASTSENDTIME"));
            estateInquiry.LastSendTime = GeneralHelper.GetTimePart(GetValue<string>(data, "LASTSENDTIME"));
            estateInquiry.Mention = GetValue<string>(data, "MENTION");
            estateInquiry.MobileNo= GetValue<string>(data, "SMSRECIEVERMOBILENUMBER");
            estateInquiry.MortgageText = GetValue<string>(data, "HASMORTGAGE");
            estateInquiry.No= GetValue<string>(data, "INQUIRYUNIQUEID");
            estateInquiry.No2= GetValue<string>(data, "UNIQUENO");
            estateInquiry.Note21PaymentRefno= GetValue<string>(data, "POSRELATEDITEM");
            estateInquiry.PageNo= GetValue<string>(data, "PAGENO");
            estateInquiry.PayCostDate = GeneralHelper.GetDatePart( GetValue<string>(data, "PAYCOSTDATETIME"));
            estateInquiry.PayCostTime = GeneralHelper.GetTimePart(GetValue<string>(data, "PAYCOSTDATETIME"));
            estateInquiry.PaymentType = GetValue<string>(data, "PAYMENTTYPE");
            estateInquiry.ReceiptNo = GetValue<string>(data, "RECEIPTNO");
            estateInquiry.RegisterNo= GetValue<string>(data, "REGISTERNO");
            if (!string.IsNullOrWhiteSpace(sendingStatus) && (sendingStatus == "8" || sendingStatus == "5"))
                estateInquiry.Response = GetValue<string>(data, "UNITMESSAGE");
            else
                estateInquiry.Response = GetValue<string>(data, "RESPONSE");
            estateInquiry.ResponseCode= GetValue<string>(data, "RESPONSECODE");
            estateInquiry.ResponseDate = GeneralHelper.GetDatePart(GetValue<string>(data, "RESPONSEDATE"));
            estateInquiry.ResponseTime= GeneralHelper.GetTimePart(GetValue<string>(data, "RESPONSEDATE"));
            estateInquiry.ResponseDigitalsignature = GetValue<byte[]>(data, "RESPONSEDIGITALSIGNATURE");
            estateInquiry.ResponseNumber = GetValue<string>(data, "RESPONSENUMBER");
            estateInquiry.ResponseReceiveDate = GeneralHelper.GetDatePart(GetValue<string>(data, "RESPONSERECEIVETIME"));
            estateInquiry.ResponseReceiveTime = GeneralHelper.GetTimePart(GetValue<string>(data, "RESPONSERECEIVETIME"));
            estateInquiry.ResponseResult= GetValue<string>(data, "RESPONSERESULT");
            estateInquiry.ResponseSubjectdn= GetValue<string>(data, "RESPONSESUBJECTDN");            
            estateInquiry.ScriptoriumId = scriptoriumId;
            estateInquiry.Secondary = GetValue<string>(data, "SECONDARY");
            var secondaryRemaining = GetValue<string>(data, "SECONDARYAPPENDANT");
            estateInquiry.SecondaryRemaining = secondaryRemaining != null && secondaryRemaining.Equals("true", StringComparison.OrdinalIgnoreCase) ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            estateInquiry.SeparationDate= GetValue<string>(data, "SEPARATIONDATE");
            estateInquiry.SeparationNo= GetValue<string>(data, "SEPARATIONNO");
            estateInquiry.SpecificStatus= GetValue<string>(data, "SPECIFICSTATUS");
            estateInquiry.SumPrices= GetValue<int?>(data, "SUMPRICES");
            estateInquiry.Timestamp = GetValue<decimal>(data, "Timestamp");
            estateInquiry.TrtsReadDate= GeneralHelper.GetDatePart(GetValue<string>(data, "TRTSREADTIME"));
            estateInquiry.TrtsReadTime = GeneralHelper.GetTimePart(GetValue<string>(data, "TRTSREADTIME"));
            estateInquiry.UnitId = unitId;
            var workflowStatesId = GetEstateInquiryWorkflowState(sendingStatus, estateInquiry.ResponseResult);
            estateInquiry.WorkflowStatesId = workflowStatesId;
            var FOLLOWEINQUIRYNO = GetValue<string>(data, "FOLLOWEINQUIRYNO");
            var FOLLOWERINQUIRYDATE =GetValue<string>(data, "FOLLOWERINQUIRYDATE");
            var FOLLOWINGINQUIRYID= GetValue<string>(data, "FOLLOWINGINQUIRYID");
            if (!string.IsNullOrWhiteSpace(FOLLOWEINQUIRYNO))
            {
                if (!string.IsNullOrWhiteSpace(FOLLOWINGINQUIRYID))
                {
                    estateInquiry.IsFollowedInquiryUpdated = EstateConstant.BooleanConstant.True;
                    if (estateInquiry.EstateInquiryNavigation != null)
                    {
                        if ((estateInquiry.EstateInquiryNavigation.Id == FOLLOWINGINQUIRYID.ToGuid() && string.IsNullOrWhiteSpace(estateInquiry.LegacyId)) || (!string.IsNullOrWhiteSpace(estateInquiry.EstateInquiryNavigation.LegacyId) && estateInquiry.EstateInquiryNavigation.LegacyId.Equals(FOLLOWINGINQUIRYID, StringComparison.OrdinalIgnoreCase)))
                        {

                            estateInquiry.EstateInquiryNavigation.ResponseResult = "False";
                            estateInquiry.EstateInquiryNavigation.WorkflowStatesId = EstateConstant.EstateInquiryStates.RejectResponse;                            
                        }
                        else
                        {
                            estateInquiry.EstateInquiryNavigation.ResponseResult = "True";
                            estateInquiry.EstateInquiryNavigation.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;
                            var followedInquiry = _estateInquiryRepository.Get(x => x.Id == FOLLOWINGINQUIRYID.ToGuid() || x.LegacyId.Equals(FOLLOWINGINQUIRYID, StringComparison.OrdinalIgnoreCase));
                            if (followedInquiry != null)
                            {
                                followedInquiry.ResponseResult = "False";
                                followedInquiry.WorkflowStatesId = EstateConstant.EstateInquiryStates.RejectResponse;
                                estateInquiry.EstateInquiryId = followedInquiry.Id;                                
                            }
                        }
                    }
                    else
                    {
                        var followedInquiry = _estateInquiryRepository.Get(x => x.Id == FOLLOWINGINQUIRYID.ToGuid() || x.LegacyId.Equals(FOLLOWINGINQUIRYID, StringComparison.OrdinalIgnoreCase));
                        if (followedInquiry != null)
                        {
                            followedInquiry.ResponseResult = "False";
                            followedInquiry.WorkflowStatesId = EstateConstant.EstateInquiryStates.RejectResponse;
                            estateInquiry.EstateInquiryId = followedInquiry.Id;                            
                        }
                    }
                }
                else
                {
                    estateInquiry.IsFollowedInquiryUpdated = EstateConstant.BooleanConstant.False;                    
                    if (estateInquiry.EstateInquiryNavigation != null)
                    {
                        estateInquiry.EstateInquiryNavigation.ResponseResult = "True";
                        estateInquiry.EstateInquiryNavigation.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;                        
                    }                    
                    var followedInquiry = _estateInquiryRepository.Get(x => x.ScriptoriumId == scriptoriumId && x.InquiryDate == FOLLOWERINQUIRYDATE && x.InquiryNo == FOLLOWEINQUIRYNO);
                    if (followedInquiry != null)
                    {
                        estateInquiry.EstateInquiryId = followedInquiry.Id;                        
                    }
                }
            }
            else
            {
                estateInquiry.IsFollowedInquiryUpdated = EstateConstant.BooleanConstant.False;
               
                if (estateInquiry.EstateInquiryNavigation != null)
                {
                    estateInquiry.EstateInquiryNavigation.ResponseResult = "True";
                    estateInquiry.EstateInquiryNavigation.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;
                }
                estateInquiry.EstateInquiryId = null;                
            }
        }
        private static string GetEstateInquiryWorkflowState(string sendingStatus,string responseResult)
        {
            if (string.IsNullOrWhiteSpace(sendingStatus))
                return EstateConstant.EstateInquiryStates.NotSended;
            else if (sendingStatus == "1")
                return EstateConstant.EstateInquiryStates.Sended;
            else if (sendingStatus == "5")
                return EstateConstant.EstateInquiryStates.NeedDocument;
            else if (sendingStatus == "8")
                return EstateConstant.EstateInquiryStates.NeedCorrection;
            else if (sendingStatus == "4")
                return EstateConstant.EstateInquiryStates.Archived;
            else if (sendingStatus == "6")
                return EstateConstant.EstateInquiryStates.EditAndReSend;
            else if (sendingStatus == "3" && responseResult!=null && responseResult.Equals("true", StringComparison.OrdinalIgnoreCase))
                return EstateConstant.EstateInquiryStates.ConfirmResponse;
            else if (sendingStatus == "3" && responseResult != null && responseResult.Equals("false", StringComparison.OrdinalIgnoreCase))
                return EstateConstant.EstateInquiryStates.RejectResponse;
            else
                return EstateConstant.EstateInquiryStates.None;
            return "";
        }
        private static T GetValue<T>(EntityData data,string fieldName)
        {
            return GeneralHelper.GetValue<T>(data, fieldName);
        }                      
        private async Task SendSMS(string estateInquiryId, CancellationToken cancellationToken)
        {
            if(!this._request.SendSms) return;
            if (!Convert.ToBoolean(await _configurationParameterHelper.EstateInquirySendSMSIsEnabled(cancellationToken)))
                return;
            var masterEntity = await _estateInquiryRepository.TableNoTracking.Include(x=>x.EstateInquiryPeople).Where(x=>x.Id==estateInquiryId.ToGuid()).FirstOrDefaultAsync(cancellationToken);
            if (masterEntity == null) return;
            if (masterEntity.WorkflowStatesId!=EstateConstant.EstateInquiryStates.ConfirmResponse && masterEntity.WorkflowStatesId!=EstateConstant.EstateInquiryStates.RejectResponse) return;
            var person = masterEntity.EstateInquiryPeople.FirstOrDefault();
            if (person == null) return;
            if (!string.IsNullOrWhiteSpace(person.MobileNo) && person.MobileNoState == EstateConstant.BooleanConstant.True)
            {
                var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { masterEntity.UnitId }, cancellationToken);
                var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { masterEntity.ScriptoriumId }, cancellationToken);
                var msg = "";
                string msg1 = "مالک محترم" + Environment.NewLine + "استعلام ملک شما با شناسه یکتا سند مالکیت {0} در تاریخ {1} توسط واحد ثبت املاک {2} به {3} پاسخ داده شد.";
                string msg2 = "مالک محترم" + Environment.NewLine + "استعلام ملک شما با پلک اصلی  {0} و فرعی {1} در تاریخ {2} واحد ثبت املاک {3} به {4} پاسخ داده شد.";
                if (!string.IsNullOrWhiteSpace(masterEntity.ElectronicEstateNoteNo))
                    msg = string.Format(msg1, masterEntity.ElectronicEstateNoteNo, masterEntity.TrtsReadDate,  (unit!=null && unit.UnitList.Count>0) ? unit.UnitList.First().Name.Replace("حوزه ثبت ملک", "").Replace("حوزه ثبت ملك", ""):"-",  (scriptorium!=null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList.First().Name:"-");
                else
                    msg = string.Format(msg2, masterEntity.Basic, masterEntity.Secondary, masterEntity.TrtsReadDate, (unit != null && unit.UnitList.Count > 0) ?  unit.UnitList.First().Name.Replace("حوزه ثبت ملک", "").Replace("حوزه ثبت ملك", ""):"-", (scriptorium != null && scriptorium.ScriptoriumList.Count > 0) ? scriptorium.ScriptoriumList.First().Name:"-");
                var sendSMSServiceInput = new SendSmsFromKanoonServiceInput()
                {
                    ClientId = "SSAR",
                    MobileNo = person.MobileNo,
                    Message = msg
                };
                var smsResult = await _mediator.Send(sendSMSServiceInput, cancellationToken);
                if (smsResult.IsSuccess)
                {
                    if (!string.IsNullOrWhiteSpace(smsResult.Data.TrackCode))
                    {
                        var estateInquirySendedSm = new EstateInquirySendedSm()
                        {
                            EstateInquiryId = estateInquiryId.ToGuid(),
                            Id = Guid.NewGuid(),
                            Message = msg,
                            MobileNo = person.MobileNo,
                            SendDate = _dateTimeService.CurrentPersianDate,
                            SendTime = _dateTimeService.CurrentTime,
                            SmsTrackingNo = smsResult.Data.TrackCode
                        };
                        await _EstateInquirySendedSmRepository.AddAsync(estateInquirySendedSm, cancellationToken);
                    }
                }
            }
        }       
    }
}
