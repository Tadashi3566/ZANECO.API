using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactByNumberSpec : Specification<Contact>, ISingleResultSpecification
{
    public ContactByNumberSpec(string number) => Query.Where(p => p.PhoneNumber == number);
}