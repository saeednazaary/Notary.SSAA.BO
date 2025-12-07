using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    public class DataCollectionResponse
    {
        public bool Succeeded { get; set; }
        public string ServiceMessage { get; set; }
        public string LegalText { get; set; }
    }
}
