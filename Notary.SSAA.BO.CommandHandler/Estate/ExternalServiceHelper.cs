using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.MediaService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.MediaService;
using System.Data;
using Newtonsoft.Json;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.Coordinator;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;

namespace Notary.SSAA.BO.CommandHandler.Estate
{
    public class ExternalServiceHelper
    {
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeridaftarRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IDateTimeService _dateTimeService;
        public Domain.Entities.EstateInquiry EstateInquiry { get; set; }
        public Domain.Entities.EstateTaxInquiry EstateTaxInquiry { get; set; }
        public Domain.Entities.DealSummary DealSummary { get; set; }        
        private ConfigurationParameterHelper _configurationParameterHelper { get; set; }
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public ExternalServiceHelper(IMediator mediator, IDateTimeService dateTimeService, IUserService userService,
            ConfigurationParameterHelper configurationParameterHelper, IEstateSeriDaftarRepository estateSeridaftarRepository, IEstateSectionRepository estateSectionRepository, IEstateSubSectionRepository estateSubSectionRepository,
            IConfiguration configuration,
            IHttpEndPointCaller httpEndPointCaller,
            IWorkfolwStateRepository workfolwStateRepository,
             IEstateTaxInquiryRepository estateTaxInquiryRepository)

        {
            _dateTimeService = dateTimeService;
            _configurationParameterHelper = configurationParameterHelper;
            _estateSeridaftarRepository = estateSeridaftarRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _userService = userService;
            _mediator = mediator;
            _configuration = configuration;
            _workfolwStateRepository = workfolwStateRepository;
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
        }
        
        public ScriptoriumData estateInquiryScriptorium = null;
        public UnitData estateInquiryUnit = null;
        public OrganizationData estateInquiryScriptoriumOrganization = null;
        public OrganizationData estateInquiryUnitOrganization = null;
        public List<GeolocationData> estateInquiryGeolocations = null;
        public async Task GetEstateInquiryBaseInfoData(CancellationToken cancellationToken)
        {
            if (EstateInquiry == null) return;
            var getScriptoriumByIdViewModel = await GetScriptoriumById(new string[] { EstateInquiry.ScriptoriumId }, cancellationToken);
            estateInquiryScriptorium = getScriptoriumByIdViewModel.ScriptoriumList.First();

            var getUnitByIdViewModel = await GetUnitById(new string[] { EstateInquiry.UnitId }, cancellationToken);
            estateInquiryUnit = getUnitByIdViewModel.UnitList.First();

            var organizationViewModel = await GetOrganizationByScriptoriumId(new string[] { EstateInquiry.ScriptoriumId }, cancellationToken);
            estateInquiryScriptoriumOrganization = organizationViewModel.OrganizationList.First();

            var organizationViewModel1 = await GetOrganizationByUnitId(new string[] { EstateInquiry.UnitId }, cancellationToken);
            estateInquiryUnitOrganization = organizationViewModel1.OrganizationList.First();

            var geoLocationIdList = new List<int>();
            if (EstateInquiry.GeoLocationId.HasValue)
                geoLocationIdList.Add(EstateInquiry.GeoLocationId.Value);
            if (EstateInquiry.EstateInquiryPeople != null && EstateInquiry.EstateInquiryPeople.Count > 0)
            {
                if (EstateInquiry.EstateInquiryPeople.First().NationalityId.HasValue)
                {
                    geoLocationIdList.Add(EstateInquiry.EstateInquiryPeople.First().NationalityId.Value);
                }
                if (EstateInquiry.EstateInquiryPeople.First().BirthPlaceId.HasValue)
                {
                    geoLocationIdList.Add(EstateInquiry.EstateInquiryPeople.First().BirthPlaceId.Value);
                }
                if (EstateInquiry.EstateInquiryPeople.First().CityId.HasValue)
                {
                    geoLocationIdList.Add(EstateInquiry.EstateInquiryPeople.First().CityId.Value);
                }
                if (EstateInquiry.EstateInquiryPeople.First().IssuePlaceId.HasValue)
                {
                    geoLocationIdList.Add(EstateInquiry.EstateInquiryPeople.First().IssuePlaceId.Value);
                }
            }
            var getGeolocationByIdViewModel = await GetGeoLocationById(geoLocationIdList.ToArray(), cancellationToken);
            estateInquiryGeolocations = getGeolocationByIdViewModel.GeolocationList;
        }
        private async Task<DataTransferReceiveServiceInput> GetEstateInquiryCommands(int commandType, CancellationToken cancellationToken)
        {
            
            var inquiryPerson = EstateInquiry.EstateInquiryPeople.FirstOrDefault();
            DataTransferReceiveServiceInput input = new();
            input.ReceiverCmsorganizationId = estateInquiryUnitOrganization.LegacyId;
            input.SenderCmsorganizationId = estateInquiryScriptoriumOrganization.LegacyId;
            input.ReceiverSubsystemName = 2;
            input.RequestDateTime = _dateTimeService.CurrentPersianDateTime;

            var EstateInquiryMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Inquiry_Mapping", cancellationToken);
            var BstPersonMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Inquiry_BSTPerson_Mapping", cancellationToken);
            int orderNo = 1;
            if (commandType == 1 && !string.IsNullOrWhiteSpace(EstateInquiry.LegacyId))
                commandType = 2;
            EntityData inquiryData = new();
            await FillInquiryData(EstateInquiry, commandType, orderNo++, EstateInquiryMappingInfo, inquiryData, cancellationToken);
            input.EntityList.Add(inquiryData);

            if (commandType == 2 && inquiryPerson != null)
            {
                EntityData deleteRealPerson = new();
                deleteRealPerson.CommandType = 3;
                deleteRealPerson.EntityName = "BSTREALPERSON";
                deleteRealPerson.OrderNo = orderNo++;
                deleteRealPerson.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                deleteRealPerson.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(inquiryPerson.LegacyId) ? inquiryPerson.LegacyId : inquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(deleteRealPerson);

                EntityData deleteLegalPerson = new();
                deleteLegalPerson.CommandType = 3;
                deleteLegalPerson.EntityName = "BSTLEGALPERSON";
                deleteLegalPerson.OrderNo = orderNo++;
                deleteLegalPerson.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                deleteLegalPerson.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(inquiryPerson.LegacyId) ? inquiryPerson.LegacyId : inquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(deleteLegalPerson);

                EntityData deletePerson = new();
                deletePerson.CommandType = 3;
                deletePerson.EntityName = "BSTPERSON";
                deletePerson.OrderNo = orderNo++;
                deletePerson.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                deletePerson.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(inquiryPerson.OtherLegacyId) ? inquiryPerson.OtherLegacyId : inquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(deletePerson);
            }

            if (EstateInquiry.EstateInquiryId != null)
            {
                if (EstateInquiry.IsFollowedInquiryUpdated == EstateConstant.BooleanConstant.True)
                {
                    EntityData followedInquiryData = new();
                    followedInquiryData.CommandType = 2;
                    followedInquiryData.EntityName = "ESTESTATEINQUIRY";
                    followedInquiryData.OrderNo = orderNo++;
                    followedInquiryData.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                    followedInquiryData.Fields.Add(new Field() { Length = 4000, Type = "varchar2", Name = "UNITMESSAGE" });
                    followedInquiryData.Fields.Add(new Field() { Length = 25, Type = "varchar2", Name = "RESPONSERESULT" });

                    followedInquiryData.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(EstateInquiry.EstateInquiryNavigation.LegacyId) ? EstateInquiry.EstateInquiryNavigation.LegacyId : EstateInquiry.EstateInquiryId.Value.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                    followedInquiryData.FieldValues.Add(new FieldValue() { FieldName = "UNITMESSAGE", Value = string.Format("استعلام پیرو به شماره {0} و تاریخ {1} برای این استعلام ثبت شده است", EstateInquiry.InquiryNo, EstateInquiry.InquiryDate).NormalizeTextChars() });
                    followedInquiryData.FieldValues.Add(new FieldValue() { FieldName = "RESPONSERESULT", Value = "False" });
                    input.EntityList.Add(followedInquiryData);
                }

            }
            if (EstateInquiry.PrevFollowedInquiryId != null)
            {
                var prevFollowedInquiry = await _estateTaxInquiryRepository.GetByIdAsync(cancellationToken, EstateInquiry.PrevFollowedInquiryId.Value);
                if (prevFollowedInquiry != null)
                {
                    EntityData prevfollowedInquiryData = new();
                    prevfollowedInquiryData.CommandType = 2;
                    prevfollowedInquiryData.EntityName = "ESTESTATEINQUIRY";
                    prevfollowedInquiryData.OrderNo = orderNo++;
                    prevfollowedInquiryData.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                    prevfollowedInquiryData.Fields.Add(new Field() { Length = 4000, Type = "varchar2", Name = "UNITMESSAGE" });
                    prevfollowedInquiryData.Fields.Add(new Field() { Length = 25, Type = "varchar2", Name = "RESPONSERESULT" });

                    prevfollowedInquiryData.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(prevFollowedInquiry.LegacyId) ? prevFollowedInquiry.LegacyId : prevFollowedInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                    prevfollowedInquiryData.FieldValues.Add(new FieldValue() { FieldName = "UNITMESSAGE", Value = null });
                    prevfollowedInquiryData.FieldValues.Add(new FieldValue() { FieldName = "RESPONSERESULT", Value = "True" });
                    input.EntityList.Add(prevfollowedInquiryData);
                }

            }
            if (inquiryPerson != null)
            {
                EntityData bstPersonData = new();
                FillBstPersonData(inquiryPerson, orderNo++, BstPersonMappingInfo, bstPersonData);
                input.EntityList.Add(bstPersonData);

                if (inquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                {
                    var BstRealPersonMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Inquiry_BSTRealPerson_Mapping", cancellationToken);

                    EntityData bstRealPersonData = new();
                    FillBstRealPersonData(inquiryPerson, orderNo++, BstRealPersonMappingInfo, bstRealPersonData);
                    input.EntityList.Add(bstRealPersonData);
                }
                else
                {
                    var BstLegalPersonMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Inquiry_BSTLegalPerson_Mapping", cancellationToken);

                    EntityData bstLegalPersonData = new();
                    FillBstLegalPersonData(inquiryPerson, orderNo++, BstLegalPersonMappingInfo, bstLegalPersonData);
                    input.EntityList.Add(bstLegalPersonData);
                }
            }
            if (EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.Sended || EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.EditAndReSend || EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedCorrection)
            {
                EntityData data = new();
                data.EntityName = "ESTESTATEINQUIRY";
                data.CommandType = 2;
                data.OrderNo = orderNo;
                data.Fields.Add(new Field() { Length = 19, Type = "varchar2", Name = "SENDTIME" });
                data.Fields.Add(new Field() { Length = 19, Type = "varchar2", Name = "LASTSENDTIME" });
                data.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });

                data.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(EstateInquiry.LegacyId) ? EstateInquiry.LegacyId : EstateInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                data.FieldValues.Add(new FieldValue() { FieldName = "SENDTIME", Value = string.Concat(EstateInquiry.FirstSendDate, " ", EstateInquiry.FirstSendTime.AsSpan(0, 5)) });
                data.FieldValues.Add(new FieldValue() { FieldName = "LASTSENDTIME", Value = string.Concat(EstateInquiry.LastSendDate, " ", EstateInquiry.LastSendTime.AsSpan(0, 5)) });
                input.EntityList.Add(data);
            }
            return input;
        }

        public DataTransferReceiveServiceInput GetDeleteEstateInquiryCommands()
        {
            var inquiryPerson = EstateInquiry.EstateInquiryPeople.FirstOrDefault();
            DataTransferReceiveServiceInput input = new();
            input.ReceiverCmsorganizationId = estateInquiryScriptoriumOrganization.LegacyId;
            input.SenderCmsorganizationId = estateInquiryScriptoriumOrganization.LegacyId;
            input.ReceiverSubsystemName = 2;
            input.RequestDateTime = _dateTimeService.CurrentPersianDateTime;
            int orderNo = 1;
            if (inquiryPerson != null)
            {
                EntityData deleteRealPerson = new();
                deleteRealPerson.CommandType = 3;
                deleteRealPerson.EntityName = "BSTREALPERSON";
                deleteRealPerson.OrderNo = orderNo++;
                deleteRealPerson.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                deleteRealPerson.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(inquiryPerson.LegacyId) ? inquiryPerson.LegacyId : inquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(deleteRealPerson);

                EntityData deleteLegalPerson = new();
                deleteLegalPerson.CommandType = 3;
                deleteLegalPerson.EntityName = "BSTLEGALPERSON";
                deleteLegalPerson.OrderNo = orderNo++;
                deleteLegalPerson.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                deleteLegalPerson.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(inquiryPerson.LegacyId) ? inquiryPerson.LegacyId : inquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(deleteLegalPerson);

                EntityData deletePerson = new();
                deletePerson.CommandType = 3;
                deletePerson.EntityName = "BSTPERSON";
                deletePerson.OrderNo = orderNo++;
                deletePerson.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                deletePerson.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(inquiryPerson.OtherLegacyId) ? inquiryPerson.OtherLegacyId : inquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(deletePerson);
            }
            EntityData deleteInquiry = new();
            deleteInquiry.CommandType = 3;
            deleteInquiry.EntityName = "ESTESTATEINQUIRY";
            deleteInquiry.OrderNo = orderNo;
            deleteInquiry.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
            deleteInquiry.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(EstateInquiry.LegacyId) ? EstateInquiry.LegacyId : EstateInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
            input.EntityList.Add(deleteInquiry);

            return input;
        }        
        public async Task<string> SendEstateInquiryToSabtEstateSystemByService(CancellationToken cancellationToken)
        {

            string str = "";
            int commandType = 0;
            if (EstateInquiry == null) return "استعلام مرتبط مشخص نیست";
            if (EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.Sended)
                commandType = 1;
            else if (EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.EditAndReSend)
                commandType = 2;
            var input = await GetEstateInquiryCommands(commandType, cancellationToken);

            var result = await _mediator.Send(input, cancellationToken);
            if (result.IsSuccess)
            {
                if (!result.Data.Successful)
                {
                    while (EstateInquiryUniqueNoUniqunessIsViolated(result.Data.ErrorMessage))
                    {
                        var fv = input.EntityList.Where(x => x.EntityName == "ESTESTATEINQUIRY").First().FieldValues.Where(x => x.FieldName == "UniqueNo").First();
                        fv.Value = (Convert.ToInt64(fv.Value) + 1).ToString();
                        result = await _mediator.Send(input, cancellationToken);
                        if (result.IsSuccess && result.Data.Successful)
                            break;
                    }
                    if (result.IsSuccess)
                    {
                        if (!result.Data.Successful)
                            str = "خطا در همگام سازی اقلام اطلاعاتی استعلام با سرویس دهنده سازمان ثبت :" + result.Data.ErrorMessage;
                    }
                    else
                        str = "خطا در همگام سازی اقلام اطلاعاتی استعلام با سرویس دهنده سازمان ثبت :" + result.message.FirstOrDefault();

                }
            }
            else
                str = "خطا در همگام سازی اقلام اطلاعاتی استعلام با سرویس دهنده سازمان ثبت :" + result.message.FirstOrDefault();


            return str;
        }
        public static bool EstateInquiryUniqueNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIX_UNO"))
                return true;
            return false;
        }
        public static bool EstateTaxInquiryUniqueNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIXTIUNIQUENO"))
                return true;
            return false;
        }
        public static bool EstateTaxInquiryNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIXNO"))
                return true;
            return false;
        }

        private async Task FillInquiryData(Domain.Entities.EstateInquiry masterEntity, int commandType, int orderNo, string estateInquiryMappingInfo, EntityData inquiryData, CancellationToken cancellationToken)
        {
            Type estateInquiryType = masterEntity.GetType();
            inquiryData.EntityName = "ESTESTATEINQUIRY";
            inquiryData.CommandType = commandType;
            inquiryData.OrderNo = orderNo;
            var regex = new System.Text.RegularExpressions.Regex(@"\s");
            estateInquiryMappingInfo = regex.Replace(estateInquiryMappingInfo, "");
            string[] mappings = estateInquiryMappingInfo.Split(';');
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsInquirySpecialField(mappingDetail[0]))
                {
                    var propertyInfo = estateInquiryType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(masterEntity, null);
                    inquiryData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                else
                {
                    var propertyInfo = estateInquiryType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(masterEntity, null);
                    propertyValue = await GetInquirySpecialFieldValue(mappingDetail[0], mappingDetail[1], propertyValue, cancellationToken);
                    inquiryData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }

                inquiryData.Fields.Add(new Field() { Name = mappingDetail[1], Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2] });
            }
            var enquiryNo = inquiryData.FieldValues.Where(f => f.FieldName == "EnquiryNo").First();
            enquiryNo.Value = Convert.ToInt32(enquiryNo.Value);

            if (!string.IsNullOrWhiteSpace(EstateInquiry.Response))
            {

                if (EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedCorrection || EstateInquiry.WorkflowStatesId == EstateConstant.EstateInquiryStates.NeedDocument)
                {
                    inquiryData.FieldValues.Where(x => x.FieldName == "UnitMessage").First().Value = EstateInquiry.Response;
                }
                else
                {
                    inquiryData.FieldValues.Where(x => x.FieldName == "Response").First().Value = EstateInquiry.Response;
                }
            }

            if (masterEntity.EstateInquiryId != null)
            {

                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "FollowerInquiryDate", Value = masterEntity.EstateInquiryNavigation.InquiryDate });
                inquiryData.Fields.Add(new Field() { Name = "FollowerInquiryDate", Length = 20, Type = "Varchar2" });

                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "FolloweInquiryNo", Value = masterEntity.EstateInquiryNavigation.InquiryNo });
                inquiryData.Fields.Add(new Field() { Name = "FolloweInquiryNo", Length = 16, Type = "Varchar2" });

                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "HasFollowerInquiry", Value = 1 });
                inquiryData.Fields.Add(new Field() { Name = "HasFollowerInquiry", Length = 1, Type = "Number" });
                if (masterEntity.IsFollowedInquiryUpdated == EstateConstant.BooleanConstant.True)
                {
                    inquiryData.FieldValues.Add(new FieldValue() { FieldName = "FOLLOWINGINQUIRYID", Value = !string.IsNullOrWhiteSpace(masterEntity.EstateInquiryNavigation.LegacyId) ? masterEntity.EstateInquiryNavigation.LegacyId : masterEntity.EstateInquiryId.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                    inquiryData.Fields.Add(new Field() { Name = "FOLLOWINGINQUIRYID", Length = 32, Type = "Varchar2" });
                }

            }
            else
            {
                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "FollowerInquiryDate", Value = null });
                inquiryData.Fields.Add(new Field() { Name = "FollowerInquiryDate", Length = 20, Type = "Varchar2" });

                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "FolloweInquiryNo", Value = null });
                inquiryData.Fields.Add(new Field() { Name = "FolloweInquiryNo", Length = 16, Type = "Varchar2" });

                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "HasFollowerInquiry", Value = null });
                inquiryData.Fields.Add(new Field() { Name = "HasFollowerInquiry", Length = 1, Type = "Number" });

                inquiryData.FieldValues.Add(new FieldValue() { FieldName = "FOLLOWINGINQUIRYID", Value = null });
                inquiryData.Fields.Add(new Field() { Name = "FOLLOWINGINQUIRYID", Length = 32, Type = "Varchar2" });

            }

            inquiryData.FieldValues.Add(new FieldValue() { FieldName = "SendFromNewNotary", Value = 1 });
            inquiryData.Fields.Add(new Field() { Name = "SendFromNewNotary", Length = 1, Type = "Number" });

            var timestamp = inquiryData.FieldValues.Where(x => x.FieldName.Equals("timestamp", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (timestamp != null)
                timestamp.Value = Convert.ToInt32(timestamp.Value);
        }

        private async Task<object> GetInquirySpecialFieldValue(string fieldName, string mappingFieldName, object fieldValue, CancellationToken cancellationToken)
        {
            if (fieldValue == null) return null;
            if (fieldName == "EstateSectionId")
            {
                var section = await _estateSectionRepository.GetByIdAsync(cancellationToken, fieldValue);
                return section.LegacyId;
            }
            else if (fieldName == "EstateSubSectionId")
            {
                var section = await _estateSubSectionRepository.GetByIdAsync(cancellationToken, fieldValue);
                return section.LegacyId;
            }
            else if (fieldName == "EstateSeridaftarId")
            {
                var section = await _estateSeridaftarRepository.GetByIdAsync(cancellationToken, fieldValue);
                return section.LegacyId;
            }
            else if (fieldName == "GeoLocationId")
            {
                var geoLocation = estateInquiryGeolocations.Where(x => x.Id == fieldValue.ToString()).First();
                return geoLocation.LegacyId;
            }
            else if (fieldName == "ScriptoriumId" && mappingFieldName == "ProducerInquiryId")
            {
                return estateInquiryScriptoriumOrganization.LegacyId;
            }
            else if (fieldName == "ScriptoriumId" && mappingFieldName == "ScriptoriumId")
            {
                return estateInquiryScriptorium.LegacyId;
            }
            else if (fieldName == "WorkflowStatesId")
            {
                if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.NeedCorrection)
                    return "8";
                else if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.Archived)
                    return "4";
                else if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.ConfirmResponse)
                    return "3";
                else if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.EditAndReSend)
                    return "6";
                if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.NeedDocument)
                    return "5";
                else if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.NotSended)
                    return null;
                else if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.RejectResponse)
                    return "3";
                else if (fieldValue.ToString() == EstateConstant.EstateInquiryStates.Sended)
                    return "1";
            }
            else if (fieldName == "UnitId" && mappingFieldName == "SelfInquiryId")
            {
                return estateInquiryUnitOrganization.LegacyId;
            }
            else if (fieldName == "UnitId" && mappingFieldName == "UnitId")
            {
                return estateInquiryUnit.LegacyId;
            }
            else if (fieldName == "SecondaryRemaining")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "BasicRemaining")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "DocumentIsNote")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "DocumentIsReplica")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "Id")
            {
                if (EstateInquiry != null)
                    return !string.IsNullOrWhiteSpace(EstateInquiry.LegacyId) ? EstateInquiry.LegacyId : EstateInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
              
            }
            else if (fieldName == "Ilm")
            {
                return Convert.ToInt32(fieldValue);
            }
            else if (fieldName == "Response")
                return null;
            else if (fieldName == "LastSendDate")
            {
                if (EstateInquiry != null)
                    return string.Concat(fieldValue.ToString(), " ", EstateInquiry.LastSendTime.AsSpan(0, 5));
                
            }
            else if (fieldName == "FirstSendDate")
            {
                if (EstateInquiry != null)
                    return  string.Concat(fieldValue.ToString(), " ", EstateInquiry.FirstSendTime.AsSpan(0, 5));
                
            }
            else if (fieldName == "CreateDate")
            {
                if (EstateInquiry != null)
                    return string.Concat(fieldValue.ToString(), " ", EstateInquiry.CreateTime.AsSpan(0, 5));
                
            }
            else if (fieldName == "TrtsReadDate")
            {
                if (EstateInquiry != null)
                    return string.Concat(fieldValue.ToString(), " ", EstateInquiry.TrtsReadTime.AsSpan(0, 5));
               
            }
            else if (fieldName == "ResponseDate")
            {
                if (EstateInquiry != null)
                    return string.Concat(fieldValue.ToString(), " ", EstateInquiry.ResponseTime.AsSpan(0, 5));
               
            }

            return null;
        }

        private static bool IsInquirySpecialField(string fieldName)
        {
            string[] sa = new string[]
                {
                    "EstateSectionId",
                    "EstateSubSectionId",
                    "EstateSeridaftarId",
                    "GeoLocationId",
                    "ScriptoriumId",
                    "WorkflowStatesId",
                    "UnitId",
                    "SecondaryRemaining",
                    "BasicRemaining",
                    "DocumentIsNote",
                    "DocumentIsReplica",
                    "Id",
                    "Ilm",
                    "Response",
                    "LastSendDate",
                    "FirstSendDate",
                    "CreateDate",
                    "TrtsReadDate",
                    "ResponseDate"
                };
            if (sa.Contains(fieldName))
                return true;
            return false;
        }

        private void FillBstPersonData(Domain.Entities.EstateInquiryPerson estateInquiryPerson, int orderNo, string bstPersonMappingInfo, EntityData personData)
        {
            Type estateInquiryPersonType = estateInquiryPerson.GetType();
            personData.EntityName = "BSTPERSON";
            personData.CommandType = 1;
            personData.OrderNo = orderNo;
            string[] mappings = bstPersonMappingInfo.Split(';');
            foreach (string mapping in mappings)
            {

                string[] mappingDetail = mapping.Split(',');
                if (personData.Fields.Where(f => f.Name == mappingDetail[1]).Any())
                    continue;
                if (!IsBstPersonSpecialField(mappingDetail[0]))
                {
                    var propertyInfo = estateInquiryPersonType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(estateInquiryPerson, null);
                    personData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                else
                {
                    var propertyInfo = estateInquiryPersonType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(estateInquiryPerson, null);
                    if (mappingDetail[0] != "IsSabtahvalCorrect" && mappingDetail[0] != "IsForeignerssysCorrect" && mappingDetail[0] != "IsIlencCorrect")
                        propertyValue = GetBstPersonSpecialFieldValue(mappingDetail[0], propertyValue);
                    else
                    {
                        propertyValue = null;
                    }
                    personData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                personData.Fields.Add(new Field() { Name = mappingDetail[1], Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2] });
            }
            if (estateInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
            {
                if (estateInquiryPerson.IsSabtahvalCorrect == EstateConstant.BooleanConstant.True || estateInquiryPerson.IsForeignerssysCorrect == EstateConstant.BooleanConstant.True)
                {
                    var fv = personData.FieldValues.Where(x => x.FieldName == "IsVerified").FirstOrDefault();
                    if (fv != null)
                        fv.Value = "true";
                }
                else
                {
                    var fv = personData.FieldValues.Where(x => x.FieldName == "IsVerified").FirstOrDefault();
                    if (fv != null)
                        fv.Value = "false";
                }
            }
            else if (estateInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.LegalPerson)
            {
                if (estateInquiryPerson.IsIlencCorrect == EstateConstant.BooleanConstant.True || estateInquiryPerson.IsForeignerssysCorrect == EstateConstant.BooleanConstant.True)
                {
                    var fv = personData.FieldValues.Where(x => x.FieldName == "IsVerified").FirstOrDefault();
                    if (fv != null)
                        fv.Value = "true";
                }
                else
                {
                    var fv = personData.FieldValues.Where(x => x.FieldName == "IsVerified").FirstOrDefault();
                    if (fv != null)
                        fv.Value = "false";
                }
            }
            var timestamp = personData.FieldValues.Where(x => x.FieldName == "Timestamp").FirstOrDefault();
            if (timestamp != null)
                timestamp.Value = Convert.ToInt32(timestamp.Value);

            personData.Fields.Add(new Field() { Length = 32, Name = "UnitId", Type = "Varchar2" });
            personData.FieldValues.Add(new FieldValue() { FieldName = "UnitId", Value = estateInquiryUnit.LegacyId });
        }

        private object GetBstPersonSpecialFieldValue(string fieldName,  object fieldValue)
        {
            if (fieldValue == null) return null;
            if (fieldName == "NationalityId")
            {
                var geoLocation = estateInquiryGeolocations.Where(x => x.Id == fieldValue.ToString()).First();
                return geoLocation.LegacyId;
            }
            else if (fieldName == "CityId")
            {
                var geoLocation = estateInquiryGeolocations.Where(x => x.Id == fieldValue.ToString()).First();
                return geoLocation.LegacyId;
            }
            else if (fieldName == "ScriptoriumId")
            {
                return estateInquiryScriptorium.LegacyId;
            }
            else if (fieldName == "EstateInquiryId")
            {
                return fieldValue.ToString().ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "IsSabtahvalCorrect")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "IsForeignerssysCorrect")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "IsIlencCorrect")
            {
                if (fieldValue == "1")
                    return "true";
                else
                    return "false";
            }
            else if (fieldName == "Id")
            {
                return fieldValue.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "Ilm")
            {
                return Convert.ToInt32(fieldValue);
            }
            return null;
        }

        private static bool IsBstPersonSpecialField(string fieldName)
        {
            string[] sa = new string[]
                {
                    "NationalityId",
                    "ScriptoriumId",
                    "CityId",
                    "EstateInquiryId",
                    "IsSabtahvalCorrect",
                    "IsForeignerssysCorrect",
                    "IsIlencCorrect",
                    "Ilm",
                    "Id"
                };
            if (sa.Contains(fieldName))
                return true;
            return false;
        }

        private void FillBstRealPersonData(Domain.Entities.EstateInquiryPerson estateInquiryPerson, int orderNo, string bstRealPersonMappingInfo, EntityData realPersonData)
        {
            Type estateInquiryPersonType = estateInquiryPerson.GetType();
            realPersonData.EntityName = "BSTREALPERSON";
            realPersonData.CommandType = 1;
            realPersonData.OrderNo = orderNo;
            string[] mappings = bstRealPersonMappingInfo.Split(';');
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsBstRealPersonSpecialField(mappingDetail[0]))
                {
                    var propertyInfo = estateInquiryPersonType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(estateInquiryPerson, null);
                    realPersonData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                else
                {
                    var propertyInfo = estateInquiryPersonType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(estateInquiryPerson, null);
                    propertyValue = GetBstRealPersonSpecialFieldValue(mappingDetail[0], propertyValue);
                    realPersonData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                realPersonData.Fields.Add(new Field() { Name = mappingDetail[1], Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2] });
            }
            var sexValue = realPersonData.FieldValues.Where(f => f.FieldName == "Sex").First();
            sexValue.Value = Convert.ToDecimal(sexValue.Value) - 1;

            realPersonData.Fields.Add(new Field() { Length = 32, Name = "BSTPersonId", Type = "Varchar2" });
            realPersonData.FieldValues.Add(new FieldValue() { FieldName = "BSTPersonId", Value = estateInquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });

            realPersonData.Fields.Add(new Field() { Length = 32, Name = "UnitId", Type = "Varchar2" });
            realPersonData.FieldValues.Add(new FieldValue() { FieldName = "UnitId", Value = estateInquiryUnit.LegacyId });

            var timestamp = realPersonData.FieldValues.Where(x => x.FieldName.Equals("timestamp", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (timestamp != null)
                timestamp.Value = Convert.ToInt32(timestamp.Value);
        }

        private object GetBstRealPersonSpecialFieldValue(string fieldName,  object fieldValue)
        {
            if (fieldValue == null) return null;
            if (fieldName == "IssuePlaceId")
            {
                var geoLocation = estateInquiryGeolocations.Where(x => x.Id == fieldValue.ToString()).First();
                return geoLocation.LegacyId;
            }
            else if (fieldName == "IdentityNo")
            {
                return Convert.ToInt64(fieldValue);
            }
            else if (fieldName == "ScriptoriumId")
            {
                return estateInquiryScriptorium.LegacyId;
            }
            else if (fieldName == "Id")
            {
                return fieldValue.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "Ilm")
            {
                return Convert.ToInt32(fieldValue);
            }
            else if (fieldName == "SerialNo")
            {
                return Convert.ToInt32(fieldValue);
            }
            else if (fieldName == "SexType")
            {
                return Convert.ToInt32(fieldValue);

            }
            if (fieldName == "ExecutiveTransfer")
            {
                if (fieldValue.ToString() == EstateConstant.BooleanConstant.True)
                    return 1;
                else
                    return 0;
            }
            return null;
        }


        private static bool IsBstRealPersonSpecialField(string fieldName)
        {
            string[] sa = new string[]
                {
                    "IdentityNo",
                    "ScriptoriumId",
                    "SerialNo",
                    "SexType",
                    "ExecutiveTransfer",
                    "IssuePlaceId",
                    "Ilm",
                    "Id"
                };
            if (sa.Contains(fieldName))
                return true;
            return false;
        }


        private void FillBstLegalPersonData(Domain.Entities.EstateInquiryPerson estateInquiryPerson, int orderNo, string bstLegalPersonMappingInfo, EntityData legalPersonData)
        {
            Type estateInquiryPersonType = estateInquiryPerson.GetType();
            legalPersonData.EntityName = "BSTLEGALPERSON";
            legalPersonData.CommandType = 1;
            legalPersonData.OrderNo = orderNo;
            string[] mappings = bstLegalPersonMappingInfo.Split(';');
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsBstLegalPersonSpecialField(mappingDetail[0]))
                {
                    var propertyInfo = estateInquiryPersonType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(estateInquiryPerson, null);
                    legalPersonData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                else
                {
                    var propertyInfo = estateInquiryPersonType.GetProperty(mappingDetail[0]);
                    var propertyValue = propertyInfo.GetValue(estateInquiryPerson, null);
                    propertyValue = GetBstLegalPersonSpecialFieldValue(mappingDetail[0], propertyValue);
                    legalPersonData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1], Value = propertyValue });
                }
                legalPersonData.Fields.Add(new Field() { Name = mappingDetail[1], Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2] });
            }

            legalPersonData.Fields.Add(new Field() { Length = 32, Name = "BSTPersonId", Type = "Varchar2" });
            legalPersonData.FieldValues.Add(new FieldValue() { FieldName = "BSTPersonId", Value = estateInquiryPerson.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });

            legalPersonData.Fields.Add(new Field() { Length = 32, Name = "UnitId", Type = "Varchar2" });
            legalPersonData.FieldValues.Add(new FieldValue() { FieldName = "UnitId", Value = estateInquiryUnit.LegacyId });

            var timestamp = legalPersonData.FieldValues.Where(x => x.FieldName.Equals("timestamp", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (timestamp != null)
                timestamp.Value = Convert.ToInt32(timestamp.Value);
        }

        private object GetBstLegalPersonSpecialFieldValue(string fieldName, object fieldValue)
        {
            if (fieldValue == null) return null;

            else if (fieldName == "ScriptoriumId")
            {
                return estateInquiryScriptorium.LegacyId;
            }
            else if (fieldName == "Id")
            {
                return fieldValue.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "Ilm")
            {
                return Convert.ToInt32(fieldValue);
            }
            else
            if (fieldName == "ExecutiveTransfer")
            {
                if (fieldValue.ToString() == EstateConstant.BooleanConstant.True)
                    return 1;
                else
                    return 0;
            }
            return null;
        }


        private static bool IsBstLegalPersonSpecialField(string fieldName)
        {
            string[] sa = new string[]
                {

                    "ScriptoriumId",
                    "ExecutiveTransfer",
                    "Ilm",
                    "Id"
                };
            if (sa.Contains(fieldName))
                return true;
            return false;
        }


        private static string GetSeriAlphaByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return "";
            if (code == "1") return "الف";
            else
                    if (code == "2") return "ب";
            else
                if (code == "3") return "پ";
            else
                    if (code == "4") return "ت";
            else
                    if (code == "5") return "ث";
            else
                    if (code == "6") return "ج";
            else
                    if (code == "7") return "چ";
            else
                    if (code == "9") return "خ";
            else
                    if (code == "10") return "د";
            else
                    if (code == "11") return "ذ";
            else
                    if (code == "12") return "ر";
            else
                    if (code == "13") return "ز";
            else
                    if (code == "15") return "س";
            else
                    if (code == "17") return "ص";
            else
                    if (code == "18") return "ض";
            else
                    if (code == "19") return "ط";
            else
                    if (code == "20") return "ظ";
            else
                    if (code == "21") return "ع";
            else
                    if (code == "22") return "غ";
            else
                    if (code == "23") return "ف";
            else
                    if (code == "24") return "ق";
            else
                    if (code == "25") return "ك";
            else
                    if (code == "26") return "گ";
            else
                    if (code == "27") return "ل";
            else
                    if (code == "28") return "م";
            else
                    if (code == "29") return "ن";
            else
                    if (code == "30") return "و";
            else
                    if (code == "31") return "ه";
            else
                    if (code == "32") return "ي";
            return "";
        }

        string externalServiceBaseUrl = "";        
        public async Task<GetUnitByIdViewModel> GetUnitById(string[] unitId, CancellationToken cancellationToken)
        {
            var result = new GetUnitByIdViewModel();
            

            if (unitId == null) return result;
            if (unitId.Length == 0) return result;
            var response = await _mediator.Send(new GetUnitByIdQuery(unitId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }

        public async Task<GetScriptoriumByIdViewModel> GetScriptoriumById(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var result = new GetScriptoriumByIdViewModel();
            

            if (scriptoriumId == null) return result;
            if (scriptoriumId.Length == 0) return result;
            var response = await _mediator.Send(new GetScriptoriumByIdQuery(scriptoriumId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }

        public async Task<OrganizationViewModel> GetOrganizationByScriptoriumId(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var result = new OrganizationViewModel();            

            if (scriptoriumId == null) return result;
            if (scriptoriumId.Length == 0) return result;
            var response = await _mediator.Send(new GetOrganizationByScriptoriumIdQuery(scriptoriumId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }

        public async Task<OrganizationViewModel> GetOrganizationByUnitId(string[] unitId, CancellationToken cancellationToken)
        {
            var result = new OrganizationViewModel();
            
            if (unitId == null) return result;
            if (unitId.Length == 0) return result;
            var response = await _mediator.Send(new GetOrganizationByUnitIdQuery(unitId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<GetGeolocationByIdViewModel> GetGeoLocationById(int[] geoLocationId, CancellationToken cancellationToken)
        {
            var result = new GetGeolocationByIdViewModel();            

            if (geoLocationId == null)  return result;   
            if(geoLocationId.Length == 0) return result; 
            var response = await _mediator.Send(new GetGeolocationByIdQuery(geoLocationId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }

        OrganizationData dealSummaryScriptoriumOrganization = null;
        OrganizationData dealSummaryUnitOrganization = null;
        public async Task GetDealSummaryBaseInfoData(CancellationToken cancellationToken)
        {
            if (DealSummary != null && DealSummary.EstateInquiry != null)
            {
                var organizationViewModel = await GetOrganizationByScriptoriumId(new string[] { DealSummary.EstateInquiry.ScriptoriumId }, cancellationToken);
                dealSummaryScriptoriumOrganization = organizationViewModel.OrganizationList.First();

                var organizationViewModel1 = await GetOrganizationByUnitId(new string[] { DealSummary.EstateInquiry.UnitId }, cancellationToken);
                dealSummaryUnitOrganization = organizationViewModel1.OrganizationList.First();
            }

        }
        public async Task<string> SendDealSummaryToSabtEstateSystemByService(CancellationToken cancellationToken)
        {
            if (DealSummary == null) return "خلاصه معامله مرتبط مشخص نیست";
            DataTransferReceiveServiceInput input = new();
            input.ReceiverCmsorganizationId = dealSummaryUnitOrganization.LegacyId;
            input.SenderCmsorganizationId = dealSummaryScriptoriumOrganization.LegacyId;
            input.ReceiverSubsystemName = 2;
            input.RequestDateTime = _dateTimeService.CurrentPersianDateTime;


            input.EntityList.Add(new EntityData()
            {
                CommandType = 2,
                EntityName = "DSUDEALSUMMARY",
                Fields = new List<Field>() {
                       new Field() {  Length=10, Name= "RemoveRestrictionDate", Type="Varchar2"},
                       new Field() { Length=20, Name= "RemoveRestrictionNo", Type="Varchar2"},
                       new Field() { Length=32, Name= "DSURemoveRestirctionTypeId", Type="Varchar2"},
                       new Field() { Length=32, Name= "unrestrictedOrganizationId", Type="Varchar2"},
                       new Field() { Length=32, Name= "id", Type="Varchar2"},
                       new Field() { Length=16, Name= "sendingstatus", Type="Number"},
                       new Field() { Length=1, Name= "SendFromNewNotary", Type="Number"}
                   },

                FieldValues = new List<FieldValue>()
                {
                    new FieldValue(){FieldName="RemoveRestrictionDate", Value=DealSummary.RemoveRestrictionDate},
                    new FieldValue(){FieldName="RemoveRestrictionNo", Value=DealSummary.RemoveRestrictionNo},
                    new FieldValue(){FieldName="DSURemoveRestirctionTypeId", Value= DealSummary.UnrestrictionType!=null ? DealSummary.UnrestrictionType.LegacyId:null},
                    new FieldValue(){FieldName="unrestrictedOrganizationId", Value=DealSummary.UnrestrictionType!=null ? dealSummaryScriptoriumOrganization.LegacyId:null},
                    new FieldValue(){FieldName="sendingstatus", Value=DealSummary.UnrestrictionType!=null ? 7:3 },
                    new FieldValue(){FieldName="id", Value=!string.IsNullOrWhiteSpace(DealSummary.LegacyId) ? DealSummary.LegacyId : DealSummary.Id.ToString().Replace("-","").Replace("_","").ToUpper()},
                    new FieldValue(){FieldName="SendFromNewNotary", Value=1 }
                },
                OrderNo = 1
            });

            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsSuccess)
            {
                if (result.Data.Successful)
                    return "";
                else
                    return "خطا در همگام سازی اقلام اطلاعاتی خلاصه معامله با سرور سازمان ثبت :" + result.Data.ErrorMessage;
            }
            else
                return "خطا در همگام سازی اقلام اطلاعاتی خلاصه معامله با سرور سازمان ثبت :" + result.message.FirstOrDefault();



        }

       
        public async Task<string> SendEstateTaxInquiry(string id, string requestType, CancellationToken cancellationToken)
        {
            string errMessage = "";
            //string requestType = "";
            var theTaxAffairs = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(id, cancellationToken);
            this.EstateTaxInquiry = theTaxAffairs;
            this.EstateInquiry = theTaxAffairs.EstateInquiry;
            //string actionTitle = "";
            if (requestType == "02" && theTaxAffairs.WorkflowStates.State == "40")
            {
                //actionTitle = "تمدید گواهی";
            }
            else if (theTaxAffairs.WorkflowStates.State == "33" || theTaxAffairs.WorkflowStates.State == "43")
            {
                requestType = "03";
                //actionTitle = "ویرایش استعلام";
            }
            else if (theTaxAffairs.WorkflowStates.State == "41")
            {
                requestType = "01";
                //actionTitle = "تغییر خریدار";
            }
            else if (theTaxAffairs.WorkflowStates.State == "0")
            {
                requestType = "0";
                //actionTitle = "ارسال";
            }
            else if (theTaxAffairs.WorkflowStates.State == "8")
            {
                requestType = "0";
                //actionTitle = "ارسال مجدد";
            }
            else
            {
                errMessage = "استعلام قبلا ارسال شده است";
            }
            if (!string.IsNullOrWhiteSpace(errMessage))
            {
                return errMessage;
            }
            var isRealSendEnabledForEstateTaxInquiry = await _configurationParameterHelper.IsRealSendEnabledForEstateTaxInquiry(cancellationToken);
            if (!isRealSendEnabledForEstateTaxInquiry)
            {
                #region fake Send
                if (requestType == "02")
                {
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "42", cancellationToken)).Id;
                    theTaxAffairs.Timestamp += 1;
                }
                else if (theTaxAffairs.WorkflowStates.State == "33" || theTaxAffairs.WorkflowStates.State == "43" || theTaxAffairs.WorkflowStates.State == "41")
                {
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == theTaxAffairs.WorkflowStates.State + "0", cancellationToken)).Id;
                    theTaxAffairs.Timestamp += 1;
                }
                else
                {
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "10", cancellationToken)).Id;
                    theTaxAffairs.Timestamp += 1;
                }
                _estateTaxInquiryRepository.Update(theTaxAffairs);
                return "";
                #endregion
            }
            var saobj = new TaxAffairsInquiryObject();
            saobj.Specific_Information = new _Specific_Information();

            saobj.Specific_Information.Sa_Estate_Transfer_Type = theTaxAffairs.EstateTaxInquiryTransferTypeId;
            if (theTaxAffairs.IsFirstCession == EstateConstant.BooleanConstant.True || theTaxAffairs.IsFirstDeal == EstateConstant.BooleanConstant.True)
                saobj.Specific_Information.Sa_Is_It_The_First_Transfer = "1";
            else
                saobj.Specific_Information.Sa_Is_It_The_First_Transfer = "0";
            theTaxAffairs.CessionDate = _dateTimeService.CurrentPersianDate;
            saobj.Specific_Information.Sa_Transfer_Date = (theTaxAffairs.CessionDate != null) ? theTaxAffairs.CessionDate.Replace("/", "") : null;
            saobj.Specific_Information.Sa_Previous_Transactions_Based_Facilitate_Law = (theTaxAffairs.PrevTransactionsAccordingToFacilitateRule == EstateConstant.BooleanConstant.True) ? "1" : "0";
            saobj.Specific_Information.Sa_Has_Transfered_Estate_More_One_Property_Usage_And_Dose_Not_Seperated_Deed = (theTaxAffairs.IsMissingSeparateDocument == EstateConstant.BooleanConstant.True) ? "1" : "0";
            saobj.Specific_Information.Sa_Assessor_Office = (theTaxAffairs.EstateTaxUnit != null) ? theTaxAffairs.EstateTaxUnit.Code : null;//کد اداره مالیاتی
            saobj.Specific_Information.RequestType = requestType;
            if (requestType != "0")
                saobj.Specific_Information.FollowCode = theTaxAffairs.TrackingCode;
            saobj.Notary_Public_Information = new _Notary_Public_Information();
            saobj.Notary_Public_Information.Type_Of_Request_For_Document_Drafting = theTaxAffairs.EstateTaxInquiryDocumentRequestTypeId;

            var theScriptorium = await GetScriptoriumById(new string[] { theTaxAffairs.ScriptoriumId }, cancellationToken);
            saobj.Notary_Public_Information.Notary_Office_City = theScriptorium.ScriptoriumList.First().GeoLocationName;//theInquiry.TheProducerInquiry.Name.Substring(idex1, (index2 - idex1));
            saobj.Notary_Public_Information.Notary_Office_Enquiry_Date = theTaxAffairs.CreateDate.Replace("/", "");
            saobj.Notary_Public_Information.Notary_Office_Enquiry_Refrence = theTaxAffairs.No;//(IsValidTaxInquiryNo(theTaxAffairs.No,theInquiry.TheProducerInquiry.Code,Rad.CMS.BaseInfoContext.Instance.CurrentForm.Code)) ? theTaxAffairs.No : theInquiry.EnquiryNo.Value.ToString();
            saobj.Notary_Public_Information.Notary_Office_Number = theScriptorium.ScriptoriumList.First().ScriptoriumNo;
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction = new _Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction();
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction.Plate_No = theTaxAffairs.PlateNo;
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction.Postal_Code = theTaxAffairs.EstatePostCode;
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction.Province = theTaxAffairs.FkEstateTaxProvince.Code;
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction.Township = theTaxAffairs.EstateTaxCounty.Code;
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction.Region = theTaxAffairs.EstateTaxCounty.Code + "_" + theTaxAffairs.EstateTaxCity.Code;
            saobj.Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction.Street_Name = theTaxAffairs.Avenue;
            saobj.Ownership_Deed_Specification = new _Ownership_Deed_Specification();
            saobj.Ownership_Deed_Specification.Primary_Registered_No = theTaxAffairs.Estatebasic;
            saobj.Ownership_Deed_Specification.Secondary_Registered_No = theTaxAffairs.Estatesecondary;
            saobj.Ownership_Deed_Specification.Land_Lot_Number = theTaxAffairs.EstateSector;
            saobj.Ownership_Deed_Specification.Registry_District = theTaxAffairs.EstateSection.SsaaCode;
            saobj.Building_Specification = new _Building_Specification();
            saobj.Building_Specification.Contsraction_Certificate_Date = (theTaxAffairs.LicenseDate != null) ? theTaxAffairs.LicenseDate.Replace("/", "") : null;
            saobj.Building_Specification.Issue_Date_Building_Termination_Certificate = (theTaxAffairs.WorkCompletionCertificateDate != null) ? theTaxAffairs.WorkCompletionCertificateDate.Replace("/", "") : null;
            saobj.Building_Specification.Seperated_Minutes_Refrence = theTaxAffairs.SeparationProcessNo;
            saobj.Building_Specification.Ground = (theTaxAffairs.IsGroundLevel == EstateConstant.BooleanConstant.True) ? "1" : "0";
            if (theTaxAffairs.IsGroundLevel == EstateConstant.BooleanConstant.False)
                saobj.Building_Specification.Floor_Number = (theTaxAffairs.FloorNo.HasValue) ? theTaxAffairs.FloorNo.Value.ToString() : null;
            else
                saobj.Building_Specification.Floor_Number = null;
            saobj.Building_Specification.Age_Buidling = (theTaxAffairs.BuildingOld.HasValue) ? theTaxAffairs.BuildingOld.Value.ToString() : null;
            decimal pas = 0;
            decimal was = 0;
            foreach (var att in theTaxAffairs.EstateTaxInquiryAttaches)
            {
                if (att.ChangeState == "3") continue;
                if (att.EstatePieceType.Code == "1")//parking
                {
                    saobj.Building_Specification.Parking_No += att.Piece + ",";
                    pas += att.Area;
                }
                else if (att.EstatePieceType.Code == "2")//warehouse
                {
                    saobj.Building_Specification.Warehouse_No += att.Piece + ",";
                    was += att.Area;
                }
            }
            if (!string.IsNullOrEmpty(saobj.Building_Specification.Parking_No))
            {
                saobj.Building_Specification.Parking_No = saobj.Building_Specification.Parking_No.Remove(saobj.Building_Specification.Parking_No.Length - 1, 1);
            }
            if (!string.IsNullOrEmpty(saobj.Building_Specification.Warehouse_No))
            {
                saobj.Building_Specification.Warehouse_No = saobj.Building_Specification.Warehouse_No.Remove(saobj.Building_Specification.Warehouse_No.Length - 1, 1);
            }
            saobj.Specification_Of_Property_Sale_Contract_Transfer_Right = new _Specification_Of_Property_Sale_Contract_Transfer_Right();
            saobj.Specification_Of_Property_Sale_Contract_Transfer_Right.Value_Of_The_Property_To_Be_Transferred_According_To_The_Sales_Contract_Or_Contract = theTaxAffairs.CessionPrice.Value.ToString();

            saobj.Calculation_Of_Transfer_Share_Coefficient = new _Calculation_Of_Transfer_Share_Coefficient();
            saobj.Calculation_Of_Transfer_Share_Coefficient.Share_From_Ownership = theTaxAffairs.ShareOfOwnership.Value.ToString();
            saobj.Calculation_Of_Transfer_Share_Coefficient.Share_From_The_Whole_Ownership = theTaxAffairs.TotalOwnershipShare.Value.ToString();
            saobj.Calculation_Of_Transfer_Share_Coefficient.Transfered_Share_Amount_Per_Part_From_6_Parts = theTaxAffairs.TransitionShare.Value.ToString();

            saobj.Calculation_Of_Building_Share_Coefficient = new _Calculation_Of_Building_Share_Coefficient();
            saobj.Calculation_Of_Building_Share_Coefficient.Applicable_Area_Building = (theTaxAffairs.ApartmentArea.HasValue) ? theTaxAffairs.ApartmentArea.Value.ToString() : null;// theInquiry.Area.Value.ToString();
            saobj.Calculation_Of_Building_Share_Coefficient.Area_Of_The_Parking = (pas != 0) ? pas.ToString() : null;
            saobj.Calculation_Of_Building_Share_Coefficient.Warehouse_Area = (was != 0) ? was.ToString() : null;
            saobj.Calculation_Of_Building_Share_Coefficient.Total_Applicable_Areas_Whole_Building = (theTaxAffairs.TotalArea.HasValue) ? theTaxAffairs.TotalArea.ToString() : null;//(((theTaxAffairs.ApartmentArea.HasValue) ? theTaxAffairs.ApartmentArea.Value : 0) + pas + was).ToString();

            saobj.Calculation_Of_Land_Transactional_Value = new _Calculation_Of_Land_Transactional_Value();
            saobj.Calculation_Of_Land_Transactional_Value.Block_Number_Based_Transactional_Value_Booklet = (theTaxAffairs.ValuebookletBlockNo.HasValue) ? theTaxAffairs.ValuebookletBlockNo.Value.ToString() : null;
            saobj.Calculation_Of_Land_Transactional_Value.Row_Number_Based_On_Transactional_Value_Booklet = (theTaxAffairs.ValuebookletRowNo.HasValue) ? theTaxAffairs.ValuebookletRowNo.Value.ToString() : null;
            saobj.Calculation_Of_Land_Transactional_Value.Property_Application_Type = (theTaxAffairs.FieldUsingType != null) ? theTaxAffairs.FieldUsingType.Code : null;
            saobj.Calculation_Of_Land_Transactional_Value.Total_Area_Of_The_Land = (theTaxAffairs.ArsehArea.HasValue) ? theTaxAffairs.ArsehArea.Value.ToString() : null;
            saobj.Calculation_Of_Land_Transactional_Value.Private_Passways = (theTaxAffairs.HasSpecialTrance == EstateConstant.BooleanConstant.True) ? "1" : "0";
            saobj.Calculation_Of_Land_Transactional_Value.Land_Specification = (theTaxAffairs.EstateTaxInquiryFieldType != null) ? theTaxAffairs.EstateTaxInquiryFieldType.Code : null;
            saobj.Calculation_Of_Land_Transactional_Value.Land_Passway_Width = theTaxAffairs.TranceWidth.HasValue ? theTaxAffairs.TranceWidth.Value.ToString() : null;
            saobj.Calculation_Of_Land_Transactional_Value.Does_Property_Have_Determined_Transactional_Value = (theTaxAffairs.HasSpecifiedTradingValue == EstateConstant.BooleanConstant.True) ? "1" : "0";

            saobj.Calculation_Of_Building_Transactional_Value = new _Calculation_Of_Building_Transactional_Value();
            saobj.Calculation_Of_Building_Transactional_Value.is_it_the_enfeebled_restrictr = (theTaxAffairs.IsWornTexture == EstateConstant.BooleanConstant.True) ? "1" : "0";
            saobj.Calculation_Of_Building_Transactional_Value.Structure_Type = (theTaxAffairs.EstateTaxInquiryBuildingType != null) ? theTaxAffairs.EstateTaxInquiryBuildingType.Code : null;
            saobj.Calculation_Of_Building_Transactional_Value.Building_Status = (theTaxAffairs.EstateTaxInquiryBuildingStatus != null) ? theTaxAffairs.EstateTaxInquiryBuildingStatus.Code : null;
            saobj.Calculation_Of_Building_Transactional_Value.Construction_Stage = (theTaxAffairs.EstateTaxInquiryBuildingConstructionStep != null) ? theTaxAffairs.EstateTaxInquiryBuildingConstructionStep.Code : null;
            saobj.Calculation_Of_Building_Transactional_Value.Application_Type_Completed_Buildings = (theTaxAffairs.BuildingUsingType != null) ? theTaxAffairs.BuildingUsingType.Code : null;

            saobj.Specifications_Of_Transfer_Right = new _Specifications_Of_Transfer_Right();
            saobj.Specifications_Of_Transfer_Right.Goodwill_Ownership_Type = (theTaxAffairs.LocationAssignRigthOwnershipType != null) ? theTaxAffairs.LocationAssignRigthOwnershipType.Code : null;

            var Owner_Specification = new _Owner_Specification();
            var inquiryPerson = theTaxAffairs.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108" && x.ChangeState != "3").FirstOrDefault();
            if (inquiryPerson != null && inquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
            {
                Owner_Specification.BirthDate = inquiryPerson.BirthDate.Replace("/", "");
                Owner_Specification.Citizenship_Type = inquiryPerson.IsIranian == EstateConstant.BooleanConstant.True ? "01" : "02";
                if (Owner_Specification.Citizenship_Type == "01")
                {
                    Owner_Specification.National_No = inquiryPerson.NationalityCode;
                }
                else
                {
                    Owner_Specification.Foreign_National_Code = inquiryPerson.NationalityCode;
                }
                Owner_Specification.RegisterNumber = inquiryPerson.IdentityNo;
                Owner_Specification.Surname = inquiryPerson.Family;
                Owner_Specification.Landlord_Name = inquiryPerson.Name;
                Owner_Specification.FatherName = inquiryPerson.FatherName;
                saobj.Specific_Information.Mobile = inquiryPerson.MobileNo;
                //nationalityCode = theOwnerRealPerson.MELLICODE;
            }
            else if (inquiryPerson != null && inquiryPerson.PersonType == EstateConstant.PersonTypeConstant.LegalPerson)
            {
                //Owner_Specification.BirthDate = inquiryPerson.BirthDate.Replace("/", "");
                Owner_Specification.Citizenship_Type = inquiryPerson.IsIranian == EstateConstant.BooleanConstant.True ? "01" : "02";
                if (Owner_Specification.Citizenship_Type == "01")
                {
                    Owner_Specification.National_ID_National_Code = inquiryPerson.NationalityCode;
                }
                else
                {
                    Owner_Specification.Foreign_National_Code = inquiryPerson.NationalityCode;
                }
                Owner_Specification.BirthDate = (inquiryPerson.BirthDate != null) ? inquiryPerson.BirthDate.Replace("/", "") : "";
                Owner_Specification.RegisterNumber = (!string.IsNullOrWhiteSpace(inquiryPerson.IdentityNo)) ? inquiryPerson.IdentityNo : "";
                Owner_Specification.Landlord_Name = inquiryPerson.Name;
                saobj.Specific_Information.Mobile = inquiryPerson.MobileNo;

                //nationalityCode = theOwnerLegalPerson.MELLICODE;
            }
            saobj.Owner_Specification_List = new _Owner_Specification[1];
            saobj.Owner_Specification_List[0] = Owner_Specification;
            saobj.Facilities = new _Facilities();
            saobj.Facilities.Goodwill_Application_Type = (theTaxAffairs.LocationAssignRigthUsingType != null) ? theTaxAffairs.LocationAssignRigthUsingType.Code : null;
            saobj.Facilities.Transferred_Goodwill_Value_Based_Contract = (theTaxAffairs.LocationAssignRightDealCurrentValue != null) ? theTaxAffairs.LocationAssignRightDealCurrentValue.Value.ToString() : null;
            saobj.Buyers_Information_List = [];// new _Buyers_Information[theTaxAffairs.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId != "108").Count()];
            //int c = 0;
            //var persons = theTaxAffairs.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId != "108" && x.ChangeState != "3").ToList();
            //foreach (var rp in persons)
            //{
            //    var tp = new _Buyers_Information();
            //    tp.BirthDate = rp.BirthDate.Replace("/", "");
            //    tp.Purchased_Shares = rp.SharePart.Value.ToString();
            //    tp.Total_Share = rp.ShareTotal.Value.ToString();
            //    if (rp.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
            //    {
            //        tp.National_Identifier_National_ID_Code = rp.NationalityCode;
            //    }
            //    else if (rp.PersonType == EstateConstant.PersonTypeConstant.LegalPerson)
            //    {
            //        tp.National_ID = rp.NationalityCode;
            //    }
            //    tp.Name = rp.Name;
            //    tp.FatherName = rp.FatherName;
            //    tp.Surname = rp.Family;
            //    tp.RegisterNumber = rp.IdentityNo;

            //    saobj.Buyers_Information_List[c] = tp;
            //    c++;
            //}
            if (theTaxAffairs.TransitionShare.Value > theTaxAffairs.ShareOfOwnership)
            {

                errMessage = "میزان سهم انتقال نباید بیشتر از سهم از مالکیت مالک باشد";
                return errMessage;

            }
            //decimal df1 = theTaxAffairs.TransitionShare.Value;
            //decimal dt1 = theTaxAffairs.TotalOwnershipShare.Value;
            //RationalNumber rn = new RationalNumber(df1, dt1);
            //List<RationalNumber> RNLst = new List<RationalNumber>();
            
            //    foreach (var irp in theTaxAffairs.EstateTaxInquiryPeople)
            //    {
            //        if (irp.DealsummaryPersonRelateTypeId == "101" || irp.DealsummaryPersonRelateTypeId == "102")
            //        {
            //            RNLst.Add(new RationalNumber() { S = irp.SharePart.Value, M = irp.ShareTotal.Value });
            //        }
            //    }
            //    if (RNLst.Count > 0)
            //    {
            //        RationalNumber trn = RNLst[0];
            //        bool tr = false;
            //        for (int k = 1; k < RNLst.Count; k++)
            //        {
            //            decimal[] da = Helper.Sum(trn.S, RNLst[k].S, trn.M, RNLst[k].M);
            //            trn = new RationalNumber(da[0], da[1]);
            //        }
            //        double d1 = Convert.ToDouble(Convert.ToDecimal(trn.S * rn.M));
            //        double d2 = Convert.ToDouble(Convert.ToDecimal(rn.S * trn.M));
            //        if (d1 <= d2)
            //        {
            //            tr = true;

            //        }
            //        else
            //            tr = false;
            //        if (!tr)
            //        {

            //            errMessage = "سهم خریداری شده خریداران نباید بیشتر از میزان انتقال مالک باشد";
            //            return errMessage;
            //        }
            //    }
            
            saobj.Information_Related_To_Renovation = new _Information_Related_To_Renovation();
            saobj.Information_Related_To_Renovation.Block = !string.IsNullOrWhiteSpace(theTaxAffairs.RenovationRelatedBlockNo) ? theTaxAffairs.RenovationRelatedBlockNo : null;
            saobj.Information_Related_To_Renovation.Real_Estate = !string.IsNullOrWhiteSpace(theTaxAffairs.RenovationRelatedEstateNo) ? theTaxAffairs.RenovationRelatedEstateNo : null;
            saobj.Information_Related_To_Renovation.Row = !string.IsNullOrWhiteSpace(theTaxAffairs.RenovationRelatedRowNo) ? theTaxAffairs.RenovationRelatedRowNo : null;

            var taxAffairsInquiryRequest = new SendEstateTaxInquiryInput();
            taxAffairsInquiryRequest.AmlakObject = saobj;
            taxAffairsInquiryRequest.ServiceID = "473514";
            taxAffairsInquiryRequest.NationalID = inquiryPerson.NationalityCode;
            if (theTaxAffairs.PrevTransactionsAccordingToFacilitateRule == "1")
            {
                taxAffairsInquiryRequest.FacilityLawNumber = theTaxAffairs.FacilityLawNumber;
                taxAffairsInquiryRequest.FacilityLawYear = Convert.ToInt32(theTaxAffairs.FacilityLawYear);
            }
            var result = await _mediator.Send(taxAffairsInquiryRequest, cancellationToken);
            if (result.IsSuccess)
            {
                theTaxAffairs.LastSendDate = _dateTimeService.CurrentPersianDate;
                theTaxAffairs.LastSendTime = _dateTimeService.CurrentTime;
                theTaxAffairs.Timestamp += 1;
                
                if (result.Data.IsDataPassed)
                {
                    Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();

                    if (!string.IsNullOrWhiteSpace(result.Data.FollowCode) && result.Data.FollowCode != theTaxAffairs.TrackingCode)
                        theTaxAffairs.TrackingCode = result.Data.FollowCode;
                    theTaxAffairs.BuildingValue = result.Data.BuildingValue;
                    theTaxAffairs.LandValue = result.Data.LandValue;
                    theTaxAffairs.GoodWillValue = result.Data.GoodWillValue;
                    theTaxAffairs.TaxGoodWillValue = result.Data.TaxGoodWillValue;
                    if (requestType == "02")
                    {
                        theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "42", cancellationToken)).Id;
                    }
                    else if (theTaxAffairs.WorkflowStates.State == "33" || theTaxAffairs.WorkflowStates.State == "43" || theTaxAffairs.WorkflowStates.State == "41")
                    {
                        theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == theTaxAffairs.WorkflowStates.State + "0", cancellationToken)).Id;
                    }
                    else
                    {
                        theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == result.Data.Status.ToString(), cancellationToken)).Id;
                    }
                    if (requestType == "0" || requestType == "03")
                    {

                        var attachments = await LoadEstateTaxInquiryAttachments(theTaxAffairs, cancellationToken);
                        foreach (var media in attachments)
                        {
                           
                            if (!theTaxAffairs.EstateTaxInquiryFiles.Where(x => x.ArchiveMediaFileId == media.MediaId).Any())
                            {
                                theTaxAffairs.EstateTaxInquiryFiles.Add(new EstateTaxInquiryFile()
                                {
                                    Id = Guid.NewGuid(),
                                    AttachmentDate = "-",
                                    AttachmentDesc = "-",
                                    AttachmentNo = "-",
                                    Ilm = "1",
                                    AttachmentTitle = "-",
                                    ArchiveMediaFileId = media.MediaId,
                                    CreateDate = _dateTimeService.CurrentPersianDate,
                                    CreateTime = _dateTimeService.CurrentTime,
                                    EstateTaxInquiryId = theTaxAffairs.Id,
                                    Timestamp = 1,
                                    FileExtention = media.MediaExtension,
                                    ScriptoriumId = theTaxAffairs.ScriptoriumId,
                                    ChangeState = "1"
                                });
                                files.Add(media.MediaId, media.MediaFile);
                            }
                            else
                            {
                                var file = theTaxAffairs.EstateTaxInquiryFiles.Where(x => x.ArchiveMediaFileId == media.MediaId).First();
                                if (string.IsNullOrWhiteSpace(file.SendDate))
                                {
                                    file.AttachmentDate = "-";
                                    file.AttachmentDesc = "-";
                                    file.AttachmentNo = "-";
                                    file.Ilm = "1";
                                    file.AttachmentTitle = "-";
                                    file.ArchiveMediaFileId = media.MediaId;
                                    file.Timestamp = 1;
                                    file.FileExtention = media.MediaExtension;
                                    file.ChangeState = "2";
                                    files.Add(media.MediaId, media.MediaFile);
                                }
                            }
                            
                            
                        }
                        foreach (var file in theTaxAffairs.EstateTaxInquiryFiles)
                        {
                            if (string.IsNullOrWhiteSpace(file.ArchiveMediaFileId))
                                continue;

                            bool exists = false;
                            if (attachments.Where(x => x.MediaId == file.ArchiveMediaFileId).Any())
                                exists = true;
                            
                            if (!exists)
                                file.ChangeState = "3";

                        }
                        foreach (var file in theTaxAffairs.EstateTaxInquiryFiles)
                        {
                            if (file.ChangeState != "3" && string.IsNullOrWhiteSpace(file.SendDate))
                            {
                                await SendEstateTaxInquiryFile(theTaxAffairs, file, files, cancellationToken);
                            }
                        }
                    }
                    await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                    return "";
                }
                else
                {
                    theTaxAffairs.Timestamp += 1;
                    theTaxAffairs.TrackingCode = result.Data.FollowCode;
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "8", cancellationToken)).Id;
                    theTaxAffairs.StatusDescription = "خطا در ارسال استعلام به سازمان امور مالیاتی :" + result.Data.ErrorMessage;
                    await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                    return "خطا در ارسال استعلام به سازمان امور مالیاتی :" + result.Data.ErrorMessage;
                }
            }
            else
            {
                theTaxAffairs.Timestamp += 1;
                theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "8", cancellationToken)).Id;
                theTaxAffairs.StatusDescription = "خطا در ارسال استعلام به سازمان امور مالیاتی :" + (result.message.Count > 0 ? result.message.FirstOrDefault() : "");
                await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                return "خطا در ارسال استعلام به سازمان امور مالیاتی :" + (result.message.Count > 0 ? result.message.FirstOrDefault() : "");
            }


        }

        private async Task SendEstateTaxInquiryFile(Domain.Entities.EstateTaxInquiry inquiry, EstateTaxInquiryFile file, Dictionary<string, byte[]> files, CancellationToken cancellationToken)
        {
            var fileData = files[file.ArchiveMediaFileId];
            if (fileData!=null)
            {
                var TaxAffairInquiryFileRequest = new SendEstateTaxInquiryFileInput()
                {
                    FileByte = fileData,
                    FileComment = "-",
                    FileName = "-",
                    FollowCode = inquiry.TrackingCode,
                    ServiceID = "473514"
                };

                var result = await _mediator.Send(TaxAffairInquiryFileRequest, cancellationToken);
                if (result.IsSuccess)
                {
                    if (result.Data != null)
                    {
                        if (result.Data.FileRefrenceNumber != 0)
                        {
                            file.SendDate = _dateTimeService.CurrentPersianDate;
                            file.SendTime = _dateTimeService.CurrentTime;
                        }
                    }
                }
            }
        }

        public async Task<List<SSAA.BO.DataTransferObject.ServiceInputs.Media.DownloadAttachmentViewModel>> LoadEstateTaxInquiryAttachments(Notary.SSAA.BO.Domain.Entities.EstateTaxInquiry estateTaxInquiry, CancellationToken cancellationToken)
        {
            var result = new List<SSAA.BO.DataTransferObject.ServiceInputs.Media.DownloadAttachmentViewModel>();
            var relatedRecordId = estateTaxInquiry.Id.ToString();

            var attachmentInput = new LoadAttachmentServiceInput
            {
                ClientId = "9007",
                RelatedRecordIds = new List<string> { relatedRecordId }
            };
            var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);
            if (attachmentResult == null || attachmentResult.Data == null || attachmentResult.Data.AttachmentViewModels == null)
                return result;

            foreach (var item in attachmentResult.Data.AttachmentViewModels)
            {
                if (item?.Medias == null || item.DocTypeId != "0910")
                    continue;
                foreach (var foundMedia in item.Medias)
                {
                    var downloadAttachmentInput = new DownloadMediaServiceInput
                    {
                        AttachmentFileId = foundMedia.MediaId,
                        AttachmentClientId = attachmentInput.ClientId,
                        AttachmentRelatedRecordId = estateTaxInquiry.Id.ToString(),
                        AttachmentTypeId = "0910"
                    };
                    var downloadAttachmentOutput = await _mediator.Send(downloadAttachmentInput, cancellationToken);
                    if (downloadAttachmentOutput?.IsSuccess == true && downloadAttachmentOutput.Data != null)
                    {
                        result.Add(downloadAttachmentOutput.Data);
                    }
                   
                }
            }
            return result;
        }

        public async Task<string> SendTransferStopedToTaxAffairs(string inquiryId, CancellationToken cancellationToken)
        {
            string errMessage = "";
            //string requestType = "";
            var theTaxAffairs = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(inquiryId, cancellationToken);
            this.EstateTaxInquiry = theTaxAffairs;
            this.EstateInquiry = theTaxAffairs.EstateInquiry;
            //string actionTitle = "";
            if (theTaxAffairs.WorkflowStates.State == "40")
            {
                //actionTitle = "اعلام عدم انجام معامله و درخواست ابطال گواهی";
            //}
            //else
            //{
                errMessage = "استعلام در وضعیت مجاز برای ،ارسال ابطال گواهی و عدم انجام معامله نمی باشد ";
                return errMessage;
            }
            var isRealSendEnabledForEstateTaxInquiry = await _configurationParameterHelper.IsRealSendEnabledForEstateTaxInquiry(cancellationToken);
            if (!isRealSendEnabledForEstateTaxInquiry)
            {
                #region fake send
                theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "44", cancellationToken)).Id;
                await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                return "";
                #endregion
            }
            var taxAffairsTransferStopedRequest = new RevokeTaxCertificateInput();
            taxAffairsTransferStopedRequest.FollowCode = theTaxAffairs.TrackingCode;
            taxAffairsTransferStopedRequest.ServiceID = "473514";
            taxAffairsTransferStopedRequest.NationalID = theTaxAffairs.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108" && x.ChangeState != "3").First().NationalityCode;

            var result = await _mediator.Send(taxAffairsTransferStopedRequest, cancellationToken);
            if (result.IsSuccess)
            {
                if (result.Data.IsDataPassed)
                {

                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "44", cancellationToken)).Id;
                    theTaxAffairs.Timestamp += 1;
                    await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                    return "";
                }
                else
                {

                    return "خطا در ارسال عدم وضعیت  انجام معامله و درخواست ابطال گواهی به سازمان امور مالیاتی :" + result.Data.ErrorMessage;
                }
            }
            else
            {

                return "خطا در ارسال عدم وضعیت  انجام معامله و درخواست ابطال گواهی به سازمان امور مالیاتی :" + (result.message.Count > 0 ? result.message.FirstOrDefault() : "-");
            }


        }

        public async Task<string> CancelEstateTaxInquiry(string inquiryId, CancellationToken cancellationToken)
        {
            string errMessage = "";

            var theTaxAffairs = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(inquiryId, cancellationToken);
            this.EstateTaxInquiry = theTaxAffairs;
            this.EstateInquiry = theTaxAffairs.EstateInquiry;
            var isRealSendEnabledForEstateTaxInquiry = await _configurationParameterHelper.IsRealSendEnabledForEstateTaxInquiry(cancellationToken);
            if (!isRealSendEnabledForEstateTaxInquiry)
            {
                #region fake send
                theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "9", cancellationToken)).Id;
                theTaxAffairs.Timestamp += 1;
                await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                return "";
                #endregion
            }

            var cancelEstateTaxInquiryRequest = new CancelEstateTaxInquiryInput();
            cancelEstateTaxInquiryRequest.FollowCode = theTaxAffairs.TrackingCode;
            cancelEstateTaxInquiryRequest.ServiceID = "473514";
            cancelEstateTaxInquiryRequest.NationalID = theTaxAffairs.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108" && x.ChangeState != "3").First().NationalityCode;
            var result = await _mediator.Send(cancelEstateTaxInquiryRequest, cancellationToken);
            if (result.IsSuccess)
            {
                if (result.Data.Status == 9)
                {

                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "9", cancellationToken)).Id;
                    theTaxAffairs.Timestamp += 1;
                    await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                    return "";
                }
                else
                {

                    return "در خواست توقف بررسی استعلام با خطا مواجه شده است :" + result.Data.ErrorMessage;
                }
            }
            else
            {

                return "در خواست توقف بررسی استعلام با خطا مواجه شده است :" + (result.message.Count > 0 ? result.message.FirstOrDefault() : "-");
            }


        }

        public async Task<string> GetEstateTaxInquiryStatus(string InquiryId, CancellationToken cancellationToken)
        {
            var theTaxAffairs = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(InquiryId, cancellationToken);
            this.EstateTaxInquiry = theTaxAffairs;
            this.EstateInquiry = theTaxAffairs.EstateInquiry;
            var getEstateTaxInquiryStatusQuery = new GetEstateTaxInquiryStatusInput();
            getEstateTaxInquiryStatusQuery.FollowCode = theTaxAffairs.TrackingCode;
            getEstateTaxInquiryStatusQuery.ServiceID = "473514";
            getEstateTaxInquiryStatusQuery.NationalID = theTaxAffairs.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108" && x.ChangeState != "3").First().NationalityCode;

            var result = await _mediator.Send(getEstateTaxInquiryStatusQuery, cancellationToken);
            if (result.IsSuccess)
            {
                if (result.Data.ErrorID != 0 || !string.IsNullOrWhiteSpace(result.Data.ErrorMessage))
                {
                    return "خطا در دریافت اخرین وضعیت استعلام مالیاتی رخ داد : " + result.Data.ErrorMessage;
                }
                theTaxAffairs.LastReceiveStatusDate = _dateTimeService.CurrentPersianDate;
                theTaxAffairs.LastReceiveStatusTime = _dateTimeService.CurrentTime;
                theTaxAffairs.Timestamp += 1;
                bool bv = false;
                if (result.Data.Status == 30)
                {                    
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "30", cancellationToken)).Id;
                    theTaxAffairs.TaxAmount = result.Data.TaxAmount;
                    theTaxAffairs.TaxBillIdentity = result.Data.SlipID;
                    theTaxAffairs.TaxPaymentIdentity = result.Data.PaymentID;
                    theTaxAffairs.ShebaNo = result.Data.PaymentID;
                    bv = true;
                }
                else if (result.Data.Status == 40)
                {                    
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "40", cancellationToken)).Id;
                    theTaxAffairs.IsLicenceReady = result.Data.IsLicenceReady ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
                    theTaxAffairs.CertificateNo = result.Data.LicenceNumber;
                    theTaxAffairs.CertificateFile = result.Data.LicenceFile;
                    theTaxAffairs.CertificateHtml = result.Data.LicenceHTML;
                    bv = true;
                }
                else if (result.Data.Status == 10)
                {                    
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "10", cancellationToken)).Id;
                    bv = true;
                }
                else if (result.Data.Status == 20)
                {
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "20", cancellationToken)).Id;
                    bv = true;
                }
                else if (result.Data.Status == 9)
                {
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "9", cancellationToken)).Id;
                    bv = true;
                }
                else if (result.Data.Status == 8)
                {
                    theTaxAffairs.WorkflowStatesId = (await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == "8", cancellationToken)).Id;
                    bv = true;
                }
                if (bv)
                    await _estateTaxInquiryRepository.UpdateAsync(theTaxAffairs, cancellationToken);
                return "";
            }
            else
            {

                return "دریافت آخرین وضعیت استعلام با خطا مواجه شده است :" + (result.message.Count > 0 ? result.message.FirstOrDefault() : "-");
            }
        }

        public async Task<string> SendEstateTaxInquiryToSabtTerminalServerByService(CancellationToken cancellationToken)
        {
            string str = "";
            int commandType = 0;
            if (this.EstateTaxInquiry.WorkflowStates.State == "10")
            {
                commandType = 1;
            }
            else
                commandType = 2;
            var input = await GetEstateTaxInquiryCommands(this.EstateTaxInquiry, commandType, cancellationToken);
            var result = await _mediator.Send(input, cancellationToken);
            if (result.IsSuccess)
            {
                if (!result.Data.Successful)
                {
                    do
                    {
                        var fv = input.EntityList.Where(x => x.EntityName == "INQUIRYTAXAFFAIRS").First().FieldValues.Where(x => x.FieldName == "UniqueNo").First();
                        fv.Value = (Convert.ToInt64(fv.Value) + 1).ToString();
                        result = await _mediator.Send(input, cancellationToken);

                    }
                    while (result.IsSuccess && !result.Data.Successful && EstateTaxInquiryUniqueNoUniqunessIsViolated(result.Data.ErrorMessage));

                    while (result.IsSuccess && !result.Data.Successful && EstateTaxInquiryNoUniqunessIsViolated(result.Data.ErrorMessage))
                    {
                        var fv = input.EntityList.Where(x => x.EntityName == "INQUIRYTAXAFFAIRS").First().FieldValues.Where(x => x.FieldName == "No").First();
                        fv.Value = (Convert.ToInt64(fv.Value) + 1).ToString();
                        result = await _mediator.Send(input, cancellationToken);

                    }
                    while (result.IsSuccess && !result.Data.Successful && EstateTaxInquiryUniqueNoUniqunessIsViolated(result.Data.ErrorMessage))
                    {
                        var fv = input.EntityList.Where(x => x.EntityName == "INQUIRYTAXAFFAIRS").First().FieldValues.Where(x => x.FieldName == "UniqueNo").First();
                        fv.Value = (Convert.ToInt64(fv.Value) + 1).ToString();
                        result = await _mediator.Send(input, cancellationToken);
                    }

                }
                if (result.IsSuccess)
                {
                    if (!result.Data.Successful)
                        str = "خطا در همگام سازی اقلام اطلاعاتی استعلام با سرور سازمان ثبت :" + result.Data.ErrorMessage;
                }
                else
                    str = "خطا در همگام سازی اقلام اطلاعاتی استعلام با سرور سازمان ثبت :" + result.message.FirstOrDefault();

            }
            else
                str = "خطا در همگام سازی اقلام اطلاعاتی استعلام با سرور سازمان ثبت :" + result.message.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(str))
            {
                bool flag = false;
                var deletedList = EstateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.ChangeState == "3").ToList();
                foreach (var deleted in deletedList)
                {
                    EstateTaxInquiry.EstateTaxInquiryPeople.Remove(deleted);
                    flag = true;
                }
                var updatedList = EstateTaxInquiry.EstateTaxInquiryPeople.Where(x => !string.IsNullOrWhiteSpace(x.ChangeState)).ToList();
                foreach (var updated in updatedList)
                {
                    updated.ChangeState = null;
                    flag = true;
                }

                var deletedList1 = EstateTaxInquiry.EstateTaxInquiryAttaches.Where(x => x.ChangeState == "3").ToList();
                foreach (var deleted in deletedList1)
                {
                    EstateTaxInquiry.EstateTaxInquiryAttaches.Remove(deleted);
                    flag = true;
                }
                var updatedList1 = EstateTaxInquiry.EstateTaxInquiryAttaches.Where(x => !string.IsNullOrWhiteSpace(x.ChangeState)).ToList();
                foreach (var updated in updatedList1)
                {
                    updated.ChangeState = null;
                    flag = true;
                }

                var deletedList2 = EstateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.ChangeState == "3").ToList();
                foreach (var deleted in deletedList2)
                {
                    EstateTaxInquiry.EstateTaxInquiryFiles.Remove(deleted);
                    flag = true;
                }
                var updatedList2 = EstateTaxInquiry.EstateTaxInquiryFiles.Where(x => !string.IsNullOrWhiteSpace(x.ChangeState)).ToList();
                foreach (var updated in updatedList2)
                {
                    updated.ChangeState = null;
                    flag = true;
                }
                if (flag)
                    await _estateTaxInquiryRepository.UpdateAsync(EstateTaxInquiry, cancellationToken);
            }
            return str;
        }

        private async Task<DataTransferReceiveServiceInput> GetEstateTaxInquiryCommands(Domain.Entities.EstateTaxInquiry estateTaxInquiry, int commandType, CancellationToken cancellationToken)
        {
            //var inquiryPerson = EstateInquiry.EstateInquiryPeople.First();
            //await GetEstateInquiryBaseInfoData(cancellationToken);
            var organizationViewModel = await GetOrganizationByScriptoriumId(new string[] { estateTaxInquiry.ScriptoriumId }, cancellationToken);
            var ScriptoriumOrganization = organizationViewModel.OrganizationList.First();

            DataTransferReceiveServiceInput input = new();
            input.ReceiverCmsorganizationId = ScriptoriumOrganization.LegacyId;
            input.SenderCmsorganizationId = ScriptoriumOrganization.LegacyId;
            input.ReceiverSubsystemName = 2;
            input.RequestDateTime = _dateTimeService.CurrentPersianDateTime;
            int orderNo = 1;
            if (commandType == 1 && !string.IsNullOrWhiteSpace(estateTaxInquiry.LegacyId))
                commandType = 2;

            var personList = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.ChangeState != "1").ToList();
            foreach (var person in personList)
            {
                EntityData personData = new()
                {
                    CommandType = 3,
                    EntityName = "InquiryTaxRelatedPersons".ToUpper(),
                    OrderNo = orderNo++
                };
                personData.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                personData.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(person.LegacyId) ? person.LegacyId : person.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(personData);
            }
            var attachList = estateTaxInquiry.EstateTaxInquiryAttaches.Where(x => x.ChangeState != "1").ToList();
            foreach (var attach in attachList)
            {
                EntityData attachData = new()
                {
                    CommandType = 3,
                    EntityName = "InquiryTaxAffairsAttach".ToUpper(),
                    OrderNo = orderNo++
                };
                attachData.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                attachData.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(attach.LegacyId) ? attach.LegacyId : attach.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(attachData);
            }
            var fileList = estateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.ChangeState != "1").ToList();
            foreach (var file in fileList)
            {
                EntityData fileData = new()
                {
                    CommandType = 3,
                    EntityName = "InquiryTaxAffairsFile".ToUpper(),
                    OrderNo = orderNo++
                };
                fileData.Fields.Add(new Field() { Length = 32, Type = "varchar2", Name = "ID" });
                fileData.FieldValues.Add(new FieldValue() { FieldName = "ID", Value = !string.IsNullOrWhiteSpace(file.LegacyId) ? file.LegacyId : file.Id.ToString().Replace("-", "").Replace("_", "").ToUpper() });
                input.EntityList.Add(fileData);
            }
            var EstateTaxInquiryMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Tax_Inquiry_Mapping", cancellationToken);
            var PersonMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Tax_Inquiry_Person_Mapping", cancellationToken);
            var attachMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Tax_Inquiry_Attach_Mapping", cancellationToken);
            var fileMappingInfo = await _configurationParameterHelper.GetConfigurationParameter("Estate_Tax_Inquiry_File_Mapping", cancellationToken);

            EntityData inquiryData = new();
            FillTaxInquiryData(estateTaxInquiry, commandType, orderNo++, EstateTaxInquiryMappingInfo, inquiryData);
            input.EntityList.Add(inquiryData);

            var persons = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId != "108" && x.ChangeState != "3").ToList();
            foreach (var person in persons)
            {
                EntityData PersonData = new();
                await FillTaxInquiryPersonData(estateTaxInquiry, person, orderNo++, PersonMappingInfo, PersonData, cancellationToken);
                input.EntityList.Add(PersonData);
            }
            var attachList1 = estateTaxInquiry.EstateTaxInquiryAttaches.Where(x => x.ChangeState != "3").ToList();
            foreach (var attach in attachList1)
            {
                EntityData attachData = new();
                FillTaxInquiryAttachData(estateTaxInquiry, attach, orderNo++, attachMappingInfo, attachData);
                input.EntityList.Add(attachData);
            }

            var attachments = await LoadEstateTaxInquiryAttachments(estateTaxInquiry, cancellationToken);
            foreach (var media in attachments)
            {                
                if (!estateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.ArchiveMediaFileId == media.MediaId).Any())
                {
                    var inquiryFile = new EstateTaxInquiryFile()
                    {
                        Id = Guid.NewGuid(),
                        AttachmentDate = "-",
                        AttachmentDesc = "-",
                        AttachmentNo = "-",
                        Ilm = "1",
                        AttachmentTitle = "-",
                        ArchiveMediaFileId = media.MediaId,
                        CreateDate = _dateTimeService.CurrentPersianDate,
                        CreateTime = _dateTimeService.CurrentTime,
                        EstateTaxInquiryId = estateTaxInquiry.Id,
                        Timestamp = 1,
                        FileExtention = media.MediaExtension,
                        ScriptoriumId = estateTaxInquiry.ScriptoriumId,
                        ChangeState = "1"

                    };
                    estateTaxInquiry.EstateTaxInquiryFiles.Add(inquiryFile);
                }
            }
            var fileList1 = estateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.ChangeState != "3").ToList();
            foreach (var file in fileList1)
            {
                EntityData fileData = new();
                FillTaxInquiryFileData(estateTaxInquiry, file, orderNo++, fileMappingInfo, fileData);
                var fileContent = fileData.FieldValues.Where(x => x.FieldName.Equals("FileContent", StringComparison.OrdinalIgnoreCase)).First();
                if (!string.IsNullOrWhiteSpace(file.ArchiveMediaFileId))
                {
                    var result = attachments.Where(x => x.MediaId == file.ArchiveMediaFileId).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.MediaFile != null)
                            fileContent.Value = result.MediaFile;
                    }
                }
                input.EntityList.Add(fileData);
            }
            return input;
        }

        private static void FillTaxInquiryAttachData(Domain.Entities.EstateTaxInquiry estateTaxInquiry, EstateTaxInquiryAttach attach, int orderNo, string attachMappingInfo, EntityData attachData)
        {
            Type estateInquiryType = attach.GetType();
            attachData.EntityName = "InquiryTaxAffairsAttach".ToUpper();
            attachData.CommandType = 1;
            attachData.OrderNo = orderNo;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\s");
            attachMappingInfo = regex.Replace(attachMappingInfo, "");
            string[] mappings = attachMappingInfo.Split(';');
            string[] sa = new string[] { "Boolean", "boolean", "BOOLEAN" };
            string[] sa1 = new string[] { "EnmYesNo", "enmyesno", "ENMYESNO" };
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsTaxInquiryPersonOrAttachSpecialField(mappingDetail[0]))
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, attach, mappingDetail[0]);
                    if (sa.Contains(mappingDetail[2]))
                    {
                        if (propertyValue != null && propertyValue.ToString() == "1")
                            propertyValue = 1;
                        else if (propertyValue != null && propertyValue.ToString() == "2")
                            propertyValue = 0;
                    }
                    else if (mappingDetail[2] == "EnmYesNo")
                    {
                        if (propertyValue != null)
                            propertyValue = Convert.ToInt32(propertyValue);

                    }
                    attachData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }
                else
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, attach, mappingDetail[0]);
                    propertyValue = GetTaxInquiryAttachSpecialFieldValue(attach, mappingDetail[0], propertyValue);
                    attachData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }

                attachData.Fields.Add(new Field() { Name = mappingDetail[1].ToUpper(), Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2].ToLower() });
            }
            var fv = attachData.FieldValues.Where(x => x.FieldName.Equals("InquiryTaxAffairsId", StringComparison.OrdinalIgnoreCase)).First();
            fv.Value = !string.IsNullOrWhiteSpace(estateTaxInquiry.LegacyId) ? estateTaxInquiry.LegacyId : estateTaxInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();

            foreach (var field in attachData.Fields)
            {
                if (sa.Contains(field.Type) || sa1.Contains(field.Type))
                    field.Type = "number";
            }
        }

        private static string GetTaxInquiryAttachSpecialFieldValue(EstateTaxInquiryAttach attach, string fieldName,  object fieldValue)
        {
            if (fieldName == "Id")
            {
                return !string.IsNullOrWhiteSpace(attach.LegacyId) ? attach.LegacyId : attach.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "EstateTaxInquiryId")
            {
                return string.Empty;
            }
            return string.Empty;
        }

        

        private async Task FillTaxInquiryPersonData(Domain.Entities.EstateTaxInquiry estateTaxInquiry, EstateTaxInquiryPerson person, int orderNo, string personMappingInfo, EntityData personData, CancellationToken cancellationToken)
        {
            Type estateInquiryType = person.GetType();
            personData.EntityName = "InquiryTaxRelatedPersons".ToUpper();
            personData.CommandType = 1;
            personData.OrderNo = orderNo;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\s");
            personMappingInfo = regex.Replace(personMappingInfo, "");
            string[] mappings = personMappingInfo.Split(';');
            string[] sa = new string[] { "Boolean", "boolean", "BOOLEAN" };
            string[] sa1 = new string[] { "EnmYesNo", "enmyesno", "ENMYESNO" };
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsTaxInquiryPersonOrAttachSpecialField(mappingDetail[0]))
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, person, mappingDetail[0]);
                    if (sa.Contains(mappingDetail[2]))
                    {
                        if (propertyValue != null && propertyValue.ToString() == "1")
                            propertyValue = 1;
                        else if (propertyValue != null && propertyValue.ToString() == "2")
                            propertyValue = 0;
                    }
                    else if (mappingDetail[2] == "EnmYesNo")
                    {
                        if (propertyValue != null)
                            propertyValue = Convert.ToInt32(propertyValue);

                    }
                    personData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }
                else
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, person, mappingDetail[0]);
                    propertyValue =  GetTaxInquiryPersonSpecialFieldValue(person, mappingDetail[0]);
                    personData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }

                personData.Fields.Add(new Field() { Name = mappingDetail[1].ToUpper(), Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2].ToLower() });
            }
            var fv = personData.FieldValues.Where(x => x.FieldName.Equals("InquiryTaxAffairsId", StringComparison.OrdinalIgnoreCase)).First();
            fv.Value = !string.IsNullOrWhiteSpace(estateTaxInquiry.LegacyId) ? estateTaxInquiry.LegacyId : estateTaxInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();

            fv = personData.FieldValues.Where(x => x.FieldName.Equals("NtionalityId", StringComparison.OrdinalIgnoreCase)).First();
            if (fv.Value != null)
                fv.Value = (await GetGeoLocationById(new int[] { Convert.ToInt32(fv.Value) }, cancellationToken)).GeolocationList.First().LegacyId;

            fv = personData.FieldValues.Where(x => x.FieldName == "PERSONTYPE").First();
            if (fv.Value != null)
                fv.Value = Convert.ToInt16(fv.Value);

            foreach (var field in personData.Fields)
            {
                if (sa.Contains(field.Type) || sa1.Contains(field.Type))
                    field.Type = "number";
            }
        }

        private static void FillTaxInquiryFileData(Domain.Entities.EstateTaxInquiry estateTaxInquiry, EstateTaxInquiryFile file, int orderNo, string fileMappingInfo, EntityData fileData)
        {
            Type estateInquiryType = file.GetType();
            fileData.EntityName = "InquiryTaxAffairsFile".ToUpper();
            fileData.CommandType = 1;
            fileData.OrderNo = orderNo;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\s");
            fileMappingInfo = regex.Replace(fileMappingInfo, "");
            string[] mappings = fileMappingInfo.Split(';');
            string[] sa = new string[] { "Boolean", "boolean", "BOOLEAN" };
            string[] sa1 = new string[] { "EnmYesNo", "enmyesno", "ENMYESNO" };
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsTaxInquiryFileSpecialField(mappingDetail[0]))
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, file, mappingDetail[0]);
                    if (sa.Contains(mappingDetail[2]))
                    {
                        if (propertyValue != null && propertyValue.ToString() == "1")
                            propertyValue = 1;
                        else if (propertyValue != null && propertyValue.ToString() == "2")
                            propertyValue = 0;
                    }
                    else if (mappingDetail[2] == "EnmYesNo")
                    {
                        if (propertyValue != null)
                            propertyValue = Convert.ToInt32(propertyValue);

                    }
                    fileData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }
                else
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, file, mappingDetail[0]);
                    propertyValue = GetTaxInquiryFileSpecialFieldValue(file, mappingDetail[0], propertyValue);
                    fileData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }

                fileData.Fields.Add(new Field() { Name = mappingDetail[1].ToUpper(), Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2].ToLower() });
            }
            var fv = fileData.FieldValues.Where(x => x.FieldName.Equals("InquiryTaxAffairsId", StringComparison.OrdinalIgnoreCase)).First();
            fv.Value = !string.IsNullOrWhiteSpace(estateTaxInquiry.LegacyId) ? estateTaxInquiry.LegacyId : estateTaxInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();

            foreach (var field in fileData.Fields)
            {
                if (sa.Contains(field.Type) || sa1.Contains(field.Type))
                    field.Type = "number";
            }
        }

        private object GetTaxInquiryPersonSpecialFieldValue(EstateTaxInquiryPerson person, string fieldName)
        {
            if (fieldName == "Id")
            {
                return !string.IsNullOrWhiteSpace(person.LegacyId) ? person.LegacyId : person.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "EstateTaxInquiryId")
            {
                return null;
            }
            else if (fieldName == "NationalityId")
            {
                return null;
            }
            return null;
        }

        private static bool IsTaxInquiryPersonOrAttachSpecialField(string fieldName)
        {
            string[] fieldNams = new string[] { "Id", "EstateTaxInquiryId" };
            return fieldNams.Contains(fieldName);
        }

        private static string GetTaxInquiryFileSpecialFieldValue(EstateTaxInquiryFile file, string fieldName,  object fieldValue)
        {
            if (fieldName == "Id")
            {
                return !string.IsNullOrWhiteSpace(file.LegacyId) ? file.LegacyId : file.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            else if (fieldName == "EstateTaxInquiryId")
            {
                return null;
            }
            if (fieldName == "CreateDate")
            {
                return string.Concat(file.CreateDate, "-", file.CreateTime.AsSpan(0, 5));
            }
            else if (fieldName == "SendDate")
            {
                return (!string.IsNullOrWhiteSpace(file.SendDate)) ? string.Concat(file.SendDate, "-", file.SendTime.AsSpan(0, 5)) : "";
            }
            return string.Empty;
        }

        private static bool IsTaxInquiryFileSpecialField(string fieldName)
        {
            string[] fieldNams = new string[] { "Id", "EstateTaxInquiryId", "CreateDate", "SendDate" };
            return fieldNams.Contains(fieldName);
        }

        private static object GetPropertyValue(Type type, object Object, string propertyName)
        {
            if (!propertyName.Contains('.'))
            {
                return type.GetProperty(propertyName).GetValue(Object);
            }
            else
            {
                string[] sa = propertyName.Split('.');
                var tmp = type.GetProperty(sa[0]).GetValue(Object);
                if (tmp != null)
                    return GetPropertyValue(tmp.GetType(), tmp, propertyName.Substring(propertyName.IndexOf('.') + 1));
                return null;
            }
        }
        private static void FillTaxInquiryData(Domain.Entities.EstateTaxInquiry estateTaxInquiry, int commandType, int orderNo, string estateTaxInquiryMappingInfo, EntityData inquiryData)
        {
            Type estateInquiryType = estateTaxInquiry.GetType();
            inquiryData.EntityName = "InquiryTaxAffairs".ToUpper();
            inquiryData.CommandType = commandType;
            inquiryData.OrderNo = orderNo;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\s");
            estateTaxInquiryMappingInfo = regex.Replace(estateTaxInquiryMappingInfo, "");
            string[] mappings = estateTaxInquiryMappingInfo.Split(';');
            string[] sa = new string[] { "Boolean", "boolean", "BOOLEAN" };
            string[] sa1 = new string[] { "EnmYesNo", "enmyesno", "ENMYESNO" };
            foreach (string mapping in mappings)
            {
                string[] mappingDetail = mapping.Split(',');
                if (!IsTaxInquirySpecialField(mappingDetail[0]))
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, estateTaxInquiry, mappingDetail[0]);
                    if (sa.Contains(mappingDetail[2]))
                    {
                        if (propertyValue != null && propertyValue.ToString() == "1")
                        {
                            propertyValue = 1;
                            if (mappingDetail[0] == "IsActive")
                                propertyValue = null;
                        }
                        else if (propertyValue != null && propertyValue.ToString() == "2")
                            propertyValue = 0;

                    }
                    else if (mappingDetail[2] == "EnmYesNo")
                    {
                        if (propertyValue != null)
                            propertyValue = Convert.ToInt32(propertyValue);

                    }
                    inquiryData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }
                else
                {
                    var propertyValue = GetPropertyValue(estateInquiryType, estateTaxInquiry, mappingDetail[0]);
                    propertyValue = GetTaxInquirySpecialFieldValue(estateTaxInquiry, mappingDetail[0], propertyValue);
                    inquiryData.FieldValues.Add(new FieldValue() { FieldName = mappingDetail[1].ToUpper(), Value = propertyValue });
                }

                inquiryData.Fields.Add(new Field() { Name = mappingDetail[1].ToUpper(), Length = mappingDetail.Length > 3 ? Convert.ToInt32(mappingDetail[3]) : 0, Type = mappingDetail[2].ToLower() });
            }
            var fv = inquiryData.FieldValues.Where(x => x.FieldName == "TAXAFFAIRSSTATE").First();
            fv.Value = Convert.ToInt32(fv.Value);

            foreach (var field in inquiryData.Fields)
            {
                if (sa.Contains(field.Type) || sa1.Contains(field.Type))
                    field.Type = "number";
            }
        }

        private static string GetTaxInquirySpecialFieldValue(Domain.Entities.EstateTaxInquiry estateTaxInquiry, string fieldName,  object fieldValue)
        {
            if (fieldName == "EstateInquiryId")
            {
                if (estateTaxInquiry.EstateInquiry != null)
                    return !string.IsNullOrWhiteSpace(estateTaxInquiry.EstateInquiry.LegacyId) ? estateTaxInquiry.EstateInquiry.LegacyId : estateTaxInquiry.EstateInquiryId.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            if (fieldName == "Id")
            {
                return !string.IsNullOrWhiteSpace(estateTaxInquiry.LegacyId) ? estateTaxInquiry.LegacyId : estateTaxInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
            }
            if (fieldName == "CreateDate")
            {
                return string.Concat(estateTaxInquiry.CreateDate, "-", estateTaxInquiry.CreateTime.AsSpan(0, 5));
            }
            if (fieldName == "LastSendDate")
            {
                return !string.IsNullOrWhiteSpace(estateTaxInquiry.LastSendDate) ? string.Concat(estateTaxInquiry.LastSendDate, "-", estateTaxInquiry.LastSendTime.AsSpan(0, 5)) : "";
            }
            if (fieldName == "LastReceiveStatusDate")
            {
                return !string.IsNullOrWhiteSpace(estateTaxInquiry.LastReceiveStatusDate) ? string.Concat(estateTaxInquiry.LastReceiveStatusDate, "-", estateTaxInquiry.LastReceiveStatusTime.AsSpan(0, 5)) : "";
            }
            if (fieldName == "PayCostDate")
            {
                return !string.IsNullOrWhiteSpace(estateTaxInquiry.PayCostDate) ? string.Concat(estateTaxInquiry.PayCostDate, "-", estateTaxInquiry.PayCostTime.AsSpan(0, 5)) : "";
            }

            return null;
        }

        private static bool IsTaxInquirySpecialField(string fieldName)
        {
            string[] fieldNams = new string[] { "EstateInquiryId", "Id", "CreateDate", "LastSendDate", "LastReceiveStatusDate", "PayCostDate" };
            return fieldNams.Contains(fieldName);
        }       
        public async Task<ApiResult<GetEstateSeparationInfoResponse>> GetEstateSeparationInfo(GetEstateSepartionInfoInput request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;

        }        
    }
}
