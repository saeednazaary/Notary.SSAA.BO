using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.Estate
{
    public class DSUDealSummarySignPacket
    {
        public string InquiryObjectID { get; set; }

        /// <summary>
        /// The MainObjectID By Which The DSU Is Being Generated
        /// </summary>
        public string RegisterServiceReqObjectID { get; set; }

        /// <summary>
        /// The Base64 Format Of Generated DSU. This Is Being Calculated Using DSUService
        /// </summary>
        public string RawDataB64 { get; set; }

        /// <summary>
        /// The Signed Format Of RawB64
        /// </summary>
        public string SignB64 { get; set; }

        /// <summary>
        /// The Signature Certificate By Which The DSU Is Being Signed
        /// </summary>
        public string CertificateB64 { get; set; }

        /// <summary>
        /// If The Current Restricted DSU Has Any Failed Deterministic DSU Log In Database, 
        /// which its main Deterministic Object Is Being Finalized, 
        /// This Flag Is Being Set To Paint Current Generated DSUPacket, as FAILEDDSU
        /// Failed DSUs Should Be Sent By System Right Before Sending Deterministic DSU
        /// </summary>
        public bool FailedOldDeterministicDSU { get; set; }

        public byte [ ] RawDataByteArray { get; set; }
    }
}
