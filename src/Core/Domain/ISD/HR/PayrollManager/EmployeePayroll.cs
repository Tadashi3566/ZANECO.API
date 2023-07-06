using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class EmployeePayroll : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public virtual Payroll Payroll { get; private set; } = default!;
    public DefaultIdType PayrollId { get; private set; }
    public string PayrollName { get; private set; } = default!;

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime PayrollDate { get; private set; }
    public decimal Salary { get; private set; }
    public decimal Additional { get; private set; }
    public decimal Gross { get; private set; }
    public decimal Deduction { get; private set; }
    public decimal Net { get; private set; }

    public EmployeePayroll(DefaultIdType employeeId, string employeeName, DefaultIdType payrollId, string payrollName, decimal salary, decimal additional, decimal gross, decimal deduction, decimal net, DateTime startDate, DateTime endDate, DateTime payrollDate, string? description = "", string? notes = "")
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        PayrollId = payrollId;
        PayrollName = payrollName;

        Salary = salary;
        Additional = additional;
        Gross = gross;
        Deduction = deduction;
        Net = net;

        StartDate = startDate;
        EndDate = endDate;
        PayrollDate = payrollDate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public EmployeePayroll Update(string employeeName, string payrollName, decimal salary, decimal additional, decimal gross, decimal deduction, decimal net, DateTime startDate, DateTime endDate, DateTime payrollDate, string? description = "", string? notes = "")
    {
        if (employeeName is not null && !EmployeeName.Equals(employeeName)) EmployeeName = employeeName;
        if (payrollName is not null && !PayrollName.Equals(payrollName)) PayrollName = payrollName;

        if (!Salary.Equals(salary)) Salary = salary;
        if (!Additional.Equals(additional)) Additional = additional;
        if (!Gross.Equals(gross)) Gross = gross;
        if (!Deduction.Equals(deduction)) Deduction = deduction;
        if (!Net.Equals(net)) Net = net;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;
        if (!PayrollDate.Equals(payrollDate)) PayrollDate = payrollDate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}