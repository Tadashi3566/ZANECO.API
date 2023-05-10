using Mapster;

namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeGetViaDapperRequest : IRequest<BarcodeDto>
{
    public DefaultIdType Id { get; set; }

    public BarcodeGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class BarcodeViaDapperGetRequestHandler : IRequestHandler<BarcodeGetViaDapperRequest, BarcodeDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<BarcodeViaDapperGetRequestHandler> _localizer;

    public BarcodeViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<BarcodeViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<BarcodeDto> Handle(BarcodeGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var barcode = await _repository.QueryFirstOrDefaultAsync<Barcode>(
            $"SELECT * FROM datazaneco.\"Barcodes\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = barcode ?? throw new NotFoundException(string.Format(_localizer["barcode not found."], request.Id));

        return barcode.Adapt<BarcodeDto>();
    }
}