using ZANECO.API.Domain.App;

namespace ZANECO.API.Domain.CAD;

public class PowerConsumption : AuditableEntity, IAggregateRoot
{
    public PowerConsumption()
    {
    }

    public virtual Group Group { get; private set; } = default!;
    public Guid GroupId { get; private set; }
    public string GroupCode { get; private set; } = default!;
    public string GroupName { get; private set; } = default!;
    public string BillMonth { get; private set; } = default!;
    public decimal KWHPurchased { get; private set; }

    public PowerConsumption(Guid groupId, string groupCode, string groupName, string billMonth, decimal kwhPurchased, string? description = "", string? notes = "")
    {
        GroupId = groupId;
        GroupCode = groupCode;
        GroupName = groupName;

        BillMonth = billMonth;
        KWHPurchased = kwhPurchased;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public PowerConsumption Update(string groupCode, string groupName, string billMonth, decimal kWHPurchased, string? description = "", string? notes = "")
    {
        if (groupCode is not null && GroupCode != groupCode) GroupCode = groupCode;
        if (groupName is not null && GroupName != groupName) GroupName = groupName;

        if (billMonth is not null && BillMonth != billMonth) BillMonth = billMonth;
        if (!KWHPurchased.Equals(kWHPurchased)) KWHPurchased = kWHPurchased;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}