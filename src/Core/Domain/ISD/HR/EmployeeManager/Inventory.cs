namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Inventory : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; } = default!;

    public string MrNumber { get; private set; } = default!;
    public string ItemNumber { get; private set; } = default!;
    public DateTime DateReceived { get; private set; } = default!;
}
