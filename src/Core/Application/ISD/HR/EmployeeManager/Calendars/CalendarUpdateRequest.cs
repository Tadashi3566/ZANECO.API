using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; } = default!;

    public DateTime CalendarDate { get; set; }
    public string CalendarType { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CalendarUpdateRequestValidator : CustomValidator<CalendarUpdateRequest>
{
    public CalendarUpdateRequestValidator(IReadRepository<Calendar> repoCalendar, IStringLocalizer<CalendarUpdateRequestValidator> localizer)
    {
        //RuleFor(p => p.EmployeeId)
        //    .NotEmpty();

        RuleFor(p => p.CalendarType)
            .NotEmpty();

        RuleFor(p => p.Name)
           .NotEmpty();

        // .MaximumLength(32)
        // .MustAsync(async (calendar, name, ct) => await repoCalendar.FirstOrDefaultAsync(new CalendarByNameSpec(name), ct)
        //            is not Calendar existingCalendar || existingCalendar.Id == calendar.Id)
        // .WithMessage((_, name) => string.Format(localizer["Calendar already exists"], name))
    }
}

public class CalendarUpdateRequestHandler : IRequestHandler<CalendarUpdateRequest, DefaultIdType>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Calendar> _repoCalendar;
    private readonly IStringLocalizer<CalendarUpdateRequestHandler> _localizer;

    public CalendarUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Calendar> repoCalendar, IStringLocalizer<CalendarUpdateRequestHandler> localizer) =>
        (_repoEmployee, _repoCalendar, _localizer) = (repoEmployee, repoCalendar, localizer);

    public async Task<DefaultIdType> Handle(CalendarUpdateRequest request, CancellationToken cancellationToken)
    {
        var calendar = await _repoCalendar.GetByIdAsync(request.Id, cancellationToken);
        _ = calendar ?? throw new NotFoundException(string.Format(_localizer["Calendar not found."], request.Id));

        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("Employee not found.");

        if (!employee.IsActive) throw new Exception("Employee is no longer Active");

        //DefaultIdType employeeId;
        //string employeeName = string.Empty;

        //if (request.Subject.Contains("HOLIDAY"))
        //{
        //    employeeId = Guid.Parse("08da447f-6575-4e91-8be4-b5ddefef61d0");
        //}
        //else
        //{
        //    employeeId = request.EmployeeId;
        //    employeeName = employee!.NameFullInitial();
        //}

        var updatedCalendar = calendar.Update(request.CalendarType, request.CalendarDate!, request.Name, request.Description, request.Notes);

        await _repoCalendar.UpdateAsync(updatedCalendar, cancellationToken);

        return request.Id;
    }
}