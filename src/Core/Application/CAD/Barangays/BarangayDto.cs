namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType AreaId { get; set; } = default!;
    public string AreaName { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}