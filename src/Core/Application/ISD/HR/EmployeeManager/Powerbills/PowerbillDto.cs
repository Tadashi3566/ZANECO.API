namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

    public string Account { get; set; } = default!;
    public string Meter { get; set; } = default!;
    public string Address { get; set; } = default!;
}