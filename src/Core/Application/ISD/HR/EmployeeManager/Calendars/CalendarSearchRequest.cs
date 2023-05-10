using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarSearchRequest : PaginationFilter, IRequest<PaginationResponse<CalendarDto>>
{
}

public class CalendarBySearchRequestSpec : EntitiesByPaginationFilterSpec<Calendar, CalendarDto>
{
    public CalendarBySearchRequestSpec(CalendarSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.CalendarDate, !request.HasOrderBy());
}

public class CalendarSearchRequestHandler : IRequestHandler<CalendarSearchRequest, PaginationResponse<CalendarDto>>
{
    private readonly IReadRepository<Calendar> _repoCalendar;

    public CalendarSearchRequestHandler(IReadRepository<Calendar> repoCalendar) =>
        _repoCalendar = repoCalendar;

    public async Task<PaginationResponse<CalendarDto>> Handle(CalendarSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new CalendarBySearchRequestSpec(request);
        var result = await _repoCalendar.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        return result;
    }
}