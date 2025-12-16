using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentModify
{
    /// <summary>
    /// Defines the <see cref="DocumentModifyMapper" />
    /// </summary>
    public static class DocumentModifyMapper
    {


        public static DocumentModifyViewModel ToViewModel(Domain.Entities.SsrDocModifyClassifyNo ssrDocModifyClassifyNo)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.SsrDocModifyClassifyNo, DocumentModifyViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)

                // شناسه‌ها
                .Map(dest => dest.Id, src => src.Id.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())

                // شماره‌های ترتیب
                .Map(dest => dest.ClassifyNoOld, src => src.ClassifyNoOld.ToString())
                .Map(dest => dest.ClassifyNoNew, src => src.ClassifyNoNew.HasValue ? src.ClassifyNoNew.Value.ToString() : null)

                // تاریخ‌های ثبت در دفتر
                .Map(dest => dest.WriteInBookDateOld, src => src.WriteInBookDateOld)
                .Map(dest => dest.WriteInBookDateNew, src => src.WriteInBookDateNew)

                // جلد دفتر
                .Map(dest => dest.RegisterVolumeNoOld, src => src.RegisterVolumeNoOld)
                .Map(dest => dest.RegisterVolumeNoNew, src => src.RegisterVolumeNoNew)

                // صفحات دفتر
                .Map(dest => dest.RegisterPagesNoOld, src => src.RegisterPapersNoOld)
                .Map(dest => dest.RegisterPagesNoNew, src => src.RegisterPapersNoNew)

                // تاریخ اصلاح
                .Map(dest => dest.ModifyDate, src => src.ModifyDate + ":" + src.ModifyTime)

                .Map(dest => dest.DocumentDate, src => src.Document.DocumentDate)
                .Map(dest => dest.DocumentType, src => src.Document.DocumentTypeTitle)
                .Map(dest => dest.NationalNo, src => src.Document.NationalNo)

                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var returnObject = ssrDocModifyClassifyNo.Adapt<DocumentModifyViewModel>(config);
            return returnObject;
        }
        public static DocumentModifyViewModel ToViewModel(Domain.Entities.Document ssrDocModifyClassifyNo)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.Document, DocumentModifyViewModel>()
                .Map(dest => dest.IsNew, src => true)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)

                // شناسه‌ها
                .Map(dest => dest.DocumentId, src => src.Id.ToString())

                // شماره‌های ترتیب
                .Map(dest => dest.ClassifyNoOld, src => src.ClassifyNo.HasValue ? src.ClassifyNo.Value.ToString() : null)

                // تاریخ‌های ثبت در دفتر
                .Map(dest => dest.WriteInBookDateOld, src => src.WriteInBookDate)

                // جلد دفتر
                .Map(dest => dest.RegisterVolumeNoOld, src => src.BookVolumeNo)

                // صفحات دفتر
                .Map(dest => dest.RegisterPagesNoOld, src => src.BookPapersNo)

                // تاریخ اصلاح

                .Map(dest => dest.DocumentDate, src => src.DocumentDate)
                .Map(dest => dest.DocumentType, src => src.DocumentTypeTitle)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var returnObject = ssrDocModifyClassifyNo.Adapt<DocumentModifyViewModel>(config);
            return returnObject;
        }
        public static void ToEntity(ref SsrDocModifyClassifyNo entity, CreateDocumentModifyCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateDocumentModifyCommand, SsrDocModifyClassifyNo>()
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.ClassifyNoOld, src => src.ClassifyNoOld.ToInt())
                .Map(dest => dest.ClassifyNoNew, src => src.ClassifyNoNew.ToNullableInt())
                .Map(dest => dest.WriteInBookDateOld, src => src.WriteInBookDateOld)
                .Map(dest => dest.RegisterVolumeNoOld, src => src.RegisterVolumeNoOld)
                .Map(dest => dest.RegisterVolumeNoNew, src => src.RegisterVolumeNoNew)
                .Map(dest => dest.RegisterPapersNoOld, src => src.RegisterPagesNoOld)
                .Map(dest => dest.RegisterPapersNoNew, src => src.RegisterPagesNoNew)
                .IgnoreNonMapped(true);
            config.Compile();

            viewModel.Adapt(entity, config);
        }
    }
}
