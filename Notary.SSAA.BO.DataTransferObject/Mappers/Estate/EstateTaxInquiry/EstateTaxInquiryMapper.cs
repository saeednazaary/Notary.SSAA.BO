using Mapster;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateTaxInquiry
{
    public class EstateTaxInquiryMapper
    {
        public static Domain.Entities.EstateTaxInquiry ViewModelToEntity(CreateEstateTaxInquiryCommand viewModel)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<CreateEstateTaxInquiryCommand, Domain.Entities.EstateTaxInquiry>()
              .Map(dest => dest.ApartmentArea, src => src.InquiryApartmentArea)
              .Map(dest => dest.ArsehArea, src => src.InquiryArsehArea)
              .Map(dest => dest.Avenue, src => src.InquiryAvenue)
              .Map(dest => dest.BuildingOld, src => src.InquiryBuildingOld)
              //.Map(dest => dest.BuildingType, src => src.InquiryBUILDINGTYPE)
              .Map(dest => dest.BuildingUsingTypeId, src => src.InquiryBuildingUsingTypeId != null && src.InquiryBuildingUsingTypeId.Count > 0 ? src.InquiryBuildingUsingTypeId.First() : null)
              //.Map(dest => dest.CertificateFile, src => src.InquiryCERTIFICATEFILE)
              //.Map(dest => dest.CertificateHtml, src => src.InquiryCERTIFICATEHTML)
              .Map(dest => dest.CertificateNo, src => src.InquiryCertificateNo)
              .Map(dest => dest.CessionDate, src => src.InquiryCessionDate)
              .Map(dest => dest.CessionPrice, src => src.InquiryCessionPrice)
              //.Map(dest => dest.CreateDate, src => src.InquiryCreateDate)
              //.Map(dest => dest.CreateTime, src => src.InquiryCreateTime)
              .Map(dest => dest.EstateAddress, src => src.InquiryEstateAddress)
              //.Map(dest => dest.EstateInquiryId, src => src.EstateInquiryId != null && src.EstateInquiryId.Count > 0 ? src.EstateInquiryId.First() : null)
              .Map(dest => dest.EstatePostCode, src => src.InquiryEstatePostCode)
              .Map(dest => dest.EstateSectionId, src => src.InquiryEstateSectionId.First())
              .Map(dest => dest.EstateSector, src => src.InquiryEstateSector)
              .Map(dest => dest.EstateTaxCityId, src => src.InquiryEstateTaxCityId.First())
              .Map(dest => dest.EstateTaxCountyId, src => src.InquiryEstateTaxCountyId.First())
              .Map(dest => dest.EstateTaxInquiryBuildingConstructionStepId, src => src.InquiryBuildingConstructionStepId != null && src.InquiryBuildingConstructionStepId.Count > 0 ? src.InquiryBuildingConstructionStepId.First() : null)
              .Map(dest => dest.EstateTaxInquiryBuildingStatusId, src => src.InquiryBuildingStatusId != null && src.InquiryBuildingStatusId.Count > 0 ? src.InquiryBuildingStatusId.First() : null)
              .Map(dest => dest.EstateTaxInquiryBuildingTypeId, src => src.InquiryBuildingTypeId != null && src.InquiryBuildingTypeId.Count > 0 ? src.InquiryBuildingTypeId.First() : null)
              .Map(dest => dest.EstateTaxInquiryDocumentRequestTypeId, src => src.InquiryDocumentRequestTypeId.First())
              .Map(dest => dest.EstateTaxInquiryFieldTypeId, src => src.InquiryFieldTypeId.First())
              .Map(dest => dest.EstateTaxInquiryTransferTypeId, src => src.InquiryTransferTypeId.First())
              .Map(dest => dest.EstateTaxUnitId, src => src.InquiryEstateTaxUnitId != null && src.InquiryEstateTaxUnitId.Count > 0 ? src.InquiryEstateTaxUnitId.First() : null)
              .Map(dest => dest.Estatebasic, src => src.InquiryEstatebasic)
              .Map(dest => dest.Estatesecondary, src => src.InquiryEstatesecondary)
              .Map(dest => dest.FieldUsingTypeId, src => src.InquiryFieldUsingTypeId.First())
              .Map(dest => dest.FkEstateTaxProvinceId, src => src.InquiryEstateTaxProvinceId.First())
              .Map(dest => dest.FloorNo, src => src.InquiryFloorNo)
              .Map(dest => dest.HasSpecialTrance, src => src.InquiryHasSpecialTrance.ToYesNo())
              .Map(dest => dest.HasSpecifiedTradingValue, src => src.InquiryHasSpecifiedTradingValue.ToYesNo())
              //.Map(dest => dest.IsActive, src => src.InquiryISACTIVE)
              .Map(dest => dest.IsFirstCession, src => src.InquiryIsFirstCession.ToYesNo())
              .Map(dest => dest.IsFirstDeal, src => src.InquiryIsFirstDeal.ToYesNo())
              .Map(dest => dest.IsGroundLevel, src => src.InquiryIsGroundLevel.ToYesNo())
              .Map(dest => dest.IsLicenceReady, src => src.InquiryIsLicenceReady.ToYesNo())
              .Map(dest => dest.IsMissingSeparateDocument, src => src.InquiryIsMissingSeparateDocument.ToYesNo())
              .Map(dest => dest.IsWornTexture, src => src.InquiryIsWornTexture.ToYesNo())
              //.Map(dest => dest.LastReceiveStatusDate, src => src.InquiryLastReceiveStatusDate)
              //.Map(dest => dest.LastReceiveStatusTime, src => src.InquiryLastReceiveStatusTime)
              //.Map(dest => dest.LASTSENDDATE, src => src.InquiryLASTSENDDATE)
              //.Map(dest => dest.LASTSENDTIME, src => src.InquiryLASTSENDTIME)
              .Map(dest => dest.LicenseDate, src => src.InquiryLicenseDate)
              .Map(dest => dest.LocationAssignRightDealCurrentValue, src => src.InquiryLocationAssignRightDealCurrentValue)
              .Map(dest => dest.LocationAssignRigthOwnershipTypeId, src => src.InquiryLocationAssignRigthOwnershipTypeId != null && src.InquiryLocationAssignRigthOwnershipTypeId.Count > 0 ? src.InquiryLocationAssignRigthOwnershipTypeId.First() : null)
              .Map(dest => dest.LocationAssignRigthUsingTypeId, src => src.InquiryLocationAssignRigthUsingTypeId != null && src.InquiryLocationAssignRigthUsingTypeId.Count > 0 ? src.InquiryLocationAssignRigthUsingTypeId.First() : null)
              //.Map(dest => dest.NO, src => src.InquiryNO)
              //.Map(dest => dest.NO2, src => src.InquiryNO2)
              .Map(dest => dest.EstateUnitId, src => src.InquiryEstateUnitId.First())
              .Map(dest => dest.EstateSubsectionId, src => src.InquiryEstateSubSectionId.First())
              .Map(dest => dest.BasicRemaining, src => src.InquiryEstateBasicRemaining.ToYesNo())
              .Map(dest => dest.SecondaryRemaining, src => src.InquiryEstateSecondaryRemaining.ToYesNo())
              .Map(dest => dest.PlateNo, src => src.InquiryPlateNo)
              .Map(dest => dest.PrevTransactionsAccordingToFacilitateRule, src => src.InquiryPrevTransactionsAccordingToFacilitateRule.ToYesNo())
              .Map(dest => dest.RenovationRelatedBlockNo, src => src.InquiryRenovationRelatedBlockNo)
              .Map(dest => dest.RenovationRelatedEstateNo, src => src.InquiryRenovationRelatedEstateNo)
              .Map(dest => dest.RenovationRelatedRowNo, src => src.InquiryRenovationRelatedRowNo)
              //.Map(dest => dest.ScriptoriumId, src => src.InquirySCRIPTORIUMID)
              .Map(dest => dest.SeparationProcessNo, src => src.InquirySeparationProcessNo)
              .Map(dest => dest.ShareOfOwnership, src => src.InquiryShareOfOwnership)
              //.Map(dest => dest.StatusDescription, src => src.InquiryStatusDescription)
              //.Map(dest => dest.TaxAmount, src => src.InquiryTaxAmount)
              //.Map(dest => dest.TaxBillHtml, src => src.InquiryTaxBillHtml)
              //.Map(dest => dest.TaxBillIdentity, src => src.InquiryTaxBillIdentity)
              //.Map(dest => dest.TaxPaymentIdentity, src => src.InquiryTaxPaymentIdentity)
              //.Map(dest => dest.TIMESTAMP, src => src.InquiryTIMESTAMP)
              .Map(dest => dest.TotalArea, src => src.InquiryTotalArea)
              .Map(dest => dest.TotalOwnershipShare, src => src.InquiryTotalOwnershipShare)
              //.Map(dest => dest.TrackingCode, src => src.InquiryTrackingCode)
              .Map(dest => dest.TranceWidth, src => src.InquiryTranceWidth)
              .Map(dest => dest.TransitionShare, src => src.InquiryTransitionShare)
              .Map(dest => dest.ValuebookletBlockNo, src => src.InquiryValuebookletBlockNo)
              .Map(dest => dest.ValuebookletRowNo, src => src.InquiryValuebookletRowNo)
              .Map(dest => dest.WorkCompletionCertificateDate, src => src.InquiryWorkCompletionCertificateDate);
            //.Map(dest => dest.WorkflowStatesId, src => src.InquiryWORKFLOWSTATESID)
            //.Map(dest => dest.ILM, src => src.InquiryILM)
            //.Map(dest => dest.LEGACYID, src => src.InquiryLEGACYID)
            //.Map(dest => dest.ShebaNo, src => src.InquiryShebaNo);
            config.Compile();

            var entity = viewModel.Adapt<Domain.Entities.EstateTaxInquiry>(config);
            if (viewModel.EstateInquiryId != null && viewModel.EstateInquiryId.Count > 0)
                entity.EstateInquiryId = viewModel.EstateInquiryId.First().ToGuid();
            Helper.NormalizeStringValues(entity, true);
            return entity;
        }


        public static EstateTaxInquiryViewModel EntityToViewModel(Domain.Entities.EstateTaxInquiry entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.EstateTaxInquiry, EstateTaxInquiryViewModel>()
               .Map(dest => dest.InquiryApartmentArea, src => src.ApartmentArea)
               .Map(dest => dest.InquiryArsehArea, src => src.ArsehArea)
               .Map(dest => dest.InquiryAvenue, src => src.Avenue)
               .Map(dest => dest.InquiryBuildingOld, src => src.BuildingOld)
               //.Map(dest => dest.BuildingType, src => src.InquiryBUILDINGTYPE)
               .Map(dest => dest.InquiryBuildingUsingTypeId, src => src.BuildingUsingTypeId != null ? new List<string>() { src.BuildingUsingTypeId } : new List<string>())
               //.Map(dest => dest.CertificateFile, src => src.InquiryCERTIFICATEFILE)
               //.Map(dest => dest.CertificateHtml, src => src.InquiryCERTIFICATEHTML)
               .Map(dest => dest.InquiryCertificateNo, src => src.CertificateNo)
               .Map(dest => dest.InquiryCessionDate, src => src.CessionDate)
               .Map(dest => dest.InquiryCessionPrice, src => src.CessionPrice)
               .Map(dest => dest.InquiryCreateDate, src => src.CreateDate)
               .Map(dest => dest.InquiryCreateTime, src => src.CreateTime)
               .Map(dest => dest.InquiryEstateAddress, src => src.EstateAddress)
               .Map(dest => dest.EstateInquiryId, src => src.EstateInquiryId != null ? new List<string>() { src.EstateInquiryId.ToString() } : new List<string>())
               .Map(dest => dest.EstateInquiryTimeStamp, src => src.EstateInquiry != null ? Convert.ToInt32(src.EstateInquiry.Timestamp) : 0)
               .Map(dest => dest.InquiryEstatePostCode, src => src.EstatePostCode)
               .Map(dest => dest.InquiryEstateSectionId, src => new List<string>() { src.EstateSectionId })
               .Map(dest => dest.InquiryEstateSector, src => src.EstateSector)
               .Map(dest => dest.InquiryEstateTaxCityId, src => new List<string>() { src.EstateTaxCityId })
               .Map(dest => dest.InquiryEstateTaxCountyId, src => new List<string>() { src.EstateTaxCountyId })
               .Map(dest => dest.InquiryBuildingConstructionStepId, src => src.EstateTaxInquiryBuildingConstructionStepId != null ? new List<string>() { src.EstateTaxInquiryBuildingConstructionStepId } : new List<string>())
               .Map(dest => dest.InquiryBuildingStatusId, src => src.EstateTaxInquiryBuildingStatusId != null ? new List<string>() { src.EstateTaxInquiryBuildingStatusId } : new List<string>())
               .Map(dest => dest.InquiryBuildingTypeId, src => src.EstateTaxInquiryBuildingTypeId != null ? new List<string>() { src.EstateTaxInquiryBuildingTypeId } : new List<string>())
               .Map(dest => dest.InquiryDocumentRequestTypeId, src => new List<string>() { src.EstateTaxInquiryDocumentRequestTypeId })
               .Map(dest => dest.InquiryFieldTypeId, src => new List<string> { src.EstateTaxInquiryFieldTypeId })
               .Map(dest => dest.InquiryTransferTypeId, src => new List<string>() { src.EstateTaxInquiryTransferTypeId })
               .Map(dest => dest.InquiryEstateTaxUnitId, src => src.EstateTaxUnitId != null ? new List<string>() { src.EstateTaxUnitId } : new List<string>())
               .Map(dest => dest.InquiryEstatebasic, src => src.Estatebasic)
               .Map(dest => dest.InquiryEstatesecondary, src => src.Estatesecondary)
               .Map(dest => dest.InquiryFieldUsingTypeId, src => new List<string>() { src.FieldUsingTypeId })
               .Map(dest => dest.InquiryEstateTaxProvinceId, src => new List<string>() { src.FkEstateTaxProvinceId })
               .Map(dest => dest.InquiryFloorNo, src => src.FloorNo)
               .Map(dest => dest.InquiryHasSpecialTrance, src => src.HasSpecialTrance.ToBoolean())
               .Map(dest => dest.InquiryHasSpecifiedTradingValue, src => src.HasSpecifiedTradingValue.ToBoolean())
               //.Map(dest => dest.IsActive, src => src.InquiryISACTIVE)
               .Map(dest => dest.InquiryIsFirstCession, src => src.IsFirstCession.ToBoolean())
               .Map(dest => dest.InquiryIsFirstDeal, src => src.IsFirstDeal.ToBoolean())
               .Map(dest => dest.InquiryIsGroundLevel, src => src.IsGroundLevel.ToBoolean())
               .Map(dest => dest.InquiryIsLicenceReady, src => src.IsLicenceReady.ToBoolean())
               .Map(dest => dest.InquiryIsMissingSeparateDocument, src => src.IsMissingSeparateDocument.ToBoolean())
               .Map(dest => dest.InquiryIsWornTexture, src => src.IsWornTexture.ToBoolean())
               .Map(dest => dest.InquiryLastReceiveStatusDate, src => src.LastReceiveStatusDate)
               .Map(dest => dest.InquiryLastReceiveStatusTime, src => src.LastReceiveStatusTime)
               .Map(dest => dest.InquiryLastSendDate, src => src.LastSendDate)
               .Map(dest => dest.InquiryLastSendTime, src => src.LastSendTime)
               .Map(dest => dest.InquiryLicenseDate, src => src.LicenseDate)
               .Map(dest => dest.InquiryLocationAssignRightDealCurrentValue, src => src.LocationAssignRightDealCurrentValue)
               .Map(dest => dest.InquiryLocationAssignRigthOwnershipTypeId, src => src.LocationAssignRigthOwnershipTypeId != null ? new List<string>() { src.LocationAssignRigthOwnershipTypeId } : new List<string>())
               .Map(dest => dest.InquiryLocationAssignRigthUsingTypeId, src => src.LocationAssignRigthUsingTypeId != null ? new List<string>() { src.LocationAssignRigthUsingTypeId } : new List<string>())
               .Map(dest => dest.InquiryNo, src => src.No)
               .Map(dest => dest.InquiryNo2, src => src.No2)
               .Map(dest => dest.InquiryEstateUnitId, src => new List<string>() { src.EstateUnitId })
               .Map(dest => dest.InquiryEstateSubSectionId, src => new List<string>() { src.EstateSubsectionId })
               .Map(dest => dest.InquiryEstateBasicRemaining, src => src.BasicRemaining.ToBoolean())
               .Map(dest => dest.InquiryEstateSecondaryRemaining, src => src.SecondaryRemaining.ToBoolean())
               .Map(dest => dest.InquiryPlateNo, src => src.PlateNo)
               .Map(dest => dest.InquiryPrevTransactionsAccordingToFacilitateRule, src => src.PrevTransactionsAccordingToFacilitateRule.ToBoolean())
               .Map(dest => dest.InquiryRenovationRelatedBlockNo, src => src.RenovationRelatedBlockNo)
               .Map(dest => dest.InquiryRenovationRelatedEstateNo, src => src.RenovationRelatedEstateNo)
               .Map(dest => dest.InquiryRenovationRelatedRowNo, src => src.RenovationRelatedRowNo)
               //.Map(dest => dest.ScriptoriumId, src => src.InquirySCRIPTORIUMID)
               .Map(dest => dest.InquirySeparationProcessNo, src => src.SeparationProcessNo)
               .Map(dest => dest.InquiryShareOfOwnership, src => src.ShareOfOwnership)
               .Map(dest => dest.InquiryStatusDescription, src => src.WorkflowStates.Description)
               .Map(dest => dest.InquiryTaxAmount, src => src.TaxAmount)
               //.Map(dest => dest.TaxBillHtml, src => src.InquiryTaxBillHtml)
               .Map(dest => dest.InquiryTaxBillIdentity, src => src.TaxBillIdentity)
               .Map(dest => dest.InquiryTaxPaymentIdentity, src => src.TaxPaymentIdentity)
               .Map(dest => dest.InquiryTimestamp, src => src.Timestamp)
               .Map(dest => dest.InquiryTotalArea, src => src.TotalArea)
               .Map(dest => dest.InquiryTotalOwnershipShare, src => src.TotalOwnershipShare)
               .Map(dest => dest.InquiryTrackingCode, src => src.TrackingCode)
               .Map(dest => dest.InquiryTranceWidth, src => src.TranceWidth)
               .Map(dest => dest.InquiryTransitionShare, src => src.TransitionShare)
               .Map(dest => dest.InquiryValuebookletBlockNo, src => src.ValuebookletBlockNo)
               .Map(dest => dest.InquiryValuebookletRowNo, src => src.ValuebookletRowNo)
               .Map(dest => dest.InquiryWorkCompletionCertificateDate, src => src.WorkCompletionCertificateDate)
               .Map(dest => dest.InquiryStatus, src => src.WorkflowStates.State)
               .Map(dest => dest.InquiryShebaNo, src => src.ShebaNo)
               .Map(dest => dest.InquiryStatusTitle, src => src.WorkflowStates.Title)
               .Map(dest => dest.InquiryId, src => src.Id.ToString());
            //.Map(dest => dest.ILM, src => src.InquiryILM)
            //.Map(dest => dest.LEGACYID, src => src.InquiryLEGACYID)
            //.Map(dest => dest.ShebaNo, src => src.InquiryShebaNo);
            config.Compile();

            var viewModel = entity.Adapt<EstateTaxInquiryViewModel>(config);
            viewModel.IsValid = true;
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;

        }

        public static Domain.Entities.EstateTaxInquiryPerson ViewModelToEntity(EstateTaxInquiryOwnerViewModel viewModel)
        {

            var config = new TypeAdapterConfig();


            config.NewConfig<EstateTaxInquiryOwnerViewModel, Domain.Entities.EstateTaxInquiryPerson>()
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.IsIranian, src => src.PersonIsIrani.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.ForiegnIssuePlace, src => src.PersonForiegnIssuePlace)
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.IssuePlace, src => src.PersonIssuePlaceText)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.NationalityCode, src => src.PersonNationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerialNo)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.SeriAlpha, src => src.PersonSeriAlpha)
                .Map(dest => dest.SanaState, src => src.PersonSanaState.HasValue ? src.PersonSanaState.Value.ToYesNo() : null)
                .Map(dest => dest.SanaMobileNo, src => src.PersonSanaMoileNo)
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.HasValue ? src.PersonMobileNoState.Value.ToYesNo() : null)
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                //.Map(dest => dest.IsOriginal, src => "1")
                //.Map(dest => dest.IsRelated, src => "2")
                .Map(dest => dest.IsAlive, src => src.PersonIsAlive.ToYesNo())
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.Fax, src => src.PersonFax)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .Map(dest => dest.IsIlencChecked, src => src.PersonIsIlencChecked.ToYesNo())
                .Map(dest => dest.IsIlencCorrect, src => src.PersonIsIlencCorrect.ToYesNo())
                .Map(dest => dest.IsSabtahvalChecked, src => src.PersonIsSabtahvalChecked.ToYesNo())
                .Map(dest => dest.IsSabtahvalCorrect, src => src.PersonIsSabtahvalCorrect.ToYesNo())
                .Map(dest => dest.IsForeignerssysChecked, src => src.PersonIsForeignerssysChecked.ToYesNo())
                .Map(dest => dest.IsForeignerssysCorrect, src => src.PersonIsForeignerssysCorrect.ToYesNo())
                //.Map(dest => dest.DealsummaryPersonRelateTypeId, src => "108")
                .IgnoreNonMapped(true);



            config.Compile();


            var estateTaxInquiryPerson = viewModel.Adapt<Domain.Entities.EstateTaxInquiryPerson>(config);
            estateTaxInquiryPerson.CityId = viewModel.PersonCityId != null && viewModel.PersonCityId.Count > 0 ? Convert.ToInt32(viewModel.PersonCityId.First()) : null;
            estateTaxInquiryPerson.IssuePlaceId = viewModel.PersonIssuePlaceId != null && viewModel.PersonIssuePlaceId.Count > 0 ? Convert.ToInt32(viewModel.PersonIssuePlaceId.First()) : null;
            estateTaxInquiryPerson.NationalityId = Convert.ToInt32(viewModel.PersonNationalityId.First());
            estateTaxInquiryPerson.IsRelated = "2";
            estateTaxInquiryPerson.IsOriginal = "1";
            estateTaxInquiryPerson.DealsummaryPersonRelateTypeId = "108";
            if (!viewModel.PersonIsIrani)
            {
                estateTaxInquiryPerson.IssuePlaceId = estateTaxInquiryPerson.NationalityId;
            }
            Helper.NormalizeStringValues(estateTaxInquiryPerson);
            return estateTaxInquiryPerson;
        }
        public static EstateTaxInquiryOwnerViewModel EntityToOwnerViewModel(Domain.Entities.EstateTaxInquiryPerson entity)
        {

            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.EstateTaxInquiryPerson, EstateTaxInquiryOwnerViewModel>()
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonIsIrani, src => src.IsIranian.ToBoolean())
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonForiegnIssuePlace, src => src.ForiegnIssuePlace)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonIssuePlaceText, src => src.IssuePlace)
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.PersonNationalityCode, src => src.NationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerialNo, src => src.Serial)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonSeriAlpha, src => src.SeriAlpha)
                .Map(dest => dest.PersonSanaState, src => src.SanaState.ToNullabbleBoolean())
                .Map(dest => dest.PersonSanaMoileNo, src => src.SanaMobileNo)
                .Map(dest => dest.PersonMobileNoState, src => src.MobileNoState.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.PersonIsOriginal, src => true)
                .Map(dest => dest.PersonIsRelated, src => false)
                .Map(dest => dest.PersonIsAlive, src => src.IsAlive.ToBoolean())
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonFax, src => src.Fax)
                .Map(dest => dest.PersonDescription, src => src.Description)
                .Map(dest => dest.PersonTel, src => src.Tel)
                .Map(dest => dest.PersonIsIlencChecked, src => src.IsIlencChecked.ToBoolean())
                .Map(dest => dest.PersonIsIlencCorrect, src => src.IsIlencCorrect.ToBoolean())
                .Map(dest => dest.PersonIsForeignerssysChecked, src => src.IsForeignerssysChecked.ToBoolean())
                .Map(dest => dest.PersonIsForeignerssysCorrect, src => src.IsForeignerssysCorrect.ToBoolean())
                .Map(dest => dest.PersonIsSabtahvalChecked, src => src.IsSabtahvalChecked.ToBoolean())
                .Map(dest => dest.PersonIsSabtahvalCorrect, src => src.IsSabtahvalCorrect.ToBoolean())
                .Map(dest => dest.PersonTimestamp, src => src.Timestamp)
                .IgnoreNonMapped(true);



            config.Compile();


            var viewModel = entity.Adapt<EstateTaxInquiryOwnerViewModel>(config);
            viewModel.PersonCityId = entity.CityId != null ? new List<string> { entity.CityId.Value.ToString() } : new List<string>();
            viewModel.PersonIssuePlaceId = entity.IssuePlaceId != null ? new List<string> { entity.IssuePlaceId.Value.ToString() } : new List<string>();
            viewModel.PersonNationalityId = entity.NationalityId != null ? new List<string> { entity.NationalityId.ToString() } : new List<string>();
            viewModel.IsValid = true;
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;
        }

        public static Domain.Entities.EstateTaxInquiryPerson ViewModelToEntity(EstateTaxInquiryBuyerViewModel viewModel)
        {

            var config = new TypeAdapterConfig();


            config.NewConfig<EstateTaxInquiryBuyerViewModel, Domain.Entities.EstateTaxInquiryPerson>()
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.IsIranian, src => src.PersonIsIrani.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                //.Map(dest => dest.ForiegnIssuePlace, src => src.PersonForiegnIssuePlace)
                //.Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.PersonId.ToGuid())
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.IssuePlace, src => src.PersonIssuePlaceText)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.NationalityCode, src => src.PersonNationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerialNo)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.SeriAlpha, src => src.PersonSeriAlpha)
                .Map(dest => dest.SanaState, src => src.PersonSanaState.HasValue ? src.PersonSanaState.Value.ToYesNo() : null)
                .Map(dest => dest.SanaMobileNo, src => src.PersonSanaMoileNo)
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.HasValue ? src.PersonMobileNoState.Value.ToYesNo() : null)
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                //.Map(dest => dest.IsOriginal, src => "1")
                //.Map(dest => dest.IsRelated, src => "2")
                .Map(dest => dest.IsAlive, src => src.PersonIsAlive.ToYesNo())
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.Fax, src => src.PersonFax)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .Map(dest => dest.IsIlencChecked, src => src.PersonIsIlencChecked.ToYesNo())
                .Map(dest => dest.IsIlencCorrect, src => src.PersonIsIlencCorrect.ToYesNo())
                .Map(dest => dest.IsSabtahvalChecked, src => src.PersonIsSabtahvalChecked.ToYesNo())
                .Map(dest => dest.IsSabtahvalCorrect, src => src.PersonIsSabtahvalCorrect.ToYesNo())
                .Map(dest => dest.DealsummaryPersonRelateTypeId, src => src.PersonRelationTypeId.First())
                .Map(dest => dest.SharePart, src => src.PersonSharePart)
                .Map(dest => dest.ShareTotal, src => src.PersonShareTotal)
                .IgnoreNonMapped(true);



            config.Compile();


            var estateTaxInquiryPerson = viewModel.Adapt<Domain.Entities.EstateTaxInquiryPerson>(config);
            estateTaxInquiryPerson.CityId = viewModel.PersonCityId != null && viewModel.PersonCityId.Count > 0 ? Convert.ToInt32(viewModel.PersonCityId.First()) : null;
            estateTaxInquiryPerson.IssuePlaceId = viewModel.PersonIssuePlaceId != null && viewModel.PersonIssuePlaceId.Count > 0 ? Convert.ToInt32(viewModel.PersonIssuePlaceId.First()) : null;
            estateTaxInquiryPerson.NationalityId = Convert.ToInt32(viewModel.PersonNationalityId.First());
            estateTaxInquiryPerson.IsRelated = "2";
            estateTaxInquiryPerson.IsOriginal = "1";

            Helper.NormalizeStringValues(estateTaxInquiryPerson);
            return estateTaxInquiryPerson;
        }
        public static EstateTaxInquiryBuyerViewModel EntityToBuyerViewModel(Domain.Entities.EstateTaxInquiryPerson entity)
        {

            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.EstateTaxInquiryPerson, EstateTaxInquiryBuyerViewModel>()
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonIsIrani, src => src.IsIranian.ToBoolean())
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonForiegnIssuePlace, src => src.ForiegnIssuePlace)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonIssuePlaceText, src => src.IssuePlace)
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.PersonNationalityCode, src => src.NationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerialNo, src => src.Serial)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonSeriAlpha, src => src.SeriAlpha)
                .Map(dest => dest.PersonSanaState, src => src.SanaState.ToNullabbleBoolean())
                .Map(dest => dest.PersonSanaMoileNo, src => src.SanaMobileNo)
                .Map(dest => dest.PersonMobileNoState, src => src.MobileNoState.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.PersonIsOriginal, src => true)
                .Map(dest => dest.PersonIsRelated, src => false)
                .Map(dest => dest.PersonIsAlive, src => src.IsAlive.ToBoolean())
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonFax, src => src.Fax)
                .Map(dest => dest.PersonDescription, src => src.Description)
                .Map(dest => dest.PersonTel, src => src.Tel)

                .Map(dest => dest.PersonIsIlencChecked, src => src.IsIlencChecked.ToBoolean())
                .Map(dest => dest.PersonIsIlencCorrect, src => src.IsIlencCorrect.ToBoolean())
                .Map(dest => dest.PersonIsSabtahvalChecked, src => src.IsSabtahvalChecked.ToBoolean())
                .Map(dest => dest.PersonIsSabtahvalCorrect, src => src.IsSabtahvalCorrect.ToBoolean())
                .Map(dest => dest.PersonRelationTypeId, src => new List<string> { src.DealsummaryPersonRelateTypeId })
                .Map(dest => dest.PersonSharePart, src => src.SharePart.Value)
                .Map(dest => dest.PersonShareTotal, src => src.ShareTotal)
                .Map(dest => dest.PersonTimestamp, src => src.Timestamp)
                .IgnoreNonMapped(true);



            config.Compile();


            var viewModel = entity.Adapt<EstateTaxInquiryBuyerViewModel>(config);
            viewModel.PersonCityId = entity.CityId != null ? new List<string> { entity.CityId.Value.ToString() } : new List<string>();
            viewModel.PersonIssuePlaceId = entity.IssuePlaceId != null ? new List<string> { entity.IssuePlaceId.Value.ToString() } : new List<string>();
            viewModel.PersonNationalityId = entity.NationalityId != null ? new List<string> { entity.NationalityId.ToString() } : new List<string>();
            viewModel.IsValid = true;
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;
        }

        public static void SetEntityValues(UpdateEstateTaxInquiryCommand viewModel, ref Domain.Entities.EstateTaxInquiry entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<UpdateEstateTaxInquiryCommand, Domain.Entities.EstateTaxInquiry>()
               .Map(dest => dest.ApartmentArea, src => src.InquiryApartmentArea)
               .Map(dest => dest.ArsehArea, src => src.InquiryArsehArea)
               .Map(dest => dest.Avenue, src => src.InquiryAvenue)
               .Map(dest => dest.BuildingOld, src => src.InquiryBuildingOld)
               //.Map(dest => dest.BuildingType, src => src.InquiryBUILDINGTYPE)
               .Map(dest => dest.BuildingUsingTypeId, src => src.InquiryBuildingUsingTypeId != null && src.InquiryBuildingUsingTypeId.Count > 0 ? src.InquiryBuildingUsingTypeId.First() : null)
               //.Map(dest => dest.CertificateFile, src => src.InquiryCERTIFICATEFILE)
               //.Map(dest => dest.CertificateHtml, src => src.InquiryCERTIFICATEHTML)
               .Map(dest => dest.CertificateNo, src => src.InquiryCertificateNo)
               .Map(dest => dest.CessionDate, src => src.InquiryCessionDate)
               .Map(dest => dest.CessionPrice, src => src.InquiryCessionPrice)
               //.Map(dest => dest.CreateDate, src => src.InquiryCreateDate)
               //.Map(dest => dest.CreateTime, src => src.InquiryCreateTime)
               .Map(dest => dest.EstateAddress, src => src.InquiryEstateAddress)
               //.Map(dest => dest.EstateInquiryId, src => src.EstateInquiryId != null && src.EstateInquiryId.Count > 0 ? src.EstateInquiryId.First() : null)
               .Map(dest => dest.EstatePostCode, src => src.InquiryEstatePostCode)
               .Map(dest => dest.EstateSectionId, src => src.InquiryEstateSectionId.First())
               .Map(dest => dest.EstateSector, src => src.InquiryEstateSector)
               .Map(dest => dest.EstateTaxCityId, src => src.InquiryEstateTaxCityId.First())
               .Map(dest => dest.EstateTaxCountyId, src => src.InquiryEstateTaxCountyId.First())
               .Map(dest => dest.EstateTaxInquiryBuildingConstructionStepId, src => src.InquiryBuildingConstructionStepId != null && src.InquiryBuildingConstructionStepId.Count > 0 ? src.InquiryBuildingConstructionStepId.First() : null)
               .Map(dest => dest.EstateTaxInquiryBuildingStatusId, src => src.InquiryBuildingStatusId != null && src.InquiryBuildingStatusId.Count > 0 ? src.InquiryBuildingStatusId.First() : null)
               .Map(dest => dest.EstateTaxInquiryBuildingTypeId, src => src.InquiryBuildingTypeId != null && src.InquiryBuildingTypeId.Count > 0 ? src.InquiryBuildingTypeId.First() : null)
               .Map(dest => dest.EstateTaxInquiryDocumentRequestTypeId, src => src.InquiryDocumentRequestTypeId.First())
               .Map(dest => dest.EstateTaxInquiryFieldTypeId, src => src.InquiryFieldTypeId.First())
               .Map(dest => dest.EstateTaxInquiryTransferTypeId, src => src.InquiryTransferTypeId.First())
               .Map(dest => dest.EstateTaxUnitId, src => src.InquiryEstateTaxUnitId != null && src.InquiryEstateTaxUnitId.Count > 0 ? src.InquiryEstateTaxUnitId.First() : null)
               .Map(dest => dest.Estatebasic, src => src.InquiryEstatebasic)
               .Map(dest => dest.Estatesecondary, src => src.InquiryEstatesecondary)
               .Map(dest => dest.FieldUsingTypeId, src => src.InquiryFieldUsingTypeId.First())
               .Map(dest => dest.FkEstateTaxProvinceId, src => src.InquiryEstateTaxProvinceId.First())
               .Map(dest => dest.FloorNo, src => src.InquiryFloorNo)
               .Map(dest => dest.HasSpecialTrance, src => src.InquiryHasSpecialTrance.ToYesNo())
               .Map(dest => dest.HasSpecifiedTradingValue, src => src.InquiryHasSpecifiedTradingValue.ToYesNo())
               //.Map(dest => dest.IsActive, src => src.InquiryISACTIVE)
               .Map(dest => dest.IsFirstCession, src => src.InquiryIsFirstCession.ToYesNo())
               .Map(dest => dest.IsFirstDeal, src => src.InquiryIsFirstDeal.ToYesNo())
               .Map(dest => dest.IsGroundLevel, src => src.InquiryIsGroundLevel.ToYesNo())
               .Map(dest => dest.IsLicenceReady, src => src.InquiryIsLicenceReady.ToYesNo())
               .Map(dest => dest.IsMissingSeparateDocument, src => src.InquiryIsMissingSeparateDocument.ToYesNo())
               .Map(dest => dest.IsWornTexture, src => src.InquiryIsWornTexture.ToYesNo())
               //.Map(dest => dest.LastReceiveStatusDate, src => src.InquiryLastReceiveStatusDate)
               //.Map(dest => dest.LastReceiveStatusTime, src => src.InquiryLastReceiveStatusTime)
               //.Map(dest => dest.LASTSENDDATE, src => src.InquiryLASTSENDDATE)
               //.Map(dest => dest.LASTSENDTIME, src => src.InquiryLASTSENDTIME)
               .Map(dest => dest.LicenseDate, src => src.InquiryLicenseDate)
               .Map(dest => dest.LocationAssignRightDealCurrentValue, src => src.InquiryLocationAssignRightDealCurrentValue)
               .Map(dest => dest.LocationAssignRigthOwnershipTypeId, src => src.InquiryLocationAssignRigthOwnershipTypeId != null && src.InquiryLocationAssignRigthOwnershipTypeId.Count > 0 ? src.InquiryLocationAssignRigthOwnershipTypeId.First() : null)
               .Map(dest => dest.LocationAssignRigthUsingTypeId, src => src.InquiryLocationAssignRigthUsingTypeId != null && src.InquiryLocationAssignRigthUsingTypeId.Count > 0 ? src.InquiryLocationAssignRigthUsingTypeId.First() : null)
               //.Map(dest => dest.NO, src => src.InquiryNO)
               //.Map(dest => dest.NO2, src => src.InquiryNO2)
               .Map(dest => dest.EstateUnitId, src => src.InquiryEstateUnitId.First())
               .Map(dest => dest.EstateSubsectionId, src => src.InquiryEstateSubSectionId.First())
               .Map(dest => dest.BasicRemaining, src => src.InquiryEstateBasicRemaining.ToYesNo())
               .Map(dest => dest.SecondaryRemaining, src => src.InquiryEstateSecondaryRemaining.ToYesNo())
               .Map(dest => dest.PlateNo, src => src.InquiryPlateNo)
               .Map(dest => dest.PrevTransactionsAccordingToFacilitateRule, src => src.InquiryPrevTransactionsAccordingToFacilitateRule.ToYesNo())
               .Map(dest => dest.RenovationRelatedBlockNo, src => src.InquiryRenovationRelatedBlockNo)
               .Map(dest => dest.RenovationRelatedEstateNo, src => src.InquiryRenovationRelatedEstateNo)
               .Map(dest => dest.RenovationRelatedRowNo, src => src.InquiryRenovationRelatedRowNo)
               //.Map(dest => dest.ScriptoriumId, src => src.InquirySCRIPTORIUMID)
               .Map(dest => dest.SeparationProcessNo, src => src.InquirySeparationProcessNo)
               .Map(dest => dest.ShareOfOwnership, src => src.InquiryShareOfOwnership)
               //.Map(dest => dest.StatusDescription, src => src.InquiryStatusDescription)
               //.Map(dest => dest.TaxAmount, src => src.InquiryTaxAmount)
               //.Map(dest => dest.TaxBillHtml, src => src.InquiryTaxBillHtml)
               //.Map(dest => dest.TaxBillIdentity, src => src.InquiryTaxBillIdentity)
               //.Map(dest => dest.TaxPaymentIdentity, src => src.InquiryTaxPaymentIdentity)
               //.Map(dest => dest.TIMESTAMP, src => src.InquiryTIMESTAMP)
               .Map(dest => dest.TotalArea, src => src.InquiryTotalArea)
               .Map(dest => dest.TotalOwnershipShare, src => src.InquiryTotalOwnershipShare)
               //.Map(dest => dest.TrackingCode, src => src.InquiryTrackingCode)
               .Map(dest => dest.TranceWidth, src => src.InquiryTranceWidth)
               .Map(dest => dest.TransitionShare, src => src.InquiryTransitionShare)
               .Map(dest => dest.ValuebookletBlockNo, src => src.InquiryValuebookletBlockNo)
               .Map(dest => dest.ValuebookletRowNo, src => src.InquiryValuebookletRowNo)
               .Map(dest => dest.WorkCompletionCertificateDate, src => src.InquiryWorkCompletionCertificateDate);
            //.Map(dest => dest.WorkflowStatesId, src => src.InquiryWORKFLOWSTATESID)
            //.Map(dest => dest.ILM, src => src.InquiryILM)
            //.Map(dest => dest.LEGACYID, src => src.InquiryLEGACYID)
            //.Map(dest => dest.ShebaNo, src => src.InquiryShebaNo);
            config.Compile();

            viewModel.Adapt(entity, config);
            if (viewModel.EstateInquiryId != null && viewModel.EstateInquiryId.Count > 0)
                entity.EstateInquiryId = viewModel.EstateInquiryId.First().ToGuid();
            Helper.NormalizeStringValues(entity, true);


        }

        public static void SetEntityValues(EstateTaxInquiryOwnerViewModel viewModel, ref Domain.Entities.EstateTaxInquiryPerson entity)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<EstateTaxInquiryOwnerViewModel, Domain.Entities.EstateTaxInquiryPerson>()
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.IsIranian, src => src.PersonIsIrani.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.ForiegnIssuePlace, src => src.PersonForiegnIssuePlace)
                //.Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.PersonId.ToGuid())
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.IssuePlace, src => src.PersonIssuePlaceText)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.NationalityCode, src => src.PersonNationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerialNo)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.SeriAlpha, src => src.PersonSeriAlpha)
                .Map(dest => dest.SanaState, src => src.PersonSanaState.HasValue ? src.PersonSanaState.Value.ToYesNo() : null)
                .Map(dest => dest.SanaMobileNo, src => src.PersonSanaMoileNo)
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.HasValue ? src.PersonMobileNoState.Value.ToYesNo() : null)
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.IsOriginal, src => src.PersonIsOriginal.ToYesNo())
                .Map(dest => dest.IsRelated, src => src.PersonIsRelated.ToYesNo())
                .Map(dest => dest.IsAlive, src => src.PersonIsAlive.ToYesNo())
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.Fax, src => src.PersonFax)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.Tel, src => src.PersonTel)

                .Map(dest => dest.IsIlencChecked, src => src.PersonIsIlencChecked.ToYesNo())
                .Map(dest => dest.IsIlencCorrect, src => src.PersonIsIlencCorrect.ToYesNo())
                .Map(dest => dest.IsForeignerssysChecked, src => src.PersonIsForeignerssysChecked.ToYesNo())
                .Map(dest => dest.IsForeignerssysCorrect, src => src.PersonIsForeignerssysCorrect.ToYesNo())
                .Map(dest => dest.IsSabtahvalChecked, src => src.PersonIsSabtahvalChecked.ToYesNo())
                .Map(dest => dest.IsSabtahvalCorrect, src => src.PersonIsSabtahvalCorrect.ToYesNo())
                //.Map(dest => dest.DealsummaryPersonRelateTypeId, src => "108")
                .IgnoreNonMapped(true);

            config.Compile();
            viewModel.Adapt(entity, config);


            entity.CityId = viewModel.PersonCityId != null && viewModel.PersonCityId.Count > 0 ? Convert.ToInt32(viewModel.PersonCityId.First()) : null;
            entity.IssuePlaceId = viewModel.PersonIssuePlaceId != null && viewModel.PersonIssuePlaceId.Count > 0 ? Convert.ToInt32(viewModel.PersonIssuePlaceId.First()) : null;
            entity.NationalityId = Convert.ToInt32(viewModel.PersonNationalityId.First());
            entity.DealsummaryPersonRelateTypeId = "108";
            if (!viewModel.PersonIsIrani)
            {
                entity.IssuePlaceId = entity.NationalityId;
            }
            Helper.NormalizeStringValues(entity);



        }

        public static void SetEntityValues(EstateTaxInquiryBuyerViewModel viewModel, ref Domain.Entities.EstateTaxInquiryPerson entity)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<EstateTaxInquiryBuyerViewModel, Domain.Entities.EstateTaxInquiryPerson>()
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.IsIranian, src => src.PersonIsIrani.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                //.Map(dest => dest.ForiegnIssuePlace, src => src.PersonForiegnIssuePlace)                
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.IssuePlace, src => src.PersonIssuePlaceText)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.NationalityCode, src => src.PersonNationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerialNo)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.SeriAlpha, src => src.PersonSeriAlpha)
                .Map(dest => dest.SanaState, src => src.PersonSanaState.HasValue ? src.PersonSanaState.Value.ToYesNo() : null)
                .Map(dest => dest.SanaMobileNo, src => src.PersonSanaMoileNo)
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.HasValue ? src.PersonMobileNoState.Value.ToYesNo() : null)
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                //.Map(dest => dest.IsOriginal, src => "1")
                //.Map(dest => dest.IsRelated, src => "2")
                .Map(dest => dest.IsAlive, src => src.PersonIsAlive.ToYesNo())
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.Fax, src => src.PersonFax)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .Map(dest => dest.IsIlencChecked, src => src.PersonIsIlencChecked.ToYesNo())
                .Map(dest => dest.IsIlencCorrect, src => src.PersonIsIlencCorrect.ToYesNo())
                //.Map(dest => dest.IsForeignerssysChecked, src => src.PersonIsForeignerssysChecked.ToYesNo())
                //.Map(dest => dest.IsForeignerssysCorrect, src => src.PersonIsForeignerssysCorrect.ToYesNo())
                .Map(dest => dest.IsSabtahvalChecked, src => src.PersonIsSabtahvalChecked.ToYesNo())
                .Map(dest => dest.IsSabtahvalCorrect, src => src.PersonIsSabtahvalCorrect.ToYesNo())
                .Map(dest => dest.DealsummaryPersonRelateTypeId, src => src.PersonRelationTypeId.First())
                .Map(dest => dest.SharePart, src => src.PersonSharePart)
                .Map(dest => dest.ShareTotal, src => src.PersonShareTotal)
                .IgnoreNonMapped(true);

            config.Compile();
            viewModel.Adapt(entity, config);


            entity.CityId = viewModel.PersonCityId != null && viewModel.PersonCityId.Count > 0 ? Convert.ToInt32(viewModel.PersonCityId.First()) : null;
            entity.IssuePlaceId = viewModel.PersonIssuePlaceId != null && viewModel.PersonIssuePlaceId.Count > 0 ? Convert.ToInt32(viewModel.PersonIssuePlaceId.First()) : null;
            entity.NationalityId = Convert.ToInt32(viewModel.PersonNationalityId.First());
            entity.IsOriginal = "1";
            entity.IsRelated = "2";
            Helper.NormalizeStringValues(entity);



        }


        public static EstateTaxInquiryAttach ViewModelToEntity(EstateTaxInquiryAttachViewModel viewModel)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<EstateTaxInquiryAttachViewModel, Domain.Entities.EstateTaxInquiryAttach>()
                .Map(dest => dest.Area, src => src.AttachArea)
                .Map(dest => dest.Block, src => src.AttachBlock)
                .Map(dest => dest.EstatePieceTypeId, src => src.AttachEstatePieceTypeId.First())
                .Map(dest => dest.EstateSideTypeId, src => src.AttachEstateSideTypeId.First())
                .Map(dest => dest.Floor, src => src.AttachFloor)
                .Map(dest => dest.Piece, src => src.AttachPiece)
                .IgnoreNonMapped(true);



            config.Compile();


            var estateTaxInquiryAttach = viewModel.Adapt<Domain.Entities.EstateTaxInquiryAttach>(config);
            Helper.NormalizeStringValues(estateTaxInquiryAttach);
            return estateTaxInquiryAttach;
        }

        public static EstateTaxInquiryAttachViewModel EntityToViewModel(EstateTaxInquiryAttach entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.EstateTaxInquiryAttach, EstateTaxInquiryAttachViewModel>()
                .Map(dest => dest.AttachArea, src => src.Area)
                .Map(dest => dest.AttachBlock, src => src.Block)
                .Map(dest => dest.AttachEstatePieceTypeId, src => new List<string> { src.EstatePieceTypeId })
                .Map(dest => dest.AttachEstateSideTypeId, src => new List<string> { src.EstateSideTypeId })
                .Map(dest => dest.AttachFloor, src => src.Floor)
                .Map(dest => dest.AttachPiece, src => src.Piece)
                .Map(dest => dest.AttachId, src => src.Id)
                .Map(dest => dest.AttachTimeStamp, src => src.Timestamp)
                .Map(dest => dest.AttachEstatePieceTypeTitle, src => src.EstatePieceType.Title)
                .Map(dest => dest.AttachEstateSideTypeTitle, src => src.EstateSideType.Title)
                .IgnoreNonMapped(true);

            config.Compile();


            var model = entity.Adapt<EstateTaxInquiryAttachViewModel>(config);
            model.IsValid = true;
            Helper.NormalizeStringValues(model, false);
            return model;
        }

        public static void SetEntityValues(EstateTaxInquiryAttachViewModel viewModel, ref Domain.Entities.EstateTaxInquiryAttach entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<EstateTaxInquiryAttachViewModel, Domain.Entities.EstateTaxInquiryAttach>()
                .Map(dest => dest.Area, src => src.AttachArea)
                .Map(dest => dest.Block, src => src.AttachBlock)
                .Map(dest => dest.EstatePieceTypeId, src => src.AttachEstatePieceTypeId.First())
                .Map(dest => dest.EstateSideTypeId, src => src.AttachEstateSideTypeId.First())
                .Map(dest => dest.Floor, src => src.AttachFloor)
                .Map(dest => dest.Piece, src => src.AttachPiece)
                .IgnoreNonMapped(true);



            config.Compile();


            var estateTaxInquiryAttach = viewModel.Adapt(entity, config);
            Helper.NormalizeStringValues(estateTaxInquiryAttach);

        }
    }
}
