namespace Notary.SSAA.BO.SharedKernel.SharedModels
{
    public class OrganizationHierarchyViewModel
    {
        public OrganizationHierarchyViewModel ( )
        {
            OrganizationHierarchy = new List<OrganizationInformation> ();
        }
        public List<OrganizationInformation> OrganizationHierarchy { get; set; }
    }
    public class OrganizationInformation
    {
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string UnitId { get; set; }        
      
    }
}
