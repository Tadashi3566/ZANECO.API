using Mapster;

namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierGetViaDapperRequest : IRequest<SupplierDto>
{
    public DefaultIdType Id { get; set; }

    public SupplierGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class SupplierViaDapperGetRequestHandler : IRequestHandler<SupplierGetViaDapperRequest, SupplierDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<SupplierViaDapperGetRequestHandler> _localizer;

    public SupplierViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<SupplierViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<SupplierDto> Handle(SupplierGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.QueryFirstOrDefaultAsync<Supplier>(
            $"SELECT * FROM datazaneco.\"Suppliers\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = supplier ?? throw new NotFoundException($"supplier {request.Id} not found.");

        return supplier.Adapt<SupplierDto>();
    }
}