using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class ONotaryRegServiceInquiryInputMessage
    {


        public List<DSUDealSummarySignPacket> SignPacketCollection { get; set; }
        public Document registerServiceReq { get; set; }
        public Document regServiceInquiry { get; set; }
        public DocumentPerson docPerson { get; set; }
        public DocumentEstate regCase { get; set; }
        public DocumentEstateOwnershipDocument ownershipDoc { get; set; }
        public DocumentCost regServiceCost { get; set; }
        public byte[] SignedDataByte { get; set; }
        public string B64SignedData { get; set; }
        public string MainObjectHashedData { get; set; }
        public string ONotaryRegisterServiceReqID { get; set; }
        public string SelectedCertificate { get; set; }
        public string[] InquiryNumbersArray { get; set; }
        public List<string> NoteBookSeriList { get; set; }
        public List<string> ESTEStateInquiryIdCollection { get; set; }
        public List<DocumentPerson> UnSignedPersonsList { get; set; }

    }

    public class ONotaryRegServiceInquiryOutputMessage 
    {
        public List<DSUDealSummarySignPacket> SignPacketCollection { get; set; }
        public List<dynamic> DSUDealSummaryRawDataCollection { get; set; }
        public List<dynamic> DSUDealSummaryCollection { get; set; }

        public EstateInquiry esteStateInquiry { get; set; }
        public List<EstateInquiry> esteStateInquiryList { get; set; }
        public List<dynamic> esteStateRealPersonsList { get; set; }
        public List<dynamic> esteStateLegalPersonsList { get; set; }
        public List<dynamic> seriDaftarList { get; set; }

        public dynamic theOneCMSOrganization { get; set; }
        public dynamic theOneInquiryOrg { get; set; }
        public dynamic theCurrentScriptorium { get; set; }

        public bool InquiryAndONotaryDataVerified { get; set; }
        public string VerificationMessage { get; set; }
        public string MainObjectHashedData { get; set; }
        public string SeparationDSCount { get; set; }
    }

}