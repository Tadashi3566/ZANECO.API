using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarGetRequest : IRequest<CalendarDto>
{
    public DefaultIdType Id { get; set; }

    public CalendarGetRequest(DefaultIdType id) => Id = id;
}

public class CalendarGetRequestHandler : IRequestHandler<CalendarGetRequest, CalendarDto>
{
    private readonly IRepository<Calendar> _repository;
    private readonly IStringLocalizer<CalendarGetRequestHandler> _localizer;

    public CalendarGetRequestHandler(IRepository<Calendar> repository, IStringLocalizer<CalendarGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CalendarDto> Handle(CalendarGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Calendar, CalendarDto>)new CalendarByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Calendar not found."], request.Id));
}