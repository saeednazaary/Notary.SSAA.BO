using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentElectronicBookDataToSignViewModel
    {
        public List<string> TheDigitalBookHashedDataCollection { get; set; }
    }
}
