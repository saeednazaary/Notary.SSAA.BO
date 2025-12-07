using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.SharedKernel.SharedModels
{
    public class PrepareDocumentConfirmation
    {
        public List<DocumentElectronicBook> TheGeneratedDigitalBookEntityCollection { get; set; }
        public List<string> TheDigitalBookHashedDataCollection { get; set; }
        public Document TheCurrentReq { get; set; }
        public string TheCurrentReqSignText { get; set; }
        public List<DSUDealSummarySignPacket> DSUSignPacketCollection { get; set; }
        public bool ServiceExecutionSucceeded { get; set; }
        public string ServiceExecutionMessage { get; set; }
        public int ConfirmationTimeLimit { get; set; }
        public dynamic SignInfoDataGraph { get; set; }
        public string SDSCount { get; set; }
    }
    public class PrepareDocumentConfirmationRequest 
    {
        public string CertificateSign { get; set; }
        public string CurrentReqSign { get; set; }
        public string DigitalBookSignature { get; set; }
        public string CurrentReqObjectID { get; set; }
        public string CurrentActionType { get; set; }
        public List<DSUDealSummarySignPacket> SignedDSUDealSummaryCollection { get; set; }
        public DocumentElectronicBook TheCurrentDigitalBookEntity { get; set; }
        public Document TheCurrentReq { get; set; }
        public List<DocumentPerson> UnSignedPersonsList { get; set; }
       
        public List<string> UnSignedPersonsListIds { get; set; }

    }
}
