using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint
{
    public class GetPersonFingerprintMocRelatedDataViewModel
    {
        public byte[] FingerPrint { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public int Resolution { get; set; }
        public int FingerIndex { get; set; }
    }
}
