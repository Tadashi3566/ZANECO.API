using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayByIdSpec : Specification<Barangay, BarangayDto>, ISingleResultSpecification
{
    public BarangayByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}