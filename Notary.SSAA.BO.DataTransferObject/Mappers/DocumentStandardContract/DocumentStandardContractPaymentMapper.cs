using Mapster;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    /// <summary>
    /// Defines the <see cref="DocumentStandardContractPaymentMapper" />
    /// </summary>
    public static class DocumentStandardContractPaymentMapper
    {      /// <summary>
           /// The MapToDocumentStandardContractPaymentsViewModel
           /// </summary>
           /// <param name="documentPayments">The documentPayments<see cref="List{DocumentPayment}"/></param>
           /// <returns>The <see cref="List{DocumentStandardContractPaymentViewModel}"/></returns>

        #region ToViewModel
        public static List<DocumentStandardContractPaymentViewModel> MapToDocumentStandardContractPaymentsViewModel(IList<DocumentPayment> documentPayments)
        {
            List<DocumentStandardContractPaymentViewModel> documentPaymentsViewModel = new List<DocumentStandardContractPaymentViewModel>();
            documentPayments.ToList<DocumentPayment>().ForEach(sp =>
            {
                documentPaymentsViewModel.Add(MapToDocumentStandardContractPaymentViewModel(sp));
            });
            return documentPaymentsViewModel;
        }

        public static DocumentStandardContractPaymentViewModel MapToDocumentStandardContractPaymentViewModel(DocumentPayment documentPayment)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentPayment, DocumentStandardContractPaymentViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.Id, src => src.Id.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())
                .Map(dest => dest.CostTypeId, src => src.CostTypeId != null ? new string[] { src.CostTypeId } : Array.Empty<string>())
                .Map(dest => dest.CostTypeTitle, src => src.CostType != null ? src.CostType.Title : null)
                .Map(dest => dest.No, src => src.No)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.HowToQuotation, src => src.HowToQuotation)
                .Map(dest => dest.HowToPay, src => src.HowToPay)
                .Map(dest => dest.HowToPayTitle, src => ((HowToPay)src.HowToPay.ToInt()).GetDescription())
                .Map(dest => dest.PaymentNo, src => src.PaymentNo)
                .Map(dest => dest.PaymentDate, src => src.PaymentDate)
                .Map(dest => dest.PaymentTime, src => src.PaymentTime)
                .Map(dest => dest.PaymentType, src => src.PaymentType)
                .Map(dest => dest.BankCounterInfo, src => src.BankCounterInfo)
                .Map(dest => dest.CardNo, src => src.CardNo)
                .Map(dest => dest.IsReused, src => src.IsReused)
                .Map(dest => dest.ReusedDocumentPaymentId, src => src.ReusedDocumentPaymentId != null ? new string[] { src.ReusedDocumentPaymentId.ToString() } : Array.Empty<string>())
                .Map(dest => dest.ReusedDocumentPaymentTitle, src => src.ReusedDocumentPayment != null ? src.ReusedDocumentPayment.No : null)
                .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
                .Map(dest => dest.RecordDate, src => src.RecordDate)
                .Map(dest => dest.Ilm, src => src.Ilm);


            config.Compile();
            var documentPaymentViewModel = new DocumentStandardContractPaymentViewModel();
            documentPaymentViewModel = documentPayment.Adapt<DocumentStandardContractPaymentViewModel>(config);
            return documentPaymentViewModel;
        }
        #endregion

        #region ToEntity
        public static List<DocumentPayment> MapToDocumentStandardContractPayments(IList<DocumentStandardContractPaymentViewModel> documentPayments, string ScriptoriumId)
        {
            List<DocumentPayment> DocumentPayments = new List<DocumentPayment>();
            documentPayments.ToList<DocumentStandardContractPaymentViewModel>().ForEach(x =>
            {
                DocumentPayment documentPayment = new();
                MapToDocumentStandardContractPayment(ref documentPayment, x, ScriptoriumId);
                DocumentPayments.Add(documentPayment);
            });
            return DocumentPayments;
        }

        public static void MapToDocumentStandardContractPayment(ref DocumentPayment DocumentPayment, DocumentStandardContractPaymentViewModel documentPaymentViewModel, string ScriptoriumId)
        {

            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractPaymentViewModel, DocumentPayment>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : Guid.Parse(src.Id))
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.CostTypeId, src => src.CostTypeId.Length > 0 ? src.CostTypeId.First() : null)
                .Map(dest => dest.No, src => src.No)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.HowToQuotation, src => src.HowToQuotation)
                .Map(dest => dest.HowToPay, src => src.HowToPay)
                .Map(dest => dest.PaymentNo, src => src.PaymentNo)
                .Map(dest => dest.PaymentDate, src => src.PaymentDate)
                .Map(dest => dest.PaymentTime, src => src.PaymentTime)
                .Map(dest => dest.PaymentType, src => src.PaymentType)
                .Map(dest => dest.BankCounterInfo, src => src.BankCounterInfo)
                .Map(dest => dest.CardNo, src => src.CardNo)
                .Map(dest => dest.IsReused, src => src.IsReused)
                .Map(dest => dest.ReusedDocumentPaymentId, src => src.ReusedDocumentPaymentId.Length > 0 ? Guid.Parse(src.ReusedDocumentPaymentId.First()) : (Guid?)null)
                .Map(dest => dest.RecordDate, src => DateTime.Now)
                .Map(dest => dest.ScriptoriumId, src => ScriptoriumId)
                .Map(dest => dest.Ilm, src => DocumentConstants.CreateIlm);

            config.Compile();
            documentPaymentViewModel.Adapt(DocumentPayment, config);
        }

        public static void MapToInquiryPayment(ref DocumentPayment DocumentPayment, InquiryPaymentViewModel inquiryPaymentView, Guid DocumentId, Guid Id, string ScriptoriumId)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<InquiryPaymentViewModel, DocumentPayment>()
            .Map(dest => dest.Id, src => Id)
            .Map(dest => dest.No, src => src.No)
            .Map(dest => dest.DocumentId, src => DocumentId)
            .Map(dest => dest.PaymentDate, src => src.PaymentDate)
            .Map(dest => dest.PaymentTime, src => src.PaymentTime)
            .Map(dest => dest.PaymentType, src => src.PaymentType != null ? src.PaymentType : "PCPos")
            .Map(dest => dest.PaymentNo, src => src.PaymentNo)
            .Map(dest => dest.CardNo, src => src.CardNo)
            .Map(dest => dest.Price, src => (long)(Convert.ToDecimal(src.Price)))
            .Map(dest => dest.HowToPay, src => HowToPay.Electronic.ToAssignedValue())
            .Map(dest => dest.HowToQuotation, src => src.HowToQuotation)
            .Map(dest => dest.IsReused, src => YesNo.No.ToAssignedValue())
            .Map(dest => dest.ScriptoriumId, src => ScriptoriumId)
            .Map(dest => dest.Ilm, src => "1");
            config.Compile();
            inquiryPaymentView.Adapt(DocumentPayment, config);

        }
        #endregion
    }
}
