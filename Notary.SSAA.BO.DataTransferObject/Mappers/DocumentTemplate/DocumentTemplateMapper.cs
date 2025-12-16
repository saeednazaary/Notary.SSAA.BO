using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentTemplate;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentTemplate
{
    public static class DocumentTemplateMapper
    {
        public static void ToEntity(CreateDocumentTemplateCommand viewModel, ref Domain.Entities.DocumentTemplate entity)
        {
            TypeAdapterConfig config = new();

            _ = config.NewConfig<CreateDocumentTemplateCommand, Domain.Entities.DocumentTemplate>()

                .Map(dest => dest.Text, src => src.DocumentTemplateText)
                .Map(dest => dest.Code, src => src.DocumentTemplateCode)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.First())
                .Map(dest => dest.Title, src => src.DocumentTemplateTitle)
                .Map(dest => dest.State, src => src.DocumentTemplateStateId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = viewModel.Adapt(entity, config);
        }
        public static void ToEntity(UpdateDocumentTemplateCommand viewModel, ref Domain.Entities.DocumentTemplate entity)
        {
            TypeAdapterConfig config = new();

            _ = config.NewConfig<UpdateDocumentTemplateCommand, Domain.Entities.DocumentTemplate>()

                .Map(dest => dest.Text, src => src.DocumentTemplateText)
                .Map(dest => dest.Code, src => src.DocumentTemplateCode)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.First())
                .Map(dest => dest.Title, src => src.DocumentTemplateTitle)
                .Map(dest => dest.State, src => src.DocumentTemplateStateId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = viewModel.Adapt(entity, config);
        }
        public static void ToEntity(DeleteDocumentTemplateCommand viewModel, ref Domain.Entities.DocumentTemplate entity)
        {
            TypeAdapterConfig config = new();

            _ = config.NewConfig<DeleteDocumentTemplateCommand, Domain.Entities.DocumentTemplate>()

                .Map(dest => dest.State, src => src.DocumentTemplateStateId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = viewModel.Adapt(entity, config);
        }
        public static DocumentTemplateViewModel ToViewModel(Domain.Entities.DocumentTemplate entity)
        {
            DocumentTemplateViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<Domain.Entities.DocumentTemplate, DocumentTemplateViewModel>()

                .Map(dest => dest.DocumentTemplateText, src => src.Text)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.DocumentTemplateCode, src => src.Code)
                .Map(dest => dest.DocumentTemplateId, src => src.Id.ToString())
                .Map(dest => dest.DocumentTypeId, src => new List<string> { src.DocumentTypeId })
                .Map(dest => dest.DocumentTemplateTitle, src => src.Title)
                .Map(dest => dest.DocumentTemplateStateId, src => src.State)
                .Map(dest => dest.LastModifer, src => src.Modifier)
                .Map(dest => dest.LastModifyDateTime, src => src.ModifyDateTime.Replace("-", " - "))
                .Map(dest => dest.CreateDate, src => src.CreateDate + " - " + src.CreateTime)
                .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = entity.Adapt(documentTemplateViewModel, config);

            return documentTemplateViewModel;
        }
    }
}
