namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateExportDto : DtoExtension<RatingTemplateExportDto>, IDto
{
    public string RateNumber { get; set; } = default!;
    public string RateName { get; set; } = default!;
    public string Comment { get; set; } = default!;
}