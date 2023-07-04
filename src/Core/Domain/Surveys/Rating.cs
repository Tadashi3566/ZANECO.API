namespace ZANECO.API.Domain.Surveys;

public class Rating : AuditableEntity, IAggregateRoot
{
    public Rating()
    {
    }

    public virtual Rate Rate { get; set; } = default!;
    public DefaultIdType RateId { get; private set; }
    public int RateNumber { get; private set; }
    public string RateName { get; private set; } = default!;
    public string Comment { get; private set; } = string.Empty;
    public string Reference { get; private set; } = string.Empty;

    public Rating(DefaultIdType rateId, int rateNumber, string rateName, string comment, string reference, string? description = "", string? notes = "")
    {
        RateId = rateId;
        RateNumber = rateNumber;
        RateName = rateName;
        Comment = comment;
        Reference = reference;
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Rating Update(int rateNumber, string rateName, string comment, string reference, string? description = "", string? notes = "")
    {
        if (rateNumber is not 0 && !RateNumber.Equals(rateNumber)) RateNumber = rateNumber;
        if (rateName is not null && !RateName.Equals(rateName)) RateName = rateName;
        if (comment is not null && !Comment.Equals(comment)) Comment = comment;
        if (reference is not null && !Reference.Equals(reference)) Reference = reference;
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        return this;
    }
}