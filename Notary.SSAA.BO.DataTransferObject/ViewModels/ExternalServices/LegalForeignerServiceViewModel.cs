using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class LegalForeignerServiceViewModel
    {
        public string Name { get; set; }
        public string RegisterNo { get; set; }
        public string RegisterDate { get; set; }

        public string NationalityId { get; set; }

        public bool IsForeignerServiceEnabled { get; set; }
    }
}
