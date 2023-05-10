using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailSearchRequest : PaginationFilter, IRequest<PaginationResponse<ScheduleDetailDto>>
{
    public Guid? ScheduleId { get; set; }
}

public class ScheduleDetailBySearchRequestSpec : EntitiesByPaginationFilterSpec<ScheduleDetail, ScheduleDetailDto>
{
    public ScheduleDetailBySearchRequestSpec(ScheduleDetailSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Schedule)
            .OrderBy(x => x.Day, !request.HasOrderBy())
            .Where(x => x.ScheduleId.Equals(request.ScheduleId!.Value), request.ScheduleId.HasValue);
}

public class ScheduleDetailSearchRequestHandler : IRequestHandler<ScheduleDetailSearchRequest, PaginationResponse<ScheduleDetailDto>>
{
    private readonly IReadRepository<ScheduleDetail> _repository;

    public ScheduleDetailSearchRequestHandler(IReadRepository<ScheduleDetail> repository) => _repository = repository;

    public async Task<PaginationResponse<ScheduleDetailDto>> Handle(ScheduleDetailSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new ScheduleDetailBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}