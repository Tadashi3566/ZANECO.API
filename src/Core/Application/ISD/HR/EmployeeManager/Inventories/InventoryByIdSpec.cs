using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryByIdSpec : Specification<Inventory, InventoryDto>, ISingleResultSpecification<Inventory>
{
    public InventoryByIdSpec(Guid id)
    {
        Query.Where(p => p.Id == id);
    }
}