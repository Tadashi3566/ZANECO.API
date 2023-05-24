using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public EmployeeDto Employee { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public string? Area { get; set; }
    public string? Department { get; set; }
    public string? Division { get; set; }
    public string? Section { get; set; }
    public string? Position { get; set; }

    public string? EmploymentType { get; set; }
    public int SalaryNumber { get; set; }
    public decimal SalaryAmount { get; set; }
    public string? RateType { get; set; }
    public int DaysPerMonth { get; set; }
    public decimal RatePerDay { get; set; }
    public int HoursPerDay { get; set; }
    public decimal RatePerHour { get; set; }
    public string? TaxType { get; set; }
    public string? PayType { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}