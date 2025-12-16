using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    public class MessageOutputPacket
    {
        public bool MessageGenerated { get; set; }

        public List<string> TrmSMSIDCollection { get; set; }

        public string ResponseMessage { get; set; }

        public string OutputMessage { get; set; }
    }
}
