namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillExportDto : IDto
{
    public string Account { get; set; } = default!;
    public string Meter { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}