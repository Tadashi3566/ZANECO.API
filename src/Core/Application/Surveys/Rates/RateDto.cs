namespace ZANECO.API.Application.Surveys.Rates;

public class RateDto : DtoExtension, IDto
{
    public string Number { get; set; } = default!;
}