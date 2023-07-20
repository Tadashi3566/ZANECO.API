using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid PayrollId { get; set; }
    public Guid AdjustmentId { get; set; }
    public decimal Amount { get; set; } = default!;
    public string Contributor { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class EmployeePayrollDetailUpdateRequestValidator : CustomValidator<EmployeePayrollDetailUpdateRequest>
{
    public EmployeePayrollDetailUpdateRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();

        RuleFor(p => p.PayrollId)
            .NotEmpty();

        RuleFor(p => p.AdjustmentId)
            .NotEmpty();

        RuleFor(p => p.Amount)
            .GreaterThan(0);
    }
}

public class EmployeePayrollDetailUpdateRequestHandler : IRequestHandler<EmployeePayrollDetailUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Payroll> _repoPayroll;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<EmployeePayrollDetail> _repoEmployeePayrollDetail;
    private readonly IStringLocalizer<EmployeePayrollDetailUpdateRequestHandler> _localizer;

    public EmployeePayrollDetailUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Payroll> repoPayroll, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<EmployeePayrollDetail> repoEmployeePayrollDetail, IStringLocalizer<EmployeePayrollDetailUpdateRequestHandler> localizer) =>
        (_repoEmployee, _repoPayroll, _repoAdjustment, _repoEmployeePayrollDetail, _localizer) = (repoEmployee, repoPayroll, repoAdjustment, repoEmployeePayrollDetail, localizer);

    public async Task<Guid> Handle(EmployeePayrollDetailUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");
        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        // Get Payroll Information
        var payroll = await _repoPayroll.GetByIdAsync(request.PayrollId, cancellationToken);
        _ = payroll ?? throw new NotFoundException($"Payroll {request.PayrollId} not found.");

        // Get Adjustment Information
        var adjustment = await _repoAdjustment.GetByIdAsync(request.AdjustmentId, cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.AdjustmentId} not found.");

        var employeePayrollDetail = await _repoEmployeePayrollDetail.GetByIdAsync(request.Id, cancellationToken);

        _ = employeePayrollDetail ?? throw new NotFoundException($"EmployeePayrollDetail {request.Id} not found.");

        var updatedEmployeePayrollDetail = employeePayrollDetail.Update(employee.NameFull(), payroll.Name, adjustment.Name, request.Amount, payroll.StartDate, payroll.EndDate, payroll.PayrollDate, "EMPLOYEE", request.Description, request.Notes);

        await _repoEmployeePayrollDetail.UpdateAsync(updatedEmployeePayrollDetail, cancellationToken);

        return request.Id;
    }
}