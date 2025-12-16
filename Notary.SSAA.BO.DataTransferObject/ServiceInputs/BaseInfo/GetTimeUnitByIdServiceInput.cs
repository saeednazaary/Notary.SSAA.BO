namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo
{
    public class GetTimeUnitByIdServiceInput
    {
        public GetTimeUnitByIdServiceInput()
        {
            IdList = new();
        }
        public List<string> IdList { get; set; }
    }
}
