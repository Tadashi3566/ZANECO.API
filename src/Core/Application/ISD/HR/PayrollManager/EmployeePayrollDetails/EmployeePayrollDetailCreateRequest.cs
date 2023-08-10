using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailCreateRequest : BaseRequest, IRequest<Guid>
{
    public Guid EmployeeId { get; set; } = default!;
    public Guid PayrollId { get; set; } = default!;
    public Guid AdjustmentId { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string Contributor { get; set; } = default!;
}

public class EmployeePayrollDetailCreateRequestValidator : CustomValidator<EmployeePayrollDetailCreateRequest>
{
    public EmployeePayrollDetailCreateRequestValidator()
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

public class EmployeePayrollDetailCreateRequestHandler : IRequestHandler<EmployeePayrollDetailCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Payroll> _repoPayroll;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<EmployeePayrollDetail> _repoEmployeePayrollDetail;

    public EmployeePayrollDetailCreateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Payroll> repoPayroll, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<EmployeePayrollDetail> repoEmployeePayrollDetail) =>
        (_repoEmployee, _repoPayroll, _repoAdjustment, _repoEmployeePayrollDetail) = (repoEmployee, repoPayroll, repoAdjustment, repoEmployeePayrollDetail);

    public async Task<Guid> Handle(EmployeePayrollDetailCreateRequest request, CancellationToken cancellationToken)
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

        var employeePayrollDetail = new EmployeePayrollDetail(request.EmployeeId, employee.NameFull(), request.PayrollId, payroll.Name, request.AdjustmentId, adjustment.AdjustmentType, adjustment.Name, request.Amount, payroll.StartDate, payroll.EndDate, payroll.PayrollDate, "EMPLOYEE", request.Description, request.Notes);

        await _repoEmployeePayrollDetail.AddAsync(employeePayrollDetail, cancellationToken);

        return employeePayrollDetail.Id;
    }
}