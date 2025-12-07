using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
     public class PersonsVerificationResponse
    {
        public List<DocumentPerson> VerifiedPersonsCollection { get; internal set; }

        public bool IsResponseValid { get; internal set; }

        public string ValidationMessage { get; internal set; }

        public string SabtAhvalErrorMessage { get; internal set; }
    }
}
