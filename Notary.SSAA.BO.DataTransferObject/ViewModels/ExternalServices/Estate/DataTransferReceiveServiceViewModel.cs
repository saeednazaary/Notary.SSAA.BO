namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class DataTransferReceiveServiceViewModel
    {
        public string ErrorMessage { get; set; }
        public string RequestDateTime { get; set; }
        public string ResponseDateTime { get; set; }
        public bool Successful { get; set; }

    }
}
