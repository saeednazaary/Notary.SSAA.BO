namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService
{
    public class GetUnitByIdViewModel
    {
        public GetUnitByIdViewModel()
        {
            UnitList = new List<UnitData>();
        }
        public List<UnitData> UnitList { get; set; }
    }
    public class UnitData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string No { get; set; }
        public string GeoLocationId { get; set; }
        public string LegacyId { get; set; }
        public string LevelCode { get; set; }
    }
}
