namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierGetRequest : IRequest<SupplierDto>
{
    public DefaultIdType Id { get; set; }

    public SupplierGetRequest(DefaultIdType id) => Id = id;
}

public class GetSupplierRequestHandler : IRequestHandler<SupplierGetRequest, SupplierDto>
{
    private readonly IRepository<Supplier> _repository;
    private readonly IStringLocalizer _localizer;

    public GetSupplierRequestHandler(IRepository<Supplier> repository, IStringLocalizer<GetSupplierRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<SupplierDto> Handle(SupplierGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Supplier, SupplierDto>)new SupplierByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_localizer["Supplier {0} Not Found.", request.Id]);
}