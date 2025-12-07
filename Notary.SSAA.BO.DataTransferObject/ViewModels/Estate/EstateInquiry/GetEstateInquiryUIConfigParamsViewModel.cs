using Microsoft.Extensions.Options;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry
{
    public class GetEstateInquiryUIConfigParamsViewModel
    {        
        public List<ConfigValue> ConfigValues { get; set; }
    }
    public class ConfigValue
    {
        public string ConfigName { get; set; }
        public string Value { get; set; }
    }
}
