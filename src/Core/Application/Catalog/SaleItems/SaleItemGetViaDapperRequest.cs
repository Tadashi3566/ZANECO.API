using Mapster;

namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemGetViaDapperRequest : IRequest<SaleItemDto>
{
    public DefaultIdType Id { get; set; }

    public SaleItemGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class SaleItemViaDapperGetRequestHandler : IRequestHandler<SaleItemGetViaDapperRequest, SaleItemDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<SaleItemViaDapperGetRequestHandler> _localizer;

    public SaleItemViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<SaleItemViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<SaleItemDto> Handle(SaleItemGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var saleItem = await _repository.QueryFirstOrDefaultAsync<SaleItem>(
            $"SELECT * FROM datazaneco.\"SaleItems\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = saleItem ?? throw new NotFoundException($"saleItem {request.Id} not found.");

        return saleItem.Adapt<SaleItemDto>();
    }
}