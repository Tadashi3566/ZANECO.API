namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemSearchRequest : PaginationFilter, IRequest<PaginationResponse<SaleItemDto>>
{
    public DefaultIdType? SaleId { get; set; }
}

public class SaleItemsBySearchRequestSpec : EntitiesByPaginationFilterSpec<SaleItem, SaleItemDto>
{
    public SaleItemsBySearchRequestSpec(SaleItemSearchRequest request)
        : base(request) =>
         Query
            .Include(x => x.Sale)
            .OrderBy(x => x.Name, !request.HasOrderBy())
            .Where(x => x.SaleId.Equals(request.SaleId!.Value), request.SaleId.HasValue);
}

public class SearchSaleItemsRequestHandler : IRequestHandler<SaleItemSearchRequest, PaginationResponse<SaleItemDto>>
{
    private readonly IReadRepository<SaleItem> _repository;

    public SearchSaleItemsRequestHandler(IReadRepository<SaleItem> repository) => _repository = repository;

    public async Task<PaginationResponse<SaleItemDto>> Handle(SaleItemSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new SaleItemsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}