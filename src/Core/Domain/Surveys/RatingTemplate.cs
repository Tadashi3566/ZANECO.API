namespace ZANECO.API.Domain.Surveys;

public class RatingTemplate : AuditableEntity, IAggregateRoot
{
    public virtual Rate Rate { get; private set; } = default!;
    public DefaultIdType RateId { get; private set; }
    public string Comment { get; private set; } = default!;

    public RatingTemplate(DefaultIdType rateId, string comment, string? description = "", string? notes = "")
    {
        RateId = rateId;
        Comment = comment;
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public RatingTemplate Update(DefaultIdType? rateID, string comment, string? description = "", string? notes = "")
    {
        if (rateID.HasValue && rateID.Value != DefaultIdType.Empty && !RateId.Equals(rateID.Value)) RateId = rateID.Value;
        if (comment is not null && !Comment.Equals(comment)) Comment = comment;
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        return this;
    }
}