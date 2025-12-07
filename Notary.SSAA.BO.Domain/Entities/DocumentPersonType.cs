using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع سمت اشخاص سند
/// </summary>
[Table("DOCUMENT_PERSON_TYPE")]
[Index("IsOwner", Name = "IDX_DOCUMENT_PERSON_TYPE###IS_OWNER")]
[Index("IsProhibitionCheckRequired", Name = "IDX_DOCUMENT_PERSON_TYPE###IS_PROHIBITION_CHECK_REQUIRED")]
[Index("IsRequired", Name = "IDX_DOCUMENT_PERSON_TYPE###IS_REQUIRED")]
[Index("IsSanaRequired", Name = "IDX_DOCUMENT_PERSON_TYPE###IS_SANA_REQUIRED")]
[Index("IsShahkarRequired", Name = "IDX_DOCUMENT_PERSON_TYPE###IS_SHAHKAR_REQUIRED")]
[Index("RowNoInForm", Name = "IDX_DOCUMENT_PERSON_TYPE###ROW_NO_IN_FORM")]
[Index("RowNoInPrint", Name = "IDX_DOCUMENT_PERSON_TYPE###ROW_NO_IN_PRINT")]
[Index("SingularTitle", Name = "IDX_DOCUMENT_PERSON_TYPE###SINGULAR_TITLE")]
[Index("State", Name = "IDX_DOCUMENT_PERSON_TYPE###STATE")]
[Index("IsAmlakEskanRequired", Name = "IX_SSR_DOCPRSTYP_AMLAK_ESKAN")]
[Index("DocumentTypeId", "Code", Name = "UDX_DOCUMENT_PERSON_TYPE###DOCUMENT_TYPE_ID#CODE", IsUnique = true)]
[Index("DocumentTypeId", "SingularTitle", Name = "UDX_DOCUMENT_PERSON_TYPE###DOCUMENT_TYPE_ID#SINGULAR_TITLE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_PERSON_TYPE###LEGACY_ID", IsUnique = true)]
public partial class DocumentPersonType
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه نوع سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(4)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// ترتیب نمایش در فرم سند
    /// </summary>
    [Column("ROW_NO_IN_FORM")]
    [Precision(2)]
    public byte RowNoInForm { get; set; }

    /// <summary>
    /// ترتیب نمایش در چاپ سند
    /// </summary>
    [Column("ROW_NO_IN_PRINT")]
    [Precision(2)]
    public byte RowNoInPrint { get; set; }

    /// <summary>
    /// عنوان مفرد سمت در فرم سند
    /// </summary>
    [Required]
    [Column("SINGULAR_TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string SingularTitle { get; set; }

    /// <summary>
    /// عنوان جمع سمت در فرم سند
    /// </summary>
    [Required]
    [Column("PLURAL_TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PluralTitle { get; set; }

    /// <summary>
    /// عنوان مفرد سمت در چاپ سند
    /// </summary>
    [Required]
    [Column("PRINT_SINGULAR_TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PrintSingularTitle { get; set; }

    /// <summary>
    /// عنوان جمع سمت در چاپ سند
    /// </summary>
    [Required]
    [Column("PRINT_PLURAL_TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PrintPluralTitle { get; set; }

    /// <summary>
    /// آیا ثبت حداقل یك شخص از این نوع اجباری است؟
    /// </summary>
    [Required]
    [Column("IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRequired { get; set; }

    /// <summary>
    /// آیا ممنوع‌المعامله بودن برای این نوع اشخاص چك شود؟
    /// </summary>
    [Required]
    [Column("IS_PROHIBITION_CHECK_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsProhibitionCheckRequired { get; set; }

    /// <summary>
    /// آیا این سمت با سمت معامل تناظر دارد؟
    /// </summary>
    [Column("IS_OWNER")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsOwner { get; set; }

    /// <summary>
    /// آیا برای اشخاص با این نوع سمت، داشتن ثنا اجباری است؟
    /// </summary>
    [Column("IS_SANA_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSanaRequired { get; set; }

    /// <summary>
    /// آیا برای اشخاص با این نوع سمت، داشتن شماره موبایل معتبر با مالكیت خود شخص، اجباری است؟
    /// </summary>
    [Column("IS_SHAHKAR_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsShahkarRequired { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// آیا ثبت نام در سامانه املاک و اسکان برای این نوع اشخاص چک شود؟
    /// </summary>
    [Column("IS_AMLAK_ESKAN_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAmlakEskanRequired { get; set; }

    [InverseProperty("DocumentPersonType")]
    public virtual ICollection<DocumentPerson> DocumentPeople { get; set; } = new List<DocumentPerson>();

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("DocumentPersonTypes")]
    public virtual DocumentType DocumentType { get; set; }

    [InverseProperty("DocumentPersonType")]
    public virtual ICollection<SsrConfigConditionDcprstp> SsrConfigConditionDcprstps { get; set; } = new List<SsrConfigConditionDcprstp>();
}
