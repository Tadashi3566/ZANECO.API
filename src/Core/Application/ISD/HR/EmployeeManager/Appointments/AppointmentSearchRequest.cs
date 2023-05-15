using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentSearchRequest : PaginationFilter, IRequest<PaginationResponse<AppointmentDto>>
{
    public int? Id { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class AppointmentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Appointment, AppointmentDto>
{
    public AppointmentsBySearchRequestSpec(AppointmentSearchRequest request)
        : base(request) =>
            Query
            .Include(x => x.Employee)
            .Where(x => x.Id.Equals(request.Id), request.Id.HasValue)
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value) || x.RecommendedBy.Equals(request.EmployeeId) || x.ApprovedBy.Equals(request.EmployeeId), request.EmployeeId.HasValue)
            .Where(x => x.StartDateTime >= request.StartDate)
            .Where(x => x.EndDateTime <= request.EndDate)
            .OrderBy(x => x.StartDateTime, !request.HasOrderBy());
}

public class AppointmentSearchRequestHandler : IRequestHandler<AppointmentSearchRequest, PaginationResponse<AppointmentDto>>
{
    private readonly IReadRepository<Appointment> _repository;

    public AppointmentSearchRequestHandler(IReadRepository<Appointment> repository) => _repository = repository;

    public async Task<PaginationResponse<AppointmentDto>> Handle(AppointmentSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AppointmentsBySearchRequestSpec(request);

        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}