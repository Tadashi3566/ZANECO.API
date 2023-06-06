using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarCreateRequest : IRequest<DefaultIdType>
{
    public DateTime CalendarDate { get; set; } = default!;
    public string CalendarType { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateCalendarRequestValidator : CustomValidator<CalendarCreateRequest>
{
    public CreateCalendarRequestValidator()
    {
        RuleFor(p => p.CalendarDate)
            .NotEmpty();

        RuleFor(p => p.CalendarType)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty();
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

        var calendar = new Calendar(request.CalendarType, request.CalendarDate, request.Name, request.Description, request.Notes);

        await _repoCalendar.AddAsync(calendar, cancellationToken);

        return calendar.Id;
    }
}