using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ReportTools;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ReportTools
{
    public class SignRequestGenerateReportServiceInput : BaseExternalRequest<ApiResult<ReportResult>>
    {
        public SignRequestGenerateReportServiceInput()
        {
            Persons = new List<SignRequestPersonPrintViewModel>();
            SignRequestPersons = new List<SignRequestPersonsPrintViewModel>();
        }
        public string SecretCode { get; set; }
        public string SignNationalNo { get; set; }
        public string SignSubjectType { get; set; }
        public string SignGetter { get; set; }
        public string SignDate { get; set; }
        public string SardaftarNameFamily { get; set; }
        public string ScriptoriumCode { get; set; }
        public string ScriptoriumLocation { get; set; }
        public string ScriptoriumAddress { get; set; }
        public string ScriptoriumTell { get; set; }
        public string Title { get; set; }
        public string ScriptoriumName { get; set; }
        public string LegacyCode { get; set; }
        //public string KafilNameFamily { get; set; }
        public string Desc { get; set; }
        public string LegalText { get; set; }
        public string ScriptoriumId { get; set; }
        public byte[] SignPics { get; set; }
        public string MatrixBarcode { get; set; }
        public IList<SignRequestPersonPrintViewModel> Persons { get; set; }
        public IList<SignRequestPersonsPrintViewModel> SignRequestPersons { get; set; }
    }
}
