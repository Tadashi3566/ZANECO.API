namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemGetRequest : IRequest<SaleItemDto>
{
    public DefaultIdType Id { get; set; }

    public SaleItemGetRequest(DefaultIdType id) => Id = id;
}

public class GetSaleItemRequestHandler : IRequestHandler<SaleItemGetRequest, SaleItemDto>
{
    private readonly IRepository<SaleItem> _repository;
    private readonly IStringLocalizer _localizer;

    public GetSaleItemRequestHandler(IRepository<SaleItem> repository, IStringLocalizer<GetSaleItemRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<SaleItemDto> Handle(SaleItemGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<SaleItem, SaleItemDto>)new SaleItemByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_localizer["SaleItem {0} Not Found.", request.Id]);
}