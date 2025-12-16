using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractInfoConfirmViewModel : EntityState
    {

        public string DocumentId { get; set; }
        public string DocumentInfoConfirmId { get; set; }

        public string CreatorNameFamily { get; set; }

        public string CreateDate { get; set; }


        public string CreateTime { get; set; }
        public string SardaftarNameFamily { get; set; }
        public string SardaftarConfirmTime { get; set; }
        public string SardaftarConfirmDate { get; set; }
        public string DaftaryarNameFamily { get; set; }
        public string DaftaryarConfirmDate { get; set; }
        public string DaftaryarConfirmTime { get; set; }

    }
}
