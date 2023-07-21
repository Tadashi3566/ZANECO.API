using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarMobileSearchRequest : PaginationFilter, IRequest<List<CalendarDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class CalendarByMobileSearchRequestSpec : EntitiesByPaginationFilterSpec<Calendar, CalendarDto>
{
    public CalendarByMobileSearchRequestSpec(CalendarMobileSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.CalendarDate, !request.HasOrderBy())
        .Where(x => x.CalendarDate >= request.StartDate)
        .Where(x => x.CalendarDate <= request.EndDate);
}

public class CalendarMobileSearchRequestHandler : IRequestHandler<CalendarMobileSearchRequest, List<CalendarDto>>
{
    private readonly IReadRepository<Calendar> _repoCalendar;

    public CalendarMobileSearchRequestHandler(IReadRepository<Calendar> repoCalendar) =>
        _repoCalendar = repoCalendar;

    public async Task<List<CalendarDto>> Handle(CalendarMobileSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new CalendarByMobileSearchRequestSpec(request);
        return await _repoCalendar.ListAsync(spec, cancellationToken);
    }
}