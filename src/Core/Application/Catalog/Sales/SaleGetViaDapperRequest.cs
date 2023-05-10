using Mapster;

namespace ZANECO.API.Application.Catalog.Sales;

public class SaleGetViaDapperRequest : IRequest<SaleDto>
{
    public DefaultIdType Id { get; set; }

    public SaleGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class SaleViaDapperGetRequestHandler : IRequestHandler<SaleGetViaDapperRequest, SaleDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<SaleViaDapperGetRequestHandler> _localizer;

    public SaleViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<SaleViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<SaleDto> Handle(SaleGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var sale = await _repository.QueryFirstOrDefaultAsync<Sale>(
            $"SELECT * FROM datazaneco.\"Sales\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = sale ?? throw new NotFoundException(string.Format(_localizer["sale not found."], request.Id));

        return sale.Adapt<SaleDto>();
    }
}