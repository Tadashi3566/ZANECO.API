namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollDto : DtoExtension, IDto
{
    public string PayrollType { get; set; } = default!; // FULL MONTH, MONTH-MID & MONTH-END
    public string EmploymentType { get; set; } = default!;
    public decimal TotalSalary { get; set; } = default!;
    public decimal TotalAdditional { get; set; } = default!;
    public decimal TotalGross { get; set; } = default!;
    public decimal TotalDeduction { get; set; } = default!;
    public decimal TotalNet { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public int WorkingDays { get; set; } = default!;
    public DateTime PayrollDate { get; set; } = default!;
    public bool IsClosed { get; set; }
}