namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionDto : BaseDto, IDto
{
    public string ContributionType { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public decimal RangeStart { get; set; } = default!;
    public decimal RangeEnd { get; set; } = default!;
    public decimal EmployerContribution { get; set; } = default!;
    public decimal EmployeeContribution { get; set; } = default!;
    public decimal TotalContribution { get; set; } = default!;
    public decimal Percentage { get; set; } = default!;
    public bool IsFixed { get; set; } = default!;
}