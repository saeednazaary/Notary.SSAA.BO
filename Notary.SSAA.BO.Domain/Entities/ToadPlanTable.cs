using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Keyless]
[Table("TOAD_PLAN_TABLE")]
public partial class ToadPlanTable
{
    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("STATEMENT_ID")]
    [StringLength(30)]
    [Unicode(false)]
    public string StatementId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("PLAN_ID", TypeName = "NUMBER")]
    public decimal? PlanId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "DATE")]
    public DateTime? Timestamp { get; set; }

    [Column("REMARKS")]
    [Unicode(false)]
    public string Remarks { get; set; }

    [Column("OPERATION")]
    [StringLength(30)]
    [Unicode(false)]
    public string Operation { get; set; }

    [Column("OPTIONS")]
    [StringLength(255)]
    [Unicode(false)]
    public string Options { get; set; }

    [Column("OBJECT_NODE")]
    [StringLength(128)]
    [Unicode(false)]
    public string ObjectNode { get; set; }

    [Column("OBJECT_OWNER")]
    [StringLength(128)]
    [Unicode(false)]
    public string ObjectOwner { get; set; }

    [Column("OBJECT_NAME")]
    [StringLength(128)]
    [Unicode(false)]
    public string ObjectName { get; set; }

    [Column("OBJECT_ALIAS")]
    [StringLength(65)]
    [Unicode(false)]
    public string ObjectAlias { get; set; }

    [Column("OBJECT_INSTANCE", TypeName = "NUMBER(38)")]
    public decimal? ObjectInstance { get; set; }

    [Column("OBJECT_TYPE")]
    [StringLength(30)]
    [Unicode(false)]
    public string ObjectType { get; set; }

    [Column("OPTIMIZER")]
    [StringLength(255)]
    [Unicode(false)]
    public string Optimizer { get; set; }

    [Column("SEARCH_COLUMNS", TypeName = "NUMBER")]
    public decimal? SearchColumns { get; set; }

    /// <summary>
    /// شناسه
    /// </summary>
    [Column("ID", TypeName = "NUMBER(38)")]
    public decimal? Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("PARENT_ID", TypeName = "NUMBER(38)")]
    public decimal? ParentId { get; set; }

    [Column("DEPTH", TypeName = "NUMBER(38)")]
    public decimal? Depth { get; set; }

    [Column("POSITION", TypeName = "NUMBER(38)")]
    public decimal? Position { get; set; }

    [Column("COST", TypeName = "NUMBER(38)")]
    public decimal? Cost { get; set; }

    [Column("CARDINALITY", TypeName = "NUMBER(38)")]
    public decimal? Cardinality { get; set; }

    [Column("BYTES", TypeName = "NUMBER(38)")]
    public decimal? Bytes { get; set; }

    [Column("OTHER_TAG")]
    [StringLength(255)]
    [Unicode(false)]
    public string OtherTag { get; set; }

    [Column("PARTITION_START")]
    [StringLength(255)]
    [Unicode(false)]
    public string PartitionStart { get; set; }

    [Column("PARTITION_STOP")]
    [StringLength(255)]
    [Unicode(false)]
    public string PartitionStop { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("PARTITION_ID", TypeName = "NUMBER(38)")]
    public decimal? PartitionId { get; set; }

    [Column("OTHER", TypeName = "LONG")]
    public string Other { get; set; }

    [Column("DISTRIBUTION")]
    [StringLength(30)]
    [Unicode(false)]
    public string Distribution { get; set; }

    [Column("CPU_COST", TypeName = "NUMBER(38)")]
    public decimal? CpuCost { get; set; }

    [Column("IO_COST", TypeName = "NUMBER(38)")]
    public decimal? IoCost { get; set; }

    [Column("TEMP_SPACE", TypeName = "NUMBER(38)")]
    public decimal? TempSpace { get; set; }

    [Column("ACCESS_PREDICATES")]
    [Unicode(false)]
    public string AccessPredicates { get; set; }

    [Column("FILTER_PREDICATES")]
    [Unicode(false)]
    public string FilterPredicates { get; set; }

    [Column("PROJECTION")]
    [Unicode(false)]
    public string Projection { get; set; }

    [Column("TIME", TypeName = "NUMBER(38)")]
    public decimal? Time { get; set; }

    [Column("QBLOCK_NAME")]
    [StringLength(128)]
    [Unicode(false)]
    public string QblockName { get; set; }

    [Column("OTHER_XML", TypeName = "CLOB")]
    public string OtherXml { get; set; }
}
