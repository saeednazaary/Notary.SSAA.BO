using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Microsoft.Extensions.Configuration;
using Azure.Core;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using System.Threading;
using System.Runtime.ConstrainedExecution;

namespace Notary.SSAA.BO.QueryHandler.DigitalSign
{
    public class ValidateCertificateQueryHandler : BaseQueryHandler<ValidateCertificateQuery, ApiResult<ValidateCertificateViewModel>>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public ValidateCertificateQueryHandler(IMediator mediator, IUserService userService,IDateTimeService dateTimeService,IHttpEndPointCaller httpEndPointCaller,IConfiguration configuration)
            : base(mediator, userService)
        {
            _dateTimeService = dateTimeService;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(ValidateCertificateQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<ValidateCertificateViewModel>> RunAsync(ValidateCertificateQuery request, CancellationToken cancellationToken)
        {
            ValidateCertificateViewModel result = new() { Result = true, ErrorMessage = "" };
            ApiResult<ValidateCertificateViewModel> apiResult = new();
            ApiResult<ValidateCertificateByOCSPViewModel> ocspResult = null;
                        
            if (!string.IsNullOrWhiteSpace(request.Certificate))
            {
                using (var cer = new System.Security.Cryptography.X509Certificates.X509Certificate2(Convert.FromBase64String(request.Certificate)))
                {
                    if (TokenIsForTheUser(cer.Subject))
                    {
                        var expirationDateTime = DateTime.Parse(cer.GetExpirationDateString());
                        var dateTimeNow = _dateTimeService.CurrentDateTime;
                        if (dateTimeNow > expirationDateTime)
                        {
                            result.Result = false;
                            result.ErrorMessage = "گواهی انتخاب شده منقضی شده است!\n لطفاً با مراجعه به دفاتر RA، گواهی معتبر دریافت نمایید.";
                        }
                        else
                        {
                            ocspResult = await ValidateCertificateByOCSP(request.Certificate, cancellationToken);
                            if (ocspResult != null && ocspResult.IsSuccess)
                            {
                                if (!ocspResult.Data.IsValid)
                                {
                                    result.Result = false;
                                    result.ErrorMessage = "گواهی انتخاب شده معتبر نیست!\n لطفاً با مراجعه به دفاتر RA، گواهی معتبر دریافت نمایید.";
                                }
                            }
                            else
                            {
                                result.Result = false;
                                if (ocspResult != null)
                                    result.ErrorMessage = ocspResult.message.FirstOrDefault();
                                else
                                    result.ErrorMessage = " خطا در دسترسی و فراخوانی سرویس اعتبارسنجی گواهی سازمان ثبت رخ داد  ";
                            }
                        }
                    }
                    else
                    {
                        result.Result = false;
                        result.ErrorMessage = "توکن مورد استفاده ، متعلق به کاربر جاری نمی باشد";
                    }
                }              
            }

            apiResult.Data = result;
            apiResult.IsSuccess = result.Result;
            apiResult.statusCode = ApiResultStatusCode.Success;
            if(!result.Result)
            {
                if (ocspResult != null && ocspResult.message.Count > 0)
                    apiResult.message.AddRange(ocspResult.message);
                else
                    apiResult.message.Add(result.ErrorMessage);
            }
            return apiResult;
        }

        async Task<ApiResult<ValidateCertificateByOCSPViewModel>> ValidateCertificateByOCSP(string certificate, CancellationToken cancellationToken)
        {
            try
            {
                string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/";
                ValidateCertificateByOCSPServiceQuery validateCertificateByOCSPServiceQuery = new ValidateCertificateByOCSPServiceQuery() { Certificate = certificate };
                var httpRequest = new HttpEndpointRequest<ValidateCertificateByOCSPServiceQuery>(mainUrl + "DigitalSignService/ValidateCertificateByOCSP", _userService.UserApplicationContext.Token, validateCertificateByOCSPServiceQuery);
                var result = await _httpEndPointCaller.CallPostApiAsync<ValidateCertificateByOCSPViewModel, ValidateCertificateByOCSPServiceQuery>(httpRequest, cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        bool TokenIsForTheUser(string certificateSubject)
        {
            return true;
            if (string.IsNullOrWhiteSpace(certificateSubject)) return false;
            bool result = false;
            string[] sa = certificateSubject.Split(',');
            if (sa.Length > 1)
            {
                foreach (string str in sa)
                {
                    if (str.StartsWith("SERIALNUMBER=") || str.StartsWith(" SERIALNUMBER="))
                    {
                        string[] a1 = str.Split('=');
                        if (this._userService.UserApplicationContext.User.UserName == a1[1])
                            result = true;

                    }
                }
            }
            return result;
        }

    }
}
