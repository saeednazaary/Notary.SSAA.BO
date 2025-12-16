using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class GetExecutiveSupportByCaseNoQuery
    {
        public string XCaseNo { get; set; }
        public UnitIdentity unitIdentity { get; set; }
    }
}
