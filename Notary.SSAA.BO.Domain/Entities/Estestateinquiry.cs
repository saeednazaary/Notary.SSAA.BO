using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("ESTESTATEINQUIRY")]
public partial class Estestateinquiry
{
    [Column("ACCOUNTEDINREPORT1")]
    [Precision(2)]
    public byte? Accountedinreport1 { get; set; }

    [Column("ACCOUNTEDINREPORT2")]
    [Precision(2)]
    public byte? Accountedinreport2 { get; set; }

    [Column("ADDRESS")]
    [Unicode(false)]
    public string Address { get; set; }

    [Column("APARTMENTSTOTALAREA", TypeName = "NUMBER(20,2)")]
    public decimal? Apartmentstotalarea { get; set; }

    [Column("AREA", TypeName = "NUMBER(20,3)")]
    public decimal? Area { get; set; }

    [Column("ATOMATICALYRESPONSEDBYSERVICE", TypeName = "NUMBER")]
    public decimal? Atomaticalyresponsedbyservice { get; set; }

    [Column("ATTACHEDTODEALSUMMARY")]
    [StringLength(25)]
    [Unicode(false)]
    public string Attachedtodealsummary { get; set; }

    [Column("AWEDESC")]
    [Unicode(false)]
    public string Awedesc { get; set; }

    [Column("BASEDONDOC")]
    [StringLength(1)]
    [Unicode(false)]
    public string Basedondoc { get; set; }

    [Column("BASIC")]
    [StringLength(77)]
    [Unicode(false)]
    public string Basic { get; set; }

    [Column("BASICAPPENDANT")]
    [StringLength(25)]
    [Unicode(false)]
    public string Basicappendant { get; set; }

    [Column("BILLNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string Billno { get; set; }

    [Column("BLOCK")]
    [Precision(5)]
    public short? Block { get; set; }

    [Column("BSTSALEINSTANCEID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Bstsaleinstanceid { get; set; }

    [Column("BSTSERIDAFTARID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Bstseridaftarid { get; set; }

    [Column("CLASSIFFIERNO")]
    [StringLength(25)]
    [Unicode(false)]
    public string Classiffierno { get; set; }

    [Column("CONFIRMPAYCOST")]
    [Precision(3)]
    public byte? Confirmpaycost { get; set; }

    [Column("CONTEXTPLAQUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string Contextplaque { get; set; }

    [Column("DATASIGNATURE", TypeName = "BLOB")]
    public byte[] Datasignature { get; set; }

    [Column("DEALSUMMARYDATE")]
    [StringLength(19)]
    [Unicode(false)]
    public string Dealsummarydate { get; set; }

    [Column("DEALSUMMARYNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Dealsummaryno { get; set; }

    [Column("DEFINITE")]
    [Precision(10)]
    public int? Definite { get; set; }

    [Column("DIRECTION")]
    [StringLength(25)]
    [Unicode(false)]
    public string Direction { get; set; }

    [Column("DIVIDEND")]
    [Precision(4)]
    public byte? Dividend { get; set; }

    [Column("DOCPRINTEDYEAR")]
    [StringLength(10)]
    [Unicode(false)]
    public string Docprintedyear { get; set; }

    [Column("EDECLERATIONDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Edeclerationdate { get; set; }

    [Column("EDECLERATIONNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string Edeclerationno { get; set; }

    [Column("ENMESTATETYPEINQUIRY")]
    [Precision(3)]
    public byte? Enmestatetypeinquiry { get; set; }

    [Column("ENQUIRYDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Enquirydate { get; set; }

    [Column("ENQUIRYNO")]
    [Precision(16)]
    public long? Enquiryno { get; set; }

    [Column("ESTASSUMEDKINDID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estassumedkindid { get; set; }

    [Column("ESTAWEKINDID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estawekindid { get; set; }

    [Column("ESTDOCTYPEID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estdoctypeid { get; set; }

    [Column("ESTESTATEOWNERTYPEID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estestateownertypeid { get; set; }

    [Column("ESTESTATEUSAGEKINDID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estestateusagekindid { get; set; }

    [Column("ESTINQUIRYMETHODID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estinquirymethodid { get; set; }

    [Column("ESTUSAGEDESCID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estusagedescid { get; set; }

    [Column("FACTORDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Factordate { get; set; }

    [Column("FACTORNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string Factorno { get; set; }

    [Column("FINALAPPROVEDTIME")]
    [StringLength(20)]
    [Unicode(false)]
    public string Finalapprovedtime { get; set; }

    [Column("FINALAPPROVER")]
    [StringLength(20)]
    [Unicode(false)]
    public string Finalapprover { get; set; }

    [Column("FIRSTTRTSREADTIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Firsttrtsreadtime { get; set; }

    [Column("FLOOR")]
    [Precision(4)]
    public byte? Floor { get; set; }

    [Column("FOLLOWEINQUIRYNO")]
    [StringLength(16)]
    [Unicode(false)]
    public string Followeinquiryno { get; set; }

    [Column("FOLLOWERINQUIRYDATE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Followerinquirydate { get; set; }

    [Column("FOLLOWINGINQUIRYID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Followinginquiryid { get; set; }

    [Column("GEOLOCATIONID")]
    [Precision(6)]
    public int? Geolocationid { get; set; }

    [Column("HASFOLLOWERINQUIRY", TypeName = "NUMBER")]
    public decimal? Hasfollowerinquiry { get; set; }

    [Column("HASMORTGAGE")]
    [Unicode(false)]
    public string Hasmortgage { get; set; }

    [Column("HASNOTEBOOKAMENDMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string Hasnotebookamendment { get; set; }

    [Column("HASREPLACEDOCUMENT")]
    [StringLength(5)]
    [Unicode(false)]
    public string Hasreplacedocument { get; set; }

    [Column("HASSENDEDDELETEDPERSON")]
    [StringLength(10)]
    [Unicode(false)]
    public string Hassendeddeletedperson { get; set; }

    [Column("HOKM")]
    [StringLength(5)]
    [Unicode(false)]
    public string Hokm { get; set; }

    [Column("HOWTOPAY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Howtopay { get; set; }

    [Key]
    [Column("ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Id { get; set; }

    [Column("ILM")]
    [Precision(4)]
    public byte? Ilm { get; set; }

    [Column("INQUIRYUNIQUEID")]
    [StringLength(18)]
    [Unicode(false)]
    public string Inquiryuniqueid { get; set; }

    [Column("INSERTTIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Inserttime { get; set; }

    [Column("ISCURRENTESTATEINQUIRY", TypeName = "NUMBER")]
    public decimal? Iscurrentestateinquiry { get; set; }

    [Column("ISNOTEBOOKDOCUMENT")]
    [StringLength(5)]
    [Unicode(false)]
    public string Isnotebookdocument { get; set; }

    [Column("LASTDELETEDPERSONLOG")]
    [StringLength(300)]
    [Unicode(false)]
    public string Lastdeletedpersonlog { get; set; }

    [Column("LASTEDITRECEIVETIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Lasteditreceivetime { get; set; }

    [Column("LASTSENDTIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Lastsendtime { get; set; }

    [Column("MENTION")]
    [Unicode(false)]
    public string Mention { get; set; }

    [Column("NOTEBOOKNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string Notebookno { get; set; }

    [Column("NOTEBOOKSERI")]
    [StringLength(32)]
    [Unicode(false)]
    public string Notebookseri { get; set; }

    [Column("NOTEBOOKSUPPLEMENT")]
    [StringLength(25)]
    [Unicode(false)]
    public string Notebooksupplement { get; set; }

    [Column("ONOTARYREGISTERSERVICEREQID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Onotaryregisterservicereqid { get; set; }

    [Column("OWNERMANNER")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ownermanner { get; set; }

    [Column("PAGENO")]
    [StringLength(15)]
    [Unicode(false)]
    public string Pageno { get; set; }

    [Column("PAGENOTESYSTEMNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Pagenotesystemno { get; set; }

    [Column("PAYCOSTDATETIME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Paycostdatetime { get; set; }

    [Column("PAYMENTTYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Paymenttype { get; set; }

    [Column("PICENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Piceno { get; set; }

    [Column("PISHNO")]
    [StringLength(30)]
    [Unicode(false)]
    public string Pishno { get; set; }

    [Column("POSRELATEDITEM")]
    [StringLength(30)]
    [Unicode(false)]
    public string Posrelateditem { get; set; }

    [Column("POSTALCODE")]
    [StringLength(16)]
    [Unicode(false)]
    public string Postalcode { get; set; }

    [Column("PRESELLINQUIRYTYPE", TypeName = "NUMBER")]
    public decimal? Presellinquirytype { get; set; }

    [Column("PRINTEDDOCSNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Printeddocsno { get; set; }

    [Column("PRINTEDSERI")]
    [StringLength(25)]
    [Unicode(false)]
    public string Printedseri { get; set; }

    [Column("PRINTEDSERIAL")]
    [Precision(10)]
    public int? Printedserial { get; set; }

    [Column("PRINTNUMBERDOC", TypeName = "NUMBER(20)")]
    public decimal? Printnumberdoc { get; set; }

    [Column("PRODUCERINQUIRYID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Producerinquiryid { get; set; }

    [Column("PRSESTATEINQUIRYID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Prsestateinquiryid { get; set; }

    [Column("PRSPRESELLESTATEID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Prspresellestateid { get; set; }

    [Column("RECEIPTNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string Receiptno { get; set; }

    [Column("RECORDTYPE")]
    [StringLength(25)]
    [Unicode(false)]
    public string Recordtype { get; set; }

    [Column("REGISTERDATE")]
    [StringLength(19)]
    [Unicode(false)]
    public string Registerdate { get; set; }

    [Column("REGISTERNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Registerno { get; set; }

    [Column("REGISTRATIONTIME")]
    [StringLength(16)]
    [Unicode(false)]
    public string Registrationtime { get; set; }

    [Column("REGULATORYNOTARYOFFICE")]
    [StringLength(80)]
    [Unicode(false)]
    public string Regulatorynotaryoffice { get; set; }

    [Column("RELATEDOWNERSHIPID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Relatedownershipid { get; set; }

    [Column("REPETEDNO")]
    [Precision(16)]
    public long? Repetedno { get; set; }

    [Column("RESPONSE", TypeName = "CLOB")]
    public string Response { get; set; }

    [Column("RESPONSECODE")]
    [StringLength(25)]
    [Unicode(false)]
    public string Responsecode { get; set; }

    [Column("RESPONSEDATE")]
    [StringLength(19)]
    [Unicode(false)]
    public string Responsedate { get; set; }

    [Column("RESPONSEDBYSERVICE", TypeName = "NUMBER")]
    public decimal? Responsedbyservice { get; set; }

    [Column("RESPONSEDIGITALSIGNATURE", TypeName = "BLOB")]
    public byte[] Responsedigitalsignature { get; set; }

    [Column("RESPONSENUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string Responsenumber { get; set; }

    [Column("RESPONSERECEIVETIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Responsereceivetime { get; set; }

    [Column("RESPONSERESULT")]
    [StringLength(25)]
    [Unicode(false)]
    public string Responseresult { get; set; }

    [Column("RESPONSESUBJECTDN")]
    [StringLength(200)]
    [Unicode(false)]
    public string Responsesubjectdn { get; set; }

    [Column("SCRIPTORIUMID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Scriptoriumid { get; set; }

    [Column("SECONDARY")]
    [StringLength(100)]
    [Unicode(false)]
    public string Secondary { get; set; }

    [Column("SECONDARYAPPENDANT")]
    [StringLength(25)]
    [Unicode(false)]
    public string Secondaryappendant { get; set; }

    [Column("SECTIONID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Sectionid { get; set; }

    [Column("SELFINQUIRYID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Selfinquiryid { get; set; }

    [Column("SENDEDBYSABTEANI")]
    [StringLength(32)]
    [Unicode(false)]
    public string Sendedbysabteani { get; set; }

    [Column("SENDEDBYSERVICE", TypeName = "NUMBER")]
    public decimal? Sendedbyservice { get; set; }

    [Column("SENDINGSTATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string Sendingstatus { get; set; }

    [Column("SENDTIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Sendtime { get; set; }

    [Column("SEPARATIONDATE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Separationdate { get; set; }

    [Column("SEPARATIONNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Separationno { get; set; }

    [Column("SEPERATEFROM")]
    [StringLength(500)]
    [Unicode(false)]
    public string Seperatefrom { get; set; }

    [Column("SIGNERUSERID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Signeruserid { get; set; }

    [Column("SMSRECIEVERMOBILENUMBER")]
    [StringLength(15)]
    [Unicode(false)]
    public string Smsrecievermobilenumber { get; set; }

    [Column("SPECIFICSTATUS")]
    [Unicode(false)]
    public string Specificstatus { get; set; }

    [Column("SUBJECTDN")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Subjectdn { get; set; }

    [Column("SUBSECTIONID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Subsectionid { get; set; }

    [Column("SUMPRICES")]
    [Precision(15)]
    public long? Sumprices { get; set; }

    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal? Timestamp { get; set; }

    [Column("TRTSREADTIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string Trtsreadtime { get; set; }

    [Column("UARCHIVISTCONFIRMDATE")]
    [StringLength(16)]
    [Unicode(false)]
    public string Uarchivistconfirmdate { get; set; }

    [Column("UARCHIVISTUSERID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Uarchivistuserid { get; set; }

    [Column("UARRESTNOTECONFIRMDATE")]
    [StringLength(16)]
    [Unicode(false)]
    public string Uarrestnoteconfirmdate { get; set; }

    [Column("UARRESTNOTEUSERID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Uarrestnoteuserid { get; set; }

    [Column("UCONFIRMSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string Uconfirmstatus { get; set; }

    [Column("UESTATENOTECONFIRMDATE")]
    [StringLength(16)]
    [Unicode(false)]
    public string Uestatenoteconfirmdate { get; set; }

    [Column("UESTATENOTEUSERID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Uestatenoteuserid { get; set; }

    [Column("UFINALCONFIRMDATE")]
    [StringLength(16)]
    [Unicode(false)]
    public string Ufinalconfirmdate { get; set; }

    [Column("UFINALCONFIRMUSERID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Ufinalconfirmuserid { get; set; }

    [Column("UNITMESSAGE")]
    [Unicode(false)]
    public string Unitmessage { get; set; }

    [Column("URESPONSENO")]
    [StringLength(19)]
    [Unicode(false)]
    public string Uresponseno { get; set; }
}
