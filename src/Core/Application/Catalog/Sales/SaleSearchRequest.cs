namespace ZANECO.API.Application.Catalog.Sales;

public class SaleSearchRequest : PaginationFilter, IRequest<PaginationResponse<SaleDto>>
{
}

public class SalesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Sale, SaleDto>
{
    public SalesBySearchRequestSpec(SaleSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Date, !request.HasOrderBy());
}

public class SearchSalesRequestHandler : IRequestHandler<SaleSearchRequest, PaginationResponse<SaleDto>>
{
    private readonly IReadRepository<Sale> _repository;

    public SearchSalesRequestHandler(IReadRepository<Sale> repository) => _repository = repository;

    public async Task<PaginationResponse<SaleDto>> Handle(SaleSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new SalesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}