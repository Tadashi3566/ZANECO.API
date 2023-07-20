using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public ScheduleDetailDeleteRequest(Guid id) => Id = id;
}

public class ScheduleDetailDeleteRequestHandler : IRequestHandler<ScheduleDetailDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<ScheduleDetail> _repository;
    private readonly IStringLocalizer<ScheduleDetailDeleteRequestHandler> _localizer;

    public ScheduleDetailDeleteRequestHandler(IRepositoryWithEvents<ScheduleDetail> repository, IStringLocalizer<ScheduleDetailDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(ScheduleDetailDeleteRequest request, CancellationToken cancellationToken)
    {
        var scheduleDetail = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = scheduleDetail ?? throw new NotFoundException($"ScheduleDetail {request.Id} not found.");

        await _repository.DeleteAsync(scheduleDetail, cancellationToken);

        return request.Id;
    }
}