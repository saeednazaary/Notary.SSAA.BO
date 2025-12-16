

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestAffidavitViewModel: DocumentyVerificationResponseBase
    {
        public string NationalRegisterNo { get; set; }
        public string DocType { get; set; }
        public string Role { get; set; }
        public byte[] DocImage { get; set; }
        public List<Output2> lstFindPersonInQuery { get; set; }
        public string ScriptoriumName { get; set; }
        public string SignGetterTitle { get; set; }
        public string SignSubject { get; set; }
        public string DocDate { get; set; }
        public string CaseClasifyNo { get; set; }
        public string ImpotrtantAnnexText { get; set; }
    }
    public class DocumentyVerificationResponseBase
    {
        public bool succseed { get; set; }
        public bool HasPermission { get; set; }
        public bool ExistDoc { get; set; }
        public string Desc { get; set; }
    }
    public class Output2
    {
        public string Name { get; set; }
        public string NationalNo { get; set; }
        public string Family { get; set; }
        public string AgentType { get; set; }
        public string PersonType { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public string NameMovakel { get; set; }
        public string FamilyMovakel { get; set; }
        public string AddressMovakel { get; set; }
        public string PostalCodeMovakel { get; set; }
        public string TelMovakel { get; set; }
        public string MobileMovakel { get; set; }
        public string NationalNoMovakel { get; set; }
        public string txtRelation { get; set; }
        public string RpleType { get; set; }
        public string Role { get; set; }
        public byte[] FingerPrintImage { get; set; }

    }
}
