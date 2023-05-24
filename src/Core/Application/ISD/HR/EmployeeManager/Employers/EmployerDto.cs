namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Designation { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}