using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo
{
    public class ExordiumViewModel
    {
        public ExordiumViewModel()
        {
            exordium = new();
        }
        public ExordiumItem   exordium { get; set; }

    }
    public class ExordiumItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string MobileNo { get; set; }
        public string ScriptoriumId { get; set; }
        public string ScriptoriumCode { get; set; }

    }
}
