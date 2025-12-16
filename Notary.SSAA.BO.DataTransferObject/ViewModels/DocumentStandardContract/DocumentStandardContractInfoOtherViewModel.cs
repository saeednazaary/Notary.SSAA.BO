using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractInfoOtherViewModel : EntityState
    {
        public string DocumentId { get; set; }
        public string DocumentInfoOtherId { get; set; }


        public string EndDateTime { get; set; }
        public string DocumentDescription { get; set; }


        public string AdvocacyEndDate { get; set; }


        public string RentDuration { get; set; }


        public string RentStartDate { get; set; }




        public string PeaceDuration { get; set; }




        public string DividedSectionsCount { get; set; }


        public string RegisterCount { get; set; }

        public int? VaghfType { get; set; }


        public int HagheEntefae { get; set; }


        public string PaperCount { get; set; }


        public string MortageDuration { get; set; }

        public string Title { get; set; }
        public string ApplicationReason { get; set; }
        public string ExecutiveTypeId { get; set; }
        public string XexecutiveId { get; set; }



        public string XexecutiveOldId { get; set; }


        public string ExecutiveReceiverUnitId { get; set; }


        public string RequestScriptoriumId { get; set; }

        public string RecordDate { get; set; }
        public bool HasTime { get; set; }

        public bool HasAdvocacyToOthersPermit { get; set; }


        public bool HasMultipleAdvocacyPermit { get; set; }

        public bool HasDismissalPermit { get; set; }

        public bool IsKadastr { get; set; }

        //public bool IsEstateRegistered { get; set; }

        public bool IsRelatedToDivisionCommission { get; set; }

        public bool IsDocumentBrief { get; set; }


        public bool IsRentWithSarghofli { get; set; }

        public bool IsPeaceForLifetime { get; set; }


        public string DocumentTypeSubjectTitle { get; set; }

        public IList<string> DocumentTypeSubjectId { get; set; }
        public IList<string> DocumentAssetTypeId { get; set; }
        public string DocumentTypeTitle { get; set; }

        public IList<string> MortageTimeUnitId { get; set; }





    }
}
