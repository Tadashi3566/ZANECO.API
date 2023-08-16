using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryGetRequest : IRequest<InventoryDto>
{
    public DefaultIdType Id { get; set; }

    public InventoryGetRequest(Guid id) => Id = id;
}

public class InventoryGetRequestHandler : IRequestHandler<InventoryGetRequest, InventoryDto>
{
    private readonly IRepository<Inventory> _repository;

    public InventoryGetRequestHandler(IRepository<Inventory> repository) =>
        _repository = repository;

    public async Task<InventoryDto> Handle(InventoryGetRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.FirstOrDefaultAsync(new InventoryByIdSpec(request.Id), cancellationToken);

        return result is null ? throw new NotFoundException($"Inventory {request.Id} not found.") : result;
    }

    //public async Task<InventoryDto> Handle(InventoryGetRequest request, CancellationToken cancellationToken) =>
    //    await _repository.FirstOrDefaultAsync(new InventoryByIdSpec(request.Id), cancellationToken)
    //    ?? throw new NotFoundException($"Inventory {request.Id} not found.");
}