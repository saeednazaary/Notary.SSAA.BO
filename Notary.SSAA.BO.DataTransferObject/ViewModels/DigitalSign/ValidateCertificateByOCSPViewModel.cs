using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign
{
    public class ValidateCertificateByOCSPViewModel
    {

        public string RevokeReason { get; set; }
        public bool IsValid { get; set; }


    }
}
