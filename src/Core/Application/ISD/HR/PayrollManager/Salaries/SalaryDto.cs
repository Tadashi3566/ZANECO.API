namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string RateType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; private set; } = default!;
    public int IncrementYears { get; set; } = default!;
    public decimal IncrementAmount { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = default!;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}