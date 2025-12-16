using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractVehicleViewModel : EntityState
    {
        public DocumentStandardContractVehicleViewModel()
        {

        }
        public string RowNo { get; set; }
        public string VehicleId { get; set; }

        public string DocumentId { get; set; }

        public bool? IsInTaxList { get; set; }

        public bool? MadeInIran { get; set; }
        public string MadeInYear { get; set; }


        public string Type { get; set; }

        public string System { get; set; }


        public string Tip { get; set; }

        public string Model { get; set; }

        public string EngineNo { get; set; }


        public string ChassisNo { get; set; }


        public string EngineCapacity { get; set; }

        public string Color { get; set; }

        public string CylinderCount { get; set; }

        public string CardNo { get; set; }


        public string DutyFicheNo { get; set; }

        public string FuelCardNo { get; set; }


        public string OtherInfo { get; set; }


        public string InssuranceCo { get; set; }


        public string InssuranceNo { get; set; }


        public string OwnershipPrintedDocumentNo { get; set; }

        public string OldDocumentNo { get; set; }

        public string OldDocumentIssuer { get; set; }


        public string OldDocumentDate { get; set; }

        public bool? IsVehicleNumbered { get; set; }

        public string NumberingLocation { get; set; }

        public string PlaqueNo1Seller { get; set; }

        public string PlaqueNo2Seller { get; set; }

        public string PlaqueSeriSeller { get; set; }

        public string PlaqueNoAlphaSeller { get; set; }

        public string PlaqueSeller { get; set; }

        public string PlaqueNo1Buyer { get; set; }


        public string PlaqueNo2Buyer { get; set; }


        public string PlaqueSeriBuyer { get; set; }

        public string PlaqueNoAlphaBuyer { get; set; }

        public string PlaqueBuyer { get; set; }

        public string Price { get; set; }

        public string OwnershipType { get; set; }

        public string OwnershipDetailQuota { get; set; }
        public string OwnershipTotalQuota { get; set; }

        public string SellDetailQuota { get; set; }

        public string SellTotalQuota { get; set; }
        public string Description { get; set; }
        public string QuotaText { get; set; }
        public string DocumentVehicleTypeTitle { get; set; }
        public string DocumentVehicleSystemTitle { get; set; }
        public string DocumentVehicleTipTitle { get; set; }
        public IList<string> DocumentVehicleTypeId { get; set; }
        public IList<string> DocumentVehicleSystemId { get; set; }
        public IList<string> DocumentVehicleTipId { get; set; }
        public IList<DocumentStandardContractVehicleQuotumViewModel> DocumentVehicleQuotums { get; set; }
        public IList<DocumentStandardContractVehicleQuotaDetailViewModel> DocumentVehicleQuotaDetails { get; set; }

    }

    public partial class DocumentStandardContractVehicleQuotumViewModel : EntityState
    {

        public string DocumentVehicleQuotumId { get; set; }

        public string DocumentVehicleId { get; set; }

        public IList<string> DocumentPersonId { get; set; }

        public string DetailQuota { get; set; }

        public string TotalQuota { get; set; }

        public string QuotaText { get; set; }


    }

    public partial class DocumentStandardContractVehicleQuotaDetailViewModel : EntityState
    {
    
        public string DocumentVehicleQuotaDetailId { get; set; }

        public string DocumentVehicleId { get; set; }

        public IList<string> DocumentPersonSellerId { get; set; }

        public IList<string> DocumentPersonBuyerId { get; set; }

        public string OwnershipDetailQuota { get; set; }

        public string OwnershipTotalQuota { get; set; }

        public string SellDetailQuota { get; set; }

        public string SellTotalQuota { get; set; }

        public string QuotaText { get; set; }

   
    }

}
