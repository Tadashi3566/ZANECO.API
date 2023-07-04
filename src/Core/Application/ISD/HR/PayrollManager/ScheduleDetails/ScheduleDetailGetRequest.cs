using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailGetRequest : IRequest<ScheduleDetailDto>
{
    public DefaultIdType Id { get; set; }

    public ScheduleDetailGetRequest(Guid id) => Id = id;
}

public class ScheduleDetailGetRequestHandler : IRequestHandler<ScheduleDetailGetRequest, ScheduleDetailDto>
{
    private readonly IRepository<ScheduleDetail> _repository;
    private readonly IStringLocalizer<ScheduleDetailGetRequestHandler> _localizer;

    public ScheduleDetailGetRequestHandler(IRepository<ScheduleDetail> repository, IStringLocalizer<ScheduleDetailGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ScheduleDetailDto> Handle(ScheduleDetailGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(new ScheduleDetailByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["ScheduleDetail not found."], request.Id));
}