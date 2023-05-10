using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentByEmployeeTypeSpec : Specification<Adjustment, AdjustmentDto>, ISingleResultSpecification
{
    public AdjustmentByEmployeeTypeSpec(string type) => Query.Where(p => p.EmployeeType == type);
}