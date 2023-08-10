namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillExportDto : BaseDto, IDto
{
    public string Account { get; set; } = default!;
    public string Meter { get; set; } = default!;
    public string Address { get; set; } = default!;
}