using ZANECO.API.Application.Surveys.Rates;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public RateDto Rate { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}