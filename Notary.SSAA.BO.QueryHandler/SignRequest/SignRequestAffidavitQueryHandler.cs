using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ClientLogin;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ClientLogin;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.SharedModels;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class SignRequestAffidavitQueryHandler : BaseExternalQueryHandler<SignRequestAffidavitQuery, ExternalApiResult>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly IDateTimeService _dateTimeService;
        private static ClientLoginViewModel _cachedToken;
        private readonly string docTypeId = "cd1d8d95d1ea49dea5f40f8883f3e6b4";

        public SignRequestAffidavitQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, IPersonFingerprintRepository personFingerprintRepository,
            IDateTimeService dateTimeService, IRepository<SsrApiExternalUser> ssrApiExternalUser) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _ssrApiExternalUser = ssrApiExternalUser ?? throw new ArgumentNullException(nameof(ssrApiExternalUser));
        }

        protected override bool HasAccess(SignRequestAffidavitQuery request, IList<string> userRoles)
        {
            return true;
        }

        protected async override Task<ExternalApiResult> RunAsync(SignRequestAffidavitQuery request, CancellationToken cancellationToken)
        {
            ExternalApiResult<SignRequestAffidavitViewModel> apiResult = new();
            var personOutputs = new List<Output2>();
            var affidavit = new SignRequestAffidavitViewModel();
            var user = await _ssrApiExternalUser.TableNoTracking.Include(x => x.SsrDocVerifExternalUsers).Where(x => x.UserName == request.UserName && x.UserPassword == request.Password &&
            x.State == "1" && x.SsrDocVerifExternalUsers.Any(x => x.IsActive == "1" && x.MethodName == "EghrarNameMethod")).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                var outputObj = new SignRequestAffidavitViewModel();
                outputObj.Desc = "100_کاربر محترم ، شما دسترسی لازم را برای استفاده از این متد ندارید.";
                outputObj.HasPermission = false;

                apiResult.ResMessage = "100_کاربر محترم ، شما دسترسی لازم را برای استفاده از این متد ندارید.";
                apiResult.ResCode = "102";
                apiResult.Data= outputObj;
                return apiResult;
            }
            if (!HasPermissionToDocTypeVer2(user.SsrDocVerifExternalUsers))
            {
                var outputObj_1 = new SignRequestAffidavitViewModel();
                outputObj_1.HasPermission = false;
                outputObj_1.Desc = "کاربر محترم ، شما دسترسی لازم برای استفاده از این نوع سند را ندارید_105";
                outputObj_1.succseed = true;

                apiResult.ResMessage = "کاربر محترم ، شما دسترسی لازم برای استفاده از این نوع سند را ندارید_105";
                apiResult.ResCode = "102";
                apiResult.Data = outputObj_1;
                return apiResult;
            }
            string signRequestScriptoriumNo = request.SignRequestScriptoriumNo;
            if (string.IsNullOrWhiteSpace(request.SignRequestScriptoriumNo))
            {
                signRequestScriptoriumNo = request.SignRequestNationalNo.Substring(7, 5);

            }
            var databaseRes = await _signRequestRepository.SignRequestAffidavit(request.SignRequestNationalNo, request.SignRequestSecretCode, signRequestScriptoriumNo, TestScriptoriumIds.ScriptoriumIds, cancellationToken);
            if (databaseRes is null || databaseRes.Count == 0)
            {
                affidavit.ExistDoc = false;
                affidavit.HasPermission = true;
                affidavit.Desc = "هیچ گواهی امضایی با این مشخصات یافت نشد.";
                affidavit.succseed = true;
                apiResult.ResCode = "101";
                apiResult.ResMessage = "هیچ گواهی امضایی با این مشخصات یافت نشد.";
                return apiResult;
            }
            string signRequestId = databaseRes.Select(x => x.SignRequestId).FirstOrDefault();
            string signRequestNo = databaseRes.Select(x => x.SignRequestNo).FirstOrDefault();
            List<string> signRequestPersonIds = databaseRes.Select(x => x.PersonId).ToList();


            var fingerprints = await _personFingerprintRepository.TableNoTracking.Where(x => signRequestPersonIds.Contains(x.UseCasePersonObjectId) && x.UseCaseMainObjectId == signRequestId).ToListAsync(cancellationToken);
            if (fingerprints == null || fingerprints.Count == 0)
            {
                affidavit.ExistDoc = false;
                affidavit.HasPermission = true;
                affidavit.Desc = "ارتباط با اطلاعات پایه برقرار نشد.";
                affidavit.succseed = true;
                apiResult.Data = affidavit;
                apiResult.ResCode = "105";
                apiResult.ResMessage = "اثر انگشت شخص یافت نشد.";
                return apiResult;
            }
            ClientLoginServiceInput clientLoginServiceInput = new();
            if (_cachedToken is null || string.IsNullOrWhiteSpace(_cachedToken.Credential.AccessToken) || _dateTimeService.CurrentDateTime.AddSeconds(30) >= _cachedToken.Credential.ExpireDate)
            {
                _cachedToken = new();
                var tokenRes = await _mediator.Send(clientLoginServiceInput, cancellationToken);
                if (tokenRes is not null && tokenRes.IsSuccess)
                {
                    _cachedToken.Credential.AccessToken = "Bearer " + tokenRes.Data.Credential.AccessToken;
                    _cachedToken.Credential.ExpireDate = tokenRes.Data.Credential.ExpireDate;
                    _cachedToken.UserBranchAccesses.BranchAccesses = tokenRes.Data.UserBranchAccesses.BranchAccesses;
                }
                else
                {
                    affidavit.ExistDoc = false;
                    affidavit.HasPermission = true;
                    affidavit.Desc = "ارتباط با سرویس توکن برقرار نشد.";
                    affidavit.succseed = true;
                    apiResult.Data = affidavit;
                    apiResult.ResCode = "106";
                    apiResult.ResMessage = "ارتباط با سرویس توکن برقرار نشد.";
                    return apiResult;
                }
            }
            _userService.UserApplicationContext.Token = _cachedToken.Credential.AccessToken;
            _userService.UserApplicationContext.UserRole.RoleId = _cachedToken.UserBranchAccesses.BranchAccesses.FirstOrDefault().RoleId;

            var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
            scriptoriumInfo.ScriptoriumId = databaseRes.Select(x => x.ScriptoriumId).FirstOrDefault();
            scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
            var baseInfoapiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
            if (!baseInfoapiResult.IsSuccess || baseInfoapiResult.Data is null)
            {
                affidavit.ExistDoc = false;
                affidavit.HasPermission = true;
                affidavit.Desc = "ارتباط با اطلاعات پایه برقرار نشد.";
                affidavit.succseed = true;
                apiResult.Data = affidavit;
                apiResult.ResCode = "103";
                apiResult.ResMessage = "ارتباط با اطلاعات پایه برقرار نشد.";
                return apiResult;
            }


            foreach (var person in databaseRes)
            {
                // Person’s fingerprint
                var fingerprint = fingerprints
                    .FirstOrDefault(f => f.UseCasePersonObjectId == person.PersonId.ToString());

                var outputPerson = new Output2
                {
                    NationalNo = person.NationalNo,
                    Name = person.Name,
                    Family = person.Family,
                    Mobile = person.MobileNo,
                    PostalCode = person.PostalCode,
                    Tel = person.Tel,
                    Address = person.Address,
                    AgentType = person.Title,
                    NameMovakel = person.MovakelName,
                    FamilyMovakel = person.MovakelFamily,
                    PostalCodeMovakel = person.PostalCode,
                    TelMovakel = person.MovakelTelNo,
                    MobileMovakel = person.MovakelMobileNo,
                    AddressMovakel = person.MovakelAddress,
                    NationalNoMovakel = person.MovakelAddress,
                    FingerPrintImage = fingerprint?.FingerprintImageFile
                };

                personOutputs.Add(outputPerson);
            }
            // 5️⃣ Build the final affidavit ViewModel
            affidavit = new SignRequestAffidavitViewModel
            {
                DocDate = databaseRes.First().DocDate,
                ScriptoriumName = baseInfoapiResult.Data.ScriptoriumName,
                SignGetterTitle = $"جهت ارائه به  {databaseRes.First().SignGetterTitle}",
                SignSubject = $"گواهی امضا -- با موضوع {databaseRes.First().SignGetterTitle}",
                DocType = "گواهی امضا",
                NationalRegisterNo = databaseRes.First().NationalRegisterNo,
                lstFindPersonInQuery = personOutputs,
                ExistDoc = true,
                succseed = true,
                HasPermission = true
            };
            if (!string.IsNullOrWhiteSpace(request?.HasPermissionToPdf) && request?.HasPermissionToPdf == "2")
            {
                try
                {
                    var reportQuery = new ReportSignRequestQuery() { SignRequestNo = signRequestNo };
                    var reportRes = await _mediator.Send(reportQuery, cancellationToken);

                    if (reportRes.IsSuccess && reportRes?.Data?.Data is not null)
                    {
                        affidavit.DocImage = reportRes?.Data?.Data;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(ex));
                }

            }

            apiResult.Data = affidavit;
            apiResult.ResMessage = "عملیات با موفقیت انجام شد.";
            apiResult.ResCode = "1";
            return apiResult;
        }

        public bool HasPermissionToDocTypeVer2(ICollection<SsrDocVerifExternalUser> _user)
        {
            foreach (SsrDocVerifExternalUser user in _user)
            {

                if (user.AllowedDocTypesId == "*")
                { return true; }
                if (user.AllowForAllDocTypes == "1")
                { return true; }
                if (string.IsNullOrWhiteSpace(user?.AllowedDocTypesId))
                    return false;
                string user_DocType = user.AllowedDocTypesId;

                string[] DocTypes = user_DocType.Split(',');

                foreach (var DocType in DocTypes)
                {
                    if (DocType == docTypeId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
