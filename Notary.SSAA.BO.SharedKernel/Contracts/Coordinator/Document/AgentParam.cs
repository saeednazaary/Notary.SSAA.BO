using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    public class AgentParam
    {
        public AgentParam(string agentDocumentScriptoriumId, string agentDocumentNationalNo)
        {
            AgentDocumentScriptoriumId = agentDocumentScriptoriumId;
            AgentDocumentNationalNo = agentDocumentNationalNo;
        }

        public string AgentDocumentScriptoriumId { get; set; }
        public string AgentDocumentNationalNo { get; set; }
    }
}
