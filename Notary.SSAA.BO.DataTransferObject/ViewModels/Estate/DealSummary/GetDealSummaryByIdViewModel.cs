using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary
{

    public class DealSummaryExtraParam
    {
        public DealSummaryExtraParam()
        {
            Message = string.Empty;
            PopupMessage = string.Empty;
        }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSend { get; set; }
        public bool CanUnRestrict { get; set; }
        
        public string Message { get; set; }
        public string PopupMessage { get; set; }
        public string RelatedServer { get; set; }
    }

    public class DealSummaryViewModel : EntityState
    {

        public DealSummaryExtraParam ExtraParams { get; set; }
        public string Status { get; set; }
        public string StatusTitle { get; set; }
        public bool IsRestricted { get; set; }
        public string TransferType { get; set; }
        public string EstateInquiry { get; set; }
        public string DS_Id { get; set; }


        public string DS_Basic { get; set; } = null!;


        public bool DS_BasicRemaining { get; set; }


        public string DS_Secondary { get; set; } = null!;


        public bool DS_SecondaryRemaining { get; set; }


        public decimal? DS_Area { get; set; }
        
        public string DS_Date { get; set; }

        public string DS_No { get; set; }

        public string DS_DocPrintNo { get; set; } = null!;

        public bool DS_DocumentIsNote { get; set; }

        public bool DS_DocumentIsReplica { get; set; }

        public string DS_EdeclarationNo { get; set; }

        public string DS_ElectronicEstateNoteNo { get; set; }

        public string DS_EstatePostalCode { get; set; }

        public string DS_EstateNoteNo { get; set; }

        public string DS_SendDate { get; set; }


        public string DS_SendTime { get; set; }

        public string DS_MortgageText { get; set; }


        public string DS_UniqueNo { get; set; } = null!;


        public string DS_TransactionDate { get; set; }


        public string DS_PageNo { get; set; }


        public string DS_RegisterNo { get; set; }


        public string DS_Response { get; set; }


        public string DS_ResponseDate { get; set; }


        public string DS_ResponseTime { get; set; }

        public string DS_ResponseNumber { get; set; }

        public string DS_Description { get; set; }

        public string DS_AttachedText { get; set; }

        public string DS_Amount { get; set; }

        public string DS_AmountType { get; set; }

        public string DS_Duration { get; set; }

        public string DS_DurationType { get; set; }

        public string DS_RemoveRestrictionNo { get; set; }

        public string DS_RemoveRestrictionDate { get; set; }

        public string DS_RemoveRestrictionType { get; set; }



        public string DS_EstateSection { get; set; }


        public string DS_EstateSeridaftar { get; set; }


        public string DS_EstateSubsection { get; set; }

        public string DS_GeoLocation { get; set; }


        public string DS_Scriptorium { get; set; }


        public string DS_Unit { get; set; } = null!;


        public decimal DS_Timestamp { get; set; }


        public string DS_Ilm { get; set; }

        public List<DealSummaryPersonViewModel> DS_Persons { get; set; }
    }

    public class DealSummaryPersonViewModel : EntityState
    {

        public string PersonId { get; set; }

        public string PersonRelationType { get; set; }

        public string PersonName { get; set; } = null!;

        public string PersonConditionText { get; set; }

        public string PersonOctantQuarter { get; set; }

        public string PersonOctantQuarterPart { get; set; }

        public string PersonOctantQuarterTotal { get; set; } = null!;
        
        public string PersonFatherName { get; set; }

        public string PersonIdentityNo { get; set; }

        public string PersonBirthDate { get; set; }

        public string PersonNationalityCode { get; set; }

        public string PersonSeri { get; set; }

        public string PersonSerialNo { get; set; }

        public string PersonSeriAlpha { get; set; }

        public decimal? PersonSharePart { get; set; }


        public string PersonShareText { get; set; }


        public decimal? PersonShareTotal { get; set; }

        public string PersonIssuePlace { get; set; }

        public string PersonSexType { get; set; }

        public string PersonType { get; set; }

        public string PersonPostalCode { get; set; } = null!;

        public string PersonAddress { get; set; } = null!;

        public bool PersonExecutiveTransfer { get; set; }

        public string PersonNationality { get; set; }

        public string PersonBirthPlace { get; set; }
        public string PersonCity { get; set; }

        public decimal PersonTimestamp { get; set; }

        public string PersonIlm { get; set; }              
        public string PersonImage { get; set; }
      
    }

}
