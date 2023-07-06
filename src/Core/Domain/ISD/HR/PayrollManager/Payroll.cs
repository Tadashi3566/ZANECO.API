namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Payroll : AuditableEntity, IAggregateRoot
{
    public Payroll()
    {
    }

    public string PayrollType { get; private set; } = default!; // FULL MONTH, MONTH-MID & MONTH-END
    public string EmploymentType { get; private set; } = default!; // REGULAR, JO etc.
    public string Name { get; private set; } = default!;
    public decimal TotalSalary { get; private set; }
    public decimal TotalAdditional { get; private set; }
    public decimal TotalGross { get; private set; }
    public decimal TotalDeduction { get; private set; }
    public decimal TotalNet { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime PayrollDate { get; private set; }
    public int WorkingDays { get; private set; }
    public bool IsClosed { get; private set; }

    public Payroll(string payrollType, string employmentType, string name, decimal totalSalary, decimal totalAdditional, decimal totalGross, decimal totalDeduction, decimal totalNet, DateTime startDate, DateTime endDate, int workingDays, DateTime payrollDate, string? description = "", string? notes = "")
    {
        PayrollType = payrollType;
        EmploymentType = employmentType;
        Name = name.Trim().ToUpper();

        TotalSalary = totalSalary;
        TotalAdditional = totalAdditional;
        TotalGross = totalGross;
        TotalDeduction = totalDeduction;
        TotalNet = totalNet;

        StartDate = startDate;
        EndDate = endDate;
        WorkingDays = workingDays;
        PayrollDate = payrollDate;

        if (description is not null) Description = description!.Trim();
        if (notes is not null) Notes = notes!.Trim();
    }

    public Payroll Update(string payrollType, string employmentType, string name, decimal totalSalary, decimal totalAdditional, decimal totalGross, decimal totalDeduction, decimal totalNet, DateTime startDate, DateTime endDate, int workingDays, DateTime payrollDate, bool isClosed, string? description = "", string? notes = "")
    {
        if (payrollType is not null && !PayrollType.Equals(payrollType)) PayrollType = payrollType;
        if (employmentType is not null && !EmploymentType.Equals(employmentType)) EmploymentType = employmentType;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (!TotalSalary.Equals(totalSalary)) TotalSalary = totalSalary;
        if (!TotalAdditional.Equals(totalAdditional)) TotalAdditional = totalAdditional;
        if (!TotalGross.Equals(totalGross)) TotalGross = totalGross;
        if (!TotalDeduction.Equals(totalDeduction)) TotalDeduction = totalDeduction;
        if (!TotalNet.Equals(totalNet)) TotalNet = totalNet;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;
        if (!WorkingDays.Equals(workingDays)) WorkingDays = workingDays;
        if (!PayrollDate.Equals(payrollDate)) PayrollDate = payrollDate;

        if (!IsClosed.Equals(isClosed)) IsClosed = isClosed;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}