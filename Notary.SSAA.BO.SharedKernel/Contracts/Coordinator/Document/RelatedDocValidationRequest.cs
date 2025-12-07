using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    public class RelatedDocValidationRequest
    {
        public string RelatedDocumentNationalNo { get; set; }

        public string RelatedDocumentScriptoriumId { get; set; }

        public string RelatedDocumentDate { get; set; }

        public string DocumentTypeID { get; set; }
    }
    public class RelatedDocumentValidationRequest
    {
        public bool SMSIsRequired { get; set; }
        public string DocumentNationalNo { get; set; }
        public string DocumentScriptoriumId { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentTypeID { get; set; }
        public string MainObjectID { get; set; }
        public Enumerations.YesNo IsRelatedDocumentInSSAR { get; set; }
    }

}