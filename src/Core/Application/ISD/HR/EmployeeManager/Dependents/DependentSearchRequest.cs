using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentSearchRequest : PaginationFilter, IRequest<PaginationResponse<DependentDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class DependentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Dependent, DependentDto>
{
    public DependentsBySearchRequestSpec(DependentSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.Name, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class DependentSearchRequestHandler : IRequestHandler<DependentSearchRequest, PaginationResponse<DependentDto>>
{
    private readonly IReadRepository<Dependent> _repository;

    public DependentSearchRequestHandler(IReadRepository<Dependent> repository) => _repository = repository;

    public async Task<PaginationResponse<DependentDto>> Handle(DependentSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new DependentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}