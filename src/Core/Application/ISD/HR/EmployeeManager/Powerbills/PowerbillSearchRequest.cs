using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillSearchRequest : PaginationFilter, IRequest<PaginationResponse<PowerbillDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class PowerbillsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Powerbill, PowerbillDto>
{
    public PowerbillsBySearchRequestSpec(PowerbillSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.Name, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class PowerbillSearchRequestHandler : IRequestHandler<PowerbillSearchRequest, PaginationResponse<PowerbillDto>>
{
    private readonly IReadRepository<Powerbill> _repository;

    public PowerbillSearchRequestHandler(IReadRepository<Powerbill> repository) => _repository = repository;

    public async Task<PaginationResponse<PowerbillDto>> Handle(PowerbillSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new PowerbillsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}