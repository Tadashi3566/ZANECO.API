namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Inventory : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; } = default!;
    public string MrCode { get; private set; } = default!;
    public string ItemCode { get; private set; } = default!;
    public DateTime DateReceived { get; private set; } = default!;
    public decimal Cost { get; private set; } = default!;

    public Inventory(DefaultIdType employeeId, string mrCode, string itemCode, DateTime dateReceived, string name, decimal cost, string? description = null, string? notes = null, string? imagePath = null)
    {
        EmployeeId = employeeId;

        MrCode = mrCode.Trim().ToUpper();
        ItemCode = itemCode.Trim().ToUpper();

        DateReceived = dateReceived;

        Name = name.Trim().ToUpper();
        Cost = cost;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Inventory Update(string mrCode, string itemCode, DateTime dateReceived, string name, decimal cost, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!MrCode.Equals(mrCode)) MrCode = mrCode.Trim().ToUpper();
        if (!ItemCode.Equals(itemCode)) ItemCode = itemCode.Trim().ToUpper();

        if (!DateReceived.Equals(dateReceived)) DateReceived = dateReceived;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (!Cost.Equals(cost)) Cost = cost;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Inventory ClearFilePath()
    {
        ImagePath = null;

        return this;
    }
}
