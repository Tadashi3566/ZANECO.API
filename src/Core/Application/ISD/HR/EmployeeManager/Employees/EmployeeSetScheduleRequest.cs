using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeSetScheduleRequest : IRequest<bool>
{
    public Guid ScheduleId { get; set; }
}

public class EmployeeSetScheduleRequestValidator : CustomValidator<EmployeeSetScheduleRequest>
{
    public EmployeeSetScheduleRequestValidator()
    {
        RuleFor(p => p.ScheduleId)
            .NotEmpty();
    }
}

public class EmployeeSetScheduleRequestHandler : IRequestHandler<EmployeeSetScheduleRequest, bool>
{
    private readonly IReadRepository<Designation> _repoDesignation;
    private readonly IRepositoryWithEvents<Employee> _repoEmployee;
    private readonly IStringLocalizer<EmployeeSetScheduleRequestHandler> _localizer;

    public EmployeeSetScheduleRequestHandler(IReadRepository<Designation> repoDesignation, IRepositoryWithEvents<Employee> repoEmployee, IStringLocalizer<EmployeeSetScheduleRequestHandler> localizer) =>
        (_repoDesignation, _repoEmployee, _localizer) = (repoDesignation, repoEmployee, localizer);

    public async Task<bool> Handle(EmployeeSetScheduleRequest request, CancellationToken cancellationToken)
    {
        var schedule = await _repoDesignation.GetByIdAsync(request.ScheduleId, cancellationToken);

        _ = schedule ?? throw new NotFoundException(string.Format(_localizer["schedule not found."], request.ScheduleId));

        var employeesToBeUpdated = await _repoEmployee.ListAsync(new EmployeeByEmptyScheduleIdSpec(), cancellationToken);

        // foreach (var employee in employeesToBeUpdated)
        // {
        //    Employee? emp = _repoEmployee.GetByIdAsync(new EmployeeByIdSpec(employee.Id));
        //    var updatedEmployee = emp.Schedule();
        // }

        return true;
    }
}