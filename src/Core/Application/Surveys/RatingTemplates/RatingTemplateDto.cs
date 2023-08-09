namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateDto : DtoExtension<RatingTemplateDto>, IDto
{
    public DefaultIdType RateId { get; set; }
    public string RateNumber { get; set; } = default!;
    public string RateName { get; set; } = default!;
    public string Comment { get; set; } = default!;
}