using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص وابسته به شخص اصلی درخواست
/// </summary>
[Table("ESTATE_DOC_REQ_PERSON_RELATE")]
[Index("AgentPersonId", Name = "IX_SSR_DCRQPRRL_APRSID")]
[Index("AgentTypeId", Name = "IX_SSR_DCRQPRRL_ATYPID")]
[Index("MainPersonId", Name = "IX_SSR_DCRQPRRL_MPERID")]
[Index("ReliablePersonReasonId", Name = "IX_SSR_DCRQPRRL_RELBID")]
[Index("EstateDocumentRequestId", Name = "IX_SSR_DCRQPRRL_REQID")]
public partial class EstateDocReqPersonRelate
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// ردیف كشوری كه وكالتنامه در آن تنظیم شده 
    /// </summary>
    [Column("AGENT_DOCUMENT_COUNTRY_ID")]
    [Precision(6)]
    public int? AgentDocumentCountryId { get; set; }

    /// <summary>
    /// تاریخ مدرك
    /// </summary>
    [Column("AGENT_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string AgentDocumentDate { get; set; }

    /// <summary>
    /// Agent_Document_Id
    /// </summary>
    [Column("AGENT_DOCUMENT_ID")]
    public Guid? AgentDocumentId { get; set; }

    /// <summary>
    /// مرجع صدور مدرك
    /// </summary>
    [Column("AGENT_DOCUMENT_ISSUER")]
    [StringLength(500)]
    [Unicode(false)]
    public string AgentDocumentIssuer { get; set; }

    /// <summary>
    /// شماره مدرك
    /// </summary>
    [Column("AGENT_DOCUMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string AgentDocumentNo { get; set; }

    /// <summary>
    /// ردیف Agent_Document_Scriptorium_
    /// </summary>
    [Column("AGENT_DOCUMENT_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string AgentDocumentScriptoriumId { get; set; }

    /// <summary>
    /// رمز تصدیق
    /// </summary>
    [Column("AGENT_DOCUMENT_SECRET_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string AgentDocumentSecretCode { get; set; }

    /// <summary>
    /// ردیف شخص نماینده
    /// </summary>
    [Column("AGENT_PERSON_ID")]
    public Guid AgentPersonId { get; set; }

    /// <summary>
    /// ردیف نوع ارتباط
    /// </summary>
    [Required]
    [Column("AGENT_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string AgentTypeId { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// آیا وكالتنامه در مرجع قانونی خارج از كشور ثبت شده است؟
    /// </summary>
    [Column("IS_AGENT_DOCUMENT_ABROAD")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAgentDocumentAbroad { get; set; }

    /// <summary>
    /// آیا وكیل دادگستری است؟
    /// </summary>
    [Column("IS_LAWYER")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsLawyer { get; set; }

    /// <summary>
    /// آیا وكالتنامه در سامانه ثبت اسناد ثبت شده است?
    /// </summary>
    [Column("IS_RELATED_DOCUMENT_IN_SSAR")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelatedDocumentInSsar { get; set; }

    /// <summary>
    /// ردیف شخص اصلی
    /// </summary>
    [Column("MAIN_PERSON_ID")]
    public Guid MainPersonId { get; set; }

    /// <summary>
    /// ردیف دلیل نیاز به متعمد
    /// </summary>
    [Column("RELIABLE_PERSON_REASON_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ReliablePersonReasonId { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ESTATE_DOCUMENT_REQUEST_ID")]
    public Guid EstateDocumentRequestId { get; set; }

    [ForeignKey("AgentPersonId")]
    [InverseProperty("EstateDocReqPersonRelateAgentPeople")]
    public virtual EstateDocumentRequestPerson AgentPerson { get; set; }

    [ForeignKey("AgentTypeId")]
    [InverseProperty("EstateDocReqPersonRelates")]
    public virtual AgentType AgentType { get; set; }

    [ForeignKey("EstateDocumentRequestId")]
    [InverseProperty("EstateDocReqPersonRelates")]
    public virtual EstateDocumentRequest EstateDocumentRequest { get; set; }

    [ForeignKey("MainPersonId")]
    [InverseProperty("EstateDocReqPersonRelateMainPeople")]
    public virtual EstateDocumentRequestPerson MainPerson { get; set; }

    [ForeignKey("ReliablePersonReasonId")]
    [InverseProperty("EstateDocReqPersonRelates")]
    public virtual ReliablePersonReason ReliablePersonReason { get; set; }
}
