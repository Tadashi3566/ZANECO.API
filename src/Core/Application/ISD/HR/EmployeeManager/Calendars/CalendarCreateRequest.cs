using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType EmployeeId { get; set; } = default!;

    public DateTime CalendarDate { get; set; } = default!;
    public string CalendarType { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class CreateCalendarRequestValidator : CustomValidator<CalendarCreateRequest>
{
    public CreateCalendarRequestValidator(IReadRepository<Calendar> repoCalendar, IStringLocalizer<CreateCalendarRequestValidator> localizer)
    {
        //RuleFor(p => p.EmployeeId)
        //    .NotEmpty();

        RuleFor(p => p.CalendarDate)
            .NotEmpty();

        RuleFor(p => p.CalendarType)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty();

        // .MustAsync(async (name, ct) => await repoCalendar.FirstOrDefaultAsync(new CalendarByNameSpec(name), ct) is null)
        // .WithMessage((_, name) => string.Format(localizer["adjustment already exists"], name));
    }
}

public class CalendarCreateRequestHandler : IRequestHandler<CalendarCreateRequest, DefaultIdType>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Domain.App.Group> _repoGroup;
    private readonly IRepositoryWithEvents<Calendar> _repoCalendar;

    public CalendarCreateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Domain.App.Group> repoGroup, IRepositoryWithEvents<Calendar> repoCalendar) =>
        (_repoEmployee, _repoGroup, _repoCalendar) = (repoEmployee, repoGroup, repoCalendar);

    public async Task<DefaultIdType> Handle(CalendarCreateRequest request, CancellationToken cancellationToken)
    {
        var checkCalendar = await _repoCalendar.FirstOrDefaultAsync(new CalendarByNameSpec(request.CalendarDate, request.Name), cancellationToken);
        if (checkCalendar is not null)
        {
            throw new NotFoundException("Name already exist");
        }

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

        //string description = request.Description;

        //if (request.Description.Equals(string.Empty))
        //{
        //    var group = await _repoGroup.FirstOrDefaultAsync(new GroupByNameSpec(request.Subject), cancellationToken);
        //    if (group is not null) description = group.Description!;
        //}

        var calendar = new Calendar(request.CalendarType, request.CalendarDate, request.Name, request.Description, request.Notes);

        await _repoCalendar.AddAsync(calendar, cancellationToken);

        return calendar.Id;
    }
}