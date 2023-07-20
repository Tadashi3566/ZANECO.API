namespace ZANECO.API.Application.Catalog.Sales;

public class SaleGetRequest : IRequest<SaleDto>
{
    public DefaultIdType Id { get; set; }

    public SaleGetRequest(DefaultIdType id) => Id = id;
}

public class GetSaleRequestHandler : IRequestHandler<SaleGetRequest, SaleDto>
{
    private readonly IRepository<Sale> _repository;
    private readonly IStringLocalizer _localizer;

    public GetSaleRequestHandler(IRepository<Sale> repository, IStringLocalizer<GetSaleRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<SaleDto> Handle(SaleGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Sale, SaleDto>)new SaleByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Sale {request.Id} not found.");
}