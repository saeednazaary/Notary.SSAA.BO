using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.SharedModels
{
    public class GetOrganizationHierarchyQuery 
    {
        public GetOrganizationHierarchyQuery ( string scriptoriumId, string unitId )
        {
            ScriptoriumId = scriptoriumId;
            UnitId = unitId;

        }
        public string ScriptoriumId { get; set; }
        public string UnitId { get; set; }
    }
}
