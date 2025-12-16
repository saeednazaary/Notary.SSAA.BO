using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Bases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractTypeViewModel
    {

        public string DocumentTypeId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string DocumentTypeGroupOneCode { get; set; }
        public string DocumentTypeGroupTwoCode { get; set; }


        public bool? IsSupportive { get; set; }


        public bool? HasAsset { get; set; }


        public bool? AssetIsRequired { get; set; }


        public bool? WealthType { get; set; }


        public string CaseTitle { get; set; }


        public bool? DocumentTextWritingAllowed { get; set; }


        public bool? HasRelatedDocument { get; set; }


        public bool? HasCount { get; set; }


        public bool? HasSubject { get; set; }


        public bool? SubjectIsRequired { get; set; }

        public bool? HasEstateInquiry { get; set; }


        public bool? EstateInquiryIsRequired { get; set; }


        public bool? HasNonregisteredEstate { get; set; }


        public bool? HasEstateAttachments { get; set; }


        public bool? HasAssetType { get; set; }


        public bool? AssetTypeIsRequired { get; set; }

        public string GeneralPersonPostTitle { get; set; }
        public string AssetTypeId { get; set; }
        public string DocumentTypeSubjectId { get; set; }

    }
}
