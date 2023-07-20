using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentDeleteRequest : IRequest<int>
{
    public int Id { get; set; }

    public AppointmentDeleteRequest(int id) => Id = id;
}

public class AppointmentDeleteRequestHandler : IRequestHandler<AppointmentDeleteRequest, int>
{
    private readonly IRepositoryWithEvents<Appointment> _repository;
    private readonly IStringLocalizer<AppointmentDeleteRequestHandler> _localizer;

    public AppointmentDeleteRequestHandler(IRepositoryWithEvents<Appointment> repository, IStringLocalizer<AppointmentDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<int> Handle(AppointmentDeleteRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = appointment ?? throw new NotFoundException($"appointment {request.Id} not found.");

        await _repository.DeleteAsync(appointment, cancellationToken);

        return request.Id;
    }
}