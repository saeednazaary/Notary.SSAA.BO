using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class FinalVerificationOutputMessage 
    {
        public dynamic esPerson { get; set; }
        public dynamic sabtAhvalExtraData { get; set; }
        public string PersonIssueLocation { get; set; }
        public List<dynamic> circularItems { get; set; }
        public List<string> imageReadyCirculars { get; set; }
        public List<DocumentPerson> personsList { get; set; }
        public int totalItemsCount { get; set; }
        public string SabtAhvalErrors { get; set; }

        public bool IsPersonsListPermitted { get; set; }
        public string PersonsValidationMessage { get; set; }


        public byte [ ] circularImage { get; set; }
        // for sanaaccount.
        public bool IsSanaAccount { get; set; }
        public string MobileNumber { get; set; }


    }
}
