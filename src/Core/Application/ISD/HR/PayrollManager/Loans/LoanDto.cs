namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DefaultIdType AdjustmentId { get; set; }
    public string AdjustmentName { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime DateReleased { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public int Months { get; set; } = default!;
    public decimal Ammortization { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
}