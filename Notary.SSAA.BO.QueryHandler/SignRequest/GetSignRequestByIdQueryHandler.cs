using MediatR;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class GetSignRequestByIdQueryHandler : BaseQueryHandler<GetSignRequestByIdQuery, ApiResult<SignRequestViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;

        public GetSignRequestByIdQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, IApplicationIdGeneratorService applicationIdGeneratorService)
            : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
        }
        protected override bool HasAccess(GetSignRequestByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar)|| userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.SSAAAdmin)|| userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignRequestViewModel>> RunAsync(GetSignRequestByIdQuery request, CancellationToken cancellationToken)
        {

            SignRequestViewModel result = new();
            ApiResult<SignRequestViewModel> apiResult = new();
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            Domain.Entities.SignRequest signRequest = await _signRequestRepository.GetSignRequest(signRequestId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);


            if (signRequest != null)
            {


                if (signRequest.IsCostPaid == "3")
                {
                    UpdateSignRequestPaymentStateCommand paymentStateCommand = new();
                    paymentStateCommand.SignRequestId = signRequest.Id.ToString();
                    paymentStateCommand.InquiryMode = true;
                    var paymentInquiryResult = await _mediator.Send(paymentStateCommand, cancellationToken);
                    if (paymentInquiryResult.IsSuccess)
                    {
                        signRequest.IsCostPaid = paymentInquiryResult.Data.IsCostPaid;
                        signRequest.PayCostDate = paymentInquiryResult.Data.PayCostDate;
                        signRequest.PayCostTime = paymentInquiryResult.Data.PayCostTime;
                        signRequest.ReceiptNo = paymentInquiryResult.Data.ReceiptNo;
                        signRequest.BillNo = paymentInquiryResult.Data.BillNo;
                        signRequest.SumPrices = paymentInquiryResult.Data.SumPrices.ToInt();
                        signRequest.PaymentType = paymentInquiryResult.Data.PaymentType;

                    }
                }

                result = SignRequestMapper.ToViewModel(signRequest);
                result.SignRequestId= _applicationIdGeneratorService.EncryptGuid(signRequest.Id);
                ApiResult<GetPersonPhotoListViewModel> personalImages = new();
                personalImages.IsSuccess = false;
                GetPersonPhotoListFromSabteAhvalServiceInput getPerson = new();
                getPerson.Persons = result.SignRequestPersons.Where(x => x.IsPersonIranian).Select(x =>new PersonPhotoListItem() {NationalNo= x.PersonNationalNo ,BirthDate= x.PersonBirthDate}).ToList();
                getPerson.MainObjectId=signRequestId.ToString();
                if (getPerson.Persons.Count > 0)
                    personalImages = await _mediator.Send(getPerson, cancellationToken);

                foreach (SignRequestPersonViewModel item in result.SignRequestPersons)
                {
                    if (personalImages?.IsSuccess == true)
                    {
                        var personalImage = personalImages.Data.PersonsData.Where(x => x.NationalNo == item.PersonNationalNo).FirstOrDefault();
                        item.PersonalImage = personalImage?.PersonalImage == null ? null : Convert.ToBase64String(personalImage.PersonalImage);

                    }
                    if (item.IsPersonOriginal)
                        item.PersonPost = "متقاضی";

                    var personRelated = signRequest.SignRequestPersonRelateds.Where(x => x.AgentPersonId == Guid.Parse(item.PersonId)).OrderBy(x => x.RowNo).ToList();
                    foreach (var personRelatedItem in personRelated)
                    {
                        if (!item.PersonPost.IsNullOrWhiteSpace())
                            item.PersonPost += " و ";
                        item.PersonPost += personRelatedItem.AgentType.Title + " " + signRequest.SignRequestPeople.
                                Where(x => x.Id == personRelatedItem.MainPersonId).Select(x => x.Name + " " + x.Family).FirstOrDefault();
                    }

                }
                result.SignRequestPersons = result.SignRequestPersons
                    .OrderBy(x => int.TryParse(x.RowNo, out var n) ? n : int.MaxValue)
                    .ToList();

                result.SignRequestRelatedPersons = result.SignRequestRelatedPersons
                    .OrderBy(x => int.TryParse(x.RowNo, out var n) ? n : int.MaxValue)
                    .ToList();

                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("گواهی امضا مربوطه پیدا نشد");
            }
            return apiResult; 
        }

    }
}
