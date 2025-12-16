namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DocumentSmsMapper" />
    /// </summary>
    public static class DocumentSmsMapper
    {
        /// <summary>
        /// The MapToDocumentSm
        /// </summary>
        /// <param name="DocumentSm">The DocumentSm<see cref="DocumentSm"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentSmsViewModel"/></param>
        public static void MapToDocumentSms(ref DocumentSm DocumentSms, DocumentSmsViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentSmsViewModel, DocumentSm>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.Id.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.ReceiverName, src => src.ReceiverName)
                .Map(dest => dest.MobileNo, src => src.MobileNo)
                .Map(dest => dest.SmsText, src => src.SmsText)
                .Map(dest => dest.IsSent, src => src.IsSent.ToYesNo())
                .Map(dest => dest.IsMechanized, src => src.IsMechanized.ToYesNo())
                .Ignore(src => src.Ilm)
                .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentId)
                ;
            config.Compile();
            viewModel.Adapt(DocumentSms, config);
        }

        /// <summary>
        /// The MapToDocumentSmsViewModel
        /// </summary>
        /// <param name="documentSms">The documentSms<see cref="List{DocumentSm}"/></param>
        /// <returns>The <see cref="List{DocumentPersonViewModel}"/></returns>
        public static List<DocumentSmsViewModel>  MapToDocumentSmsViewModel(
        List<DocumentSm> documentSms
    )
        {
            List<DocumentSmsViewModel> documentSmsViewModel =
                new List<DocumentSmsViewModel>();
            documentSms.ForEach(sp =>
            {
                documentSmsViewModel.Add(MapToDocumentTheOneSmsViewModel(sp));
            });
            return documentSmsViewModel;
        }


        /// <summary>
        /// The MapToDocumentSmsViewModel
        /// </summary>
        /// <param name="documentSms">The documentSms<see cref="DocumentSm"/></param>
        /// <returns>The <see cref="DocumentSmsViewModel"/></returns>
        public static DocumentSmsViewModel MapToDocumentTheOneSmsViewModel(
          DocumentSm documentSms
      )
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentSm, DocumentSmsViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.Id, src => src.Id.ToString())
                .Map(dest => dest.SmsText, src => src.SmsText.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())
                .Map(dest => dest.CreateDateTime, src => src.CreateDate.ToString()+src.CreateTime.ToString())
                .Map(dest => dest.ReceiverName, src => src.ReceiverName.ToString())
                .Map(dest => dest.IsMechanized, src => src.IsMechanized.ToBoolean())
                .Map(dest => dest.IsSent, src => src.IsSent.ToBoolean())
                .Map(dest => dest.IsSentTitle, src => src.IsSent=="1"?"بله":"خیر")
                .Map(dest => dest.IsMechanizedTitle, src => src.IsMechanized == "1" ? "بله" : "خیر")

                ;
                
            config.Compile();

            var documentSmsViewModel = documentSms.Adapt<DocumentSmsViewModel>(config);
            return documentSmsViewModel;
        }
    }
}
