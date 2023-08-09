namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayDto : DtoExtension<BarangayDto>, IDto
{
    public DefaultIdType AreaId { get; set; } = default!;
    public string AreaName { get; set; } = default!;


}