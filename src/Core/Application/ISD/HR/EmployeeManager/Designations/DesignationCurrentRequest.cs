using ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;
using ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationCurrentRequest : IRequest<bool>
{
    public Guid EmployeeId { get; set; } = default!;
    public Guid DesignationId { get; set; } = default!;
}

public class DesignationCurrentRequestValidator : CustomValidator<DesignationCurrentRequest>
{
    public DesignationCurrentRequestValidator(IReadRepository<Employee> repoEmployee, IReadRepository<Designation> repoDesignation, IStringLocalizer<DesignationCurrentRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.DesignationId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoDesignation.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["designation not found."], id));
    }
}

public class DesignationCurrentRequestHandler : IRequestHandler<DesignationCurrentRequest, bool>
{
    private readonly IReadRepository<Salary> _repoSalary;
    private readonly IReadRepository<Schedule> _repoSchedule;
    private readonly IRepositoryWithEvents<Designation> _repoDesignation;
    private readonly IRepositoryWithEvents<Employee> _repoEmployee;
    private readonly IDapperRepository _dapper;
    private readonly IStringLocalizer<DesignationCurrentRequestValidator> _localizer;

    public DesignationCurrentRequestHandler(IReadRepository<Salary> repoRank, IReadRepository<Schedule> repoSchedule, IRepositoryWithEvents<Designation> repoDesignation, IRepositoryWithEvents<Employee> repoEmployee, IDapperRepository dapper, IStringLocalizer<DesignationCurrentRequestValidator> localizer) =>
        (_repoSalary, _repoSchedule, _repoDesignation, _repoEmployee, _dapper, _localizer) = (repoRank, repoSchedule, repoDesignation, repoEmployee, dapper, localizer);

    public async Task<bool> Handle(DesignationCurrentRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        // Get Designation Information
        var designation = await _repoDesignation.GetByIdAsync(request.DesignationId, cancellationToken);
        _ = designation ?? throw new NotFoundException($"Designation {request.DesignationId} not found.");

        // Get Rank Information
        var salary = await _repoSalary.FirstOrDefaultAsync(new SalaryByNumberSpec(designation.SalaryNumber), cancellationToken);
        _ = salary ?? throw new NotFoundException($"Salary {designation.SalaryNumber} not found.");

        // Get Schedule Information
        var schedule = await _repoSchedule.FirstOrDefaultAsync(new ScheduleByIdSpec(designation.ScheduleId), cancellationToken);
        _ = schedule ?? throw new NotFoundException($"Schedule {designation.ScheduleId} not found.");

        // Set all Employee Designations as Active=false
        await _dapper.ExecuteScalarAsync<Designation>($"UPDATE datazaneco.designations SET IsActive = 0 WHERE EmployeeId LIKE '{employee.Id}'", cancellationToken: cancellationToken);

        // Update Employee Designation
        var employeeDesignation = employee.Designation(request.DesignationId, designation.StartDate, employee.RegularDate, designation.Area, designation.Department, designation.Division, designation.Section, designation.Position, designation.EmploymentType, salary.Number, salary.Name, salary.Amount, salary.IncrementAmount, designation.RateType, designation.HoursPerDay, designation.TaxType, designation.PayType, schedule.Id, schedule.Name, designation.EndDate > DateTime.Today);
        await _repoEmployee.UpdateAsync(employeeDesignation, cancellationToken);

        // Set current Designation as IsActive=true
        var updatedDesignation = designation.Activate();
        await _repoDesignation.UpdateAsync(updatedDesignation, cancellationToken);

        return true;
    }
}