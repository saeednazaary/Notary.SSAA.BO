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
using Notary.SSAA.BO.Domain.Abstractions.Base;

namespace Notary.SSAA.BO.QueryHandler.DigitalSignNew
{
    public class VerifySignQueryHandler : BaseQueryHandler<VerifyNewSignQuery, ApiResult<VerifySignViewModel>>
    {
        private readonly IDateTimeService _dateTimeService;        
        private readonly IRepository<DigitalSignatureValue> _digitalSignatureValueRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        public VerifySignQueryHandler(IMediator mediator, IUserService userService, IDateTimeService dateTimeService, IRepository<DigitalSignatureValue> digitalSignatureValueRepository, IRepository<ConfigurationParameter> configurationParameterRepository)
            : base(mediator, userService)
        {
            _dateTimeService = dateTimeService;
            _digitalSignatureValueRepository = digitalSignatureValueRepository;
            _configurationParameterRepository = configurationParameterRepository;
        }
        protected override bool HasAccess(VerifyNewSignQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<VerifySignViewModel>> RunAsync(VerifyNewSignQuery request, CancellationToken cancellationToken)
        {
            var signValueValidationSeconds = await GetDigitalSignatureValueValidationTimeInSeconds(cancellationToken);
            VerifySignViewModel result = new() { Result = true, ErrorMessage = "" };
            ApiResult<VerifySignViewModel> apiResult = new();
            X509Certificate2 cer = null;
            var dataToSign = request.Data;
            if (string.IsNullOrWhiteSpace(dataToSign))
            {
                GetDataToNewSignQuery GetDataToNewSignQuery = new GetDataToNewSignQuery() { HandlerInput = request.HandlerInput, HandlerName = request.HandlerName };
                var GetDataToNewSignOutput = await _mediator.Send(GetDataToNewSignQuery, cancellationToken);
                var signDataViewModel = GetDataToNewSignOutput.Data;
                foreach (var item in signDataViewModel.SignDataList)
                {
                    if (item.MainObjectId == request.MainObjectId && signDataViewModel.HandlerName == request.HandlerName)
                    {
                        dataToSign = item.Data;
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(dataToSign))
            {
                result.Result = false;
                result.ErrorMessage = "داده  جهت تصدیق امضای دیجیتال یافت نشد";
                apiResult.IsSuccess = false;
                apiResult.message.Add(result.ErrorMessage);
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.Data = result;

                return apiResult;
            }
            
            try
            {
                byte[] rawDataByte = Convert.FromBase64String(dataToSign);
                cer = new X509Certificate2(Convert.FromBase64String(request.Certificate));
                var rsa = cer.GetRSAPublicKey();
                if (rsa != null)
                {
                    if (rsa.VerifyData(rawDataByte, Convert.FromBase64String(request.Sign), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1))
                    {
                        
                        if (request.SaveSignValue)
                        {
                            var digitalSignatureValue = new DigitalSignatureValue()
                            {
                                Certificate = request.Certificate,
                                CreateDate = _dateTimeService.CurrentPersianDate,
                                CreateTime = _dateTimeService.CurrentTime.Substring(0, 5),
                                DataHandlerInput = request.HandlerInput,
                                DataHandlerName = request.HandlerName,
                                Id = Guid.NewGuid(),
                                SignValue = request.Sign,
                                MainObjectId = request.MainObjectId
                            };
                            var date = digitalSignatureValue.CreateDate + "-" + digitalSignatureValue.CreateTime;
                            var expireDateTime = date.AddSeconds(signValueValidationSeconds);
                            digitalSignatureValue.ExpireDate = expireDateTime.Substring(0, 10);
                            digitalSignatureValue.ExpireTime = expireDateTime.Substring(11);
                            await _digitalSignatureValueRepository.AddAsync(digitalSignatureValue, cancellationToken);
                            result.Id = digitalSignatureValue.Id.ToString();
                            
                        }
                        result.HandlerName = request.HandlerName;
                        result.SignDataQueryHandler = request.SignDataQueryHandler;
                        if (result.SignDataQueryHandler != SignDataQueryHandlers.None)
                            result.HandlerName = "";
                        result.MainObjectId = request.MainObjectId;
                        result.RawDataBase64 = dataToSign;
                        result.Result = true;
                        result.ErrorMessage = "";
                    }
                    else
                    {
                        result.Result = false;
                        result.ErrorMessage = "تصدیق امضای دیجیتال با شکست مواجه شد";
                    }
                }
                else
                {
                    result.Result = false;
                    result.ErrorMessage = "خطا در دریافت کلید عمومی گواهی جهت انجام عملیایت تصدیق رخ داد";
                }
            }
            catch
            {
                result.Result = false;
                result.ErrorMessage = "خطا در تصدیق امضای دیجیتال رخ داد";
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

        private async Task<int> GetDigitalSignatureValueValidationTimeInSeconds(CancellationToken cancellationToken)
        {
            var cp = await _configurationParameterRepository
                          .TableNoTracking
                          .Where(x => x.Subject == "Digital_Signature_Value_Validation_Time_In_Seconds")
                          .FirstOrDefaultAsync(cancellationToken);
            if (cp != null && !string.IsNullOrWhiteSpace(cp.Value))
                return Convert.ToInt32(cp.Value);
            return 10;
        }
    }
}
