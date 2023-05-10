namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeGetRequest : IRequest<BarcodeDto>
{
    public DefaultIdType Id { get; set; }

    public BarcodeGetRequest(DefaultIdType id) => Id = id;
}

public class GetBarcodeRequestHandler : IRequestHandler<BarcodeGetRequest, BarcodeDto>
{
    private readonly IRepository<Barcode> _repository;
    private readonly IStringLocalizer _localizer;

    public GetBarcodeRequestHandler(IRepository<Barcode> repository, IStringLocalizer<GetBarcodeRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<BarcodeDto> Handle(BarcodeGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Barcode, BarcodeDto>)new BarcodeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_localizer["Barcode {0} Not Found.", request.Id]);
}