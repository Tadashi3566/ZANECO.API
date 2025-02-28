using ZANECO.API.Application.Surveys.Rates;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateDetailsDto : BaseDto, IDto
{
    public RateDto Rate { get; set; } = default!;
    public string Comment { get; set; } = default!;
}