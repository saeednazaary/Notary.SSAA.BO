using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notary.SSAA.BO.Infrastructure.Services.Implementations.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Attributes;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Contracts.Security;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Stimulsoft.Report;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;

namespace Notary.SSAA.BO.Infrastructure.Services.Implementations.Security
{

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly CryptoService _cryptoService;

        public UserService (IHttpContextAccessor httpContextAccessor, IConfiguration _configuration, CryptoService cryptoService )
        {
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
            _cryptoService = cryptoService;

            InitializeUserContextAsync ().Wait();


        }

        private async Task InitializeUserContextAsync()
        {

            IEnumerable<System.Security.Claims.Claim> userClaimsList = _httpContextAccessor.HttpContext.User.Claims;
            string info= userClaimsList?.FirstOrDefault(x => x.Type.Equals("info", StringComparison.OrdinalIgnoreCase))?.Value;
            string applicationUserName = null;

            if (!string.IsNullOrEmpty(info))
            {
                var decrypted = _cryptoService.Decrypt<dynamic>(info, userClaimsList);
                if (decrypted != null)
                {
                    var infoListString =decrypted.ToString();
                    List<ClaimData> infoList  = JsonConvert.DeserializeObject<List<ClaimData>> ( infoListString );
                    string branchIdFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("BranchId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchCodeFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("BranchCode", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchAccessIdFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("BranchAccessId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchNameFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("BranchName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string userRoleIdFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("UserRoleId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string userRoleNameFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("UserRoleName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchAccessDisplayNameFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("BranchAccessDisplayName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string firstNameFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("FirstName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string lastNameFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("LastName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string idFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string userNameFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;
                    
                    string issueDocAccessFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("IssueDocAccess", StringComparison.OrdinalIgnoreCase))?.Value;
                    string isBranchOwnerFromInfo = infoList?.FirstOrDefault(x => x.Type.Equals("IsBranchOwner", StringComparison.OrdinalIgnoreCase))?.Value;




                    string clientIdFromInfo = userClaimsList?.FirstOrDefault(x => x.Type.Equals("client_id", StringComparison.OrdinalIgnoreCase))?.Value;
                    applicationUserName = userNameFromInfo;
                    var tokenFromInfo = GetBearerToken();
                    ApplicationUser applicationUserFromInfo = new(idFromInfo, userNameFromInfo, firstNameFromInfo, lastNameFromInfo);
                    ApplicationUserRole applicationUserRoleFromInfo = new(userRoleIdFromInfo, userRoleNameFromInfo);
                    ApplicationBranchAccess branchAccessFromInfo = new(branchAccessIdFromInfo, branchIdFromInfo, branchCodeFromInfo, branchNameFromInfo, branchAccessDisplayNameFromInfo,issueDocAccessFromInfo,isBranchOwnerFromInfo);
                    if ( applicationUserName == "Administrator" || applicationUserName == null )
                    {
                        UserApplicationContext = new UserApplicationContext ( applicationUserFromInfo, applicationUserRoleFromInfo, branchAccessFromInfo, tokenFromInfo, new ScriptoriumInformation () );
                    }
                    else
                    {
                        var httpContext = _httpContextAccessor.HttpContext;
                        var endpoint = httpContext.GetEndpoint();
                        if ( endpoint != null )
                        {
                            var routeAttribute = endpoint.Metadata.GetMetadata<GetScriptoriumInformation>();
                            if ( routeAttribute != null )
                            {
                                var scriptoriumInfo = await getScriptoriumInformation(new string[] { branchCodeFromInfo }, tokenFromInfo);
                                UserApplicationContext = new UserApplicationContext ( applicationUserFromInfo, applicationUserRoleFromInfo, branchAccessFromInfo, tokenFromInfo, scriptoriumInfo );

                            }
                            else
                            {

                                UserApplicationContext = new UserApplicationContext ( applicationUserFromInfo, applicationUserRoleFromInfo, branchAccessFromInfo, tokenFromInfo, null );

                            }
                        }
                        else
                        {
                            UserApplicationContext = new UserApplicationContext ( applicationUserFromInfo, applicationUserRoleFromInfo, branchAccessFromInfo, tokenFromInfo, null );

                        }

                    }
                }
                else
                {
                    Console.WriteLine ( "0x54321:" + GetBearerToken () );
                    string branchId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchCode = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchCode", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchAccessId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchAccessId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string userRoleId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserRoleId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string userRoleName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserRoleName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string branchAccessDisplayName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchAccessDisplayName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string firstName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("FirstName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string lastName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("LastName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string id = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value;
                    string userName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;
                    string clientId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("client_id", StringComparison.OrdinalIgnoreCase))?.Value;
                    string issueDocAccess = userClaimsList?.FirstOrDefault(x => x.Type.Equals("IssueDocAccess", StringComparison.OrdinalIgnoreCase))?.Value;
                    string isBranchOwner = userClaimsList?.FirstOrDefault(x => x.Type.Equals("IsBranchOwner", StringComparison.OrdinalIgnoreCase))?.Value;

                    applicationUserName = userName;
                    var token = GetBearerToken();
                    ApplicationUser applicationUser = new(id, userName, firstName, lastName);
                    ApplicationUserRole applicationUserRole = new(userRoleId, userRoleName);
                    ApplicationBranchAccess branchAccess = new(branchAccessId, branchId, branchCode, branchName, branchAccessDisplayName,issueDocAccess,isBranchOwner);
                    if ( applicationUserName == "Administrator" || applicationUserName == null )
                    {
                        UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, new ScriptoriumInformation () );
                    }
                    else
                    {
                        var httpContext = _httpContextAccessor.HttpContext;
                        var endpoint = httpContext.GetEndpoint();
                        if ( endpoint != null )
                        {
                            var routeAttribute = endpoint.Metadata.GetMetadata<GetScriptoriumInformation>();
                            if ( routeAttribute != null )
                            {
                                var scriptoriumInfo = await getScriptoriumInformation(new string[] { branchCode }, token);
                                UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, scriptoriumInfo );

                            }
                            else
                            {

                                UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, null );

                            }
                        }
                        else
                        {
                            UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, null );

                        }

                    }
                    //UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token );
                }
            }
            else
            {
                string branchId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchId", StringComparison.OrdinalIgnoreCase))?.Value;
                string branchCode = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchCode", StringComparison.OrdinalIgnoreCase))?.Value;
                string branchAccessId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchAccessId", StringComparison.OrdinalIgnoreCase))?.Value;
                string branchName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchName", StringComparison.OrdinalIgnoreCase))?.Value;
                string userRoleId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserRoleId", StringComparison.OrdinalIgnoreCase))?.Value;
                string userRoleName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserRoleName", StringComparison.OrdinalIgnoreCase))?.Value;
                string branchAccessDisplayName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchAccessDisplayName", StringComparison.OrdinalIgnoreCase))?.Value;
                string firstName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("FirstName", StringComparison.OrdinalIgnoreCase))?.Value;
                string lastName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("LastName", StringComparison.OrdinalIgnoreCase))?.Value;
                string id = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value;
                string userName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;
                string clientId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("client_id", StringComparison.OrdinalIgnoreCase))?.Value;
                string issueDocAccess = userClaimsList?.FirstOrDefault(x => x.Type.Equals("IssueDocAccess", StringComparison.OrdinalIgnoreCase))?.Value;
                string isBranchOwner = userClaimsList?.FirstOrDefault(x => x.Type.Equals("IsBranchOwner", StringComparison.OrdinalIgnoreCase))?.Value;
                applicationUserName = userName;
                var token = GetBearerToken();
                ApplicationUser applicationUser = new(id, userName, firstName, lastName);
                ApplicationUserRole applicationUserRole = new(userRoleId, userRoleName);
                ApplicationBranchAccess branchAccess = new(branchAccessId, branchId, branchCode, branchName, branchAccessDisplayName,issueDocAccess,isBranchOwner);
                if ( applicationUserName == "Administrator" || applicationUserName == null )
                {
                    UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, new ScriptoriumInformation () );
                }
                else
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    var endpoint = httpContext.GetEndpoint();
                    if ( endpoint != null )
                    {
                        var routeAttribute = endpoint.Metadata.GetMetadata<GetScriptoriumInformation>();
                        if ( routeAttribute != null )
                        {
                            var scriptoriumInfo = await getScriptoriumInformation(new string[] { branchCode }, token);
                            UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, scriptoriumInfo );

                        }
                        else
                        {

                            UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, null );

                        }
                    }
                    else
                    {
                        UserApplicationContext = new UserApplicationContext ( applicationUser, applicationUserRole, branchAccess, token, null );

                    }

                }
            }


            //var branchId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchId", StringComparison.OrdinalIgnoreCase))?.Value;
            //var branchCode = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchCode", StringComparison.OrdinalIgnoreCase))?.Value;
            //var branchAccessId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchAccessId", StringComparison.OrdinalIgnoreCase))?.Value;
            //var branchName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchName", StringComparison.OrdinalIgnoreCase))?.Value;
            //var userRoleId = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserRoleId", StringComparison.OrdinalIgnoreCase))?.Value;
            //var userRoleName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserRoleName", StringComparison.OrdinalIgnoreCase))?.Value;
            //var branchAccessDisplayName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("BranchAccessDisplayName", StringComparison.OrdinalIgnoreCase))?.Value;
            //var firstName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("FirstName", StringComparison.OrdinalIgnoreCase))?.Value;
            //var lastName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("LastName", StringComparison.OrdinalIgnoreCase))?.Value;
            //var id = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value;
            //var userName = userClaimsList?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;

            //var applicationUser = new ApplicationUser(id, userName, firstName, lastName);
            //var applicationUserRole = new ApplicationUserRole(userRoleId, userRoleName);
            //var branchAccess = new ApplicationBranchAccess(branchAccessId, branchId, branchCode, branchName, branchAccessDisplayName);
            //var token = GetBearerToken();

           
        }


        private UserApplicationContext applicationContext;

        public UserApplicationContext UserApplicationContext
        {
            get
            {
                return applicationContext;
            }
            set
            {
                applicationContext = value;
            }
        }

        private string GetBearerToken()
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string authorizationHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                // Extract the token from the Authorization header.
                //string token = authorizationHeader.Substring("Bearer ".Length).Trim();
                return authorizationHeader;
            }

            return null; // No Bearer token found in the request.
        }

        public async Task<ScriptoriumInformation> getScriptoriumInformation(string[] ids, string token)
        {
            ScriptoriumInformation scriptoriumInformation = new ScriptoriumInformation();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            string bo_Apis_prefix = configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
            var baseInfoUrl = configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/Common/GetScriptoriumById";
            ScriptoriumInput scriptoriumInput = new ScriptoriumInput();
            scriptoriumInput.IdList = ids;
            StringContent content = new(JsonConvert.SerializeObject(scriptoriumInput), System.Text.Encoding.UTF8, "application/json");


            var eventResponse = await client.PostAsync(baseInfoUrl, content).ConfigureAwait(false);

            if (eventResponse.StatusCode == HttpStatusCode.OK)
            {
                string res = await eventResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ApiResult<ScriptoriumData>>(res);
                if (data.IsSuccess && data.Data.ScriptoriumList != null && data.Data.ScriptoriumList.Count > 0)
                {
                    return data.Data.ScriptoriumList[0];
                }
                return null;


            }
            else
            {
                return null;
            }
            return null;

        }



        public class ScriptoriumInput
        {
            public string[] IdList;

        }
        public class ClaimData
        {
            public string Issuer { get; set; }
            public string OriginalIssuer { get; set; }
            public Dictionary<string, string> Properties { get; set; }
            public string Subject { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
            public string ValueType { get; set; }
        }


    }
}