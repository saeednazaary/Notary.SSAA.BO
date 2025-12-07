using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment
{
    public class CreateInvoiceServiceInput: BaseExternalRequest<ApiResult<CreateInvoiceViewModel>>
    {
        public CreateInvoiceServiceInput()
        {
            Vat = new();
            Quotation = new();
        }
        public string UnitId { get; set; }
        public string BusinessObjectId { get; set; }
        public string UserNationalNo { get; set; }
        public string OrganizationId { get; set; }
        public string ProvinceId { get; set; }
        public string NationalNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExtraParam { get; set; }
        public List<Quotation> Quotation { get; set; }
        public string GeoCodeForest { get; set; }
        public string ReturnURL { get; set; }
        public Vat Vat { get; set; }
        public string ExordiumShebaSarDaftar { get; set; }
        public string ServiceName { get; set; }

    }

    public class Vat
    {
        public string CodeMelli { get; set; }
        public string FollowCode { get; set; }
    }

    public class Quotation
    {
        public string DetailPrice { get; set; }
        public string CostTypeID { get; set; }
        public string UnitID { get; set; }
        public string ForestShenasePay { get; set; }
    }

}
