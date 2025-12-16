using Notary.SSAA.BO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class ONotaryRegisterServiceReqOutputMessage 
    {
        public Document registerServiceReq { get; set; }
        public bool Message { get; set; }
        public bool CurrentActionResult { get; set; }
        public bool ClientConfirmationIsNeeded { get; set; }
        public bool ClientRawTextSignIsNeeded { get; set; }
        public bool ReSignIsNeeded { get; set; }
        public int SignCounter { get; set; }
        public bool ShowServerTransactionLog { get; set; }
        public string VerificationExceptionMessage { get; set; }
        public string MainObjectHashedDataToSign { get; set; }
        public byte [ ] DocImage { get; set; }
        public string MessageText { get; set; }
    }
}
