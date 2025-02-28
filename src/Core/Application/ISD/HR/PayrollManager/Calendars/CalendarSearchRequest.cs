using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

public class CalendarSearchRequest : PaginationFilter, IRequest<PaginationResponse<CalendarDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class CalendarBySearchRequestSpec : EntitiesByPaginationFilterSpec<Calendar, CalendarDto>
{
    public CalendarBySearchRequestSpec(CalendarSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.CalendarDate, !request.HasOrderBy())
        .Where(x => x.CalendarDate >= request.StartDate)
        .Where(x => x.CalendarDate <= request.EndDate);
}

public class CalendarSearchRequestHandler : IRequestHandler<CalendarSearchRequest, PaginationResponse<CalendarDto>>
{
    private readonly IReadRepository<Calendar> _repoCalendar;

    public CalendarSearchRequestHandler(IReadRepository<Calendar> repoCalendar) =>
        _repoCalendar = repoCalendar;

    public async Task<PaginationResponse<CalendarDto>> Handle(CalendarSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new CalendarBySearchRequestSpec(request);
        return await _repoCalendar.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}