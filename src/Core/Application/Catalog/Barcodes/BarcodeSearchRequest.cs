namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeSearchRequest : PaginationFilter, IRequest<PaginationResponse<BarcodeDto>>
{
}

public class BarcodesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Barcode, BarcodeDto>
{
    public BarcodesBySearchRequestSpec(BarcodeSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchBarcodesRequestHandler : IRequestHandler<BarcodeSearchRequest, PaginationResponse<BarcodeDto>>
{
    private readonly IReadRepository<Barcode> _repository;

    public SearchBarcodesRequestHandler(IReadRepository<Barcode> repository) => _repository = repository;

    public async Task<PaginationResponse<BarcodeDto>> Handle(BarcodeSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new BarcodesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}