using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetEstateOwnersByInquiryIdQueryHandler : BaseQueryHandler<GetEstateOwnersByInquiryIdQuery, ApiResult<List<DSURealLegalPersonObject>>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IDealSummaryPersonRepository _dealSummaryPersonRepository;


        public GetEstateOwnersByInquiryIdQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository,
            IDealSummaryPersonRepository dealSummaryPersonRepository)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _dealSummaryPersonRepository = dealSummaryPersonRepository;
        }
        protected override bool HasAccess(GetEstateOwnersByInquiryIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<DSURealLegalPersonObject>>> RunAsync(GetEstateOwnersByInquiryIdQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryViewModel result = new();
            ApiResult<List<DSURealLegalPersonObject>> apiResult = new();

            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
           
            if (estateInquiry != null)
            {
                var resultList = new List<DSURealLegalPersonObject>();
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);
                var owners = await _dealSummaryPersonRepository.GetEstateOwnersByDealSummaryInfo(request.EstateInquiryId, cancellationToken);
                foreach (var person in owners)
                {
                    var dealSummaryPerson = new DSURealLegalPersonObject
                    {
                        Address = person.Address,
                        BirthDate = person.BirthDate,
                        BirthdateId = person.BirthPlaceId.HasValue ? person.BirthPlaceId.ToString() : null,
                        CityId = person.CityId.HasValue ? person.CityId.ToString() : null,
                        Description = person.ConditionText,
                        DSURelationKindId = "100",
                        Family = person.Family,
                        FatherName = person.FatherName,
                        IdentificationNo = person.IdentityNo,
                        IsInquiryPerson = 0,
                        IssuePlaceId = person.IssuePlaceId.HasValue ? person.IssuePlaceId.ToString() : null,
                        Name = person.Name,
                        NationalCode = person.NationalityCode,
                        NationalityId = person.NationalityId.HasValue ? person.NationalityId.ToString() : null,
                        OctantQuarter = person.OctantQuarter,
                        OctantQuarterPart = person.OctantQuarterPart,
                        OctantQuarterTotal = person.OctantQuarterTotal,
                        personType = person.PersonType == "1" ? 1 : 0,
                        PostalCode = person.PostalCode,
                        RelatedPersonId = person.Id.ToString(),
                        Seri = person.Seri,
                        Serial = !string.IsNullOrWhiteSpace(person.SerialNo) ? Convert.ToInt32(person.SerialNo) : null,
                        //SeriAlpha = person.SeriAlpha,
                        sex = person.SexType == "1" ? 2 : 1,
                        ShareContext = person.ShareText,
                        SharePart = person.SharePart.HasValue ? person.SharePart : null,
                        ShareTotal = person.ShareTotal.HasValue ? person.ShareTotal : null,
                        //criptoriumId = person.ScriptoriumId
                    };
                    if (person.IsInquiryPerson == EstateConstant.BooleanConstant.True)
                    {
                        dealSummaryPerson.RelatedPersonId = person.DealSummary.EstateInquiry.EstateInquiryPeople.First().Id.ToString();
                        dealSummaryPerson.IsInquiryPerson = 1;
                    }
                    resultList.Add(dealSummaryPerson);
                }
                if (owners.Count == 0)
                {
                    var person = estateInquiry.EstateInquiryPeople.First();
                    var dealSummaryPerson = new DSURealLegalPersonObject
                    {
                        Address = person.Address,
                        BirthDate = person.BirthDate,
                        BirthdateId = person.BirthPlaceId.HasValue ? person.BirthPlaceId.ToString() : null,
                        CityId = person.CityId.HasValue ? person.CityId.ToString() : null,
                        Description = "",
                        DSURelationKindId = "100",
                        Family = person.Family,
                        FatherName = person.FatherName,
                        IdentificationNo = person.IdentityNo,
                        IsInquiryPerson = 1,
                        IssuePlaceId = person.IssuePlaceId.HasValue ? person.IssuePlaceId.ToString() : null,
                        Name = person.Name,
                        NationalCode = person.NationalityCode,
                        NationalityId = person.NationalityId.HasValue ? person.NationalityId.ToString() : null,
                        OctantQuarter = "",
                        OctantQuarterPart = "",
                        OctantQuarterTotal = "",
                        personType = person.PersonType == "1" ? 1 : 0,
                        PostalCode = person.PostalCode,
                        RelatedPersonId = person.Id.ToString(),
                        Seri = person.Seri,
                        Serial = !string.IsNullOrWhiteSpace(person.SerialNo) ? Convert.ToInt32(person.SerialNo) : null,
                        //SeriAlpha = person.SeriAlpha,
                        sex = person.SexType == "1" ? 2 : 1,
                        ShareContext = person.ShareText,
                        SharePart = person.SharePart.HasValue ? person.SharePart : null,
                        ShareTotal = person.ShareTotal.HasValue ? person.ShareTotal : null,
                        //ScriptoriumId = person.ScriptoriumId
                    };

                    resultList.Add(dealSummaryPerson);
                }

                apiResult.Data = resultList;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام ملک مربوطه یافت نشد");
            }
            return apiResult;
        }

    }
}
