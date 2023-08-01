namespace ZANECO.API.Domain.AGMA;

public class Master2022 : AuditableEntity<int>, IAggregateRoot
{
    public string? AccountNumber { get; }
    public string? area { get; }
    public string? area_str { get; }
    public string? District { get; }
    public string? address { get; }
    public string? contact_number { get; }
    public bool? is_registered { get; }
    public string? verification_code { get; }
}