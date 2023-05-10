using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerSearchRequest : PaginationFilter, IRequest<PaginationResponse<EmployerDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class EmployersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Employer, EmployerDto>
{
    public EmployersBySearchRequestSpec(EmployerSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.Name, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class EmployerSearchRequestHandler : IRequestHandler<EmployerSearchRequest, PaginationResponse<EmployerDto>>
{
    private readonly IReadRepository<Employer> _repository;

    public EmployerSearchRequestHandler(IReadRepository<Employer> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployerDto>> Handle(EmployerSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}