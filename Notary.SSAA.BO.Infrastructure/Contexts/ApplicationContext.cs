using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.Infrastructure.Contexts
{
    public partial class ApplicationContext : SsarContext
    {
        public ApplicationContext(DbContextOptions<SsarContext> options)
        : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(Guid))
                    {
                        modelBuilder
                            .Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasConversion<GuidConverter>();
                    }
                }
            }
            modelBuilder.Entity<SignRequestPerson>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<SignRequestPersonRelated>()
                .Property(e => e.Id)
                .ValueGeneratedNever();
            modelBuilder.Entity<SignRequestPerson>()
                .HasMany(e => e.SignRequestPersonRelatedMainPeople)
                .WithOne(e => e.MainPerson).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SignRequestPerson>()
                .HasMany(e => e.SignRequestPersonRelatedAgentPeople)
                .WithOne(e => e.AgentPerson).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExecutiveRequestBinding>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

            modelBuilder.Entity<ExecutiveRequestDocument>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

            modelBuilder.Entity<ExecutiveRequestPerson>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

            modelBuilder.Entity<ExecutiveRequestPersonRelated>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

            modelBuilder.Entity<ForestorgInquiryPerson>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<ForestorgInquiryPoint>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<ForestorgInquiryFile>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

            modelBuilder.Entity<EstateInquiryPerson>()
           .Property(e => e.Id)
           .ValueGeneratedNever();

            modelBuilder.Entity<EstateInquirySendreceiveLog>()
           .Property(e => e.Id)
           .ValueGeneratedNever();

            modelBuilder.Entity<InquiryFromUnit>()
                .Property(e => e.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<InquiryFromUnitPerson>()
           .Property(e => e.Id)
           .ValueGeneratedNever();

            modelBuilder.Entity<Document>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentPerson>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentPersonRelated>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentCase>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstate>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentVehicle>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentInfoText>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentInfoConfirm>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<DocumentRelation>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentCost>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<DocumentCostUnchanged>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<DocumentPayment>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<DocumentInfoJudgement>()
           .Property(e => e.Id)
           .ValueGeneratedNever();
            modelBuilder.Entity<DocumentInfoOther>()
           .Property(e => e.Id)
           .ValueGeneratedNever();
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<DocumentVehicleQuotaDetail>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<DocumentVehicleQuotum>()
           .Property(e => e.Id)
         .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstateAttachment>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<DocumentInquiry>()
          .Property(e => e.Id)
        .ValueGeneratedNever();

            modelBuilder.Entity<EstateTaxInquiryAttach>()
           .Property(e => e.Id)
           .ValueGeneratedNever();

            modelBuilder.Entity<EstateTaxInquiryFile>()
           .Property(e => e.Id)
           .ValueGeneratedNever();

            modelBuilder.Entity<EstateTaxInquiryPerson>()
           .Property(e => e.Id)
           .ValueGeneratedNever();

            modelBuilder.Entity<EstateDocumentRequestPerson>()
         .Property(e => e.Id)
         .ValueGeneratedNever();

            modelBuilder.Entity<EstateDocReqPersonRelate>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstateOwnershipDocument>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstateSeparationPiece>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstateSeparationPiecesQuotum>()
         .Property(e => e.Id)
         .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstateQuotum>()
        .Property(e => e.Id)
        .ValueGeneratedNever();
            modelBuilder.Entity<DocumentEstateQuotaDetail>()
                        .Property(e => e.Id)
        .ValueGeneratedNever();
            modelBuilder.Entity<DocumentSm>()
        .Property(e => e.Id)
        .ValueGeneratedNever();
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<SsrArticle6InqPerson>();

            modelBuilder.Entity<SsrArticle6InqPerson>()
          .Property(e => e.Id)
          .ValueGeneratedNever();

            modelBuilder.Entity<SsrArticle6InqReceiver>()
          .Property(e => e.Id)
          .ValueGeneratedNever();

            modelBuilder.Entity<SsrArticle6InqResponse>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
            modelBuilder.Entity<SsrSignEbookBaseinfo>()
   .Property(e => e.Id)
   .ValueGeneratedNever();
            modelBuilder.Entity<SsrDocModifyClassifyNo>()
  .Property(e => e.Id)
  .ValueGeneratedNever();

            modelBuilder.Entity<SsrArticle6InqReceiverOrg>()
          .Property(e => e.Id)
          .ValueGeneratedNever();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class GuidConverter : ValueConverter<Guid, byte[]>
    {

        public GuidConverter() : base(g => ToByteArray(g), b => ToGuid(b))
        {

        }

        static byte[] ToByteArray(Guid guid)
        {
            return new Guid(BitConverter.ToString(guid.ToByteArray()).Replace("-", "")).ToByteArray();
        }

        static Guid ToGuid(byte[] data)
        {
            return new Guid(BitConverter.ToString(data).Replace("-", ""));
        }

    }
}
