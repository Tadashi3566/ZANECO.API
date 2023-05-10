namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierSearchRequest : PaginationFilter, IRequest<PaginationResponse<SupplierDto>>
{
}

public class SuppliersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Supplier, SupplierDto>
{
    public SuppliersBySearchRequestSpec(SupplierSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchSuppliersRequestHandler : IRequestHandler<SupplierSearchRequest, PaginationResponse<SupplierDto>>
{
    private readonly IReadRepository<Supplier> _repository;

    public SearchSuppliersRequestHandler(IReadRepository<Supplier> repository) => _repository = repository;

    public async Task<PaginationResponse<SupplierDto>> Handle(SupplierSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new SuppliersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}