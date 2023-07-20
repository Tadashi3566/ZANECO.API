namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public SaleItemDeleteRequest(DefaultIdType id) => Id = id;
}

public class DeleteSaleItemRequestHandler : IRequestHandler<SaleItemDeleteRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SaleItem> _repoSaleItem;

    private readonly IStringLocalizer _localizer;

    public DeleteSaleItemRequestHandler(IRepositoryWithEvents<SaleItem> repoSaleItem, IStringLocalizer<DeleteSaleItemRequestHandler> localizer) =>
        (_repoSaleItem, _localizer) = (repoSaleItem, localizer);

    public async Task<DefaultIdType> Handle(SaleItemDeleteRequest request, CancellationToken cancellationToken)
    {
        //if (await _repoSaleItemItem.AnyAsync(new SaleItemByIdSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_localizer["saleItem cannot be deleted as it's being used."]);
        //}

        var saleItem = await _repoSaleItem.GetByIdAsync(request.Id, cancellationToken);

        _ = saleItem ?? throw new NotFoundException($"saleItem {0} {request.Id} not found.");

        await _repoSaleItem.DeleteAsync(saleItem, cancellationToken);

        return request.Id;
    }
}