namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountSearchRequest : PaginationFilter, IRequest<PaginationResponse<DiscountDto>>
{
}

public class DiscountsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Discount, DiscountDto>
{
    public DiscountsBySearchRequestSpec(DiscountSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchDiscountsRequestHandler : IRequestHandler<DiscountSearchRequest, PaginationResponse<DiscountDto>>
{
    private readonly IReadRepository<Discount> _repository;

    public SearchDiscountsRequestHandler(IReadRepository<Discount> repository) => _repository = repository;

    public async Task<PaginationResponse<DiscountDto>> Handle(DiscountSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new DiscountsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}