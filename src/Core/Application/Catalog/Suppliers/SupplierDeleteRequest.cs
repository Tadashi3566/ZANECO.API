namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public SupplierDeleteRequest(DefaultIdType id) => Id = id;
}

public class DeleteSupplierRequestHandler : IRequestHandler<SupplierDeleteRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _repository;

    private readonly IStringLocalizer _localizer;

    public DeleteSupplierRequestHandler(IRepositoryWithEvents<Supplier> repository, IStringLocalizer<DeleteSupplierRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(SupplierDeleteRequest request, CancellationToken cancellationToken)
    {
        //if (await _repoSupplierItem.AnyAsync(new SupplierByIdSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_localizer["supplier cannot be deleted as it's being used."]);
        //}

        var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier ?? throw new NotFoundException($"supplier {0} {request.Id} not found.");

        await _repository.DeleteAsync(supplier, cancellationToken);

        return request.Id;
    }
}