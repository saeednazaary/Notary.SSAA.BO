using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    public class DealSummaryObjectConvertHelper
    {
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeriDaftarRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IRepository<EstateOwnershipType> _estateOwnershipRepository;
        private readonly IRepository<EstateTransitionType> _estateTransitionRepository;
        private readonly IRepository<DealSummaryTransferType> _dealSummaryTransferTypeRepository;
        private readonly IRepository<DealsummaryUnrestrictionType> _dealsummaryUnrestrictionTypeRepository;
        private readonly IRepository<DealsummaryPersonRelateType> _dealsummaryPersonRelateTypeRepository;
        private readonly IRepository<EstateInquiryPerson> _estateInquiryPersonRepository;
        private readonly IRepository<DealSummaryPerson> _dealSummaryPersonRepository;
        private readonly IMediator _mediator;
        public DealSummaryObjectConvertHelper(IMediator mediator,
         IEstateSectionRepository estateSectionRepository, IEstateInquiryRepository estateInquiryRepository, IEstateSeriDaftarRepository estateSeriDaftarRepository, IEstateSubSectionRepository estateSubSectionRepository
        , IRepository<EstateOwnershipType> estateOwnershipRepository, IRepository<EstateTransitionType> estateTransitionRepository
        , IRepository<DealSummaryTransferType> dealSummaryTransferTypeRepository
        , IRepository<DealsummaryUnrestrictionType> dealsummaryUnrestrictionTypeRepository
        , IRepository<DealsummaryPersonRelateType> dealsummaryPersonRelateTypeRepository,
         IRepository<EstateInquiryPerson> estateInquiryPersonRepository,
         IRepository<DealSummaryPerson> dealSummaryPersonRepository
        )
        {
            _estateInquiryRepository = estateInquiryRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSeriDaftarRepository = estateSeriDaftarRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _estateOwnershipRepository = estateOwnershipRepository;
            _estateTransitionRepository = estateTransitionRepository;
            _dealsummaryPersonRelateTypeRepository = dealsummaryPersonRelateTypeRepository;
            _dealSummaryTransferTypeRepository = dealSummaryTransferTypeRepository;
            _dealsummaryUnrestrictionTypeRepository = dealsummaryUnrestrictionTypeRepository;
            _estateInquiryPersonRepository = estateInquiryPersonRepository;
            _dealSummaryPersonRepository = dealSummaryPersonRepository;
            _mediator = mediator;
        }
        public async Task<List<DSUDealSummaryObject>> CorrectInput(List<DSUDealSummaryObject> DsuDealSummaryList, CancellationToken cancellationToken)
        {
            var DsuDealSummary = DsuDealSummaryList.Where(x => x.InputCorrectionIsDone == 0).ToList();
            if (DsuDealSummary.Count == 0) return DsuDealSummaryList;

            var inquiryIdList = DsuDealSummary.Select(x => x.ESTEstateInquiryId).ToList();
            var inquiryList = new List<EstateInquiry>();
            if (inquiryIdList.Count > 0)
            {
                var inquiryGuIdList = inquiryIdList.Distinct().Select(x => x.ToGuid()).ToList();
                inquiryList = await _estateInquiryRepository.TableNoTracking.Where(x => inquiryGuIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            var sectionIdList = DsuDealSummary.Select(x => x.SectionId).ToList();
            var sectionList = new List<EstateSection>();
            if (sectionIdList.Count > 0)
            {
                sectionIdList = sectionIdList.Distinct().ToList();
                sectionList = await _estateSectionRepository.TableNoTracking.Where(x => sectionIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            var subSectionIdList = DsuDealSummary.Select(x => x.SubsectionId).ToList();
            var subSectionList = new List<EstateSubsection>();
            if (subSectionIdList.Count > 0)
            {
                subSectionIdList = subSectionIdList.Distinct().ToList();
                subSectionList = await _estateSubSectionRepository.TableNoTracking.Where(x => subSectionIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            var seriDaftarIdList = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.BSTSeriDaftarId)).Select(x => x.BSTSeriDaftarId).ToList();
            var seridaftarList = new List<EstateSeridaftar>();
            if (seriDaftarIdList.Count > 0)
            {
                seriDaftarIdList = seriDaftarIdList.Distinct().ToList();
                seridaftarList = await _estateSeriDaftarRepository.TableNoTracking.Where(x => seriDaftarIdList.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            var ownrshipTypeIdList = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.DSUOwnerShipTypeId)).Select(x => x.DSUOwnerShipTypeId).ToList();
            var ownershipTypeList = new List<EstateOwnershipType>();
            if (ownrshipTypeIdList.Count > 0)
            {
                ownrshipTypeIdList = ownrshipTypeIdList.Distinct().ToList();
                ownershipTypeList = await _estateOwnershipRepository.TableNoTracking.Where(x => ownrshipTypeIdList.Contains(x.Id) || ownrshipTypeIdList.Contains(x.LegacyId)).ToListAsync(cancellationToken);
            }

            var trantiosionTypeIdList = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.DSUTransitionCaseId)).Select(x => x.DSUTransitionCaseId).ToList();
            var transisionTypeList = new List<EstateTransitionType>();
            if (trantiosionTypeIdList.Count > 0)
            {
                trantiosionTypeIdList = trantiosionTypeIdList.Distinct().ToList();
                transisionTypeList = await _estateTransitionRepository.TableNoTracking.Where(x => trantiosionTypeIdList.Contains(x.Id) || trantiosionTypeIdList.Contains(x.LegacyId)).ToListAsync(cancellationToken);
            }

            var measurmentUnitIdList = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.AmountUnitId)).Select(x => x.AmountUnitId).ToList();
            measurmentUnitIdList.AddRange(DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.TimeUnitId)).Select(x => x.TimeUnitId).ToList());
            var measurmentUnitTypeList = new MeasurementUnitTypeByIdViewModel();
            if (measurmentUnitIdList.Count > 0)
            {
                measurmentUnitTypeList = await GetMeasurementUnitTypes(measurmentUnitIdList.Distinct().ToArray(), cancellationToken);
            }

            var organizationIdList = DsuDealSummary.Select(x => x.DealSummeryIssuerId).ToList();
            var lst = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.unrestrictedOrganizationId)).Select(x => x.unrestrictedOrganizationId).ToList();
            if (lst.Count > 0)
                organizationIdList.AddRange(lst);
            var organizationList = new OrganizationViewModel();
            if (organizationIdList.Count > 0)
            {
                organizationList = await GetOrganizationByScriptoriumId(organizationIdList.Distinct().ToArray(), cancellationToken);
            }

            organizationIdList = DsuDealSummary.Select(x => x.DealSummeryIssueeId).ToList();
            if (organizationIdList.Count > 0)
            {
                var orgList = await GetOrganizationByUnitId(organizationIdList.Distinct().ToArray(), cancellationToken);
                organizationList.OrganizationList.AddRange(orgList.OrganizationList);
            }

            var geoIdList = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.GeoLocationId)).Select(x => Convert.ToInt32(x.GeoLocationId)).ToList();
            var personList = DsuDealSummary.SelectMany(x => x.TheDSURealLegalPersonList);

            geoIdList.AddRange(personList.Where(x => !string.IsNullOrWhiteSpace(x.IssuePlaceId)).Select(x => Convert.ToInt32(x.IssuePlaceId)));

            geoIdList.AddRange(personList.Where(x => !string.IsNullOrWhiteSpace(x.BirthdateId)).Select(x => Convert.ToInt32(x.BirthdateId)));

            geoIdList.AddRange(personList.Where(x => !string.IsNullOrWhiteSpace(x.NationalityId)).Select(x => Convert.ToInt32(x.NationalityId)));


            geoIdList.AddRange(personList.Where(x => !string.IsNullOrWhiteSpace(x.CityId)).Select(x => Convert.ToInt32(x.CityId)));

            var geoList = new GetGeolocationByIdViewModel();
            if (geoIdList.Count > 0)
            {
                geoList = await GetGeoLocationById(geoIdList.Distinct().ToArray(), cancellationToken);
            }

            var transferTypeIdList = DsuDealSummary.Select(x => x.DSUTransferTypeId).ToList();
            var transfareTypeList = new List<DealSummaryTransferType>();
            if (transferTypeIdList.Count > 0)
            {
                transferTypeIdList = transferTypeIdList.Distinct().ToList();
                transfareTypeList = await _dealSummaryTransferTypeRepository.TableNoTracking.Where(x => transferTypeIdList.Contains(x.Id) || transferTypeIdList.Contains(x.LegacyId)).ToListAsync(cancellationToken);
            }

            var unrestrictionTypeIdList = DsuDealSummary.Where(x => !string.IsNullOrWhiteSpace(x.DSURemoveRestirctionTypeId)).Select(x => x.DSURemoveRestirctionTypeId).ToList();
            var unrestrictionTypeList = new List<DealsummaryUnrestrictionType>();
            if (unrestrictionTypeIdList.Count > 0)
            {
                unrestrictionTypeIdList = unrestrictionTypeIdList.Distinct().ToList();
                unrestrictionTypeList = await _dealsummaryUnrestrictionTypeRepository.TableNoTracking.Where(x => unrestrictionTypeIdList.Contains(x.Id) || unrestrictionTypeIdList.Contains(x.LegacyId)).ToListAsync(cancellationToken);
            }

            var relationKindIdList = DsuDealSummary.SelectMany(x => x.TheDSURealLegalPersonList).ToList().Where(x => !string.IsNullOrWhiteSpace(x.DSURelationKindId)).Select(x => x.DSURelationKindId).ToList();
            var relationKindList = new List<DealsummaryPersonRelateType>();
            if (relationKindIdList.Count > 0)
            {
                relationKindIdList = relationKindIdList.Distinct().ToList();
                relationKindList = await _dealsummaryPersonRelateTypeRepository.TableNoTracking.Where(x => relationKindIdList.Contains(x.Id) || relationKindIdList.Contains(x.LegacyId)).ToListAsync(cancellationToken);
            }

            foreach (var item in DsuDealSummary)
            {

                if (!string.IsNullOrWhiteSpace(item.BasicAppendant))
                    item.BasicAppendant = item.BasicAppendant == "1" ? "true" : "false";
                if (!string.IsNullOrWhiteSpace(item.SecondaryAppendant))
                    item.SecondaryAppendant = item.SecondaryAppendant == "1" ? "true" : "false";
                if (!string.IsNullOrWhiteSpace(item.AmountUnitId))
                    item.AmountUnitId = measurmentUnitTypeList.MesurementUnitTypeList.Where(x => x.Id == item.AmountUnitId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.BSTSeriDaftarId))
                    item.BSTSeriDaftarId = seridaftarList.Where(x => x.Id == item.BSTSeriDaftarId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.DealSummeryIssueeId))
                    item.DealSummeryIssueeId = organizationList.OrganizationList.Where(x => x.Id == item.DealSummeryIssueeId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.DealSummeryIssuerId))
                    item.DealSummeryIssuerId = organizationList.OrganizationList.Where(x => x.Id == item.DealSummeryIssuerId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.DSUOwnerShipTypeId))
                    item.DSUOwnerShipTypeId = ownershipTypeList.Where(x => x.Id == item.DSUOwnerShipTypeId || x.LegacyId == item.DSUOwnerShipTypeId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.DSURemoveRestirctionTypeId))
                    item.DSURemoveRestirctionTypeId = unrestrictionTypeList.Where(x => x.Id == item.DSURemoveRestirctionTypeId || x.LegacyId == item.DSURemoveRestirctionTypeId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.DSUTransferTypeId))
                    item.DSUTransferTypeId = transfareTypeList.Where(x => x.Id == item.DSUTransferTypeId || x.LegacyId == item.DSUTransferTypeId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.DSUTransitionCaseId))
                    item.DSUTransitionCaseId = transisionTypeList.Where(x => x.Id == item.DSUTransitionCaseId || x.LegacyId == item.DSUTransitionCaseId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.ESTEstateInquiryId))
                {
                    var inquiry = inquiryList.Where(x => x.Id == item.ESTEstateInquiryId.ToGuid()).First();
                    item.ESTEstateInquiryId = !string.IsNullOrWhiteSpace(inquiry.LegacyId) ? inquiry.LegacyId : inquiry.Id.ToString().ToUpper().Replace("-", "").Replace("_", "");
                }
                if (!string.IsNullOrWhiteSpace(item.GeoLocationId))
                    item.GeoLocationId =  geoList.GeolocationList.Where(x => x.Id == item.GeoLocationId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.SectionId))
                    item.SectionId = sectionList.Where(x => x.Id == item.SectionId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.SubsectionId))
                    item.SubsectionId = subSectionList.Where(x => x.Id == item.SubsectionId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.TimeUnitId))
                    item.TimeUnitId =  measurmentUnitTypeList.MesurementUnitTypeList.Where(x => x.Id == item.TimeUnitId).First().LegacyId;
                if (!string.IsNullOrWhiteSpace(item.unrestrictedOrganizationId))
                    item.unrestrictedOrganizationId = organizationList.OrganizationList.Where(x => x.Id == item.unrestrictedOrganizationId).First().LegacyId;
                foreach (var person in item.TheDSURealLegalPersonList)
                {
                    if (person.IsInquiryPerson == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(person.RelatedPersonId))
                        {
                            var p = await _estateInquiryPersonRepository.GetByIdAsync(cancellationToken, person.RelatedPersonId.ToGuid());
                            if (p != null)
                            {
                                if (!string.IsNullOrWhiteSpace(p.OtherLegacyId))
                                    person.RelatedPersonId = p.OtherLegacyId;
                                else
                                    person.RelatedPersonId = person.RelatedPersonId.ToUpper().Replace("-", "").Replace("_", "");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(person.RelatedPersonId))
                        {
                            var dsp = await _dealSummaryPersonRepository.GetByIdAsync(cancellationToken, person.RelatedPersonId.ToGuid());
                            if (dsp != null)
                            {
                                if (!string.IsNullOrWhiteSpace(dsp.LegacyId))
                                    person.RelatedPersonId = dsp.LegacyId;
                                else
                                    person.RelatedPersonId = person.RelatedPersonId.ToUpper().Replace("-", "").Replace("_", "");
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(person.BirthdateId))
                        person.BirthdateId = geoList.GeolocationList.Where(x => x.Id == person.BirthdateId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(person.CityId))
                        person.CityId = geoList.GeolocationList.Where(x => x.Id == person.CityId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(person.DSURelationKindId))
                        person.DSURelationKindId = relationKindList.Where(x => x.Id == person.DSURelationKindId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(person.IssuePlaceId))
                        person.IssuePlaceId = geoList.GeolocationList.Where(x => x.Id == person.IssuePlaceId).First().LegacyId;
                    if (!string.IsNullOrWhiteSpace(person.NationalityId))
                        person.NationalityId = geoList.GeolocationList.Where(x => x.Id == person.NationalityId).First().LegacyId;

                }
                item.InputCorrectionIsDone = 1;
            }

            return DsuDealSummary;
        }

        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypes(string[] Id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new MeasurementUnitTypeByIdQuery(Id), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return new MeasurementUnitTypeByIdViewModel();
            }

        }
        public async Task<OrganizationViewModel> GetOrganizationByScriptoriumId(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganizationByScriptoriumIdQuery(scriptoriumId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return new OrganizationViewModel();
            }

        }
        public async Task<OrganizationViewModel> GetOrganizationByUnitId(string[] unitId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganizationByUnitIdQuery(unitId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return new OrganizationViewModel();
            }

        }
        public async Task<GetGeolocationByIdViewModel> GetGeoLocationById(int[] id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetGeolocationByIdQuery(id) { FetchGeolocationOfIran = false }, cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return new GetGeolocationByIdViewModel();
            }

        }
    }
}
