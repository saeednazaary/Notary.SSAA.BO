using MediatR;
using Newtonsoft.Json;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal class GetSignRequestPersonFingerprintQueryHandler : BaseQueryHandler<GetSignRequestPersonFingerprintQuery, ApiResult<List<SignRequestPersonFingerprintViewModel>>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly ISsrConfigRepository _ssrConfigRepository;
        private readonly IDateTimeService _dateTimeService;

        public GetSignRequestPersonFingerprintQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, 
            IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository, IDateTimeService dateTimeService)
            : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        protected override bool HasAccess(GetSignRequestPersonFingerprintQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<SignRequestPersonFingerprintViewModel>>> RunAsync(GetSignRequestPersonFingerprintQuery request, CancellationToken cancellationToken)
        {
            List<SignRequestPersonFingerprintViewModel> result = new List<SignRequestPersonFingerprintViewModel>();
            ApiResult<List<SignRequestPersonFingerprintViewModel>> apiResult = new();

            UpdateSignRequestFingerprintStateCommand check = new();
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            check.SignRequestId = request.SignRequestId;
            var res = await _mediator.Send(check, cancellationToken);

            Domain.Entities.SignRequest signRequest = await _signRequestRepository.GetSignRequest(signRequestId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (signRequest != null)
            {
                var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                scriptoriumInfo.ScriptoriumId = signRequest.ScriptoriumId;
                scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
                var baseInfoApiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
                if (!baseInfoApiResult.IsSuccess || baseInfoApiResult.Data is null)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("ارتباط با اطلاعات پایه برقرار نشد.");
                    return apiResult;
                }

                SsrConfigRepositoryInput configRepositoryInput = new SsrConfigRepositoryInput();
                configRepositoryInput.CurrentScriptoriumId = baseInfoApiResult.Data.ScriptoriumId;
                configRepositoryInput.CurrentGeoLocationId = baseInfoApiResult.Data.GeoLocationId;
                configRepositoryInput.UnitLevelCode = baseInfoApiResult.Data.UnitLevelCode;
                configRepositoryInput.CurrentDayOfWeek = baseInfoApiResult.Data.DayOfWeek;
                configRepositoryInput.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;

                var configs = await _ssrConfigRepository.GetBusinessConfig(configRepositoryInput, cancellationToken);
                if (!SignRequestBusinessRule.CheckSignRequestConfig(configs))
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("خطا در پیکربندی سرویس های گواهی امضا. لفطا با راهبر سیستم تماس بگیرید");
                    return apiResult;

                }
                if (!SignRequestBusinessRule.CheckWorkPermit(configs, baseInfoApiResult.Data.ScriptoriumId))
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("دفترخانه مجوز ثبت گواهی امضا ندارد. لفطا با راهبر سیستم تماس بگیرید");
                    return apiResult;
                }
                var getFingerprintPermitted = SignRequestBusinessRule.IsGetFingerprintOnWorkTime(_dateTimeService.CurrentDateTime, baseInfoApiResult.Data.DayOfWeek, configs);
                if (!getFingerprintPermitted)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("ساعت کاری اخذ اثرانگشت به پایان رسیده است.");
                    return apiResult;
                }

                string message = SignRequestPersonBusinessRule.CheckSabteAhval(signRequest.SignRequestPeople, configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;

                    apiResult.message.Add(message);
                    return apiResult;
                }
                if (signRequest.ScriptoriumId == "57999" || signRequest.ScriptoriumId == "57998")
                {
                    Console.WriteLine(JsonConvert.SerializeObject(_userService.UserApplicationContext));

                    if (_userService.UserApplicationContext.UserRole.RoleId == RoleConstants.Sardaftar)
                    {
                        if (_userService.UserApplicationContext.BranchAccess.IsBranchOwner == "false" &&
                            _userService.UserApplicationContext.BranchAccess.IssueDocAccess != "true")
                        {
                            apiResult.message.Add("با توجه به حکم ابلاغ صادر شده، شما مجاز به تنظیم اسناد نمی باشید.");
                            return apiResult;
                        }
                    }
                }
                if (!SignRequestRelatedPersonBusinessRule.CheckLoopInPersonRelated(signRequest.SignRequestPersonRelateds))
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("در گراف وابستگی اشخصاص حلقه به وجود آمده است .");
                    return apiResult;
                }
                ApiResult<GetPersonPhotoListViewModel> personalImages = new();
                personalImages.IsSuccess = false;
                GetPersonPhotoListFromSabteAhvalServiceInput getPerson = new();
                getPerson.Persons = signRequest.SignRequestPeople.Where(x => x.IsIranian=="1").Select(x => new PersonPhotoListItem() { NationalNo = x.NationalNo, BirthDate = x.BirthDate }).ToList();
                getPerson.MainObjectId = signRequestId.ToString();

                if (getPerson.Persons.Count > 0)
                    personalImages = await _mediator.Send(getPerson, cancellationToken);


                foreach (var item in signRequest.SignRequestPeople.OrderBy(x => x.RowNo).ToList())
                {
                    if (item.PersonType == "1" && item.IsAlive == "1")
                    {

                        var addingPerson = SignRequestMapper.ToFingerprintViewModel(item);
                        addingPerson.IsTFARequired = false;

                        if (personalImages.IsSuccess)
                        {
                            var personalImage = personalImages.Data.PersonsData.Where(x => x.NationalNo == item.NationalNo).FirstOrDefault();
                            addingPerson.PersonalImage = personalImage?.PersonalImage == null ? null : Convert.ToBase64String(personalImage.PersonalImage);
                        }

                        if (item.IsOriginal == "1")
                            addingPerson.PersonPost = "متقاضی";

                        var personRelated = signRequest.SignRequestPersonRelateds.Where(x => x.AgentPersonId == item.Id).OrderBy(x => x.RowNo).ToList();
                        foreach (var personRelatedItem in personRelated)
                        {
                            if (!addingPerson.PersonPost.IsNullOrWhiteSpace())
                                addingPerson.PersonPost += " و ";
                            addingPerson.PersonPost += personRelatedItem.AgentType.Title + " " + signRequest.SignRequestPeople.
                                Where(x => x.Id == personRelatedItem.MainPersonId).Select(x => x.Name + " " + x.Family).FirstOrDefault();
                        }
                        result.Add(addingPerson);




                    }
                }
                var notCheckPersonList = new List<Guid>();
                foreach (var item in signRequest.SignRequestPersonRelateds)
                {
                    switch (item.AgentTypeId)
                    {
                        case "2":
                            notCheckPersonList.Add(item.MainPersonId);
                            break;
                        case "3":
                            notCheckPersonList.Add(item.MainPersonId);
                            break;
                        case "5":
                            notCheckPersonList.Add(item.MainPersonId);
                            break;
                        case "7":
                            notCheckPersonList.Add(item.MainPersonId);
                            break;
                        case "14":
                            notCheckPersonList.Add(item.MainPersonId);
                            break;
                        case "18":
                            notCheckPersonList.Add(item.MainPersonId);
                            break;
                        case "10":
                            if (item.ReliablePersonReasonId == "7")
                                notCheckPersonList.Add(item.MainPersonId);
                            break;
                        default:
                            break;
                    }
                }
                notCheckPersonList = notCheckPersonList.Distinct().ToList();
                foreach (var item in notCheckPersonList)
                {
                    result.Remove(result.First(x => x.PersonId == item.ToString()));
                }
                message = SignRequestPersonBusinessRule.CheckSana(signRequest.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;

                    apiResult.message.Add(message);
                    return apiResult;
                }
                message = SignRequestPersonBusinessRule.CheckShahkar(signRequest.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;

                    apiResult.message.Add(message);
                    return apiResult;
                }
                message = SignRequestPersonBusinessRule.CheckAmlakEskan(signRequest.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
                if (!message.IsNullOrWhiteSpace())
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(message);
                    return apiResult;
                }
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("گواهی امضا مربوطه پیدا نشد");
            }
            return apiResult;
        }
    }
}
