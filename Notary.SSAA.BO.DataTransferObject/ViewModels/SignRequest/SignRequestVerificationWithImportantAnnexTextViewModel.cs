

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestVerificationWithImportantAnnexTextViewModel : DocumentyVerificationResponseBase
    {
        public SignRequestVerificationWithImportantAnnexTextViewModel()
        {
            RegCases = new List<RegCase>();
            FollowerDocuments = new List<FollowerDocument>();
            BaseDocuments = new List<BaseDocument>();
            lstFindPersonInQuery = new List<OutputEconomic>();
        }
        public string NationalRegisterNo { get; set; }
        public string DocType { get; set; }
        public string DocType_code { get; set; }
        public string ScriptoriumName { get; set; }
        public string SignGetterTitle { get; set; }
        public string SignSubject { get; set; }
        public string DocDate { get; set; }
        public string CaseClasifyNo { get; set; }
        public string ImpotrtantAnnexText { get; set; }
        public byte[] DocImage { get; set; }
        public string DocImage_Base64 { get; set; }
        public string ADVOCACYENDDATE { get; set; }
        public List<RegCase> RegCases { get; set; }
        public List<BaseDocument> BaseDocuments { get; set; }
        public List<FollowerDocument> FollowerDocuments { get; set; }
        public List<OutputEconomic> lstFindPersonInQuery { get; set; }
    }
    public class BaseDocument
    {
        public string baseDOCUMENTNO { get; set; }
        public string baseDOCUMENTSECRETCODE { get; set; }

    }
    public class RegCase
    {
        public string VEHICLEPLAQUESELLER { get; set; }
        public string VEHICLEPLAQUESELLER_NUMERIC_STD { get; set; }
        public string VEHICLEPLAQUEBUYER { get; set; }
        public string VEHICLEPLAQUEBUYER_NUMERIC_STD { get; set; }
    }
    public class FollowerDocument
    {
        public string FollowerDocumentNO { get; set; }
        public string FollowerDocumentSECRETCODE { get; set; }
    }
    public class OutputEconomic
    {
        public string NationalNo { get; set; }
        public string Birthdate { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string AgentType { get; set; }
        public string PersonType { get; set; }
        public string PersonType_code { get; set; }
        public string NationalNoMovakel { get; set; }
        public string NameMovakel { get; set; }
        public string FamilyMovakel { get; set; }
        public string txtRelation { get; set; }
        public string RoleType { get; set; }
        public string Person_RoleType_code { get; set; }
    }
}
