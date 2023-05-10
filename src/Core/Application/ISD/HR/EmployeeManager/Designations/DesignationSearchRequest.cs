using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationSearchRequest : PaginationFilter, IRequest<PaginationResponse<DesignationDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class DesignationsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Designation, DesignationDto>
{
    public DesignationsBySearchRequestSpec(DesignationSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.StartDate, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class DesignationSearchRequestHandler : IRequestHandler<DesignationSearchRequest, PaginationResponse<DesignationDto>>
{
    private readonly IReadRepository<Designation> _repository;

    public DesignationSearchRequestHandler(IReadRepository<Designation> repository) => _repository = repository;

    public async Task<PaginationResponse<DesignationDto>> Handle(DesignationSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new DesignationsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}