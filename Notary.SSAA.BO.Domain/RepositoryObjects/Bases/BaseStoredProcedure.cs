namespace Notary.SSAA.BO.Domain.RepositoryObjects.Bases
{
    public class BaseStoredProcedure
    {
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
        public int EffectedRows { get; set; }
    }
}
