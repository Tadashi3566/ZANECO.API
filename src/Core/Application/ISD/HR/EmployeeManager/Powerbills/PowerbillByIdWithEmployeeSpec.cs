using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillByIdWithEmployeeSpec : Specification<Powerbill, PowerbillDetailsDto>, ISingleResultSpecification
{
    public PowerbillByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}