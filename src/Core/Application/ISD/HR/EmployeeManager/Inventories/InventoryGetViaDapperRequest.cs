using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryGetViaDapperRequest : IRequest<InventoryDto>
{
    public DefaultIdType Id { get; set; }

    public InventoryGetViaDapperRequest(Guid id) => Id = id;
}

public class InventoryGetViaDapperRequestHandler : IRequestHandler<InventoryGetViaDapperRequest, InventoryDto>
{
    private readonly IDapperRepository _repository;

    public InventoryGetViaDapperRequestHandler(IDapperRepository repository) =>
        _repository = repository;

    public async Task<InventoryDto> Handle(InventoryGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var inventory = await _repository.QueryFirstOrDefaultAsync<Inventory>(
            $"SELECT * FROM datazaneco.\"Inventories\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = inventory ?? throw new NotFoundException($"Inventory {request.Id} not found.");

        return inventory.Adapt<InventoryDto>();
    }
}