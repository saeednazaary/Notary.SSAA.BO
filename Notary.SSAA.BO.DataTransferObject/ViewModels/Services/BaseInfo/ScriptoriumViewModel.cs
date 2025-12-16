using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo
{
    public class ScriptoriumViewModel
    {
        public ScriptoriumViewModel()
        {
            ScriptoriumList = new();
        }
        public List<ScriptoriumItem> ScriptoriumList { get; set; }

    }
    public class ScriptoriumItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string ProvinceId { get; set; }
        public string GeoLocationId { get; set; }
        public string ScriptoriumNo { get; set; }
        public string GeoLocationName { get; set; }
        public string Address { get; set; }
        public string ExordiumFullName { get; set; }
        public string Tel { get; set; }
    }
}
