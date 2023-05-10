using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceGetRequest : IRequest<AttendanceDto>
{
    public DefaultIdType Id { get; set; }

    public AttendanceGetRequest(DefaultIdType id) => Id = id;
}

public class AttendanceGetRequestHandler : IRequestHandler<AttendanceGetRequest, AttendanceDto>
{
    private readonly IRepository<Attendance> _repository;
    private readonly IStringLocalizer<AttendanceGetRequestHandler> _localizer;

    public AttendanceGetRequestHandler(IRepository<Attendance> repository, IStringLocalizer<AttendanceGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AttendanceDto> Handle(AttendanceGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Attendance, AttendanceDto>)new AttendanceByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Attendance not found."], request.Id));
}