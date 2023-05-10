namespace ZANECO.API.Application.Catalog.Sales;

public class SaleDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public SaleDeleteRequest(DefaultIdType id) => Id = id;
}

public class DeleteSaleRequestHandler : IRequestHandler<SaleDeleteRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Sale> _repoSale;

    private readonly IReadRepository<SaleItem> _repoSaleItem;
    private readonly IStringLocalizer _localizer;

    public DeleteSaleRequestHandler(IRepositoryWithEvents<Sale> repoSale, IReadRepository<SaleItem> repoSaleItem, IStringLocalizer<DeleteSaleRequestHandler> localizer) =>
        (_repoSale, _repoSaleItem, _localizer) = (repoSale, repoSaleItem, localizer);

    public async Task<DefaultIdType> Handle(SaleDeleteRequest request, CancellationToken cancellationToken)
    {
        //if (await _repoSaleItem.AnyAsync(new SaleByIdSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_localizer["sale cannot be deleted as it's being used."]);
        //}

        var sale = await _repoSale.GetByIdAsync(request.Id, cancellationToken);

        _ = sale ?? throw new NotFoundException(_localizer["sale {0} Not Found."]);

        await _repoSale.DeleteAsync(sale, cancellationToken);

        return request.Id;
    }
}