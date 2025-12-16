using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Notary.SSAA.BO.Utilities.Extensions;
using Newtonsoft.Json;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.QueryHandler.DigitalSign
{
    public class VerifySignQueryHandler : BaseQueryHandler<VerifySignQuery, ApiResult<VerifySignViewModel>>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IDigitalSignatureConfigurationRepository _digitalSignatureConfigurationRepository;
        private readonly IServiceProvider _serviceProvider;
        public VerifySignQueryHandler(IMediator mediator, IUserService userService,IDateTimeService dateTimeService, IDigitalSignatureConfigurationRepository digitalSignatureConfigurationRepository, IServiceProvider serviceProvider)
            : base(mediator, userService)
        {
            _dateTimeService = dateTimeService;
            _digitalSignatureConfigurationRepository = digitalSignatureConfigurationRepository;
            _serviceProvider = serviceProvider;
        }
        protected override bool HasAccess(VerifySignQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<VerifySignViewModel>> RunAsync(VerifySignQuery request, CancellationToken cancellationToken)
        {
            VerifySignViewModel result = new() { Result = true, ErrorMessage = "" };
            ApiResult<VerifySignViewModel> apiResult = new();            
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
            if (_digitalSignatureConfiguration != null)
            {
                object entity = null;
                if (string.IsNullOrWhiteSpace(request.JsonData))
                    entity = await GetRelatedEntity(_digitalSignatureConfiguration.RelatedRepository, _digitalSignatureConfiguration.RelatedRepositoryMethod, request.EntityId, cancellationToken);
               
                if (entity != null || !string.IsNullOrWhiteSpace(request.JsonData))
                {
                    string signCertificate = request.Certificate;
                    if (string.IsNullOrWhiteSpace(signCertificate))
                    {

                        var propertyValue = GetPropertyValue(entity, _digitalSignatureConfiguration.RelatedCertificateField);
                        if (propertyValue != null)
                        {
                            signCertificate = propertyValue.ToString();
                        }

                    }
                    if(!string.IsNullOrWhiteSpace(signCertificate))
                    {
                        string signValue = request.Sign;
                        if (string.IsNullOrWhiteSpace(signValue))
                        {

                            var propertyValue = GetPropertyValue(entity, _digitalSignatureConfiguration.RelatedDigitalSignField);
                            if (propertyValue != null)
                            {
                                signValue = propertyValue.ToString();
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(signValue))
                        {

                            string dataToSign = "";

                            if (string.IsNullOrWhiteSpace(request.JsonData))
                            {
                                var provider = new Utilities.DigitalSign.FormSignInfoDataGraphProvider();
                                var dataGrophInfo = provider.GetFormSignInfoDataGraph(_digitalSignatureConfiguration.Descriptor);
                                if (dataGrophInfo != null)
                                {
                                    var dataGraphSignHelper = new DataGraphSignHelper(this._mediator);
                                    dataGraphSignHelper.DigitalSignatureConfiguration = _digitalSignatureConfiguration;
                                    var signData = await dataGraphSignHelper.GetDataSignText(entity, dataGrophInfo, cancellationToken);
                                    if (_digitalSignatureConfiguration.GetDataAsJson.ToBoolean())
                                    {
                                        dataToSign = ToJson(signData.Data, _digitalSignatureConfiguration);
                                    }
                                    else
                                        dataToSign = signData.StringData;
                                }
                            }
                            else
                                dataToSign = request.JsonData;
                            if (!string.IsNullOrWhiteSpace(dataToSign))
                            {
                                List<byte> byteList = new List<byte>();
                                byteList.AddRange(System.Text.Encoding.UTF8.GetBytes(dataToSign));
                                byte[] rawDataByte = byteList.ToArray();


                                using (var cer = new X509Certificate2(Convert.FromBase64String(signCertificate)))
                                {
                                    var rsa = cer.GetRSAPublicKey();
                                    if (rsa != null)
                                    {
                                        if (rsa.VerifyData(rawDataByte, Convert.FromBase64String(signValue), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1))
                                        {
                                            result.Result = true;
                                            result.ErrorMessage = "";
                                        }
                                        else
                                        {
                                            byteList = new List<byte>();
                                            byteList.AddRange(System.Text.Encoding.Unicode.GetBytes(dataToSign));
                                            rawDataByte = byteList.ToArray();
                                            if (rsa.VerifyData(rawDataByte, Convert.FromBase64String(signValue), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1))
                                            {
                                                result.Result = true;
                                                result.ErrorMessage = "";
                                            }
                                            else
                                            {
                                                result.Result = false;
                                                result.ErrorMessage = "تصدیق امضای دیجیتال با شکست مواجه شد";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        result.Result = false;
                                        result.ErrorMessage = "خطا در دریافت کلید عمومی گواهی جهت انجام عملیایت تصدیق رخ داد";
                                    }
                                }
                            }
                            else
                            {
                                result.Result = false;
                                result.ErrorMessage = "خطا در تصدیق امضای دیجیتال رخ داد";
                            }
                        }
                        else
                        {
                            result.Result = false;
                            result.ErrorMessage = "  امضای دیجیتال جهت تصدیق یافت نشد ";
                        }
                    }
                    else
                    {
                        result.Result = false;
                        result.ErrorMessage = "گواهی امضای دیجیتال یافت نشد";
                    }
                }
                else
                {
                    result.Result = false;
                    result.ErrorMessage = "بازیابی اطلاعات موجودیت مرتبط از بانک اطلاعاتی انجام نشد";
                }

            }
            else
            {
                result.Result = false;
                result.ErrorMessage = "مشخصات تنظیمات امضای دیجیتال برای این فرم یافت نشد";
            }

            apiResult.IsSuccess = result.Result;
            if (!result.Result)
            {
                apiResult.message.Add(result.ErrorMessage);
            }
            apiResult.statusCode = ApiResultStatusCode.Success;
            apiResult.Data = result;

            return apiResult;
        }

        private async Task<object> GetRelatedEntity(string repository, string repositoryMethod, string entityId, CancellationToken cancellationToken)
        {
            var repostoryType = Type.GetType(repository + ", SSAA.Notary.Infrastructure");
            var applicationContextType = Type.GetType("SSAA.Notary.Infrastructure.Contexts.ApplicationContext, SSAA.Notary.Infrastructure");            
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

       

        private static object GetPropertyValue(object ownerObject, string propertyPath)
        {
            if (ownerObject == null) return null;
            if (string.IsNullOrEmpty(propertyPath)) return null;
            string[] propertyPathSplited = propertyPath.Split('.');
            Type t = ownerObject.GetType();
            var pi = t.GetProperty(propertyPathSplited[0]);
            object value = null;
            if (pi != null)
            {
                value = pi.GetValue(ownerObject, null);
                if (propertyPathSplited.Length > 1)
                {
                    string np = "";
                    for (int i = 1; i < propertyPathSplited.Length; i++)
                    {

                        np += propertyPathSplited[i] + ".";

                    }
                    np = np.Remove(np.Length - 1);
                    value = GetPropertyValue(value, np);
                }

            }


            return value;
        }

        private static string ToJson(object entity, DigitalSignatureConfiguration digitalSignatureConfiguration)
        {

            Formatting formatting = Formatting.None;
            JsonSerializerSettings jsonSerializerSettings = null;
            if (!string.IsNullOrWhiteSpace(digitalSignatureConfiguration.JsonFormatting))
                formatting = (Formatting)Enum.Parse(typeof(Formatting), digitalSignatureConfiguration.JsonFormatting);
            if (!string.IsNullOrWhiteSpace(digitalSignatureConfiguration.JsonSerializerSettings))
                jsonSerializerSettings = JsonConvert.DeserializeObject<Newtonsoft.Json.JsonSerializerSettings>(digitalSignatureConfiguration.JsonSerializerSettings);
            if (jsonSerializerSettings != null)
                return JsonConvert.SerializeObject(entity, formatting, jsonSerializerSettings);
            else
                return JsonConvert.SerializeObject(entity, formatting);
        }

    }
}
