using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class UnitIdentity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RequestOrganization { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
