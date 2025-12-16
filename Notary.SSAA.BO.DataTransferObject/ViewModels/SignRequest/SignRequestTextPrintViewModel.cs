

using Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestTextPrintViewModel
    {
        public SignRequestTextPrintViewModel()
        {
            Persons = new List<PersonEntity>();
            SignablePerson = new List<Signables>();
        }
        public IList<PersonEntity> Persons { get; set; }
        public IList<Signables> SignablePerson { get; set; }
        public string ScriptoriumCode { get; set; }
        public string ScriptoriumNo { get; set; }
        public string ScriptoriumLocation { get; set; }
        public string ScriptoriumAddress { get; set; }
        public string ScriptoriumTell { get; set; }
        public string Title { get; set; }
        public string ScriptoriumName { get; set; }
        public string LegacyCode { get; set; }
        public string Desc { get; set; }
        public string Text { get; set; }
        public string KafilNameFamily { get; set; }
        public bool FinalPrint { get; set; }
        public string SignGetter { get; set; }
    }
    public class PersonEntity
    {
        public string PersonDescription { get; set; }
        public string IsOriginalPerson { get; set; }
        public string IsIranian { get; set; }
        public string SexType { get; set; }
        public string IsRelated { get; set; }
    }

    public class Signables
    {
        public string FullName { get; set; }
    }
}
