using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.Infrastructure.Contexts;

public partial class SsarContext : DbContext
{
    public SsarContext(DbContextOptions<SsarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgentType> AgentTypes { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookpage> Bookpages { get; set; }

    public virtual DbSet<ConfigurationParameter> ConfigurationParameters { get; set; }

    public virtual DbSet<ConvertLegacyDataLog> ConvertLegacyDataLogs { get; set; }

    public virtual DbSet<ConvertLegacyDataLogDetail> ConvertLegacyDataLogDetails { get; set; }

    public virtual DbSet<ConvertLegacyDataLogScriptoriumLastDate> ConvertLegacyDataLogScriptoriumLastDates { get; set; }

    public virtual DbSet<ConvertLegacyDataRun> ConvertLegacyDataRuns { get; set; }

    public virtual DbSet<ConvertLegacyDataRunDetail> ConvertLegacyDataRunDetails { get; set; }

    public virtual DbSet<ConvertLegacyDataRunDetailsError> ConvertLegacyDataRunDetailsErrors { get; set; }

    public virtual DbSet<ConvertLegacyDataUseCase> ConvertLegacyDataUseCases { get; set; }

    public virtual DbSet<ConvertLegacyDataUseCaseDetail> ConvertLegacyDataUseCaseDetails { get; set; }

    public virtual DbSet<CostType> CostTypes { get; set; }

    public virtual DbSet<DealSummary> DealSummaries { get; set; }

    public virtual DbSet<DealSummaryActionType> DealSummaryActionTypes { get; set; }

    public virtual DbSet<DealSummaryPerson> DealSummaryPeople { get; set; }

    public virtual DbSet<DealSummarySendreceiveLog> DealSummarySendreceiveLogs { get; set; }

    public virtual DbSet<DealSummaryStatus> DealSummaryStatuses { get; set; }

    public virtual DbSet<DealSummaryTransferType> DealSummaryTransferTypes { get; set; }

    public virtual DbSet<DealsummaryPersonRelateType> DealsummaryPersonRelateTypes { get; set; }

    public virtual DbSet<DealsummaryUnrestrictionType> DealsummaryUnrestrictionTypes { get; set; }

    public virtual DbSet<DigitalSignatureConfiguration> DigitalSignatureConfigurations { get; set; }

    public virtual DbSet<DigitalSignaturePropertyMapping> DigitalSignaturePropertyMappings { get; set; }

    public virtual DbSet<DigitalSignaturePropertyMappingDetail> DigitalSignaturePropertyMappingDetails { get; set; }

    public virtual DbSet<DigitalSignatureValue> DigitalSignatureValues { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentAssetType> DocumentAssetTypes { get; set; }

    public virtual DbSet<DocumentCase> DocumentCases { get; set; }

    public virtual DbSet<DocumentCost> DocumentCosts { get; set; }

    public virtual DbSet<DocumentCostUnchanged> DocumentCostUnchangeds { get; set; }

    public virtual DbSet<DocumentElectronicBook> DocumentElectronicBooks { get; set; }

    public virtual DbSet<DocumentElectronicBookBaseinfo> DocumentElectronicBookBaseinfos { get; set; }

    public virtual DbSet<DocumentEstate> DocumentEstates { get; set; }

    public virtual DbSet<DocumentEstateAttachment> DocumentEstateAttachments { get; set; }

    public virtual DbSet<DocumentEstateDealSummaryGenerated> DocumentEstateDealSummaryGenerateds { get; set; }

    public virtual DbSet<DocumentEstateDealSummarySelected> DocumentEstateDealSummarySelecteds { get; set; }

    public virtual DbSet<DocumentEstateDealSummarySeparation> DocumentEstateDealSummarySeparations { get; set; }

    public virtual DbSet<DocumentEstateInquiry> DocumentEstateInquiries { get; set; }

    public virtual DbSet<DocumentEstateOwnershipDocument> DocumentEstateOwnershipDocuments { get; set; }

    public virtual DbSet<DocumentEstateQuotaDetail> DocumentEstateQuotaDetails { get; set; }

    public virtual DbSet<DocumentEstateQuotum> DocumentEstateQuota { get; set; }

    public virtual DbSet<DocumentEstateSeparationPiece> DocumentEstateSeparationPieces { get; set; }

    public virtual DbSet<DocumentEstateSeparationPieceKind> DocumentEstateSeparationPieceKinds { get; set; }

    public virtual DbSet<DocumentEstateSeparationPiecesQuotum> DocumentEstateSeparationPiecesQuota { get; set; }

    public virtual DbSet<DocumentEstateType> DocumentEstateTypes { get; set; }

    public virtual DbSet<DocumentEstateTypeGroup> DocumentEstateTypeGroups { get; set; }

    public virtual DbSet<DocumentFile> DocumentFiles { get; set; }

    public virtual DbSet<DocumentInfoConfirm> DocumentInfoConfirms { get; set; }

    public virtual DbSet<DocumentInfoJudgement> DocumentInfoJudgements { get; set; }

    public virtual DbSet<DocumentInfoOther> DocumentInfoOthers { get; set; }

    public virtual DbSet<DocumentInfoText> DocumentInfoTexts { get; set; }

    public virtual DbSet<DocumentInquiry> DocumentInquiries { get; set; }

    public virtual DbSet<DocumentInquiryOrganization> DocumentInquiryOrganizations { get; set; }

    public virtual DbSet<DocumentJudgementType> DocumentJudgementTypes { get; set; }

    public virtual DbSet<DocumentLimit> DocumentLimits { get; set; }

    public virtual DbSet<DocumentMessage> DocumentMessages { get; set; }

    public virtual DbSet<DocumentPayment> DocumentPayments { get; set; }

    public virtual DbSet<DocumentPerson> DocumentPeople { get; set; }

    public virtual DbSet<DocumentPersonRelated> DocumentPersonRelateds { get; set; }

    public virtual DbSet<DocumentPersonType> DocumentPersonTypes { get; set; }

    public virtual DbSet<DocumentRelation> DocumentRelations { get; set; }

    public virtual DbSet<DocumentSemaphore> DocumentSemaphores { get; set; }

    public virtual DbSet<DocumentSm> DocumentSms { get; set; }

    public virtual DbSet<DocumentSpecialChange> DocumentSpecialChanges { get; set; }

    public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<DocumentTypeGroup1> DocumentTypeGroup1s { get; set; }

    public virtual DbSet<DocumentTypeGroup2> DocumentTypeGroup2s { get; set; }

    public virtual DbSet<DocumentTypeSubject> DocumentTypeSubjects { get; set; }

    public virtual DbSet<DocumentVehicle> DocumentVehicles { get; set; }

    public virtual DbSet<DocumentVehicleQuotaDetail> DocumentVehicleQuotaDetails { get; set; }

    public virtual DbSet<DocumentVehicleQuotum> DocumentVehicleQuota { get; set; }

    public virtual DbSet<DocumentVehicleSystem> DocumentVehicleSystems { get; set; }

    public virtual DbSet<DocumentVehicleTaxTable> DocumentVehicleTaxTables { get; set; }

    public virtual DbSet<DocumentVehicleTip> DocumentVehicleTips { get; set; }

    public virtual DbSet<DocumentVehicleType> DocumentVehicleTypes { get; set; }

    public virtual DbSet<EstateDivisionRequestElement> EstateDivisionRequestElements { get; set; }

    public virtual DbSet<EstateDocReqPersonRelate> EstateDocReqPersonRelates { get; set; }

    public virtual DbSet<EstateDocumentCurrentType> EstateDocumentCurrentTypes { get; set; }

    public virtual DbSet<EstateDocumentRequest> EstateDocumentRequests { get; set; }

    public virtual DbSet<EstateDocumentRequestPerson> EstateDocumentRequestPeople { get; set; }

    public virtual DbSet<EstateDocumentRequestStatus> EstateDocumentRequestStatuses { get; set; }

    public virtual DbSet<EstateDocumentRequestType> EstateDocumentRequestTypes { get; set; }

    public virtual DbSet<EstateInquiry> EstateInquiries { get; set; }

    public virtual DbSet<EstateInquiryActionType> EstateInquiryActionTypes { get; set; }

    public virtual DbSet<EstateInquiryPerson> EstateInquiryPeople { get; set; }

    public virtual DbSet<EstateInquirySendedSm> EstateInquirySendedSms { get; set; }

    public virtual DbSet<EstateInquirySendreceiveLog> EstateInquirySendreceiveLogs { get; set; }

    public virtual DbSet<EstateInquiryStatus> EstateInquiryStatuses { get; set; }

    public virtual DbSet<EstateInquiryType> EstateInquiryTypes { get; set; }

    public virtual DbSet<EstateOwnershipType> EstateOwnershipTypes { get; set; }

    public virtual DbSet<EstatePieceMainType> EstatePieceMainTypes { get; set; }

    public virtual DbSet<EstatePieceType> EstatePieceTypes { get; set; }

    public virtual DbSet<EstateSection> EstateSections { get; set; }

    public virtual DbSet<EstateSeridaftar> EstateSeridaftars { get; set; }

    public virtual DbSet<EstateSideType> EstateSideTypes { get; set; }

    public virtual DbSet<EstateSubsection> EstateSubsections { get; set; }

    public virtual DbSet<EstateTaxCity> EstateTaxCities { get; set; }

    public virtual DbSet<EstateTaxCounty> EstateTaxCounties { get; set; }

    public virtual DbSet<EstateTaxInquiry> EstateTaxInquiries { get; set; }

    public virtual DbSet<EstateTaxInquiryAttach> EstateTaxInquiryAttaches { get; set; }

    public virtual DbSet<EstateTaxInquiryBuildingConstructionStep> EstateTaxInquiryBuildingConstructionSteps { get; set; }

    public virtual DbSet<EstateTaxInquiryBuildingStatus> EstateTaxInquiryBuildingStatuses { get; set; }

    public virtual DbSet<EstateTaxInquiryBuildingType> EstateTaxInquiryBuildingTypes { get; set; }

    public virtual DbSet<EstateTaxInquiryDocumentRequestType> EstateTaxInquiryDocumentRequestTypes { get; set; }

    public virtual DbSet<EstateTaxInquiryFieldType> EstateTaxInquiryFieldTypes { get; set; }

    public virtual DbSet<EstateTaxInquiryFile> EstateTaxInquiryFiles { get; set; }

    public virtual DbSet<EstateTaxInquiryLocationAssignRigthOwnershipType> EstateTaxInquiryLocationAssignRigthOwnershipTypes { get; set; }

    public virtual DbSet<EstateTaxInquiryLocationAssignRigthUsingType> EstateTaxInquiryLocationAssignRigthUsingTypes { get; set; }

    public virtual DbSet<EstateTaxInquiryPerson> EstateTaxInquiryPeople { get; set; }

    public virtual DbSet<EstateTaxInquirySendedSm> EstateTaxInquirySendedSms { get; set; }

    public virtual DbSet<EstateTaxInquiryTransferType> EstateTaxInquiryTransferTypes { get; set; }

    public virtual DbSet<EstateTaxInquiryUsingType> EstateTaxInquiryUsingTypes { get; set; }

    public virtual DbSet<EstateTaxProvince> EstateTaxProvinces { get; set; }

    public virtual DbSet<EstateTaxUnit> EstateTaxUnits { get; set; }

    public virtual DbSet<EstateTransitionType> EstateTransitionTypes { get; set; }

    public virtual DbSet<EstateType> EstateTypes { get; set; }

    public virtual DbSet<ExecutiveAddressType> ExecutiveAddressTypes { get; set; }

    public virtual DbSet<ExecutiveBindingSubjectType> ExecutiveBindingSubjectTypes { get; set; }

    public virtual DbSet<ExecutiveFieldType> ExecutiveFieldTypes { get; set; }

    public virtual DbSet<ExecutiveGeneralFieldType> ExecutiveGeneralFieldTypes { get; set; }

    public virtual DbSet<ExecutiveGeneralPersonPostType> ExecutiveGeneralPersonPostTypes { get; set; }

    public virtual DbSet<ExecutivePersonPostType> ExecutivePersonPostTypes { get; set; }

    public virtual DbSet<ExecutiveRequest> ExecutiveRequests { get; set; }

    public virtual DbSet<ExecutiveRequestBinding> ExecutiveRequestBindings { get; set; }

    public virtual DbSet<ExecutiveRequestDocument> ExecutiveRequestDocuments { get; set; }

    public virtual DbSet<ExecutiveRequestPerson> ExecutiveRequestPeople { get; set; }

    public virtual DbSet<ExecutiveRequestPersonRelated> ExecutiveRequestPersonRelateds { get; set; }

    public virtual DbSet<ExecutiveSupport> ExecutiveSupports { get; set; }

    public virtual DbSet<ExecutiveSupportAsset> ExecutiveSupportAssets { get; set; }

    public virtual DbSet<ExecutiveSupportAssetField> ExecutiveSupportAssetFields { get; set; }

    public virtual DbSet<ExecutiveSupportPerson> ExecutiveSupportPeople { get; set; }

    public virtual DbSet<ExecutiveSupportPersonRelated> ExecutiveSupportPersonRelateds { get; set; }

    public virtual DbSet<ExecutiveSupportType> ExecutiveSupportTypes { get; set; }

    public virtual DbSet<ExecutiveType> ExecutiveTypes { get; set; }

    public virtual DbSet<ExecutiveTypeAttachmentType> ExecutiveTypeAttachmentTypes { get; set; }

    public virtual DbSet<ExecutiveTypeExecutiveBindingSubjectType> ExecutiveTypeExecutiveBindingSubjectTypes { get; set; }

    public virtual DbSet<ExecutiveTypeExecutivePersonPostType> ExecutiveTypeExecutivePersonPostTypes { get; set; }

    public virtual DbSet<ExecutiveWealthFieldType> ExecutiveWealthFieldTypes { get; set; }

    public virtual DbSet<ExecutiveWealthType> ExecutiveWealthTypes { get; set; }

    public virtual DbSet<ForestorgCity> ForestorgCities { get; set; }

    public virtual DbSet<ForestorgInquiry> ForestorgInquiries { get; set; }

    public virtual DbSet<ForestorgInquiryFile> ForestorgInquiryFiles { get; set; }

    public virtual DbSet<ForestorgInquiryPerson> ForestorgInquiryPeople { get; set; }

    public virtual DbSet<ForestorgInquiryPoint> ForestorgInquiryPoints { get; set; }

    public virtual DbSet<ForestorgInquiryResponse> ForestorgInquiryResponses { get; set; }

    public virtual DbSet<ForestorgInquiryResponsetype> ForestorgInquiryResponsetypes { get; set; }

    public virtual DbSet<ForestorgInquiryStatus> ForestorgInquiryStatuses { get; set; }

    public virtual DbSet<ForestorgProvince> ForestorgProvinces { get; set; }

    public virtual DbSet<ForestorgResponsepoint> ForestorgResponsepoints { get; set; }

    public virtual DbSet<ForestorgSection> ForestorgSections { get; set; }

    public virtual DbSet<GeneralOrganization> GeneralOrganizations { get; set; }

    public virtual DbSet<ImportantActionType> ImportantActionTypes { get; set; }

    public virtual DbSet<ImportantActionTypeIllegalTime> ImportantActionTypeIllegalTimes { get; set; }

    public virtual DbSet<InquiryFromUnit> InquiryFromUnits { get; set; }

    public virtual DbSet<InquiryFromUnitPerson> InquiryFromUnitPeople { get; set; }

    public virtual DbSet<InquiryFromUnitType> InquiryFromUnitTypes { get; set; }

    public virtual DbSet<InquiryMartyrLog> InquiryMartyrLogs { get; set; }

    public virtual DbSet<InquiryMocLog> InquiryMocLogs { get; set; }

    public virtual DbSet<InquirySabteahvalLog> InquirySabteahvalLogs { get; set; }

    public virtual DbSet<InquirySanaLog> InquirySanaLogs { get; set; }

    public virtual DbSet<InquiryTfaLog> InquiryTfaLogs { get; set; }

    public virtual DbSet<OtherPayment> OtherPayments { get; set; }

    public virtual DbSet<OtherPaymentsType> OtherPaymentsTypes { get; set; }

    public virtual DbSet<PersonFingerType> PersonFingerTypes { get; set; }

    public virtual DbSet<PersonFingerprint> PersonFingerprints { get; set; }

    public virtual DbSet<PersonFingerprintUseCase> PersonFingerprintUseCases { get; set; }

    public virtual DbSet<ReliablePersonReason> ReliablePersonReasons { get; set; }

    public virtual DbSet<ScriptoriumEmployee> ScriptoriumEmployees { get; set; }

    public virtual DbSet<ScriptoriumEmployeeAccess> ScriptoriumEmployeeAccesses { get; set; }

    public virtual DbSet<ScriptoriumSetup> ScriptoriumSetups { get; set; }

    public virtual DbSet<SignElectronicBook> SignElectronicBooks { get; set; }

    public virtual DbSet<SignRequest> SignRequests { get; set; }

    public virtual DbSet<SignRequestFile> SignRequestFiles { get; set; }

    public virtual DbSet<SignRequestGetter> SignRequestGetters { get; set; }

    public virtual DbSet<SignRequestPerson> SignRequestPeople { get; set; }

    public virtual DbSet<SignRequestPersonRelated> SignRequestPersonRelateds { get; set; }

    public virtual DbSet<SignRequestSemaphore> SignRequestSemaphores { get; set; }

    public virtual DbSet<SignRequestSubject> SignRequestSubjects { get; set; }

    public virtual DbSet<SignRequestSubjectGroup> SignRequestSubjectGroups { get; set; }

    public virtual DbSet<SsrApiExternalUser> SsrApiExternalUsers { get; set; }

    public virtual DbSet<SsrApiExternalUserAccess> SsrApiExternalUserAccesses { get; set; }

    public virtual DbSet<SsrArticle6County> SsrArticle6Counties { get; set; }

    public virtual DbSet<SsrArticle6EstateType> SsrArticle6EstateTypes { get; set; }

    public virtual DbSet<SsrArticle6EstateUsing> SsrArticle6EstateUsings { get; set; }

    public virtual DbSet<SsrArticle6Inq> SsrArticle6Inqs { get; set; }

    public virtual DbSet<SsrArticle6InqPerson> SsrArticle6InqPeople { get; set; }

    public virtual DbSet<SsrArticle6InqReceiver> SsrArticle6InqReceivers { get; set; }

    public virtual DbSet<SsrArticle6InqReceiverOrg> SsrArticle6InqReceiverOrgs { get; set; }

    public virtual DbSet<SsrArticle6InqResponse> SsrArticle6InqResponses { get; set; }

    public virtual DbSet<SsrArticle6OppositReason> SsrArticle6OppositReasons { get; set; }

    public virtual DbSet<SsrArticle6Organ> SsrArticle6Organs { get; set; }

    public virtual DbSet<SsrArticle6Province> SsrArticle6Provinces { get; set; }

    public virtual DbSet<SsrArticle6SubOrgan> SsrArticle6SubOrgans { get; set; }

    public virtual DbSet<SsrConfig> SsrConfigs { get; set; }

    public virtual DbSet<SsrConfigConditionAgntType> SsrConfigConditionAgntTypes { get; set; }

    public virtual DbSet<SsrConfigConditionCostType> SsrConfigConditionCostTypes { get; set; }

    public virtual DbSet<SsrConfigConditionDcprstp> SsrConfigConditionDcprstps { get; set; }

    public virtual DbSet<SsrConfigConditionDoctype> SsrConfigConditionDoctypes { get; set; }

    public virtual DbSet<SsrConfigConditionGeoloc> SsrConfigConditionGeolocs { get; set; }

    public virtual DbSet<SsrConfigConditionScrptrm> SsrConfigConditionScrptrms { get; set; }

    public virtual DbSet<SsrConfigConditionTime> SsrConfigConditionTimes { get; set; }

    public virtual DbSet<SsrConfigConditionUnit> SsrConfigConditionUnits { get; set; }

    public virtual DbSet<SsrConfigMainSubject> SsrConfigMainSubjects { get; set; }

    public virtual DbSet<SsrConfigSubject> SsrConfigSubjects { get; set; }

    public virtual DbSet<SsrDocModifyClassifyNo> SsrDocModifyClassifyNos { get; set; }

    public virtual DbSet<SsrDocVerifCallLog> SsrDocVerifCallLogs { get; set; }

    public virtual DbSet<SsrDocVerifExternalUser> SsrDocVerifExternalUsers { get; set; }

    public virtual DbSet<SsrDocumentAsset> SsrDocumentAssets { get; set; }

    public virtual DbSet<SsrDocumentAssetQuotaDtl> SsrDocumentAssetQuotaDtls { get; set; }

    public virtual DbSet<SsrDocumentAssetQuotum> SsrDocumentAssetQuota { get; set; }

    public virtual DbSet<SsrSignEbookBaseinfo> SsrSignEbookBaseinfos { get; set; }

    public virtual DbSet<SystemMessage> SystemMessages { get; set; }

    public virtual DbSet<ToadPlanTable> ToadPlanTables { get; set; }

    public virtual DbSet<TransactionInfo> TransactionInfos { get; set; }

    public virtual DbSet<TransactionStep> TransactionSteps { get; set; }

    public virtual DbSet<ValueAddedTax> ValueAddedTaxes { get; set; }

    public virtual DbSet<VatInfo> VatInfos { get; set; }

    public virtual DbSet<WorkflowState> WorkflowStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("SSAR_NEW")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AgentType>(entity =>
        {
            entity.ToTable("AGENT_TYPE", tb => tb.HasComment("نوع وابستگی اشخاص"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Adjective).HasComment("صفت");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.DocumentTitle).HasComment("نوع مستند");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Root).HasComment("ریشه");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("BOOK", tb => tb.HasComment("دفاتر دست نويس"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.ScriptoriumId).HasComment("کد دفترخانه");
        });

        modelBuilder.Entity<Bookpage>(entity =>
        {
            entity.ToTable("BOOKPAGES", tb => tb.HasComment("صفحات دفاتر دست نويس"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشاني");
            entity.Property(e => e.BookId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.Pageno).HasComment("شماره صفحه");
            entity.Property(e => e.ScriptoriumId).HasComment("کد دفترخانه");

            entity.HasOne(d => d.Book).WithMany(p => p.Bookpages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_BOOKPAGES_BOOK");
        });

        modelBuilder.Entity<ConfigurationParameter>(entity =>
        {
            entity.ToTable("CONFIGURATION_PARAMETERS", tb => tb.HasComment("پارامترهای پیكربندی سامانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Subject).HasComment("موضوع");
            entity.Property(e => e.Value).HasComment("مقدار");
        });

        modelBuilder.Entity<ConvertLegacyDataLog>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_LOG", tb => tb.HasComment("لاگ سوابق كانورت اطلاعات قدیمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConvertedCount).HasComment("تعداد ركورد كانورت شده");
            entity.Property(e => e.DataSubjectName).HasComment("عنوان موضوع اطلاعاتی");
            entity.Property(e => e.EndDate).HasComment("تاریخ و زمان خاتمه");
            entity.Property(e => e.ErrorLog).HasComment("شرح خطاها");
            entity.Property(e => e.ForceStop).HasComment("آیا اجرای برنامه سریعاً متوقف شود؟");
            entity.Property(e => e.StartDate).HasComment("تاریخ و زمان شروع");
        });

        modelBuilder.Entity<ConvertLegacyDataLogDetail>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_LOG_DETAILS", tb => tb.HasComment("جزئیات لاگ سوابق كانورت اطلاعات قدیمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConvertLegacyDataLogId).HasComment("شناسه لاگ سوابق كانورت اطلاعات قدیمی");
            entity.Property(e => e.DataCategory).HasComment("عنوان موضوع اطلاعاتی");
            entity.Property(e => e.DoDate).HasComment("تاریخ و زمان انجام");
            entity.Property(e => e.ErrorLog).HasComment("شرح خطاها");
            entity.Property(e => e.RecordCount).HasComment("تعداد ركورد كانورت شده");

            entity.HasOne(d => d.ConvertLegacyDataLog).WithMany(p => p.ConvertLegacyDataLogDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONVERT_LEGACY_DATA_LOG_DETAILS_CONVERT_LEGACY_DATA_LOG");
        });

        modelBuilder.Entity<ConvertLegacyDataLogScriptoriumLastDate>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_LOG_SCRIPTORIUM_LAST_DATE", tb => tb.HasComment("آخرین تاریخ در هر دفترخانه كه اطلاعات دفترخانه تا آن تاریخ كانورت شده است"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.LastConvertedDate).HasComment("آخرین تاریخ اطلاعات كانورت شده");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
        });

        modelBuilder.Entity<ConvertLegacyDataRun>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_RUN", tb => tb.HasComment("اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConvertLegacyDataUseCaseDetailsId).HasComment("شناسه جزئیات موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی");
            entity.Property(e => e.ConvertLegacyDataUseCaseId).HasComment("شناسه موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی");
            entity.Property(e => e.ConvertedCount).HasComment("تعداد ركوردهای كانورت شده");
            entity.Property(e => e.ErrorCount).HasComment("تعداد ركوردهای دچار خطا شده");
            entity.Property(e => e.ErrorLog).HasComment("شرح خطاها");
            entity.Property(e => e.FullTimeWorkInHolidays)
                .IsFixedLength()
                .HasComment("آیا در روزهای تعطیل بصورت تمام وقت اجرا شود؟");
            entity.Property(e => e.IsFinished)
                .IsFixedLength()
                .HasComment("آیا اجرای برنامه تمام شده است؟");
            entity.Property(e => e.IsForceStoped)
                .IsFixedLength()
                .HasComment("آیا اجرای برنامه سریعاً متوقف شود؟");
            entity.Property(e => e.RemainedCount).HasComment("تعداد ركوردهای باقیمانده");
            entity.Property(e => e.RunEndDate)
                .IsFixedLength()
                .HasComment("تاریخ اتمام اجرا");
            entity.Property(e => e.RunEndTime).HasComment("زمان اتمام اجرا");
            entity.Property(e => e.RunStartDate)
                .IsFixedLength()
                .HasComment("تاریخ شروع اجرا");
            entity.Property(e => e.RunStartTime).HasComment("زمان شروع اجرا");
            entity.Property(e => e.ScheduledEndDate)
                .IsFixedLength()
                .HasComment("تاریخ برنامه ریزی شده برای اتمام كار");
            entity.Property(e => e.ScheduledEndTime).HasComment("زمان برنامه ریزی شده برای اتمام كار");
            entity.Property(e => e.ScheduledStartDate)
                .IsFixedLength()
                .HasComment("تاریخ برنامه ریزی شده برای شروع كار");
            entity.Property(e => e.ScheduledStartTime).HasComment("زمان برنامه ریزی شده برای شروع كار");
            entity.Property(e => e.TotalCount).HasComment("تعداد كل ركوردهای لازم به كانورت در این موضوع");

            entity.HasOne(d => d.ConvertLegacyDataUseCaseDetails).WithMany(p => p.ConvertLegacyDataRuns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONVERT_LEGACY_DATA_RUN_CONVERT_LEGACY_DATA_USE_CASE_DETAILS");

            entity.HasOne(d => d.ConvertLegacyDataUseCase).WithMany(p => p.ConvertLegacyDataRuns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONVERT_LEGACY_DATA_RUN_CONVERT_LEGACY_DATA_USE_CASE");
        });

        modelBuilder.Entity<ConvertLegacyDataRunDetail>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_RUN_DETAILS", tb => tb.HasComment("جزئیات اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConvertLegacyDataRunId).HasComment("شناسه ركورد اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی");
            entity.Property(e => e.ConvertedCount).HasComment("تعداد ركوردهای كانورت شده");
            entity.Property(e => e.DataCategory).HasComment("شاخصه دسته بندی اطلاعات");
            entity.Property(e => e.ErrorCount).HasComment("تعداد ركوردهای دچار خطا شده");
            entity.Property(e => e.ErrorLog).HasComment("شرح خطاها");
            entity.Property(e => e.FinishDate)
                .IsFixedLength()
                .HasComment("تاریخ اتمام این مرحله");
            entity.Property(e => e.FinishTime).HasComment("زمان اتمام این مرحله");
            entity.Property(e => e.IsFinished)
                .IsFixedLength()
                .HasComment("آیا خاتمه یافته است؟");
            entity.Property(e => e.StartDate)
                .IsFixedLength()
                .HasComment("تاریخ شروع این مرحله");
            entity.Property(e => e.StartTime).HasComment("زمان شروع این مرحله");
            entity.Property(e => e.TotalCount).HasComment("تعداد كل ركوردهای لازم به كانورت در این شاخصه اطلاعات");

            entity.HasOne(d => d.ConvertLegacyDataRun).WithMany(p => p.ConvertLegacyDataRunDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONVERT_LEGACY_DATA_RUN_DETAILS_CONVERT_LEGACY_DATA_RUN");
        });

        modelBuilder.Entity<ConvertLegacyDataRunDetailsError>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_RUN_DETAILS_ERRORS", tb => tb.HasComment("جزئیات ركوردهای خطادار در اجرای برنامه های كانورت اطلاعات قدیمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConvertLegacyDataRunDetailsId).HasComment("شناسه ركورد جزئیات اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی");
            entity.Property(e => e.ConvertLegacyDataRunId).HasComment("شناسه ركورد اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی");
            entity.Property(e => e.ErrorLog).HasComment("شرح خطاها");
            entity.Property(e => e.ObjectContent).HasComment("محتوای آبجكت");
            entity.Property(e => e.TroubledRecordId).HasComment("شناسه ركوردی كه تبدیل اطلاعات آن به مشكل برخورده است");

            entity.HasOne(d => d.ConvertLegacyDataRun).WithMany(p => p.ConvertLegacyDataRunDetailsErrors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONVERT_LEGACY_DATA_RUN_DETAILS_ERRORS_CONVERT_LEGACY_DATA_RUN");
        });

        modelBuilder.Entity<ConvertLegacyDataUseCase>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_USE_CASE", tb => tb.HasComment("موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.RefreshInterval).HasComment("بازه زمانی تازه سازی آمارهای پیشرفت");
            entity.Property(e => e.SmsInterval).HasComment("بازه زمانی ارسال پیامك های آمارهای پیشرفت");
            entity.Property(e => e.SmsReceiversMobileNo).HasComment("شماره موبایل های دریافت كننده پیامك های آمارهای پیشرفت");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ConvertLegacyDataUseCaseDetail>(entity =>
        {
            entity.ToTable("CONVERT_LEGACY_DATA_USE_CASE_DETAILS", tb => tb.HasComment("جزئیات موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.ConvertLegacyDataUseCaseId).HasComment("شناسه موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی");
            entity.Property(e => e.FromDate)
                .IsFixedLength()
                .HasComment("از تاریخ");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.ToDate)
                .IsFixedLength()
                .HasComment("تا تاریخ");

            entity.HasOne(d => d.ConvertLegacyDataUseCase).WithMany(p => p.ConvertLegacyDataUseCaseDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONVERT_LEGACY_DATA_USE_CASE_DETAILS_CONVERT_LEGACY_DATA_USE_CASE");
        });

        modelBuilder.Entity<CostType>(entity =>
        {
            entity.ToTable("COST_TYPE", tb => tb.HasComment("انواع هزینه های خدمات ثبتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DealSummary>(entity =>
        {
            entity.ToTable("DEAL_SUMMARY", tb => tb.HasComment("خلاصه معامله"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Amount).HasComment("مبلغ");
            entity.Property(e => e.AmountUnitId).HasComment("ردیف واحد مبلغ");
            entity.Property(e => e.AttachedText).HasComment("متن منضم");
            entity.Property(e => e.CertificateBase64).HasComment("گواهی امضای دیجیتال");
            entity.Property(e => e.DataDigitalSignature).HasComment("امضای دیجیتال");
            entity.Property(e => e.DealDate)
                .IsFixedLength()
                .HasComment("تاریخ خلاصه معامله");
            entity.Property(e => e.DealNo).HasComment("شماره خلاصه معامله");
            entity.Property(e => e.DealSummaryTransferTypeId).HasComment("ردیف نوع انتقال");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Duration).HasComment("مدت");
            entity.Property(e => e.EstateInquiryId).HasComment("ردیف استعلام ");
            entity.Property(e => e.EstateOwnershipTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.EstateTransitionTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.FirstReceiveDate)
                .IsFixedLength()
                .HasComment("تاریخ دریافت اولین ارسال");
            entity.Property(e => e.FirstReceiveTime).HasComment("زمان دریافت اولین ارسال");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LasteditReceiveDate)
                .IsFixedLength()
                .HasComment("تاریخ دریافت آخرین ویرایش");
            entity.Property(e => e.LasteditReceiveTime).HasComment("زمان دریافت آخرین ویرایش");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.NewNotaryDocumentId).HasComment("شناسه سند در سیستم ثبت آنی جدید");
            entity.Property(e => e.No).HasComment("شماره سیستمی");
            entity.Property(e => e.NotaryDocumentId).HasComment("شناسه سند ثبت انی قدیم");
            entity.Property(e => e.RemoveRestrictionDate)
                .IsFixedLength()
                .HasComment("تاریخ رفع محدودیت");
            entity.Property(e => e.RemoveRestrictionNo).HasComment("شماره رفع محدودیت");
            entity.Property(e => e.Response).HasComment("متن پاسخ");
            entity.Property(e => e.ResponseDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ");
            entity.Property(e => e.ResponseNumber).HasComment("شماره پاسخ");
            entity.Property(e => e.ResponseReceiveDate)
                .IsFixedLength()
                .HasComment("تاریخ دریافت پاسخ");
            entity.Property(e => e.ResponseReceiveTime).HasComment("زمان دریافت پاسخ");
            entity.Property(e => e.ResponseTime).HasComment("زمان پاسخ");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.SubjectDn).HasComment("موضوع امضای دیجیتال");
            entity.Property(e => e.TimeUnitId).HasComment("ردیف واحد مدت");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.TransactionDate)
                .IsFixedLength()
                .HasComment("تاریخ انجام معامله");
            entity.Property(e => e.UnrestrictionTypeId).HasComment("ردیف نوع رفع محدودیت");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.DealSummaryTransferType).WithMany(p => p.DealSummaries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEAL_SUMMARY_TRANSFER_TYPE");

            entity.HasOne(d => d.EstateInquiry).WithMany(p => p.DealSummaries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEAL_SUMMARY_ESTATE_INQUIRY");

            entity.HasOne(d => d.EstateOwnershipType).WithMany(p => p.DealSummaries).HasConstraintName("FK_DEAL_SUMMARY_ESTATE_OWNERSHIP_TYPE");

            entity.HasOne(d => d.EstateTransitionType).WithMany(p => p.DealSummaries).HasConstraintName("FK_DEAL_SUMMARY_ESTATE_TRANSITION_TYPE");

            entity.HasOne(d => d.NewNotaryDocument).WithMany(p => p.DealSummaries).HasConstraintName("FK_DEAL_SUMMARY_DOCUMENT");

            entity.HasOne(d => d.UnrestrictionType).WithMany(p => p.DealSummaries).HasConstraintName("FK_DEAL_SUMMARY_UNRESTRICTTYPE");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.DealSummaries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEALSUMMARY_WORKFLOWSTATES");
        });

        modelBuilder.Entity<DealSummaryActionType>(entity =>
        {
            entity.ToTable("DEAL_SUMMARY_ACTION_TYPE", tb => tb.HasComment("نوع اقدام خلاصه معامله"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DealSummaryPerson>(entity =>
        {
            entity.ToTable("DEAL_SUMMARY_PERSON", tb => tb.HasComment("اشخاص خلاصه معامله"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس");
            entity.Property(e => e.BirthDate)
                .IsFixedLength()
                .HasComment("تاریخ تولد/ثبت");
            entity.Property(e => e.BirthPlaceId).HasComment("ردیف محل تولد");
            entity.Property(e => e.CityId).HasComment("ردیف شهر");
            entity.Property(e => e.ConditionText).HasComment("متن شرط");
            entity.Property(e => e.DealSummaryId).HasComment("ردیف خلاصه معامله");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه/ثبت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsInquiryPerson)
                .IsFixedLength()
                .HasComment("آيا همان شخص استعلام است؟");
            entity.Property(e => e.IssuePlaceId).HasComment("ردیف محل صدور");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalityCode).HasComment("شماره/شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("ردیف تابعیت");
            entity.Property(e => e.OctantQuarter).HasComment("ثمنیه-ربعیه");
            entity.Property(e => e.OctantQuarterPart).HasComment("جز ثمنیه-ربعیه");
            entity.Property(e => e.OctantQuarterTotal).HasComment("كل ثمنیه-ربعیه");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RelatedPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RelationTypeId).HasComment("ردیف نوع ارتباط در معامله");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه");
            entity.Property(e => e.SeriAlpha).HasComment("بخش حرفي شماره شناسنامه");
            entity.Property(e => e.SerialNo).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.SharePart).HasComment("جز سهم");
            entity.Property(e => e.ShareText).HasComment("متن سهم");
            entity.Property(e => e.ShareTotal).HasComment("كل سهم");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.DealSummary).WithMany(p => p.DealSummaryPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEAL_SUMMARY_PERSON_DEAL_SUMMARY");

            entity.HasOne(d => d.RelationType).WithMany(p => p.DealSummaryPeople).HasConstraintName("FK_DEAL_SUMMARY_PERSON_REL_TYPE");
        });

        modelBuilder.Entity<DealSummarySendreceiveLog>(entity =>
        {
            entity.ToTable("DEAL_SUMMARY_SENDRECEIVE_LOG", tb => tb.HasComment("سوابق ارسال و دریافت پاسخ خلاصه معامله"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ActionDate)
                .IsFixedLength()
                .HasComment("تاریخ اقدام");
            entity.Property(e => e.ActionNumber).HasComment("شماره اقدام");
            entity.Property(e => e.ActionText).HasComment("متن اقدام");
            entity.Property(e => e.ActionTime).HasComment("زمان اقدام");
            entity.Property(e => e.DealSummaryActionTypeId).HasComment("ردیف نوع خلاصه معامله");
            entity.Property(e => e.DealSummaryId).HasComment("ردیف خلاصه معامله");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.DealSummaryActionType).WithMany(p => p.DealSummarySendreceiveLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEAL_SUMMARY_SENDRECIEVE_LOG_ACTION_TYPE");

            entity.HasOne(d => d.DealSummary).WithMany(p => p.DealSummarySendreceiveLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEAL_SUMMARY_SENDRECEIVE_LOG_DEAL_SUMMARY");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.DealSummarySendreceiveLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEAL_SUMMARYSENDRECEIVELOG_WORKFLOWSTATES");
        });

        modelBuilder.Entity<DealSummaryStatus>(entity =>
        {
            entity.ToTable("DEAL_SUMMARY_STATUS", tb => tb.HasComment("وضعیت  خلاصه معامله"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DealSummaryTransferType>(entity =>
        {
            entity.ToTable("DEAL_SUMMARY_TRANSFER_TYPE", tb => tb.HasComment("نوع انتقال"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.Isrestricted)
                .IsFixedLength()
                .HasComment("محدودیت می باشد");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DealsummaryPersonRelateType>(entity =>
        {
            entity.ToTable("DEALSUMMARY_PERSON_RELATE_TYPE", tb => tb.HasComment("نوع ارتباط در معامله"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DealsummaryUnrestrictionType>(entity =>
        {
            entity.ToTable("DEALSUMMARY_UNRESTRICTION_TYPE", tb => tb.HasComment("نوع رفع محدودیت"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DigitalSignatureConfiguration>(entity =>
        {
            entity.ToTable("DIGITAL_SIGNATURE_CONFIGURATION", tb => tb.HasComment("کانفيگ امضاي الکترونيک"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ConfigName).HasComment("نام کانفيگ");
            entity.Property(e => e.Descriptor).HasComment("توصيف");
            entity.Property(e => e.FormName).HasComment("نام فرم");
            entity.Property(e => e.GetDataAsJson)
                .IsFixedLength()
                .HasComment("آيا بصورت جيسون گرفته شود؟");
            entity.Property(e => e.IsForNewSystemEntity)
                .IsFixedLength()
                .HasComment("آيا مربوط به سامانه جديد است؟");
            entity.Property(e => e.JsonFormatting)
                .IsFixedLength()
                .HasComment("فرمت جيسون");
            entity.Property(e => e.JsonSerializerSettings).HasComment("تنظيمات جيسون");
            entity.Property(e => e.LagacyId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RelatedCertificateField).HasComment("فيلد گواهي وابسته");
            entity.Property(e => e.RelatedDigitalSignField).HasComment("فيلد وابسته");
            entity.Property(e => e.RelatedRepository).HasComment("ريپو وابسته");
            entity.Property(e => e.RelatedRepositoryMethod).HasComment("متد ريپو وابسته");
            entity.Property(e => e.VersionNo).HasComment("شماره نسخه");
        });

        modelBuilder.Entity<DigitalSignaturePropertyMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DGITAL_SIGNATURE_PROPERTY_M");

            entity.ToTable("DIGITAL_SIGNATURE_PROPERTY_MAPPING", tb => tb.HasComment("نگاشت آیتم های امضا از سامانه قدیم به جدید"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DigitalSignatureConfigurationId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OwnerType).HasComment("نوع");

            entity.HasOne(d => d.DigitalSignatureConfiguration).WithMany(p => p.DigitalSignaturePropertyMappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIGITAL_SIGNATURE_PROPERTY_MAPPING_CONFIGURATION");
        });

        modelBuilder.Entity<DigitalSignaturePropertyMappingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DIGITAL_SIGNATURE_PROPERTY_");

            entity.ToTable("DIGITAL_SIGNATURE_PROPERTY_MAPPING_DETAIL", tb => tb.HasComment("جزییات نگاشت آیتم های امضا از سامانه قدیم به جدید"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DigitalSignaturePropertyMappingId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.NewPropertyName).HasComment("نام ويژگي جديد");
            entity.Property(e => e.NewPropertyOwnerPath).HasComment("مسير ويژگي جديد");
            entity.Property(e => e.OldPropertyName).HasComment("نام ويژگي قبلي");
            entity.Property(e => e.OldPropertyType).HasComment("نوع ويژگي قبلي");

            entity.HasOne(d => d.DigitalSignaturePropertyMapping).WithMany(p => p.DigitalSignaturePropertyMappingDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIGITAL_SIGNATURE_PROPERTY_MAPPING_PARENT");
        });

        modelBuilder.Entity<DigitalSignatureValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DIGITAL_SIGNATURE_VALUE_ID");

            entity.ToTable("DIGITAL_SIGNATURE_VALUE", tb => tb.HasComment("محتواي امضاي الکترونيک"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Certificate).HasComment("گواهي");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاريخ ايجاد");
            entity.Property(e => e.CreateTime).HasComment("زمان ايجاد");
            entity.Property(e => e.DataHandlerInput).HasComment("ورودي هنلدر");
            entity.Property(e => e.DataHandlerName).HasComment("نام هندلر");
            entity.Property(e => e.ExpireDate)
                .IsFixedLength()
                .HasComment("تاريخ اعتبار");
            entity.Property(e => e.ExpireTime).HasComment("زمان اعتبار");
            entity.Property(e => e.MainObjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.SignData).HasComment("داده ی امضا شدها");
            entity.Property(e => e.SignValue).HasComment("مقدار امضا");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("DOCUMENT", tb => tb.HasComment("اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض پرداخت");
            entity.Property(e => e.BookPapersNo).HasComment("شماره صفحه دفتر دستنویس");
            entity.Property(e => e.BookVolumeNo).HasComment("شماره جلد دفتر دستنویس");
            entity.Property(e => e.ClassifyNo).HasComment("شماره ترتیب سند");
            entity.Property(e => e.ClassifyNoReserved).HasComment("شماره ترتیب - رزرو شده");
            entity.Property(e => e.CostPaymentDate).HasComment("تاریخ پرداخت هزینه ها");
            entity.Property(e => e.CostPaymentTime).HasComment("زمان پرداخت هزینه ها");
            entity.Property(e => e.CurrencyTypeId).HasComment("شناسه نوع ارز");
            entity.Property(e => e.DocumentDate).HasComment("تاریخ سند");
            entity.Property(e => e.DocumentSecretCode).HasComment("رمز تصدیق سند");
            entity.Property(e => e.DocumentTypeId).HasComment("شناسه نوع سند");
            entity.Property(e => e.DocumentTypeTitle).HasComment("عنوان نوع سند سایر");
            entity.Property(e => e.FactorDate).HasComment("تاریخ تراكنش/فیش");
            entity.Property(e => e.FactorNo).HasComment("شماره تراكنش/فیش");
            entity.Property(e => e.GetDocumentNoDate).HasComment("تاریخ اخذ شناسه یكتا");
            entity.Property(e => e.HowToPay).HasComment("شیوه پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsBasedJudgment)
                .IsFixedLength()
                .HasComment("آیا ثبت سند براساس حكم دادگاه یا سایر مراجع قانونی است؟");
            entity.Property(e => e.IsCostCalculateConfirmed)
                .IsFixedLength()
                .HasComment("آیا همه هزینه های قانونی سند محاسبه شده است؟");
            entity.Property(e => e.IsCostPaymentConfirmed)
                .IsFixedLength()
                .HasComment("آیا همه هزینه های قانونی سند پرداخت شده است؟");
            entity.Property(e => e.IsFinalVerificationVisited)
                .IsFixedLength()
                .HasComment("آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟");
            entity.Property(e => e.IsRahProcessed)
                .IsFixedLength()
                .HasComment("آیا سند نقل و انتقال وسیله نقلیه برای وزارت راه آماده سازی شده است؟");
            entity.Property(e => e.IsRegistered)
                .IsFixedLength()
                .HasComment("آیا ملك ثبت شده است؟");
            entity.Property(e => e.IsRelatedDocAbroad)
                .IsFixedLength()
                .HasComment("آیا سند وابسته در مراجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.IsRemoteRequest)
                .IsFixedLength()
                .HasComment("آیا بصورت غیرحضوری درخواست شده است؟");
            entity.Property(e => e.IsSentToTaxOrganization)
                .IsFixedLength()
                .HasComment("آیا خلاصه معامله برای سازمان مالیاتی ارسال شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.NationalNo).HasComment("شناسه یكتا سند");
            entity.Property(e => e.OldRelatedDocAbroadCountryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldRelatedDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PaymentType).HasComment("نوع پرداخت");
            entity.Property(e => e.Price).HasComment("مبلغ سند");
            entity.Property(e => e.ReceiptNo).HasComment("شماره مرجع تراكنش یا شناسه پرداخت");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده به میلادی");
            entity.Property(e => e.RefundDescription).HasComment("ملاحظات استرداد");
            entity.Property(e => e.RefundId).HasComment("كلید اصلی ركورد استرداد");
            entity.Property(e => e.RefundPrice).HasComment("مبلغ استرداد حق الثبت غیركاداستر");
            entity.Property(e => e.RefundPriceHaghosabtCadastre).HasComment("مبلغ استرداد شده حق الثبت كاداستر");
            entity.Property(e => e.RefundPriceHaghosabtHalfPrcnt).HasComment("مبلغ استرداد شده افزایش نیم درصد حق الثبت");
            entity.Property(e => e.RefundState)
                .IsFixedLength()
                .HasComment("وضعیت استرداد");
            entity.Property(e => e.RegionalPrice).HasComment("قیمت منطقه ای ملك");
            entity.Property(e => e.RelatedDocAbroadCountryId).HasComment("شناسه كشوری كه سند وابسته در آن تنظیم شده است؟");
            entity.Property(e => e.RelatedDocumentDate).HasComment("تاریخ سند وابسته");
            entity.Property(e => e.RelatedDocumentId).HasComment("شناسه سند وابسته");
            entity.Property(e => e.RelatedDocumentIsInSsar)
                .IsFixedLength()
                .HasComment("آیا سند وابسته در سیستم ثبت الكترونیك اسناد ثبت شده است؟");
            entity.Property(e => e.RelatedDocumentNo).HasComment("شماره سند وابسته");
            entity.Property(e => e.RelatedDocumentScriptorium).HasComment("شماره و محل دفترخانه صادركننده سند وابسته");
            entity.Property(e => e.RelatedDocumentSecretCode).HasComment("رمز تصدیق سند وابسته");
            entity.Property(e => e.RelatedDocumentSmsId).HasComment("شناسه ركورد پیامك معادل سند وابسته");
            entity.Property(e => e.RelatedDocumentTypeId).HasComment("شناسه نوع سند وابسته");
            entity.Property(e => e.RelatedRegCaseId).HasComment("شناسه مورد ثبتی وابسته");
            entity.Property(e => e.RelatedScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند وابسته");
            entity.Property(e => e.RemoteRequestId).HasComment("شناسه درخواست غیرحضوری");
            entity.Property(e => e.RequestDate).HasComment("تاریخ درخواست");
            entity.Property(e => e.RequestNo).HasComment("شماره درخواست");
            entity.Property(e => e.RequestTime).HasComment("زمان درخواست");
            entity.Property(e => e.SabtPrice).HasComment("مأخذ حق الثبت سند");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SignDate).HasComment("تاریخ امضاء سند");
            entity.Property(e => e.SignTime).HasComment("زمان امضاء سند");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت پرونده");
            entity.Property(e => e.WriteInBookDate).HasComment("تاریخ ورود سند به دفتر");
            entity.Property(e => e.WriteInBookDateReserved).HasComment("تاریخ ورود به دفتر - رزرو شده");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentDocumentTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_DOCUMENT_TYPE");

            entity.HasOne(d => d.RelatedDocument).WithMany(p => p.InverseRelatedDocument).HasConstraintName("FK_DOCUMENT_RELATED_DOCUMENT");

            entity.HasOne(d => d.RelatedDocumentType).WithMany(p => p.DocumentRelatedDocumentTypes).HasConstraintName("FK_DOCUMENT_RELATED_DOCUMENT_TYPE");
        });

        modelBuilder.Entity<DocumentAssetType>(entity =>
        {
            entity.ToTable("DOCUMENT_ASSET_TYPE", tb => tb.HasComment("انواع سایر اموال منقول"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.ParentDocumentAssetTypeId).HasComment("شناسه ركورد بالادست");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.ParentDocumentAssetType).WithMany(p => p.InverseParentDocumentAssetType).HasConstraintName("FK_DOCUMENT_ASSET_TYPE_PARENT_DOCUMENT_ASSET_TYPE");
        });

        modelBuilder.Entity<DocumentCase>(entity =>
        {
            entity.ToTable("DOCUMENT_CASE", tb => tb.HasComment("موارد ثبت شده در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.Title).HasComment("عنوان مورد ثبت");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentCases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_CASE_DOCUMENT");
        });

        modelBuilder.Entity<DocumentCost>(entity =>
        {
            entity.ToTable("DOCUMENT_COST", tb => tb.HasComment("ریز هزینه های اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ChangeReason).HasComment("دلیل ابراز شده برای تغییر در هزینه توسط دفترخانه");
            entity.Property(e => e.CostTypeId).HasComment("شناسه نوع هزینه");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.EpaymentMethodId).HasComment("شناسه نوع پرداخت");
            entity.Property(e => e.FactorDate)
                .IsFixedLength()
                .HasComment("تاریخ تراكنش/فیش");
            entity.Property(e => e.FactorNo).HasComment("شماره تراكنش/فیش");
            entity.Property(e => e.FactorTime).HasComment("زمان تراكنش/فیش");
            entity.Property(e => e.Ilm)
                .HasDefaultValueSql("1                     ")
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Price).HasComment("مبلغ");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.TerminalNo).HasComment("شماره ترمینال");

            entity.HasOne(d => d.CostType).WithMany(p => p.DocumentCosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_COST_COST_TYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentCosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_COST_DOCUMENT");
        });

        modelBuilder.Entity<DocumentCostUnchanged>(entity =>
        {
            entity.ToTable("DOCUMENT_COST_UNCHANGED", tb => tb.HasComment("ریز هزینه های اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CostTypeId).HasComment("شناسه نوع هزینه");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Price).HasComment("مبلغ");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");

            entity.HasOne(d => d.CostType).WithMany(p => p.DocumentCostUnchangeds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_COST_UNCHANGED_COST_TYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentCostUnchangeds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_COST_UNCHANGED_DOCUMENT");
        });

        modelBuilder.Entity<DocumentElectronicBook>(entity =>
        {
            entity.ToTable("DOCUMENT_ELECTRONIC_BOOK", tb => tb.HasComment("دفتر الكترونیك اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ClassifyNo).HasComment("شماره ترتیب سند");
            entity.Property(e => e.ClassifyNoReserved).HasComment("شماره ترتیب - رزرو شده");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentDate).HasComment("تاریخ سند");
            entity.Property(e => e.DocumentTypeId).HasComment("شناسه نوع سند");
            entity.Property(e => e.EnterBookDateTime).HasComment("تاریخ و زمان ورود به دفتر");
            entity.Property(e => e.ExordiumConfirmDateTime).HasComment("تاریخ و زمان امضای سردفتر");
            entity.Property(e => e.ExordiumDigitalSign).HasComment("امضای الكترونیك سردفتر");
            entity.Property(e => e.HashOfFingerprints).HasComment("hash of fingerprints");
            entity.Property(e => e.HashOfPdf).HasComment("hash of document PDF");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("وضعیت ركورد از لحاظ آرشیو");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد در سامانه قدیمی");
            entity.Property(e => e.NationalNo).HasComment("شناسه یكتا سند");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ركورد به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SignDate).HasComment("تاریخ امضاء");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentElectronicBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ELECTRONIC_BOOK_DOCUMENT_TYPE");
        });

        modelBuilder.Entity<DocumentElectronicBookBaseinfo>(entity =>
        {
            entity.ToTable("DOCUMENT_ELECTRONIC_BOOK_BASEINFO", tb => tb.HasComment("اطلاعات پایه دفتر الكترونیك اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ExordiumConfirmDate).HasComment("تاریخ امضای سردفتر");
            entity.Property(e => e.ExordiumConfirmTime).HasComment("زمان امضای سردفتر");
            entity.Property(e => e.ExordiumDigitalSign).HasComment("امضای الكترونیك سردفتر");
            entity.Property(e => e.LastClassifyNo).HasComment("شماره ترتیب آخرین سند ثبت شده در دفاتر كاغذی");
            entity.Property(e => e.LastRegisterPaperNo).HasComment("شماره صفحه دفتر آخرین سند ثبت شده در دفاتر كاغذی");
            entity.Property(e => e.LastRegisterVolumeNo).HasComment("شماره جلد دفتر آخرین سند ثبت شده در دفاتر كاغذی");
            entity.Property(e => e.NumberOfBooks).HasComment("تعداد كل دفاتر كاغذی");
            entity.Property(e => e.NumberOfBooksAgent).HasComment("تعداد دفاتر نماینده");
            entity.Property(e => e.NumberOfBooksArzi).HasComment("تعداد دفاتر اصلاحات ارضی");
            entity.Property(e => e.NumberOfBooksJari).HasComment("تعداد دفاتر جاری (سردفتر)");
            entity.Property(e => e.NumberOfBooksOghaf).HasComment("تعداد دفاتر اوقاف");
            entity.Property(e => e.NumberOfBooksOthers).HasComment("تعداد دفاتر سایر");
            entity.Property(e => e.NumberOfBooksRahni).HasComment("تعداد دفاتر رهنی");
            entity.Property(e => e.NumberOfBooksVehicle).HasComment("تعداد دفاتر اتومبیل");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
        });

        modelBuilder.Entity<DocumentEstate>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE", tb => tb.HasComment("املاك ثبت شده در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.Area).HasComment("مساحت");
            entity.Property(e => e.AreaDescription).HasComment("توضیحات مساحت");
            entity.Property(e => e.AttachmentDescription).HasComment("توضیحات - در انتقال منضم");
            entity.Property(e => e.AttachmentSpecifications).HasComment("مشخصات منضم - در انتقال منضم");
            entity.Property(e => e.AttachmentType).HasComment("نوع منضم");
            entity.Property(e => e.AttachmentTypeOthers).HasComment("نوع منضم - سایر");
            entity.Property(e => e.BasicPlaque).HasComment("پلاك ثبتی اصلی");
            entity.Property(e => e.BasicPlaqueHasRemain)
                .IsFixedLength()
                .HasComment("پلاك اصلی باقیمانده دارد؟");
            entity.Property(e => e.Block).HasComment("شماره بلوك");
            entity.Property(e => e.CommitmentPrice).HasComment("وجه التزام");
            entity.Property(e => e.Commons).HasComment("مشاعات و مشتركات");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.Direction).HasComment("سمت");
            entity.Property(e => e.DivFromBasicPlaque).HasComment("مفروز و مجزی از اصلی");
            entity.Property(e => e.DivFromSecondaryPlaque).HasComment("مفروز و مجزی از فرعی");
            entity.Property(e => e.DocumentEstateTypeId).HasComment("شناسه انواع املاك مندرج در اسناد رسمی");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.EstateInquiryId).HasComment("شناسه ركورد استعلام معادل در جدول سیستم ملك");
            entity.Property(e => e.EstateSectionId).HasComment("شناسه بخش ثبتی");
            entity.Property(e => e.EstateSubsectionId).HasComment("شناسه ناحیه ثبتی");
            entity.Property(e => e.EvacuatedDate).HasComment("تاریخ تخلیه");
            entity.Property(e => e.EvacuationDescription).HasComment("توضیحات در مورد وضعیت تخلیه");
            entity.Property(e => e.EvacuationPapers).HasComment("مشخصات قبوض تخلیه");
            entity.Property(e => e.FieldOrGrandee)
                .IsFixedLength()
                .HasComment("عرصه یا اعیان");
            entity.Property(e => e.Floor).HasComment("طبقه");
            entity.Property(e => e.GeoLocationId).HasComment("شناسه محل جغرافیایی ملك");
            entity.Property(e => e.GrandeeExceptionDetailQuota).HasComment("جزء استثناء");
            entity.Property(e => e.GrandeeExceptionTotalQuota).HasComment("كل استثناء");
            entity.Property(e => e.GrandeeExceptionType)
                .IsFixedLength()
                .HasComment("نوع استثناء - ثمنیه یا ربعیه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.ImmovaleType).HasComment("نوع ملك");
            entity.Property(e => e.IsAttachment)
                .IsFixedLength()
                .HasComment("آیا انتقال منضم است؟");
            entity.Property(e => e.IsEvacuated)
                .IsFixedLength()
                .HasComment("بنا به اظهار متعاملین ملك تخلیه شده است؟");
            entity.Property(e => e.IsFacilitationLaw)
                .IsFixedLength()
                .HasComment("آیا برای این ملك از قانون تسهیل استفاده شده است؟");
            entity.Property(e => e.IsProportionateQuota)
                .IsFixedLength()
                .HasComment("آیا سهم بندی مورد معامله بین اصحاب سند بصورت حسب السهم خواهد بود؟");
            entity.Property(e => e.IsRegistered)
                .IsFixedLength()
                .HasComment("آیا ملك ثبت شده است؟");
            entity.Property(e => e.IsRemortage)
                .IsFixedLength()
                .HasComment("رهن مجدد با حفظ حقوق سند رهنی قبلی است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Limits).HasComment("حدود");
            entity.Property(e => e.LocationType)
                .IsFixedLength()
                .HasComment("نوع محل استقرار");
            entity.Property(e => e.MunicipalityDate).HasComment("تاریخ پایان كار شهرداری");
            entity.Property(e => e.MunicipalityIssuer).HasComment("مرجع صدور پایان كار شهرداری");
            entity.Property(e => e.MunicipalityNo).HasComment("شماره پایان كار شهرداری");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateSectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateSubsectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldGeoLocationId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldSaleDescription).HasComment("سلسله انتقالات - ایادی");
            entity.Property(e => e.OwnershipDetailQuota).HasComment("جزء سهم مورد مالكیت");
            entity.Property(e => e.OwnershipTotalQuota).HasComment("كل سهم مورد مالكیت");
            entity.Property(e => e.OwnershipType)
                .IsFixedLength()
                .HasComment("نوع مالكیت");
            entity.Property(e => e.Piece).HasComment("شماره قطعه");
            entity.Property(e => e.PlaqueText).HasComment("پلاك متنی");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.Price).HasComment("مبلغ سند");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.ReceiverBasicPlaque).HasComment("پلاك ثبتی اصلی - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.ReceiverBasicPlaqueHasRemain)
                .IsFixedLength()
                .HasComment("پلاك اصلی باقیمانده دارد؟ - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.ReceiverDivFromBasicPlaque).HasComment("مفروز و مجزی از اصلی - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.ReceiverDivFromSecondaryPlaque).HasComment("مفروز و مجزی از فرعی - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.ReceiverPlaqueText).HasComment("پلاك متنی - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.ReceiverSecondaryPlaque).HasComment("پلاك ثبتی فرعی - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.ReceiverSecondaryPlaqueHasRemain)
                .IsFixedLength()
                .HasComment("پلاك فرعی باقیمانده دارد؟ - ملكی كه منضم به آن منتقل می شود");
            entity.Property(e => e.RegionalPrice).HasComment("ارزش منطقه ای ملك");
            entity.Property(e => e.RegisterCaseTitle).HasComment("عنوان مورد ثبتی");
            entity.Property(e => e.Rights).HasComment("حقوق ارتفاقی");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.SecondaryPlaque).HasComment("پلاك ثبتی فرعی");
            entity.Property(e => e.SecondaryPlaqueHasRemain)
                .IsFixedLength()
                .HasComment("پلاك فرعی باقیمانده دارد؟");
            entity.Property(e => e.SellDetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.SellTotalQuota).HasComment("كل سهم مورد معامله");
            entity.Property(e => e.SeparationDate).HasComment("تاریخ صورتمجلس تفكیكی");
            entity.Property(e => e.SeparationDescription).HasComment("توضیحات صورتمجلس تفكیكی");
            entity.Property(e => e.SeparationIssuer).HasComment("مرجع صدور صورتمجلس تفكیكی");
            entity.Property(e => e.SeparationNo).HasComment("شماره صورتمجلس تفكیكی");
            entity.Property(e => e.SeparationText).HasComment("متن صورتمجلس تفكیكی");
            entity.Property(e => e.SeparationType)
                .IsFixedLength()
                .HasComment("نوع تفكیك");
            entity.Property(e => e.SomnyehRobeyehActionType)
                .IsFixedLength()
                .HasComment("نوع عمل ثمنیه یا ربعیه");
            entity.Property(e => e.SomnyehRobeyehFieldGrandee)
                .IsFixedLength()
                .HasComment("عرصه یا اعیان در ثمنیه یا ربعیه");
            entity.Property(e => e.UnitId).HasComment("شناسه حوزه ثبتی");

            entity.HasOne(d => d.DocumentEstateType).WithMany(p => p.DocumentEstates).HasConstraintName("FK_DOCUMENT_ESTATE_DOCUMENT_ESTATE_TYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentEstates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_DOCUMENT");
        });

        modelBuilder.Entity<DocumentEstateAttachment>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_ATTACHMENT", tb => tb.HasComment("منضمات املاك ثبت شده در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Area).HasComment("مساحت");
            entity.Property(e => e.AttachmentType).HasComment("نوع منضم");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در اسناد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Location).HasComment("حدود محل");
            entity.Property(e => e.No).HasComment("شماره");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentEstateAttachments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_ATTACHMENT_DOCUMENT_ESTATE");
        });

        modelBuilder.Entity<DocumentEstateDealSummaryGenerated>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED", tb => tb.HasComment("خلاصه معامله های تولید شده در ثبت اسناد ملكی (به غیر از تقسیم نامه)"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد");
            entity.Property(e => e.CreateTime).HasComment("زمان ایجاد");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.GeneratedDealSummaryCount).HasComment("تعداد خلاصه معامله (های) ایجاد شده");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsSent)
                .IsFixedLength()
                .HasComment("آیا ارسال شده است؟");
            entity.Property(e => e.LastErrorDescription).HasComment("آخرین پیام خطای دریافت شده هنگام ارسال");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده سند به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SendAttemptCount).HasComment("تعداد دفعات تلاش برای ارسال");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.Xml).HasComment("ایكس ام ال خلاصه معامله (های) تولید شده");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentEstateDealSummaryGenerateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED_DOCUMENT");
        });

        modelBuilder.Entity<DocumentEstateDealSummarySelected>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_DEAL_SUMMARY_SELECTED", tb => tb.HasComment("خلاصه معامله های انتخاب شده در ثبت اسناد ملكی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.EstateInquiryId).HasComment("شناسه استعلام ملك");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قبلی");
            entity.Property(e => e.OldEstateInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Xml).HasComment("اكس ام ال خلاصه معامله");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentEstateDealSummarySelecteds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_DEAL_SUMMARY_SELECTED_DOCUMENT");
        });

        modelBuilder.Entity<DocumentEstateDealSummarySeparation>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_DEAL_SUMMARY_SEPARATION", tb => tb.HasComment("خلاصه معامله های تولید شده در ثبت اسناد تقسیم نامه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد");
            entity.Property(e => e.CreateTime).HasComment("زمان ایجاد");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.GeneratedDealSummaryCount).HasComment("تعداد خلاصه معامله (های) ایجاد شده");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsSent)
                .IsFixedLength()
                .HasComment("آیا ارسال شده است؟");
            entity.Property(e => e.LastErrorDescription).HasComment("آخرین پیام خطای دریافت شده هنگام ارسال");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده سند به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SendAttemptCount).HasComment("تعداد دفعات تلاش برای ارسال");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.Xml).HasComment("ایكس ام ال خلاصه معامله (های) تولید شده");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentEstateDealSummarySeparations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_DEAL_SUMMARY_SEPARATION_DOCUMENT");
        });

        modelBuilder.Entity<DocumentEstateInquiry>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_INQUIRY", tb => tb.HasComment("استعلام های ملكی مربوط به املاك ثبت شده در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در اسناد");
            entity.Property(e => e.EstateInquiryId).HasComment("شناسه استعلام ملك");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قبلی");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentEstateInquiries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_INQUIRY_DOCUMENT_ESTATE");
        });

        modelBuilder.Entity<DocumentEstateOwnershipDocument>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT", tb => tb.HasComment("مستندات مالكیت املاك مندرج در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DealSummaryText).HasComment("موارد لازم به ذكر در خلاصه معامله");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در سند");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه اشخاص سند");
            entity.Property(e => e.EstateBookNo).HasComment("شماره دفتر املاك");
            entity.Property(e => e.EstateBookPageNo).HasComment("شماره صفحه دفتر املاك");
            entity.Property(e => e.EstateBookType)
                .IsFixedLength()
                .HasComment("نوع دفتر املاك");
            entity.Property(e => e.EstateDocumentNo).HasComment("شماره چاپی سند");
            entity.Property(e => e.EstateDocumentType)
                .IsFixedLength()
                .HasComment("نوع سند ملك");
            entity.Property(e => e.EstateElectronicPageNo).HasComment("شماره صفحه دفتر الكترونیك املاك");
            entity.Property(e => e.EstateInquiriesId).HasComment("شناسه استعلام های املاك مربوط به سند");
            entity.Property(e => e.EstateIsReplacementDocument)
                .IsFixedLength()
                .HasComment("آیا سند المثنی است؟");
            entity.Property(e => e.EstateSabtNo).HasComment("شماره ثبت");
            entity.Property(e => e.EstateSeridaftarId).HasComment("شناسه سری دفتر املاك");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MortgageText).HasComment("متن رهن");
            entity.Property(e => e.NotaryDocumentDate).HasComment("تاریخ صدور سند بیع دفترخانه");
            entity.Property(e => e.NotaryDocumentNo).HasComment("شماره سند بیع دفترخانه");
            entity.Property(e => e.NotaryLocation).HasComment("محل دفترخانه صادركننده سند بیع");
            entity.Property(e => e.NotaryNo).HasComment("شماره دفترخانه صادركننده سند بیع");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateInquiriesId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateSeridaftarId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OwnershipDocumentType)
                .IsFixedLength()
                .HasComment("نوع مستند مالكیت");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.SabtStateReportDate).HasComment("تاریخ گزارش وضعیت ثبتی");
            entity.Property(e => e.SabtStateReportNo).HasComment("شماره گزارش وضعیت ثبتی");
            entity.Property(e => e.SabtStateReportUnitName).HasComment("نام واحد ثبتی صادركننده گزارش وضعیت ثبتی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SpecificationsText).HasComment("شرح مشخصات مورد ثبتی مطابق سند");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentEstateOwnershipDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT_DOCUMENT_ESTATE");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.DocumentEstateOwnershipDocuments).HasConstraintName("FK_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT_DOCUMENT_PERSON");

            entity.HasOne(d => d.EstateSeridaftar).WithMany(p => p.DocumentEstateOwnershipDocuments).HasConstraintName("FK_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT_ESTATE_SERIDAFTAR");
        });

        modelBuilder.Entity<DocumentEstateQuotaDetail>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_QUOTA_DETAILS", tb => tb.HasComment("جزئیات سهم بندی فروشنده و خریدار از املاك مورد معامله در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DealSummaryPersonConditions).HasComment("متن شرط مندرج در خلاصه معامله شخص");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در سند");
            entity.Property(e => e.DocumentEstateOwnershipDocumentId).HasComment("شناسه مستندات مالكیت املاك مندرج در سند");
            entity.Property(e => e.DocumentPersonBuyerId).HasComment("شناسه شخص خریدار در سند");
            entity.Property(e => e.DocumentPersonSellerId).HasComment("شناسه شخص فروشنده در سند");
            entity.Property(e => e.EstateInquiriesId).HasComment("شناسه استعلام های املاك مربوط به سند");
            entity.Property(e => e.FieldOrGrandee)
                .IsFixedLength()
                .HasComment("عرصه یا اعیان");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentEstateOwnershipDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonBuyerId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonSellerId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OwnershipDetailQuota).HasComment("جزء سهم مورد مالكیت");
            entity.Property(e => e.OwnershipTotalQuota).HasComment("كل سهم مورد مالكیت");
            entity.Property(e => e.OwnershipType)
                .IsFixedLength()
                .HasComment("نوع مالكیت");
            entity.Property(e => e.QuotaText).HasComment("متن سهم مورد معامله");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SellDetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.SellTotalQuota).HasComment("كل سهم مورد معامله");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentEstateQuotaDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_QUOTA_DETAILS_DOCUMENT_ESTATE");

            entity.HasOne(d => d.DocumentEstateOwnershipDocument).WithMany(p => p.DocumentEstateQuotaDetails).HasConstraintName("FK_DOCUMENT_ESTATE_QUOTA_DETAILS_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT");

            entity.HasOne(d => d.DocumentPersonBuyer).WithMany(p => p.DocumentEstateQuotaDetailDocumentPersonBuyers).HasConstraintName("FK_DOCUMENT_ESTATE_QUOTA_DETAILS_BUYER");

            entity.HasOne(d => d.DocumentPersonSeller).WithMany(p => p.DocumentEstateQuotaDetailDocumentPersonSellers).HasConstraintName("FK_DOCUMENT_ESTATE_QUOTA_DETAILS_SELLER");
        });

        modelBuilder.Entity<DocumentEstateQuotum>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_QUOTA", tb => tb.HasComment("سهم اصحاب سند از املاك مورد معامله در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در اسناد");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه اشخاص اسناد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.TotalQuota).HasComment("كل سهم مورد معامله");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentEstateQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_QUOTA_DOCUMENT_ESTATE");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.DocumentEstateQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_QUOTA_DOCUMENT_PERSON");
        });

        modelBuilder.Entity<DocumentEstateSeparationPiece>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_SEPARATION_PIECES", tb => tb.HasComment("قطعات تقسیم شده در اسناد تقسیم نامه ملكی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.AnbariId).HasComment("شناسه قطعه ای كه این انباری به آن منضم شده است");
            entity.Property(e => e.Area).HasComment("مساحت");
            entity.Property(e => e.BasicPlaque).HasComment("پلاك ثبتی اصلی");
            entity.Property(e => e.Block).HasComment("بلوك");
            entity.Property(e => e.Commons).HasComment("مشاعات و مشتركات");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Direction).HasComment("سمت");
            entity.Property(e => e.DividedFromBasicPlaque).HasComment("مفروز و مجزی از پلاك اصلی");
            entity.Property(e => e.DividedFromSecondaryPlaque).HasComment("مفروز و مجزی از پلاك فرعی");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در اسناد");
            entity.Property(e => e.DocumentEstateSeparationPieceKindId).HasComment("شناسه جنس قطعه در سامانه ثبت آنی");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.EasternLimits).HasComment("حد شرقی");
            entity.Property(e => e.EstatePieceTypeId).HasComment("شناسه نوع قطه در سامانه املاك");
            entity.Property(e => e.EstateSectionId).HasComment("شناسه بخش ثبتی");
            entity.Property(e => e.EstateSubsectionId).HasComment("شناسه ناحیه ثبتی");
            entity.Property(e => e.Floor).HasComment("طبقه");
            entity.Property(e => e.HasOwner)
                .IsFixedLength()
                .HasComment("آیا این قطعه مالك دارد؟");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("وضعیت ركورد از لحاظ آرشیو");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MeasurementUnitTypeId).HasComment("شناسه واحد اندازه گیری مساحت");
            entity.Property(e => e.NorthLimits).HasComment("حد شمالی");
            entity.Property(e => e.OldAnbariId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateSectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateSubsectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldParkingId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OtherAttachments).HasComment("سایر منضمات به غیر از پاركینگ و انباری");
            entity.Property(e => e.ParkingId).HasComment("شناسه قطعه ای كه این پاركینگ به آن منضم شده است");
            entity.Property(e => e.PieceNo).HasComment("شماره قطعه");
            entity.Property(e => e.PlaqueText).HasComment("پلاك متنی");
            entity.Property(e => e.Rights).HasComment("حقوق ارتفاقی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.SecondaryPlaque).HasComment("پلاك ثبتی فرعی");
            entity.Property(e => e.SouthLimits).HasComment("حد جنوبی");
            entity.Property(e => e.UnitId).HasComment("شناسه حوزه ثبتی");
            entity.Property(e => e.WesternLimits).HasComment("حد غربی");

            entity.HasOne(d => d.Anbari).WithMany(p => p.InverseAnbari).HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_ANBARI");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentEstateSeparationPieces).HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_DOCUMENT_ESTATE");

            entity.HasOne(d => d.DocumentEstateSeparationPieceKind).WithMany(p => p.DocumentEstateSeparationPieces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_DOCUMENT_ESTATE_SEPARATION_KIND");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentEstateSeparationPieces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_DOCUMENT");

            entity.HasOne(d => d.EstatePieceType).WithMany(p => p.DocumentEstateSeparationPieces).HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_ESTATE_PIECE_TYPE");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.DocumentEstateSeparationPieces).HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_ESTATE_SECTION");

            entity.HasOne(d => d.EstateSubsection).WithMany(p => p.DocumentEstateSeparationPieces).HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_ESTATE_SUBSECTION");

            entity.HasOne(d => d.Parking).WithMany(p => p.InverseParking).HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_PARKING");
        });

        modelBuilder.Entity<DocumentEstateSeparationPieceKind>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_SEPARATION_PIECE_KIND", tb => tb.HasComment("جنس قطعه تقسیم شده در اسناد تقسیم نامه ملكی"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DocumentEstateSeparationPiecesQuotum>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA", tb => tb.HasComment("سهم مالكین از قطعات تفكیك شده در تقسیم نامه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DealSummaryPersonConditions).HasComment("متن شرط مندرج در خلاصه معامله شخص");
            entity.Property(e => e.DetailQuota).HasComment("جزء سهم");
            entity.Property(e => e.DocumentEstateSeparationPiecesId).HasComment("شناسه قطعات تقسیم شده در اسناد تقسیم نامه ملكی");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه اشخاص اسناد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentEstateSeparationPiecesId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.TotalQuota).HasComment("كل سهم");

            entity.HasOne(d => d.DocumentEstateSeparationPieces).WithMany(p => p.DocumentEstateSeparationPiecesQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA_DOCUMENT_ESTATE_SEPARATION_PIECES");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.DocumentEstateSeparationPiecesQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA_DOCUMENT_PERSON");
        });

        modelBuilder.Entity<DocumentEstateType>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_TYPE", tb => tb.HasComment("انواع املاك مندرج در اسناد رسمی"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.CountUnitTitle).HasComment("واحد شمارش");
            entity.Property(e => e.DocumentEstateTypeGroupId).HasComment("شناسه گروه بندی انواع املاك مندرج در اسناد رسمی");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentEstateTypeGroup).WithMany(p => p.DocumentEstateTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_ESTATE_TYPE_DOCUMENT_ESTATE_TYPE_GROUP");
        });

        modelBuilder.Entity<DocumentEstateTypeGroup>(entity =>
        {
            entity.ToTable("DOCUMENT_ESTATE_TYPE_GROUP", tb => tb.HasComment("گروه بندی انواع املاك مندرج در اسناد رسمی"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DocumentFile>(entity =>
        {
            entity.ToTable("DOCUMENT_FILE", tb => tb.HasComment("نسخه های پشتیبان و نهایی اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.FirstFile).HasComment("محتوای فایل نسخه پشتیبان سند");
            entity.Property(e => e.FirstFileCreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد فایل نسخه پشتیبان سند");
            entity.Property(e => e.FirstFileCreateTime).HasComment("زمان ایجاد فایل نسخه پشتیبان سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LastFile).HasComment("محتوای فایل نسخه نهایی سند");
            entity.Property(e => e.LastFileCreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد فایل نسخه نهایی سند");
            entity.Property(e => e.LastFileCreateTime).HasComment("زمان ایجاد فایل نسخه نهایی سند");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ایجاد ركورد به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentFile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_FILE_DOCUMENT");
        });

        modelBuilder.Entity<DocumentInfoConfirm>(entity =>
        {
            entity.ToTable("DOCUMENT_INFO_CONFIRM", tb => tb.HasComment("تأییدات اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConfirmDate).HasComment("تاریخ تأیید نهایی سند");
            entity.Property(e => e.ConfirmTime).HasComment("زمان تأیید نهایی سند");
            entity.Property(e => e.ConfirmerNameFamily).HasComment("نام و نام خانوادگی تأیید نهایی كننده سند");
            entity.Property(e => e.CreateDate).HasComment("تاریخ ایجاد یا اصلاح");
            entity.Property(e => e.CreateTime).HasComment("زمان ایجاد یا اصلاح");
            entity.Property(e => e.CreatorNameFamily).HasComment("نام و نام خانوادگی ایجادكننده یا اصلاح كننده");
            entity.Property(e => e.DaftaryarAccessId).HasComment("شناسه دسترسی دفتریار");
            entity.Property(e => e.DaftaryarConfirmDate).HasComment("تاریخ تأیید دفتریار");
            entity.Property(e => e.DaftaryarConfirmTime).HasComment("زمان تأیید دفتریار");
            entity.Property(e => e.DaftaryarDocumentDigitalSign).HasComment("امضای الکترونیک دفتریار");
            entity.Property(e => e.DaftaryarNameFamily).HasComment("نام و نام خانوادگی دفتریار");
            entity.Property(e => e.DaftaryarSignCertificateDn).HasComment("گواهی امضای الكترونیك مورد استفاده برای امضای سند - توسط دفتریار");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LogEfpinConfirmId).HasComment("شناسه سابقه اثر انگشت سردفتر در زمان تایید نهایی سند");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SardaftarAccessId).HasComment("شناسه دسترسی سردفتر");
            entity.Property(e => e.SardaftarConfirmDate).HasComment("تاریخ تأیید سردفتر");
            entity.Property(e => e.SardaftarConfirmTime).HasComment("زمان تأیید سردفتر");
            entity.Property(e => e.SardaftarDocumentDigitalSign).HasComment("امضای الکترونیک سردفتر");
            entity.Property(e => e.SardaftarNameFamily).HasComment("نام و نام خانوادگی سردفتر");
            entity.Property(e => e.SardaftarSignCertificateDn).HasComment("گواهی امضای الكترونیك مورد استفاده برای امضای سند - توسط سردفتر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentInfoConfirm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_INFO_CONFIRM_DOCUMENT");
        });

        modelBuilder.Entity<DocumentInfoJudgement>(entity =>
        {
            entity.ToTable("DOCUMENT_INFO_JUDGEMENT", tb => tb.HasComment("اطلاعات حكم دادگاه یا سایر مراجع قانونی كه بر مبنای آن سند رسمی صادر شده است"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CaseClassifyNo).HasComment("شماره كلاسه حكم");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentJudgementTypeId).HasComment("شناسه نوع حكم");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IssueDate).HasComment("تاریخ حكم");
            entity.Property(e => e.IssueNo).HasComment("شماره حكم");
            entity.Property(e => e.IssuerName).HasComment("صادركننده");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentInfoJudgement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_INFO_JUDGEMENT_DID");

            entity.HasOne(d => d.DocumentJudgementType).WithMany(p => p.DocumentInfoJudgements).HasConstraintName("FK_DOCUMENT_INFO_JUDGEMENT_DOCUMENT_JUDGEMENT_TYPE");
        });

        modelBuilder.Entity<DocumentInfoOther>(entity =>
        {
            entity.ToTable("DOCUMENT_INFO_OTHER", tb => tb.HasComment("سایر اقلام اطلاعاتی بسته به نوع اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AdvocacyEndDate).HasComment("تاریخ پایان مدت وكالتنامه");
            entity.Property(e => e.ApplicationReason).HasComment("علت تقاضای صدور اجرائیه");
            entity.Property(e => e.DividedSectionsCount).HasComment("در اسناد تقسیم نامه، تعداد واحد تقسیم");
            entity.Property(e => e.DocumentAssetTypeId).HasComment("شناسه انواع سایر اموال منقول");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentTypeSubjectId).HasComment("شناسه انواع موضوعات اسناد وكالتنامه");
            entity.Property(e => e.EndDateTime).HasComment("تاریخ و زمان مختومه شدن اجرائیه");
            entity.Property(e => e.ExecutiveReceiverUnitId).HasComment("شناسه واحد ثبتی دریافت كننده تقاضانامه صدور اجرائیه");
            entity.Property(e => e.ExecutiveTypeId).HasComment("شناسه انواع اجرائیه");
            entity.Property(e => e.HagheEntefae)
                .IsFixedLength()
                .HasComment("نوع حق انتفاع");
            entity.Property(e => e.HasAdvocacyToOthersPermit)
                .IsFixedLength()
                .HasComment("در این وكالتنامه حق توكیل به غیر دارد؟");
            entity.Property(e => e.HasDismissalPermit)
                .IsFixedLength()
                .HasComment("در این وكالتنامه آیا حق عزل وكیل دارد؟");
            entity.Property(e => e.HasMultipleAdvocacyPermit)
                .IsFixedLength()
                .HasComment("در این وكالتنامه آیا حق انجام مورد وكالت به كرات را دارد؟");
            entity.Property(e => e.HasTime)
                .IsFixedLength()
                .HasComment("آیا سند مدت دار است؟");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsDocumentBrief)
                .IsFixedLength()
                .HasComment("آیا سند مختصر است؟");
            entity.Property(e => e.IsEstateRegistered)
                .IsFixedLength()
                .HasComment("آیا این سند مربوط به املاك ثبت شده است؟");
            entity.Property(e => e.IsKadastr)
                .IsFixedLength()
                .HasComment("آیا سند كاداستری است؟");
            entity.Property(e => e.IsPeaceForLifetime)
                .IsFixedLength()
                .HasComment("در اسناد صلح، آیا صلح عمری است؟");
            entity.Property(e => e.IsRelatedToDivisionCommission)
                .IsFixedLength()
                .HasComment("آیا این سند مربوط به كمیسیون تقسیم اسناد دولتی می شود؟");
            entity.Property(e => e.IsRentWithSarghofli)
                .IsFixedLength()
                .HasComment("در اسناد اجاره غیرمنقول آیا اجاره با حق سرقفلی است؟");
            entity.Property(e => e.IsSeparationPiecesServiceCalled)
                .IsFixedLength()
                .HasComment("آیا سرویس اخذ اطلاعات قطعات صورتمجلس تفکیکی فراخوانی شده است؟");
            entity.Property(e => e.MortageDuration).HasComment("مدت رهن");
            entity.Property(e => e.MortageTimeUnitId).HasComment("شناسه واحد زمان مدت رهن");
            entity.Property(e => e.NumberOfSeparationPieces).HasComment("تعداد قطعات صورتمجلس تفکیکی");
            entity.Property(e => e.OldDocumentAssetTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentTypeSubjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PaperCount).HasComment("تعداد برگ برای محاسبه حق التحریر");
            entity.Property(e => e.PeaceDuration).HasComment("مدت صلح");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.RegisterCount).HasComment("در سایر خدمات ثبتی، تعداد");
            entity.Property(e => e.RentDuration).HasComment("مدت اجاره");
            entity.Property(e => e.RentStartDate).HasComment("تاریخ شروع اجاره");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SeparationPiecesServiceCallDateTime).HasComment("تاریخ و زمان فراخوانی سرویس اخذ اطلاعات قطعات صورتمجلس تفکیکی");
            entity.Property(e => e.Title).HasComment("عنوان اجرائیه");
            entity.Property(e => e.VaghfType).HasComment("نوع وقف");
            entity.Property(e => e.XexecutiveId).HasComment("شناسه اجرائیه");
            entity.Property(e => e.XexecutiveOldId).HasComment("شناسه اجرائیه قبلی اصلاح شده");

            entity.HasOne(d => d.DocumentAssetType).WithMany(p => p.DocumentInfoOthers).HasConstraintName("FK_DOCUMENT_INFO_OTHER_DOCUMENT_ASSET_TYPE");

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentInfoOther)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_INFO_OTHER_DOCUMENT");

            entity.HasOne(d => d.DocumentTypeSubject).WithMany(p => p.DocumentInfoOthers).HasConstraintName("FK_DOCUMENT_INFO_OTHER_DOCUMENT_TYPE_SUBJECT");

            entity.HasOne(d => d.ExecutiveType).WithMany(p => p.DocumentInfoOthers).HasConstraintName("FK_DOCUMENT_INFO_OTHER_EXECUTIVE_TYPE");
        });

        modelBuilder.Entity<DocumentInfoText>(entity =>
        {
            entity.ToTable("DOCUMENT_INFO_TEXT", tb => tb.HasComment("متون اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ConditionsText).HasComment("متن شرط");
            entity.Property(e => e.Description).HasComment("ملاحظات و توضیحات در مورد پرونده");
            entity.Property(e => e.DocumentDescription).HasComment("ملاحظات و توضیحات در مورد مشخصات سند");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentModifyDescription).HasComment("توضیحات در مورد سند پس از تأیید نهایی");
            entity.Property(e => e.DocumentText).HasComment("متن سند");
            entity.Property(e => e.DocumentTextFontSize).HasComment("فونت متن سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegalText).HasComment("متن حقوقی سند");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PrintMode).HasComment("شیوه چاپ سند");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentInfoText)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_INFO_TEXT_DOCUMENT");
        });

        modelBuilder.Entity<DocumentInquiry>(entity =>
        {
            entity.ToTable("DOCUMENT_INQUIRY", tb => tb.HasComment("استعلام ها در فرآیند ثبت اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Conditions).HasComment("عبارات و شرایط لازم به ذكر در سند");
            entity.Property(e => e.CostPayType)
                .IsFixedLength()
                .HasComment("نوع پرداخت");
            entity.Property(e => e.DocumentEstateId).HasComment("شناسه املاك ثبت شده در اسناد");
            entity.Property(e => e.DocumentEstateOwnershipDocumentId).HasComment("شناسه مستندات مالكیت املاك مندرج در سند");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentInquiryOrganizationId).HasComment("شناسه سازمان های استعلام شونده در فرآیند ثبت اسناد");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه اشخاص سند");
            entity.Property(e => e.EstateInquiriesId).HasComment("شناسه استعلام املاك");
            entity.Property(e => e.ExpireDate)
                .IsFixedLength()
                .HasComment("تاریخ اعتبار");
            entity.Property(e => e.FactorDate)
                .IsFixedLength()
                .HasComment("تاریخ تراكنش/فیش");
            entity.Property(e => e.FactorNo).HasComment("شماره تراكنش/فیش");
            entity.Property(e => e.FactorTime).HasComment("زمان تراكنش/فیش");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("وضعیت ركورد از لحاظ آرشیو");
            entity.Property(e => e.InputParameter1).HasComment("پارامتر ورودی 1 سرویس الكترونیك");
            entity.Property(e => e.InputParameter10).HasComment("پارامتر ورودی 10 سرویس الكترونیك");
            entity.Property(e => e.InputParameter2).HasComment("پارامتر ورودی 2 سرویس الكترونیك");
            entity.Property(e => e.InputParameter3).HasComment("پارامتر ورودی 3 سرویس الكترونیك");
            entity.Property(e => e.InputParameter4).HasComment("پارامتر ورودی 4 سرویس الكترونیك");
            entity.Property(e => e.InputParameter5).HasComment("پارامتر ورودی 5 سرویس الكترونیك");
            entity.Property(e => e.InputParameter6).HasComment("پارامتر ورودی 6 سرویس الكترونیك");
            entity.Property(e => e.InputParameter7).HasComment("پارامتر ورودی 7 سرویس الكترونیك");
            entity.Property(e => e.InputParameter8).HasComment("پارامتر ورودی 8 سرویس الكترونیك");
            entity.Property(e => e.InputParameter9).HasComment("پارامتر ورودی 9 سرویس الكترونیك");
            entity.Property(e => e.InquiryIssuer).HasComment("مرجع صدور استعلام");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentEstateId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentEstateOwnershipDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Price).HasComment("مبلغ");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate ")
                .HasComment("تاریخ پرونده سند به میلادی");
            entity.Property(e => e.ReplyDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ استعلام");
            entity.Property(e => e.ReplyDetailQuota).HasComment("جزء سهم - در پاسخ استعلام");
            entity.Property(e => e.ReplyImage).HasComment("تصویر اسكن شده پاسخ استعلام");
            entity.Property(e => e.ReplyNo).HasComment("شماره پاسخ استعلام");
            entity.Property(e => e.ReplyQuotaText).HasComment("متن سهم - در پاسخ استعلام");
            entity.Property(e => e.ReplyText).HasComment("متن پاسخ استعلام");
            entity.Property(e => e.ReplyTotalQuota).HasComment("كل سهم - در پاسخ استعلام");
            entity.Property(e => e.ReplyType)
                .IsFixedLength()
                .HasComment("نوع پاسخ : مثبت/منفی");
            entity.Property(e => e.RequestDate)
                .IsFixedLength()
                .HasComment("تاریخ درخواست استعلام");
            entity.Property(e => e.RequestNo).HasComment("شماره درخواست استعلام");
            entity.Property(e => e.RequestText).HasComment("متن درخواست استعلام");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.State).HasComment("وضعیت");
            entity.Property(e => e.TaxTrackingNo).HasComment("شماره رهگیری استعلام الكترونیك دارایی");
            entity.Property(e => e.TerminalNo).HasComment("شماره ترمینال");

            entity.HasOne(d => d.DocumentEstate).WithMany(p => p.DocumentInquiries).HasConstraintName("FK_DOCUMENT_INQUIRY_DOCUMENT_ESTATE");

            entity.HasOne(d => d.DocumentEstateOwnershipDocument).WithMany(p => p.DocumentInquiries).HasConstraintName("FK_DOCUMENT_INQUIRY_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentInquiries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_INQUIRY_DOCUMENT");

            entity.HasOne(d => d.DocumentInquiryOrganization).WithMany(p => p.DocumentInquiries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_INQUIRY_DOCUMENT_INQUIRY_ORGANIZATION");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.DocumentInquiries).HasConstraintName("FK_DOCUMENT_INQUIRY_DOCUMENT_PERSON");
        });

        modelBuilder.Entity<DocumentInquiryOrganization>(entity =>
        {
            entity.ToTable("DOCUMENT_INQUIRY_ORGANIZATION", tb => tb.HasComment("سازمان های استعلام شونده در فرآیند ثبت اسناد"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.IsSabt)
                .IsFixedLength()
                .HasComment("آیا این سازمان از واحدهای ثبتی است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قبلی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DocumentJudgementType>(entity =>
        {
            entity.ToTable("DOCUMENT_JUDGEMENT_TYPE", tb => tb.HasComment("انواع حكم"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DocumentLimit>(entity =>
        {
            entity.ToTable("DOCUMENT_LIMITS", tb => tb.HasComment("سابقه محدودیت ها در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ChangeDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.ChangeTime).HasComment("زمان ثبت");
            entity.Property(e => e.Description).HasComment("توضیحات در مورد محدودیت اِعمال شده");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.FromDate)
                .IsFixedLength()
                .HasComment("تاریخ شروع محدودیت");
            entity.Property(e => e.LetterDate).HasComment("تاریخ نامه تاییدیه امور اسناد");
            entity.Property(e => e.LetterImage).HasComment("تصویر نامه تاییدیه امور اسناد");
            entity.Property(e => e.LetterNo).HasComment("شماره نامه تاییدیه امور اسناد");
            entity.Property(e => e.Modifier).HasComment("نام و نام خانوادگی ثبت كننده");
            entity.Property(e => e.ToDate)
                .IsFixedLength()
                .HasComment("تاریخ پایان محدودیت");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentLimits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_LIMITS_DOCUMENT");
        });

        modelBuilder.Entity<DocumentMessage>(entity =>
        {
            entity.ToTable("DOCUMENT_MESSAGES", tb => tb.HasComment("پیام های ارسالی در مورد اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("وضعیت ركورد از لحاظ آرشیو");
            entity.Property(e => e.IsMechanized)
                .IsFixedLength()
                .HasComment("آیا پیام به صورت مكانیزه تولید شده است؟");
            entity.Property(e => e.IsSeen)
                .IsFixedLength()
                .HasComment("آیا پیام توسط گیرنده رؤیت شده است؟");
            entity.Property(e => e.IsSent)
                .IsFixedLength()
                .HasComment("آیا پیام ارسال شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.Receiver).HasComment("گیرنده");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ركورد به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.SeenDate)
                .IsFixedLength()
                .HasComment("تاریخ رؤیت");
            entity.Property(e => e.SeenTime).HasComment("زمان رؤیت");
            entity.Property(e => e.Text).HasComment("متن پیام");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentMessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_MESSAGES_DOCUMENT");
        });

        modelBuilder.Entity<DocumentPayment>(entity =>
        {
            entity.ToTable("DOCUMENT_PAYMENT", tb => tb.HasComment("اطلاعات پرداخت هزینه اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.BankCounterInfo).HasComment("كد و نام شعبه بانك محل پرداخت");
            entity.Property(e => e.CardNo).HasComment("شماره كارت");
            entity.Property(e => e.CostTypeId).HasComment("شناسه نوع هزینه");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.HowToPay)
                .IsFixedLength()
                .HasComment("شیوه پرداخت");
            entity.Property(e => e.HowToQuotation).HasComment("شیوه تسهیم");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsReused)
                .IsFixedLength()
                .HasComment("آیا از پرداخت های اسناد بی اثر شده قبلی استفاده مجدد شده است؟");
            entity.Property(e => e.LegacyId).HasComment("شناسه ركورد در سامانه قدیمی");
            entity.Property(e => e.No).HasComment("شناسه یكتا قبض پرداخت");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldReusedDocumentPaymentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PaymentDate).HasComment("تاریخ پرداخت");
            entity.Property(e => e.PaymentNo).HasComment("شماره مرجع تراكنش پرداخت");
            entity.Property(e => e.PaymentTime).HasComment("زمان پرداخت");
            entity.Property(e => e.PaymentType).HasComment("ابزار پرداخت الكترونیك - بانك؛ابزار");
            entity.Property(e => e.Price).HasComment("مبلغ");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.ReusedDocumentPaymentId).HasComment("شناسه پرداخت استفاده شده مجدد قبلی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");

            entity.HasOne(d => d.CostType).WithMany(p => p.DocumentPayments).HasConstraintName("FK_DOCUMENT_PAYMENT_COST_TYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PAYMENT_DOCUMENT");

            entity.HasOne(d => d.ReusedDocumentPayment).WithMany(p => p.InverseReusedDocumentPayment).HasConstraintName("FK_DOCUMENT_PAYMENT_DOCUMENT_PAYMENT");
        });

        modelBuilder.Entity<DocumentPerson>(entity =>
        {
            entity.ToTable("DOCUMENT_PERSON", tb => tb.HasComment("اشخاص اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.AddressType)
                .IsFixedLength()
                .HasComment("نوع نشانی");
            entity.Property(e => e.AmlakEskanState)
                .IsFixedLength()
                .HasComment("وضعیت ثبت نام در سامانه املاك و اسكان");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد");
            entity.Property(e => e.BirthYear)
                .IsFixedLength()
                .HasComment("سال تولد");
            entity.Property(e => e.CompanyRegisterDate).HasComment("تاریخ ثبت");
            entity.Property(e => e.CompanyRegisterLocation).HasComment("محل ثبت");
            entity.Property(e => e.CompanyRegisterNo).HasComment("شماره ثبت");
            entity.Property(e => e.CompanyTypeId).HasComment("نوع شركت");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentDraftConfirmDate)
                .IsFixedLength()
                .HasComment("تاريخ تأييد پيش نويس سند توسط شخص");
            entity.Property(e => e.DocumentDraftConfirmTime).HasComment("زمان تأييد پيش نويس سند توسط شخص");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentPersonTypeId).HasComment("شناسه نوع سمت اشخاص سند");
            entity.Property(e => e.Email).HasComment("آدرس پست الكترونیكی");
            entity.Property(e => e.EstateInquiryId).HasComment("شناسه استعلام های ملك مرتبط");
            entity.Property(e => e.EstemlakDate)
                .IsFixedLength()
                .HasComment("تاریخ مجوز استملاك");
            entity.Property(e => e.EstemlakNo).HasComment("شماره مجوز استملاك");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.FaxNo).HasComment("شماره فكی");
            entity.Property(e => e.GrowthDescription).HasComment("توضیحات مربوط به حكم رشد");
            entity.Property(e => e.GrowthIssuerId).HasComment("شناسه سازمان عمومی صادركننده");
            entity.Property(e => e.GrowthJudgmentDate)
                .IsFixedLength()
                .HasComment("تاریخ حكم رشد");
            entity.Property(e => e.GrowthJudgmentNo).HasComment("شماره حكم رشد");
            entity.Property(e => e.GrowthLetterDate)
                .IsFixedLength()
                .HasComment("تاریخ نامه مربوط به حكم رشد");
            entity.Property(e => e.GrowthLetterNo).HasComment("شماره نامه مربوط به حكم رشد");
            entity.Property(e => e.GrowthSenderId).HasComment("شناسه سازمان عمومی ارسال كننده");
            entity.Property(e => e.HasGrowthJudgment)
                .IsFixedLength()
                .HasComment("آیا حكم رشد دارد؟");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("آیا كارت هوشمند ملی دارد؟");
            entity.Property(e => e.IdentityIssueGeoLocationId).HasComment("شناسه محل جغرافیایی صدور شناسنامه");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آیا این شخص زنده است؟");
            entity.Property(e => e.IsDocumentDraftConfirmed)
                .IsFixedLength()
                .HasComment("آيا اين شخص پيش نويس سند را تأييد کرده است؟");
            entity.Property(e => e.IsFingerprintGotten)
                .IsFixedLength()
                .HasComment("آیا تصویر اثر انگشت شخص گرفته شده است؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsMartyrApplicant)
                .IsFixedLength()
                .HasComment("آیا این شخص، متقاضی استفاده از تخفیف حق الثبت مخصوص ایثارگران هست؟");
            entity.Property(e => e.IsMartyrIncluded)
                .IsFixedLength()
                .HasComment("بر اساس استعلام انجام شده از بنیاد شهید، آیا این شخص مشمول تخفیف حق الثبت مخصوص ایثارگران هست؟");
            entity.Property(e => e.IsOriginal)
                .IsFixedLength()
                .HasComment("آیا این شخص اصیل است؟");
            entity.Property(e => e.IsPrisoner)
                .IsFixedLength()
                .HasComment("آیا ثبت سند برای این شخص در زندان صورت گرفته است؟");
            entity.Property(e => e.IsRelated)
                .IsFixedLength()
                .HasComment("آیا این شخص وكیل یا نماینده اشخاص دیگری است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟");
            entity.Property(e => e.IsSignedDocument)
                .IsFixedLength()
                .HasComment("آیا این شخص سند را امضاء كرده است؟");
            entity.Property(e => e.IsTfaRequired)
                .IsFixedLength()
                .HasComment("آیا انجام مرحله اعتبارسنجی عامل دوم برای این شخص لازم است؟");
            entity.Property(e => e.LastLegalPaperDate).HasComment("تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه ماهیت شخص حقوقی");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه نوع شخص حقوقی");
            entity.Property(e => e.MartyrCode).HasComment("كد رهگیری استعلام انجام شده از بنیاد شهید");
            entity.Property(e => e.MartyrDescription).HasComment("توضیحات استعلام انجام شده از بنیاد شهید");
            entity.Property(e => e.MartyrInquiryDate).HasComment("تاریخ استعلام انجام شده از بنیاد شهید");
            entity.Property(e => e.MartyrInquiryTime).HasComment("زمان استعلام انجام شده از بنیاد شهید");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعیت مالكیت شماره خط تلفن همراه");
            entity.Property(e => e.MocInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام ام او سی");
            entity.Property(e => e.MocInquiryTime).HasComment("زمان استعلام ام او سی");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت تطابق اثر انگشت با اثر انگشت مندرج در كارت هوشمند ملی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شناسه ملی");
            entity.Property(e => e.Nationality).HasComment("ملیت");
            entity.Property(e => e.NationalityId).HasComment("شناسه كشور تابعیت");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldEstateInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldIdentityIssueGeoLocationId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldNationalityId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PassportNo).HasComment("شماره گذرنامه");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("حقیقی است یا حقوقی؟");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.SabtahvalInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام ثبت احوال");
            entity.Property(e => e.SabtahvalInquiryTime).HasComment("زمان استعلام ثبت احوال");
            entity.Property(e => e.SanaHasOrganizationChart)
                .IsFixedLength()
                .HasComment("آیا شخص حقوقی، در سامانه ثنا ساختار تشكیلات دارد؟");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaNotificationCode).HasComment("کد ارسال سند به کارتابل ثنا شخص");
            entity.Property(e => e.SanaNotificationDateTime).HasComment("تاریخ و زمان ارسال سند به کارتابل ثنا شخص");
            entity.Property(e => e.SanaOrganizationCode).HasComment("كد شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("كد شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعیت حساب كاربری ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه - بخش عددی");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حرفی");
            entity.Property(e => e.Serial).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.SignDate)
                .IsFixedLength()
                .HasComment("تاریخ امضاء");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("نتیجه انجام احراز هویت دو مرحله ای");
            entity.Property(e => e.WouldSignedDocument)
                .IsFixedLength()
                .HasComment("آیا این شخص سند را امضاء خواهد كرد؟");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PERSON_DOCUMENT");

            entity.HasOne(d => d.DocumentPersonType).WithMany(p => p.DocumentPeople).HasConstraintName("FK_DOCUMENT_PERSON_DOCUMENT_PERSON_TYPE");

            entity.HasOne(d => d.GrowthIssuer).WithMany(p => p.DocumentPersonGrowthIssuers).HasConstraintName("FK_DOCUMENT_PERSON_GROWTH_ISSUER");

            entity.HasOne(d => d.GrowthSender).WithMany(p => p.DocumentPersonGrowthSenders).HasConstraintName("FK_DOCUMENT_PERSON_GROWTH_SENDER");
        });

        modelBuilder.Entity<DocumentPersonRelated>(entity =>
        {
            entity.ToTable("DOCUMENT_PERSON_RELATED", tb => tb.HasComment("وابستگی اشخاص اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AgentDocumentCountryId).HasComment("شناسه كشوری كه وكالتنامه در آن تنظیم شده است");
            entity.Property(e => e.AgentDocumentDate)
                .IsFixedLength()
                .HasComment("تاریخ وكالتنامه");
            entity.Property(e => e.AgentDocumentId).HasComment("شناسه وكالتنامه");
            entity.Property(e => e.AgentDocumentIssuer).HasComment("مرجع صدور وكالتنامه");
            entity.Property(e => e.AgentDocumentNo).HasComment("شماره وكالتنامه");
            entity.Property(e => e.AgentDocumentScriptoriumId).HasComment("شناسه دفترخانه تنظیم كننده وكالتنامه");
            entity.Property(e => e.AgentDocumentSecretCode).HasComment("رمز تصدیق وكالتنامه");
            entity.Property(e => e.AgentPersonId).HasComment("شناسه شخص وابسته");
            entity.Property(e => e.AgentTypeId).HasComment("شناسه نوع وابستگی");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentScriptoriumId).HasComment("شناسه دفترخانه گواهی امضاء");
            entity.Property(e => e.DocumentSmsId).HasComment("شناسه ركورد پیامك مربوط به وكالتنامه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAgentDocumentAbroad)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در مراجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.IsLawyer)
                .IsFixedLength()
                .HasComment("آیا وكیل دادگستری است؟");
            entity.Property(e => e.IsRelatedDocumentInSsar)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در سامانه ثبت الكترونیك اسناد ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MainPersonId).HasComment("شناسه شخص اصلی");
            entity.Property(e => e.OldAgentDocumentCountryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldAgentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldMainPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ReliablePersonReasonId).HasComment("شناسه دلیل نیاز به معتمد");
            entity.Property(e => e.RowNo).HasComment("ردیف");

            entity.HasOne(d => d.AgentDocument).WithMany(p => p.DocumentPersonRelatedAgentDocuments).HasConstraintName("FK_DOCUMENT_PERSON_RELATED_AGENT_DOCUMENT");

            entity.HasOne(d => d.AgentPerson).WithMany(p => p.DocumentPersonRelatedAgentPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PERSON_RELATED_AGENT_PERSON");

            entity.HasOne(d => d.AgentType).WithMany(p => p.DocumentPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PERSON_RELATED_AGENT_TYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentPersonRelatedDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PERSON_RELATED_DOCUMENT");

            entity.HasOne(d => d.DocumentSms).WithMany(p => p.DocumentPersonRelateds).HasConstraintName("FK_DOCUMENT_PERSON_RELATED_DOCUMENT_SMS");

            entity.HasOne(d => d.MainPerson).WithMany(p => p.DocumentPersonRelatedMainPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PERSON_RELATED_CORE_PERSON");

            entity.HasOne(d => d.ReliablePersonReason).WithMany(p => p.DocumentPersonRelateds).HasConstraintName("FK_DOCUMENT_PERSON_RELATED_RELIABLE_PERSON_REASON");
        });

        modelBuilder.Entity<DocumentPersonType>(entity =>
        {
            entity.ToTable("DOCUMENT_PERSON_TYPE", tb => tb.HasComment("نوع سمت اشخاص سند"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.DocumentTypeId).HasComment("شناسه نوع سند");
            entity.Property(e => e.IsAmlakEskanRequired)
                .IsFixedLength()
                .HasComment("آیا ثبت نام در سامانه املاک و اسکان برای این نوع اشخاص چک شود؟");
            entity.Property(e => e.IsOwner)
                .IsFixedLength()
                .HasComment("آیا این سمت با سمت معامل تناظر دارد؟");
            entity.Property(e => e.IsProhibitionCheckRequired)
                .IsFixedLength()
                .HasComment("آیا ممنوع‌المعامله بودن برای این نوع اشخاص چك شود؟");
            entity.Property(e => e.IsRequired)
                .IsFixedLength()
                .HasComment("آیا ثبت حداقل یك شخص از این نوع اجباری است؟");
            entity.Property(e => e.IsSanaRequired)
                .IsFixedLength()
                .HasComment("آیا برای اشخاص با این نوع سمت، داشتن ثنا اجباری است؟");
            entity.Property(e => e.IsShahkarRequired)
                .IsFixedLength()
                .HasComment("آیا برای اشخاص با این نوع سمت، داشتن شماره موبایل معتبر با مالكیت خود شخص، اجباری است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.PluralTitle).HasComment("عنوان جمع سمت در فرم سند");
            entity.Property(e => e.PrintPluralTitle).HasComment("عنوان جمع سمت در چاپ سند");
            entity.Property(e => e.PrintSingularTitle).HasComment("عنوان مفرد سمت در چاپ سند");
            entity.Property(e => e.RowNoInForm).HasComment("ترتیب نمایش در فرم سند");
            entity.Property(e => e.RowNoInPrint).HasComment("ترتیب نمایش در چاپ سند");
            entity.Property(e => e.SingularTitle).HasComment("عنوان مفرد سمت در فرم سند");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentPersonTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_PERSON_TYPE_DOCUMENT_TYPE");
        });

        modelBuilder.Entity<DocumentRelation>(entity =>
        {
            entity.ToTable("DOCUMENT_RELATION", tb => tb.HasComment("اسناد وابسته مربوط به اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند اصلی");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsRelatedDocAbroad)
                .IsFixedLength()
                .HasComment("آیا سند وابسته در مراجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldRelatedDocAbroadCountryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldRelatedScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده به میلادی");
            entity.Property(e => e.RelatedDocAbroadCountryId).HasComment("شناسه كشوری كه سند وابسته در آن تنظیم شده است؟");
            entity.Property(e => e.RelatedDocumentDate).HasComment("تاریخ سند وابسته");
            entity.Property(e => e.RelatedDocumentId).HasComment("شناسه سند وابسته");
            entity.Property(e => e.RelatedDocumentIsInSsar)
                .IsFixedLength()
                .HasComment("آیا سند وابسته در سیستم ثبت الكترونیك اسناد ثبت شده است؟");
            entity.Property(e => e.RelatedDocumentNo).HasComment("شماره سند وابسته");
            entity.Property(e => e.RelatedDocumentScriptorium).HasComment("شماره و محل دفترخانه صادركننده سند وابسته");
            entity.Property(e => e.RelatedDocumentSecretCode).HasComment("رمز تصدیق سند وابسته");
            entity.Property(e => e.RelatedDocumentSmsId).HasComment("شناسه ركورد پیامك معادل سند وابسته");
            entity.Property(e => e.RelatedDocumentTypeId).HasComment("شناسه نوع سند وابسته");
            entity.Property(e => e.RelatedRegCaseId).HasComment("شناسه مورد ثبتی وابسته");
            entity.Property(e => e.RelatedScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند وابسته");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند اصلی");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentRelationDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_RELATION_DOCUMENT");

            entity.HasOne(d => d.RelatedDocument).WithMany(p => p.DocumentRelationRelatedDocuments).HasConstraintName("FK_DOCUMENT_RELATION_RELATED_DOCUMENT");

            entity.HasOne(d => d.RelatedDocumentType).WithMany(p => p.DocumentRelations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_RELATION_DOCUMENT_TYPE");
        });

        modelBuilder.Entity<DocumentSemaphore>(entity =>
        {
            entity.ToTable("DOCUMENT_SEMAPHORE", tb => tb.HasComment("سمافور برای تایید نهایی سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentElectronicBookData).HasComment("اقلام اطلاعاتی ركوردهایی كه در دفتر الكترونیك سندء ساخته خواهند شد");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند مربوطه");
            entity.Property(e => e.LastChangeDate).HasComment("تاریخ آخرین تغییر");
            entity.Property(e => e.LastChangeTime).HasComment("زمان آخرین تغییر");
            entity.Property(e => e.LastClassifyNo).HasComment("آخرین شماره ترتیب سندء در دفترخانه مربوطه قبل از شروع عملیات");
            entity.Property(e => e.NewDocumentData).HasComment("اقلام اطلاعاتی سند مربوطه بعد از شروع عملیات");
            entity.Property(e => e.OperationType)
                .IsFixedLength()
                .HasComment("نوع عملیات");
            entity.Property(e => e.OriginalDocumentData).HasComment("اقلام اطلاعاتی سند مربوطه قبل از شروع عملیات");
            entity.Property(e => e.RecordDate).HasComment("تاریخ ایجاد ركورد");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه مربوطه");
            entity.Property(e => e.State).HasComment("وضعیت سمافور");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentSemaphores).HasConstraintName("FK_DOCUMENT_SEMAPHORE_DOCUMENT");
        });

        modelBuilder.Entity<DocumentSm>(entity =>
        {
            entity.ToTable("DOCUMENT_SMS", tb => tb.HasComment("پیامك های ارسالی در مورد اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CreateDate).HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.DocumentId).HasComment("شناسه پرونده خدمات ثبتی");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("وضعیت ركورد از لحاظ آرشیو");
            entity.Property(e => e.IsMechanized)
                .IsFixedLength()
                .HasComment("آیا پیامك به صورت مكانیزه تولید شده است؟");
            entity.Property(e => e.IsSent)
                .IsFixedLength()
                .HasComment("آیا پیامك ارسال شده است؟");
            entity.Property(e => e.LegacyId).HasComment("شناسه ركورد در سامانه قدیمی");
            entity.Property(e => e.MobileNo).HasComment("شماره همراه جهت ارسال پیام كوتاه");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ReceiverName).HasComment("نام گیرنده");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ركورد به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SmsText).HasComment("متن پیامك");
            entity.Property(e => e.Trmsmsid).HasComment("شناسه ركورد پیامك در جدول پیامك فریمورك قدیم");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentSms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOCUMENT_SMS_DOCUMENT");
        });

        modelBuilder.Entity<DocumentSpecialChange>(entity =>
        {
            entity.ToTable("DOCUMENT_SPECIAL_CHANGES", tb => tb.HasComment("سابقه تغییرات خاص در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ChangeDate)
                .IsFixedLength()
                .HasComment("تاریخ انجام اصلاح");
            entity.Property(e => e.ChangeTime).HasComment("زمان انجام اصلاح");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.LetterDate).HasComment("تاریخ نامه تاییدیه امور اسناد");
            entity.Property(e => e.LetterImage).HasComment("تصویر نامه تاییدیه امور اسناد");
            entity.Property(e => e.LetterNo).HasComment("شماره نامه تاییدیه امور اسناد");
            entity.Property(e => e.Modifier).HasComment("نام و نام خانوادگی تغییردهنده");
            entity.Property(e => e.NewState)
                .IsFixedLength()
                .HasComment("مقدار فیلد وضعیت جدید");
            entity.Property(e => e.OldState)
                .IsFixedLength()
                .HasComment("مقدار فیلد وضعیت قبلی");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentSpecialChanges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_SPECIAL_CHANGES_DOCUMENT");
        });

        modelBuilder.Entity<DocumentTemplate>(entity =>
        {
            entity.ToTable("DOCUMENT_TEMPLATE", tb => tb.HasComment("كلیشه اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.CreateDate).HasComment("تاریخ ایجاد");
            entity.Property(e => e.CreateTime).HasComment("زمان ایجاد");
            entity.Property(e => e.DocumentTypeId).HasComment("شناسه نوع سند");
            entity.Property(e => e.Modifier).HasComment("مشخصات آخرین ویرایش كننده");
            entity.Property(e => e.ModifyDateTime).HasComment("تاریخ و زمان آخرین ویرایش");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ایجاد كلیشه به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Text).HasComment("متن");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentTemplates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_TEMPLATE_DOCUMENT_TYPE");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.ToTable("DOCUMENT_TYPE", tb => tb.HasComment("انواع اسناد و خدمات ثبتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.AssetIsRequired)
                .IsFixedLength()
                .HasComment("آیا برای این نوع سند تعیین اموال مورد ثبت اجباری است؟");
            entity.Property(e => e.AssetTypeIsRequired)
                .IsFixedLength()
                .HasComment("آیا نوع اموال منقول اجباری است؟");
            entity.Property(e => e.CaseTitle).HasComment("عنوان كلی مورد ثبتی");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.DocumentTextWritingAllowed)
                .IsFixedLength()
                .HasComment("آیا برای این نوع سند نوشتن كل متن سند توسط دفترخانه مجاز است؟");
            entity.Property(e => e.DocumentTypeGroup1Id).HasComment("شناسه سطح اول گروه‌بندی اسناد و خدمات ثبتی");
            entity.Property(e => e.DocumentTypeGroup2Id).HasComment("شناسه سطح دوم  گروه‌بندی اسناد و خدمات ثبتی");
            entity.Property(e => e.EstateInquiryIsRequired)
                .IsFixedLength()
                .HasComment("آیا استعلام ملك اجباری است؟");
            entity.Property(e => e.GeneralPersonPostTitle).HasComment("عنوان كلی اشخاص سند");
            entity.Property(e => e.HasAsset)
                .IsFixedLength()
                .HasComment("آیا برای این نوع سند تعیین اموال مورد ثبت تعریف شده است؟");
            entity.Property(e => e.HasAssetType)
                .IsFixedLength()
                .HasComment("آیا نوع اموال منقول دارد؟");
            entity.Property(e => e.HasCount)
                .IsFixedLength()
                .HasComment("آیا تعداد مطرح است؟");
            entity.Property(e => e.HasEstateAttachments)
                .IsFixedLength()
                .HasComment("آیا گزینه انتقال منضم دارد؟");
            entity.Property(e => e.HasEstateInquiry)
                .IsFixedLength()
                .HasComment("آیا استعلام ملك دارد؟");
            entity.Property(e => e.HasNonregisteredEstate)
                .IsFixedLength()
                .HasComment("آیا شامل املاك جاری هم می شود؟");
            entity.Property(e => e.HasRelatedDocument)
                .IsFixedLength()
                .HasComment("آیا برای این نوع سند، سند وابسته باید تعیین شود؟");
            entity.Property(e => e.HasSubject)
                .IsFixedLength()
                .HasComment("آیا موضوع دارد؟");
            entity.Property(e => e.IsSupportive)
                .IsFixedLength()
                .HasComment("آیا مخصوص خدمات ثبتی است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.RowNo).HasComment("ترتیب");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.SubjectIsRequired)
                .IsFixedLength()
                .HasComment("آیا موضوع اجباری است؟");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.WealthType)
                .IsFixedLength()
                .HasComment("نوع اموال مندرج در سند");

            entity.HasOne(d => d.DocumentTypeGroup1).WithMany(p => p.DocumentTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_TYPE_DOC_TYPE_GRP1");

            entity.HasOne(d => d.DocumentTypeGroup2).WithMany(p => p.DocumentTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_TYPE_DOC_TYPE_GRP2");
        });

        modelBuilder.Entity<DocumentTypeGroup1>(entity =>
        {
            entity.ToTable("DOCUMENT_TYPE_GROUP1", tb => tb.HasComment("سطح اول گروه‌بندی اسناد و خدمات ثبتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.IsSupportive)
                .IsFixedLength()
                .HasComment("آیا مخصوص خدمات ثبتی است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<DocumentTypeGroup2>(entity =>
        {
            entity.ToTable("DOCUMENT_TYPE_GROUP2", tb => tb.HasComment("سطح دوم گروه‌بندی اسناد و خدمات ثبتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.DocumentTypeGroup1Id).HasComment("شناسه سطح اول گروه‌بندی اسناد و خدمات ثبتی");
            entity.Property(e => e.IsSupportive)
                .IsFixedLength()
                .HasComment("آیا مخصوص خدمات ثبتی است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentTypeGroup1).WithMany(p => p.DocumentTypeGroup2s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_TYPE_GROUP2_DOC_TYPE_GRP1");
        });

        modelBuilder.Entity<DocumentTypeSubject>(entity =>
        {
            entity.ToTable("DOCUMENT_TYPE_SUBJECT", tb => tb.HasComment("انواع موضوعات اسناد"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.DocumentTypeId).HasComment("شناسه نوع سند");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentTypeSubjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_TYPE_SUBJECT_DOCUMENT_TYPE");
        });

        modelBuilder.Entity<DocumentVehicle>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE", tb => tb.HasComment("وسایل نقلیه ثبت شده در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CardNo).HasComment("شماره كارت وسیله نقلیه");
            entity.Property(e => e.ChassisNo).HasComment("شماره شاسی");
            entity.Property(e => e.Color).HasComment("رنگ");
            entity.Property(e => e.CylinderCount).HasComment("تعداد سیلندر");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.DocumentVehicleSystemId).HasComment("شناسه سیستم وسیله نقلیه");
            entity.Property(e => e.DocumentVehicleTipId).HasComment("شناسه تیپ وسیله نقلیه");
            entity.Property(e => e.DocumentVehicleTypeId).HasComment("شناسه نوع وسیله نقلیه");
            entity.Property(e => e.DutyFicheNo).HasComment("شماره فیش پرداخت عوارض");
            entity.Property(e => e.EngineCapacity).HasComment("حجم موتور");
            entity.Property(e => e.EngineNo).HasComment("شماره موتور");
            entity.Property(e => e.FuelCardNo).HasComment("شماره كارت سوخت");
            entity.Property(e => e.HasBuyerExclusivePlaque)
                .IsFixedLength()
                .HasComment("آیا خریدار پلاک انتظامی خاص دارد؟");
            entity.Property(e => e.HasBuyerPlaque)
                .IsFixedLength()
                .HasComment("آیا خریدار پلاک انتظامی دارد؟");
            entity.Property(e => e.HasSellerExclusivePlaque)
                .IsFixedLength()
                .HasComment("آیا فروشنده پلاک انتظامی خاص دارد؟");
            entity.Property(e => e.HasSellerPlaque)
                .IsFixedLength()
                .HasComment("آیا فروشنده پلاک انتظامی دارد؟");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.InssuranceCo).HasComment("شركت بیمه كننده");
            entity.Property(e => e.InssuranceNo).HasComment("شماره بیمه شخص ثالث");
            entity.Property(e => e.IsInTaxList)
                .IsFixedLength()
                .HasComment("آیا این وسیله نقلیه در جدول سازمان امور مالیاتی وجود دارد؟");
            entity.Property(e => e.IsVehicleNumbered)
                .IsFixedLength()
                .HasComment("آیا وسیله نقلیه شماره گذاری شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MadeInIran)
                .IsFixedLength()
                .HasComment("آیا خودرو داخلی است؟");
            entity.Property(e => e.MadeInYear)
                .IsFixedLength()
                .HasComment("سال تولید");
            entity.Property(e => e.Model).HasComment("مدل");
            entity.Property(e => e.NumberingLocation).HasComment("محل شماره گذاری");
            entity.Property(e => e.OldDocumentDate).HasComment("مرجع صادركننده سند قبلی");
            entity.Property(e => e.OldDocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentIssuer).HasComment("تاریخ سند قبلی");
            entity.Property(e => e.OldDocumentNo).HasComment("شماره سند قبلی");
            entity.Property(e => e.OldDocumentVehicleSystemId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentVehicleTipId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentVehicleTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OtherInfo).HasComment("سایر مشخصات وسیله نقلیه");
            entity.Property(e => e.OwnershipDetailQuota).HasComment("جزء سهم مورد مالكیت");
            entity.Property(e => e.OwnershipPrintedDocumentNo).HasComment("شماره چاپی شناسنامه مالكیت");
            entity.Property(e => e.OwnershipTotalQuota).HasComment("كل سهم مورد مالكیت");
            entity.Property(e => e.OwnershipType)
                .IsFixedLength()
                .HasComment("نوع مالكیت");
            entity.Property(e => e.PlaqueBuyer).HasComment("شماره انتظامی خریدار");
            entity.Property(e => e.PlaqueNo1Buyer).HasComment("بخش اول عددی شماره انتظامی خریدار");
            entity.Property(e => e.PlaqueNo1Seller).HasComment("بخش اول عددی شماره انتظامی فروشنده");
            entity.Property(e => e.PlaqueNo2Buyer).HasComment("بخش دوم عددی شماره انتظامی خریدار");
            entity.Property(e => e.PlaqueNo2Seller).HasComment("بخش دوم عددی شماره انتظامی فروشنده");
            entity.Property(e => e.PlaqueNoAlphaBuyer).HasComment("بخش حرفی شماره انتظامی خریدار");
            entity.Property(e => e.PlaqueNoAlphaSeller).HasComment("بخش حرفی شماره انتظامی فروشنده");
            entity.Property(e => e.PlaqueSeller).HasComment("شماره انتظامی فروشنده");
            entity.Property(e => e.PlaqueSeriBuyer).HasComment("سری شماره انتظامی خریدار");
            entity.Property(e => e.PlaqueSeriSeller).HasComment("سری شماره انتظامی فروشنده");
            entity.Property(e => e.Price).HasComment("مبلغ سند");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SellDetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.SellTotalQuota).HasComment("كل سهم مورد معامله");
            entity.Property(e => e.System).HasComment("سیستم");
            entity.Property(e => e.Tip).HasComment("تیپ");
            entity.Property(e => e.Type).HasComment("نوع");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentVehicles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_DOCUMENT");

            entity.HasOne(d => d.DocumentVehicleSystem).WithMany(p => p.DocumentVehicles).HasConstraintName("FK_DOCUMENT_VEHICLE_VEHICLE_SYSTEM");

            entity.HasOne(d => d.DocumentVehicleTip).WithMany(p => p.DocumentVehicles).HasConstraintName("FK_DOCUMENT_VEHICLE_VEHICLE_TIP");

            entity.HasOne(d => d.DocumentVehicleType).WithMany(p => p.DocumentVehicles).HasConstraintName("FK_DOCUMENT_VEHICLE_VEHICLE_TYPE");
        });

        modelBuilder.Entity<DocumentVehicleQuotaDetail>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE_QUOTA_DETAILS", tb => tb.HasComment("جزئیات سهم بندی فروشنده و خریدار از وسایل نقلیه مورد معامله در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentPersonBuyerId).HasComment("شناسه شخص خریدار در سند");
            entity.Property(e => e.DocumentPersonSellerId).HasComment("شناسه شخص فروشنده در سند");
            entity.Property(e => e.DocumentVehicleId).HasComment("شناسه وسایل نقلیه ثبت شده در سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentPersonBuyerId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentPersonSellerId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentVehicleId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OwnershipDetailQuota).HasComment("جزء سهم مورد مالكیت");
            entity.Property(e => e.OwnershipTotalQuota).HasComment("كل سهم مورد مالكیت");
            entity.Property(e => e.QuotaText).HasComment("متن سهم مورد معامله");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SellDetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.SellTotalQuota).HasComment("كل سهم مورد معامله");

            entity.HasOne(d => d.DocumentPersonBuyer).WithMany(p => p.DocumentVehicleQuotaDetailDocumentPersonBuyers).HasConstraintName("FK_DOCUMENT_VEHICLE_QUOTA_DETAILS_BUYER");

            entity.HasOne(d => d.DocumentPersonSeller).WithMany(p => p.DocumentVehicleQuotaDetailDocumentPersonSellers).HasConstraintName("FK_DOCUMENT_VEHICLE_QUOTA_DETAILS_SELLER");

            entity.HasOne(d => d.DocumentVehicle).WithMany(p => p.DocumentVehicleQuotaDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_QUOTA_DETAILS_DOCUMENT_VEHICLE");
        });

        modelBuilder.Entity<DocumentVehicleQuotum>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE_QUOTA", tb => tb.HasComment("سهم اصحاب سند از وسایل نقلیه مورد معامله در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه اشخاص اسناد");
            entity.Property(e => e.DocumentVehicleId).HasComment("شناسه وسایل نقلیه ثبت شده در اسناد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldDocumentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldDocumentVehicleId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.TotalQuota).HasComment("كل سهم مورد معامله");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.DocumentVehicleQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_QUOTA_DOCUMENT_PERSON");

            entity.HasOne(d => d.DocumentVehicle).WithMany(p => p.DocumentVehicleQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_QUOTA_DOCUMENT_VEHICLE");
        });

        modelBuilder.Entity<DocumentVehicleSystem>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE_SYSTEM", tb => tb.HasComment("سیستم های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.DocumentVehicleTypeId).HasComment("شناسه انواع كلی وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MadeInIran)
                .IsFixedLength()
                .HasComment("آیا تولید داخل است؟");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentVehicleType).WithMany(p => p.DocumentVehicleSystems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_SYSTEM_DOCUMENT_VEHICLE_TYPE");
        });

        modelBuilder.Entity<DocumentVehicleTaxTable>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE_TAX_TABLE", tb => tb.HasComment("جدول ارزش وسایل نقلیه برای محاسبه مالیات نقل و انتقال"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentVehicleTipId).HasComment("شناسه تیپ های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MadeInYear)
                .IsFixedLength()
                .HasComment("سال تولید وسیله نقلیه");
            entity.Property(e => e.Price).HasComment("ارزش");
            entity.Property(e => e.SaleTaxPercent).HasComment("درصد مالیات نقل و انتقال");
            entity.Property(e => e.Tax).HasComment("مالیات نقل و انتقال");
            entity.Property(e => e.TaxYear)
                .IsFixedLength()
                .HasComment("سال اعلام جدول ارزش مالیاتی");

            entity.HasOne(d => d.DocumentVehicleTip).WithMany(p => p.DocumentVehicleTaxTables)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_TAX_TABLE_DOCUMENT_VEHICLE_TIP");
        });

        modelBuilder.Entity<DocumentVehicleTip>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE_TIP", tb => tb.HasComment("تیپ های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.CountUnitTitle).HasComment("عنوان واحد شمارش");
            entity.Property(e => e.DocumentVehicleSystemId).HasComment("شناسه انواع سیستم های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی");
            entity.Property(e => e.DocumentVehicleTypeId).HasComment("شناسه انواع كلی وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MadeInIran)
                .IsFixedLength()
                .HasComment("آیا تولید داخل است؟");
            entity.Property(e => e.MalyatiCode).HasComment("کد در جداول سازمان مالیاتی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentVehicleSystem).WithMany(p => p.DocumentVehicleTips)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_TIP_DOCUMENT_VEHICLE_SYSTEM");

            entity.HasOne(d => d.DocumentVehicleType).WithMany(p => p.DocumentVehicleTips)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_VEHICLE_TIP_DOCUMENT_VEHICLE_TYPE");
        });

        modelBuilder.Entity<DocumentVehicleType>(entity =>
        {
            entity.ToTable("DOCUMENT_VEHICLE_TYPE", tb => tb.HasComment("انواع كلی وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MadeInIran)
                .IsFixedLength()
                .HasComment("آیا تولید داخل است؟");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateDivisionRequestElement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ESTATE_DIVISION_REQUEST_ELE");

            entity.ToTable("ESTATE_DIVISION_REQUEST_ELEMENTS", tb => tb.HasComment("جدول فیك قطعات تقسیم نامه ملك"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ElementsJson).HasComment("جيسون");
            entity.Property(e => e.EstateBasic).HasComment("اصلي");
            entity.Property(e => e.EstateSecondary).HasComment("فرعي");
            entity.Property(e => e.EstateSectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.EstateSubsectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Unitid).HasComment("شناسه رکورد در جدول متناظر");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.EstateDivisionRequestElements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_DIVISION_REQUEST_ELE");

            entity.HasOne(d => d.EstateSubsection).WithMany(p => p.EstateDivisionRequestElements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_DIVISION_REQUEST_EL2");
        });

        modelBuilder.Entity<EstateDocReqPersonRelate>(entity =>
        {
            entity.ToTable("ESTATE_DOC_REQ_PERSON_RELATE", tb => tb.HasComment("اشخاص وابسته به شخص اصلی درخواست"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AgentDocumentCountryId).HasComment("ردیف كشوری كه وكالتنامه در آن تنظیم شده ");
            entity.Property(e => e.AgentDocumentDate)
                .IsFixedLength()
                .HasComment("تاریخ مدرك");
            entity.Property(e => e.AgentDocumentId).HasComment("Agent_Document_Id");
            entity.Property(e => e.AgentDocumentIssuer).HasComment("مرجع صدور مدرك");
            entity.Property(e => e.AgentDocumentNo).HasComment("شماره مدرك");
            entity.Property(e => e.AgentDocumentScriptoriumId).HasComment("ردیف Agent_Document_Scriptorium_");
            entity.Property(e => e.AgentDocumentSecretCode).HasComment("رمز تصدیق");
            entity.Property(e => e.AgentPersonId).HasComment("ردیف شخص نماینده");
            entity.Property(e => e.AgentTypeId).HasComment("ردیف نوع ارتباط");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.EstateDocumentRequestId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAgentDocumentAbroad)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در مرجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.IsLawyer)
                .IsFixedLength()
                .HasComment("آیا وكیل دادگستری است؟");
            entity.Property(e => e.IsRelatedDocumentInSsar)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در سامانه ثبت اسناد ثبت شده است?");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.MainPersonId).HasComment("ردیف شخص اصلی");
            entity.Property(e => e.ReliablePersonReasonId).HasComment("ردیف دلیل نیاز به متعمد");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.AgentPerson).WithMany(p => p.EstateDocReqPersonRelateAgentPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_PER_REL_AGENT_PERSON");

            entity.HasOne(d => d.AgentType).WithMany(p => p.EstateDocReqPersonRelates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_PER_REL_AGENT");

            entity.HasOne(d => d.EstateDocumentRequest).WithMany(p => p.EstateDocReqPersonRelates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_DOC_REQ_PERSON_RELATE_DOCUMENT_REQUEST");

            entity.HasOne(d => d.MainPerson).WithMany(p => p.EstateDocReqPersonRelateMainPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_PER_REL_CORE_PERSON");

            entity.HasOne(d => d.ReliablePersonReason).WithMany(p => p.EstateDocReqPersonRelates).HasConstraintName("FK_DOCREQPERSRELNTTPERSONTYPE");
        });

        modelBuilder.Entity<EstateDocumentCurrentType>(entity =>
        {
            entity.ToTable("ESTATE_DOCUMENT_CURRENT_TYPE", tb => tb.HasComment("نوع سند مالكیت"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateDocumentRequest>(entity =>
        {
            entity.ToTable("ESTATE_DOCUMENT_REQUEST", tb => tb.HasComment("درخواست صدور سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی ملك");
            entity.Property(e => e.Basic).HasComment("پلاك اصلی ملك");
            entity.Property(e => e.BasicRemaining)
                .IsFixedLength()
                .HasComment("پلاك اصلی ملك-باقیمانده");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.BlockNo).HasComment("شماره بلوك ملك");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.DefectiveRequestId).HasComment("ردیف درخواست دارای نقص قبلی");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentCurrentTypeId).HasComment("ردیف نوع سند فعلی");
            entity.Property(e => e.DocumentDigitalSign).HasComment("امضای الكترونیك سند - توسط سردفتر");
            entity.Property(e => e.DocumentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.DocumentRequestTypeId).HasComment("ردیف نوع درخواست");
            entity.Property(e => e.EstateOwnershipTypeId).HasComment("ردیف نوع مالكیت");
            entity.Property(e => e.EstateSectionId).HasComment("ردیف بخش");
            entity.Property(e => e.EstateSubsectionId).HasComment("ردیف ناحیه");
            entity.Property(e => e.EstateTypeId).HasComment("ردیف نوع ملك");
            entity.Property(e => e.FinalVerificationVisited)
                .IsFixedLength()
                .HasComment("آیا وضعیت احراز هویت اشخاص با ثبت احوال  و كنترل بخشناه ها و بررسی و رویت شده است؟");
            entity.Property(e => e.FloorNo).HasComment("شماره طبقه ملك");
            entity.Property(e => e.Hamesh).HasComment("نواقص و اشكالات درخواست كه از سوی اداره املاك برگشت داده شده است");
            entity.Property(e => e.HasSecondary)
                .IsFixedLength()
                .HasComment("پلاك فرعی دارد");
            entity.Property(e => e.HowToPay).HasComment("روش پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آيا هزينه ها پرداخت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.No).HasComment("شماره درخواست");
            entity.Property(e => e.PayCostDate)
                .IsFixedLength()
                .HasComment("تاريخ پرداخت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت");
            entity.Property(e => e.PaymentType).HasComment("شيوه پرداخت");
            entity.Property(e => e.PieceNo).HasComment("شماره قطعه ملك");
            entity.Property(e => e.PostBarcode).HasComment("باركد پستی");
            entity.Property(e => e.PostalCode).HasComment("كد پستی ملك");
            entity.Property(e => e.ReceiptNo).HasComment("شماره فاکتور");
            entity.Property(e => e.RequestDate)
                .IsFixedLength()
                .HasComment("تاریخ درخواست");
            entity.Property(e => e.RevokedRequestId).HasComment("ردیف درخواست باطل شده ای كه از هزینه آن برای پرداخت این درخواست استفاده شده است");
            entity.Property(e => e.SardaftarConfirmDate).HasComment("تاریخ و زمان تایید سردفتر");
            entity.Property(e => e.SardaftarNamefamily).HasComment("نام و نام خانوادگی سردفتر");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفترخانه");
            entity.Property(e => e.Secondary).HasComment("پلاك فرعی ملك");
            entity.Property(e => e.SecondaryRemaining)
                .IsFixedLength()
                .HasComment("پلاك فرعی ملك- باقیمانده");
            entity.Property(e => e.SignCertificate).HasComment("گواهي");
            entity.Property(e => e.SignCertificateDn).HasComment("گواهی امضای الكترونیك مورد استفاده برای امضای سردفتر");
            entity.Property(e => e.SumPrices).HasComment("مبلغ قابل پرداخت");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.TransferDocumentDate)
                .IsFixedLength()
                .HasComment("تاريخ سند ارسالي");
            entity.Property(e => e.TransferDocumentIsInSsar)
                .IsFixedLength()
                .HasComment("آيا سند در ثبت آني هست؟");
            entity.Property(e => e.TransferDocumentNo).HasComment("شماره سند ارسالي");
            entity.Property(e => e.TransferDocumentScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.TransferDocumentTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.TransferDocumentVerificationCode).HasComment("کد سند ارسالي");
            entity.Property(e => e.UnitId).HasComment("ردیف واحد ثبتی");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.DefectiveRequest).WithMany(p => p.InverseDefectiveRequest).HasConstraintName("FK_EST_DOC_REQ_DEFECTIVE_REQ");

            entity.HasOne(d => d.DocumentCurrentType).WithMany(p => p.EstateDocumentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_DOC_CUR_TYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.EstateDocumentRequests).HasConstraintName("FK_ESTATE_DOCUMENT_REQUEST_DOCUMENT");

            entity.HasOne(d => d.DocumentRequestType).WithMany(p => p.EstateDocumentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_REQUEST_TYPE");

            entity.HasOne(d => d.EstateOwnershipType).WithMany(p => p.EstateDocumentRequests).HasConstraintName("FK_EST_DOC_REQ_EST_OWNERSHIP_TYPE");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.EstateDocumentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_EST_SECTION");

            entity.HasOne(d => d.EstateSubsection).WithMany(p => p.EstateDocumentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_EST_SUB_SECTION");

            entity.HasOne(d => d.EstateType).WithMany(p => p.EstateDocumentRequests).HasConstraintName("FK_EST_DOC_REQ_EST_TYPE");

            entity.HasOne(d => d.RevokedRequest).WithMany(p => p.InverseRevokedRequest).HasConstraintName("FK_EST_DOC_REQ_REVOKED_REQ");

            entity.HasOne(d => d.TransferDocumentType).WithMany(p => p.EstateDocumentRequests).HasConstraintName("FK_ESTATE_DOCUMENT_REQUEST_DOCUMENT_TYPE");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.EstateDocumentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATEDOCUMENTREQUEST_WORKFLOWSTATES");
        });

        modelBuilder.Entity<EstateDocumentRequestPerson>(entity =>
        {
            entity.ToTable("ESTATE_DOCUMENT_REQUEST_PERSON", tb => tb.HasComment("اشخاص مرتبط"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس");
            entity.Property(e => e.AgentTypeId).HasComment("ردیف نوع نمایندگی");
            entity.Property(e => e.BirthDate)
                .IsFixedLength()
                .HasComment("تاریخ تولد/ثبت");
            entity.Property(e => e.CityId).HasComment("شناسه شهر");
            entity.Property(e => e.CompanyTypeId).HasComment("ردیف نوع شركت");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.DocumentRequestId).HasComment("ردیف درخواست");
            entity.Property(e => e.Email).HasComment("ایمیل");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.FaxNo).HasComment("شماره فكس");
            entity.Property(e => e.ForiegnIssuePlace).HasComment("محل صدور خارجي");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("كارت ملی هوشمند دارد");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور/ثبت");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه/ثبت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("زنده است؟");
            entity.Property(e => e.IsFingerprintGotten)
                .IsFixedLength()
                .HasComment("اخذ اثر انگشت انجام شده");
            entity.Property(e => e.IsForeignerssysChecked)
                .IsFixedLength()
                .HasComment("آيا شخص خارجي چک شده است؟");
            entity.Property(e => e.IsForeignerssysCorrect)
                .IsFixedLength()
                .HasComment("آيا شخص خارجي صحيح است؟");
            entity.Property(e => e.IsIlencChecked)
                .IsFixedLength()
                .HasComment("آيا با پايگاه اشخاص حقوقي چک شده است؟");
            entity.Property(e => e.IsIlencCorrect)
                .IsFixedLength()
                .HasComment("آيا با پايگاه اشخاص حقوقي تطابق دارد؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .IsFixedLength()
                .HasComment("مالك است؟");
            entity.Property(e => e.IsRelated)
                .IsFixedLength()
                .HasComment("نماینده یا وكیل یا ... است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("با ثبت احوال چك شده؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("تطابق اطلعات با ثبت احوال");
            entity.Property(e => e.IssuePlaceId).HasComment("شناسه محل صدور");
            entity.Property(e => e.LastLegalPaperDate)
                .IsFixedLength()
                .HasComment("تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.LegalpersonNatureId).HasComment("ردیف ماهیت");
            entity.Property(e => e.LegalpersonTypeId).HasComment("ردیف نوع");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعیت صحت سنجی مویایل");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت moc");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شماره/شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("ردیف تابعیت");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.SanaHasOrganizationChart)
                .IsFixedLength()
                .HasComment("آیا شخص حقوقی ساختار تشكیلات دارد در سامانه ثنا");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام از ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام از ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره تلفن همراه در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("كد شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("ایا در سامانه ثنا حساب كاربری دارد؟");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Seri).HasComment("شماره سریال شناسنامه-قسمت دوم");
            entity.Property(e => e.SeriAlpha).HasComment("شماره سریال شناسنامه-قسمت اول");
            entity.Property(e => e.Serial).HasComment("شماره سریال شناسنامه-قسمت سوم");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");
            entity.Property(e => e.TfaRequired)
                .IsFixedLength()
                .HasComment("رمز دو عاملی نیاز است؟");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("وضعیت رمز دو عاملی");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.AgentType).WithMany(p => p.EstateDocumentRequestPeople).HasConstraintName("FK_EST_DOC_REQ_PER_AGENT_TYPE");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.EstateDocumentRequestPeople).HasConstraintName("FK_ESTATE_DOCUMENT_REQUEST_PERSON_DOCUMENT_PERSON");

            entity.HasOne(d => d.DocumentRequest).WithMany(p => p.EstateDocumentRequestPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EST_DOC_REQ_PER_EST_DOC_REQ");
        });

        modelBuilder.Entity<EstateDocumentRequestStatus>(entity =>
        {
            entity.ToTable("ESTATE_DOCUMENT_REQUEST_STATUS", tb => tb.HasComment("وضعیت  درخواست"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateDocumentRequestType>(entity =>
        {
            entity.ToTable("ESTATE_DOCUMENT_REQUEST_TYPE", tb => tb.HasComment("نوع درخواست"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateInquiry>(entity =>
        {
            entity.ToTable("ESTATE_INQUIRY", tb => tb.HasComment("استعلام ملك"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ApartmentsTotalarea).HasComment("مجموع مساحت اعیانی ها");
            entity.Property(e => e.Area).HasComment("مساحت");
            entity.Property(e => e.AttachedToDealsummary)
                .IsFixedLength()
                .HasComment("ضمیمه به خلاصه معامله");
            entity.Property(e => e.Basic).HasComment("پلاك اصلی");
            entity.Property(e => e.BasicRemaining)
                .IsFixedLength()
                .HasComment("پلاك اصلی_باقیمانده");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.CaseagentResponse).HasComment("پاسخ متصدی پرونده ملك");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت سیسیتمی");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت سیستمی استعلام");
            entity.Property(e => e.DealSummaryDate)
                .IsFixedLength()
                .HasComment("تاریخ خلاصخه معامله قطعی");
            entity.Property(e => e.DealSummaryNo).HasComment("شماره خلاصه معامله قطعی");
            entity.Property(e => e.DealSummaryScriptorium).HasComment("دفترخانه تنظیم كننده");
            entity.Property(e => e.DocPrintNo).HasComment("شماره چاپی سند");
            entity.Property(e => e.DocumentIsNote)
                .IsFixedLength()
                .HasComment("سند دفنرچه ای است");
            entity.Property(e => e.DocumentIsReplica)
                .IsFixedLength()
                .HasComment("سند المثنی است");
            entity.Property(e => e.EdeclarationNo).HasComment("شماره اظهارنامه");
            entity.Property(e => e.ElectronicEstateNoteNo).HasComment("شماره دفتر املاك الكترونیك");
            entity.Property(e => e.EstateAddress).HasComment("آدرس ملك");
            entity.Property(e => e.EstateInquiryId).HasComment("ردیف استعلام پیرو");
            entity.Property(e => e.EstateInquiryTypeId).HasComment("ردیف نوع استعلام");
            entity.Property(e => e.EstateNoteNo).HasComment("شماره دفتر");
            entity.Property(e => e.EstatePostalCode).HasComment("كد پستی ملك");
            entity.Property(e => e.EstateSectionId).HasComment("ردیف بخش");
            entity.Property(e => e.EstateSeridaftarId).HasComment("ردیف سری دفتر");
            entity.Property(e => e.EstateSubsectionId).HasComment("ردیف ناحیه");
            entity.Property(e => e.FirstReceiveDate)
                .IsFixedLength()
                .HasComment("تاریخ دریافت اولین ارسال");
            entity.Property(e => e.FirstReceiveTime).HasComment("زمان دریافت اولین ارسال");
            entity.Property(e => e.FirstSendDate)
                .IsFixedLength()
                .HasComment("تاریخ اولین ارسال");
            entity.Property(e => e.FirstSendTime).HasComment("زمان اولین ارسال");
            entity.Property(e => e.Followeinquiryno).HasComment("شماره استعلام پيروي");
            entity.Property(e => e.Followerinquirydate).HasComment("تاريخ استعلام پيروي");
            entity.Property(e => e.GeoLocationId).HasComment("ردیف شهر ملك");
            entity.Property(e => e.HowToPay).HasComment("روش پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.InquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام");
            entity.Property(e => e.InquiryKey).HasComment("کليد استعلام");
            entity.Property(e => e.InquiryNo).HasComment("شماره استعلام");
            entity.Property(e => e.InquiryPaymantRefno).HasComment("شماره مرجع پرداخت هزینه استعلام");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آيا هزينه ها پرداخت شده است؟");
            entity.Property(e => e.IsFollowedInquiryUpdated)
                .IsFixedLength()
                .HasComment("آيا استعلام پيروي اصلاح شده است؟");
            entity.Property(e => e.LastSendDate)
                .IsFixedLength()
                .HasComment("تاریخ آخرین ارسال");
            entity.Property(e => e.LastSendTime).HasComment("زمان اخرین ارسال");
            entity.Property(e => e.LasteditReceiveDate)
                .IsFixedLength()
                .HasComment("تاریخ دریافت آخرین ویرایش");
            entity.Property(e => e.LasteditReceiveTime).HasComment("زمان دریافت آخرین ویرایش");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.Mention).HasComment("تذكر");
            entity.Property(e => e.MobileNo).HasComment("تلفن همرا ه برای پیامك");
            entity.Property(e => e.MortgageText).HasComment("متن رهن");
            entity.Property(e => e.No).HasComment("شماره سیستمی");
            entity.Property(e => e.No2).HasComment("شماره سیستمی 2");
            entity.Property(e => e.Note21PaymentRefno).HasComment("شماره مرجع پرداخت تبصره 21 ق.ب");
            entity.Property(e => e.PageNo).HasComment("شماره صفحه");
            entity.Property(e => e.PayCostDate)
                .IsFixedLength()
                .HasComment("تاريخ پرداخت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت");
            entity.Property(e => e.PaymentType).HasComment("شيوه پرداخت");
            entity.Property(e => e.PrevFollowedInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ReceiptNo).HasComment("شماره فاکتور");
            entity.Property(e => e.RegisterNo).HasComment("شماره ثبت");
            entity.Property(e => e.RelatedOwnershipId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Response).HasComment("متن پاسخ");
            entity.Property(e => e.ResponseCode).HasComment("كد پاسخ");
            entity.Property(e => e.ResponseDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ");
            entity.Property(e => e.ResponseDigitalsignature).HasComment("امضای دیجیتال پاسخ");
            entity.Property(e => e.ResponseNumber).HasComment("شماره پاسخ");
            entity.Property(e => e.ResponseReceiveDate)
                .IsFixedLength()
                .HasComment("تاریخ دریافت پاسخ");
            entity.Property(e => e.ResponseReceiveTime).HasComment("زمان دریافت پاسخ");
            entity.Property(e => e.ResponseResult).HasComment("نتیجه پاسخ");
            entity.Property(e => e.ResponseSubjectdn).HasComment("موضوع امضای دیجیتال پاسخ");
            entity.Property(e => e.ResponseTime).HasComment("زمان پاسخ");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف فرستنده");
            entity.Property(e => e.Secondary).HasComment("پلاك فرعی");
            entity.Property(e => e.SecondaryRemaining)
                .IsFixedLength()
                .HasComment("پلاك فرعی_باقیمانده");
            entity.Property(e => e.SeparationDate).HasComment("تاریخ صورت مجلس تفكیك");
            entity.Property(e => e.SeparationNo).HasComment("شماره صورت مجلس تفكیك");
            entity.Property(e => e.SpecificStatus).HasComment("وضعیت خاص");
            entity.Property(e => e.SumPrices).HasComment("مبلغ قابل پرداخت");
            entity.Property(e => e.SystemMessage).HasComment("متن پيام سيستم");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.TrtsReadDate)
                .IsFixedLength()
                .HasComment("TrtsReadDate");
            entity.Property(e => e.TrtsReadTime).HasComment("TrtsReadTime");
            entity.Property(e => e.UnitId).HasComment("ردیف گیرنده");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.EstateInquiryNavigation).WithMany(p => p.InverseEstateInquiryNavigation).HasConstraintName("FK_ESTATE_INQUIRY_INQUIRY");

            entity.HasOne(d => d.EstateInquiryType).WithMany(p => p.EstateInquiries).HasConstraintName("FK_ESTATE_INQUIRY_TYPE");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.EstateInquiries).HasConstraintName("FK_ESTATE_INQUIRY_SECTION");

            entity.HasOne(d => d.EstateSeridaftar).WithMany(p => p.EstateInquiries).HasConstraintName("FK_ESTATE_INQUIRY_SERIDAFTAR");

            entity.HasOne(d => d.EstateSubsection).WithMany(p => p.EstateInquiries).HasConstraintName("FK_ESTATE_INQUIRY_SUBSECTION");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.EstateInquiries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATEINQUIRY_WORKFLOWSTATES");
        });

        modelBuilder.Entity<EstateInquiryActionType>(entity =>
        {
            entity.ToTable("ESTATE_INQUIRY_ACTION_TYPE", tb => tb.HasComment("نوع اقدام استعلام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateInquiryPerson>(entity =>
        {
            entity.ToTable("ESTATE_INQUIRY_PERSON", tb => tb.HasComment("شخص استعلام(مالك)"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد/ثبت");
            entity.Property(e => e.BirthPlaceId).HasComment("ردیف محل تولد");
            entity.Property(e => e.CityId).HasComment("ردیف محل سكونت");
            entity.Property(e => e.CompanyTypeId).HasComment("شناسه نوع شرکت");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.Email).HasComment("پست الکترونيک");
            entity.Property(e => e.EstateInquiryId).HasComment("ردیف استعلام");
            entity.Property(e => e.ExecutiveTransfer)
                .IsFixedLength()
                .HasComment("انتقال اجرایی");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.Fax).HasComment("فکس");
            entity.Property(e => e.ForiegnBirthPlace).HasComment("محل تولد خارجی");
            entity.Property(e => e.ForiegnIssuePlace).HasComment("محل صدور خارجی");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("كارت ملی هوشمند دارد؟");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه/ثبت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آيا زنده است؟");
            entity.Property(e => e.IsForeignerssysChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام از سامانه اتباع خارجی انجام شده است؟");
            entity.Property(e => e.IsForeignerssysCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با ساماه اتباع خارجی تطابق دارد؟");
            entity.Property(e => e.IsIlencChecked)
                .IsFixedLength()
                .HasComment("ایا استعلام از سامانه اشخاص حقوقی برای این شخص انجام شده است؟");
            entity.Property(e => e.IsIlencCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با سامانه اشخاص حقوقی تطابق دارد؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .HasDefaultValueSql("'1'                   ")
                .IsFixedLength()
                .HasComment("اصيل است؟");
            entity.Property(e => e.IsRelated)
                .HasDefaultValueSql("'2'                   ")
                .IsFixedLength()
                .HasComment("وابسته است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با ثبت احوال تطابق دارد؟");
            entity.Property(e => e.IssuePlaceId).HasComment("ردیف محل صدور");
            entity.Property(e => e.IssuePlaceText).HasComment("محل صدور");
            entity.Property(e => e.LastLegalPaperDate)
                .IsFixedLength()
                .HasComment("تاريخ آخرين روزنامه رسمي");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرين روزنامه رسمي");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعيت مالکيت خط موبايل با شاهکار");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت كارت ملی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalityCode).HasComment("كد/شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("ردیف تابعیت");
            entity.Property(e => e.OtherLegacyId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.SanaHasOrganizationChart).HasComment("آيا در ثنا چارت سازماني دارد؟");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاريخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره موبايل ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("کد سازمان ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام سازمان ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعیت ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حروفی");
            entity.Property(e => e.SerialNo).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.SharePart).HasComment("جزسهم");
            entity.Property(e => e.ShareText).HasComment("متن سهم");
            entity.Property(e => e.ShareTotal).HasComment("كل سهم");
            entity.Property(e => e.Tel).HasComment("تلفن");
            entity.Property(e => e.TfaRequired)
                .IsFixedLength()
                .HasComment("رمز دو عاملی نیاز است؟");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("وضعیت رمز دوعاملی؟");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.VerifiedBySourceservice).HasComment("كنترل شده با سرویس مبدا");

            entity.HasOne(d => d.EstateInquiry).WithMany(p => p.EstateInquiryPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INQUIRY_PERSON_ESTATE_INQ");
        });

        modelBuilder.Entity<EstateInquirySendedSm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ESTATE_INQUIRY_SENDED_SMS_I");

            entity.ToTable("ESTATE_INQUIRY_SENDED_SMS", tb => tb.HasComment("پیامك های ارسال شده برای استعلام مالیاتی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.EstateInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Message).HasComment("پيام");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاريخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.SmsTrackingNo).HasComment("شماره رهگيري پيامک");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.EstateInquiry).WithMany(p => p.EstateInquirySendedSms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_INQUIRY_SENDED_SMS_ESTATE_INQUIRY");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.EstateInquirySendedSms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_INQUIRY_SENDED_SMS_WORKFLOW_STATES");
        });

        modelBuilder.Entity<EstateInquirySendreceiveLog>(entity =>
        {
            entity.ToTable("ESTATE_INQUIRY_SENDRECEIVE_LOG", tb => tb.HasComment("سوابق ارسال و دریافت پاسخ"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ActionDate)
                .IsFixedLength()
                .HasComment("تاریخ اقدام");
            entity.Property(e => e.ActionNumber).HasComment("شماره اقدام");
            entity.Property(e => e.ActionText).HasComment("متن اقدام");
            entity.Property(e => e.ActionTime).HasComment("زمان اقدام");
            entity.Property(e => e.EstateInquiryActionTypeId).HasComment("ردیف نوع اقدام");
            entity.Property(e => e.EstateInquiryId).HasComment("ردیف استعلام ملك");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.EstateInquiryActionType).WithMany(p => p.EstateInquirySendreceiveLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INQUIRY_SR_LOG_ACTION_TYPE");

            entity.HasOne(d => d.EstateInquiry).WithMany(p => p.EstateInquirySendreceiveLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IINQUIRY_SR_LOG_ESTATE_INQ");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.EstateInquirySendreceiveLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATEINQUIRYSENDRECEIVE_LOG_WORKFLOWSTATES");
        });

        modelBuilder.Entity<EstateInquiryStatus>(entity =>
        {
            entity.ToTable("ESTATE_INQUIRY_STATUS", tb => tb.HasComment("وضعیت استعلام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateInquiryType>(entity =>
        {
            entity.ToTable("ESTATE_INQUIRY_TYPE", tb => tb.HasComment("نوع استعلام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateOwnershipType>(entity =>
        {
            entity.ToTable("ESTATE_OWNERSHIP_TYPE", tb => tb.HasComment("نوع مالكیت"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("کليد اصلي رکورد در سامانه قبلي");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstatePieceMainType>(entity =>
        {
            entity.ToTable("ESTATE_PIECE_MAIN_TYPE", tb => tb.HasComment("نوع اصلی قطعه ملكی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstatePieceType>(entity =>
        {
            entity.ToTable("ESTATE_PIECE_TYPE", tb => tb.HasComment("نوع قطعه ملكی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.EstatePieceMainTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.EstatePieceMainType).WithMany(p => p.EstatePieceTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_PIECE_TYPE_MAIN_ID");
        });

        modelBuilder.Entity<EstateSection>(entity =>
        {
            entity.ToTable("ESTATE_SECTION", tb => tb.HasComment("بخش ثبتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.SsaaCode).HasComment("كد ثبتی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.UnitId).HasComment("ردیف واحد ثبتی");
        });

        modelBuilder.Entity<EstateSeridaftar>(entity =>
        {
            entity.ToTable("ESTATE_SERIDAFTAR", tb => tb.HasComment("سری دفتر"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.SsaaCode).HasComment("كد ثبتی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.UnitId).HasComment("ردیف واحد ثبتی");
        });

        modelBuilder.Entity<EstateSideType>(entity =>
        {
            entity.ToTable("ESTATE_SIDE_TYPE", tb => tb.HasComment("سمت قطعه ملكی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateSubsection>(entity =>
        {
            entity.ToTable("ESTATE_SUBSECTION", tb => tb.HasComment("ناحیه ثبتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.EstateSectionId).HasComment("ردیف بخش");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.SsaaCode).HasComment("كد ثبتی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.UnitId).HasComment("ردیف عامل پارتیشن");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.EstateSubsections)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_SUBSECTION_SECTION");
        });

        modelBuilder.Entity<EstateTaxCity>(entity =>
        {
            entity.ToTable("ESTATE_TAX_CITY", tb => tb.HasComment("شهر امور مالیاتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.EstateTaxCountyId).HasComment("ردیف شهرستان");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.EstateTaxCounty).WithMany(p => p.EstateTaxCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TAXCITY_TAXCOUNTY");
        });

        modelBuilder.Entity<EstateTaxCounty>(entity =>
        {
            entity.ToTable("ESTATE_TAX_COUNTY", tb => tb.HasComment("شهرستان امور مالیاتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.EstateTaxProvinceId).HasComment("ردیف استان");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.EstateTaxProvince).WithMany(p => p.EstateTaxCounties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TAXCOUNTY_TAXPROVINCE");
        });

        modelBuilder.Entity<EstateTaxInquiry>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY", tb => tb.HasComment("استعلام مالیاتی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ApartmentArea).HasComment("مساحت مفید اعیانی");
            entity.Property(e => e.ArsehArea).HasComment("مساحت عرصه");
            entity.Property(e => e.Avenue).HasComment("خیابان");
            entity.Property(e => e.BasicRemaining)
                .IsFixedLength()
                .HasComment("باقيمانده اصلي");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.BuildingOld).HasComment("قدمت ساختمان");
            entity.Property(e => e.BuildingType).HasComment("نوع ساختمان");
            entity.Property(e => e.BuildingUsingTypeId).HasComment("ردیف نوع كاربری اعیان");
            entity.Property(e => e.BuildingValue).HasComment("ارزش معاملاتي محاسبه شده اعيان");
            entity.Property(e => e.CertificateFile).HasComment("فایل گواهی");
            entity.Property(e => e.CertificateHtml).HasComment("فایل اچ تی ام ال گواهی");
            entity.Property(e => e.CertificateNo).HasComment("شماره گواهی");
            entity.Property(e => e.CessionDate)
                .IsFixedLength()
                .HasComment("تاریخ نقل و انتقال");
            entity.Property(e => e.CessionPrice).HasComment("مبلغ معامله");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.EstateAddress).HasComment("آدرس ملك");
            entity.Property(e => e.EstateInquiryId).HasComment("ردیف استعلام ملك");
            entity.Property(e => e.EstatePostCode).HasComment("كد پستی ملك");
            entity.Property(e => e.EstateSectionId).HasComment("ردیف بخش ثبتی");
            entity.Property(e => e.EstateSector).HasComment("شماره قطعه ملك");
            entity.Property(e => e.EstateSubsectionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.EstateTaxCityId).HasComment("ردیف شهر");
            entity.Property(e => e.EstateTaxCountyId).HasComment("ردیف شهرستان");
            entity.Property(e => e.EstateTaxInquiryBuildingConstructionStepId).HasComment("ردیف مرحله ساخت ساختمان اعیانی");
            entity.Property(e => e.EstateTaxInquiryBuildingStatusId).HasComment("ردیف وضعیت ساختمان اعیانی");
            entity.Property(e => e.EstateTaxInquiryBuildingTypeId).HasComment("ردیف نوع ساختمان اعیانی");
            entity.Property(e => e.EstateTaxInquiryDocumentRequestTypeId).HasComment("ردیف نوع درخواست سند");
            entity.Property(e => e.EstateTaxInquiryFieldTypeId).HasComment("ردیف نوع عرصه");
            entity.Property(e => e.EstateTaxInquiryTransferTypeId).HasComment("ردیف موضوع نقل و انتقال");
            entity.Property(e => e.EstateTaxUnitId).HasComment("ردیف اداره مالیاتی");
            entity.Property(e => e.EstateUnitId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Estatebasic).HasComment("پلاك اصلی ملك");
            entity.Property(e => e.Estatesecondary).HasComment("پلاك فرعی ملك");
            entity.Property(e => e.FacilityLawNumber).HasComment("شماره قانون تسهیل");
            entity.Property(e => e.FacilityLawYear).HasComment("سال قانون تسهيل");
            entity.Property(e => e.FieldUsingTypeId).HasComment("ردیف نوع كاربری عرصه");
            entity.Property(e => e.FkEstateTaxProvinceId).HasComment("ردیف استان");
            entity.Property(e => e.FloorNo).HasComment("شماره طبقه");
            entity.Property(e => e.GoodWillValue).HasComment("مبلغ حق واگذاري محل ابرازي مودي");
            entity.Property(e => e.HasSpecialTrance)
                .IsFixedLength()
                .HasComment("آیا ملك دارای معابر اختصاصی می باشد");
            entity.Property(e => e.HasSpecifiedTradingValue)
                .IsFixedLength()
                .HasComment("آیا ملك دارای ارزش معاملاتی تعیین شده می باشد");
            entity.Property(e => e.HowToPay).HasComment("روش پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsActive)
                .IsFixedLength()
                .HasComment("فعال بودن");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آيا هزينه ها پرداخت شده است؟");
            entity.Property(e => e.IsFirstCession)
                .IsFixedLength()
                .HasComment("آیا پایان كار یا پروانه ساختمان و یا صورت مجلس تفكیكی به نام فرد انتقال دهنده (متقاضی) است؟");
            entity.Property(e => e.IsFirstDeal)
                .IsFixedLength()
                .HasComment("آیا نقل و انتقال فعلی ، اولین نقل و انتقال ملك است؟");
            entity.Property(e => e.IsGroundLevel)
                .IsFixedLength()
                .HasComment("در طبقه همكف واع است");
            entity.Property(e => e.IsLicenceReady)
                .IsFixedLength()
                .HasComment("آیا گواهی صادر شده؟");
            entity.Property(e => e.IsMissingSeparateDocument)
                .IsFixedLength()
                .HasComment("ملك فاقد سند تفكیكی و دارای بیش از یك نوع كاربری");
            entity.Property(e => e.IsWornTexture)
                .IsFixedLength()
                .HasComment("بافت فرسوده");
            entity.Property(e => e.LandValue).HasComment("ارزش معاملاتي محاسبه شده عرصه");
            entity.Property(e => e.LastReceiveStatusDate)
                .IsFixedLength()
                .HasComment("تاریخ آخرین دریافت وضعیت");
            entity.Property(e => e.LastReceiveStatusTime).HasComment("زمان آخرین دریافت وضعیت");
            entity.Property(e => e.LastSendDate)
                .IsFixedLength()
                .HasComment("تاریخ آخرین ارسال");
            entity.Property(e => e.LastSendTime).HasComment("زمان آخرین ارسال");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.LicenseDate)
                .IsFixedLength()
                .HasComment("تاریخ جواز");
            entity.Property(e => e.LocationAssignRightDealCurrentValue).HasComment("ارزش روز معامله حق واگذاری محل");
            entity.Property(e => e.LocationAssignRigthOwnershipTypeId).HasComment("ردیف نوع مالكیت حق واگذاری محل");
            entity.Property(e => e.LocationAssignRigthUsingTypeId).HasComment("ردیف نوع كاربری حق واگذاری محل");
            entity.Property(e => e.No).HasComment("شماره سیستمی اول");
            entity.Property(e => e.No2).HasComment("شماره سیستمی دوم");
            entity.Property(e => e.PayCostDate)
                .IsFixedLength()
                .HasComment("تاريخ پرداخت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت");
            entity.Property(e => e.PaymentType).HasComment("شيوه پرداخت");
            entity.Property(e => e.PlateNo).HasComment("شماره پلاك");
            entity.Property(e => e.PrevTransactionsAccordingToFacilitateRule)
                .IsFixedLength()
                .HasComment("معاملات قبلی براساس قانون تسهیل انجام شده");
            entity.Property(e => e.ReceiptNo).HasComment("شماره فاکتور");
            entity.Property(e => e.RenovationRelatedBlockNo).HasComment("شماره بلوك قبض عوارض نوسازی");
            entity.Property(e => e.RenovationRelatedEstateNo).HasComment("شماره ملك قبض عوارض نوع سازی");
            entity.Property(e => e.RenovationRelatedRowNo).HasComment("شماره ردیف قبض عوارض نوسازی");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفترخانه");
            entity.Property(e => e.SecondaryRemaining)
                .IsFixedLength()
                .HasComment("باقيمانده فرعي");
            entity.Property(e => e.SeparationProcessNo).HasComment("شماره صورت مجلس تفكیكی");
            entity.Property(e => e.ShareOfOwnership).HasComment("سهم مالك از مالكیت");
            entity.Property(e => e.ShebaNo).HasComment("شبا");
            entity.Property(e => e.ShebaNo2).HasComment("شماره شبا 2");
            entity.Property(e => e.ShebaNo3).HasComment("شماره شبا 3");
            entity.Property(e => e.StatusDescription).HasComment("متن وضعیت استعلام");
            entity.Property(e => e.SumPrices).HasComment("مبلغ قابل پرداخت");
            entity.Property(e => e.TaxAmount).HasComment("بدهی مالیاتی");
            entity.Property(e => e.TaxAmount2).HasComment("مبلغ بدهي مالياتي 2");
            entity.Property(e => e.TaxAmount3).HasComment("مبلغ بدهي مالياتي 3");
            entity.Property(e => e.TaxBillHtml).HasComment("قبض مالیاتی");
            entity.Property(e => e.TaxBillIdentity).HasComment("شناصه قبض");
            entity.Property(e => e.TaxBillIdentity2).HasComment("شناسه قبض 2");
            entity.Property(e => e.TaxBillIdentity3).HasComment("شناسه قبض 3");
            entity.Property(e => e.TaxGoodWillValue).HasComment("مبلغ ماليات حق واگذاري محل");
            entity.Property(e => e.TaxPaymentIdentity).HasComment("شناسه پرداخت");
            entity.Property(e => e.TaxPaymentIdentity2).HasComment("شناسه پرداخت 2");
            entity.Property(e => e.TaxPaymentIdentity3).HasComment("شناسه پرداخت 3");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.TotalArea).HasComment("مساحت كل اعیانی ها");
            entity.Property(e => e.TotalOwnershipShare).HasComment("كل سهم مالكیت");
            entity.Property(e => e.TrackingCode).HasComment("كد رهگیری");
            entity.Property(e => e.TranceWidth).HasComment("عرض از معبر عرصه");
            entity.Property(e => e.TransitionShare).HasComment("میزان انتقال برحسب سهم");
            entity.Property(e => e.ValuebookletBlockNo).HasComment("شماره بلوك بر اساس دفترچه ارزش معاملاتی");
            entity.Property(e => e.ValuebookletRowNo).HasComment("شماره ردیف براساس دفترچه ارزش معاملاتی");
            entity.Property(e => e.WorkCompletionCertificateDate)
                .IsFixedLength()
                .HasComment("تاریخ گواهی پایان كار");
            entity.Property(e => e.WorkflowStatesId).HasComment("ردیف وضعیت");

            entity.HasOne(d => d.BuildingUsingType).WithMany(p => p.EstateTaxInquiryBuildingUsingTypes).HasConstraintName("FK_ESTATETAXINQUIRY_BUILDING_USING_TYPE");

            entity.HasOne(d => d.EstateInquiry).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_ESTATE_INQUIRY");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_ESTATE_SECTION");

            entity.HasOne(d => d.EstateSubsection).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_ESTATE_SUBSECTION");

            entity.HasOne(d => d.EstateTaxCity).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_TAX_CITY");

            entity.HasOne(d => d.EstateTaxCounty).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_TAXCOUNTY");

            entity.HasOne(d => d.EstateTaxInquiryBuildingConstructionStep).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_BUILDING_CONSTRUCTION_STEP");

            entity.HasOne(d => d.EstateTaxInquiryBuildingStatus).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_BUILDING_STATUS");

            entity.HasOne(d => d.EstateTaxInquiryBuildingType).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_BUILDING_TYPE");

            entity.HasOne(d => d.EstateTaxInquiryDocumentRequestType).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATE_TAX_INQUIRY_DOCUMENT_REQUEST_TYPE");

            entity.HasOne(d => d.EstateTaxInquiryFieldType).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_FIELD_TYPE");

            entity.HasOne(d => d.EstateTaxInquiryTransferType).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_TRANSFER_TYPE");

            entity.HasOne(d => d.EstateTaxUnit).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_TAXUNIT");

            entity.HasOne(d => d.FieldUsingType).WithMany(p => p.EstateTaxInquiryFieldUsingTypes).HasConstraintName("FK_ESTATETAXINQUIRY_FIELD_USING_TYPE");

            entity.HasOne(d => d.FkEstateTaxProvince).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_TAXPROVINCE");

            entity.HasOne(d => d.LocationAssignRigthOwnershipType).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_LOCATION_ASSIGN_RIGTH_OWNERSHIP_TYPE");

            entity.HasOne(d => d.LocationAssignRigthUsingType).WithMany(p => p.EstateTaxInquiries).HasConstraintName("FK_ESTATETAXINQUIRY_LOCATION_ASSIGN_RIGTH_USING_TYPE");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.EstateTaxInquiries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATETAXINQUIRY_WORKFLOW_STATES");
        });

        modelBuilder.Entity<EstateTaxInquiryAttach>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_ATTACH", tb => tb.HasComment("منضمات ملك مورد استعلام"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Area).HasComment("مساحت");
            entity.Property(e => e.Block).HasComment("بلوك");
            entity.Property(e => e.ChangeState)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.EstatePieceTypeId).HasComment("ردیف نوع قطعه");
            entity.Property(e => e.EstateSideTypeId).HasComment("ردیف سمت");
            entity.Property(e => e.EstateTaxInquiryId).HasComment("ردیف استعلام مرتبط");
            entity.Property(e => e.Floor).HasComment("طبقه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.Piece).HasComment("شماره قطعه");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفترخانه");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.EstatePieceType).WithMany(p => p.EstateTaxInquiryAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATEINQUIRYATTACH_PIECE_TYPE");

            entity.HasOne(d => d.EstateSideType).WithMany(p => p.EstateTaxInquiryAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATETAXINQUIRYATTACH_SIDE");

            entity.HasOne(d => d.EstateTaxInquiry).WithMany(p => p.EstateTaxInquiryAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATETAXINQUIRYATTACH_TAXINQUIRY");
        });

        modelBuilder.Entity<EstateTaxInquiryBuildingConstructionStep>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_BUILDING_CONSTRUCTION_STEP", tb => tb.HasComment("مرحله ساخت ساختمان اعیانی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryBuildingStatus>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_BUILDING_STATUS", tb => tb.HasComment("وضعیت ساختمان اعیانی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryBuildingType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_BUILDING_TYPE", tb => tb.HasComment("نوع ساختمان اعیانی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryDocumentRequestType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_DOCUMENT_REQUEST_TYPE", tb => tb.HasComment("نوع درخواست سند"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryFieldType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_FIELD_TYPE", tb => tb.HasComment("نوع عرصه مورد استعلام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryFile>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_FILE", tb => tb.HasComment("فایل های پیوست استعلام مالیاتی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ArchiveAttachmentTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ArchiveMediaFileId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.AttachmentDate)
                .IsFixedLength()
                .HasComment("تاریخ پیوست");
            entity.Property(e => e.AttachmentDesc).HasComment("شرح");
            entity.Property(e => e.AttachmentNo).HasComment("شماره پیوست");
            entity.Property(e => e.AttachmentTitle).HasComment("عنوان پیوست");
            entity.Property(e => e.ChangeState)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.EstateTaxInquiryId).HasComment("ردیف استعلام مرتبط");
            entity.Property(e => e.FileContent).HasComment("محتویات فایل");
            entity.Property(e => e.FileExtention).HasComment("پسوند فایل");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفترخانه");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.EstateTaxInquiry).WithMany(p => p.EstateTaxInquiryFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATETAXINQUIRYFILE_TAXINQUIRY");
        });

        modelBuilder.Entity<EstateTaxInquiryLocationAssignRigthOwnershipType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_LOCATION_ASSIGN_RIGTH_OWNERSHIP_TYPE", tb => tb.HasComment("نوع مالكیت حق واگذاری محل"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryLocationAssignRigthUsingType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_LOCATION_ASSIGN_RIGTH_USING_TYPE", tb => tb.HasComment("نوع كاربری حق واگذاری محل"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryPerson>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_PERSON", tb => tb.HasComment("اشخاص مرتبط با امور مالیاتی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس");
            entity.Property(e => e.BirthDate)
                .IsFixedLength()
                .HasComment("تاریخ تولد/ثبت");
            entity.Property(e => e.BirthPlaceId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ChangeState)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.CityId).HasComment("شناسه شهر");
            entity.Property(e => e.CompanyTypeId).HasComment("شناسه نوع شرکت");
            entity.Property(e => e.DealsummaryPersonRelateTypeId).HasComment("ردیف نوع ارتباط");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.Email).HasComment("پست الکترونيک");
            entity.Property(e => e.EstateTaxInquiryId).HasComment("ردیف استعلام مرتبط");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.Fax).HasComment("فکس");
            entity.Property(e => e.ForiegnBirthPlace).HasComment("محل تولد خارجي");
            entity.Property(e => e.ForiegnIssuePlace).HasComment("محل صدور خارجي");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("آيا کارت ملي هوشمند دارد؟");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه/ثبت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آيا زنده است؟");
            entity.Property(e => e.IsForeignerssysChecked)
                .IsFixedLength()
                .HasComment("آيا شخص خارجي چک شده است؟");
            entity.Property(e => e.IsForeignerssysCorrect)
                .IsFixedLength()
                .HasComment("آيا شخص خارجي صحيح است؟");
            entity.Property(e => e.IsIlencChecked)
                .IsFixedLength()
                .HasComment("آيا با پايگاه اشخاص حقوقي چک شده است؟");
            entity.Property(e => e.IsIlencCorrect)
                .IsFixedLength()
                .HasComment("آيا با پايگاه اشخاص حقوقي تطابق دارد؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آيا ايراني است؟");
            entity.Property(e => e.IsOriginal)
                .HasDefaultValueSql("'1' ")
                .IsFixedLength()
                .HasComment("اصيل است؟");
            entity.Property(e => e.IsRelated)
                .HasDefaultValueSql("'2' ")
                .IsFixedLength()
                .HasComment("وابسته است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آيا با ثبت احوال چک شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آيا اطلاعات شخص با ثبت احوال تطابق دارد؟");
            entity.Property(e => e.IssuePlace).HasComment("محل صدور");
            entity.Property(e => e.IssuePlaceId).HasComment("شناسه محل صدور");
            entity.Property(e => e.LastLegalPaperDate)
                .IsFixedLength()
                .HasComment("تاريخ آخرين روزنامه رسمي");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرين روزنامه رسمي");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعيت مالکيت خط موبايل با شاهکار");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعيت ام او سي");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalityCode).HasComment("شماره/شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("ردیف تابعیت");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RoleType).HasComment("نوع سمت");
            entity.Property(e => e.SanaHasOrganizationChart).HasComment("آيا در ثنا چارت سازماني دارد؟");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاريخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره موبايل ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("کد سازمان ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام سازمان ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعيت ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفترخانه");
            entity.Property(e => e.Seri).HasComment("سري شناسنامه");
            entity.Property(e => e.SeriAlpha).HasComment("بخش حرفي شماره شناسنامه");
            entity.Property(e => e.Serial).HasComment("سريال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسيت");
            entity.Property(e => e.SharePart).HasComment("سهم از كل سهم خریداری شده");
            entity.Property(e => e.ShareText).HasComment("متن سهم");
            entity.Property(e => e.ShareTotal).HasComment("كل سهم خریداری شده");
            entity.Property(e => e.Tel).HasComment("تلفن");
            entity.Property(e => e.TfaRequired)
                .IsFixedLength()
                .HasComment("آيا کد دو عاملي اجباري است؟");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("وضعيت کد دو عاملي");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.DealsummaryPersonRelateType).WithMany(p => p.EstateTaxInquiryPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_TAX_INQUIRY_PERSON_RELATION_TYPE");

            entity.HasOne(d => d.EstateTaxInquiry).WithMany(p => p.EstateTaxInquiryPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATETAXINQUIRYPERSON_TAXINQUIRY");
        });

        modelBuilder.Entity<EstateTaxInquirySendedSm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ESTATE_TAX_INQUIRY_SENDED_SMS_ID");

            entity.ToTable("ESTATE_TAX_INQUIRY_SENDED_SMS", tb => tb.HasComment("پيامک ارسالي در مورد استعلام مالياتي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.EstateTaxInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Message).HasComment("پيام");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاريخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.SmsTrackingNo).HasComment("شماره رهگيري پيامک");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.EstateTaxInquiry).WithMany(p => p.EstateTaxInquirySendedSms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_TAX_INQUIRY_SENDED_SMS_ESTATE_TAX_INQUIRY");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.EstateTaxInquirySendedSms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESTATE_TAX_INQUIRY_SENDED_SMS_WORKFLOW_STATES");
        });

        modelBuilder.Entity<EstateTaxInquiryTransferType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_TRANSFER_TYPE", tb => tb.HasComment("موضوع نقل و انتقال"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxInquiryUsingType>(entity =>
        {
            entity.ToTable("ESTATE_TAX_INQUIRY_USING_TYPE", tb => tb.HasComment("نوع كاربری ملك"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.Type).HasComment("نوع");
        });

        modelBuilder.Entity<EstateTaxProvince>(entity =>
        {
            entity.ToTable("ESTATE_TAX_PROVINCE", tb => tb.HasComment("استان امور مالیاتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTaxUnit>(entity =>
        {
            entity.ToTable("ESTATE_TAX_UNIT", tb => tb.HasComment("اداره امور مالیاتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه در سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateTransitionType>(entity =>
        {
            entity.ToTable("ESTATE_TRANSITION_TYPE", tb => tb.HasComment("نوع انتقال ملك"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<EstateType>(entity =>
        {
            entity.ToTable("ESTATE_TYPE", tb => tb.HasComment("نوع ملك"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutiveAddressType>(entity =>
        {
            entity.ToTable("EXECUTIVE_ADDRESS_TYPE", tb => tb.HasComment("نوع نشانی اشخاص در سامانه اجرا"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutiveBindingSubjectType>(entity =>
        {
            entity.ToTable("EXECUTIVE_BINDING_SUBJECT_TYPE", tb => tb.HasComment("انواع موضوع لازم الاجرا در اجرائیات ثبت"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutiveFieldType>(entity =>
        {
            entity.ToTable("EXECUTIVE_FIELD_TYPE", tb => tb.HasComment("نوع اقلام اطلاعاتی در اجرا"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.ExecutiveGeneralFieldTypeId).HasComment("شناسه نوع كلی اقلام اطلاعاتی در اجرا");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.ExecutiveGeneralFieldType).WithMany(p => p.ExecutiveFieldTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_FIELD_TYPE_GENERAL_TYPE");
        });

        modelBuilder.Entity<ExecutiveGeneralFieldType>(entity =>
        {
            entity.ToTable("EXECUTIVE_GENERAL_FIELD_TYPE", tb => tb.HasComment("نوع كلی اقلام اطلاعاتی در اجرا"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutiveGeneralPersonPostType>(entity =>
        {
            entity.ToTable("EXECUTIVE_GENERAL_PERSON_POST_TYPE", tb => tb.HasComment("نوع كلی سمت اشخاص در اجرائیات ثبت"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutivePersonPostType>(entity =>
        {
            entity.ToTable("EXECUTIVE_PERSON_POST_TYPE", tb => tb.HasComment("سمت اشخاص در اجرائیات ثبت"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.ExecutiveGeneralPersonPostTypeId).HasComment("شناسه نوع كلی سمت اشخاص در اجرائیات ثبت");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.ExecutiveGeneralPersonPostType).WithMany(p => p.ExecutivePersonPostTypes).HasConstraintName("FK_EXECUTIVE_PERSON_POST_TYPE_GENERAL_TYPE");
        });

        modelBuilder.Entity<ExecutiveRequest>(entity =>
        {
            entity.ToTable("EXECUTIVE_REQUEST", tb => tb.HasComment("تقاضانامه اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.ApplicationReason).HasComment("دلیل درخواست");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.CurrencyTypeId).HasComment("شناسه واحدهای اندازه گیری - واحد پولی");
            entity.Property(e => e.DescriptionNotary).HasComment("توضیحات دفترخانه");
            entity.Property(e => e.DescriptionUnit).HasComment("هامش اداره اجرا");
            entity.Property(e => e.DocumentDigitalSign).HasComment("امضای الكترونیك");
            entity.Property(e => e.ExecutiveRequestOldId).HasComment("شناسه تقاضانامه قبلی");
            entity.Property(e => e.ExecutiveTypeId).HasComment("نوع اجرائیه");
            entity.Property(e => e.FactorDate).HasComment("تاریخ فاكتور");
            entity.Property(e => e.FactorNo).HasComment("شماره فاكتور");
            entity.Property(e => e.HowToPay).HasComment("شیوه پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsCorrectiveOfAnotherReq)
                .IsFixedLength()
                .HasComment("آیا این تقاضا، اصلاحیه تقاضای دیگری است؟");
            entity.Property(e => e.IsFinalVerificationVisited)
                .IsFixedLength()
                .HasComment("آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟");
            entity.Property(e => e.IsPayCostConfirmed)
                .IsFixedLength()
                .HasComment("آیا هزینه های ارائه خدمت پرداخت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.LegalText).HasComment("متن حقوقی");
            entity.Property(e => e.Modifier).HasComment("نام و نام خانوادگی آخرین اصلاح كننده");
            entity.Property(e => e.ModifyDate).HasComment("تاریخ آخرین اصلاح");
            entity.Property(e => e.ModifyTime).HasComment("زمان آخرین اصلاح");
            entity.Property(e => e.No).HasComment("شناسه یكتا");
            entity.Property(e => e.PaperCount).HasComment("تعداد اوراق");
            entity.Property(e => e.PayCostDate).HasComment("تاریخ پرداخت هزینه های ارائه خدمت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت هزینه های ارائه خدمت");
            entity.Property(e => e.PaymentType).HasComment("نوع پرداخت");
            entity.Property(e => e.Price).HasComment("مبلغ اجرائیه");
            entity.Property(e => e.ReceiptNo).HasComment("شناسه مرجع تراكنش");
            entity.Property(e => e.RequestDate)
                .IsFixedLength()
                .HasComment("تاریخ درخواست");
            entity.Property(e => e.SardaftarConfirmDate).HasComment("تاریخ تأیید سردفتر");
            entity.Property(e => e.SardaftarConfirmTime).HasComment("زمان تأیید سردفتر");
            entity.Property(e => e.SardaftarNameFamily).HasComment("نام و نام خانوادگی سردفتر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SignCertificateDn).HasComment("شناسه گواهی امضای الكترونیك");
            entity.Property(e => e.State).HasComment("شناسه وضعیت گردش موضوعات اطلاعاتی");
            entity.Property(e => e.SumPrices).HasComment("جمع هزینه های ارائه خدمت");
            entity.Property(e => e.Title).HasComment("عنوان تقاضای صدور  اجرائیه");
            entity.Property(e => e.UnitId).HasComment("شناسه واحد ثبتی دریافت كننده تقاضانامه اجرائیه");

            entity.HasOne(d => d.ExecutiveRequestOld).WithMany(p => p.InverseExecutiveRequestOld).HasConstraintName("FK_EXECUTIVE_REQUEST_EXC_REQ_OLD");

            entity.HasOne(d => d.ExecutiveType).WithMany(p => p.ExecutiveRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_REQUEST_EXECUTIVE_TYPE");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.ExecutiveRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_REQUEST_STATE");
        });

        modelBuilder.Entity<ExecutiveRequestBinding>(entity =>
        {
            entity.ToTable("EXECUTIVE_REQUEST_BINDING", tb => tb.HasComment("موضوعات لازم الاجرای تقاضانامه اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CurrencyTypeId).HasComment("شناسه واحد پولی");
            entity.Property(e => e.Description).HasComment("شرح");
            entity.Property(e => e.DurDate)
                .IsFixedLength()
                .HasComment("تاریخ مبنای محاسبه خسارت");
            entity.Property(e => e.ExecutiveBindingSubjectTypeId).HasComment("شناسه نوع موضوع لازم الاجرا");
            entity.Property(e => e.ExecutiveRequestId).HasComment("شناسه تقاضانامه اجرائیه");
            entity.Property(e => e.ExecutiveRequestScriptoriumId).HasComment("شناسه دفترخانه تقاضانامه اجرائیه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Price).HasComment("مبلغ");

            entity.HasOne(d => d.ExecutiveBindingSubjectType).WithMany(p => p.ExecutiveRequestBindings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_BINDING_BINDING_SUBJ");

            entity.HasOne(d => d.ExecutiveRequest).WithMany(p => p.ExecutiveRequestBindings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_BINDING_EXEC_REQ");
        });

        modelBuilder.Entity<ExecutiveRequestDocument>(entity =>
        {
            entity.ToTable("EXECUTIVE_REQUEST_DOCUMENT", tb => tb.HasComment("مستندات تقاضانامه اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.AttachmentTypeId).HasComment("ردیف نوع سند یا پیوست");
            entity.Property(e => e.BankId).HasComment("ردیف بانك");
            entity.Property(e => e.DocumentDate).HasComment("تاریخ سند مربوط");
            entity.Property(e => e.DocumentNo).HasComment("شماره سند مربوط");
            entity.Property(e => e.ExecutiveRequestId).HasComment("شناسه تقاضانامه اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.ExecutiveRequestScriptoriumId).HasComment("شناسه دفترخانه تقاضانامه اجرائیه");
            entity.Property(e => e.FieldDate1).HasComment("اولین تاریخ");
            entity.Property(e => e.FieldDate2).HasComment("دومین تاریخ");
            entity.Property(e => e.FieldDate3).HasComment("سومین تاریخ");
            entity.Property(e => e.FieldDesc1).HasComment("اولین شرح");
            entity.Property(e => e.FieldDesc2).HasComment("دومین شرح");
            entity.Property(e => e.FieldDesc3).HasComment("سومین شرح");
            entity.Property(e => e.FieldDesc4).HasComment("چهارمین شرح");
            entity.Property(e => e.FieldDesc5).HasComment("پنجمین شرح");
            entity.Property(e => e.FieldDesc6).HasComment("ششمین شرح");
            entity.Property(e => e.FieldDesc7).HasComment("هفتمین شرح");
            entity.Property(e => e.FieldValue1).HasComment("اولین مقدار");
            entity.Property(e => e.FieldValue2).HasComment("دومین مقدار");
            entity.Property(e => e.FieldValue3).HasComment("سومین مقدار");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MeasurementUnitTypeId).HasComment("ردیف انواع واحد اندازه گیری");

            entity.HasOne(d => d.ExecutiveRequest).WithMany(p => p.ExecutiveRequestDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_DOC_EXEC_REQ");
        });

        modelBuilder.Entity<ExecutiveRequestPerson>(entity =>
        {
            entity.ToTable("EXECUTIVE_REQUEST_PERSON", tb => tb.HasComment("اشخاص تقاضانامه اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد");
            entity.Property(e => e.BirthLocation).HasComment("محل تولد");
            entity.Property(e => e.CompanyRegisterDate).HasComment("تاریخ ثبت شخص حقوقی");
            entity.Property(e => e.CompanyRegisterLocation).HasComment("محل ثبت");
            entity.Property(e => e.CompanyRegisterNo).HasComment("شماره ثبت");
            entity.Property(e => e.CompanyType).HasComment("شناسه نوع شركت");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Email).HasComment("آدرس پست الكترونیكی");
            entity.Property(e => e.ExecutiveAddressTypeId).HasComment("نوع نشانی");
            entity.Property(e => e.ExecutivePersonPostTypeId).HasComment("شناسه سمت اشخاص در اجرائیات ثبت");
            entity.Property(e => e.ExecutiveRequestId).HasComment("شناسه تقاضانامه اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.Fax).HasComment("شماره فكس");
            entity.Property(e => e.HasSana).HasComment("آیا در سامانه ثنا حساب كاربری دارد؟");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آیا این شخص زنده است؟");
            entity.Property(e => e.IsFingerprintGotten)
                .IsFixedLength()
                .HasComment("آیا تصویر اثر انگشت شخص گرفته شده است؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .IsFixedLength()
                .HasComment("آیا این شخص اصیل است؟");
            entity.Property(e => e.IsRelated)
                .IsFixedLength()
                .HasComment("آیا این شخص وكیل است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟");
            entity.Property(e => e.LastLegalPaperDate).HasComment("تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.LegalPersonType)
                .IsFixedLength()
                .HasComment("نوع شخص حقوقی");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه ماهیت شخص حقوقی");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه نوع شخص حقوقی");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شناسه ملی");
            entity.Property(e => e.Nationality).HasComment("ملیت");
            entity.Property(e => e.NationalityId).HasComment("شناسه كشور تابعیت");
            entity.Property(e => e.PassportNo).HasComment("شماره گذرنامه");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("حقیقی است یا حقوقی؟");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.SabtahvalInquiryDate).HasComment("تاریخ مطابقت با ثبت احوال");
            entity.Property(e => e.SabtahvalInquiryTime).HasComment("زمان مطابقت با ثبت احوال");
            entity.Property(e => e.SanaHasOrganizationChart).HasComment("آیا شخص حقوقی، ساختار تشكیلات دارد؟ در سامانه ثنا");
            entity.Property(e => e.SanaInquiryDate).HasComment("تاریخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره تلفن همراه در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("كد شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه - بخش عددی");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حرفی");
            entity.Property(e => e.Serial).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");

            entity.HasOne(d => d.ExecutiveAddressType).WithMany(p => p.ExecutiveRequestPeople).HasConstraintName("FK_EXECUTIVE_REQUEST_PERSON_ADDRESS_TYPE");

            entity.HasOne(d => d.ExecutivePersonPostType).WithMany(p => p.ExecutiveRequestPeople).HasConstraintName("FK_EXEC_REQ_PRS_EXEC_PRS_TYP");

            entity.HasOne(d => d.ExecutiveRequest).WithMany(p => p.ExecutiveRequestPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_PRS_EXEC_REQ");
        });

        modelBuilder.Entity<ExecutiveRequestPersonRelated>(entity =>
        {
            entity.ToTable("EXECUTIVE_REQUEST_PERSON_RELATED", tb => tb.HasComment("وابستگی اشخاص تقاضانامه اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AgentDocumentCountryId).HasComment("شناسه كشوری كه وكالتنامه در آن تنظیم شده است");
            entity.Property(e => e.AgentDocumentDate)
                .IsFixedLength()
                .HasComment("تاریخ وكالتنامه");
            entity.Property(e => e.AgentDocumentId).HasComment("شناسه وكالتنامه");
            entity.Property(e => e.AgentDocumentIssuer).HasComment("مرجه صدور وكالتنامه");
            entity.Property(e => e.AgentDocumentNo).HasComment("شماره وكالتنامه");
            entity.Property(e => e.AgentDocumentScriptoriumId).HasComment("شناسه دفترخانه تنظیم كننده وكالتنامه");
            entity.Property(e => e.AgentDocumentSecretCode).HasComment("رمز تصدیق وكالتنامه");
            entity.Property(e => e.AgentPersonId).HasComment("شناسه شخص وابسته");
            entity.Property(e => e.AgentTypeId).HasComment("شناسه نوع وابستگی");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.ExecutiveRequestId).HasComment("شناسه تقاضانامه اجرائیه");
            entity.Property(e => e.ExecutiveRequestScriptoriumId).HasComment("شناسه دفترخانه تقاضانامه اجرائیه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAgentDocumentAbroad)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در مراجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.IsLawyer)
                .IsFixedLength()
                .HasComment("آیا وكیل دادگستری است؟");
            entity.Property(e => e.IsRelatedDocumentInSsar)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در سامانه ثبت الكترونیك اسناد ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MainPersonId).HasComment("شناسه شخص اصلی");
            entity.Property(e => e.ReliablePersonReasonId).HasComment("شناسه دلیل نیاز به معتمد");

            entity.HasOne(d => d.AgentPerson).WithMany(p => p.ExecutiveRequestPersonRelatedAgentPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_PRS_RELATED_AGNT_PRS");

            entity.HasOne(d => d.AgentType).WithMany(p => p.ExecutiveRequestPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_PRS_RELATED_AGENT_TYPE");

            entity.HasOne(d => d.ExecutiveRequest).WithMany(p => p.ExecutiveRequestPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_PRS_RELATED_EXEC_REQ");

            entity.HasOne(d => d.MainPerson).WithMany(p => p.ExecutiveRequestPersonRelatedMainPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_REQ_PRS_RELATED_CORE_PRS");

            entity.HasOne(d => d.ReliablePersonReason).WithMany(p => p.ExecutiveRequestPersonRelateds).HasConstraintName("FK_EXEC_REQ_PRS_REL_RELIABLE");
        });

        modelBuilder.Entity<ExecutiveSupport>(entity =>
        {
            entity.ToTable("EXECUTIVE_SUPPORT", tb => tb.HasComment("سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.Address).HasComment("نشانی اعلامی");
            entity.Property(e => e.AddressType).HasComment("نوع نشانی اعلامی");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.ChangedAddressPersonId).HasComment("شناسه شخصی كه آدرسش عوض می شود");
            entity.Property(e => e.DocumentDigitalSign).HasComment("امضای الكترونیك");
            entity.Property(e => e.ExecutiveCaseId).HasComment("كلید اصلی ركورد اجرائیه در سامانه اجرا");
            entity.Property(e => e.ExecutiveCaseNo).HasComment("شماره پرونده اجرائیه");
            entity.Property(e => e.ExecutiveDate)
                .IsFixedLength()
                .HasComment("تاریخ اجرائیه");
            entity.Property(e => e.ExecutiveInformation).HasComment("خلاصه اطلاعات اجرائیه");
            entity.Property(e => e.ExecutiveNo).HasComment("شماره اجرائیه");
            entity.Property(e => e.ExecutiveSupportTypeId).HasComment("شناسه نوع خدمات تبعی اجرائیه");
            entity.Property(e => e.ExecutiveTypeId).HasComment("شناسه نوع اجرائیه");
            entity.Property(e => e.FactorNo).HasComment("شماره فاكتور");
            entity.Property(e => e.FactordAte).HasComment("تاریخ فاكتور");
            entity.Property(e => e.HowToPay).HasComment("شیوه پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.InputLetterNoFromExecuteUnit).HasComment("شماره نامه وارده از طرف اداره اجرا");
            entity.Property(e => e.IsFinalVerificationVisited)
                .IsFixedLength()
                .HasComment("آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟");
            entity.Property(e => e.IsPayCostConfirmed)
                .IsFixedLength()
                .HasComment("آیا هزینه های ارائه خدمت پرداخت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Modifier).HasComment("نام و نام خانوادگی آخرین اصلاح كننده");
            entity.Property(e => e.ModifyDate).HasComment("تاریخ آخرین اصلاح");
            entity.Property(e => e.ModifyTime).HasComment("زمان آخرین اصلاح");
            entity.Property(e => e.No).HasComment("شناسه یكتا");
            entity.Property(e => e.OutputLetterNoFromExecuteUnit).HasComment("شماره نامه صادره از طرف اداره اجرا");
            entity.Property(e => e.PayCostDate).HasComment("تاریخ پرداخت هزینه های ارائه خدمت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت هزینه های ارائه خدمت");
            entity.Property(e => e.PaymentType).HasComment("نوع پرداخت");
            entity.Property(e => e.PostalCode).HasComment("كد پستی اعلامی");
            entity.Property(e => e.ReceiptNo).HasComment("شناسه مرجع تراكنش");
            entity.Property(e => e.ReplyDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ اداره اجرا");
            entity.Property(e => e.ReplyText).HasComment("متن پاسخ اداره اجرا");
            entity.Property(e => e.ReplyUnitId).HasComment("شناسه واحد اجرای پاسخ دهنده");
            entity.Property(e => e.RequestDate)
                .IsFixedLength()
                .HasComment("تاریخ درخواست");
            entity.Property(e => e.RequestText).HasComment("متن درخواست");
            entity.Property(e => e.RequesterId).HasComment("شناسه شخص درخواست كننده");
            entity.Property(e => e.SardaftarConfirmDate).HasComment("تاریخ تأیید سردفتر");
            entity.Property(e => e.SardaftarConfirmTime).HasComment("زمان تأیید سردفتر");
            entity.Property(e => e.SardaftarNameFamily).HasComment("نام و نام خانوادگی سردفتر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SignCertificateDn).HasComment("شناسه گواهی امضای الكترونیك");
            entity.Property(e => e.State).HasComment("وضعیت");
            entity.Property(e => e.SumPrices).HasComment("جمع هزینه های ارائه خدمت");
            entity.Property(e => e.UnitId).HasComment("شناسه واحد اجرای دریافت كننده درخواست");

            entity.HasOne(d => d.ChangedAddressPerson).WithMany(p => p.ExecutiveSupportChangedAddressPeople).HasConstraintName("FK_EXECUTIVE_SUPPORT_CHANGED_ADDRESS_PRS");

            entity.HasOne(d => d.ExecutiveSupportType).WithMany(p => p.ExecutiveSupports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_SUPPORT_EXE_SUP_TYPE");

            entity.HasOne(d => d.ExecutiveType).WithMany(p => p.ExecutiveSupports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_SUPPORT_EXECUTIVE_TYPE");

            entity.HasOne(d => d.Requester).WithMany(p => p.ExecutiveSupportRequesters).HasConstraintName("FK_EXECUTIVE_SUPPORT_REQUESTER");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.ExecutiveSupports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_SUPPORT_STATE");
        });

        modelBuilder.Entity<ExecutiveSupportAsset>(entity =>
        {
            entity.ToTable("EXECUTIVE_SUPPORT_ASSET", tb => tb.HasComment("اموال معرفی شده در سایر تقاضاهای مربوط به اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.BasicPersonId).HasComment("شناسه شخص از طرف");
            entity.Property(e => e.ExecutiveSupportId).HasComment("شناسه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.ExecutiveWealthTypeId).HasComment("شناسه نوع اموال در اجرا");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsInThirdPerson)
                .IsFixedLength()
                .HasComment("آیا مال نزد شخص ثالت است؟");
            entity.Property(e => e.KeepPlace).HasComment("محل نگهداری");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شماره ملی");
            entity.Property(e => e.OwnerPersonId).HasComment("شناسه شخص مالك");
            entity.Property(e => e.OwnerWealthType)
                .IsFixedLength()
                .HasComment("نوع مالك: متعهد/شخص ثالث");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.State).HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.WealthDescription).HasComment("شرح");

            entity.HasOne(d => d.ExecutiveSupport).WithMany(p => p.ExecutiveSupportAssets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_ASSET_EXEC_SUP");

            entity.HasOne(d => d.ExecutiveWealthType).WithMany(p => p.ExecutiveSupportAssets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_ASSET_EXEC_WEALTH_TYPE");

            entity.HasOne(d => d.OwnerPerson).WithMany(p => p.ExecutiveSupportAssets).HasConstraintName("FK_EXEC_SUP_ASSET_OWNER_PERSON");
        });

        modelBuilder.Entity<ExecutiveSupportAssetField>(entity =>
        {
            entity.ToTable("EXECUTIVE_SUPPORT_ASSET_FIELD", tb => tb.HasComment("اقلام اطلاعاتی اموال معرفی شده در سایر تقاضاهای مربوط به اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.ExecutiveSupportAssetId).HasComment("شناسه اموال معرفی شده در سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.ExecutiveWealthFieldTypeId).HasComment("شناسه انواع اقلام اطلاعاتی مربوط به یك مال");
            entity.Property(e => e.FieldDate).HasComment("تاریخ");
            entity.Property(e => e.FieldDescription).HasComment("شرح");
            entity.Property(e => e.FieldValue).HasComment("مقدار");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");

            entity.HasOne(d => d.ExecutiveSupportAsset).WithMany(p => p.ExecutiveSupportAssetFields)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_ASSET_FIELD__EXEC_SUP_ASSET");
        });

        modelBuilder.Entity<ExecutiveSupportPerson>(entity =>
        {
            entity.ToTable("EXECUTIVE_SUPPORT_PERSON", tb => tb.HasComment("اشخاص سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد");
            entity.Property(e => e.BirthLocation).HasComment("محل تولد");
            entity.Property(e => e.CompanyRegisterDate).HasComment("تاریخ ثبت شخص حقوقی");
            entity.Property(e => e.CompanyRegisterLocation).HasComment("محل ثبت");
            entity.Property(e => e.CompanyRegisterNo).HasComment("شماره ثبت");
            entity.Property(e => e.CompanyType).HasComment("شناسه نوع شركت");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Email).HasComment("آدرس پست الكترونیكی");
            entity.Property(e => e.ExecutiveAddressTypeId).HasComment("نوع نشانی");
            entity.Property(e => e.ExecutivePersonPostTypeId).HasComment("شناسه سمت اشخاص در اجرائیات ثبت");
            entity.Property(e => e.ExecutiveSupportId).HasComment("شناسه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.Fax).HasComment("شماره فكس");
            entity.Property(e => e.HasSana).HasComment("آیا در سامانه ثنا حساب كاربری دارد؟");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آیا این شخص زنده است؟");
            entity.Property(e => e.IsFingerprintGotten)
                .IsFixedLength()
                .HasComment("آیا تصویر اثر انگشت شخص گرفته شده است؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .IsFixedLength()
                .HasComment("آیا این شخص اصیل است؟");
            entity.Property(e => e.IsRelated)
                .IsFixedLength()
                .HasComment("آیا این شخص وكیل است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟");
            entity.Property(e => e.LastLegalPaperDate).HasComment("تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.LegalPersonType)
                .IsFixedLength()
                .HasComment("نوع شخص حقوقی");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه ماهیت شخص حقوقی");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه نوع شخص حقوقی");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شناسه ملی");
            entity.Property(e => e.Nationality).HasComment("ملیت");
            entity.Property(e => e.NationalityId).HasComment("شناسه كشور تابعیت");
            entity.Property(e => e.PassportNo).HasComment("شماره گذرنامه");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("حقیقی است یا حقوقی؟");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.SabtahvalInquiryDate).HasComment("تاریخ مطابقت با ثبت احوال");
            entity.Property(e => e.SabtahvalInquiryTime).HasComment("زمان مطابقت با ثبت احوال");
            entity.Property(e => e.SanaHasOrganizationChart).HasComment("آیا شخص حقوقی، ساختار تشكیلات دارد؟ در سامانه ثنا");
            entity.Property(e => e.SanaInquiryDate).HasComment("تاریخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره تلفن همراه در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("كد شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام شخص حقوقی در سامانه ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه - بخش عددی");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حرفی");
            entity.Property(e => e.Serial).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");

            entity.HasOne(d => d.ExecutiveAddressType).WithMany(p => p.ExecutiveSupportPeople).HasConstraintName("FK_EXECUTIVE_SUPPORT_PERSON_ADDRESS_TYPE");

            entity.HasOne(d => d.ExecutivePersonPostType).WithMany(p => p.ExecutiveSupportPeople).HasConstraintName("FK_EXEC_SUP_PRS_EXEC_PRS_TYP");

            entity.HasOne(d => d.ExecutiveSupport).WithMany(p => p.ExecutiveSupportPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_PRS_EXEC_REQ");
        });

        modelBuilder.Entity<ExecutiveSupportPersonRelated>(entity =>
        {
            entity.ToTable("EXECUTIVE_SUPPORT_PERSON_RELATED", tb => tb.HasComment("وابستگی اشخاص سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AgentDocumentCountryId).HasComment("شناسه كشوری كه وكالتنامه در آن تنظیم شده است");
            entity.Property(e => e.AgentDocumentDate)
                .IsFixedLength()
                .HasComment("تاریخ وكالتنامه");
            entity.Property(e => e.AgentDocumentId).HasComment("شناسه وكالتنامه");
            entity.Property(e => e.AgentDocumentIssuer).HasComment("مرجه صدور وكالتنامه");
            entity.Property(e => e.AgentDocumentNo).HasComment("شماره وكالتنامه");
            entity.Property(e => e.AgentDocumentScriptoriumId).HasComment("شناسه دفترخانه تنظیم كننده وكالتنامه");
            entity.Property(e => e.AgentDocumentSecretCode).HasComment("رمز تصدیق وكالتنامه");
            entity.Property(e => e.AgentPersonId).HasComment("شناسه شخص وابسته");
            entity.Property(e => e.AgentTypeId).HasComment("شناسه نوع وابستگی");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.ExecutiveSupportId).HasComment("شناسه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.ExecutiveSupportScriptoriumId).HasComment("شناسه دفترخانه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAgentDocumentAbroad)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در مراجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.IsLawyer)
                .IsFixedLength()
                .HasComment("آیا وكیل دادگستری است؟");
            entity.Property(e => e.IsRelatedDocumentInSsar)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در سامانه ثبت الكترونیك اسناد ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MainPersonId).HasComment("شناسه شخص اصلی");
            entity.Property(e => e.ReliablePersonReasonId).HasComment("شناسه دلیل نیاز به معتمد");

            entity.HasOne(d => d.AgentPerson).WithMany(p => p.ExecutiveSupportPersonRelatedAgentPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_PRS_RELATED_AGNT_PRS");

            entity.HasOne(d => d.AgentType).WithMany(p => p.ExecutiveSupportPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_PRS_RELATED_AGENT_TYPE");

            entity.HasOne(d => d.ExecutiveSupport).WithMany(p => p.ExecutiveSupportPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_PRS_RELATED_EXEC_REQ");

            entity.HasOne(d => d.MainPerson).WithMany(p => p.ExecutiveSupportPersonRelatedMainPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXEC_SUP_PRS_RELATED_CORE_PRS");

            entity.HasOne(d => d.ReliablePersonReason).WithMany(p => p.ExecutiveSupportPersonRelateds).HasConstraintName("FK_EXEC_SUP_PRS_REL_RELIABLE");
        });

        modelBuilder.Entity<ExecutiveSupportType>(entity =>
        {
            entity.ToTable("EXECUTIVE_SUPPORT_TYPE", tb => tb.HasComment("نوع خدمات تبعی اجرائیه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutiveType>(entity =>
        {
            entity.ToTable("EXECUTIVE_TYPE", tb => tb.HasComment("نوع اجرائیه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ExecutiveTypeAttachmentType>(entity =>
        {
            entity.ToTable("EXECUTIVE_TYPE_ATTACHMENT_TYPE", tb => tb.HasComment("انواع مستندات و پیوست ها مطرح در انواع مختلف اجرائیه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.AttachmentTypeId).HasComment("شناسه نوع پیوست");
            entity.Property(e => e.ExecutiveTypeId).HasComment("شناسه نوع اجرائیه");
            entity.Property(e => e.IsRequired)
                .IsFixedLength()
                .HasComment("آیا ورود اطلاعات این نوع پیوست اجباری است؟");

            entity.HasOne(d => d.ExecutiveType).WithMany(p => p.ExecutiveTypeAttachmentTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_TYPE_ATTACHMENT_TYPE_EXECUTIVE_TYPE");
        });

        modelBuilder.Entity<ExecutiveTypeExecutiveBindingSubjectType>(entity =>
        {
            entity.ToTable("EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE", tb => tb.HasComment("انواع موضوع لازم الاجرا در اجرائیات ثبت مطرح در انواع مختلف اجرائیه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ExecutiveBindingSubjectTypeId).HasComment("شناسه نوع موضوع لازم الاجرا در اجرائیات ثبت");
            entity.Property(e => e.ExecutiveTypeId).HasComment("شناسه نوع اجرائیه");
            entity.Property(e => e.IsRequired)
                .IsFixedLength()
                .HasComment("آیا ورود اطلاعات این نوع موضوع لازم الاجرا اجباری است؟");

            entity.HasOne(d => d.ExecutiveBindingSubjectType).WithMany(p => p.ExecutiveTypeExecutiveBindingSubjectTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE");

            entity.HasOne(d => d.ExecutiveType).WithMany(p => p.ExecutiveTypeExecutiveBindingSubjectTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE_EXECUTIVE_TYPE");
        });

        modelBuilder.Entity<ExecutiveTypeExecutivePersonPostType>(entity =>
        {
            entity.ToTable("EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE", tb => tb.HasComment("انواع سمت مطرح در انواع مختلف اجرائیه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ExecutivePersonPostTypeId).HasComment("شناسه نوع سمت اشخاص در اجرائیات ثبت");
            entity.Property(e => e.ExecutiveTypeId).HasComment("شناسه نوع اجرائیه");
            entity.Property(e => e.IsRequired)
                .IsFixedLength()
                .HasComment("آیا ورود اطلاعات این نوع سمت اجباری است؟");

            entity.HasOne(d => d.ExecutivePersonPostType).WithMany(p => p.ExecutiveTypeExecutivePersonPostTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE_EXECUTIVE_PERSON_POST_TYPE");

            entity.HasOne(d => d.ExecutiveType).WithMany(p => p.ExecutiveTypeExecutivePersonPostTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE_EXECUTIVE_TYPE");
        });

        modelBuilder.Entity<ExecutiveWealthFieldType>(entity =>
        {
            entity.ToTable("EXECUTIVE_WEALTH_FIELD_TYPE", tb => tb.HasComment("نوع اقلام اطلاعاتی مربوط به انواع اموال در اجرا"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.ExecutiveFieldTypeId).HasComment("شناسه نوع اقلام اطلاعاتی در اجرا");
            entity.Property(e => e.ExecutiveWealthTypeId).HasComment("شناسه نوع اموال در اجرا");
            entity.Property(e => e.IsRequired)
                .IsFixedLength()
                .HasComment("آیا اجباری است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.ExecutiveFieldType).WithMany(p => p.ExecutiveWealthFieldTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_WEALTH_FIELD_TYPE_FIELD_TYPE");

            entity.HasOne(d => d.ExecutiveWealthType).WithMany(p => p.ExecutiveWealthFieldTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXECUTIVE_WEALTH_FIELD_TYPE_WEALTH_TYPE");
        });

        modelBuilder.Entity<ExecutiveWealthType>(entity =>
        {
            entity.ToTable("EXECUTIVE_WEALTH_TYPE", tb => tb.HasComment("نوع اموال در اجرا"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
            entity.Property(e => e.WealthType).HasComment("منقول/غیرمنقول");
        });

        modelBuilder.Entity<ForestorgCity>(entity =>
        {
            entity.ToTable("FORESTORG_CITY", tb => tb.HasComment("شهرستان سازمان جنگل ها"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.AccountNo).HasComment("شماره حساب");
            entity.Property(e => e.BehdadNo).HasComment("شماره بهداد");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.ForestorgProvinceId).HasComment("ردیف استان");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.ShebaNo).HasComment("شماره شبا");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.ForestorgProvince).WithMany(p => p.ForestorgCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORG_CITY_PROVINCE");
        });

        modelBuilder.Entity<ForestorgInquiry>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY", tb => tb.HasComment("استعلام سازمان جنگل ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.DefectText).HasComment("متن");
            entity.Property(e => e.EstateAddress).HasComment("آدرس ملك");
            entity.Property(e => e.EstateArea).HasComment("مساحت ملك");
            entity.Property(e => e.EstateBasic).HasComment("پلاك اصلی ملك");
            entity.Property(e => e.EstateInquiryId).HasComment("ردیف استعلام ملك");
            entity.Property(e => e.EstatePostCode).HasComment("كد پستی ملك");
            entity.Property(e => e.EstateSecondary).HasComment("پلاك فرعی ملك");
            entity.Property(e => e.EstateSeparate).HasComment("مفروز و مجزا شده از");
            entity.Property(e => e.ForestorgCityId).HasComment("ردیف شهرستان");
            entity.Property(e => e.ForestorgProvinceId).HasComment("ردیف استان");
            entity.Property(e => e.ForestorgSectionId).HasComment("ردیف بخش");
            entity.Property(e => e.HowToPay).HasComment("روش پرداخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IncommingLetterNo).HasComment("شماره نامه وارده");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آيا هزينه ها پرداخت شده است؟");
            entity.Property(e => e.Isactive)
                .IsFixedLength()
                .HasComment("فعال/غیر فعال");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.LetterDate).HasComment("تاریخ نامه");
            entity.Property(e => e.LetterNo).HasComment("شماره نامه");
            entity.Property(e => e.No).HasComment("شماره");
            entity.Property(e => e.PayCostDate)
                .IsFixedLength()
                .HasComment("تاريخ پرداخت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت");
            entity.Property(e => e.PaymentType).HasComment("شيوه پرداخت");
            entity.Property(e => e.ReceiptNo).HasComment("شماره فاکتور");
            entity.Property(e => e.ResponseDateTime).HasComment("تاريخ و زمان پاسخ");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفتر خانه");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال");
            entity.Property(e => e.SerialNo).HasComment("شماره سریال");
            entity.Property(e => e.SumPrices).HasComment("مبلغ قابل پرداخت");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.TrackingCode).HasComment("كد رهگیری");
            entity.Property(e => e.UniqueNo).HasComment("شناسه یكتای جدید");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه گردش کار");

            entity.HasOne(d => d.EstateInquiry).WithMany(p => p.ForestorgInquiries).HasConstraintName("FK_FORESTORG_INQ_ESTATE_INQ");

            entity.HasOne(d => d.ForestorgCity).WithMany(p => p.ForestorgInquiries).HasConstraintName("FK_FORESTORG_INQ_CITY");

            entity.HasOne(d => d.ForestorgProvince).WithMany(p => p.ForestorgInquiries).HasConstraintName("FK_FORESTORG_INQ_PROVINCE");

            entity.HasOne(d => d.ForestorgSection).WithMany(p => p.ForestorgInquiries).HasConstraintName("FK_FORESTORG_INQ_SECTION");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.ForestorgInquiries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORGINQUIRY_WORKFLOWSTATES");
        });

        modelBuilder.Entity<ForestorgInquiryFile>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY_FILE", tb => tb.HasComment("فایل مرتبط با استعلام جنگل ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AttachmentNo).HasComment("شماره");
            entity.Property(e => e.AttachmentTitle).HasComment("عنوان");
            entity.Property(e => e.Description).HasComment("شرح");
            entity.Property(e => e.FileContent).HasComment("محتویات فایل");
            entity.Property(e => e.FileExtention).HasComment("نوع فایل");
            entity.Property(e => e.ForestorgInquiryId).HasComment("ردیف استعلام");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.ForestorgInquiry).WithMany(p => p.ForestorgInquiryFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORSTORG_INQ_FILE_INQUIRY");
        });

        modelBuilder.Entity<ForestorgInquiryPerson>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY_PERSON", tb => tb.HasComment("اشخاص مرتبط با استعلام سازمان جنگل ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس");
            entity.Property(e => e.BirthDate)
                .IsFixedLength()
                .HasComment("تاریخ تولد/ثبت");
            entity.Property(e => e.BirthPlaceId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.CityId).HasComment("شناسه شهر");
            entity.Property(e => e.CompanyTypeId).HasComment("شناسه نوع شرکت");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.Email).HasComment("پست الکترونيک");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.Fax).HasComment("فکس");
            entity.Property(e => e.ForiegnBirthPlace).HasComment("محل تولد خارجي");
            entity.Property(e => e.ForiegnIssuePlace).HasComment("محل صدور خارجي");
            entity.Property(e => e.ForstorgInquiryId).HasComment("ردیف استعلام");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("كارت ملی هوشمند دارد؟");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه/ثبت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آيا زنده است؟");
            entity.Property(e => e.IsForeignerssysChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام از سامانه اتباع خارجی انجام شده است؟");
            entity.Property(e => e.IsForeignerssysCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با ساماه اتباع خارجی تطابق دارد؟");
            entity.Property(e => e.IsIlencChecked)
                .IsFixedLength()
                .HasComment("ایا استعلام از سامانه اشخاص حقوقی برای این شخص انجام شده است؟");
            entity.Property(e => e.IsIlencCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با سامانه اشخاص حقوقی تطابق دارد؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .HasDefaultValueSql("'1'                   ")
                .IsFixedLength()
                .HasComment("اصيل است؟");
            entity.Property(e => e.IsRelated)
                .HasDefaultValueSql("'2'                   ")
                .IsFixedLength()
                .HasComment("وابسته است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با ثبت احوال تطابق دارد؟");
            entity.Property(e => e.IssuePlaceId).HasComment("شناسه محل صدور");
            entity.Property(e => e.LastLegalPaperDate)
                .IsFixedLength()
                .HasComment("تاريخ آخرين روزنامه رسمي");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرين روزنامه رسمي");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.MobileNo).HasComment("شماره موبایل");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعيت مالکيت خط موبايل با شاهکار");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت كارت ملی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalityCode).HasComment("شماره/شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.PostCode).HasComment("كد پستی");
            entity.Property(e => e.SanaHasOrganizationChart).HasComment("آيا در ثنا چارت سازماني دارد؟");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاريخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره موبايل ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("کد سازمان ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام سازمان ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعیت ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Seri).HasComment("سري شناسنامه");
            entity.Property(e => e.SeriAlpha).HasComment("بخش حرفي شماره شناسنامه");
            entity.Property(e => e.Serial).HasComment("سريال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسيت");
            entity.Property(e => e.TelNo).HasComment("شماره تلفن");
            entity.Property(e => e.TfaRequired)
                .IsFixedLength()
                .HasComment("رمز دو عاملی نیاز است؟");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("وضعیت رمز دوعاملی؟");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.ForstorgInquiry).WithMany(p => p.ForestorgInquiryPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORSTORG_INQ_PERSON_INQUIRY");
        });

        modelBuilder.Entity<ForestorgInquiryPoint>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY_POINT", tb => tb.HasComment("مختصات ملك"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ForestorgInquiryId).HasComment("ردیف استعلام");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
            entity.Property(e => e.X).HasComment("ایكس");
            entity.Property(e => e.Y).HasComment("ایگرگ");
            entity.Property(e => e.Zone).HasComment("زون");

            entity.HasOne(d => d.ForestorgInquiry).WithMany(p => p.ForestorgInquiryPoints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORG_INQ_POINT_INQUIRY");
        });

        modelBuilder.Entity<ForestorgInquiryResponse>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY_RESPONSE", tb => tb.HasComment("پاسخ استعلام سازمان جنگل ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ArchiveMediaFileId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاريخ ايجاد");
            entity.Property(e => e.CreateTime).HasComment("زمان ايجاد");
            entity.Property(e => e.ForestOrganizationInquiryId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ForestorgCityId).HasComment("ردیف شهرستان");
            entity.Property(e => e.ForestorgCreateDate)
                .IsFixedLength()
                .HasComment("تاريخ ايجاد");
            entity.Property(e => e.ForestorgCreateTime).HasComment("زمان ايجاد");
            entity.Property(e => e.ForestorgId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ForestorgInquiryId).HasComment("ردیف استعلام");
            entity.Property(e => e.ForestorgModifyDate)
                .IsFixedLength()
                .HasComment("تاريخ اصلاح");
            entity.Property(e => e.ForestorgModifyTime).HasComment("زمان اصلاح");
            entity.Property(e => e.ForestorgModifyUser).HasComment("کاربر اصلاح کننده");
            entity.Property(e => e.ForestorgProvinceId).HasComment("ردیف استان");
            entity.Property(e => e.ForestorgSectionId).HasComment("ردیف بخش");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.ResponsePdfFile).HasComment("فایل پی دی اف پاسخ");
            entity.Property(e => e.ResponseTypeId).HasComment("ردیف نوع پاسخ");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.ShapeArea).HasComment("مساحت شکل");
            entity.Property(e => e.ShapeType).HasComment("نوع شکل");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.ForestorgCity).WithMany(p => p.ForestorgInquiryResponses).HasConstraintName("FK_FORESTORG_INQ_RES_CITY");

            entity.HasOne(d => d.ForestorgInquiry).WithMany(p => p.ForestorgInquiryResponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORG_INQ_RES_INQUIRY");

            entity.HasOne(d => d.ForestorgProvince).WithMany(p => p.ForestorgInquiryResponses).HasConstraintName("FK_FORESTORG_INQ_RES_PROVINCE");

            entity.HasOne(d => d.ForestorgSection).WithMany(p => p.ForestorgInquiryResponses).HasConstraintName("FK_FORESTORG_INQ_RES_SECTION");

            entity.HasOne(d => d.ResponseType).WithMany(p => p.ForestorgInquiryResponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORG_INQ_RES_RESTYPE");
        });

        modelBuilder.Entity<ForestorgInquiryResponsetype>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY_RESPONSETYPE", tb => tb.HasComment("نوع پاسخ استعلام سازمان جنگل ها"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ForestorgInquiryStatus>(entity =>
        {
            entity.ToTable("FORESTORG_INQUIRY_STATUS", tb => tb.HasComment("وضعیت استعلام سازمان جنگل ها"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ForestorgProvince>(entity =>
        {
            entity.ToTable("FORESTORG_PROVINCE", tb => tb.HasComment("استان سازمان جنگل ها"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ForestorgResponsepoint>(entity =>
        {
            entity.ToTable("FORESTORG_RESPONSEPOINT", tb => tb.HasComment("مختصات نقاط پاسخ استعلام جنگل ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ForestorgInquiryResponseId).HasComment("ردیف پاسخ استعلام");
            entity.Property(e => e.GroupCode).HasComment("كد دسته بندی");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.Lat).HasComment("Lat");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.Lng).HasComment("Lng");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.ForestorgInquiryResponse).WithMany(p => p.ForestorgResponsepoints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORG_RESPONSEPOINT_FORESTORG_INQUIRY_RESPONSE");
        });

        modelBuilder.Entity<ForestorgSection>(entity =>
        {
            entity.ToTable("FORESTORG_SECTION", tb => tb.HasComment("بخش های سازمان جنگل ها"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.ForestorgCityId).HasComment("ردیف شهرستان");
            entity.Property(e => e.LegacyId).HasComment("شناسه سیستم قدیم");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.ForestorgCity).WithMany(p => p.ForestorgSections)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORESTORG_SECTION_CITY");
        });

        modelBuilder.Entity<GeneralOrganization>(entity =>
        {
            entity.ToTable("GENERAL_ORGANIZATION", tb => tb.HasComment("سازمان های عمومی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("کلید رکورد در سامانۀ قدیمی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.No).HasComment("شماره");
            entity.Property(e => e.OrganizationId).HasComment("شناسه معادل در سازمان اطلاعات پایه");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Type).HasComment("نوع");
        });

        modelBuilder.Entity<ImportantActionType>(entity =>
        {
            entity.ToTable("IMPORTANT_ACTION_TYPE", tb => tb.HasComment("انواع فعالیت های حساس و مهم در روند ثبت الكترونیك اسناد"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.IsIllegalInHolidays)
                .IsFixedLength()
                .HasComment("آیا انجام این فعالیت در روزهای تعطیل غیرمجاز است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ImportantActionTypeIllegalTime>(entity =>
        {
            entity.ToTable("IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES", tb => tb.HasComment("زمان هایی كه انجام انواع فعالیت های حساس و مهم در روند ثبت الكترونیك اسناد غیرمجاز است"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.DayOfWeek)
                .IsFixedLength()
                .HasComment("روز هفته");
            entity.Property(e => e.FromDate)
                .IsFixedLength()
                .HasComment("از تاریخ");
            entity.Property(e => e.FromTime).HasComment("زمان شروع بازه غیرمجاز");
            entity.Property(e => e.ImportantActionTypeId).HasComment("شناسه نوع فعالیت حساس و مهم در روند ثبت الكترونیك اسناد");
            entity.Property(e => e.LegacyId).HasComment("شناسه ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OrganizationId).HasComment("شناسه واحدثبتی یا دفترخانه");
            entity.Property(e => e.ToDate)
                .IsFixedLength()
                .HasComment("تا تاریخ");
            entity.Property(e => e.ToTime).HasComment("زمان پایان بازه غیرمجاز");

            entity.HasOne(d => d.ImportantActionType).WithMany(p => p.ImportantActionTypeIllegalTimes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES_IMPORTANT_ACTION_TYPE");
        });

        modelBuilder.Entity<InquiryFromUnit>(entity =>
        {
            entity.ToTable("INQUIRY_FROM_UNIT", tb => tb.HasComment("استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض پرداخت");
            entity.Property(e => e.DocumentDigitalSign).HasComment("امضای الكترونیك سردفتر");
            entity.Property(e => e.FactorDate)
                .IsFixedLength()
                .HasComment("تاریخ تراكنش/فیش");
            entity.Property(e => e.FactorNo).HasComment("شماره تراكنش/فیش");
            entity.Property(e => e.GincomingletterId).HasComment("كلید اصلی پاسخ استعلام دفترخانه از واحد ثبتی");
            entity.Property(e => e.GoutgoingletterId).HasComment("كلید اصلی استعلام دفترخانه از واحد ثبتی");
            entity.Property(e => e.HowToPay).HasComment("نحوه پرداخت");
            entity.Property(e => e.InquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام");
            entity.Property(e => e.InquiryFromUnitTypeId).HasComment("شناسه نوع استعلام");
            entity.Property(e => e.InquiryNo).HasComment("شماره استعلام");
            entity.Property(e => e.InquiryText).HasComment("متن استعلام");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آیا همه هزینه های قانونی دریافت شده است؟");
            entity.Property(e => e.IsFinalVerificationVisited)
                .IsFixedLength()
                .HasComment("آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟");
            entity.Property(e => e.ItemDescription).HasComment("كالا");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Modifier).HasComment("نام و نام خانوادگی آخرین اصلاح كننده");
            entity.Property(e => e.ModifyDate)
                .IsFixedLength()
                .HasComment("تاریخ آخرین ویرایش");
            entity.Property(e => e.ModifyTime).HasComment("زمان آخرین ویرایش");
            entity.Property(e => e.PayCostDate)
                .IsFixedLength()
                .HasComment("تاریخ پرداخت هزینه ها");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت هزینه ها");
            entity.Property(e => e.PayType).HasComment("شیوه پرداخت هزینه ها- بانك؛ابزار");
            entity.Property(e => e.ReceiptNo).HasComment("شماره مرجع تراكنش یا شناسه پرداخت");
            entity.Property(e => e.RegisteruserhistoryId).HasComment("كلید اصلی سابقه تنظیمات سمت كاربر");
            entity.Property(e => e.RelatedInquiryFromUnitId).HasComment("شناسه استعلام قبلی");
            entity.Property(e => e.ReplyDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ استعلام");
            entity.Property(e => e.ReplyText).HasComment("متن پاسخ");
            entity.Property(e => e.ResponseData).HasComment("اطلاعات پاسخ");
            entity.Property(e => e.SardaftarConfirmDate)
                .IsFixedLength()
                .HasComment("تاریخ تایید سردفتر");
            entity.Property(e => e.SardaftarConfirmTime).HasComment("زمان تایید سردفتر");
            entity.Property(e => e.SardaftarNameFamily).HasComment("نام و نام خانوادگی سردفتر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.Sheba).HasComment("شبا واریز حقوق دولتی");
            entity.Property(e => e.SignCertificateDn).HasComment("گواهی امضای الكترونیك مورد استفاده برای امضای سردفتر");
            entity.Property(e => e.State).HasComment("وضعیت");
            entity.Property(e => e.StatementNo).HasComment("شماره اظهارنامه");
            entity.Property(e => e.SumPrices).HasComment("مبلغ لازم به پرداخت");
            entity.Property(e => e.UnitId).HasComment("شناسه واحد ثبتی استعلام شونده");
            entity.Property(e => e.Verhoeff).HasComment("كد ورهوف واریز حقوق دولتی");

            entity.HasOne(d => d.InquiryFromUnitType).WithMany(p => p.InquiryFromUnits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INQUIRY_FROM_UNIT_INQUIRY_FROM_UNIT_TYPE");

            entity.HasOne(d => d.RelatedInquiryFromUnit).WithMany(p => p.InverseRelatedInquiryFromUnit).HasConstraintName("FK_INQUIRY_FROM_UNIT_RELATED_INQUIRY_FROM_UNIT");
        });

        modelBuilder.Entity<InquiryFromUnitPerson>(entity =>
        {
            entity.ToTable("INQUIRY_FROM_UNIT_PERSON", tb => tb.HasComment("اشخاص استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد");
            entity.Property(e => e.CompanyRegisterDate).HasComment("تاریخ ثبت");
            entity.Property(e => e.CompanyRegisterLocation).HasComment("محل ثبت");
            entity.Property(e => e.CompanyRegisterNo).HasComment("شماره ثبت");
            entity.Property(e => e.CompanyType)
                .IsFixedLength()
                .HasComment("نوع شركت");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Email).HasComment("آدرس پست الكترونیكی");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه");
            entity.Property(e => e.InquiryFromUnitId).HasComment("شناسه استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آیا این شخص زنده است؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .IsFixedLength()
                .HasComment("آیا این شخص اصیل است؟");
            entity.Property(e => e.IsRelated)
                .IsFixedLength()
                .HasComment("آیا این شخص وكیل یا نماینده اشخاص دیگری است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟");
            entity.Property(e => e.LastLegalPaperDate).HasComment("تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.LegalPersonType)
                .IsFixedLength()
                .HasComment("نوع شخص حقوقی");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه ماهیت شخص حقوقی");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه نوع شخص حقوقی");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("شناسه كشور تابعیت");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("حقیقی است یا حقوقی؟");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه - بخش عددی");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حرفی");
            entity.Property(e => e.Serial).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");

            entity.HasOne(d => d.InquiryFromUnit).WithMany(p => p.InquiryFromUnitPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INQUIRY_FROM_UNIT_PERSON_INQUIRY_FROM_UNIT");
        });

        modelBuilder.Entity<InquiryFromUnitType>(entity =>
        {
            entity.ToTable("INQUIRY_FROM_UNIT_TYPE", tb => tb.HasComment("نوع استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<InquiryMartyrLog>(entity =>
        {
            entity.ToTable("INQUIRY_MARTYR_LOG", tb => tb.HasComment("لاگ استعلام بنياد شهيد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.FormId).HasComment("شناسه فرم فراخوانی كننده سرویس");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsMartyrIncluded)
                .IsFixedLength()
                .HasComment("آیا شخص مشمول استفاده از تخفیف بنیاد شهید هست؟");
            entity.Property(e => e.MartyrCode).HasComment("شناسه منحصربفرد شخص متقاضی تخفیف حق الثبت مخصوص ایثارگران نزد بنیاد شهید");
            entity.Property(e => e.MartyrDescription).HasComment("توضیحات مربوط به استعلام وضعیت ایثارگری از بنیاد شهید برای شخص متقاضی تخفیف حق الثبت مخصوص ایثارگران");
            entity.Property(e => e.MartyrInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام از بنیاد شهید در مورد وضعیت ایثارگری برای متقاضی تخفیف حق الثبت مخصوص ایثارگران");
            entity.Property(e => e.MartyrInquiryTime).HasComment("زمان استعلام از بنیاد شهید در مورد وضعیت ایثارگری برای متقاضی تخفیف حق الثبت مخصوص ایثارگران");
            entity.Property(e => e.NationalNo).HasComment("شماره ملی متقاضی");
            entity.Property(e => e.ObjectId).HasComment("شناسه كلاس اصلی فراخوانی كننده سرویس");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
        });

        modelBuilder.Entity<InquiryMocLog>(entity =>
        {
            entity.ToTable("INQUIRY_MOC_LOG", tb => tb.HasComment("لاگ سرويس ام او سي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ActionDate)
                .IsFixedLength()
                .HasComment("تاريخ عمليات");
            entity.Property(e => e.ActionName).HasComment("نام عمليات");
            entity.Property(e => e.ActionResults).HasComment("نتايج عمليات");
            entity.Property(e => e.ActionTime).HasComment("زمان عمليات");
            entity.Property(e => e.ActionType).HasComment("نوع عمليات");
            entity.Property(e => e.ErrorCode).HasComment("کد خطا");
            entity.Property(e => e.ErrorText).HasComment("متن خطا");
            entity.Property(e => e.FaceImage).HasComment("تصوير شخص");
            entity.Property(e => e.FormId).HasComment("شناسه فرم");
            entity.Property(e => e.Ilm)
                .HasDefaultValueSql("1                     ")
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.LegacyId).HasComment("کليد اصلي رکورد در سامانه قبلي");
            entity.Property(e => e.NationalNo).HasComment("شماره ملي");
            entity.Property(e => e.ObjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OtherInformation).HasComment("ساير اطلاعات");
            entity.Property(e => e.PersonFingerTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PersonFingerprintId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PersonFingerprintUseCaseId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecievedPacket).HasComment("بسته دريافتي");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.SentPacket).HasComment("بسته ارسالي");
            entity.Property(e => e.SerialNo).HasComment("سريال شناسنامه");

            entity.HasOne(d => d.PersonFingerType).WithMany(p => p.InquiryMocLogs).HasConstraintName("FK_INQUIRY_MOC_LOG_PERSON_FINGER_TYPE");

            entity.HasOne(d => d.PersonFingerprint).WithMany(p => p.InquiryMocLogs).HasConstraintName("FK_PERSON_FINGER_TYPE_PERSON_FINGERPRINT");

            entity.HasOne(d => d.PersonFingerprintUseCase).WithMany(p => p.InquiryMocLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INQUIRY_MOC_LOG_PERSON_FINGERPRINT_USE_CASE");
        });

        modelBuilder.Entity<InquirySabteahvalLog>(entity =>
        {
            entity.ToTable("INQUIRY_SABTEAHVAL_LOG", tb => tb.HasComment("لاگ ثبت احوال"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.FormId).HasComment("شناسه فرم");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آيا زنده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آيا اطلاعات شخص با ثبت احوال تطابق دارد؟");
            entity.Property(e => e.ObjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.SabteahvalInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام ثبت احوال");
            entity.Property(e => e.SabteahvalInquiryTime).HasComment("زمان استعلام ثبت احوال");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
        });

        modelBuilder.Entity<InquirySanaLog>(entity =>
        {
            entity.ToTable("INQUIRY_SANA_LOG", tb => tb.HasComment("لاگ ثنا"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.FormId).HasComment("شناسه فرم");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.ObjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.SanaHasOrganizationChart)
                .IsFixedLength()
                .HasComment("آيا در ثنا چارت سازماني دارد؟");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره تلفن همراه در سامانه ثنا");
            entity.Property(e => e.SanaNotificationCode).HasComment("كد رهگیری ارسال پیام به كارتابل ثنا");
            entity.Property(e => e.SanaNotificationDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال پیام به كارتابل ثنا");
            entity.Property(e => e.SanaNotificationTime).HasComment("زمان ارسال پیام به كارتابل ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("کد سازمان ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام سازمان ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعيت ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
        });

        modelBuilder.Entity<InquiryTfaLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("INQUIRY_TFA_LOG_pk");

            entity.ToTable("INQUIRY_TFA_LOG", tb => tb.HasComment("شناسه"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.FormId).HasComment("شناسه فرم");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعيت مالکيت خط موبايل با شاهکار");
            entity.Property(e => e.ObjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.TfaSendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال پیامك عامل دوم");
            entity.Property(e => e.TfaSendTime).HasComment("زمان ارسال پیامك عامل دوم");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("وضعيت کد دو عاملي");
            entity.Property(e => e.TfaValidateDate)
                .IsFixedLength()
                .HasComment("تاریخ اعتبارسنجی عامل دوم اعلام شده توسط شخص");
            entity.Property(e => e.TfaValidateTime).HasComment("زمان اعتبارسنجی عامل دوم اعلام شده توسط شخص");
            entity.Property(e => e.TfaValue).HasComment("مقدار عامل دوم");
        });

        modelBuilder.Entity<OtherPayment>(entity =>
        {
            entity.ToTable("OTHER_PAYMENTS", tb => tb.HasComment("سایر پرداخت های صورت گرفته در دفترخانه ها كه مربوط به مواردی نظیر اسناد، گواهی امضاء و استعلام نیستند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" ردیف");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض پرداخت");
            entity.Property(e => e.CompanyName).HasComment("نام شركت برای استعلام ثبت شركت ها");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد ركورد");
            entity.Property(e => e.CreateTime).HasComment("زمان ایجاد ركورد");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.EstateAddress).HasComment("نشانی ملك جاری");
            entity.Property(e => e.EstateOwnerNameFamily).HasComment("نام و نام خانوادگی مالك (مالكین) ملك جاری");
            entity.Property(e => e.EstateOwnerNationalNo).HasComment("شماره ملی مالك (مالكین) ملك جاری");
            entity.Property(e => e.FactorDate)
                .IsFixedLength()
                .HasComment("تاریخ تراكنش");
            entity.Property(e => e.FactorNo).HasComment("شماره تراكنش");
            entity.Property(e => e.Fee).HasComment("مبلغ واحد");
            entity.Property(e => e.GeoLocationId).HasComment("ردیف شهر ملك جاری");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آیا همه هزینه های قانونی دریافت شده است؟");
            entity.Property(e => e.ItemCount).HasComment("تعداد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.NationalNo).HasComment("شماره پرونده");
            entity.Property(e => e.OtherPaymentsTypeId).HasComment("شناسه ركورد موضوع پرداخت");
            entity.Property(e => e.PayCostDate).HasComment("تاریخ پرداخت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت");
            entity.Property(e => e.PayType).HasComment("شیوه پرداخت هزینه ها- بانك؛ابزار");
            entity.Property(e => e.PlaqueNo).HasComment("شماره پلاك ثبتی");
            entity.Property(e => e.ReceiptNo).HasComment("شماره مرجع تراكنش یا شناسه پرداخت");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف دفترخانه");
            entity.Property(e => e.SumPrices).HasComment("كل مبلغ لازم به پرداخت");
            entity.Property(e => e.UnitId).HasComment("ردیف حوزه ثبتی ملك جاری");

            entity.HasOne(d => d.OtherPaymentsType).WithMany(p => p.OtherPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTHER_PAYMENTS_OTHER_PAYMENTS_TYPE");
        });

        modelBuilder.Entity<OtherPaymentsType>(entity =>
        {
            entity.ToTable("OTHER_PAYMENTS_TYPE", tb => tb.HasComment("انواع سایر پرداخت های صورت گرفته در دفترخانه ها كه مربوط به مواردی نظیر اسناد، گواهی امضاء و استعلام نیستند"));

            entity.Property(e => e.Id).HasComment(" ردیف");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.Fee).HasComment("قیمت واحد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<PersonFingerType>(entity =>
        {
            entity.ToTable("PERSON_FINGER_TYPE", tb => tb.HasComment("تعریف انگشتان"));

            entity.Property(e => e.Id).HasComment("كلید اصلی");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.SabtahvalCode).HasComment("كد انگشت در سازمان ثبت احوال");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<PersonFingerprint>(entity =>
        {
            entity.ToTable("PERSON_FINGERPRINT", tb => tb.HasComment("اثر انگشت های اخذ شده"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.FingerprintFeatures).HasComment("فیچرهای استخراج شده از اثر انگشت");
            entity.Property(e => e.FingerprintGetDate)
                .IsFixedLength()
                .HasComment("تاریخ اخذ اثر انگشت");
            entity.Property(e => e.FingerprintGetTime).HasComment("زمان اخذ اثر انگشت");
            entity.Property(e => e.FingerprintImageFile).HasComment("محتوای فایل تصویر اثر انگشت");
            entity.Property(e => e.FingerprintImageHeight).HasComment("ارتفاع تصویر خام اثر انگشت");
            entity.Property(e => e.FingerprintImageType).HasComment("پسوند فایل تصویر اثر انگشت");
            entity.Property(e => e.FingerprintImageWidth).HasComment("عرض تصویر خام اثر انگشت");
            entity.Property(e => e.FingerprintRawImage).HasComment("تصویر خام اثر انگشت");
            entity.Property(e => e.FingerprintScannerDeviceType).HasComment("نوع دستگاه اخذ اثر انگشت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsDeleted)
                .IsFixedLength()
                .HasComment("آیا این اثر انگشت، حذف شده بحساب می آید؟");
            entity.Property(e => e.IsRemoteRequest)
                .IsFixedLength()
                .HasComment("آیا درخواست مرتبط بصورت غیرحضوری ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MocDescription).HasComment("توضیحات تطابق اثر انگشت با كارت ملی هوشمند (MOC)");
            entity.Property(e => e.MocIsRequired)
                .IsFixedLength()
                .HasComment("آیا تطابق اثر انگشت با كارت ملی هوشمند (MOC) ضروری است؟");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت تطابق اثر انگشت با كارت ملی هوشمند (MOC)");
            entity.Property(e => e.NameFamily).HasComment("نام و نام خانوادگی شخص");
            entity.Property(e => e.OrganizationId).HasComment("شناسه دفترخانه یا واحد ثبتی");
            entity.Property(e => e.PersonFingerTypeId).HasComment("شناسه انگشتی كه اثر آن گرفته شده است");
            entity.Property(e => e.PersonFingerprintUseCaseId).HasComment("شناسه كاربردی كه اثر انگشت برای آن اخذ شده است");
            entity.Property(e => e.PersonNationalNo)
                .IsFixedLength()
                .HasComment("شماره ملی شخص");
            entity.Property(e => e.RecordDate).HasComment("تاریخ ركورد به میلادی");
            entity.Property(e => e.SmartCardRequestDate)
                .IsFixedLength()
                .HasComment("تاریخ استعلام كارت ملی هوشمند");
            entity.Property(e => e.SmartCardRequestTime).HasComment("زمان استعلام كارت ملی هوشمند");
            entity.Property(e => e.SmartCardResponseDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ استعلام كارت ملی هوشمند");
            entity.Property(e => e.SmartCardResponseTime).HasComment("زمان پاسخ استعلام كارت ملی هوشمند");
            entity.Property(e => e.SmartCardStatus)
                .IsFixedLength()
                .HasComment("آیا كارت ملی هوشمند دارد؟");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.TfaIsRequired)
                .IsFixedLength()
                .HasComment("آیا انجام مرحله اعتبارسنجی عامل دوم، بعنوان پیش نیاز اخذ اثر انگشت ضروری است؟");
            entity.Property(e => e.TfaMobileNo).HasComment("شماره موبایل برای احراز هویت دو عاملی");
            entity.Property(e => e.TfaSendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال عامل دوم");
            entity.Property(e => e.TfaSendTime).HasComment("زمان ارسال عامل دوم");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("نتیجه انجام احراز هویت دو عاملی");
            entity.Property(e => e.TfaValidateDate)
                .IsFixedLength()
                .HasComment("تاریخ اعتبارسنجی عامل دوم");
            entity.Property(e => e.TfaValidateTime).HasComment("زمان اعتبارسنجی عامل دوم");
            entity.Property(e => e.TfaValue).HasComment("مقدار عامل دوم");
            entity.Property(e => e.UseCaseMainObjectId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.UseCasePersonObjectId).HasComment("شناسه ركورد معادلی در كاربرد كه اثر انگشت برای آن گرفته شده است");

            entity.HasOne(d => d.PersonFingerType).WithMany(p => p.PersonFingerprints).HasConstraintName("FK_PERSON_FINGERPRINT_PERSON_FINGER_TYPE");

            entity.HasOne(d => d.PersonFingerprintUseCase).WithMany(p => p.PersonFingerprints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_FNGRPRNT_USECASE");
        });

        modelBuilder.Entity<PersonFingerprintUseCase>(entity =>
        {
            entity.ToTable("PERSON_FINGERPRINT_USE_CASE", tb => tb.HasComment("كاربردهای استفاده كننده از خدمات اخذ اثر انگشت"));

            entity.Property(e => e.Id).HasComment("كلید اصلی");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ReliablePersonReason>(entity =>
        {
            entity.ToTable("RELIABLE_PERSON_REASON", tb => tb.HasComment("دلیل نیاز به معتمد"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<ScriptoriumEmployee>(entity =>
        {
            entity.ToTable("SCRIPTORIUM_EMPLOYEE", tb => tb.HasComment("كاركنان دفترخانه ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.AliasName).HasComment("نام مستعار");
            entity.Property(e => e.BirthDate)
                .IsFixedLength()
                .HasComment("تاریخ تولد");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Email).HasComment("آدرس پست الكترونیكی");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آیا این شخص زنده است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعیت مالكیت شماره خط تلفن همراه");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شماره ملی");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعیت حساب كاربری ثنا");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه - بخش عددی");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حرفی");
            entity.Property(e => e.Serial).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");
        });

        modelBuilder.Entity<ScriptoriumEmployeeAccess>(entity =>
        {
            entity.ToTable("SCRIPTORIUM_EMPLOYEE_ACCESS", tb => tb.HasComment("سوابق دسترسی های كاركنان دفترخانه ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.FromDate)
                .IsFixedLength()
                .HasComment("تاریخ شروع دسترسی");
            entity.Property(e => e.FromTime).HasComment("زمان شروع دسترسی");
            entity.Property(e => e.ScriptoriumEmployeeId).HasComment("شناسه كاركنان دفترخانه ها");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.ToDate)
                .IsFixedLength()
                .HasComment("تاریخ خاتمه دسترسی");
            entity.Property(e => e.ToTime).HasComment("زمان خاتمه دسترسی");

            entity.HasOne(d => d.ScriptoriumEmployee).WithMany(p => p.ScriptoriumEmployeeAccesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SCRIPTORIUM_EMPLOYEE_ACCESS_SCRIPTORIUM_EMPLOYEE");
        });

        modelBuilder.Entity<ScriptoriumSetup>(entity =>
        {
            entity.ToTable("SCRIPTORIUM_SETUP", tb => tb.HasComment("اطلاعات اولیه ثبت اسناد در هر دفترخانه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.DocumentTextFontSize).HasComment("پیش فرض اندازه فونت در چاپ اسناد");
            entity.Property(e => e.LastDocumentClassifyNo).HasComment("آخرین شماره ترتیب سند در آغاز كار با سامانه");
            entity.Property(e => e.LastInquiryNo).HasComment("آخرین شماره استعلام ملك در آغاز كار با سامانه");
            entity.Property(e => e.LastSignNo).HasComment("آخرین شماره ترتیب گواهی امضاء در آغاز كار با سامانه");
            entity.Property(e => e.PosBaseNo).HasComment("شماره پوز");
            entity.Property(e => e.PosPortType).HasComment("نوع پورت پوز");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
        });

        modelBuilder.Entity<SignElectronicBook>(entity =>
        {
            entity.ToTable("SIGN_ELECTRONIC_BOOK", tb => tb.HasComment("دفتر الکترونيک گواهي امضا"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ClassifyNo).HasComment("شماره ترتیب ثبت در دفتر");
            entity.Property(e => e.ClassifyNoReserved).HasComment("شماره ترتيب - رزرو شده");
            entity.Property(e => e.ConfirmDate)
                .IsFixedLength()
                .HasComment("تاریخ تأیید");
            entity.Property(e => e.ConfirmTime).HasComment("زمان تأیید");
            entity.Property(e => e.DigitalSign).HasComment("امضای الكترونیك");
            entity.Property(e => e.HashOfFile).HasComment("هش فایل چاپ نهایی گواهی امضاء");
            entity.Property(e => e.HashOfFingerprint).HasComment("هش اثرانگشت شخص در گواهی امضاء");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OldSignRequestPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ و زمان ركورد به میلادی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SignCertificateDn).HasComment("اطلاعات شناسه گواهی امضای الكترونیك");
            entity.Property(e => e.SignDate)
                .IsFixedLength()
                .HasComment("تاریخ گواهی امضاء");
            entity.Property(e => e.SignRequestId).HasComment("شناسه گواهی امضاء");
            entity.Property(e => e.SignRequestNationalNo).HasComment("شناسه یكتا گواهی امضاء");
            entity.Property(e => e.SignRequestPersonId).HasComment("شناسه شخص گواهی امضاء");

            entity.HasOne(d => d.SignRequest).WithMany(p => p.SignElectronicBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_ELECTRONIC_BOOK_SIGN_REQUEST");

            entity.HasOne(d => d.SignRequestPerson).WithOne(p => p.SignElectronicBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_ELECTRONIC_BOOK_SIGN_REQUEST_PERSON");
        });

        modelBuilder.Entity<SignRequest>(entity =>
        {
            entity.ToTable("SIGN_REQUEST", tb => tb.HasComment("گواهی امضاء"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment(" شناسه");
            entity.Property(e => e.BillNo).HasComment("شناسه قبض");
            entity.Property(e => e.ConfirmDate)
                .IsFixedLength()
                .HasComment("تاریخ تأیید گواهی امضاء");
            entity.Property(e => e.ConfirmTime).HasComment("زمان تأیید گواهی امضاء");
            entity.Property(e => e.Confirmer).HasComment("تأییدكننده گواهی امضاء");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.DigitalSign).HasComment("امضای الكترونیك");
            entity.Property(e => e.HowToPay)
                .IsFixedLength()
                .HasComment("شیوه پردخت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsCostPaid)
                .IsFixedLength()
                .HasComment("آیا هزینه های گواهی امضاء پرداخت شده است؟");
            entity.Property(e => e.IsFinalizeInProgress)
                .IsFixedLength()
                .HasComment("آیا این گواهی امضاء در جریان پروسه تأیید نهایی است؟");
            entity.Property(e => e.IsReadyToPay)
                .IsFixedLength()
                .HasComment("آیا آماده پرداخت هست؟");
            entity.Property(e => e.IsRemoteRequest)
                .IsFixedLength()
                .HasComment("آیا بصورت غیرحضوری ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.Modifier).HasComment("نام و نام خانوادگی آخرین اصلاح كننده");
            entity.Property(e => e.ModifyDate)
                .IsFixedLength()
                .HasComment("تاریخ آخرین اصلاح");
            entity.Property(e => e.ModifyTime).HasComment("زمان آخرین اصلاح");
            entity.Property(e => e.NationalNo).HasComment("شناسه یكتا گواهی امضاء");
            entity.Property(e => e.PayCostDate)
                .IsFixedLength()
                .HasComment("تاریخ پرداخت هزینه های ارائه خدمت");
            entity.Property(e => e.PayCostTime).HasComment("زمان پرداخت هزینه های ارائه خدمت");
            entity.Property(e => e.PaymentType).HasComment("نوع پرداخت");
            entity.Property(e => e.ReceiptNo).HasComment("شناسه مرجع تراكنش");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ پرونده به میلادی");
            entity.Property(e => e.RemoteRequestId).HasComment("شناسه درخواست غیرحضوری");
            entity.Property(e => e.ReqDate)
                .IsFixedLength()
                .HasComment("تاریخ درخواست");
            entity.Property(e => e.ReqNo).HasComment("شماره درخواست");
            entity.Property(e => e.ReqTime).HasComment("زمان درخواست");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SecretCode).HasComment("رمز تصدیق گواهی امضاء");
            entity.Property(e => e.SignCertificateDn).HasComment("شناسه گواهی امضای الكترونیك");
            entity.Property(e => e.SignDate)
                .IsFixedLength()
                .HasComment("تاریخ گواهی امضاء");
            entity.Property(e => e.SignRequestGetterId).HasComment("شناسه ارگان دریافت كننده گواهی امضاء");
            entity.Property(e => e.SignRequestSubjectId).HasComment("شناسه موضوع گواهی امضاء");
            entity.Property(e => e.SignText).HasComment("متن گواهی امضاء");
            entity.Property(e => e.SignTime).HasComment("زمان گواهی امضاء");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.SumPrices).HasComment("جمع هزینه های ارائه خدمت");

            entity.HasOne(d => d.SignRequestGetter).WithMany(p => p.SignRequests).HasConstraintName("FK_SIGN_REQUEST_GETTER");

            entity.HasOne(d => d.SignRequestSubject).WithMany(p => p.SignRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQUEST_SUBJECT");
        });

        modelBuilder.Entity<SignRequestFile>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_FILE", tb => tb.HasComment("نسخه های پشتیبان و نهایی اسناد رسمی"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.EdmId).HasComment("شناسه سند در مخزن اسناد الکترونيک");
            entity.Property(e => e.EdmVersion).HasComment("ورژن سند در مخزن اسناد الکترونيک");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LastFile).HasComment("محتوای فایل نسخه نهایی گواهی امضاء");
            entity.Property(e => e.LastFileCreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد فایل نسخه نهایی گواهی امضاء");
            entity.Property(e => e.LastFileCreateTime).HasComment("زمان ایجاد فایل نسخه نهایی گواهی امضاء");
            entity.Property(e => e.LastLegacyId).HasComment("كلید اصلی ركورد چاپ گواهی امضاء معادل در سامانه قدیمی");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ایجاد ركورد به میلادی");
            entity.Property(e => e.ScanFile).HasComment("محتوای فایل اسكن گواهی امضاء");
            entity.Property(e => e.ScanFileCreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ایجاد فایل اسكن گواهی امضاء");
            entity.Property(e => e.ScanFileCreateTime).HasComment("زمان ایجاد فایل اسكن گواهی امضاء");
            entity.Property(e => e.ScanLegacyId).HasComment("كلید اصلی ركورد تصویر معادل در سامانه قدیمی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SignRequestId).HasComment("شناسه گواهی امضاء");

            entity.HasOne(d => d.SignRequest).WithOne(p => p.SignRequestFile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQUEST_FILE_SIGN_REQUEST");
        });

        modelBuilder.Entity<SignRequestGetter>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_GETTER", tb => tb.HasComment("ارگان دریافت كننده گواهی امضاء"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SignRequestPerson>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_PERSON", tb => tb.HasComment("شناسه گواهی امضاء"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("نشانی");
            entity.Property(e => e.AmlakEskanState)
                .IsFixedLength()
                .HasComment("وضعیت ثبت نام در سامانه املاك و اسكان");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد");
            entity.Property(e => e.ClassifyNoReserved).HasComment("شماره ترتيب - رزرو شده");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Email).HasComment("آدرس پست الكترونیكی");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("آیا كارت هوشمند ملی دارد؟");
            entity.Property(e => e.IdentityIssueLocation).HasComment("محل صدور شناسنامه");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آیا این شخص زنده است؟");
            entity.Property(e => e.IsFingerprintGotten)
                .IsFixedLength()
                .HasComment("آیا تصویر اثر انگشت شخص گرفته شده است؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .IsFixedLength()
                .HasComment("آیا این شخص اصیل است؟");
            entity.Property(e => e.IsPrisoner)
                .IsFixedLength()
                .HasComment("آیا ثبت سند برای این شخص در زندان صورت گرفته است؟");
            entity.Property(e => e.IsRelated)
                .IsFixedLength()
                .HasComment("آیا این شخص وكیل یا نماینده اشخاص دیگری است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعیت مالكیت شماره خط تلفن همراه");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت تطابق اثر انگشت با اثر انگشت مندرج در كارت هوشمند ملی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("شناسه كشور تابعیت");
            entity.Property(e => e.OldNationalityId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldSignRequestId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PassportNo).HasComment("شماره گذرنامه");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("حقیقی است یا حقوقی؟");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ركورد به میلادی");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعیت حساب كاربری ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه - بخش عددی");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حرفی");
            entity.Property(e => e.Serial).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.SignClassifyNo).HasComment("شماره ترتیب گواهی امضاء");
            entity.Property(e => e.SignClassifyNoDescription).HasComment("ملاحظات مربوط به شماره ترتیب گواهی امضاء");
            entity.Property(e => e.SignRequestId).HasComment("شناسه گواهی امضاء");
            entity.Property(e => e.Tel).HasComment("شماره تلفن ثابت");
            entity.Property(e => e.TfaRequired)
                .IsFixedLength()
                .HasComment("آیا انجام احراز هویت دو مرحله ای برای این شخص لازم است؟");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("نتیجه انجام احراز هویت دو مرحله ای");

            entity.HasOne(d => d.SignRequest).WithMany(p => p.SignRequestPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQUEST_PERSON_REQUEST");
        });

        modelBuilder.Entity<SignRequestPersonRelated>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_PERSON_RELATED", tb => tb.HasComment("وابستگی اشخاص گواهی امضاء"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AgentDocumentCountryId).HasComment("شناسه كشوری كه وكالتنامه در آن تنظیم شده است");
            entity.Property(e => e.AgentDocumentDate)
                .IsFixedLength()
                .HasComment("تاریخ وكالتنامه");
            entity.Property(e => e.AgentDocumentId).HasComment("شناسه وكالتنامه");
            entity.Property(e => e.AgentDocumentIssuer).HasComment("مرجع صدور وكالتنامه");
            entity.Property(e => e.AgentDocumentNo).HasComment("شماره وكالتنامه");
            entity.Property(e => e.AgentDocumentScriptoriumId).HasComment("شناسه دفترخانه تنظیم كننده وكالتنامه");
            entity.Property(e => e.AgentDocumentSecretCode).HasComment("رمز تصدیق وكالتنامه");
            entity.Property(e => e.AgentPersonId).HasComment("شناسه شخص وابسته");
            entity.Property(e => e.AgentTypeId).HasComment("شناسه نوع وابستگی");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.IsAgentDocumentAbroad)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در مراجع قانونی خارج از كشور ثبت شده است؟");
            entity.Property(e => e.IsLawyer)
                .IsFixedLength()
                .HasComment("آیا وكیل دادگستری است؟");
            entity.Property(e => e.IsRelatedDocumentInSsar)
                .IsFixedLength()
                .HasComment("آیا وكالتنامه در سامانه ثبت الكترونیك اسناد ثبت شده است؟");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.MainPersonId).HasComment("شناسه شخص اصلی");
            entity.Property(e => e.OldAgentPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldMainPersonId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.OldSignRequestId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("sysdate               ")
                .HasComment("تاریخ ركورد به میلادی");
            entity.Property(e => e.ReliablePersonReasonId).HasComment("شناسه دلیل نیاز به معتمد");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.SignRequestId).HasComment("شناسه گواهی امضاء");
            entity.Property(e => e.SignRequestScriptoriumId).HasComment("شناسه دفترخانه گواهی امضاء");

            entity.HasOne(d => d.AgentPerson).WithMany(p => p.SignRequestPersonRelatedAgentPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQ_PRS_RELATED_AGNT_PRS");

            entity.HasOne(d => d.AgentType).WithMany(p => p.SignRequestPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQ_PRS_RELATED_AGENT_TYPE");

            entity.HasOne(d => d.MainPerson).WithMany(p => p.SignRequestPersonRelatedMainPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQ_PRS_RELATED_CORE_PRS");

            entity.HasOne(d => d.ReliablePersonReason).WithMany(p => p.SignRequestPersonRelateds).HasConstraintName("FK_SIGN_REQ_PRS_REL_RELIABLE");

            entity.HasOne(d => d.SignRequest).WithMany(p => p.SignRequestPersonRelateds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQ_PRS_RELATED_SIGN_REQ");
        });

        modelBuilder.Entity<SignRequestSemaphore>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_SEMAPHORE", tb => tb.HasComment("سمافور برای تایید نهایی گواهی امضا"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.LastChangeDate).HasComment("تاریخ آخرین تغییر");
            entity.Property(e => e.LastChangeTime).HasComment("زمان آخرین تغییر");
            entity.Property(e => e.LastClassifyNo).HasComment("آخرین شماره ترتیب گواهی امضاء در دفترخانه مربوطه قبل از شروع عملیات");
            entity.Property(e => e.NewSignRequestData).HasComment("اقلام اطلاعاتی گواهی امضا مربوطه بعد از شروع عملیات");
            entity.Property(e => e.OriginalSignRequestData).HasComment("اقلام اطلاعاتی گواهی امضا مربوطه قبل از شروع عملیات");
            entity.Property(e => e.RecordDate).HasComment("تاریخ ایجاد ركورد");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه مربوطه");
            entity.Property(e => e.SignElectronicBookData).HasComment("اقلام اطلاعاتی ركوردهایی كه در دفتر الكترونیك گواهی امضاء ساخته خواهند شد");
            entity.Property(e => e.SignRequestId).HasComment("شناسه گواهی امضا مربوطه");
            entity.Property(e => e.State).HasComment("وضعیت سمافور");

            entity.HasOne(d => d.SignRequest).WithMany(p => p.SignRequestSemaphores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQUEST_SEMAPHORE_SIGN_REQUEST");
        });

        modelBuilder.Entity<SignRequestSubject>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_SUBJECT", tb => tb.HasComment("موضوع گواهی امضاء"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.SignRequestSubjectGroupId).HasComment("شناسه گروه بندی موضوعات گواهی امضاء");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.SignRequestSubjectGroup).WithMany(p => p.SignRequestSubjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIGN_REQUEST_SUBJECT_GROUP");
        });

        modelBuilder.Entity<SignRequestSubjectGroup>(entity =>
        {
            entity.ToTable("SIGN_REQUEST_SUBJECT_GROUP", tb => tb.HasComment("گروه بندی موضوعات گواهی امضاء"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrApiExternalUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_API_EXTERNAL_USER_ID");

            entity.ToTable("SSR_API_EXTERNAL_USER", tb => tb.HasComment("كاربران بیرونی استفاده كننده از سرویس های سامانه"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.UserName).HasComment("نام كاربری");
            entity.Property(e => e.UserPassword).HasComment("كلمه عبور");
        });

        modelBuilder.Entity<SsrApiExternalUserAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_API_EXTERNAL_USER_ACCES");

            entity.ToTable("SSR_API_EXTERNAL_USER_ACCESS", tb => tb.HasComment("جدول دسترسی های کاربران بیرونی به سرویس ها"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ApiPath).HasComment("مسیر سرویس در سامانه");
            entity.Property(e => e.SsrApiExternalUserId).HasComment("شناسه کاربر بیرونی");
            entity.Property(e => e.State).IsFixedLength();

            entity.HasOne(d => d.SsrApiExternalUser).WithMany(p => p.SsrApiExternalUserAccesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_API_EXT_USR_ACC_USR_ID");
        });

        modelBuilder.Entity<SsrArticle6County>(entity =>
        {
            entity.ToTable("SSR_ARTICLE6_COUNTY", tb => tb.HasComment("جدول شهرستان مرتبط با استعلام ماده 6 قانون الزام مطابق با تقسیمات سیاسی وزارت كشور"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.Nid).HasComment("شناسه ملی شهرستان");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrArticle6EstateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_ESTATE_TYPE_ID");

            entity.ToTable("SSR_ARTICLE6_ESTATE_TYPE", tb => tb.HasComment("انواع ملک برای استعلام ماده 6 قانون الزام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrArticle6EstateUsing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_ESTATE_USE_ID");

            entity.ToTable("SSR_ARTICLE6_ESTATE_USING", tb => tb.HasComment("انواع کاربری ملک برای استعلام ماده 6 قانون الزام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrArticle6Inq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_INQ_ID");

            entity.ToTable("SSR_ARTICLE6_INQ", tb => tb.HasComment("استعلام ماده 6 قانون الزام"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس ملک");
            entity.Property(e => e.Attachments).HasComment("فایل های پیوست");
            entity.Property(e => e.CountyId).HasComment("شناسه شهرستان");
            entity.Property(e => e.CreateDate)
                .IsFixedLength()
                .HasComment("تاریخ ثبت");
            entity.Property(e => e.CreateTime).HasComment("زمان ثبت");
            entity.Property(e => e.EstRelatedInfoLoadBySvc)
                .IsFixedLength()
                .HasComment("اطلاعات ملک و مالک با سرويس فراخواني شده اند");
            entity.Property(e => e.EstateArea).HasComment("مساحت ملك");
            entity.Property(e => e.EstateDocElectronicNoteNo).HasComment("شماره دفتر املاک الکترونيک سند");
            entity.Property(e => e.EstateMainPlaque).HasComment("پلاك اصلی ملك");
            entity.Property(e => e.EstateMap).HasComment("نقشه ملك");
            entity.Property(e => e.EstatePostCode).HasComment("كد پستی ملك");
            entity.Property(e => e.EstateSecondaryPlaque).HasComment("پلاك فرعی ملك");
            entity.Property(e => e.EstateSectionId).HasComment("شناسه بخش ثبتی مرتبط با ملك");
            entity.Property(e => e.EstateSubsectionId).HasComment("شناسه ناحیه ثبتی مرتبط با ملك");
            entity.Property(e => e.EstateTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.EstateUnitId).HasComment("شناسه واحد ثبتی مرتبط با ملك");
            entity.Property(e => e.EstateUsingId).HasComment("شناسه نوع کاربری ملک مرتبط");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.No).HasComment("شماره یكتا");
            entity.Property(e => e.ProvinceId).HasComment("شناسه استان");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه ثبت كننده");
            entity.Property(e => e.SendDate)
                .IsFixedLength()
                .HasComment("تاریخ ارسال استعلام");
            entity.Property(e => e.SendTime).HasComment("زمان ارسال استعلام");
            entity.Property(e => e.TrackingCode).HasComment("کد رهگيري");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("1")
                .HasComment("نوع درخواست");
            entity.Property(e => e.WorkflowStatesId).HasComment("شناسه وضعیت استعلام");

            entity.HasOne(d => d.County).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_CID");

            entity.HasOne(d => d.EstateSection).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_SID");

            entity.HasOne(d => d.EstateSubsection).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_SSID");

            entity.HasOne(d => d.EstateType).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_EST_TYPE_ID");

            entity.HasOne(d => d.EstateUsing).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_EST_USE_ID");

            entity.HasOne(d => d.Province).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_PID");

            entity.HasOne(d => d.WorkflowStates).WithMany(p => p.SsrArticle6Inqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_WSID");
        });

        modelBuilder.Entity<SsrArticle6InqPerson>(entity =>
        {
            entity.ToTable("SSR_ARTICLE6_INQ_PERSON", tb => tb.HasComment("شخص استعلام"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Address).HasComment("آدرس");
            entity.Property(e => e.BirthDate).HasComment("تاریخ تولد/ثبت");
            entity.Property(e => e.BirthPlaceId).HasComment("ردیف محل تولد");
            entity.Property(e => e.CityId).HasComment("ردیف محل سكونت");
            entity.Property(e => e.CompanyTypeId).HasComment("شناسه نوع شرکت");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.Email).HasComment("پست الکترونيک");
            entity.Property(e => e.ExecutiveTransfer)
                .IsFixedLength()
                .HasComment("انتقال اجرایی");
            entity.Property(e => e.Family).HasComment("نام خانوادگی");
            entity.Property(e => e.FatherName).HasComment("نام پدر");
            entity.Property(e => e.Fax).HasComment("فکس");
            entity.Property(e => e.ForiegnBirthPlace).HasComment("محل تولد خارجی");
            entity.Property(e => e.ForiegnIssuePlace).HasComment("محل صدور خارجی");
            entity.Property(e => e.HasSmartCard)
                .IsFixedLength()
                .HasComment("كارت ملی هوشمند دارد؟");
            entity.Property(e => e.Haslawyer).HasComment("آیا درخواست دهنده وکیل می باشد ");
            entity.Property(e => e.IdentityNo).HasComment("شماره شناسنامه/ثبت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.IsAlive)
                .IsFixedLength()
                .HasComment("آيا زنده است؟");
            entity.Property(e => e.IsForeignerssysChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام از سامانه اتباع خارجی انجام شده است؟");
            entity.Property(e => e.IsForeignerssysCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با ساماه اتباع خارجی تطابق دارد؟");
            entity.Property(e => e.IsIlencChecked)
                .IsFixedLength()
                .HasComment("ایا استعلام از سامانه اشخاص حقوقی برای این شخص انجام شده است؟");
            entity.Property(e => e.IsIlencCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با سامانه اشخاص حقوقی تطابق دارد؟");
            entity.Property(e => e.IsIranian)
                .IsFixedLength()
                .HasComment("آیا این شخص ایرانی است؟");
            entity.Property(e => e.IsOriginal)
                .HasDefaultValueSql("'1'                   ")
                .IsFixedLength()
                .HasComment("اصيل است؟");
            entity.Property(e => e.IsRelated)
                .HasDefaultValueSql("'2'                   ")
                .IsFixedLength()
                .HasComment("وابسته است؟");
            entity.Property(e => e.IsSabtahvalChecked)
                .IsFixedLength()
                .HasComment("آیا استعلام ثبت احوال برای این شخص انجام شده است؟");
            entity.Property(e => e.IsSabtahvalCorrect)
                .IsFixedLength()
                .HasComment("آیا اطلاعات شخص با ثبت احوال تطابق دارد؟");
            entity.Property(e => e.IssuePlaceId).HasComment("ردیف محل صدور");
            entity.Property(e => e.IssuePlaceText).HasComment("محل صدور");
            entity.Property(e => e.LastLegalPaperDate)
                .IsFixedLength()
                .HasComment("تاريخ آخرين روزنامه رسمي");
            entity.Property(e => e.LastLegalPaperNo).HasComment("شماره آخرين روزنامه رسمي");
            entity.Property(e => e.Lawyerbirthdate).HasComment("تاریخ تولد وکیل ");
            entity.Property(e => e.Lawyerfathername).HasComment("نام پدر وکیل ");
            entity.Property(e => e.Lawyermobile).HasComment("شماره موبایل وکیل ");
            entity.Property(e => e.Lawyername).HasComment("نام وکیل ");
            entity.Property(e => e.Lawyernationalid).HasComment("کد ملی وکیل  ");
            entity.Property(e => e.Lawyerpostalcode).HasComment(" کد پستی وکیل ");
            entity.Property(e => e.LegalpersonNatureId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.LegalpersonTypeId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Managerbirthdate).HasComment("تاریخ تولد مدیر شرکت");
            entity.Property(e => e.Managerfathername).HasComment("نام پدر مدیر شرکت");
            entity.Property(e => e.Managerfmily).HasComment("نام خانوادگی مدیر شرکت");
            entity.Property(e => e.Managername).HasComment("نام مدیر شرکت");
            entity.Property(e => e.Managernationalcode).HasComment("کد ملی مدیر شرکت");
            entity.Property(e => e.MobileNo).HasComment("شماره تلفن همراه");
            entity.Property(e => e.MobileNoState)
                .IsFixedLength()
                .HasComment("وضعيت مالکيت خط موبايل با شاهکار");
            entity.Property(e => e.MocState)
                .IsFixedLength()
                .HasComment("وضعیت كارت ملی");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalityCode).HasComment("كد/شناسه ملی");
            entity.Property(e => e.NationalityId).HasComment("ردیف تابعیت");
            entity.Property(e => e.OwnershipDocumentType)
                .HasDefaultValueSql("1\n")
                .HasComment("نوع سند مالکيت");
            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("نوع شخص");
            entity.Property(e => e.PostalCode).HasComment("كد پستی");
            entity.Property(e => e.RelationType).HasComment("نوع ارتباط شخص در معامله (خریدار/فروشنده)");
            entity.Property(e => e.SanaHasOrganizationChart).HasComment("آيا در ثنا چارت سازماني دارد؟");
            entity.Property(e => e.SanaInquiryDate)
                .IsFixedLength()
                .HasComment("تاريخ استعلام ثنا");
            entity.Property(e => e.SanaInquiryTime).HasComment("زمان استعلام ثنا");
            entity.Property(e => e.SanaMobileNo).HasComment("شماره موبايل ثنا");
            entity.Property(e => e.SanaOrganizationCode).HasComment("کد سازمان ثنا");
            entity.Property(e => e.SanaOrganizationName).HasComment("نام سازمان ثنا");
            entity.Property(e => e.SanaState)
                .IsFixedLength()
                .HasComment("وضعیت ثنا");
            entity.Property(e => e.ScriptoriumId).HasComment("ردیف عامل پارتیشن");
            entity.Property(e => e.Seri).HasComment("سری شناسنامه");
            entity.Property(e => e.SeriAlpha).HasComment("سری شناسنامه - بخش حروفی");
            entity.Property(e => e.SerialNo).HasComment("سریال شناسنامه");
            entity.Property(e => e.SexType)
                .IsFixedLength()
                .HasComment("جنسیت");
            entity.Property(e => e.SharePart).HasComment("جزسهم");
            entity.Property(e => e.ShareText).HasComment("متن سهم");
            entity.Property(e => e.ShareTotal).HasComment("كل سهم");
            entity.Property(e => e.SsrArticle6InqId).HasComment("ردیف استعلام");
            entity.Property(e => e.Tel).HasComment("تلفن");
            entity.Property(e => e.TfaRequired)
                .IsFixedLength()
                .HasComment("رمز دو عاملی نیاز است؟");
            entity.Property(e => e.TfaState)
                .IsFixedLength()
                .HasComment("وضعیت رمز دوعاملی؟");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");

            entity.HasOne(d => d.SsrArticle6Inq).WithMany(p => p.SsrArticle6InqPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ART6_INQ_PERSON_INQID");
        });

        modelBuilder.Entity<SsrArticle6InqReceiver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_INQ_RECEIVE");

            entity.ToTable("SSR_ARTICLE6_INQ_RECEIVER", tb => tb.HasComment("گيرنده استعلام ماده 6"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.SsrArticle6InqId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.SsrArticle6OrganId).HasComment("شناسه رکورد در جدول متناظر");

            entity.HasOne(d => d.SsrArticle6Inq).WithMany(p => p.SsrArticle6InqReceivers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ART6_INQ_REC_INQID");

            entity.HasOne(d => d.SsrArticle6Organ).WithMany(p => p.SsrArticle6InqReceivers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ES_ARTI6_INQ_RECEIVER_ORGID");
        });

        modelBuilder.Entity<SsrArticle6InqReceiverOrg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_INQ_REC_ORG");

            entity.ToTable("SSR_ARTICLE6_INQ_RECEIVER_ORG", tb => tb.HasComment("دستگاه های دریافت كننده استعلام"));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SendDate).HasComment("تاریخ ارسال استعلام به دستگاه مربوطه");
            entity.Property(e => e.SsrArticle6InqId).HasComment("شناسه استعلام");
            entity.Property(e => e.SsrArticle6SubOrganId).HasComment("شناسه دستگاه مریوطه");
            entity.Property(e => e.TrackingCode).HasComment("كد رهگیری دستگاه مربوطه");

            entity.HasOne(d => d.SsrArticle6Inq).WithMany(p => p.SsrArticle6InqReceiverOrgs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_REC_INQ_ID");

            entity.HasOne(d => d.SsrArticle6SubOrgan).WithMany(p => p.SsrArticle6InqReceiverOrgs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ARTICLE6_INQ_REC_ORG_ID");
        });

        modelBuilder.Entity<SsrArticle6InqResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_INQ_RESPONS");

            entity.ToTable("SSR_ARTICLE6_INQ_RESPONSE", tb => tb.HasComment("پاسخ استعلام ماده 6 قانون الزام"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Description).HasComment("شرح");
            entity.Property(e => e.EstateMap).HasComment("مپ");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("ILM");
            entity.Property(e => e.OppositionId).HasComment("شناسه ركورد در جدول متناظر");
            entity.Property(e => e.ResponseDate)
                .IsFixedLength()
                .HasComment("تاریخ پاسخ");
            entity.Property(e => e.ResponseNo).HasComment("شماره پاسخ");
            entity.Property(e => e.ResponseTime).HasComment("زمان پاسخ");
            entity.Property(e => e.ResponseType).HasComment("نوع پاسخ");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه ركورد در جدول متناظر");
            entity.Property(e => e.SenderOrgId).HasComment("دستگاه گیرنده استعلام");
            entity.Property(e => e.SsrArticle6InqId).HasComment("شناسه ركورد در جدول متناظر");
            entity.Property(e => e.State).IsFixedLength();

            entity.HasOne(d => d.Opposition).WithMany(p => p.SsrArticle6InqResponses).HasConstraintName("FK_SSR_ART6_INQ_RES_OPID");

            entity.HasOne(d => d.SenderOrg).WithMany(p => p.SsrArticle6InqResponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ART6_INQ_RES_SORGID");

            entity.HasOne(d => d.SsrArticle6Inq).WithMany(p => p.SsrArticle6InqResponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ART6_INQ_RES_INQID");
        });

        modelBuilder.Entity<SsrArticle6OppositReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_OPPOSIT_REA");

            entity.ToTable("SSR_ARTICLE6_OPPOSIT_REASON", tb => tb.HasComment("جدول علل مخالفت ارگان های استعلام شونده استعلام ماده 6 قانون الزام"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.SsrArticle6SubOrganId).HasComment("شناسه سازمان مرتبط");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.SsrArticle6SubOrgan).WithMany(p => p.SsrArticle6OppositReasons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ART6_OPP_REAS_ORG_ID");
        });

        modelBuilder.Entity<SsrArticle6Organ>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ARTICLE6_INQUIRER_OR");

            entity.ToTable("SSR_ARTICLE6_ORGAN", tb => tb.HasComment("وزارت خانه استعلام شونده ماده 6"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrArticle6Province>(entity =>
        {
            entity.ToTable("SSR_ARTICLE6_PROVINCE", tb => tb.HasComment("جدول استان مرتبط با استعلام ماده 6 قانون الزام مطابق با تقسیمات سیاسی وزارت كشور"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.Nid).HasComment("شناسه ملی استان");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrArticle6SubOrgan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_ART6_SUB_ORG");

            entity.ToTable("SSR_ARTICLE6_SUB_ORGAN", tb => tb.HasComment("سازمان استعلام شونده ماده 6"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد");
            entity.Property(e => e.SsrArticle6OrganId).HasComment("شناسه وزارت خانه");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعیت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.SsrArticle6Organ).WithMany(p => p.SsrArticle6SubOrgans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_ART6_SUB_ORG_PARID");
        });

        modelBuilder.Entity<SsrConfig>(entity =>
        {
            entity.ToTable("SSR_CONFIG", tb => tb.HasComment("تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ActionType)
                .IsFixedLength()
                .HasComment("نوع عملکرد در قبال بروز شرايط و عدم تحقق خواسته:      0: گذر کن       1: فقط هشدار بده و گذر کن        2: از ادامه کار جلوگيري کن");
            entity.Property(e => e.AgentTypeCondition)
                .IsFixedLength()
                .HasComment("شرط نوع وابستگي اشخاص :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.ConditionType)
                .IsFixedLength()
                .HasComment("يک-هميشه و بدون شرط انجام بشود      دو-بطور مشروط انجام بشود       سه-غيرفعال");
            entity.Property(e => e.ConfigEndDate)
                .IsFixedLength()
                .HasComment("تاريخ اتمام تأثيرگذاري تنظيمات");
            entity.Property(e => e.ConfigEndTime).HasComment("زمان اتمام تأثيرگذاري تنظيمات - مثال 16:30");
            entity.Property(e => e.ConfigStartDate)
                .IsFixedLength()
                .HasComment("تاريخ شروع تأثيرگذاري تنظيمات");
            entity.Property(e => e.ConfigStartTime).HasComment("زمان شروع تأثيرگذاري تنظيمات - مثال 11:00");
            entity.Property(e => e.ConfirmDate)
                .IsFixedLength()
                .HasComment("تاريخ تأييد");
            entity.Property(e => e.ConfirmTime).HasComment("زمان تأييد");
            entity.Property(e => e.Confirmer).HasComment("مشخصات تأييدکننده");
            entity.Property(e => e.CostTypeCondition)
                .IsFixedLength()
                .HasComment("شرط نوع هزينه :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.DocTypeCondition)
                .IsFixedLength()
                .HasComment("شرط نوع سند :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.GeoCondition)
                .IsFixedLength()
                .HasComment("شرط محل جغرافيايي :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.IsConfirmed)
                .IsFixedLength()
                .HasComment("آيا تأييد شده است؟");
            entity.Property(e => e.PersonTypeCondition)
                .IsFixedLength()
                .HasComment("شرط نوع سمت اشخاص :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.ScriptoriumCondition)
                .IsFixedLength()
                .HasComment("شرط دفترخانه :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.SsrConfigMainSubjectId).HasComment("شناسه نوع موضوع اصلي تنظيمات ثبت الکترونيک اسناد رسمي");
            entity.Property(e => e.SsrConfigSubjectId).HasComment("شناسه نوع موضوع تنظيمات ثبت الکترونيک اسناد رسمي");
            entity.Property(e => e.TimeCondition)
                .IsFixedLength()
                .HasComment("شرط محدوده زماني :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.UnitCondition)
                .IsFixedLength()
                .HasComment("شرط واحد ثبتي :     0-مطرح نيست      1-براي همه      2-براي بعضي");
            entity.Property(e => e.Value).HasComment("مقدار تنظيم شده");

            entity.HasOne(d => d.SsrConfigMainSubject).WithMany(p => p.SsrConfigs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_MAIN_SUBJ");

            entity.HasOne(d => d.SsrConfigSubject).WithMany(p => p.SsrConfigs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_SUBJ");
        });

        modelBuilder.Entity<SsrConfigConditionAgntType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_CONFIG_CONDITION_AGNTYP");

            entity.ToTable("SSR_CONFIG_CONDITION_AGNT_TYPE", tb => tb.HasComment("انواع وابستگي اشخاص مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.AgentTypeId).HasComment("شناسه نوع وابستگي اشخاص");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");

            entity.HasOne(d => d.AgentType).WithMany(p => p.SsrConfigConditionAgntTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_AGNT_AGNT");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionAgntTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_AGNT_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionCostType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_CONFIG_CONDITION_CSTYP");

            entity.ToTable("SSR_CONFIG_CONDITION_COST_TYPE", tb => tb.HasComment("انواع هزينه هاي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CostTypeId).HasComment("شناسه نوع هزينه");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");

            entity.HasOne(d => d.CostType).WithMany(p => p.SsrConfigConditionCostTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_CSTP_CSTP");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionCostTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_CSTP_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionDcprstp>(entity =>
        {
            entity.ToTable("SSR_CONFIG_CONDITION_DCPRSTP", tb => tb.HasComment("انواع سِمت هاي اشخاص در اسناد مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentPersonTypeId).HasComment("شناسه نوع سِمت در اسناد");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");

            entity.HasOne(d => d.DocumentPersonType).WithMany(p => p.SsrConfigConditionDcprstps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_DCPT_DCPT");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionDcprstps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_DCPT_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionDoctype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_CONFIG_CONDITION_DOCTYP");

            entity.ToTable("SSR_CONFIG_CONDITION_DOCTYPE", tb => tb.HasComment("انواع سند مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentTypeId).HasComment("شناسه نوع سند");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.SsrConfigConditionDoctypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_DCTP_DCTP");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionDoctypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_DCTP_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionGeoloc>(entity =>
        {
            entity.ToTable("SSR_CONFIG_CONDITION_GEOLOC", tb => tb.HasComment("محل هاي جغرافيايي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.GeoLocationId).HasComment("شناسه محل جغرافيايي");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionGeolocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_GEO_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionScrptrm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_CONFIG_CONDITION_SCRPTM");

            entity.ToTable("SSR_CONFIG_CONDITION_SCRPTRM", tb => tb.HasComment("دفترخانه هاي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionScrptrms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_SCRT_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionTime>(entity =>
        {
            entity.ToTable("SSR_CONFIG_CONDITION_TIME", tb => tb.HasComment("محدوده هاي زماني مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DayOfWeek)
                .IsFixedLength()
                .HasComment("روز هفته يا تعطيل:     0-تعطيل رسمي     1-شنبه     2-يکشنبه     3-دوشنبه     4-سه شنبه     5-چهارشنبه     6-پنجشنبه     7-جمعه");
            entity.Property(e => e.FromTime).HasComment("از ساعت - مثال 11:00");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");
            entity.Property(e => e.ToTime).HasComment("تا ساعت - مثال 16:30");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionTimes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_TIME_CNFG");
        });

        modelBuilder.Entity<SsrConfigConditionUnit>(entity =>
        {
            entity.ToTable("SSR_CONFIG_CONDITION_UNIT", tb => tb.HasComment("واحدهاي ثبتي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.SsrConfigId).HasComment("شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي");
            entity.Property(e => e.UnitLevelCode).HasComment("کد سلسله مراتبي در ساختار درختي واحدهاي ثبتي");

            entity.HasOne(d => d.SsrConfig).WithMany(p => p.SsrConfigConditionUnits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_CNDTN_UNIT_CNFG");
        });

        modelBuilder.Entity<SsrConfigMainSubject>(entity =>
        {
            entity.ToTable("SSR_CONFIG_MAIN_SUBJECT", tb => tb.HasComment("انواع موضوعات اصلي تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.Title).HasComment("عنوان");
        });

        modelBuilder.Entity<SsrConfigSubject>(entity =>
        {
            entity.ToTable("SSR_CONFIG_SUBJECT", tb => tb.HasComment("انواع موضوعات تنظيمات ثبت الکترونيک اسناد رسمي"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("کد");
            entity.Property(e => e.ConfigType)
                .IsFixedLength()
                .HasComment("نوع:        1_مقدار ثابت        2_شرط و نوع برخورد با موضوع");
            entity.Property(e => e.ConfilictResolveType)
                .IsFixedLength()
                .HasComment("نوع برخورد با تناقض احتمالي تنظيمات:       1: خوشبينانه عمل شود            2:بدبينانه عمل شود            3:آخرين کانفيگ اعمال شود");
            entity.Property(e => e.SsrConfigMainSubjectId).HasComment("شناسه موضوع اصلي تنظيمات ثبت الکترونيک اسناد رسمي");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.SsrConfigMainSubject).WithMany(p => p.SsrConfigSubjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_CONFIG_SUBJECT_MSUBJ");
        });

        modelBuilder.Entity<SsrDocModifyClassifyNo>(entity =>
        {
            entity.ToTable("SSR_DOC_MODIFY_CLASSIFY_NO", tb => tb.HasComment("سابقه اصلاح اطلاعات ثبت سند در دفتر"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ClassifyNoNew).HasComment("شماره ترتیب جدید");
            entity.Property(e => e.ClassifyNoOld).HasComment("شماره ترتیب قبلی");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.Modifier).HasComment("مشخصات اصلاح كننده");
            entity.Property(e => e.ModifyDate)
                .IsFixedLength()
                .HasComment("تاریخ اصلاح");
            entity.Property(e => e.ModifyTime).HasComment("زمان اصلاح");
            entity.Property(e => e.RegisterPapersNoNew).HasComment("شماره صفحات جدید دفتر");
            entity.Property(e => e.RegisterPapersNoOld).HasComment("شماره صفحات قبلی دفتر");
            entity.Property(e => e.RegisterVolumeNoNew).HasComment("شماره جلد جدید دفتر");
            entity.Property(e => e.RegisterVolumeNoOld).HasComment("شماره جلد قبلی دفتر");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
            entity.Property(e => e.WriteInBookDateNew)
                .IsFixedLength()
                .HasComment("تاریخ جدید ثبت در دفتر");
            entity.Property(e => e.WriteInBookDateOld)
                .IsFixedLength()
                .HasComment("تاریخ قبلی ثبت در دفتر");

            entity.HasOne(d => d.Document).WithMany(p => p.SsrDocModifyClassifyNos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOC_MDFY_CLSFYNO_DOC");
        });

        modelBuilder.Entity<SsrDocVerifCallLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_DOC_VERIF_CALL_LOG_ID");

            entity.ToTable("SSR_DOC_VERIF_CALL_LOG", tb => tb.HasComment("جدول سابقه فراخوانی های سرویس تصدیق اصالت سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.CallDateTime).HasComment("تلریخ و زمان فراخوانی");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.DocumentNo).HasComment("شماره سند مورد نظر");
            entity.Property(e => e.DocumentSecretCode).HasComment("رمز تصدیق سند مورد نظر");
            entity.Property(e => e.Input).HasComment("ورودی سرویس");
            entity.Property(e => e.Output).HasComment("خروجی سرویس");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه مربوط به سند مورد نظر");
            entity.Property(e => e.SsrDocVerifExternalUserId).HasComment("شناسه کاربر خارج از سامانه متقاضی تصدیق اصالت سند");
        });

        modelBuilder.Entity<SsrDocVerifExternalUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_DOCUMENT_VERIFICATION_U");

            entity.ToTable("SSR_DOC_VERIF_EXTERNAL_USER", tb => tb.HasComment("جدول کاربران خارجی مجاز به فراخوانی سرویس تصدیق اصالت سند"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.AllowForAllDocTypes)
                .IsFixedLength()
                .HasComment("مجاز بودن/نبودن برای تصدیق اصالت همه انواع سند");
            entity.Property(e => e.AllowShowPdf)
                .IsFixedLength()
                .HasComment("مجاز بودن/نبودن برای دریافت فایل پی دی اف سند مربوطه");
            entity.Property(e => e.AllowShowSignPdf)
                .IsFixedLength()
                .HasComment("مجاز بودن/نبودن برای دریافت فایل پی دی اف دارای امضای الکترونیک سند مربوطه");
            entity.Property(e => e.AllowedDocTypesId).HasComment("شناسه نوع سند های مجاز برای تصدیق اصالت");
            entity.Property(e => e.IsActive)
                .IsFixedLength()
                .HasComment("مجاز بودن/نبودن برای فراخوانی سرویس");
            entity.Property(e => e.SsrApiExternalUserId).HasComment("شناسه کاربر خارج از سامانه");

            entity.HasOne(d => d.SsrApiExternalUser).WithMany(p => p.SsrDocVerifExternalUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOC_VERIF_EXT_USR_ID");
        });

        modelBuilder.Entity<SsrDocumentAsset>(entity =>
        {
            entity.ToTable("SSR_DOCUMENT_ASSET", tb => tb.HasComment("سایر اموال منقول ثبت شده در اسناد"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Description).HasComment("ملاحظات");
            entity.Property(e => e.DocumentAssetTypeId).HasComment("شناسه نوع سایر اموال منقول مندرج در اسناد رسمی");
            entity.Property(e => e.DocumentId).HasComment("شناسه سند");
            entity.Property(e => e.HajBranchCode).HasComment("کد شعبه حج و زيارت");
            entity.Property(e => e.HajDocumentNo).HasComment("شماره سند حج");
            entity.Property(e => e.HajInquiryInfo).HasComment("اطلاعات دريافتي از سازمان حج و زيارت در مورد فيش حج");
            entity.Property(e => e.HajPermissionNo).HasComment("شماره مجوز حج و زيارت");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OwnershipDetailQuota).HasComment("جزء سهم مورد مالكیت");
            entity.Property(e => e.OwnershipTotalQuota).HasComment("كل سهم مورد مالكیت");
            entity.Property(e => e.OwnershipType)
                .IsFixedLength()
                .HasComment("نوع مالكیت");
            entity.Property(e => e.Price).HasComment("مبلغ سند");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.RowNo).HasComment("ردیف");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.SellDetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.SellTotalQuota).HasComment("كل سهم مورد معامله");
            entity.Property(e => e.ShipCapacity).HasComment("ظرفيت شناور");
            entity.Property(e => e.ShipDimensions).HasComment("ابعاد شناور");
            entity.Property(e => e.ShipEnginePower).HasComment("قدرت موتور شناور");
            entity.Property(e => e.ShipEngineSerialNo).HasComment("شماره سريال موتور شناور");
            entity.Property(e => e.ShipEngineType).HasComment("نوع موتور شناور");
            entity.Property(e => e.ShipEnginesInfo).HasComment("مشخصات موتورهاي شناور در پاسخ استعلام");
            entity.Property(e => e.ShipIsBodyIncluded)
                .IsFixedLength()
                .HasComment("آيا مورد معامله شامل بدنه شناور مي شود؟");
            entity.Property(e => e.ShipIsEngineIncluded)
                .IsFixedLength()
                .HasComment("آيا مورد معامله شامل موتور شناور مي شود؟");
            entity.Property(e => e.ShipName).HasComment("نام شناور");
            entity.Property(e => e.ShipOwnersInfo).HasComment("مشخصات مالکين شناور در پاسخ استعلام");
            entity.Property(e => e.ShipPort).HasComment("بندر شناور");
            entity.Property(e => e.ShipRegisterNo).HasComment("شماره ثبت شناور");
            entity.Property(e => e.ShipType).HasComment("نوع شناور");
            entity.Property(e => e.Title).HasComment("عنوان");

            entity.HasOne(d => d.DocumentAssetType).WithMany(p => p.SsrDocumentAssets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOCASSET_DOCASSETYPE");

            entity.HasOne(d => d.Document).WithMany(p => p.SsrDocumentAssets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOCASSET_DOC");
        });

        modelBuilder.Entity<SsrDocumentAssetQuotaDtl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_DOCASTQTADTLS");

            entity.ToTable("SSR_DOCUMENT_ASSET_QUOTA_DTLS", tb => tb.HasComment("جزئیات سهم بندی فروشنده و خریدار از سایر اموال منقول مورد معامله در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DocumentPersonBuyerId).HasComment("شناسه شخص خریدار در سند");
            entity.Property(e => e.DocumentPersonSellerId).HasComment("شناسه شخص فروشنده در سند");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.OwnershipDetailQuota).HasComment("جزء سهم مورد مالكیت");
            entity.Property(e => e.OwnershipTotalQuota).HasComment("كل سهم مورد مالكیت");
            entity.Property(e => e.QuotaText).HasComment("متن سهم مورد معامله");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده");
            entity.Property(e => e.SellDetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.SellTotalQuota).HasComment("كل سهم مورد معامله");
            entity.Property(e => e.SsrDocumentAssetId).HasComment("شناسه سایر اموال منقول ثبت شده در سند");

            entity.HasOne(d => d.DocumentPersonBuyer).WithMany(p => p.SsrDocumentAssetQuotaDtlDocumentPersonBuyers).HasConstraintName("FK_SSR_DOCASTQTADTLS_BUYER");

            entity.HasOne(d => d.DocumentPersonSeller).WithMany(p => p.SsrDocumentAssetQuotaDtlDocumentPersonSellers).HasConstraintName("FK_DOCASTQTADTLS_SELLER");

            entity.HasOne(d => d.SsrDocumentAsset).WithMany(p => p.SsrDocumentAssetQuotaDtls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOCASTQTADTLS_DOCAST");
        });

        modelBuilder.Entity<SsrDocumentAssetQuotum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SSR_DOCASTQUOTA");

            entity.ToTable("SSR_DOCUMENT_ASSET_QUOTA", tb => tb.HasComment("سهم اصحاب سند از سایر اموال منقول مورد معامله در سند"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.DetailQuota).HasComment("جزء سهم مورد معامله");
            entity.Property(e => e.DocumentPersonId).HasComment("شناسه اشخاص اسناد");
            entity.Property(e => e.Ilm)
                .IsFixedLength()
                .HasComment("Information Lifecycle Management");
            entity.Property(e => e.LegacyId).HasComment("كلید اصلی ركورد معادل در سامانه قدیمی");
            entity.Property(e => e.QuotaText).HasComment("متن سهم");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه صادركننده سند");
            entity.Property(e => e.SsrDocumentAssetId).HasComment("شناسه سایر اموال منقول ثبت شده در اسناد");
            entity.Property(e => e.TotalQuota).HasComment("كل سهم مورد معامله");

            entity.HasOne(d => d.DocumentPerson).WithMany(p => p.SsrDocumentAssetQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOCASTQUOTA_DOCPRS");

            entity.HasOne(d => d.SsrDocumentAsset).WithMany(p => p.SsrDocumentAssetQuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSR_DOCASTQUOTA_DOCAST");
        });

        modelBuilder.Entity<SsrSignEbookBaseinfo>(entity =>
        {
            entity.ToTable("SSR_SIGN_EBOOK_BASEINFO", tb => tb.HasComment("اطلاعات پایه دفتر الكترونیك گواهی امضاء"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.ExordiumConfirmDate).HasComment("تاریخ امضای سردفتر");
            entity.Property(e => e.ExordiumConfirmTime).HasComment("زمان امضای سردفتر");
            entity.Property(e => e.ExordiumDigitalSign).HasComment("امضای الكترونیك سردفتر");
            entity.Property(e => e.LastClassifyNo).HasComment("شماره ترتیب آخرین گواهی امضاء ثبت شده در دفاتر كاغذی");
            entity.Property(e => e.LastRegisterPaperNo).HasComment("شماره صفحه دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی");
            entity.Property(e => e.LastRegisterVolumeNo).HasComment("شماره جلد دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی");
            entity.Property(e => e.NumberOfBooks).HasComment("تعداد كل دفاتر كاغذی");
            entity.Property(e => e.ScriptoriumId).HasComment("شناسه دفترخانه");
        });

        modelBuilder.Entity<SystemMessage>(entity =>
        {
            entity.ToTable("SYSTEM_MESSAGE", tb => tb.HasComment("پیام ها"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Code).HasComment("كد پیام");
            entity.Property(e => e.Message).HasComment("متن پیام");
        });

        modelBuilder.Entity<ToadPlanTable>(entity =>
        {
            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ParentId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PartitionId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.PlanId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.StatementId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.Timestamp).HasComment("فيلد عددي کنترلي مخصوص کنترل concurrency");
        });

        modelBuilder.Entity<TransactionInfo>(entity =>
        {
            entity.ToTable("TRANSACTION_INFO", tb => tb.HasComment("اطلاعات تراکنش"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Input).HasComment("ورودي");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.Output).HasComment("خروجي");
            entity.Property(e => e.RelatedRecordId).HasComment("شناسه رکورد در جدول متناظر");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
        });

        modelBuilder.Entity<TransactionStep>(entity =>
        {
            entity.ToTable("TRANSACTION_STEPS", tb => tb.HasComment("گام هاي تراکنش"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Input).HasComment("ورودي");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.Output).HasComment("خروجي");
            entity.Property(e => e.RowNo).HasComment("رديف");
            entity.Property(e => e.State)
                .IsFixedLength()
                .HasComment("وضعيت");
            entity.Property(e => e.TransactionInfoId).HasComment("شناسه رکورد در جدول متناظر");

            entity.HasOne(d => d.TransactionInfo).WithMany(p => p.TransactionSteps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSACTION_STEPS_TRANSACTION_INFO");
        });

        modelBuilder.Entity<ValueAddedTax>(entity =>
        {
            entity.ToTable("VALUE_ADDED_TAX", tb => tb.HasComment("درصد مالیات بر ارزش افزوده"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ChargePercent).HasComment("درصد عوارض");
            entity.Property(e => e.CostTypeId).HasComment("شناسه نوع هزینه خدمات ثبتی");
            entity.Property(e => e.EndDate)
                .IsFixedLength()
                .HasComment("تا تاریخ");
            entity.Property(e => e.StartDate)
                .IsFixedLength()
                .HasComment("از تاریخ");
            entity.Property(e => e.TaxPercent).HasComment("درصد مالیات بر ارزش افزوده");
            entity.Property(e => e.TotalTaxPercent).HasComment("كل درصد مالیات بر ارزش افزوده");

            entity.HasOne(d => d.CostType).WithMany(p => p.ValueAddedTaxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VALUE_ADDED_TAX_COST_TYPE");
        });

        modelBuilder.Entity<VatInfo>(entity =>
        {
            entity.ToTable("VAT_INFO", tb => tb.HasComment("اطلاعات پرونده مالياتي سردفتران و دفترياران"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("شناسه");
            entity.Property(e => e.Family).HasComment("نام خانوادگي");
            entity.Property(e => e.LegacyId).HasComment("کليد اصلي رکورد در سامانه قبلي");
            entity.Property(e => e.Name).HasComment("نام");
            entity.Property(e => e.NationalNo).HasComment("شماره ملي");
            entity.Property(e => e.TaxAccountNo).HasComment("شماره حساب واريز ماليات");
            entity.Property(e => e.TaxCode).HasComment("كد مالیاتی");
            entity.Property(e => e.TaxOstan).HasComment("استان ماليات");
            entity.Property(e => e.TaxPercent).HasComment("درصد ماليات بر ارزش افزوده");
            entity.Property(e => e.TaxShebaNo).HasComment("شبا واريز ماليات");
            entity.Property(e => e.TaxUnitCode).HasComment("کد اداره مالياتي");
            entity.Property(e => e.TaxUnitName).HasComment("نام اداره مالياتي");
            entity.Property(e => e.TollAccountNo).HasComment("شماره حساب واريز عوارض");
            entity.Property(e => e.TollOstan).HasComment("استان عوارض");
            entity.Property(e => e.TollPercent).HasComment("درصد عوارض");
            entity.Property(e => e.TollShebaNo).HasComment("شبا واريز عوارض");
        });

        modelBuilder.Entity<WorkflowState>(entity =>
        {
            entity.ToTable("WORKFLOW_STATES", tb => tb.HasComment("انواع وضعیت گردش موضوعات اطلاعاتی"));

            entity.Property(e => e.Id).HasComment("شناسه");
            entity.Property(e => e.ColumnName).HasComment("نام انگلیسی ستون وضعیت");
            entity.Property(e => e.Description).HasComment("توضیحات");
            entity.Property(e => e.State).HasComment("مقدار وضعیت");
            entity.Property(e => e.TableName).HasComment("نام انگلیسی جدول");
            entity.Property(e => e.Title).HasComment("عنوان وضعیت");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
