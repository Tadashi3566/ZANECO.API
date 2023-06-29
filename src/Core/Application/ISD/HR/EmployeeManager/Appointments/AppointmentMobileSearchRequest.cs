using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentMobileSearchRequest : PaginationFilter, IRequest<List<AppointmentDto>>
{
    public int? Id { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public sealed class AppointmentsByMobileSearchRequestSpec : EntitiesByBaseFilterSpec<Appointment, AppointmentDto>
{
    public AppointmentsByMobileSearchRequestSpec(AppointmentMobileSearchRequest request)
        : base(request) =>
            Query
            .Include(x => x.Employee)
            .Where(x => x.Id.Equals(request.Id), request.Id.HasValue)
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value) || x.RecommendedBy.Equals(request.EmployeeId) || x.ApprovedBy.Equals(request.EmployeeId), request.EmployeeId.HasValue)
            .Where(x => x.StartDateTime >= request.StartDate)
            .Where(x => x.EndDateTime <= request.EndDate)
            .OrderBy(x => x.StartDateTime);
}

public class AppointmentMobileSearchRequestHandler : IRequestHandler<AppointmentMobileSearchRequest, List<AppointmentDto>>
{
    private readonly IReadRepository<Appointment> _repository;

    public AppointmentMobileSearchRequestHandler(IReadRepository<Appointment> repository) => _repository = repository;

    public async Task<List<AppointmentDto>> Handle(AppointmentMobileSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AppointmentsByMobileSearchRequestSpec(request);
        var result = await _repository.ListAsync(spec, cancellationToken);
        return result.ToList();
    }
}