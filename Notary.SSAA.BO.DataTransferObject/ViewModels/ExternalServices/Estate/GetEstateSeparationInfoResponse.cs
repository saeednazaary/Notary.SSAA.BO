namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class GetEstateSeparationInfoResponse 
    {
        public GetEstateSeparationInfoResponse()
        {
            Data = new EstateSeparationInfo();
        }
        public EstateSeparationInfo Data { get; set; }
    }
    public class EstateSeparationInfo
    {
        public decimal AppartmentsTotalArea { get; set; }
        public string SeparationDate { get; set; }
        public string SeparationNo { get; set; }
        public string ErrorMessage { get; set; }
        public string RequestDateTime { get; set; }
        public string ResponseDateTime { get; set; }
        public string ResponseNo { get; set; }
        public bool Successful { get; set; }
    }
}
