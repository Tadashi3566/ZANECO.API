using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactByIdSpec : Specification<Contact, ContactDto>, ISingleResultSpecification<Contact>
{
    public ContactByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}