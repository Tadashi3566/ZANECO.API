using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.SMS.Registrations;

public class RegistrationByAccountSpec : Specification<Master2022, Master2022Dto>, ISingleResultSpecification
{
    public RegistrationByAccountSpec(string acount) => Query.Where(p => p.AccountNumber == acount);
}