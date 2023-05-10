using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleGetRequest : IRequest<ScheduleDto>
{
    public DefaultIdType Id { get; set; }

    public ScheduleGetRequest(Guid id) => Id = id;
}

public class ScheduleGetRequestHandler : IRequestHandler<ScheduleGetRequest, ScheduleDto>
{
    private readonly IRepository<Schedule> _repository;
    private readonly IStringLocalizer<ScheduleGetRequestHandler> _localizer;

    public ScheduleGetRequestHandler(IRepository<Schedule> repository, IStringLocalizer<ScheduleGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ScheduleDto> Handle(ScheduleGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Schedule, ScheduleDto>)new ScheduleByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Schedule not found."], request.Id));
}