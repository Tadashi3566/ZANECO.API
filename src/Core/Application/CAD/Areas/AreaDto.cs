namespace ZANECO.API.Application.CAD.Areas;

public class AreaDto : BaseDto, IDto
{
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
}