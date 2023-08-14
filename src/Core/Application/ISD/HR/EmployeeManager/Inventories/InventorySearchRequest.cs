using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventorySearchRequest : PaginationFilter, IRequest<PaginationResponse<InventoryDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class InventoryBySearchRequestSpec : EntitiesByPaginationFilterSpec<Inventory, InventoryDto>
{
    public InventoryBySearchRequestSpec(InventorySearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.DateReceived, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class InventorySearchRequestHandler : IRequestHandler<InventorySearchRequest, PaginationResponse<InventoryDto>>
{
    private readonly IReadRepository<Inventory> _repository;

    public InventorySearchRequestHandler(IReadRepository<Inventory> repository) => _repository = repository;

    public async Task<PaginationResponse<InventoryDto>> Handle(InventorySearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new InventoryBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}