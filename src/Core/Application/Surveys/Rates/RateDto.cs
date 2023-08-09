namespace ZANECO.API.Application.Surveys.Rates;

public class RateDto : DtoExtension<RateDto>, IDto
{
    public string Number { get; set; } = default!;
}