using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class FinalVerificationInputMessage
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DocumentPerson thOnePersonInput { get; set; }
        public DocumentPerson ThOneDocReqPersonInput { get; set; }
        public InputPerson CustomInputPerson { get; set; }
        public List<DocumentPerson> personsListInput { get; set; }

        public bool SabtAhvalValidation { get; set; }
        public bool CircularLogging { get; set; }

        public string CircularID { get; set; }

        public SabtAhvalRequestPacket TheSabtAhvalRequestPacket { get; set; }
        //for controll sanaAccount.
        public SanaInput TheSanaRequestPacket { get; set; }
        public bool CompareUsingSabtAhvalData { get; set; }
    }
    public class SanaInput 
    {
        public string Person { get; set; }
        public string NationalityCode { get; set; }
        public int personType { get; set; }
    }
}
