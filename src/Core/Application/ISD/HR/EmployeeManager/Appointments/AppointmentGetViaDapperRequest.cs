using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentGetViaDapperRequest : IRequest<AppointmentDto>
{
    public int Id { get; set; }

    public AppointmentGetViaDapperRequest(int id) => Id = id;
}

public class AppointmentGetViaDapperRequestHandler : IRequestHandler<AppointmentGetViaDapperRequest, AppointmentDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<AppointmentGetViaDapperRequestHandler> _localizer;

    public AppointmentGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<AppointmentGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AppointmentDto> Handle(AppointmentGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.QueryFirstOrDefaultAsync<Appointment>(
            $"SELECT * FROM datazaneco.\"Appointments\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = appointment ?? throw new NotFoundException($"appointment {request.Id} not found.");

        return appointment.Adapt<AppointmentDto>();
    }
}