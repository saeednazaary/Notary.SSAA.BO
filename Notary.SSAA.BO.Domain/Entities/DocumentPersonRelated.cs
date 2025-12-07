using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// وابستگی اشخاص اسناد
/// </summary>
[Table("DOCUMENT_PERSON_RELATED")]
[Index("AgentDocumentCountryId", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_DOCUMENT_COUNTRY_ID")]
[Index("AgentDocumentDate", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_DOCUMENT_DATE")]
[Index("AgentDocumentId", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_DOCUMENT_ID")]
[Index("AgentDocumentNo", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_DOCUMENT_NO")]
[Index("AgentDocumentScriptoriumId", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_DOCUMENT_SCRIPTORIUM_ID")]
[Index("AgentPersonId", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_PERSON_ID")]
[Index("AgentTypeId", Name = "IDX_DOCUMENT_PERSON_RELATED###AGENT_TYPE_ID")]
[Index("DocumentId", Name = "IDX_DOCUMENT_PERSON_RELATED###DOCUMENT_ID")]
[Index("DocumentScriptoriumId", Name = "IDX_DOCUMENT_PERSON_RELATED###DOCUMENT_SCRIPTORIUM_ID")]
[Index("DocumentSmsId", Name = "IDX_DOCUMENT_PERSON_RELATED###DOCUMENT_SMS_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_PERSON_RELATED###ILM")]
[Index("IsAgentDocumentAbroad", Name = "IDX_DOCUMENT_PERSON_RELATED###IS_AGENT_DOCUMENT_ABROAD")]
[Index("IsLawyer", Name = "IDX_DOCUMENT_PERSON_RELATED###IS_LAWYER")]
[Index("IsRelatedDocumentInSsar", Name = "IDX_DOCUMENT_PERSON_RELATED###IS_RELATED_DOCUMENT_IN_SSAR")]
[Index("ReliablePersonReasonId", Name = "IDX_DOCUMENT_PERSON_RELATED###RELIABLE_PERSON_REASON_ID")]
[Index("MainPersonId", "AgentPersonId", "AgentTypeId", Name = "IDX_SSR_DC_PRS_REL_MID_AID_TID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_PERSON_RELATED###LEGACY_ID", IsUnique = true)]
public partial class DocumentPersonRelated
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// شناسه شخص اصلی
    /// </summary>
    [Column("MAIN_PERSON_ID")]
    public Guid MainPersonId { get; set; }

    /// <summary>
    /// شناسه شخص وابسته
    /// </summary>
    [Column("AGENT_PERSON_ID")]
    public Guid AgentPersonId { get; set; }

    /// <summary>
    /// شناسه نوع وابستگی
    /// </summary>
    [Required]
    [Column("AGENT_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string AgentTypeId { get; set; }

    /// <summary>
    /// آیا وكالتنامه در مراجع قانونی خارج از كشور ثبت شده است؟
    /// </summary>
    [Column("IS_AGENT_DOCUMENT_ABROAD")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAgentDocumentAbroad { get; set; }

    /// <summary>
    /// شناسه كشوری كه وكالتنامه در آن تنظیم شده است
    /// </summary>
    [Column("AGENT_DOCUMENT_COUNTRY_ID")]
    [Precision(6)]
    public int? AgentDocumentCountryId { get; set; }

    /// <summary>
    /// آیا وكالتنامه در سامانه ثبت الكترونیك اسناد ثبت شده است؟
    /// </summary>
    [Column("IS_RELATED_DOCUMENT_IN_SSAR")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelatedDocumentInSsar { get; set; }

    /// <summary>
    /// شماره وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string AgentDocumentNo { get; set; }

    /// <summary>
    /// تاریخ وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string AgentDocumentDate { get; set; }

    /// <summary>
    /// مرجع صدور وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_ISSUER")]
    [StringLength(100)]
    [Unicode(false)]
    public string AgentDocumentIssuer { get; set; }

    /// <summary>
    /// رمز تصدیق وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_SECRET_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string AgentDocumentSecretCode { get; set; }

    /// <summary>
    /// شناسه دفترخانه تنظیم كننده وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string AgentDocumentScriptoriumId { get; set; }

    /// <summary>
    /// شناسه وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_ID")]
    public Guid? AgentDocumentId { get; set; }

    /// <summary>
    /// آیا وكیل دادگستری است؟
    /// </summary>
    [Column("IS_LAWYER")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsLawyer { get; set; }

    /// <summary>
    /// شناسه دلیل نیاز به معتمد
    /// </summary>
    [Column("RELIABLE_PERSON_REASON_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ReliablePersonReasonId { get; set; }

    /// <summary>
    /// شناسه ركورد پیامك مربوط به وكالتنامه
    /// </summary>
    [Column("DOCUMENT_SMS_ID")]
    public Guid? DocumentSmsId { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه گواهی امضاء
    /// </summary>
    [Required]
    [Column("DOCUMENT_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string DocumentScriptoriumId { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_MAIN_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldMainPersonId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_AGENT_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldAgentPersonId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_AGENT_DOCUMENT_COUNTRY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldAgentDocumentCountryId { get; set; }

    [ForeignKey("AgentDocumentId")]
    [InverseProperty("DocumentPersonRelatedAgentDocuments")]
    public virtual Document AgentDocument { get; set; }

    [ForeignKey("AgentPersonId")]
    [InverseProperty("DocumentPersonRelatedAgentPeople")]
    public virtual DocumentPerson AgentPerson { get; set; }

    [ForeignKey("AgentTypeId")]
    [InverseProperty("DocumentPersonRelateds")]
    public virtual AgentType AgentType { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentPersonRelatedDocuments")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentSmsId")]
    [InverseProperty("DocumentPersonRelateds")]
    public virtual DocumentSm DocumentSms { get; set; }

    [ForeignKey("MainPersonId")]
    [InverseProperty("DocumentPersonRelatedMainPeople")]
    public virtual DocumentPerson MainPerson { get; set; }

    [ForeignKey("ReliablePersonReasonId")]
    [InverseProperty("DocumentPersonRelateds")]
    public virtual ReliablePersonReason ReliablePersonReason { get; set; }
}
