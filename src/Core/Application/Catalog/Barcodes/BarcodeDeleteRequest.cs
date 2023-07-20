namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public BarcodeDeleteRequest(DefaultIdType id) => Id = id;
}

public class DeleteBarcodeRequestHandler : IRequestHandler<BarcodeDeleteRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Barcode> _repoBarcode;

    private readonly IReadRepository<SaleItem> _repoSaleItem;
    private readonly IStringLocalizer _localizer;

    public DeleteBarcodeRequestHandler(IRepositoryWithEvents<Barcode> BarcodeRepo, IReadRepository<SaleItem> repoSaleItem, IStringLocalizer<DeleteBarcodeRequestHandler> localizer) =>
        (_repoBarcode, _repoSaleItem, _localizer) = (BarcodeRepo, repoSaleItem, localizer);

    public async Task<DefaultIdType> Handle(BarcodeDeleteRequest request, CancellationToken cancellationToken)
    {
        //if (await _repoSaleItem.AnyAsync(new BarcodeByIdSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_localizer["barcode cannot be deleted as it's being used."]);
        //}

        var barcode = await _repoBarcode.GetByIdAsync(request.Id, cancellationToken);

        _ = barcode ?? throw new NotFoundException($"barcode {0} {request.Id} not found.");

        await _repoBarcode.DeleteAsync(barcode, cancellationToken);

        return request.Id;
    }
}