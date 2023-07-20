using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailGetViaDapperRequest : IRequest<ScheduleDetailDto>
{
    public DefaultIdType Id { get; set; }

    public ScheduleDetailGetViaDapperRequest(Guid id) => Id = id;
}

public class ScheduleDetailGetViaDapperRequestHandler : IRequestHandler<ScheduleDetailGetViaDapperRequest, ScheduleDetailDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<ScheduleDetailGetViaDapperRequestHandler> _localizer;

    public ScheduleDetailGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<ScheduleDetailGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ScheduleDetailDto> Handle(ScheduleDetailGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var scheduleDetail = await _repository.QueryFirstOrDefaultAsync<ScheduleDetail>($"SELECT * FROM datazaneco.\"ScheduleDetail\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = scheduleDetail ?? throw new NotFoundException($"ScheduleDetail {request.Id} not found.");

        return scheduleDetail.Adapt<ScheduleDetailDto>();
    }
}