namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService
{
    public class GetScriptoriumByIdViewModel
    {
        public GetScriptoriumByIdViewModel()
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
        public string ScriptoriumNo { get; set; }
        public string GeoLocationName { get; set; }
        public string Address { get; set; }
        public string ExordiumFullName { get; set; }
        public string Tel { get; set; }
        public string LegacyId { get; set; }
        public UnitData Unit { get; set; }
    }
}
