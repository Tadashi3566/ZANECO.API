using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentByNameSpec : Specification<Document>, ISingleResultSpecification
{
    public DocumentByNameSpec(string name) => Query.Where(p => p.Name == name);
}