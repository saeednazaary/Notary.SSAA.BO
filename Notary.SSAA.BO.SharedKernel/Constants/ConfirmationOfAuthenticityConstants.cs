
namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public static class ConfirmationOfAuthenticityConstants
    {
        public static readonly string[] Special = { "0023", "0024", "0025" };
    }
    public static class ScriptoriumRequestConstant
    {
        //فراخوانی دفترخانه با ایدی
        public const string Scriptorium = "Common/GetScriptoriumById";
        public const string TimeUnitAddress = "Common/TimeUnit";
        public const string SignRequestBasicInfo = "SignRequest/SignRequestBasicInfo";

    }
    public static class ExordiumRequestConstant
    {
        
        public const string Exordium = "Common/GetExordiumByNationalNo";

    }
}
