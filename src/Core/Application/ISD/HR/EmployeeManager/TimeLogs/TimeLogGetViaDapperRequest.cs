using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogGetViaDapperRequest : IRequest<TimeLogDto>
{
    public DefaultIdType Id { get; set; }

    public TimeLogGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class TimeLogGetViaDapperRequestHandler : IRequestHandler<TimeLogGetViaDapperRequest, TimeLogDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<TimeLogGetViaDapperRequestHandler> _localizer;

    public TimeLogGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<TimeLogGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TimeLogDto> Handle(TimeLogGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var timeLog = await _repository.QueryFirstOrDefaultAsync<TimeLog>(
            $"SELECT * FROM datazaneco.\"TimeLogs\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = timeLog ?? throw new NotFoundException($"timeLog {request.Id} not found.");

        return timeLog.Adapt<TimeLogDto>();
    }
}