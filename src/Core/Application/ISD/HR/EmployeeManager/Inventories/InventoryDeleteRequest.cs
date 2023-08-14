using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public InventoryDeleteRequest(Guid id) => Id = id;
}

public class InventoryDeleteRequestHandler : IRequestHandler<InventoryDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Inventory> _repository;

    public InventoryDeleteRequestHandler(IRepositoryWithEvents<Inventory> repository) =>
        _repository = repository;

    public async Task<Guid> Handle(InventoryDeleteRequest request, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = inventory ?? throw new NotFoundException($"Inventory {request.Id} not found.");

        await _repository.DeleteAsync(inventory, cancellationToken);

        return request.Id;
    }
}