using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// وابستگی اشخاص گواهی امضاء
/// </summary>
[Table("SIGN_REQUEST_PERSON_RELATED")]
[Index("AgentDocumentCountryId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_DOCUMENT_COUNTRY_ID")]
[Index("AgentDocumentDate", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_DOCUMENT_DATE")]
[Index("AgentDocumentId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_DOCUMENT_ID")]
[Index("AgentDocumentNo", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_DOCUMENT_NO")]
[Index("AgentDocumentScriptoriumId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_DOCUMENT_SCRIPTORIUM_ID")]
[Index("AgentPersonId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_PERSON_ID")]
[Index("AgentTypeId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###AGENT_TYPE_ID")]
[Index("Ilm", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###ILM")]
[Index("IsAgentDocumentAbroad", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###IS_AGENT_DOCUMENT_ABROAD")]
[Index("IsLawyer", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###IS_LAWYER")]
[Index("IsRelatedDocumentInSsar", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###IS_RELATED_DOCUMENT_IN_SSAR")]
[Index("RecordDate", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###RECORD_DATE")]
[Index("ReliablePersonReasonId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###RELIABLE_PERSON_REASON_ID")]
[Index("RowNo", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###ROW_NO")]
[Index("SignRequestId", Name = "IDX_SIGN_REQUEST_PERSON_RELATED###SIGN_REQUEST_ID")]
[Index("MainPersonId", Name = "IX_SSR_SGNRQ_PRREL_MAINID")]
[Index("OldAgentPersonId", Name = "SSR_IX_SN_RQ_PRS_RLT_LD_AGNT_ID")]
[Index("OldMainPersonId", Name = "SSR_IX_SN_RQ_PRS_RLT_LD_PRS_ID")]
[Index("OldSignRequestId", Name = "SSR_IX_SN_RQ_PRS_RLT_LD_RQ_ID")]
[Index("LegacyId", Name = "UDX_SIGN_REQUEST_PERSON_RELATED###LEGACY_ID", IsUnique = true)]
[Index("SignRequestId", "RowNo", Name = "UDX_SIGN_REQUEST_PERSON_RELATED###SIGN_REQUEST_ID###ROW_NO", IsUnique = true)]
public partial class SignRequestPersonRelated
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه گواهی امضاء
    /// </summary>
    [Column("SIGN_REQUEST_ID")]
    public Guid SignRequestId { get; set; }

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
    [StringLength(200)]
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
    [StringLength(200)]
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
    [Column("SIGN_REQUEST_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string SignRequestScriptoriumId { get; set; }

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
    /// تاریخ ركورد به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_SIGN_REQUEST_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldSignRequestId { get; set; }

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

    [ForeignKey("AgentPersonId")]
    [InverseProperty("SignRequestPersonRelatedAgentPeople")]
    public virtual SignRequestPerson AgentPerson { get; set; }

    [ForeignKey("AgentTypeId")]
    [InverseProperty("SignRequestPersonRelateds")]
    public virtual AgentType AgentType { get; set; }

    [ForeignKey("MainPersonId")]
    [InverseProperty("SignRequestPersonRelatedMainPeople")]
    public virtual SignRequestPerson MainPerson { get; set; }

    [ForeignKey("ReliablePersonReasonId")]
    [InverseProperty("SignRequestPersonRelateds")]
    public virtual ReliablePersonReason ReliablePersonReason { get; set; }

    [ForeignKey("SignRequestId")]
    [InverseProperty("SignRequestPersonRelateds")]
    public virtual SignRequest SignRequest { get; set; }
}
