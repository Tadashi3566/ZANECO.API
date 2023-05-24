namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Adjustment : AuditableEntity, IAggregateRoot
{
    public DefaultIdType GroupId { get; private set; } = default!;
    public string AdjustmentType { get; private set; } = default!;
    public string EmployeeType { get; private set; } = default!;
    public int Number { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public decimal Amount { get; private set; } = default!;

    public string PaymentSchedule { get; private set; } = default!;
    public bool IsOptional { get; private set; }
    public bool IsLoan { get; private set; }
    public bool IsActive { get; private set; }

    public Adjustment(DefaultIdType groupId, string adjustmentType, string employeeType, int number, string name, decimal amount, string paymentSchedule, bool isOptional, bool isLoan, bool isActive, string? description = "", string? notes = "")
    {
        GroupId = groupId;

        AdjustmentType = adjustmentType;
        EmployeeType = employeeType;
        Number = number;
        Name = name.Trim().ToUpper();
        Amount = amount;

        PaymentSchedule = paymentSchedule;

        IsOptional = isOptional;
        IsLoan = isLoan;
        IsActive = isActive;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
    }

    public Adjustment Update(DefaultIdType? groupId, string adjustmentType, string employeeType, int number, string name, decimal amount, string paymentSchedule, bool isOptional, bool isLoan, bool isActive, string? description = "", string? notes = "")
    {
        if (groupId.HasValue && groupId.Value != DefaultIdType.Empty && !GroupId.Equals(groupId.Value)) GroupId = groupId.Value;

        if (!AdjustmentType.Equals(adjustmentType)) AdjustmentType = adjustmentType;
        if (!EmployeeType.Equals(employeeType)) EmployeeType = employeeType;
        if (!Number.Equals(number)) Number = number;
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Amount.Equals(amount)) Amount = amount;

        if (!PaymentSchedule.Equals(paymentSchedule)) PaymentSchedule = paymentSchedule;

        if (!IsOptional.Equals(isOptional)) IsOptional = isOptional;
        if (!IsLoan.Equals(isLoan)) IsLoan = isLoan;
        if (!IsActive.Equals(isActive)) IsActive = isActive;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        return this;
    }
}