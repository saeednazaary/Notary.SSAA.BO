using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class EstateBuyerLookupItem
    {
        public string DocumentNationalNo { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonNationalCode { get; set;}
        public string EstateUnit { get; set;}
        public string EstateSection { get; set; }
        public string EstateSubSection { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }        
        public string Id { get; set; }
        public bool PersonIsBuyer { get; set; }
        public bool PersonIsSeller { get; set; }
        public bool IsAttchmentTransfer { get; set; }

    }
}
