using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleGetViaDapperRequest : IRequest<ScheduleDto>
{
    public DefaultIdType Id { get; set; }

    public ScheduleGetViaDapperRequest(Guid id) => Id = id;
}

public class ScheduleGetViaDapperRequestHandler : IRequestHandler<ScheduleGetViaDapperRequest, ScheduleDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<ScheduleGetViaDapperRequestHandler> _localizer;

    public ScheduleGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<ScheduleGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ScheduleDto> Handle(ScheduleGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.QueryFirstOrDefaultAsync<Schedule>($"SELECT * FROM datazaneco.\"Schedule\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = schedule ?? throw new NotFoundException(string.Format(_localizer["Schedule not found."], request.Id));

        return schedule.Adapt<ScheduleDto>();
    }
}