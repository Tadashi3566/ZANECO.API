using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public TimeLogDeleteRequest(DefaultIdType id) => Id = id;
}

public class TimeLogDeleteRequestHandler : IRequestHandler<TimeLogDeleteRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<TimeLog> _repository;
    private readonly IStringLocalizer<TimeLogDeleteRequestHandler> _localizer;

    public TimeLogDeleteRequestHandler(IRepositoryWithEvents<TimeLog> repository, IStringLocalizer<TimeLogDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(TimeLogDeleteRequest request, CancellationToken cancellationToken)
    {
        var timeLog = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = timeLog ?? throw new NotFoundException(_localizer["timeLog not found."]);

        await _repository.DeleteAsync(timeLog, cancellationToken);

        return request.Id;
    }
}