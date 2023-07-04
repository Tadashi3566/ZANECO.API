using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogGetRequest : IRequest<TimeLogDto>
{
    public DefaultIdType Id { get; set; }

    public TimeLogGetRequest(DefaultIdType id) => Id = id;
}

public class TimeLogGetRequestHandler : IRequestHandler<TimeLogGetRequest, TimeLogDto>
{
    private readonly IRepository<TimeLog> _repository;
    private readonly IStringLocalizer<TimeLogGetRequestHandler> _localizer;

    public TimeLogGetRequestHandler(IRepository<TimeLog> repository, IStringLocalizer<TimeLogGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TimeLogDto> Handle(TimeLogGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new TimeLogByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["TimeLog not found."], request.Id));
}