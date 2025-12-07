using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry
{
    public class UpdateEstateInquiryCommand : BaseCommandRequest<ApiResult>
    {
        public string State { get; set; }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }

        public string InqId { get; set; }


        public string InqBasic { get; set; } = null!;


        public bool InqBasicRemaining { get; set; }


        public string InqSecondary { get; set; } = null!;


        public bool InqSecondaryRemaining { get; set; }


        public decimal? InqArea { get; set; }

        public string InqCreateDate { get; set; }


        public string InqCreateTime { get; set; } = null!;


        public string InqDealSummaryDate { get; set; }


        public string InqDealSummaryNo { get; set; }


        public string InqDealSummaryScriptorium { get; set; }

        public string InqSpecificStatus { get; set; }

        public string InqDocPrintNo { get; set; } = null!;


        public bool InqDocumentIsNote { get; set; }


        public bool InqDocumentIsReplica { get; set; }

        public string InqEdeclarationNo { get; set; }


        public string InqElectronicEstateNoteNo { get; set; }

        public string InqEstatePostalCode { get; set; }


        public string InqEstateNoteNo { get; set; }


        public string InqFirstSendDate { get; set; }


        public string InqFirstSendTime { get; set; }


        public string InqInquiryDate { get; set; } = null!;


        public string InqInquiryNo { get; set; } = null!;


        public string InqInquiryPaymantRefno { get; set; }


        public string InqLastSendDate { get; set; }


        public string InqLastSendTime { get; set; }

        public string InqMobileNo { get; set; }


        public string InqMortgageText { get; set; }


        public string InqNo { get; set; } = null!;

        public string InqNote21PaymentRefno { get; set; }


        public string InqPageNo { get; set; }


        public string InqRegisterNo { get; set; }


        public string InqResponse { get; set; }

        public string InqResponseDate { get; set; }


        public string InqResponseTime { get; set; }


        public byte[] InqResponseDigitalsignature { get; set; }


        public string InqResponseNumber { get; set; }

        public string InqResponseResult { get; set; }


        public string InqResponseSubjectdn { get; set; }

        public IList<string> InqEstateInquiryTypeId { get; set; } = null!;


        public IList<string> InqEstateSectionId { get; set; } = null!;


        public IList<string> InqEstateSeridaftarId { get; set; }


        public IList<string> InqEstateSubsectionId { get; set; } = null!;


        public IList<string> InqEstateInquiryId { get; set; }


        public IList<string> InqGeoLocationId { get; set; }


        public IList<string> InqScriptoriumId { get; set; } = null!;


        public IList<string> InqUnitId { get; set; } = null!;


        public decimal InqTimestamp { get; set; }


        public string InqIlm { get; set; }

        public EstateInquiryPersonViewModel InqInquiryPerson { get; set; }



    }
}
