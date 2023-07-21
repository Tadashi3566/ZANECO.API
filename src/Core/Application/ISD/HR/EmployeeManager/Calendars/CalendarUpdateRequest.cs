using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DateTime CalendarDate { get; set; }
    public string CalendarType { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsNationalHoliday { get; set; } = default!;

    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CalendarUpdateRequestValidator : CustomValidator<CalendarUpdateRequest>
{
    public CalendarUpdateRequestValidator(IReadRepository<Calendar> repoCalendar, IStringLocalizer<CalendarUpdateRequestValidator> localizer)
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
        _ = calendar ?? throw new NotFoundException($"Calendar {request.Id} not found.");

        var updatedCalendar = calendar.Update(request.CalendarType, request.CalendarDate!, request.Name, request.IsNationalHoliday, request.Description, request.Notes);

        await _repoCalendar.UpdateAsync(updatedCalendar, cancellationToken);

        return request.Id;
    }
}