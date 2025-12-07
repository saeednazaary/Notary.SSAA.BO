

namespace Notary.SSAA.BO.SharedKernel.AppSettingModel
{
    public class EPaymentCredentialsModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
        public string Serial { get; set; }
    }
    public class BOApisPrefix
    {
        public string SSAR_BO_APIS_Prefix { get; set; }
        public string CIRCULAR_BO_APIS_Prefix { get; set; }
        public string EXORDIUM_BO_APIS_Prefix { get; set; }
        public string EXTERNAL_BO_APIS_Prefix { get; set; }
        public string MEDIAMANAGER_APIS_Prefix { get; set; }
        public string BASEINFO_APIS_Prefix { get; set; }
    }    
    public class ValidateInquiryForDealSummaryServiceUser
    {
        public string UserName { get; set; }
        public string Password { set; get; }
    }
    public class WrappersApiUser
    {
        public string UserName { get; set; }
        public string Password { set; get; }
    }
    public class LoggingOptions
    {
        public bool LogRequestBody { get; set; } = false;
        public bool LogRequestQuery { get; set; } = false;
        public bool StackTrace { get; set; } = false;
        public bool IncludeExceptionInResponse { get; set; } = false;

    }
}
