using Mapster;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateInquiry
{
    public static class EstateInquiryMapper
    {
        public static Domain.Entities.EstateInquiry ViewModelToEntity(CreateEstateInquiryCommand viewModel)
        {


            var config = new TypeAdapterConfig();

            config.NewConfig<CreateEstateInquiryCommand, Domain.Entities.EstateInquiry>()
                .Map(dest => dest.Area, src => src.InqArea)
                .Map(dest => dest.Basic, src => src.InqBasic)
                .Map(dest => dest.BasicRemaining, src => src.InqBasicRemaining.ToYesNo())
                .Map(dest => dest.DealSummaryDate, src => src.InqDealSummaryDate)
                .Map(dest => dest.DealSummaryNo, src => src.InqDealSummaryNo)
                .Map(dest => dest.DealSummaryScriptorium, src => src.InqDealSummaryScriptorium)
                .Map(dest => dest.DocPrintNo, src => src.InqDocPrintNo)
                .Map(dest => dest.DocumentIsNote, src => src.InqDocumentIsNote.ToYesNo())
                .Map(dest => dest.DocumentIsReplica, src => src.InqDocumentIsReplica.ToYesNo())
                .Map(dest => dest.EdeclarationNo, src => src.InqEdeclarationNo)
                .Map(dest => dest.ElectronicEstateNoteNo, src => src.InqElectronicEstateNoteNo)
                .Map(dest => dest.EstateNoteNo, src => src.InqEstateNoteNo)
                .Map(dest => dest.EstatePostalCode, src => src.InqEstatePostalCode)
                .Map(dest => dest.EstateSectionId, src => src.InqEstateSectionId.First())
                .Map(dest => dest.EstateSeridaftarId, src => src.InqEstateSeridaftarId != null && src.InqEstateSeridaftarId.Count > 0 ? src.InqEstateSeridaftarId.First() : null)
                .Map(dest => dest.EstateSubsectionId, src => src.InqEstateSubsectionId.First())
                .Map(dest => dest.GeoLocationId, src => Convert.ToInt32(src.InqGeoLocationId.First()))
                .Map(dest => dest.InquiryDate, src => src.InqInquiryDate)
                .Map(dest => dest.InquiryNo, src => src.InqInquiryNo)
                .Map(dest => dest.InquiryPaymantRefno, src => src.InqInquiryPaymantRefno)
                .Map(dest => dest.MobileNo, src => src.InqMobileNo)
                .Map(dest => dest.MortgageText, src => src.InqMortgageText)
                .Map(dest => dest.Note21PaymentRefno, src => src.InqNote21PaymentRefno)
                .Map(dest => dest.PageNo, src => src.InqPageNo)
                .Map(dest => dest.RegisterNo, src => src.InqRegisterNo)
                .Map(dest => dest.Secondary, src => src.InqSecondary)
                .Map(dest => dest.SecondaryRemaining, src => src.InqSecondaryRemaining.ToYesNo())
                .Map(dest => dest.UnitId, src => src.InqUnitId.First())
                .Map(dest => dest.EstateInquiryTypeId, src => src.InqEstateInquiryTypeId.First())
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.InqId.ToGuid())
                .IgnoreNonMapped(true);



            config.Compile();


            var estateInquiry = viewModel.Adapt<Domain.Entities.EstateInquiry>(config);
            if (viewModel.InqEstateInquiryId != null && viewModel.InqEstateInquiryId.Count > 0)
                estateInquiry.EstateInquiryId = viewModel.InqEstateInquiryId.First().ToGuid();

            Helper.NormalizeStringValues(estateInquiry);
            return estateInquiry;
        }
        public static Domain.Entities.EstateInquiryPerson ViewModelToEntity(EstateInquiryPersonViewModel viewModel)
        {

            var config = new TypeAdapterConfig();


            config.NewConfig<EstateInquiryPersonViewModel, Domain.Entities.EstateInquiryPerson>()
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.ExecutiveTransfer, src => src.PersonExecutiveTransfer.ToYesNo())
                .Map(dest => dest.IsIranian, src => src.PersonIsIrani.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.ForiegnIssuePlace, src => src.PersonForiegnIssuePlace)
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.PersonId.ToGuid())
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.IssuePlaceText, src => src.PersonIssuePlaceText)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.NationalityCode, src => src.PersonNationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.SerialNo, src => src.PersonSerialNo)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.SharePart, src => src.PersonSharePart)
                .Map(dest => dest.ShareText, src => src.PersonShareText)
                .Map(dest => dest.ShareTotal, src => src.PersonShareTotal)
                .Map(dest => dest.SeriAlpha, src => src.PersonSeriAlpha)
                .Map(dest => dest.SanaState, src => src.PersonSanaState.HasValue ? src.PersonSanaState.Value.ToYesNo() : null)
                .Map(dest => dest.SanaMobileNo, src => src.PersonSanaMoileNo)
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.HasValue ? src.PersonMobileNoState.Value.ToYesNo() : null)
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.IsOriginal, src => "1")
                .Map(dest => dest.IsRelated, src => "2")
                .Map(dest => dest.IsAlive, src => src.PersonIsAlive.ToYesNo())
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.Fax, src => src.PersonFax)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .IgnoreNonMapped(true);



            config.Compile();


            var estateInquiryPerson = viewModel.Adapt<Domain.Entities.EstateInquiryPerson>(config);
            estateInquiryPerson.CityId = viewModel.PersonCityId != null && viewModel.PersonCityId.Count > 0 ? Convert.ToInt32(viewModel.PersonCityId.First()) : null;
            estateInquiryPerson.IssuePlaceId = viewModel.PersonIssuePlaceId != null && viewModel.PersonIssuePlaceId.Count > 0 ? Convert.ToInt32(viewModel.PersonIssuePlaceId.First()) : null;
            estateInquiryPerson.NationalityId = viewModel.PersonNationalityId != null && viewModel.PersonNationalityId.Count > 0 ? Convert.ToInt32(viewModel.PersonNationalityId.First()) : null;

            if (!viewModel.PersonIsIrani)
            {
                estateInquiryPerson.IssuePlaceId = estateInquiryPerson.NationalityId;
            }
            Helper.NormalizeStringValues(estateInquiryPerson);
            return estateInquiryPerson;
        }

        public static void SetEntityValues(UpdateEstateInquiryCommand viewModel, ref Domain.Entities.EstateInquiry entity)
        {


            var config = new TypeAdapterConfig();

            config.NewConfig<UpdateEstateInquiryCommand, Domain.Entities.EstateInquiry>()
                .Map(dest => dest.Area, src => src.InqArea)
                .Map(dest => dest.Basic, src => src.InqBasic)
                .Map(dest => dest.BasicRemaining, src => src.InqBasicRemaining.ToYesNo())
                .Map(dest => dest.DealSummaryDate, src => src.InqDealSummaryDate)
                .Map(dest => dest.DealSummaryNo, src => src.InqDealSummaryNo)
                .Map(dest => dest.DealSummaryScriptorium, src => src.InqDealSummaryScriptorium)
                .Map(dest => dest.DocPrintNo, src => src.InqDocPrintNo)
                .Map(dest => dest.DocumentIsNote, src => src.InqDocumentIsNote.ToYesNo())
                .Map(dest => dest.DocumentIsReplica, src => src.InqDocumentIsReplica.ToYesNo())
                .Map(dest => dest.EdeclarationNo, src => src.InqEdeclarationNo)
                .Map(dest => dest.ElectronicEstateNoteNo, src => src.InqElectronicEstateNoteNo)
                .Map(dest => dest.EstateNoteNo, src => src.InqEstateNoteNo)
                .Map(dest => dest.EstatePostalCode, src => src.InqEstatePostalCode)
                .Map(dest => dest.EstateSectionId, src => src.InqEstateSectionId.First())
                .Map(dest => dest.EstateSeridaftarId, src => src.InqEstateSeridaftarId != null && src.InqEstateSeridaftarId.Count > 0 ? src.InqEstateSeridaftarId.First() : null)
                .Map(dest => dest.EstateSubsectionId, src => src.InqEstateSubsectionId.First())
                .Map(dest => dest.GeoLocationId, src => Convert.ToInt32(src.InqGeoLocationId.First()))
                .Map(dest => dest.InquiryNo, src => src.InqInquiryNo)
                .Map(dest => dest.InquiryPaymantRefno, src => src.InqInquiryPaymantRefno)
                .Map(dest => dest.MobileNo, src => src.InqMobileNo)
                .Map(dest => dest.MortgageText, src => src.InqMortgageText)
                .Map(dest => dest.Note21PaymentRefno, src => src.InqNote21PaymentRefno)
                .Map(dest => dest.PageNo, src => src.InqPageNo)
                .Map(dest => dest.RegisterNo, src => src.InqRegisterNo)
                .Map(dest => dest.Secondary, src => src.InqSecondary)
                .Map(dest => dest.SecondaryRemaining, src => src.InqSecondaryRemaining.ToYesNo())
                .Map(dest => dest.UnitId, src => src.InqUnitId.First())
                .IgnoreNonMapped(true);



            config.Compile();


            viewModel.Adapt(entity, config);
            if (viewModel.InqEstateInquiryId != null && viewModel.InqEstateInquiryId.Count > 0)
                entity.EstateInquiryId = viewModel.InqEstateInquiryId.First().ToGuid();
            else
                entity.EstateInquiryId = null;

            if (viewModel.InqEstateInquiryTypeId != null && viewModel.InqEstateInquiryTypeId.Count > 0)
                entity.EstateInquiryTypeId = viewModel.InqEstateInquiryTypeId.First();
            else
                entity.EstateInquiryTypeId = null;
            Helper.NormalizeStringValues(entity);
        }
        public static void SetEntityValues(EstateInquiryPersonViewModel viewModel, ref Domain.Entities.EstateInquiryPerson entity)
        {

            var config = new TypeAdapterConfig();


            config.NewConfig<EstateInquiryPersonViewModel, Domain.Entities.EstateInquiryPerson>()
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.ExecutiveTransfer, src => src.PersonExecutiveTransfer.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.ForiegnIssuePlace, src => src.PersonForiegnIssuePlace)
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.IssuePlaceText, src => src.PersonIssuePlaceText)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.NationalityCode, src => src.PersonNationalityCode)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.SerialNo, src => src.PersonSerialNo)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.SharePart, src => src.PersonSharePart)
                .Map(dest => dest.ShareText, src => src.PersonShareText)
                .Map(dest => dest.ShareTotal, src => src.PersonShareTotal)
                .Map(dest => dest.IsIranian, src => src.PersonIsIrani.ToYesNo())
                .Map(dest => dest.SeriAlpha, src => src.PersonSeriAlpha)
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.HasValue ? src.PersonMobileNoState.ToYesNo() : null)
                .Map(dest => dest.SanaState, src => src.PersonSanaState.HasValue ? src.PersonSanaState.ToYesNo() : null)
                .Map(dest => dest.SanaMobileNo, src => src.PersonSanaMoileNo)
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.IsOriginal, src => "1")
                .Map(dest => dest.IsRelated, src => "2")
                .Map(dest => dest.IsAlive, src => src.PersonIsAlive.ToYesNo())
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.Fax, src => src.PersonFax)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .IgnoreNonMapped(true);



            config.Compile();


            viewModel.Adapt(entity, config);

            entity.CityId = viewModel.PersonCityId != null && viewModel.PersonCityId.Count > 0 ? Convert.ToInt32(viewModel.PersonCityId.First()) : null;
            entity.IssuePlaceId = viewModel.PersonIssuePlaceId != null && viewModel.PersonIssuePlaceId.Count > 0 ? Convert.ToInt32(viewModel.PersonIssuePlaceId.First()) : null;
            entity.NationalityId = viewModel.PersonNationalityId != null && viewModel.PersonNationalityId.Count > 0 ? Convert.ToInt32(viewModel.PersonNationalityId.First()) : null;

            if (!viewModel.PersonIsIrani)
            {
                entity.IssuePlaceId = entity.NationalityId;
            }
            Helper.NormalizeStringValues(entity);
        }

        public static EstateInquiryViewModel EntityToViewModel(Domain.Entities.EstateInquiry entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.EstateInquiry, EstateInquiryViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.InqTimestamp, src => src.Timestamp)
                .Map(dest => dest.InqArea, src => src.Area)
                .Map(dest => dest.InqBasic, src => src.Basic)
                .Map(dest => dest.InqBasicRemaining, src => src.BasicRemaining.ToNullabbleBoolean())
                .Map(dest => dest.InqDealSummaryDate, src => src.DealSummaryDate)
                .Map(dest => dest.InqDealSummaryNo, src => src.DealSummaryNo)
                .Map(dest => dest.InqDealSummaryScriptorium, src => src.DealSummaryScriptorium)
                .Map(dest => dest.InqDocPrintNo, src => src.DocPrintNo)
                .Map(dest => dest.InqDocumentIsNote, src => src.DocumentIsNote.ToBoolean())
                .Map(dest => dest.InqDocumentIsReplica, src => src.DocumentIsReplica.ToBoolean())
                .Map(dest => dest.InqEdeclarationNo, src => src.EdeclarationNo)
                .Map(dest => dest.InqElectronicEstateNoteNo, src => src.ElectronicEstateNoteNo)
                .Map(dest => dest.InqEstateNoteNo, src => src.EstateNoteNo)
                .Map(dest => dest.InqEstatePostalCode, src => src.EstatePostalCode)
                .Map(dest => dest.InqEstateSectionId, src => new List<string> { src.EstateSectionId })
                .Map(dest => dest.InqEstateSeridaftarId, src => !string.IsNullOrWhiteSpace(src.EstateSeridaftarId) ? new List<string> { src.EstateSeridaftarId } : new List<string>())
                .Map(dest => dest.InqEstateSubsectionId, src => new List<string> { src.EstateSubsectionId })
                .Map(dest => dest.InqGeoLocationId, src => new List<string> { src.GeoLocationId.ToString() })
                .Map(dest => dest.InqInquiryDate, src => src.InquiryDate)
                .Map(dest => dest.InqInquiryNo, src => src.InquiryNo)
                .Map(dest => dest.InqInquiryPaymantRefno, src => src.InquiryPaymantRefno)
                .Map(dest => dest.InqMobileNo, src => src.MobileNo)
                .Map(dest => dest.InqMortgageText, src => src.MortgageText)
                .Map(dest => dest.InqNote21PaymentRefno, src => src.Note21PaymentRefno)
                .Map(dest => dest.InqPageNo, src => src.PageNo)
                .Map(dest => dest.InqRegisterNo, src => src.RegisterNo)
                .Map(dest => dest.InqSecondary, src => src.Secondary)
                .Map(dest => dest.InqSecondaryRemaining, src => src.SecondaryRemaining.ToBoolean())
                .Map(dest => dest.InqUnitId, src => new List<string> { src.UnitId })
                .Map(dest => dest.InqScriptoriumId, src => new List<string> { src.ScriptoriumId })
                .Map(dest => dest.InqCreateDate, src => src.CreateDate)
                .Map(dest => dest.InqCreateTime, src => src.CreateTime)
                .Map(dest => dest.InqEstateInquiryTypeId, src => new List<string> { src.EstateInquiryTypeId })
                .Map(dest => dest.InqFirstSendDate, src => src.FirstSendDate)
                .Map(dest => dest.InqFirstSendTime, src => src.FirstSendTime)
                .Map(dest => dest.InqId, src => src.Id.ToString())
                .Map(dest => dest.InqIlm, src => src.Ilm)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.InqLastSendDate, src => src.LastSendDate)
                .Map(dest => dest.InqLastSendTime, src => src.LastSendTime)
                .Map(dest => dest.InqNo, src => src.No)
                .Map(dest => dest.InqResponse, src => src.Response)
                .Map(dest => dest.InqResponseDate, src => src.ResponseDate)
                .Map(dest => dest.InqResponseDigitalsignature, src => src.ResponseDigitalsignature)
                .Map(dest => dest.InqResponseNumber, src => src.ResponseNumber)
                .Map(dest => dest.InqResponseResult, src => src.ResponseResult)
                .Map(dest => dest.InqResponseSubjectdn, src => src.ResponseSubjectdn)
                .Map(dest => dest.InqResponseTime, src => src.ResponseTime)
                .Map(dest => dest.InqSpecificStatus, src => src.SpecificStatus)
                .Map(dest => dest.InqSeparationDate, src => src.SeparationDate)
                .Map(dest => dest.InqSeparationNo, src => src.SeparationNo)
                .Map(dest => dest.InqApartmentsTotalArea, src => src.ApartmentsTotalarea)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.InqIsCostPaid, src => src.IsCostPaid)
                .Map(dest => dest.InqPayCostDate, src => src.PayCostDate)
                .Map(dest => dest.InqPayCostTime, src => src.PayCostTime)
                .Map(dest => dest.InqReceiptNo, src => src.ReceiptNo)
                .Map(dest => dest.InqBillNo, src => src.BillNo)
                .Map(dest => dest.InqSumPrices, src => src.SumPrices)
                .Map(dest => dest.InqPaymentType, src => src.PaymentType)
                .Map(dest => dest.InqEstateInquiryId, src => src.EstateInquiryId != null ? new List<string> { src.EstateInquiryId.ToString() } : new List<string>())
                ;

            config.Compile();

            var viewModel = entity.Adapt<EstateInquiryViewModel>(config);
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;
        }

        public static EstateInquiryPersonViewModel EntityToViewModel(Domain.Entities.EstateInquiryPerson entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.EstateInquiryPerson, EstateInquiryPersonViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonCityId, src => src.CityId.HasValue ? new List<string> { src.CityId.Value.ToString() } : new List<string>())
                .Map(dest => dest.PersonExecutiveTransfer, src => src.ExecutiveTransfer.ToBoolean())
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonForiegnIssuePlace, src => src.ForiegnIssuePlace)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonIssuePlaceText, src => src.IssuePlaceText)
                .Map(dest => dest.PersonIssuePlaceId, src => src.IssuePlaceId.HasValue ? new List<string> { src.IssuePlaceId.Value.ToString() } : new List<string>())
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.PersonNationalityCode, src => src.NationalityCode)
                .Map(dest => dest.PersonNationalityId, src => src.NationalityId.HasValue ? new List<string> { src.NationalityId.Value.ToString() } : new List<string>())
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerialNo, src => src.SerialNo)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonSharePart, src => src.SharePart)
                .Map(dest => dest.PersonShareText, src => src.ShareText)
                .Map(dest => dest.PersonShareTotal, src => src.ShareTotal)
                //.Map(dest => dest.PersonIlm, src => src.Ilm)
                .Map(dest => dest.PersonTimestamp, src => src.Timestamp)
                .Map(dest => dest.PersonIsIrani, src => src.IsIranian.ToBoolean())
                .Map(dest => dest.PersonSeriAlpha, src => src.SeriAlpha)
                .Map(dest => dest.PersonIsForeignerssysChecked, src => src.IsForeignerssysChecked.ToBoolean())
                .Map(dest => dest.PersonIsForeignerssysCorrect, src => src.IsForeignerssysCorrect.ToBoolean())
                .Map(dest => dest.PersonIsIlencChecked, src => src.IsIlencChecked.ToBoolean())
                .Map(dest => dest.PersonIsIlencCorrect, src => src.IsIlencCorrect.ToBoolean())
                .Map(dest => dest.PersonIsSabtahvalChecked, src => src.IsSabtahvalChecked.ToBoolean())
                .Map(dest => dest.PersonIsSabtahvalCorrect, src => src.IsSabtahvalCorrect.ToBoolean())
                .Map(dest => dest.PersonSanaMoileNo, src => src.SanaMobileNo)
                .Map(dest => dest.PersonSanaState, src => src.SanaState.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.PersonMobileNoState, src => src.MobileNoState.ToNullabbleBoolean())
                .Map(dest => dest.PersonIsAlive, src => src.IsAlive.ToNullabbleBoolean())
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonFax, src => src.Fax)
                .Map(dest => dest.PersonDescription, src => src.Description)
                .Map(dest => dest.PersonTel, src => src.Tel)
                .Map(dest => dest.IsValid, src => true)
                ;

            config.Compile();


            var viewModel = entity.Adapt<EstateInquiryPersonViewModel>(config);
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;

        }



    }
}