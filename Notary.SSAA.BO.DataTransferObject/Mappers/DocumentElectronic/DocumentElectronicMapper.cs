

using Mapster;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentElectronic;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentElectronic
{
    public static class DocumentElectronicMapper
    {
        //    public static DocumentElectronicPersonViewModel ToElectronicBookPersonViewModel(DocumentPerson DocumentPerson)
        //    {
        //        var config = new TypeAdapterConfig();

        //        // Configure the reverse mapping from DocumentPerson to DocumentPersonViewModel
        //        config.NewConfig<DocumentPerson, DocumentElectronicPersonViewModel>().IgnoreNullValues(true)
        //            //.Map(dest => dest.PersonSignClassifyNo, src => src.SignClassifyNo)
        //            .Map(dest => dest.RowNo, src => src.RowNo.ToString())
        //            .Map(dest => dest.PersonDescription, src => $"فقط صحت امضای {src.Name} {src.Family} به شماره ملی {src.NationalNo} متولد {src.BirthDate} مورد تایید است .");
        //        // Apply the configuration
        //        config.Compile();

        //        // Perform the mapping
        //        var DocumentPersonViewModel = DocumentPerson.Adapt<DocumentElectronicPersonViewModel>(config);
        //        return DocumentPersonViewModel;
        //    }
        //    public static DocumentElectronicBookViewModel ToElectronicBookViewModel(Domain.Entities.Document Document)
        //    {
        //        TypeAdapterConfig<Domain.Entities.Document, DocumentElectronicBookViewModel>
        //            .NewConfig()
        //            .Map(dest => dest.DocumentId, src => src.Id.ToString())
        //            .Map(dest => dest.DocumentReqNo, src => src.RequestNo)
        //            .Map(dest => dest.DocumentClassifyNo, src => src.ClassifyNo)
        //            .Map(dest => dest.DocumentNationalNo, src => src.NationalNo)
        //            .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId)
        //            .Map(dest => dest.DocumentTypeGroup1Id, src => src.DocumentType.DocumentTypeGroup1Id)
        //            .Map(dest => dest.DocumentTypeGroup1Title, src => src.DocumentType.DocumentTypeGroup1.Title)
        //             .Map(dest => dest.DocumentTypeGroup2Id, src => src.DocumentType.DocumentTypeGroup2Id)
        //            .Map(dest => dest.DocumentTypeGroup2Title, src => src.DocumentType.DocumentTypeGroup2.Title)
        //            .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId)
        //            .Map(dest => dest.DocumentTypeTitle, src => src.DocumentType.Title!=null? src.DocumentType.Title : "")
        //            .Map(dest => dest.DocumentSecretCode, src => src.DocumentSecretCode)
        //            .Map(dest => dest.DocumentDate, src => src.DocumentDate)
        //            .Map(dest => dest.DocumentSignDate, src => src.SignDate)
        //            .Map(dest => dest.DocumentElectronicBookPersons, src => ToElectronicBookPersonsList(src.DocumentPeople.ToList()))
        //            .IgnoreNonMapped(true);

        //        var returnObject = Document.Adapt<DocumentElectronicBookViewModel>();

        //        return returnObject;
        //    }
        //    public static List<DocumentElectronicPersonViewModel> ToElectronicBookPersonsList(List<DocumentPerson> DocumentPersons)
        //    {
        //        List<DocumentElectronicPersonViewModel> DocumentPersonViewModels = new List<DocumentElectronicPersonViewModel>();
        //        DocumentPersons.ForEach(sp =>
        //        {
        //            DocumentPersonViewModels.Add(ToElectronicBookPersonViewModel(sp));


        //        });
        //        return DocumentPersonViewModels;

        //    }
    }
}