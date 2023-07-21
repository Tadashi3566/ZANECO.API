using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

public class CalendarDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public CalendarDeleteRequest(DefaultIdType id) => Id = id;
}

public class CalendarDeleteRequestHandler : IRequestHandler<CalendarDeleteRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Calendar> _repository;
    private readonly IStringLocalizer<CalendarDeleteRequestHandler> _localizer;

    public CalendarDeleteRequestHandler(IRepositoryWithEvents<Calendar> repository, IStringLocalizer<CalendarDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(CalendarDeleteRequest request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = calendar ?? throw new NotFoundException($"Calendar {request.Id} not found.");

        await _repository.DeleteAsync(calendar, cancellationToken);

        return request.Id;
    }
}