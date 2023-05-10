using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleSearchRequest : PaginationFilter, IRequest<PaginationResponse<ScheduleDto>>
{
}

public class ScheduleBySearchRequestSpec : EntitiesByPaginationFilterSpec<Schedule, ScheduleDto>
{
    public ScheduleBySearchRequestSpec(ScheduleSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class ScheduleSearchRequestHandler : IRequestHandler<ScheduleSearchRequest, PaginationResponse<ScheduleDto>>
{
    private readonly IReadRepository<Schedule> _repository;

    public ScheduleSearchRequestHandler(IReadRepository<Schedule> repository) => _repository = repository;

    public async Task<PaginationResponse<ScheduleDto>> Handle(ScheduleSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new ScheduleBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}