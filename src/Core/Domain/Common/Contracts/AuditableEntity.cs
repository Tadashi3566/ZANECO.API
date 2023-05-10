using System.ComponentModel.DataAnnotations.Schema;

namespace ZANECO.API.Domain.Common.Contracts;

public abstract class AuditableEntity : AuditableEntity<DefaultIdType>
{
}

public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
{
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; private set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DefaultIdType? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.Now;
        LastModifiedOn = DateTime.Now;
    }
}

public abstract class AuditableEntityWithApproval<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
{
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; private set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }

    [Column(TypeName = "VARCHAR(16)")]
    public string Status { get; set; } = "PENDING";

    public DefaultIdType? RecommendedBy { get; set; }
    public string? RecommenderName { get; set; }
    public DateTime? RecommendedOn { get; set; }

    public DefaultIdType? ApprovedBy { get; set; }
    public string? ApproverName { get; set; }
    public DateTime? ApprovedOn { get; set; }

    public DefaultIdType? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    protected AuditableEntityWithApproval()
    {
        CreatedOn = DateTime.Now;
        LastModifiedOn = DateTime.Now;
    }
}