using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentGetRequest : IRequest<AppointmentDto>
{
    public int Id { get; set; }

    public AppointmentGetRequest(int id) => Id = id;
}

public class AppointmentGetRequestHandler : IRequestHandler<AppointmentGetRequest, AppointmentDto>
{
    private readonly IRepository<Appointment> _repository;
    private readonly IStringLocalizer<AppointmentGetRequestHandler> _localizer;

    public AppointmentGetRequestHandler(IRepository<Appointment> repository, IStringLocalizer<AppointmentGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AppointmentDto> Handle(AppointmentGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new AppointmentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Appointment {request.Id} not found.");
}