namespace Notary.SSAA.BO.DataTransferObject.Bases
{
    public class EntityState
    {
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public bool IsLoaded { get; set; }
    }
}
