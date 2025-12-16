using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class RealForeignerServiceViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
       
        public string BirthDate { get; set; }
        public string IdentityNo { get; set; }

        public string SexType { get; set; }
        public string NationalityId { get; set; }
        public bool IsForeignerServiceEnabled { get; set; }
    }
}
