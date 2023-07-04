namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Contribution : AuditableEntity, IAggregateRoot
{
    public string ContributionType { get; private set; } = default!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal RangeStart { get; private set; }
    public decimal RangeEnd { get; private set; }
    public decimal EmployerContribution { get; private set; }
    public decimal EmployeeContribution { get; private set; }
    public decimal TotalContribution { get; private set; }
    public decimal Percentage { get; private set; }

    public bool IsFixed { get; private set; }

    public Contribution(string contributionType, DateTime startDate, DateTime endDate, decimal rangeStart, decimal rangeEnd, decimal employerContribution, decimal employeeContribution, decimal totalContribution, decimal percentage, bool isFixed, string? description = "", string? notes = "")
    {
        ContributionType = contributionType;

        StartDate = startDate;
        EndDate = endDate;

        RangeStart = rangeStart;
        RangeEnd = rangeEnd;

        EmployerContribution = employerContribution;
        EmployeeContribution = employeeContribution;
        TotalContribution = totalContribution;
        Percentage = percentage;

        IsFixed = isFixed;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Contribution Update(string contributionType, DateTime startDate, DateTime endDate, decimal rangeStart, decimal rangeEnd, decimal employerContribution, decimal employeeContribution, decimal totalContribution, decimal percentage, bool isFixed, string? description = "", string? notes = "")
    {
        if (contributionType is not null && !ContributionType.Equals(contributionType)) ContributionType = contributionType;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;

        if (!RangeStart.Equals(rangeStart)) RangeStart = rangeStart;
        if (!RangeEnd.Equals(rangeEnd)) RangeEnd = rangeEnd;

        if (!EmployerContribution.Equals(employerContribution)) EmployerContribution = employerContribution;
        if (!EmployeeContribution.Equals(employeeContribution)) EmployeeContribution = employeeContribution;
        if (!TotalContribution.Equals(totalContribution)) TotalContribution = totalContribution;
        if (!Percentage.Equals(percentage)) Percentage = percentage;

        if (!IsFixed.Equals(isFixed)) IsFixed = isFixed;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}