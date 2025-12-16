using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class SabtAhvalRequestPacket
    {
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string BackSerialNo { get; set; }
        public long ShenasnameNo { get; set; }
        public NotaryAlphabetLetter ShenasnamehLetter { get; set; }
        public string ShenasnamehSeri { get; set; }
        public int ShenasnamehSerial { get; set; }
    }
}
