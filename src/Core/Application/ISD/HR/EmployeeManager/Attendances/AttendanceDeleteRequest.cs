using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public AttendanceDeleteRequest(DefaultIdType id) => Id = id;
}

public class AttendanceDeleteRequestHandler : IRequestHandler<AttendanceDeleteRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Attendance> _repository;
    private readonly IStringLocalizer<AttendanceDeleteRequestHandler> _localizer;

    public AttendanceDeleteRequestHandler(IRepositoryWithEvents<Attendance> repository, IStringLocalizer<AttendanceDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(AttendanceDeleteRequest request, CancellationToken cancellationToken)
    {
        var attendance = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = attendance ?? throw new NotFoundException(_localizer["Attendance not found."]);

        //await _repository.DeleteAsync(attendance, cancellationToken);

        return request.Id;
    }
}