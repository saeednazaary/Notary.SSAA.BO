using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.SsrSignEbookBaseInfo
{
    public static class SsrSignEbookBaseinfoMapper
    {
        public static SsrSignEbookBaseInfoViewModel ToViewModel(Domain.Entities.SsrSignEbookBaseinfo ssrSignEbookBaseinfo)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.SsrSignEbookBaseinfo, SsrSignEbookBaseInfoViewModel>()
                .Map(dest => dest.SignRequestElectronicBookBaseInfoId, src => src.Id.ToString())
                .Map(dest => dest.LastClassifyNo, src => src.LastClassifyNo.ToString())
                .Map(dest => dest.LastRegisterPaperNo, src => src.LastRegisterPaperNo.ToString())
                .Map(dest => dest.LastRegisterVolumeNo, src => src.LastRegisterVolumeNo)
                .Map(dest => dest.NumberOfBooks, src => src.NumberOfBooks.ToString())
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var returnObject = ssrSignEbookBaseinfo.Adapt<SsrSignEbookBaseInfoViewModel>(config);
            return returnObject;
        }
        public static void ToEntity(ref Domain.Entities.SsrSignEbookBaseinfo entity, CreateSsrSignEbookBaseInfoCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<CreateSsrSignEbookBaseInfoCommand, Domain.Entities.SsrSignEbookBaseinfo>()

                .Map(dest => dest.LastClassifyNo, src => src.LastClassifyNo.ToInt())
                .Map(dest => dest.LastRegisterPaperNo, src => src.LastRegisterPaperNo.ToInt())
                .Map(dest => dest.NumberOfBooks, src => src.NumberOfBooks.ToInt())
                .Map(dest => dest.LastRegisterVolumeNo, src => src.LastRegisterVolumeNo)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
    }
}