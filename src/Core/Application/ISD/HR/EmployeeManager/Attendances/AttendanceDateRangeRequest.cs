using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceDateRangeRequest : PaginationFilter, IRequest<List<AttendanceDto>>
{
    public DefaultIdType EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class AttendanceByDateRangeRequestSpec : EntitiesByBaseFilterSpec<Attendance, AttendanceDto>
{
    public AttendanceByDateRangeRequestSpec(AttendanceDateRangeRequest request)
        : base(request) =>
        Query
        .Include(x => x.Employee)
        .OrderBy(x => x.AttendanceDate)
        .Where(x => x.EmployeeId.Equals(request.EmployeeId))
        .Where(x => x.AttendanceDate >= request.StartDate)
        .Where(x => x.AttendanceDate <= request.EndDate);
}

public class AttendanceDateRangeRequestHandler : IRequestHandler<AttendanceDateRangeRequest, List<AttendanceDto>>
{
    private readonly IReadRepository<Attendance> _repoAttendance;

    public AttendanceDateRangeRequestHandler(IReadRepository<Attendance> repoAttendance) =>
        _repoAttendance = repoAttendance;

    public async Task<List<AttendanceDto>> Handle(AttendanceDateRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new AttendanceByDateRangeRequestSpec(request);

        var result = await _repoAttendance.ListAsync(spec, cancellationToken);

        return result.ToList();
    }
}
