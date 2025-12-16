namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractSmsMapper" />
    /// </summary>
    public static class DocumentStandardContractSmsMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractSm
        /// </summary>
        /// <param name="DocumentSm">The DocumentSm<see cref="DocumentSm"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractSmsViewModel"/></param>
        public static void MapToDocumentStandardContractSms(ref DocumentSm DocumentSms, DocumentStandardContractSmsViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractSmsViewModel, DocumentSm>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.Id.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.ReceiverName, src => src.ReceiverName)
                .Map(dest => dest.MobileNo, src => src.MobileNo)
                .Map(dest => dest.SmsText, src => src.SmsText)
                .Map(dest => dest.IsSent, src => src.IsSent.ToYesNo())
                .Map(dest => dest.IsMechanized, src => src.IsMechanized.ToYesNo())
                .Ignore(src => src.Ilm)
                //.IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentId)
                ;
            config.Compile();
            viewModel.Adapt(DocumentSms, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractSmsViewModel
        /// </summary>
        /// <param name="documentSms">The documentSms<see cref="List{DocumentSm}"/></param>
        /// <returns>The <see cref="List{DocumentPersonViewModel}"/></returns>
        public static List<DocumentStandardContractSmsViewModel> MapToDocumentStandardContractSmsViewModel(
        List<DocumentSm> documentSms
    )
        {
            List<DocumentStandardContractSmsViewModel> documentSmsViewModel =
                new List<DocumentStandardContractSmsViewModel>();
            documentSms.ForEach(sp =>
            {
                documentSmsViewModel.Add(MapToDocumentStandardContractTheOneSmsViewModel(sp));
            });
            return documentSmsViewModel;
        }


        /// <summary>
        /// The MapToDocumentStandardContractSmsViewModel
        /// </summary>
        /// <param name="documentSms">The documentSms<see cref="DocumentSm"/></param>
        /// <returns>The <see cref="DocumentStandardContractSmsViewModel"/></returns>
        public static DocumentStandardContractSmsViewModel MapToDocumentStandardContractTheOneSmsViewModel(
          DocumentSm documentSms
      )
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentSm, DocumentStandardContractSmsViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.Id, src => src.Id.ToString())
                .Map(dest => dest.SmsText, src => src.SmsText.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())
                .Map(dest => dest.CreateDateTime, src => src.CreateDate.ToString() + src.CreateTime.ToString())
                .Map(dest => dest.ReceiverName, src => src.ReceiverName.ToString())
                .Map(dest => dest.IsMechanized, src => src.IsMechanized.ToBoolean())
                .Map(dest => dest.IsSent, src => src.IsSent.ToBoolean())
                .Map(dest => dest.IsSentTitle, src => src.IsSent == "1" ? "بله" : "خیر")
                .Map(dest => dest.IsMechanizedTitle, src => src.IsMechanized == "1" ? "بله" : "خیر")

                ;

            config.Compile();

            var documentSmsViewModel = documentSms.Adapt<DocumentStandardContractSmsViewModel>(config);
            return documentSmsViewModel;
        }
    }
}
