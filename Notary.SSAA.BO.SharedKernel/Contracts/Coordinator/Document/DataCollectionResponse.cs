using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    public class DataCollectionResponsePacket
    {
        public bool Succeeded { get; set; }
        public string ServiceResponse { get; set; }
        public string Context { get; set; }
    }
}
