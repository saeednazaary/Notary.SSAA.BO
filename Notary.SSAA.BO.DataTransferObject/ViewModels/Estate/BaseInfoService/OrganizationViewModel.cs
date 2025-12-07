namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService
{
    public class OrganizationViewModel
    {
        public OrganizationViewModel()
        {
            OrganizationList = new List<OrganizationData>();
        }
        public List<OrganizationData> OrganizationList { get; set; }
    }
    public class OrganizationData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }        
        public string LegacyId { get; set; }
        public string UnitId { get; set; }
        public string ScriptoriumId { get; set; }
    }
}
