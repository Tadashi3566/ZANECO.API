using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactByNameSpec : Specification<Contact>, ISingleResultSpecification
{
    public ContactByNameSpec(string name) => Query.Where(p => p.Name == name);
}