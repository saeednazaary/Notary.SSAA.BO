using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public  class ONotaryRegisterServiceReqInputMessage
    {

        public string EntityID { get; set; }
        public Document Entity { get; set; }
        public List<DocumentPerson> UnSignedPersonsList { get; set; }
        public string sign { get; set; }
        public string signCertificate { get; set; }
        public string signData { get; set; }
        public int SignCounter { get; set; }

        public List<DocumentElectronicBook> TheCurrentDigitalBookEntityCollection { get; set; }
        public List<string> DigitalBookSignatureCollection { get; set; }
        public List<string> DigitalBookIds { get; set; }
        public List<DSUDealSummarySignPacket> SignedDSUDealSummaryCollection { get; set; }



    }
}
