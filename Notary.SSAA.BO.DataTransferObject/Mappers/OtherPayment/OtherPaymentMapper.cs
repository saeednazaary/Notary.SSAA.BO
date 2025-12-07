using Mapster;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.OtherPayment;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.OtherPayment
{
    public static class OtherPaymentMapper
    {
        public static OtherPaymentViewModel ToDto(Domain.Entities.OtherPayment otherPayment)
        {
            TypeAdapterConfig<Domain.Entities.OtherPayment, OtherPaymentViewModel>
                .NewConfig()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .Map(dest => dest.CreateDate, src => src.CreateDate)
                .Map(dest => dest.CreateTime, src => src.CreateTime)
                .Map(dest => dest.Fee, src => src.Fee)
                .Map(dest => dest.ItemCount, src => src.ItemCount)
                .Map(dest => dest.SumPrices, src => src.SumPrices)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.EstateOwnerNationalNo, src => src.EstateOwnerNationalNo)
                .Map(dest => dest.EstateOwnerNameFamily, src => src.EstateOwnerNameFamily)
                .Map(dest => dest.PlaqueNo, src => src.PlaqueNo)
                .Map(dest => dest.EstateAddress, src => src.EstateAddress)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.OtherPaymentsTypeId, src => new List<string> { src.OtherPaymentsTypeId.ToString() })
                .Map(dest => dest.GeoLocationId, src => new List<string> { src.GeoLocationId != null ? src.GeoLocationId.ToString() : null })
                .Map(dest => dest.UnitId, src => new List<string> { src.UnitId != null ? src.UnitId.ToString() : null });

            var returnObject = otherPayment.Adapt<OtherPaymentViewModel>();
            return returnObject;
        }

        public static OtherPaymentGridViewModel ToViewModel(OtherPaymentGrid entity)
        {
            OtherPaymentGridViewModel otherPaymentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<OtherPaymentGrid, OtherPaymentGridViewModel>();

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = entity.Adapt(otherPaymentTemplateViewModel, config);

            return otherPaymentTemplateViewModel;
        }
    }
}
