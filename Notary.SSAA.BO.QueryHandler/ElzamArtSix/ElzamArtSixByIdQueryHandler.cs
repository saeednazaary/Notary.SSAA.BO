using MediatR;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.DataTransferObject.Mappers.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.Queries.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixPerson;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.ElzamArtSix
{
    public class ElzamArtSixByIdQueryHandler : BaseQueryHandler<GetElzamArtSixByIdQuery, ApiResult<ElzamArtSixViewModel>>
    {
        private readonly IElzamArtSixRepository _ElzamArtSixRepository;

        public ElzamArtSixByIdQueryHandler(IMediator mediator, IUserService userService, IElzamArtSixRepository ElzamArtSixRepository)
      : base(mediator, userService)
        {
            _ElzamArtSixRepository = ElzamArtSixRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixRepository));
        }
        protected override bool HasAccess(GetElzamArtSixByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar);
        }

        protected override async Task<ApiResult<ElzamArtSixViewModel>> RunAsync(GetElzamArtSixByIdQuery request, CancellationToken cancellationToken)
        {
            ElzamArtSixViewModel result = new();
            ApiResult<ElzamArtSixViewModel> apiResult = new();
            Domain.Entities.SsrArticle6Inq ElzamArtSix = await _ElzamArtSixRepository.GetElzamArtSixGetById(request.ElzamArtSixId, cancellationToken);


            if (ElzamArtSix == null)
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("خدمت مربوطه پیدا نشد");
                return apiResult;
            }
            result = ElzamArtSixMapper.ToViewModelElzamArtSix(ElzamArtSix);


            if (result.ElzamArtSixSellerPerson != null)
            {
                result.ElzamArtSixSellerPerson.PersonalImage = await GetPersonPic(result.ElzamArtSixSellerPerson, ElzamArtSix.Id.ToString(), cancellationToken);
            }

            if (result.ElzamArtSixBuyerPerson != null)
            {
                result.ElzamArtSixBuyerPerson.PersonalImage = await GetPersonPic(result.ElzamArtSixBuyerPerson, ElzamArtSix.Id.ToString(), cancellationToken);
            }

            apiResult.Data = result;
            return apiResult;

        }

        protected async Task<string> GetPersonPic(ElzamArtSixPersonViewModel ElzamArtSixPerson, string ElzamArtSixId, CancellationToken cancellationToken)
        {
            GetPersonPhotoListFromSabteAhvalServiceInput getPerson = new();
            PersonPhotoListItem personPhoto = new PersonPhotoListItem();
            personPhoto.NationalNo = ElzamArtSixPerson.NationalityCode;
            personPhoto.BirthDate = ElzamArtSixPerson.BirthDate;
            getPerson.MainObjectId = ElzamArtSixId;
            getPerson.Persons = new List<PersonPhotoListItem>() { personPhoto };
            ApiResult<GetPersonPhotoListViewModel> personalImages = await _mediator.Send(getPerson, cancellationToken);
            if (personalImages.IsSuccess)
            {
                var personalImage = personalImages.Data.PersonsData.Where(x => x.NationalNo == ElzamArtSixPerson.NationalityCode).FirstOrDefault();
                return personalImage != null && personalImage.PersonalImage != null ? Convert.ToBase64String(personalImage.PersonalImage) : null;
            }
            else { return null; }
        }
    }
}