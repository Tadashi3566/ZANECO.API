namespace ZANECO.API.Application.CAD.Areas;

public class AreaDto : DtoExtension, IDto
{
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;


}