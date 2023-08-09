using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollUpdateRequest : RequestExtension<EmployeePayrollUpdateRequest>, IRequest<Guid>
{
    public Guid EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public Guid PayrollId { get; set; } = default!;
    public string PayrollName { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? PayrollDate { get; set; }
    public decimal Salary { get; set; } = default!;
    public decimal Additional { get; set; } = default!;
    public decimal Gross { get; set; } = default!;
    public decimal Deduction { get; set; } = default!;
    public decimal Net { get; set; } = default!;
}

public class EmployeePayrollUpdateRequestValidator : CustomValidator<EmployeePayrollUpdateRequest>
{
    public EmployeePayrollUpdateRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();

        RuleFor(p => p.PayrollId)
            .NotEmpty();
    }
}

public class EmployeePayrollUpdateRequestHandler : IRequestHandler<EmployeePayrollUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Payroll> _repoPayroll;
    private readonly IRepositoryWithEvents<EmployeePayroll> _repoEmployeePayroll;
    private readonly IStringLocalizer<EmployeePayrollUpdateRequestHandler> _localizer;

    public EmployeePayrollUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Payroll> repoPayroll, IRepositoryWithEvents<EmployeePayroll> repoEmployeePayroll, IStringLocalizer<EmployeePayrollUpdateRequestHandler> localizer) =>
        (_repoEmployee, _repoPayroll, _repoEmployeePayroll, _localizer) = (repoEmployee, repoPayroll, repoEmployeePayroll, localizer);

    public async Task<Guid> Handle(EmployeePayrollUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");
        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        // Get Payroll Information
        var payroll = await _repoPayroll.GetByIdAsync(request.PayrollId, cancellationToken);
        _ = payroll ?? throw new NotFoundException($"Payroll {request.PayrollId} not found.");

        var employeePayroll = await _repoEmployeePayroll.GetByIdAsync(request.Id, cancellationToken);
        _ = employeePayroll ?? throw new NotFoundException($"Employee Payroll {request.Id} not found.");

        var updatedEmployeePayroll = employeePayroll.Update(employee.NameFull(), payroll.Name, request.Salary, request.Additional, request.Gross, request.Deduction, request.Net, payroll.StartDate, payroll.EndDate, payroll.PayrollDate, request.Description, request.Notes);

        await _repoEmployeePayroll.UpdateAsync(updatedEmployeePayroll, cancellationToken);

        return request.Id;
    }
}