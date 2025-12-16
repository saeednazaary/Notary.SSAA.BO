using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notary.SSAA.BO.Utilities.Extensions;
using Newtonsoft.Json;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.DigitalSign;
using System.Reflection;
using System.Text;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using System.ComponentModel;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;



namespace Notary.SSAA.BO.QueryHandler.DigitalSign
{
    public class GetDataToSignQueryHandler : BaseQueryHandler<GetDataToSignQuery, ApiResult<GetDataToSignViewModel>>
    {

        private readonly IDigitalSignatureConfigurationRepository _digitalSignatureConfigurationRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IRepository<DocumentEstate> _documentEstateRepository;
        private readonly IRepository<DocumentVehicle> _documentVehicleRepository;
        private readonly IRepository<DocumentCase> _documentCaseRepository;        
        public GetDataToSignQueryHandler(IMediator mediator, IUserService userService,
            IDigitalSignatureConfigurationRepository digitalSignatureConfigurationRepository, IEstateSectionRepository estateSectionRepository, IEstateSubSectionRepository estateSubSectionRepository, IRepository<DocumentEstate> documentEstateRepository, IRepository<DocumentVehicle> documentVehicleRepository, IRepository<DocumentCase> documentCaseRepository, IServiceProvider serviceProvider)
            : base(mediator, userService)
        {

            _digitalSignatureConfigurationRepository = digitalSignatureConfigurationRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _documentCaseRepository = documentCaseRepository;
            _documentVehicleRepository = documentVehicleRepository;
            _documentEstateRepository = documentEstateRepository;
            _serviceProvider = serviceProvider;            
        }
        protected override bool HasAccess(GetDataToSignQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetDataToSignViewModel>> RunAsync(GetDataToSignQuery request, CancellationToken cancellationToken)
        {
            GetDataToSignViewModel result = new();
            ApiResult<GetDataToSignViewModel> apiResult = new();
            DigitalSignatureConfiguration _digitalSignatureConfiguration = null;
            var lst = await _digitalSignatureConfigurationRepository
                                                       .TableNoTracking
                                                       .Include(x => x.DigitalSignaturePropertyMappings)
                                                       .ThenInclude(x => x.DigitalSignaturePropertyMappingDetails)
                                                       .Where(x => x.FormName == request.FormName && x.ConfigName == request.ConfigName)
                                                       .ToListAsync(cancellationToken);
            if (lst != null && lst.Count > 0)
            {
                var versionNo = lst.Max(x => x.VersionNo);
                _digitalSignatureConfiguration = lst.Where(x => x.VersionNo == versionNo).FirstOrDefault();
            }
            apiResult.IsSuccess = true;
            if (_digitalSignatureConfiguration != null)
            {                
                var entity = await GetRelatedEntity(_digitalSignatureConfiguration.RelatedRepository, _digitalSignatureConfiguration.RelatedRepositoryMethod, request.EntityId, cancellationToken);
                if (entity != null)
                {

                    var provider = new Utilities.DigitalSign.FormSignInfoDataGraphProvider();
                    var dataGrophInfo = provider.GetFormSignInfoDataGraph(_digitalSignatureConfiguration.Descriptor);
                    if (dataGrophInfo != null)
                    {
                        var dataGraphSignHelper = new DataGraphSignHelper(this._mediator);
                        dataGraphSignHelper.DigitalSignatureConfiguration = _digitalSignatureConfiguration;
                        dataGraphSignHelper.EstateSectionRepository = _estateSectionRepository;
                        dataGraphSignHelper.EstateSubSectionRepository = _estateSubSectionRepository;
                        dataGraphSignHelper.DocumentCaseRepository = _documentCaseRepository;
                        dataGraphSignHelper.DocumentEstateRepository = _documentEstateRepository;
                        dataGraphSignHelper.DocumentVehicleRepository = _documentVehicleRepository;
                        var dataToSign = await dataGraphSignHelper.GetDataSignText(entity, dataGrophInfo, cancellationToken);

                        List<byte> byteList = new List<byte>();
                        if (_digitalSignatureConfiguration.GetDataAsJson.ToBoolean())
                            byteList.AddRange(Encoding.UTF8.GetBytes(ToJson(dataToSign.Data, _digitalSignatureConfiguration)));
                        else
                            byteList.AddRange(Encoding.UTF8.GetBytes(dataToSign.StringData));
                        byte[] rawDataByte = byteList.ToArray();
                        result.Data = Convert.ToBase64String(rawDataByte);

                        apiResult.Data = result;
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("بازیابی اطلاعات موجودیت مرتبط از بانک اطلاعاتی انجام نشد");
                    }
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("بازیابی اطلاعات موجودیت مرتبط از بانک اطلاعاتی انجام نشد");
                }

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("مشخصات تنظیمات امضای دیجیتال برای این فرم یافت نشد");
            }

            apiResult.statusCode = ApiResultStatusCode.Success;
            return apiResult;
        }

        private async Task<object> GetRelatedEntity(string repository, string repositoryMethod, string entityId, CancellationToken cancellationToken)
        {

            var repostoryType = Type.GetType(repository + ", Notary.SSAA.BO.Infrastructure");
            var applicationContextType = Type.GetType("Notary.SSAA.BO.Infrastructure.Contexts.ApplicationContext, Notary.SSAA.BO.Infrastructure");
            var _repository = ActivatorUtilities.CreateInstance(_serviceProvider, repostoryType, _serviceProvider.GetService(applicationContextType));
            if (_repository != null)
            {
                var method = _repository.GetType().GetMethod(repositoryMethod);
                var parameterType = method.GetParameters()[0].ParameterType;
                if (parameterType == typeof(Guid))
                {
                    var task = (Task)method.Invoke(_repository, new object[] { entityId.ToGuid(), cancellationToken });
                    await task.ConfigureAwait(false);
                    var resultProperty = task.GetType().GetProperty("Result");
                    return resultProperty.GetValue(task);
                }
                else
                {
                    var task = (Task)method.Invoke(_repository, new object[] { entityId, cancellationToken });
                    await task.ConfigureAwait(false);
                    var resultProperty = task.GetType().GetProperty("Result");
                    return resultProperty.GetValue(task);
                }
            }
            return null;
        }

        private static string ToJson(object entity, DigitalSignatureConfiguration digitalSignatureConfiguration)
        {

            Formatting formatting = Formatting.None;
            JsonSerializerSettings jsonSerializerSettings = null;
            if (!string.IsNullOrWhiteSpace(digitalSignatureConfiguration.JsonFormatting))
                formatting = (Formatting)Enum.Parse(typeof(Formatting), digitalSignatureConfiguration.JsonFormatting);
            if (!string.IsNullOrWhiteSpace(digitalSignatureConfiguration.JsonSerializerSettings))
                jsonSerializerSettings = JsonConvert.DeserializeObject<JsonSerializerSettings>(digitalSignatureConfiguration.JsonSerializerSettings);
            if (jsonSerializerSettings != null)
                return JsonConvert.SerializeObject(entity, formatting, jsonSerializerSettings);
            else
                return JsonConvert.SerializeObject(entity, formatting);
        }
    }
    public class DataGraphSignHelper
    {
        IMediator _mediator;

        public IEstateSectionRepository EstateSectionRepository { get; set; }
        public IEstateSubSectionRepository EstateSubSectionRepository { get; set; }
        public IRepository<DocumentEstate> DocumentEstateRepository { get; set; }
        public IRepository<DocumentVehicle> DocumentVehicleRepository { get; set; }
        public IRepository<DocumentCase> DocumentCaseRepository { get; set; }
        private List<Type> EnumTypes { get; set; }
        private BaseInfoServiceHelper _baseInfoServiceHelper;
        public DataGraphSignHelper(IMediator mediator)
        {
            _mediator = mediator;
            EnumTypes= new List<Type>();
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        public DigitalSignatureConfiguration DigitalSignatureConfiguration { get; set; }
        public async Task<object> GetValue(string typeName, string propertyName, object obj, CancellationToken cancellationToken)
        {
            var isForNewSystemEntity = DigitalSignatureConfiguration.IsForNewSystemEntity.ToNullabbleBoolean();
            if (isForNewSystemEntity.HasValue && isForNewSystemEntity.Value)
            {
                return GetValue(obj, propertyName);
            }
            var mappingInfo = DigitalSignatureConfiguration.DigitalSignaturePropertyMappings.Where(x => x.OwnerType == typeName).FirstOrDefault();
            if (mappingInfo != null)
            {
                var mappingInfoDetail = mappingInfo.DigitalSignaturePropertyMappingDetails.Where(x => x.OldPropertyName == propertyName).FirstOrDefault();
                if (mappingInfoDetail != null)
                {
                    if (!string.IsNullOrWhiteSpace(mappingInfoDetail.NewPropertyName))
                    {
                        if (propertyName == "RelatedRegCaseId" && typeName == "Document")
                        {
                            var document = (Notary.SSAA.BO.Domain.Entities.Document)obj;
                            if (document.RelatedRegCaseId != null)
                            {
                                var documentEstate = await DocumentEstateRepository.TableNoTracking.Where(x => x.Id == document.RelatedRegCaseId.Value).FirstOrDefaultAsync(cancellationToken);
                                if (documentEstate != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(documentEstate.LegacyId))
                                        return documentEstate.LegacyId;
                                    return documentEstate.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
                                }
                                else
                                {
                                    var documentVehicle = await DocumentVehicleRepository.TableNoTracking.Where(x => x.Id == document.RelatedRegCaseId.Value).FirstOrDefaultAsync(cancellationToken);
                                    if (documentVehicle != null)
                                    {
                                        if (!string.IsNullOrWhiteSpace(documentVehicle.LegacyId))
                                            return documentVehicle.LegacyId;
                                        return documentVehicle.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
                                    }
                                    else
                                    {
                                        var documentCase = await DocumentCaseRepository.TableNoTracking.Where(x => x.Id == document.RelatedRegCaseId.Value).FirstOrDefaultAsync(cancellationToken);
                                        if (documentCase != null)
                                        {
                                            if (!string.IsNullOrWhiteSpace(documentCase.LegacyId))
                                                return documentCase.LegacyId;
                                            return documentCase.Id.ToString().Replace("-", "").Replace("_", "").ToUpper();
                                        }
                                    }
                                }
                                throw new Exception("خطا در دریافت اقلام اطلاعاتی سند جهت امضا رخ داد");
                            }
                            else return null;
                        }
                        if (propertyName == "AgentTitle" && typeName == "DocumentPersonRelated")
                        {
                            string agentTitle = "";
                            var documentPersonRelated = (DocumentPersonRelated)obj;
                            if (documentPersonRelated.AgentPerson != null && documentPersonRelated.AgentPerson.PersonType == "1")
                            {
                                if (documentPersonRelated.AgentPerson.SexType == "2")
                                    agentTitle = "آقای ";
                                else
                                    agentTitle = "خانم ";
                            }

                            if (documentPersonRelated.AgentPerson != null && documentPersonRelated.AgentType != null)
                                agentTitle += documentPersonRelated.AgentPerson.Name + (!string.IsNullOrWhiteSpace(documentPersonRelated.AgentPerson.Family) ? " " + documentPersonRelated.AgentPerson.Family : "") + " " + documentPersonRelated.AgentType.Adjective + " از طرف ";

                            if (documentPersonRelated.MainPerson != null && documentPersonRelated.MainPerson.PersonType == "1")
                            {
                                if (documentPersonRelated.MainPerson.SexType == "2")
                                    agentTitle += "آقای ";
                                else
                                    agentTitle += "خانم ";
                            }

                            if (documentPersonRelated.MainPerson != null)
                                agentTitle += documentPersonRelated.MainPerson.Name + (!string.IsNullOrWhiteSpace(documentPersonRelated.MainPerson.Family) ? " " + documentPersonRelated.MainPerson.Family : "");

                            if (!string.IsNullOrEmpty(documentPersonRelated.AgentDocumentNo))
                            {
                                if (documentPersonRelated.AgentType != null)
                                    agentTitle += " با " + documentPersonRelated.AgentType.DocumentTitle + " شماره " + documentPersonRelated.AgentDocumentNo;

                                if (!string.IsNullOrEmpty(documentPersonRelated.AgentDocumentDate))
                                    agentTitle += " به تاریخ " + documentPersonRelated.AgentDocumentDate;

                                if (!string.IsNullOrEmpty(documentPersonRelated.AgentDocumentIssuer))
                                    agentTitle += " صادره از " + documentPersonRelated.AgentDocumentIssuer;
                            }

                            return agentTitle;
                        }
                        if (propertyName == "LinkageGroup1Id" && typeName == "DocumentVehicle")
                        {
                            var documentVehicle = (DocumentVehicle)obj;
                            if (!string.IsNullOrWhiteSpace(documentVehicle.MadeInIran))
                            {
                                if (!string.IsNullOrWhiteSpace(documentVehicle.Document.LegacyId))
                                {
                                    if (documentVehicle.MadeInIran == "5")
                                        return "EC6A8C0AA17D4F4AB6010ED6B48CD573";
                                    else if (documentVehicle.MadeInIran == "6")
                                        return "46516D44787344C595900FD5ECCE91BF";
                                    else
                                        throw new Exception("خطا در دریافت اقلام اطلاعاتی سند جهت امضا رخ داد");

                                }
                                else
                                    return documentVehicle.MadeInIran;


                            }
                            else
                                return null;
                        }
                        if(propertyName == "AgentTitle" && typeName == "SignRequestPersonRelated")
                        {
                            string agentTitle = "";
                            var documentPersonRelated = (SignRequestPersonRelated)obj;
                            if (documentPersonRelated.AgentPerson != null && documentPersonRelated.AgentPerson.PersonType == "1")
                            {
                                if (documentPersonRelated.AgentPerson.SexType == "2")
                                    agentTitle = "آقای ";
                                else
                                    agentTitle = "خانم ";
                            }

                            if (documentPersonRelated.AgentPerson != null && documentPersonRelated.AgentType != null)
                                agentTitle += documentPersonRelated.AgentPerson.Name + (!string.IsNullOrWhiteSpace(documentPersonRelated.AgentPerson.Family) ? " " + documentPersonRelated.AgentPerson.Family : "") + " " + documentPersonRelated.AgentType.Adjective + " از طرف ";

                            if (documentPersonRelated.MainPerson != null && documentPersonRelated.MainPerson.PersonType == "1")
                            {
                                if (documentPersonRelated.MainPerson != null && documentPersonRelated.MainPerson.SexType == "2")
                                    agentTitle += "آقای ";
                                else
                                    agentTitle += "خانم ";
                            }

                            if (documentPersonRelated.MainPerson != null)
                                agentTitle += documentPersonRelated.MainPerson.Name + (!string.IsNullOrWhiteSpace(documentPersonRelated.MainPerson.Family) ? " " + documentPersonRelated.MainPerson.Family : "");

                            if (!string.IsNullOrEmpty(documentPersonRelated.AgentDocumentNo))
                            {
                                if (documentPersonRelated.AgentType != null)
                                    agentTitle += " با " + documentPersonRelated.AgentType.DocumentTitle + " شماره " + documentPersonRelated.AgentDocumentNo;

                                if (!string.IsNullOrEmpty(documentPersonRelated.AgentDocumentDate))
                                    agentTitle += " به تاریخ " + documentPersonRelated.AgentDocumentDate;

                                if (!string.IsNullOrEmpty(documentPersonRelated.AgentDocumentIssuer))
                                    agentTitle += " صادره از " + documentPersonRelated.AgentDocumentIssuer;
                            }

                            return agentTitle;
                        }
                        object returnValue = null;
                        if (string.IsNullOrWhiteSpace(mappingInfoDetail.NewPropertyOwnerPath))
                        {
                            returnValue = GetValue(obj, mappingInfoDetail.NewPropertyName);// obj.GetType().GetProperty(mappingInfoDetail.NewPropertyName).GetValue(obj, null);
                        }
                        else if (mappingInfoDetail.NewPropertyOwnerPath.StartsWith("BI:"))
                        {
                            var arr = mappingInfoDetail.NewPropertyOwnerPath.Split(':');
                            var baseInfoTableName = arr[1];
                            var id = GetValue(obj, mappingInfoDetail.NewPropertyName);// obj.GetType().GetProperty(mappingInfoDetail.NewPropertyName).GetValue(obj, null);
                            if (id != null)
                            {
                                if (baseInfoTableName == "Unit")
                                {
                                    var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { id.ToString() }, cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(unit.UnitList[0].LegacyId))
                                        returnValue = unit.UnitList[0].LegacyId;
                                    else
                                        returnValue = unit.UnitList[0].Id;
                                }
                                else if (baseInfoTableName == "Scriptorium")
                                {
                                    var scr = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { id.ToString() }, cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(scr.ScriptoriumList[0].LegacyId))
                                        returnValue = scr.ScriptoriumList[0].LegacyId;
                                    else
                                        returnValue = scr.ScriptoriumList[0].Id;
                                }
                                else if (baseInfoTableName == "Geolocation")
                                {

                                    var geo = await _baseInfoServiceHelper.GetGeoLocationById(new string[] { id.ToString() }, cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(geo.GeolocationList[0].LegacyId))
                                        returnValue = geo.GeolocationList[0].LegacyId;
                                    else
                                        returnValue = Convert.ToInt32(geo.GeolocationList[0].Id);
                                }
                            }

                        }
                        else if (mappingInfoDetail.NewPropertyOwnerPath.StartsWith("TB:"))
                        {
                            var arr = mappingInfoDetail.NewPropertyOwnerPath.Split(':');
                            var TableName = arr[1];
                            var id = GetValue(obj, mappingInfoDetail.NewPropertyName); //obj.GetType().GetProperty(mappingInfoDetail.NewPropertyName).GetValue(obj, null);
                            if (id != null)
                            {
                                if (TableName == "EstateSection")
                                {
                                    var estateSection = await EstateSectionRepository.TableNoTracking.Where(x => x.Id == id.ToString() || x.LegacyId == id.ToString()).FirstOrDefaultAsync(cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(estateSection.LegacyId))
                                        returnValue = estateSection.LegacyId;
                                    else
                                        returnValue = estateSection.Id;
                                }
                                else if (TableName == "EstateSubSection")
                                {
                                    var estateSubSection = await EstateSubSectionRepository.TableNoTracking.Where(x => x.Id == id.ToString() || x.LegacyId == id.ToString()).FirstOrDefaultAsync(cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(estateSubSection.LegacyId))
                                        returnValue = estateSubSection.LegacyId;
                                    else
                                        returnValue = estateSubSection.Id;
                                }

                            }
                        }
                        else
                        {
                            var tmp = GetValue(obj, mappingInfoDetail.NewPropertyOwnerPath);// obj.GetType().GetProperty(mappingInfoDetail.NewPropertyOwnerPath).GetValue(obj, null);
                            if (tmp != null)
                            {
                                if (mappingInfoDetail.NewPropertyName == "LegacyId")
                                {
                                    var lagacyIdValue = GetValue(tmp, mappingInfoDetail.NewPropertyName); //tmp.GetType().GetProperty(mappingInfoDetail.NewPropertyName).GetValue(tmp, null);
                                    if (lagacyIdValue == null)
                                        lagacyIdValue = GetValue(tmp, "Id"); //tmp.GetType().GetProperty("Id").GetValue(tmp, null);
                                    if (lagacyIdValue is Guid)
                                        returnValue = ((Guid)lagacyIdValue).ToString().Replace("-", "").Replace("_", "").ToUpper();
                                    else
                                        returnValue = lagacyIdValue;
                                }
                                else returnValue = GetValue(tmp, mappingInfoDetail.NewPropertyName); //tmp.GetType().GetProperty(mappingInfoDetail.NewPropertyName).GetValue(tmp, null);
                            }
                        }
                        if (returnValue != null)
                        {

                            if (returnValue.GetType().Name != mappingInfoDetail.OldPropertyType && mappingInfoDetail.OldPropertyType != "List")
                            {

                                if (mappingInfoDetail.OldPropertyType.StartsWith("Enm"))
                                {
                                    Type resultEnumType = null;
                                    foreach (var enumType in EnumTypes)
                                    {
                                        var dattr = enumType.GetCustomAttribute<DescriptionAttribute>();
                                        if (dattr != null)
                                        {
                                            if (dattr.Description == mappingInfoDetail.OldPropertyType)
                                            {
                                                resultEnumType = enumType;
                                                break;
                                            }
                                        }
                                    }
                                    if (resultEnumType != null)
                                    {
                                        returnValue = Enum.Parse(resultEnumType, returnValue.ToString());
                                    }
                                    else
                                        returnValue = null;
                                }
                                else
                                {

                                    var t = Type.GetType(mappingInfoDetail.OldPropertyType);
                                    try
                                    {
                                        var trv = Convert.ChangeType(returnValue, t);
                                        if (trv != null)
                                            returnValue = trv;
                                        else
                                            returnValue = null;
                                    }
                                    catch
                                    {
                                        var trv = TypeDescriptor.GetConverter(t).ConvertFrom(returnValue);
                                        if (trv != null)
                                            returnValue = trv;
                                        else
                                            returnValue = null;
                                    }
                                }
                                if (returnValue == null)
                                    throw new Exception("خطا در دریافت اقلام اطلاعاتی  جهت امضا رخ داد");
                            }
                        }
                        return returnValue;

                    }
                }

            }
            return null;
        }
        public static object GetValue(object obj, string propertyname)
        {
            if (propertyname.StartsWith("Expr:"))
            {
                var expr = propertyname.Replace("Expr:", "");
                var list = new List<string>();
                if (expr.StartsWith("ConcatString"))
                {
                    var argumans = expr.Replace("ConcatString", "").Replace("(", "").Replace(")", "").Split(',');
                    foreach (var arg in argumans)
                    {
                        if (arg.StartsWith("p:"))
                        {
                            var val = GetValue(obj, arg.Replace("p:", ""));
                            if (val != null)
                                list.Add(val.ToString());
                        }
                        else list.Add(arg);
                    }
                    if (list.Count == argumans.Length)
                        return string.Concat(list);
                    else
                        return null;
                }
                else
                    return null;
            }
            
            object returnValue = null;
            if (propertyname == "LegacyId")
            {
                returnValue = obj.GetType().GetProperty(propertyname).GetValue(obj, null);
                if (returnValue == null)
                {
                    returnValue = obj.GetType().GetProperty("Id").GetValue(obj, null);
                    if (returnValue != null)
                        returnValue = returnValue.ToString().Replace("-", "").Replace("_", "").ToUpper();
                }
                return returnValue;
            }
            var sa = propertyname.Split(',');
            foreach (string propertyName in sa)
            {
               
                returnValue = obj.GetType().GetProperty(propertyName).GetValue(obj, null);
                if (returnValue != null)
                {
                    if (returnValue is System.Collections.ICollection)
                    {
                        var lst = returnValue as System.Collections.ICollection;
                        if (lst.Count > 0)
                            break;
                    }
                }
            }
            return returnValue;
        }
        public async Task<SignData> GetDataSignText(object obj, SignInfoDataGraphNode signInfoDataGraph, CancellationToken cancellationToken)
        {
            if (EnumTypes.Count == 0)
            {
                var assembly = Assembly.Load("Notary.SSAA.BO.SharedKernel");
                EnumTypes = assembly.GetTypes().Where(t => t.Namespace == "Notary.SSAA.BO.SharedKernel.Enumerations").ToList();
            }

            return await GetDataSignText(obj, signInfoDataGraph, true, cancellationToken);
        }
        public async Task<SignData> GetDataSignText(object obj, SignInfoDataGraphNode signInfoDataGraph, bool useEntityFormatting, CancellationToken cancellationToken)
        {
            SignData signData = new SignData();
            StringBuilder sb = new StringBuilder();
            Type objType = obj.GetType();

            if (signInfoDataGraph.SignElementsFields != null)
            {
                bool isFirstField = true;
                bool hasAnyNonHullValue = false;
                foreach (string fieldName in signInfoDataGraph.SignElementsFields)
                {

                    object fieldValue = await GetValue(objType.Name, fieldName, obj, cancellationToken);
                    if (fieldValue != null)
                    {
                        hasAnyNonHullValue = true;
                        if (!isFirstField)
                        {
                            if (useEntityFormatting)
                                sb.Append(',');
                        }
                        else
                        {
                            if (useEntityFormatting)
                            {
                                sb.Append('{');
                            }
                            isFirstField = false;
                        }
                        if (useEntityFormatting)
                        {
                            sb.Append(fieldName);
                            sb.Append(':');
                            sb.Append(string.Format("'{0}'", GetFieldStringValue(fieldValue)));
                        }
                        else
                            sb.Append(GetFieldStringValue(fieldValue));
                    }
                }
                if (hasAnyNonHullValue && useEntityFormatting)
                    sb.Append('}');
            }
            if (signInfoDataGraph.RelationsDataGraphs != null)
            {
                foreach (SignInfoDataGraphNode relationSignInfoDataGraph in signInfoDataGraph.RelationsDataGraphs)
                {
                    object fieldValue = await this.GetValue(objType.Name, relationSignInfoDataGraph.RelationName, obj, cancellationToken);
                    if (fieldValue != null)
                    {
                        if (fieldValue is System.Collections.IEnumerable)
                        {
                            var sortedList = GetSortedEntityList((System.Collections.IEnumerable)fieldValue, false);
                            foreach (object element in sortedList)
                            {
                                var sd = await GetDataSignText(element, relationSignInfoDataGraph, cancellationToken);
                                sb.Append(sd.StringData);
                            }

                        }
                        else
                        {
                            var sd = await GetDataSignText(fieldValue, relationSignInfoDataGraph, cancellationToken);
                            sb.Append(sd.StringData);
                        }
                    }

                }
            }
            signData.StringData = sb.ToString();
            return signData;
        }
        private static string GetFieldStringValue(object fieldValue)
        {
            if (fieldValue != null)
            {
                if (fieldValue is double || fieldValue is double?)
                {
                    return GetDoubleFieldValueText(fieldValue);
                }
                else
                    return fieldValue.ToString();
            }
            else
                return null;
        }
        private static string GetDoubleFieldValueText(object fieldValue)
        {
            if (fieldValue != null)
            {
                if (fieldValue is double)
                {
                    return GetDoubleValueText((double)fieldValue);
                }
                else if (fieldValue is double?)
                {
                    return GetDoubleValueText((double?)fieldValue);
                }
            }
            return null;
        }
        private static string GetDoubleValueText(double? value)
        {
            string strValue = "";
            if (value.HasValue)
            {
                strValue = value.ToString();
                string decimalSeperator = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                strValue = strValue.Replace(decimalSeperator, ".");
            }
            return strValue;
        }
        private static string GetDoubleValueText(decimal? value)
        {
            if (value != null)
                return GetDoubleValueText(value.Value);
            else
                return null;
        }

        private static System.Collections.IEnumerable GetSortedEntityList(System.Collections.IEnumerable entityList, bool fDescending)
        {

            if (!IsNullOrEmptyList(entityList))
            {
                Type elementType = GetListElementsType(entityList);
                if (elementType != null)
                {
                    Type genericListType = typeof(List<>).MakeGenericType(elementType);
                    var property = elementType.GetProperty("LegacyId");
                    var listToBeSorted = (System.Collections.IList)Activator.CreateInstance(genericListType);

                    foreach (object element in entityList)
                    {
                        var pval = property.GetValue(element, null);
                        if (pval == null)
                        {
                            var idVal = (Guid)elementType.GetProperty("Id").GetValue(element, null);
                            property.SetValue(element, idVal.ToString().Replace("-", "").Replace("_", "").ToUpper(), null);
                        }
                        listToBeSorted.Add(element);
                    }
                    SortList(listToBeSorted, property, !fDescending);
                    return listToBeSorted;
                }
                else return entityList;
            }
            else
                return entityList;
        }
        public static void SortList(object dataSource, PropertyInfo property, bool isAscending)
        {
            List<object> list = new List<object>();
            var tmpLst = (System.Collections.IList)dataSource;
            foreach (var item in tmpLst)
            {
                list.Add(item);
            }
            Comparison<object> compare = delegate (object a, object b)
            {
                bool asc = isAscending;
                object valueA = asc ? property.GetValue(a, null) : property.GetValue(b, null);
                object valueB = asc ? property.GetValue(b, null) : property.GetValue(a, null);

                return valueA is IComparable ? ((IComparable)valueA).CompareTo(valueB) : 0;
            };
            list.Sort(compare);
        }
        private static bool IsNullOrEmptyList(System.Collections.IEnumerable list)
        {
            if (list == null)
                return true;
            foreach (object element in list)
            {
                return false;
            }
            return true;
        }
        private static Type GetListElementsType(System.Collections.IEnumerable list)
        {
            if (list == null)
                return null;
            foreach (object element in list)
            {
                return element.GetType();
            }
            return null;
        }       
    }

}
