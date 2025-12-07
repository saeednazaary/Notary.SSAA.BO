using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ReportObjects.SignRequest
{
    public class SignRequestPrint
    {
        public SignRequestPrint()
        {
            SignPics = new List<SignRequestFilePrintViewModel>();
            SignRequestPersons = new List<SignRequestPersonPrintViewModel>();
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
        public string FinalPrint { get; set; }
        public string Desc { get; set; }
        public string LegalText { get; set; }
        public string KafilNameFamily { get; set; }
        public IList<SignRequestPersonPrintViewModel> SignRequestPersons { get; set; }
        public IList<SignRequestFilePrintViewModel> SignPics { get; set; }
    }
}
