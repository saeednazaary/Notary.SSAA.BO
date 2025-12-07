using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class EstelamPersonInputMessage 
    {
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string fatherName { get; set; }
        public int shenasnameNo { get; set; }
        public NotaryAlphabetLetter shenasnamehLetter { get; set; }
        public string shenasnamehSeri { get; set; }
        public int shenasnamehSerial { get; set; }
        public string nationalCardSerialNo { get; set; }
        public object extraParam { get; set; }
        public string FormId { get; set; }

        public List<DocumentPerson> theDocPersonsList { get; set; }
    }
}
