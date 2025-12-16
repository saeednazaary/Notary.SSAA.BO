using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentEbookBaseInfo;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentEbookBaseInfo
{
    public static class DocumentEbookBaseInfoMapper
    {
        public static DocumentEbookBaseInfoViewModel ToViewModel(Domain.Entities.DocumentElectronicBookBaseinfo DocumentEbookBaseInfo)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.DocumentElectronicBookBaseinfo, DocumentEbookBaseInfoViewModel>()
                .Map(dest => dest.DocumentEBookBaseInfoId, src => src.Id.ToString())
                .Map(dest => dest.LastClassifyNo, src => src.LastClassifyNo.ToString())
                .Map(dest => dest.LastRegisterPaperNo, src => src.LastRegisterPaperNo.ToString())
                .Map(dest => dest.LastRegisterVolumeNo, src => src.LastRegisterVolumeNo)
                .Map(dest => dest.NumberOfBooksJari, src => src.NumberOfBooksJari.ToString())
                .Map(dest => dest.NumberOfBooksVehicle, src => src.NumberOfBooksVehicle.ToString())
                .Map(dest => dest.NumberOfBooksRahni, src => src.NumberOfBooksRahni.ToString())
                .Map(dest => dest.NumberOfBooksOghaf, src => src.NumberOfBooksOghaf.ToString())
                .Map(dest => dest.NumberOfBooksArzi, src => src.NumberOfBooksArzi.ToString())
                .Map(dest => dest.NumberOfBooksAgent, src => src.NumberOfBooksAgent.ToString())
                .Map(dest => dest.NumberOfBooksOthers, src => src.NumberOfBooksOthers.ToString())
                .Map(dest => dest.TotalBooks, src => src.NumberOfBooks.ToString())
                .IgnoreNonMapped(true);
                config.Compile();

            var returnObject = DocumentEbookBaseInfo.Adapt<DocumentEbookBaseInfoViewModel>(config);
            return returnObject;
        }
        public static void ToEntity(ref Domain.Entities.DocumentElectronicBookBaseinfo entity, CreateDocumentEbookBaseInfoCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateDocumentEbookBaseInfoCommand, Domain.Entities.DocumentElectronicBookBaseinfo>()

                .Map(dest => dest.LastClassifyNo, src => src.LastClassifyNo.ToInt())
                .Map(dest => dest.LastRegisterPaperNo, src => src.LastRegisterPaperNo.ToInt())
                .Map(dest => dest.LastRegisterVolumeNo, src => src.LastRegisterVolumeNo)
                .Map(dest => dest.NumberOfBooks, src =>src.TotalBooks.ToInt())
                .Map(dest => dest.NumberOfBooksJari, src => src.NumberOfBooksJari.ToInt())
                .Map(dest => dest.NumberOfBooksVehicle, src => src.NumberOfBooksVehicle.ToInt())
                .Map(dest => dest.NumberOfBooksRahni, src => src.NumberOfBooksRahni.ToInt())
                .Map(dest => dest.NumberOfBooksOghaf, src => src.NumberOfBooksOghaf.ToInt())
                .Map(dest => dest.NumberOfBooksArzi, src => src.NumberOfBooksArzi.ToInt())
                .Map(dest => dest.NumberOfBooksAgent, src => src.NumberOfBooksAgent.ToInt())
                .Map(dest => dest.NumberOfBooksOthers, src => src.NumberOfBooksOthers.ToInt())
                .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(entity, config);

        }
    }
}