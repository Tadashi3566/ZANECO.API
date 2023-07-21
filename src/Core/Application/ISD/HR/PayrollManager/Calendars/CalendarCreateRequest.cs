using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

public class CalendarCreateRequest : IRequest<DefaultIdType>
{
    public DateTime CalendarDate { get; set; } = default!;
    public string CalendarType { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsNationalHoliday { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateCalendarRequestValidator : CustomValidator<CalendarCreateRequest>
{
    public CreateCalendarRequestValidator()
    {
        RuleFor(p => p.CalendarDate)
            .NotEmpty()
            .LessThan(DateTime.Today.AddYears(-2));

        RuleFor(p => p.CalendarType)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(16);

        RuleFor(p => p.Name)
            .NotEmpty();
    }
}

public class CalendarCreateRequestHandler : IRequestHandler<CalendarCreateRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Calendar> _repoCalendar;
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;
    private readonly IDapperRepository _repoDapper;

    public CalendarCreateRequestHandler(IRepositoryWithEvents<Calendar> repoCalendar, IRepositoryWithEvents<Attendance> repoAttendance, IDapperRepository repoDapper) =>
        (_repoCalendar, _repoAttendance, _repoDapper) = (repoCalendar, repoAttendance, repoDapper);

    public async Task<DefaultIdType> Handle(CalendarCreateRequest request, CancellationToken cancellationToken)
    {
        var checkCalendar = await _repoCalendar.FirstOrDefaultAsync(new CalendarByNameSpec(request.CalendarDate, request.Name), cancellationToken);
        if (checkCalendar is not null)
        {
            throw new NotFoundException("Name already exist");
        }

        var calendar = new Calendar(request.CalendarType, request.CalendarDate, request.Name, request.IsNationalHoliday, request.Description, request.Notes);

        await _repoCalendar.AddAsync(calendar, cancellationToken);

        await _repoDapper.ExecuteScalarAsync<Calendar>(
            $"UPDATE datazaneco.Attendance SET DayType = 'HOLIDAY', Description = '{request.Name}' WHERE TenantId = '@tenant' AND AttendanceDate = '{request.CalendarDate:yyyy-MM-dd}'", cancellationToken: cancellationToken);

        return calendar.Id;
    }
}