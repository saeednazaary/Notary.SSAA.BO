using Mapster;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Estate;
namespace Notary.SSAA.BO.DataTransferObject.Mappers.Estate.DealSummary
{
    public static class DealSummaryMapper
    {
        
        public static DealSummaryViewModel EntityToViewModel(Domain.Entities.DealSummary entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.DealSummary, DealSummaryViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.DS_Timestamp, src => src.Timestamp)
                .Map(dest => dest.DS_Area, src => src.EstateInquiry.Area)
                .Map(dest => dest.DS_Basic, src => src.EstateInquiry.Basic)
                .Map(dest => dest.DS_BasicRemaining, src => src.EstateInquiry.BasicRemaining.ToBoolean())
                .Map(dest => dest.DS_Date, src => src.DealDate)
                .Map(dest => dest.DS_No, src => src.DealNo)
                .Map(dest => dest.DS_Amount, src => src.Amount.HasValue ? src.Amount.ToString():"")
                .Map(dest => dest.DS_AmountType, src => src.AmountUnitId)
                .Map(dest => dest.DS_Duration, src => src.Duration.HasValue ? src.Duration.Value.ToString():"")
                .Map(dest => dest.DS_DurationType, src => src.TimeUnitId)
                .Map(dest => dest.DS_DocumentIsNote, src => src.EstateInquiry.DocumentIsNote.ToBoolean())
                .Map(dest => dest.DS_DocumentIsReplica, src => src.EstateInquiry.DocumentIsReplica.ToBoolean())
                .Map(dest => dest.DS_EdeclarationNo, src => src.EstateInquiry.EdeclarationNo)
                .Map(dest => dest.DS_ElectronicEstateNoteNo, src => src.EstateInquiry.ElectronicEstateNoteNo)
                .Map(dest => dest.DS_EstateNoteNo, src => src.EstateInquiry.EstateNoteNo)
                .Map(dest => dest.DS_EstatePostalCode, src => src.EstateInquiry.EstatePostalCode)
                .Map(dest => dest.DS_EstateSection, src =>  src.EstateInquiry.EstateSection.Title )
                .Map(dest => dest.DS_EstateSeridaftar, src => !string.IsNullOrWhiteSpace(src.EstateInquiry.EstateSeridaftarId) ? src.EstateInquiry.EstateSeridaftar.Title : "")
                .Map(dest => dest.DS_EstateSubsection, src => src.EstateInquiry.EstateSubsection.Title)
                .Map(dest => dest.DS_GeoLocation, src => "")
                .Map(dest => dest.DS_UniqueNo, src => src.No)
                .Map(dest => dest.DS_TransactionDate, src => src.TransactionDate)
                .Map(dest => dest.DS_AttachedText, src => src.AttachedText)
                .Map(dest => dest.DS_Description, src => src.Description)
                .Map(dest => dest.DS_RemoveRestrictionNo, src => src.RemoveRestrictionNo)
                .Map(dest => dest.DS_RemoveRestrictionDate, src => src.RemoveRestrictionDate)
                .Map(dest => dest.DS_RemoveRestrictionType, src => (src.UnrestrictionType!=null) ? src.UnrestrictionType.Title:"")
                .Map(dest => dest.DS_MortgageText, src => src.EstateInquiry.MortgageText)                
                .Map(dest => dest.DS_PageNo, src => src.EstateInquiry.PageNo)
                .Map(dest => dest.DS_RegisterNo, src => src.EstateInquiry.RegisterNo)
                .Map(dest => dest.DS_Secondary, src => src.EstateInquiry.Secondary)
                .Map(dest => dest.DS_SecondaryRemaining, src => src.EstateInquiry.SecondaryRemaining.ToBoolean())
                .Map(dest => dest.DS_Unit, src => "")
                .Map(dest => dest.DS_Scriptorium, src => "")                
                .Map(dest => dest.DS_SendDate, src => src.SendDate)
                .Map(dest => dest.DS_SendTime, src => src.SendTime)
                .Map(dest => dest.DS_Id, src => src.Id.ToString())
                .Map(dest => dest.DS_Ilm, src => src.Ilm)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsValid, src => true)                               
                .Map(dest => dest.DS_Response, src => src.Response)
                .Map(dest => dest.DS_ResponseDate, src => src.ResponseDate)                
                .Map(dest => dest.DS_ResponseNumber, src => src.ResponseNumber)                
                .Map(dest => dest.DS_ResponseTime, src => src.ResponseTime)
                .Map(dest => dest.EstateInquiry, src => $"استعلام شماره {src.EstateInquiry.InquiryNo} مورخ {src.EstateInquiry.InquiryDate}")
                .Map(dest => dest.TransferType, src => src.DealSummaryTransferType.Title)
                .Map(dest => dest.IsRestricted, src => src.DealSummaryTransferType.Isrestricted.ToBoolean())
                .Map(dest => dest.Status, src => src.WorkflowStates.State)
                .Map(dest => dest.StatusTitle, src => src.WorkflowStates.Title)
                .Map(dest => dest.DS_DocPrintNo, src => src.EstateInquiry.DocPrintNo)
                .Map(dest => dest.IsValid, src => true)
                
                ;

            config.Compile();

            var viewModel = entity.Adapt<DealSummaryViewModel>(config);
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;
        }

        public static DealSummaryPersonViewModel EntityToViewModel(Domain.Entities.DealSummaryPerson entity)
        {
            var config = new TypeAdapterConfig();


            config.NewConfig<Domain.Entities.DealSummaryPerson, DealSummaryPersonViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.PersonTimestamp, src => src.Timestamp)
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonCity, src => src.CityId.HasValue ? src.CityId.Value.ToString() : "")
                .Map(dest => dest.PersonExecutiveTransfer, src => src.DealSummary.EstateInquiry.EstateInquiryPeople.First().ExecutiveTransfer.ToBoolean())
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonIssuePlace, src => src.IssuePlaceId.HasValue ? src.IssuePlaceId.Value.ToString() : "")
                .Map(dest => dest.PersonName, src => src.Name + (!string.IsNullOrWhiteSpace(src.Family) ? " " + src.Family : ""))
                .Map(dest => dest.PersonNationalityCode, src => src.NationalityCode)
                .Map(dest => dest.PersonNationality, src => src.NationalityId.HasValue ? src.NationalityId.Value.ToString() : "")
                .Map(dest => dest.PersonBirthPlace, src => src.BirthPlaceId.HasValue ? src.BirthPlaceId.Value.ToString() : "")
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerialNo, src => src.SerialNo)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonSharePart, src => src.SharePart)
                .Map(dest => dest.PersonShareText, src => src.ShareText)
                .Map(dest => dest.PersonShareTotal, src => src.ShareTotal)
                .Map(dest => dest.PersonIlm, src => src.Ilm)
                .Map(dest => dest.PersonTimestamp, src => src.Timestamp)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.PersonOctantQuarter, src => src.OctantQuarter)
                .Map(dest => dest.PersonOctantQuarterPart, src => src.OctantQuarterPart)
                .Map(dest => dest.PersonOctantQuarterTotal, src => src.OctantQuarterTotal)
                .Map(dest => dest.PersonConditionText, src => src.ConditionText)
                .Map(dest => dest.PersonRelationType, src => src.RelationType.Title)
                .Map(dest => dest.PersonSeriAlpha, src => src.SeriAlpha)
                ;

            config.Compile();


            var viewModel = entity.Adapt<DealSummaryPersonViewModel>(config);
            Helper.NormalizeStringValues(viewModel, false);
            return viewModel;

        }

        

    }
}