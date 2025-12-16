namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService
{
    public class MeasurementUnitTypeByIdViewModel
    {
        public MeasurementUnitTypeByIdViewModel()
        {
            MesurementUnitTypeList = new List<MesurementUnitTypeData>();
        }
        public List<MesurementUnitTypeData> MesurementUnitTypeList { get; set; }
    }
    public class MesurementUnitTypeData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LegacyId { get; set; }
    }
}
