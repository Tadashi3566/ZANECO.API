using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactByNumberSpec : Specification<Contact>, ISingleResultSpecification<Contact>
{
    public ContactByNumberSpec(string number) =>
        Query.Where(p => p.PhoneNumber == number);
}