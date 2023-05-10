using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactsByAccountSpec : Specification<Contact>
{
    public ContactsByAccountSpec(string account) => Query.Where(p => p.Reference == account);
}