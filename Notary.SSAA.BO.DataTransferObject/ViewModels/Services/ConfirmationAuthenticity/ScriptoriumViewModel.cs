namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity
{
    public class ScriptoriumViewModel
    {
        public ScriptoriumViewModel()
        {
            ScriptoriumList = new List<ScriptoriumData>();
        }
        public List<ScriptoriumData> ScriptoriumList { get; set; }
    }
    public class ScriptoriumData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string GeoLocationId { get; set; }
    }
}
