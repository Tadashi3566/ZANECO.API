using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogSearchRequest : PaginationFilter, IRequest<PaginationResponse<TimeLogDto>>
{
    public DefaultIdType? EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class TimeLogsBySearchRequestSpec : EntitiesByPaginationFilterSpec<TimeLog, TimeLogDto>
{
    public TimeLogsBySearchRequestSpec(TimeLogSearchRequest request)
        : base(request) =>
        Query
        .Include(x => x.Employee)
        .OrderBy(x => x.LogDateTime, !request.HasOrderBy())
        .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue)
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