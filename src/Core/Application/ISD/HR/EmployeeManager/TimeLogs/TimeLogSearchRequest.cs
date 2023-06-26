using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogSearchRequest : PaginationFilter, IRequest<PaginationResponse<TimeLogDto>>
{
    public DefaultIdType? EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public sealed class TimeLogsBySearchRequestSpec : EntitiesByPaginationFilterSpec<TimeLog, TimeLogDto>
{
    public TimeLogsBySearchRequestSpec(TimeLogSearchRequest request)
        : base(request) =>
        Query
        .Include(x => x.Employee)
        .OrderBy(x => x.LogDateTime)
        .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue)
    //.Where(x => x.LogDate >= new DateTime(request.StartDate.Year, request.StartDate.Month, request.StartDate.Day, 0, 0, 0))
    //.Where(x => x.LogDate <= new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day, 23, 59, 59));
    //.Where(x => x.LogDate >= request.StartDate.AddDays(-1))
    //.Where(x => x.LogDate <= request.EndDate);
    .Where(x => x.LogDate >= request.StartDate)
    .Where(x => x.LogDate <= request.EndDate);
}

public class TimeLogSearchRequestHandler : IRequestHandler<TimeLogSearchRequest, PaginationResponse<TimeLogDto>>
{
    private readonly IReadRepository<TimeLog> _repository;

    public TimeLogSearchRequestHandler(IReadRepository<TimeLog> repository) => _repository = repository;

    public async Task<PaginationResponse<TimeLogDto>> Handle(TimeLogSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new TimeLogsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}