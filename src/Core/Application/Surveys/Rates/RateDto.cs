namespace ZANECO.API.Application.Surveys.Rates;

public class RateDto : BaseDto, IDto
{
    public string Number { get; set; } = default!;
}