namespace ZANECO.API.Application.Surveys.Rates;

public class RateDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}