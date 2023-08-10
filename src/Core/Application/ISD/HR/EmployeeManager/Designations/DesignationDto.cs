namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string IdNumber { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
    public DefaultIdType DesignationId { get; set; } = default!;
    public DefaultIdType ScheduleId { get; set; } = default!;
    public string ScheduleName { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Area { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Division { get; set; } = default!;
    public string Section { get; set; } = default!;
    public string Position { get; set; } = default!;

    public string EmploymentType { get; set; } = default!;
    public int SalaryNumber { get; set; } = default!;
    public string SalaryName { get; set; } = default!;
    public decimal SalaryAmount { get; set; } = default!;
    public string RateType { get; set; } = default!;
    public string PayType { get; set; } = default!;
    public int DaysPerMonth { get; set; } = 26;
    public int HoursPerDay { get; set; } = 8;
    public decimal RatePerDay { get; set; } = default!;
    public decimal RatePerHour { get; set; } = default!;
    public string TaxType { get; set; } = default!;
}