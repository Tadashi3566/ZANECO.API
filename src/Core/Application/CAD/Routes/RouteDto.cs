namespace ZANECO.API.Application.CAD.Routes;

public class RouteDto : DtoExtension, IDto
{
    public DefaultIdType AreaId { get; set; } = default!;
    public string AreaName { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;


}