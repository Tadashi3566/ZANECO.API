namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType RateId { get; set; }
    public string RateNumber { get; set; } = default!;
    public string RateName { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}