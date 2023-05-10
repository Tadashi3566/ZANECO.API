using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentByIdSpec : Specification<Document, DocumentDto>, ISingleResultSpecification
{
    public DocumentByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}