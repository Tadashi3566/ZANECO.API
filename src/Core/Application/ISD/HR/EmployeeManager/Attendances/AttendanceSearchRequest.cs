using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceSearchRequest : PaginationFilter, IRequest<PaginationResponse<AttendanceDto>>
{
    public DefaultIdType? EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public sealed class AttendanceBySearchRequestSpec : EntitiesByPaginationFilterSpec<Attendance, AttendanceDto>
{
    public AttendanceBySearchRequestSpec(AttendanceSearchRequest request)
        : base(request) =>
        Query
        .Include(x => x.Employee)
        .OrderBy(x => x.AttendanceDate, !request.HasOrderBy())
        .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue)
        .Where(x => x.AttendanceDate >= request.StartDate)
        .Where(x => x.AttendanceDate <= request.EndDate);
}

public class AttendanceSearchRequestHandler : IRequestHandler<AttendanceSearchRequest, PaginationResponse<AttendanceDto>>
{
    private readonly IReadRepository<Attendance> _repoAttendance;

    public AttendanceSearchRequestHandler(IReadRepository<Attendance> repoAttendance) =>
        _repoAttendance = repoAttendance;

    public async Task<PaginationResponse<AttendanceDto>> Handle(AttendanceSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AttendanceBySearchRequestSpec(request);

        return await _repoAttendance.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}