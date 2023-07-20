using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceGetViaDapperRequest : IRequest<AttendanceDto>
{
    public DefaultIdType Id { get; set; }

    public AttendanceGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class AttendanceGetViaDapperRequestHandler : IRequestHandler<AttendanceGetViaDapperRequest, AttendanceDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<AttendanceGetViaDapperRequestHandler> _localizer;

    public AttendanceGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<AttendanceGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AttendanceDto> Handle(AttendanceGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var attendance = await _repository.QueryFirstOrDefaultAsync<Attendance>(
        $"SELECT * FROM datazaneco.\"Attendance\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = attendance ?? throw new NotFoundException($"Attendance {request.Id} not found.");

        return attendance.Adapt<AttendanceDto>();
    }
}