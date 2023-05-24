namespace ZANECO.API.Application.CAD.Areas;

public class AreaDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}