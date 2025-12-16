using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint_V2.Handlers
{
    internal class SetPersonMocStateCommandHandler : BaseCommandHandler<SetPersonMocStateCommand, ApiResult>
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        public SetPersonMocStateCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
           IPersonFingerprintRepository personFingerprintRepository, IDocumentRepository documentRepository, ISignRequestRepository signRequestRepository) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository;
            _documentRepository = documentRepository;
            _signRequestRepository = signRequestRepository;
            apiResult = new() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
        }

        protected async override Task<ApiResult> ExecuteAsync(SetPersonMocStateCommand request, CancellationToken cancellationToken)
        {

            if (request.UseCaseId == "7")
            {
                var ncarr = request.NationalityCode.Split('|');
                var nationalityCode1 = ncarr[0];
                var nationalityCode2 = ncarr[1];
                nationalityCode2 = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(nationalityCode2));
                nationalityCode2 = ConvertPersianToEnglish(nationalityCode2);
                if (ConvertPersianToEnglish(nationalityCode1) != nationalityCode2 && request.MocState == "1")
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("کد ملی فرد مورد نظر با کد ملی کارت هوشمند مطابقت ندارد");
                    request.MocState = "2";
                    request.MocStateDescription = "کد ملی فرد مورد نظر با کد ملی کارت هوشمند مطابقت ندارد";
                }
                return apiResult;
            }
            var personFingerprint = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            if (personFingerprint != null)
            {
                var nationalityCode = "";
                try
                {
                    nationalityCode = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(request.NationalityCode));
                }
                catch
                {
                    nationalityCode = request.NationalityCode;
                }
                nationalityCode = ConvertPersianToEnglish(nationalityCode);
                if (ConvertPersianToEnglish(personFingerprint.PersonNationalNo) != nationalityCode && request.MocState == "1")
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("کد ملی فرد مورد نظر با کد ملی کارت هوشمند مطابقت ندارد");

                    request.MocState = "2";
                    request.MocStateDescription = "کد ملی فرد مورد نظر با کد ملی کارت هوشمند مطابقت ندارد";
                }

                personFingerprint.MocState = request.MocState;
                if (!string.IsNullOrWhiteSpace(request.MocStateDescription))
                    personFingerprint.MocDescription = request.MocStateDescription;


                if (personFingerprint.PersonFingerprintUseCaseId == "1")//گواهی امضا
                {
                    var signRequest = await _signRequestRepository.GetByIdAsync(cancellationToken, personFingerprint.UseCaseMainObjectId.ToGuid());
                    if (signRequest != null)
                    {
                        await _signRequestRepository.LoadCollectionAsync(signRequest, x => x.SignRequestPeople, cancellationToken);
                        var person = signRequest.SignRequestPeople.Where(p => p.Id == personFingerprint.UseCasePersonObjectId.ToGuid()).FirstOrDefault();
                        if (person != null)
                        {
                            person.MocState = request.MocState;
                            await _signRequestRepository.UpdateAsync(signRequest, cancellationToken);
                            await _personFingerprintRepository.UpdateAsync(personFingerprint, cancellationToken);
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در ثبت وضعیت MOC شخص در سیستم");
                        }

                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("خطا در ثبت وضعیت MOC شخص در سیستم");
                    }
                }
                else if (personFingerprint.PersonFingerprintUseCaseId == "2")//سند رسمی
                {
                    var document = await _documentRepository.GetByIdAsync(cancellationToken, personFingerprint.UseCaseMainObjectId.ToGuid());
                    if (document != null)
                    {
                        await _documentRepository.LoadCollectionAsync(document, x => x.DocumentPeople, cancellationToken);
                        var person = document.DocumentPeople.Where(p => p.Id == personFingerprint.UseCasePersonObjectId.ToGuid()).FirstOrDefault();
                        if (person != null)
                        {
                            person.MocState = request.MocState;
                            await _documentRepository.UpdateAsync(document, cancellationToken);
                            await _personFingerprintRepository.UpdateAsync(personFingerprint, cancellationToken);
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در ثبت وضعیت MOC شخص در سیستم");
                        }

                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("خطا در ثبت وضعیت MOC شخص در سیستم");
                    }
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("خطا در ثبت وضعیت MOC شخص در سیستم");
            }

            if (apiResult.IsSuccess)
            {
                GetInquiryPersonFingerprintQuery inquiryFingerprintQuery = new(request.FingerprintId);
                var inquiryFingerprintResult = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);
                if (inquiryFingerprintResult.IsSuccess)
                    apiResult.Data = inquiryFingerprintResult.Data;
            }
            return apiResult;

        }

        protected override bool HasAccess(SetPersonMocStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        public static string ConvertPersianToEnglish(string persianNumber)
        {
            string[] persianDigits = new string[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            string[] englishDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            for (int i = 0; i < persianDigits.Length; i++)
            {
                persianNumber = persianNumber.Replace(persianDigits[i], englishDigits[i]);
            }

            return persianNumber;
        }
    }
}
