using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayByNameSpec : Specification<Barangay>, ISingleResultSpecification<Barangay>
{
    public BarangayByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}