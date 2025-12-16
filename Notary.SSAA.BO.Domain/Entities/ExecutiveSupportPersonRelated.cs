using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// وابستگی اشخاص سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_SUPPORT_PERSON_RELATED")]
[Index("AgentDocumentCountryId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_DOCUMENT_COUNTRY_ID")]
[Index("AgentDocumentDate", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_DOCUMENT_DATE")]
[Index("AgentDocumentId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_DOCUMENT_ID")]
[Index("AgentDocumentNo", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_DOCUMENT_NO")]
[Index("AgentDocumentScriptoriumId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_DOCUMENT_SCRIPTORIUM_ID")]
[Index("AgentPersonId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_PERSON_ID")]
[Index("AgentTypeId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###AGENT_TYPE_ID")]
[Index("ExecutiveSupportId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###EXECUTIVE_SUPPORT_ID")]
[Index("ExecutiveSupportScriptoriumId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###EXECUTIVE_SUPPORT_SCRIPTORIUM_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###ILM")]
[Index("IsAgentDocumentAbroad", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###IS_AGENT_DOCUMENT_ABROAD")]
[Index("IsLawyer", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###IS_LAWYER")]
[Index("IsRelatedDocumentInSsar", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###IS_RELATED_DOCUMENT_IN_SSAR")]
[Index("ReliablePersonReasonId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON_RELATED###RELIABLE_PERSON_REASON_ID")]
[Index("LegacyId", Name = "UDX_EXECUTIVE_SUPPORT_PERSON_RELATED###LEGACY_ID", IsUnique = true)]
[Index("MainPersonId", "AgentPersonId", "AgentTypeId", Name = "UDX_EXECUTIVE_SUPPORT_PERSON_RELATED###MAIN_PERSON_ID#AGENT_PERSON_ID#AGENT_TYPE_ID", IsUnique = true)]
public partial class ExecutiveSupportPersonRelated
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
    /// </summary>
    [Column("EXECUTIVE_SUPPORT_ID")]
    public Guid ExecutiveSupportId { get; set; }

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
    /// مرجه صدور وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_ISSUER")]
    [StringLength(100)]
    [Unicode(false)]
    public string AgentDocumentIssuer { get; set; }

    /// <summary>
    /// رمز تصدیق وكالتنامه
    /// </summary>
    [Column("AGENT_DOCUMENT_SECRET_CODE")]
    [StringLength(6)]
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
    /// شناسه دفترخانه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_SUPPORT_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ExecutiveSupportScriptoriumId { get; set; }

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

    [ForeignKey("AgentPersonId")]
    [InverseProperty("ExecutiveSupportPersonRelatedAgentPeople")]
    public virtual ExecutiveSupportPerson AgentPerson { get; set; }

    [ForeignKey("AgentTypeId")]
    [InverseProperty("ExecutiveSupportPersonRelateds")]
    public virtual AgentType AgentType { get; set; }

    [ForeignKey("ExecutiveSupportId")]
    [InverseProperty("ExecutiveSupportPersonRelateds")]
    public virtual ExecutiveSupport ExecutiveSupport { get; set; }

    [ForeignKey("MainPersonId")]
    [InverseProperty("ExecutiveSupportPersonRelatedMainPeople")]
    public virtual ExecutiveSupportPerson MainPerson { get; set; }

    [ForeignKey("ReliablePersonReasonId")]
    [InverseProperty("ExecutiveSupportPersonRelateds")]
    public virtual ReliablePersonReason ReliablePersonReason { get; set; }
}
