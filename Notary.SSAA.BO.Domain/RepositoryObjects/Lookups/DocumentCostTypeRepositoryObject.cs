namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public sealed class DocumentCostTypeLookupExtraParams
    {
        public DocumentCostTypeLookupExtraParams(List<string> costTypesId)
        {
            CostTypesId = costTypesId;
        }

        public List<string> CostTypesId { get; set; }
    }
}
