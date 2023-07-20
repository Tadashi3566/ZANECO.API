using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public ScheduleDeleteRequest(Guid id) => Id = id;
}

public class ScheduleDeleteRequestHandler : IRequestHandler<ScheduleDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Schedule> _repository;
    private readonly IStringLocalizer<ScheduleDeleteRequestHandler> _localizer;

    public ScheduleDeleteRequestHandler(IRepositoryWithEvents<Schedule> repository, IStringLocalizer<ScheduleDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(ScheduleDeleteRequest request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = schedule ?? throw new NotFoundException($"Schedule {request.Id} not found.");

        await _repository.DeleteAsync(schedule, cancellationToken);

        return request.Id;
    }
}