using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactByIdSpec : Specification<Contact, ContactDto>, ISingleResultSpecification
{
    public ContactByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}