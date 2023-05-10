using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarGetViaDapperRequest : IRequest<CalendarDto>
{
    public DefaultIdType Id { get; set; }

    public CalendarGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class CalendarGetViaDapperRequestHandler : IRequestHandler<CalendarGetViaDapperRequest, CalendarDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<CalendarGetViaDapperRequestHandler> _localizer;

    public CalendarGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<CalendarGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CalendarDto> Handle(CalendarGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.QueryFirstOrDefaultAsync<Calendar>(
        $"SELECT * FROM datazaneco.\"Calendar\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = calendar ?? throw new NotFoundException(string.Format(_localizer["Calendar not found."], request.Id));

        return calendar.Adapt<CalendarDto>();
    }
}