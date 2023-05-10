using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactByAccountSpec : Specification<Contact>, ISingleResultSpecification
{
    public ContactByAccountSpec(string account) => Query.Where(p => p.Reference == account);
}